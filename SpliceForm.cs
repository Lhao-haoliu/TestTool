using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace TestTool
{
    public partial class SpliceForm : Form
    {
        private static readonly HttpClient _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(60)
        };

        public SpliceForm()
        {
            InitializeComponent();

            pictureBoxCellPreview.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxCellPreview.BorderStyle = BorderStyle.FixedSingle;

            pictureBoxResult.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxResult.BorderStyle = BorderStyle.FixedSingle;

            dgvGrid.AllowUserToAddRows = false;
            dgvGrid.RowHeadersWidth = 50;
            dgvGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvGrid.MultiSelect = false;
            dgvGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvGrid.CellClick += dgvGrid_CellClick;
        }

        private void SpliceForm_Load(object sender, EventArgs e)
        {
            txtUrl.Text = ApiEndpoints.GetUrl("splice");

            // 默认 1 组（1 行）+ 2 张（2 列）
            InitGrid(defaultRows: 1, defaultCols: 2);
            UpdateGridInfo();
        }

        private void InitGrid(int defaultRows, int defaultCols)
        {
            dgvGrid.Columns.Clear();
            dgvGrid.Rows.Clear();

            for (int c = 0; c < defaultCols; c++)
            {
                dgvGrid.Columns.Add($"C{c + 1}", $"图{c + 1}");
            }

            for (int r = 0; r < defaultRows; r++)
            {
                dgvGrid.Rows.Add();
                dgvGrid.Rows[r].HeaderCell.Value = $"组{r + 1}";
            }
        }

        private void UpdateGridInfo()
        {
            lblInfo.Text = $"组数(行): {dgvGrid.Rows.Count}    每组最大张数(列): {dgvGrid.Columns.Count}";
        }

        // ========== 图片选择/填充 ==========

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            int idx = dgvGrid.Rows.Add();
            dgvGrid.Rows[idx].HeaderCell.Value = $"组{idx + 1}";
            UpdateGridInfo();
        }

        private void btnRemoveRow_Click(object sender, EventArgs e)
        {
            if (dgvGrid.Rows.Count <= 1)
            {
                MessageBox.Show("至少保留 1 组");
                return;
            }

            int rowIndex = dgvGrid.CurrentCell?.RowIndex ?? (dgvGrid.Rows.Count - 1);
            dgvGrid.Rows.RemoveAt(rowIndex);

            // 重新编号
            for (int r = 0; r < dgvGrid.Rows.Count; r++)
                dgvGrid.Rows[r].HeaderCell.Value = $"组{r + 1}";

            UpdateGridInfo();
        }

        private void btnAddCol_Click(object sender, EventArgs e)
        {
            int c = dgvGrid.Columns.Count;
            dgvGrid.Columns.Add($"C{c + 1}", $"图{c + 1}");
            UpdateGridInfo();
        }

        private void btnRemoveCol_Click(object sender, EventArgs e)
        {
            if (dgvGrid.Columns.Count <= 1)
            {
                MessageBox.Show("至少保留 1 列(每组至少 1 张)");
                return;
            }

            int colIndex = dgvGrid.CurrentCell?.ColumnIndex ?? (dgvGrid.Columns.Count - 1);
            dgvGrid.Columns.RemoveAt(colIndex);

            // 重新标题
            for (int c = 0; c < dgvGrid.Columns.Count; c++)
                dgvGrid.Columns[c].HeaderText = $"图{c + 1}";

            UpdateGridInfo();
        }

        private void btnLoadToCell_Click(object sender, EventArgs e)
        {
            if (dgvGrid.CurrentCell == null)
            {
                MessageBox.Show("请先点击一个单元格（某组的某张图）");
                return;
            }

            using OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "选择图片（填入当前单元格）",
                Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            string base64 = FileToBase64(ofd.FileName);
            dgvGrid.CurrentCell.Value = base64;

            ShowCellPreview(dgvGrid.CurrentCell.RowIndex, dgvGrid.CurrentCell.ColumnIndex);
        }

        private void btnLoadToRow_Click(object sender, EventArgs e)
        {
            if (dgvGrid.CurrentCell == null)
            {
                MessageBox.Show("请先点击要填充的“组(行)”里的任意一个格子");
                return;
            }

            int row = dgvGrid.CurrentCell.RowIndex;

            using OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "选择多张图片（按选择顺序填入该组，从第1列开始）",
                Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp",
                Multiselect = true
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            var files = ofd.FileNames;
            if (files.Length == 0) return;

            // 不够列就自动加列
            while (dgvGrid.Columns.Count < files.Length)
            {
                int c = dgvGrid.Columns.Count;
                dgvGrid.Columns.Add($"C{c + 1}", $"图{c + 1}");
            }

            for (int i = 0; i < files.Length; i++)
            {
                dgvGrid.Rows[row].Cells[i].Value = FileToBase64(files[i]);
            }

            UpdateGridInfo();
            ShowCellPreview(row, 0);
        }

        private void btnClearCell_Click(object sender, EventArgs e)
        {
            if (dgvGrid.CurrentCell == null) return;
            dgvGrid.CurrentCell.Value = null;
            pictureBoxCellPreview.Image?.Dispose();
            pictureBoxCellPreview.Image = null;
        }

        private void btnClearRow_Click(object sender, EventArgs e)
        {
            if (dgvGrid.CurrentCell == null) return;

            int row = dgvGrid.CurrentCell.RowIndex;
            for (int c = 0; c < dgvGrid.Columns.Count; c++)
                dgvGrid.Rows[row].Cells[c].Value = null;

            pictureBoxCellPreview.Image?.Dispose();
            pictureBoxCellPreview.Image = null;
        }

        private void dgvGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            ShowCellPreview(e.RowIndex, e.ColumnIndex);
        }

        private void ShowCellPreview(int row, int col)
        {
            string b64 = dgvGrid.Rows[row].Cells[col].Value as string;
            if (string.IsNullOrWhiteSpace(b64))
            {
                pictureBoxCellPreview.Image?.Dispose();
                pictureBoxCellPreview.Image = null;
                return;
            }

            try
            {
                byte[] bytes = Convert.FromBase64String(b64.Trim());
                using var ms = new MemoryStream(bytes);
                using var img = Image.FromStream(ms);

                var old = pictureBoxCellPreview.Image;
                pictureBoxCellPreview.Image = new Bitmap(img);
                old?.Dispose();
            }
            catch
            {
                // base64 不合法就不预览
                pictureBoxCellPreview.Image?.Dispose();
                pictureBoxCellPreview.Image = null;
            }
        }

        private static string FileToBase64(string file)
        {
            byte[] bytes = File.ReadAllBytes(file);
            return Convert.ToBase64String(bytes);
        }

        // ========== 组装请求 / 调接口 ==========

        private async void btnSend_Click(object sender, EventArgs e)
        {
            btnSend.Enabled = false;

            try
            {
                string url = txtUrl.Text.Trim();
                if (string.IsNullOrWhiteSpace(url))
                {
                    MessageBox.Show("请输入接口 URL");
                    return;
                }

                // 组装 image_grid（空单元格直接忽略，行内保持顺序）
                var grid = BuildImageGrid();
                if (grid.Count == 0)
                {
                    MessageBox.Show("请至少上传 1 组数据（至少1行内有1张图）");
                    return;
                }

                // 真正发送的 JSON（raw base64）
                string sendJson = JsonSerializer.Serialize(new { image_grid = grid });

                // UI 展示 Request：默认做短显示，避免 base64 太大卡死
                txtRequest.Text = JsonSerializer.Serialize(
                    new { image_grid = chkShortRequest.Checked ? ShortGrid(grid) : grid },
                    new JsonSerializerOptions { WriteIndented = true }
                );

                // 严格 Content-Type = application/json（不要 charset）
                var req = new HttpRequestMessage(HttpMethod.Post, url);
                req.Content = new StringContent(sendJson, Encoding.UTF8);
                req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                using var resp = await _http.SendAsync(req);
                string respText = await resp.Content.ReadAsStringAsync();

                txtResponse.Text = FormatJson.Beautify(respText);

                if (!resp.IsSuccessStatusCode)
                {
                    MessageBox.Show($"HTTP {(int)resp.StatusCode} {resp.ReasonPhrase}\n请查看 Response 中的错误信息");
                    return;
                }

                // 解析 stitched_base64 并预览
                TryShowResultImage(respText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "请求异常");
            }
            finally
            {
                btnSend.Enabled = true;
            }
        }

        private List<List<string>> BuildImageGrid()
        {
            var result = new List<List<string>>();

            for (int r = 0; r < dgvGrid.Rows.Count; r++)
            {
                var rowList = new List<string>();

                for (int c = 0; c < dgvGrid.Columns.Count; c++)
                {
                    var v = dgvGrid.Rows[r].Cells[c].Value as string;
                    if (string.IsNullOrWhiteSpace(v)) continue;

                    rowList.Add(v.Trim());
                }

                // 有内容才算一组
                if (rowList.Count > 0)
                    result.Add(rowList);
            }

            return result;
        }

        private static List<List<string>> ShortGrid(List<List<string>> grid)
        {
            // 每个 base64 只保留前 60 字符 + 长度提示
            var shortGrid = new List<List<string>>(grid.Count);
            foreach (var row in grid)
            {
                var sr = new List<string>(row.Count);
                foreach (var b64 in row)
                {
                    if (string.IsNullOrEmpty(b64))
                    {
                        sr.Add("");
                        continue;
                    }

                    string head = b64.Length > 60 ? b64.Substring(0, 60) : b64;
                    sr.Add($"{head}... (len={b64.Length})");
                }
                shortGrid.Add(sr);
            }
            return shortGrid;
        }

        private void TryShowResultImage(string respText)
        {
            try
            {
                using var doc = JsonDocument.Parse(respText);
                if (!doc.RootElement.TryGetProperty("data", out var data)) return;
                if (!data.TryGetProperty("stitched_base64", out var s)) return;

                string b64 = s.GetString() ?? "";
                b64 = b64.Trim();

                // 兼容 data:image/png;base64,xxx 这种
                int comma = b64.IndexOf(',');
                if (b64.StartsWith("data:", StringComparison.OrdinalIgnoreCase) && comma > 0)
                    b64 = b64.Substring(comma + 1);

                if (string.IsNullOrWhiteSpace(b64)) return;

                byte[] bytes = Convert.FromBase64String(b64);
                using var ms = new MemoryStream(bytes);
                using var img = Image.FromStream(ms);

                var old = pictureBoxResult.Image;
                pictureBoxResult.Image = new Bitmap(img);
                old?.Dispose();

                // width/height 显示
                int width = data.TryGetProperty("width", out var w) ? w.GetInt32() : pictureBoxResult.Image.Width;
                int height = data.TryGetProperty("height", out var h) ? h.GetInt32() : pictureBoxResult.Image.Height;
                lblResultInfo.Text = $"结果尺寸: {width} x {height}";
            }
            catch
            {
                // 不影响主流程
            }
        }

        private void btnSaveResult_Click(object sender, EventArgs e)
        {
            if (pictureBoxResult.Image == null)
            {
                MessageBox.Show("还没有结果图片");
                return;
            }

            using SaveFileDialog sfd = new SaveFileDialog
            {
                Title = "保存拼接结果",
                Filter = "PNG|*.png|JPG|*.jpg;*.jpeg|BMP|*.bmp",
                FileName = "stitched.png"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            var ext = Path.GetExtension(sfd.FileName).ToLowerInvariant();
            var img = pictureBoxResult.Image;

            if (ext == ".jpg" || ext == ".jpeg")
                img.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            else if (ext == ".bmp")
                img.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
            else
                img.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);

            MessageBox.Show("保存成功");
        }
    }
}
