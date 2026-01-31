using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace TestTool
{
    public partial class TskEtchForm : Form
    {
        private static readonly HttpClient _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(60)
        };

        public TskEtchForm()
        {
            InitializeComponent();

            pictureBoxCellPreview.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxCellPreview.BorderStyle = BorderStyle.FixedSingle;

            pictureBoxStitched.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxStitched.BorderStyle = BorderStyle.FixedSingle;

            dgvImg2D.AllowUserToAddRows = false;
            dgvImg2D.RowHeadersWidth = 60;
            dgvImg2D.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvImg2D.MultiSelect = false;
            dgvImg2D.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvImg2D.CellClick += dgvImg2D_CellClick;
        }

        private void TskEtchForm_Load(object sender, EventArgs e)
        {
            txtUrl.Text = ApiEndpoints.GetUrl("tsk etch");

            // model_key 下拉（可手输）
            cmbModelKey.Items.Clear();
            cmbModelKey.Items.AddRange(new object[]
            {
                "ETCH Box",
                "TSK",
                "Dummy"
            });
            cmbModelKey.Text = "ETCH Box";

            chkShortRequest.Checked = true;

            // 默认：2组(2行) + 3张(3列)
            InitGrid(rows: 2, cols: 3);
            UpdateInfo();
        }

        private void InitGrid(int rows, int cols)
        {
            dgvImg2D.Columns.Clear();
            dgvImg2D.Rows.Clear();

            for (int c = 0; c < cols; c++)
                dgvImg2D.Columns.Add($"C{c + 1}", $"图{c + 1}");

            for (int r = 0; r < rows; r++)
            {
                dgvImg2D.Rows.Add();
                dgvImg2D.Rows[r].HeaderCell.Value = $"组{r + 1}";
            }
        }

        private void UpdateInfo()
        {
            lblInfo.Text = $"组数(行): {dgvImg2D.Rows.Count}    每组最大张数(列): {dgvImg2D.Columns.Count}";
        }

        private static string FileToBase64(string file)
        {
            return Convert.ToBase64String(File.ReadAllBytes(file));
        }

        private static Image Base64ToImage(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            using var ms = new MemoryStream(bytes);
            using var img = Image.FromStream(ms);
            return new Bitmap(img);
        }

        // ====== img_2d_base64 组/列操作 ======

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            int idx = dgvImg2D.Rows.Add();
            dgvImg2D.Rows[idx].HeaderCell.Value = $"组{idx + 1}";
            UpdateInfo();
        }

        private void btnRemoveGroup_Click(object sender, EventArgs e)
        {
            if (dgvImg2D.Rows.Count <= 1)
            {
                MessageBox.Show("至少保留 1 组");
                return;
            }

            int row = dgvImg2D.CurrentCell?.RowIndex ?? (dgvImg2D.Rows.Count - 1);
            dgvImg2D.Rows.RemoveAt(row);

            for (int r = 0; r < dgvImg2D.Rows.Count; r++)
                dgvImg2D.Rows[r].HeaderCell.Value = $"组{r + 1}";

            UpdateInfo();
        }

        private void btnAddCol_Click(object sender, EventArgs e)
        {
            int c = dgvImg2D.Columns.Count;
            dgvImg2D.Columns.Add($"C{c + 1}", $"图{c + 1}");
            UpdateInfo();
        }

        private void btnRemoveCol_Click(object sender, EventArgs e)
        {
            if (dgvImg2D.Columns.Count <= 1)
            {
                MessageBox.Show("至少保留 1 列");
                return;
            }

            int col = dgvImg2D.CurrentCell?.ColumnIndex ?? (dgvImg2D.Columns.Count - 1);
            dgvImg2D.Columns.RemoveAt(col);

            for (int c = 0; c < dgvImg2D.Columns.Count; c++)
                dgvImg2D.Columns[c].HeaderText = $"图{c + 1}";

            UpdateInfo();
        }

        // ====== 填充/清空 ======

        private void btnLoadCell_Click(object sender, EventArgs e)
        {
            if (dgvImg2D.CurrentCell == null)
            {
                MessageBox.Show("请先点击 img_2d_base64 的某个单元格");
                return;
            }

            using OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "选择图片（填入当前格）",
                Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            dgvImg2D.CurrentCell.Value = FileToBase64(ofd.FileName);
            ShowCellPreview(dgvImg2D.CurrentCell.RowIndex, dgvImg2D.CurrentCell.ColumnIndex);
        }

        private void btnLoadGroup_Click(object sender, EventArgs e)
        {
            if (dgvImg2D.CurrentCell == null)
            {
                MessageBox.Show("请先点击要填充的组(行)任意格子");
                return;
            }

            int row = dgvImg2D.CurrentCell.RowIndex;

            using OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "选择多张图片（按选择顺序填入该组，从第1列开始）",
                Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp",
                Multiselect = true
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;
            var files = ofd.FileNames;
            if (files.Length == 0) return;

            while (dgvImg2D.Columns.Count < files.Length)
            {
                int c = dgvImg2D.Columns.Count;
                dgvImg2D.Columns.Add($"C{c + 1}", $"图{c + 1}");
            }

            for (int i = 0; i < files.Length; i++)
                dgvImg2D.Rows[row].Cells[i].Value = FileToBase64(files[i]);

            UpdateInfo();
            ShowCellPreview(row, 0);
        }

        private void btnClearCell_Click(object sender, EventArgs e)
        {
            if (dgvImg2D.CurrentCell == null) return;
            dgvImg2D.CurrentCell.Value = null;

            pictureBoxCellPreview.Image?.Dispose();
            pictureBoxCellPreview.Image = null;
        }

        private void btnClearGroup_Click(object sender, EventArgs e)
        {
            if (dgvImg2D.CurrentCell == null) return;
            int row = dgvImg2D.CurrentCell.RowIndex;

            for (int c = 0; c < dgvImg2D.Columns.Count; c++)
                dgvImg2D.Rows[row].Cells[c].Value = null;

            pictureBoxCellPreview.Image?.Dispose();
            pictureBoxCellPreview.Image = null;
        }

        private void dgvImg2D_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            ShowCellPreview(e.RowIndex, e.ColumnIndex);
        }

        private void ShowCellPreview(int row, int col)
        {
            string b64 = dgvImg2D.Rows[row].Cells[col].Value as string;
            if (string.IsNullOrWhiteSpace(b64))
            {
                pictureBoxCellPreview.Image?.Dispose();
                pictureBoxCellPreview.Image = null;
                return;
            }

            try
            {
                var old = pictureBoxCellPreview.Image;
                pictureBoxCellPreview.Image = Base64ToImage(b64.Trim());
                old?.Dispose();
            }
            catch
            {
                pictureBoxCellPreview.Image?.Dispose();
                pictureBoxCellPreview.Image = null;
            }
        }

        // ====== stitched image_base64 ======

        private void btnLoadStitched_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "选择拼接后的图片（image_base64）",
                Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            string b64 = FileToBase64(ofd.FileName);
            txtImageBase64.Text = b64;

            try
            {
                var old = pictureBoxStitched.Image;
                pictureBoxStitched.Image = Base64ToImage(b64);
                old?.Dispose();
            }
            catch
            {
                pictureBoxStitched.Image?.Dispose();
                pictureBoxStitched.Image = null;
            }
        }

        // ====== Build request & Send ======

        private List<List<string>> BuildImg2DBase64()
        {
            var result = new List<List<string>>();

            for (int r = 0; r < dgvImg2D.Rows.Count; r++)
            {
                var rowList = new List<string>();

                for (int c = 0; c < dgvImg2D.Columns.Count; c++)
                {
                    string v = dgvImg2D.Rows[r].Cells[c].Value as string;
                    if (string.IsNullOrWhiteSpace(v)) continue;

                    rowList.Add(v.Trim());
                }

                if (rowList.Count > 0)
                    result.Add(rowList);
            }

            return result;
        }

        private static List<List<string>> ShortGrid(List<List<string>> grid)
        {
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

                string modelKey = cmbModelKey.Text.Trim();
                if (string.IsNullOrWhiteSpace(modelKey))
                {
                    MessageBox.Show("请选择或输入 model_key");
                    return;
                }

                var img2d = BuildImg2DBase64();
                if (img2d.Count == 0)
                {
                    MessageBox.Show("img_2d_base64 至少要有 1 组（至少上传一张图）");
                    return;
                }

                string stitchedB64 = txtImageBase64.Text.Trim();
                if (string.IsNullOrWhiteSpace(stitchedB64))
                {
                    MessageBox.Show("image_base64 不能为空（请上传拼接后的图片）");
                    return;
                }

                // 1) 发送 JSON（raw）
                var sendObj = new
                {
                    img_2d_base64 = img2d,
                    model_key = modelKey,
                    image_base64 = stitchedB64
                };

                string sendJson = JsonSerializer.Serialize(sendObj);

                // 2) 展示 Request（短显示可防卡）
                var showObj = new
                {
                    img_2d_base64 = chkShortRequest.Checked ? ShortGrid(img2d) : img2d,
                    model_key = modelKey,
                    image_base64 = chkShortRequest.Checked
                        ? $"{(stitchedB64.Length > 60 ? stitchedB64.Substring(0, 60) : stitchedB64)}... (len={stitchedB64.Length})"
                        : FormatJson.Beautify(stitchedB64)
                };

                txtRequest.Text = JsonSerializer.Serialize(showObj, new JsonSerializerOptions { WriteIndented = true });

                // 3) 严格 Content-Type = application/json（无 charset）
                var req = new HttpRequestMessage(HttpMethod.Post, url);
                req.Content = new StringContent(sendJson, Encoding.UTF8);
                req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                using var resp = await _http.SendAsync(req);
                string respText = await resp.Content.ReadAsStringAsync();

                txtResponse.Text = FormatJson.Beautify(respText);

                if (!resp.IsSuccessStatusCode)
                {
                    MessageBox.Show($"HTTP {(int)resp.StatusCode} {resp.ReasonPhrase}\n请查看 Response 中的错误信息");
                }
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
    }
}
