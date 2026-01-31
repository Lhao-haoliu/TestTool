using ClosedXML.Excel;
using ClosedXML.Excel.Drawings;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestTool
{
    public partial class BatchRunForm : Form
    {
        private static readonly HttpClient _http = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(60)
        };

        private readonly BindingList<BatchRow> _rows = new BindingList<BatchRow>();
        private CancellationTokenSource _cts;

        public BatchRunForm()
        {
            InitializeComponent();

            dgv.AutoGenerateColumns = false;
            dgv.DataSource = _rows;

            txtUrl.Text = "http://localhost:3882/similarity-check";
            txtModelKey.Text = "Dummy";

            rbSingle.Checked = true;
            rbOnlyImage.Checked = true;

            ApplyModeUi();
        }

        #region UI

        private void btnPickPublicFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() != DialogResult.OK) return;
                txtPublicFolder.Text = fbd.SelectedPath;
            }
        }

        private void btnPickTargetFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() != DialogResult.OK) return;
                txtTargetFolder.Text = fbd.SelectedPath;
            }
        }

        private void rbSingle_CheckedChanged(object sender, EventArgs e) => ApplyModeUi();
        private void rbCompare_CheckedChanged(object sender, EventArgs e) => ApplyModeUi();

        private void ApplyModeUi()
        {
            bool isCompare = rbCompare.Checked;

            rbOnlyImage.Enabled = !isCompare;
            rbImageModel.Enabled = !isCompare;

            txtModelKey.Enabled = isCompare || rbImageModel.Checked;

            txtPublicFolder.Enabled = isCompare;
            btnPickPublicFolder.Enabled = isCompare;

            labelPublicFolder.Text = isCompare ? "公版文件夹：" : "（双图比对时启用）公版文件夹：";
        }

        #endregion

        #region Run

        private async void btnRun_Click(object sender, EventArgs e)
        {
            string url = txtUrl.Text.Trim();

            if (!url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("请输入有效的接口 URL");
                return;
            }

            bool isCompare = rbCompare.Checked;
            string publicFolder = txtPublicFolder.Text.Trim();
            string targetFolder = txtTargetFolder.Text.Trim();

            if (!Directory.Exists(targetFolder))
            {
                MessageBox.Show("请选择有效的目标图片文件夹");
                return;
            }

            if (isCompare && !Directory.Exists(publicFolder))
            {
                MessageBox.Show("双图比对模式：请选择有效的公版图片文件夹");
                return;
            }

            if (isCompare && string.IsNullOrWhiteSpace(txtModelKey.Text))
            {
                MessageBox.Show("双图比对模式：model_key 不能为空");
                return;
            }

            btnRun.Enabled = false;
            btnCancel.Enabled = true;
            btnExport.Enabled = false;

            _rows.Clear();
            txtDetail.Clear();

            _cts = new CancellationTokenSource();

            try
            {
                var exts = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".webp" };

                // 目标（按文件名排序）
                var targetFiles = Directory.GetFiles(targetFolder)
                    .Where(f => exts.Contains(Path.GetExtension(f).ToLower()))
                    .OrderBy(f => Path.GetFileName(f), StringComparer.OrdinalIgnoreCase)
                    .ToArray();

                if (targetFiles.Length == 0)
                {
                    MessageBox.Show("目标文件夹没有图片");
                    return;
                }

                string[] publicFiles = null;
                if (isCompare)
                {
                    publicFiles = Directory.GetFiles(publicFolder)
                        .Where(f => exts.Contains(Path.GetExtension(f).ToLower()))
                        .OrderBy(f => Path.GetFileName(f), StringComparer.OrdinalIgnoreCase)
                        .ToArray();

                    if (publicFiles.Length == 0)
                    {
                        MessageBox.Show("公版文件夹没有图片");
                        return;
                    }
                }

                // ★ 全组合总数：publicCount * targetCount
                int total = isCompare
                    ? checked(publicFiles.Length * targetFiles.Length)
                    : targetFiles.Length;

                progress.Value = 0;
                progress.Maximum = Math.Max(1, total);

                int idx = 0;

                if (!isCompare)
                {
                    // 单图：原来逻辑（逐个target跑）
                    for (int t = 0; t < targetFiles.Length; t++)
                    {
                        _cts.Token.ThrowIfCancellationRequested();

                        var row = new BatchRow
                        {
                            Index = ++idx,
                            TargetFileName = Path.GetFileName(targetFiles[t]),
                            TargetPath = targetFiles[t]
                        };

                        _rows.Add(row);
                        await RunOneAsync(row, url, isCompare: false, _cts.Token);

                        progress.Value = Math.Min(progress.Maximum, idx);
                        lblStatus.Text = $"{idx} / {total}";
                        Application.DoEvents();
                    }
                }
                else
                {
                    // ★ 双图：全组合（笛卡尔积）
                    for (int p = 0; p < publicFiles.Length; p++)
                    {
                        for (int t = 0; t < targetFiles.Length; t++)
                        {
                            _cts.Token.ThrowIfCancellationRequested();

                            var row = new BatchRow
                            {
                                Index = ++idx,
                                PublicFileName = Path.GetFileName(publicFiles[p]),
                                PublicPath = publicFiles[p],
                                TargetFileName = Path.GetFileName(targetFiles[t]),
                                TargetPath = targetFiles[t]
                            };

                            _rows.Add(row);
                            await RunOneAsync(row, url, isCompare: true, _cts.Token);

                            progress.Value = Math.Min(progress.Maximum, idx);
                            lblStatus.Text = $"{idx} / {total}";
                            Application.DoEvents();
                        }
                    }
                }

                btnExport.Enabled = true;
            }
            catch (OverflowException)
            {
                MessageBox.Show("图片数量太多导致组合数溢出，请减少图片数量后重试。");
            }
            catch (OperationCanceledException)
            {
                lblStatus.Text = "已取消";
            }
            finally
            {
                btnRun.Enabled = true;
                btnCancel.Enabled = false;
                _cts?.Dispose();
                _cts = null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgv.Rows[e.RowIndex].DataBoundItem as BatchRow;
            if (row == null) return;

            txtDetail.Text =
                (string.IsNullOrWhiteSpace(row.PublicFileName) ? "" : $"Public：{row.PublicFileName}\r\n") +
                $"Target：{row.TargetFileName}\r\n" +
                $"状态：{row.StatusCode}\r\n" +
                $"耗时：{row.ElapsedMs} ms\r\n" +
                $"OK：{row.Ok}\r\n" +
                $"错误：{row.Error}\r\n\r\n" +
                $"{row.ResponseFull}";
        }

        #endregion

        #region Request

        private async Task RunOneAsync(BatchRow row, string url, bool isCompare, CancellationToken ct)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                object body;

                if (!isCompare)
                {
                    byte[] img = File.ReadAllBytes(row.TargetPath);
                    string base64 = Convert.ToBase64String(img);

                    body = rbOnlyImage.Checked
                        ? new { image_base64 = base64 }
                        : new { image_base64 = base64, model_key = txtModelKey.Text.Trim() };
                }
                else
                {
                    byte[] pub = File.ReadAllBytes(row.PublicPath);
                    byte[] tar = File.ReadAllBytes(row.TargetPath);

                    body = new
                    {
                        model_key = txtModelKey.Text.Trim(),
                        public_base64 = Convert.ToBase64String(pub),
                        target_base64 = Convert.ToBase64String(tar)
                    };
                }

                string reqJson = JsonConvert.SerializeObject(body);

                using (var content = new ByteArrayContent(Encoding.UTF8.GetBytes(reqJson)))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    using (var req = new HttpRequestMessage(HttpMethod.Post, url))
                    {
                        req.Content = content;
                        req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        using (var resp = await _http.SendAsync(req, ct))
                        {
                            string respText = await resp.Content.ReadAsStringAsync();

                            row.StatusCode = (int)resp.StatusCode;
                            row.ElapsedMs = sw.ElapsedMilliseconds;
                            row.Ok = resp.IsSuccessStatusCode;

                            string pretty = CleanLeading(BeautifyJson(respText));
                            row.ResponseFull = pretty;
                            row.Response = Truncate(pretty, 300);

                            row.Error = CleanLeading(
                                resp.IsSuccessStatusCode
                                    ? ""
                                    : $"HTTP {(int)resp.StatusCode} {resp.StatusCode}"
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                row.Ok = false;
                row.StatusCode = 0;
                row.ElapsedMs = sw.ElapsedMilliseconds;
                row.Error = CleanLeading(ex.Message);
                row.Response = "";
                row.ResponseFull = "";
            }
            finally
            {
                sw.Stop();
                dgv.Refresh();
            }
        }

        #endregion

        #region Export

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (_rows.Count == 0)
            {
                MessageBox.Show("没有结果可导出");
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel (*.xlsx)|*.xlsx";
                sfd.FileName = "batch_result.xlsx";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                ExportXlsxWithImages(sfd.FileName);
                MessageBox.Show("导出完成（图片已自动缩放并嵌入单元格）");
            }
        }

        private void ExportXlsxWithImages(string filePath)
        {
            bool isCompare = rbCompare.Checked;

            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Result");

                int c = 1;
                ws.Cell(1, c++).Value = "Index";

                if (isCompare)
                {
                    ws.Cell(1, c++).Value = "PublicFile";
                    ws.Cell(1, c++).Value = "TargetFile";
                    ws.Cell(1, c++).Value = "PublicImage";
                    ws.Cell(1, c++).Value = "TargetImage";
                }
                else
                {
                    ws.Cell(1, c++).Value = "TargetFile";
                    ws.Cell(1, c++).Value = "TargetImage";
                }

                ws.Cell(1, c++).Value = "StatusCode";
                ws.Cell(1, c++).Value = "ElapsedMs";
                ws.Cell(1, c++).Value = "Ok";
                ws.Cell(1, c++).Value = "Error";
                ws.Cell(1, c++).Value = "Response";

                ws.Row(1).Style.Font.Bold = true;

                ws.Column(1).Width = 8;

                int colPublicFile = -1, colTargetFile = -1, colPublicImg = -1, colTargetImg = -1;
                int colAfterImg;

                if (isCompare)
                {
                    colPublicFile = 2;
                    colTargetFile = 3;
                    colPublicImg = 4;
                    colTargetImg = 5;

                    ws.Column(colPublicFile).Width = 35;
                    ws.Column(colTargetFile).Width = 35;

                    ws.Column(colPublicImg).Width = 22;
                    ws.Column(colTargetImg).Width = 22;

                    colAfterImg = 6;
                }
                else
                {
                    colTargetFile = 2;
                    colTargetImg = 3;

                    ws.Column(colTargetFile).Width = 35;
                    ws.Column(colTargetImg).Width = 22;

                    colAfterImg = 4;
                }

                ws.Column(colAfterImg + 0).Width = 10;
                ws.Column(colAfterImg + 1).Width = 12;
                ws.Column(colAfterImg + 2).Width = 6;
                ws.Column(colAfterImg + 3).Width = 30;
                ws.Column(colAfterImg + 4).Width = 60;

                int rExcel = 2;

                foreach (var r in _rows)
                {
                    ws.Row(rExcel).Height = 110;

                    ws.Cell(rExcel, 1).Value = r.Index;

                    if (isCompare)
                    {
                        ws.Cell(rExcel, colPublicFile).Value = r.PublicFileName ?? "";
                        ws.Cell(rExcel, colTargetFile).Value = r.TargetFileName ?? "";

                        AddPictureFitCell(ws, r.PublicPath, ws.Cell(rExcel, colPublicImg), paddingPx: 4);
                        AddPictureFitCell(ws, r.TargetPath, ws.Cell(rExcel, colTargetImg), paddingPx: 4);
                    }
                    else
                    {
                        ws.Cell(rExcel, colTargetFile).Value = r.TargetFileName ?? "";
                        AddPictureFitCell(ws, r.TargetPath, ws.Cell(rExcel, colTargetImg), paddingPx: 4);
                    }

                    ws.Cell(rExcel, colAfterImg + 0).Value = r.StatusCode;
                    ws.Cell(rExcel, colAfterImg + 1).Value = r.ElapsedMs;
                    ws.Cell(rExcel, colAfterImg + 2).Value = r.Ok ? "Y" : "N";
                    ws.Cell(rExcel, colAfterImg + 3).Value = r.Error ?? "";
                    ws.Cell(rExcel, colAfterImg + 4).Value = r.ResponseFull ?? r.Response ?? "";

                    rExcel++;
                }

                ws.Column(colAfterImg + 3).Style.Alignment.WrapText = true;
                ws.Column(colAfterImg + 4).Style.Alignment.WrapText = true;

                wb.SaveAs(filePath);
            }
        }

        private static int RowHeightToPixels(double points)
        {
            return (int)Math.Round(points * 96.0 / 72.0);
        }

        private static int ColWidthToPixels(double excelWidth)
        {
            return (int)Math.Round(excelWidth * 7.0 + 5.0);
        }

        // ★ 关键：先 WithPlacement，再 WithSize，避免你之前的异常
        private static void AddPictureFitCell(IXLWorksheet ws, string imagePath, IXLCell cell, int paddingPx = 4)
        {
            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
            {
                cell.Value = "(no image)";
                return;
            }

            int colNum = cell.Address.ColumnNumber;
            int rowNum = cell.Address.RowNumber;

            int cellW = ColWidthToPixels(ws.Column(colNum).Width);
            int cellH = RowHeightToPixels(ws.Row(rowNum).Height);

            int boxW = Math.Max(1, cellW - paddingPx * 2);
            int boxH = Math.Max(1, cellH - paddingPx * 2);

            using (var img = Image.FromFile(imagePath))
            {
                double imgW = img.Width;
                double imgH = img.Height;

                double scale = Math.Min(boxW / imgW, boxH / imgH);

                int newW = Math.Max(1, (int)Math.Round(imgW * scale));
                int newH = Math.Max(1, (int)Math.Round(imgH * scale));

                int offsetX = paddingPx + (boxW - newW) / 2;
                int offsetY = paddingPx + (boxH - newH) / 2;

                ws.AddPicture(imagePath)
                  .WithPlacement(XLPicturePlacement.Move)
                  .MoveTo(cell, offsetX, offsetY)
                  .WithSize(newW, newH);
            }
        }

        #endregion

        #region Helpers

        private static string CleanLeading(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            return s.TrimStart(' ', '\t', '\r', '\n');
        }

        private static string BeautifyJson(string text)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject(text);
                return JsonConvert.SerializeObject(obj, Formatting.Indented);
            }
            catch
            {
                return text;
            }
        }

        private static string Truncate(string s, int max)
        {
            if (string.IsNullOrEmpty(s)) return s;
            return s.Length <= max ? s : s.Substring(0, max) + " ...";
        }

        #endregion
    }

    public class BatchRow
    {
        public int Index { get; set; }

        public string PublicFileName { get; set; }
        public string PublicPath { get; set; }

        public string TargetFileName { get; set; }
        public string TargetPath { get; set; }

        public int StatusCode { get; set; }
        public long ElapsedMs { get; set; }
        public bool Ok { get; set; }
        public string Error { get; set; }
        public string Response { get; set; }
        public string ResponseFull { get; set; }
    }
}
