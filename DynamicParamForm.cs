using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace TestTool
{
    public sealed class DynamicParamForm : Form
    {
        private static readonly HttpClient HttpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(60)
        };

        private readonly TabControl tabControl = new TabControl();

        private readonly ComboBox txtUrlSingle = new ComboBox();
        private readonly DataGridView dgvParamsSingle = new DataGridView();
        private readonly Button btnPickImage = new Button();
        private readonly PictureBox pictureBox = new PictureBox();
        private readonly TextBox txtBase64 = new TextBox();
        private readonly Button btnSend = new Button();
        private readonly TextBox txtRequest = new TextBox();
        private readonly TextBox txtResponse = new TextBox();

        private readonly ComboBox txtUrlBatch = new ComboBox();
        private readonly DataGridView dgvParamsBatch = new DataGridView();
        private readonly TextBox txtBatchFolder = new TextBox();
        private readonly Button btnPickBatchFolder = new Button();
        private readonly Button btnRunBatch = new Button();
        private readonly ProgressBar progressBatch = new ProgressBar();
        private readonly Label lblBatchStatus = new Label();
        private readonly DataGridView dgvBatchResult = new DataGridView();

        public DynamicParamForm()
        {
            Text = "参数自定义";
            Width = 1400;
            Height = 900;
            StartPosition = FormStartPosition.CenterScreen;

            tabControl.Dock = DockStyle.Fill;
            tabControl.Multiline = true;

            tabControl.TabPages.Add(BuildSingleTab());
            tabControl.TabPages.Add(BuildBatchTab());

            Controls.Add(tabControl);
        }

        private TabPage BuildSingleTab()
        {
            var page = new TabPage("单张");

            var lblUrl = new Label { Text = "接口 URL：", Location = new Point(12, 14), AutoSize = true };
            txtUrlSingle.Location = new Point(80, 10);
            txtUrlSingle.Width = 900;
            ApiEndpoints.BindToUrls(txtUrlSingle, ApiEndpoints.GetUrl("detect"), "");

            var lblParams = new Label { Text = "自定义参数：", Location = new Point(12, 44), AutoSize = true };
            ConfigureParamGrid(dgvParamsSingle);
            dgvParamsSingle.Location = new Point(12, 64);
            dgvParamsSingle.Size = new Size(520, 160);

            btnPickImage.Text = "上传图片";
            btnPickImage.Location = new Point(12, 234);
            btnPickImage.Size = new Size(100, 26);
            btnPickImage.Click += BtnPickImage_Click;

            pictureBox.Location = new Point(12, 270);
            pictureBox.Size = new Size(320, 240);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;

            txtBase64.Location = new Point(12, 520);
            txtBase64.Size = new Size(520, 140);
            txtBase64.Multiline = true;
            txtBase64.ScrollBars = ScrollBars.Both;
            txtBase64.WordWrap = false;

            btnSend.Text = "发送";
            btnSend.Location = new Point(12, 670);
            btnSend.Size = new Size(100, 26);
            btnSend.Click += BtnSend_Click;

            var lblRequest = new Label { Text = "Request", Location = new Point(560, 44), AutoSize = true };
            txtRequest.Location = new Point(560, 64);
            txtRequest.Size = new Size(780, 300);
            txtRequest.Multiline = true;
            txtRequest.ScrollBars = ScrollBars.Both;
            txtRequest.WordWrap = false;

            var lblResponse = new Label { Text = "Response", Location = new Point(560, 380), AutoSize = true };
            txtResponse.Location = new Point(560, 400);
            txtResponse.Size = new Size(780, 300);
            txtResponse.Multiline = true;
            txtResponse.ScrollBars = ScrollBars.Both;
            txtResponse.WordWrap = false;

            page.Controls.Add(lblUrl);
            page.Controls.Add(txtUrlSingle);
            page.Controls.Add(lblParams);
            page.Controls.Add(dgvParamsSingle);
            page.Controls.Add(btnPickImage);
            page.Controls.Add(pictureBox);
            page.Controls.Add(txtBase64);
            page.Controls.Add(btnSend);
            page.Controls.Add(lblRequest);
            page.Controls.Add(txtRequest);
            page.Controls.Add(lblResponse);
            page.Controls.Add(txtResponse);

            return page;
        }

        private TabPage BuildBatchTab()
        {
            var page = new TabPage("批量");

            var lblUrl = new Label { Text = "接口 URL：", Location = new Point(12, 14), AutoSize = true };
            txtUrlBatch.Location = new Point(80, 10);
            txtUrlBatch.Width = 900;
            ApiEndpoints.BindToUrls(txtUrlBatch, ApiEndpoints.GetUrl("detect"), "");

            var lblParams = new Label { Text = "自定义参数：", Location = new Point(12, 44), AutoSize = true };
            ConfigureParamGrid(dgvParamsBatch);
            dgvParamsBatch.Location = new Point(12, 64);
            dgvParamsBatch.Size = new Size(520, 160);

            var lblFolder = new Label { Text = "图片文件夹：", Location = new Point(560, 44), AutoSize = true };
            txtBatchFolder.Location = new Point(640, 40);
            txtBatchFolder.Size = new Size(520, 23);
            btnPickBatchFolder.Text = "选择";
            btnPickBatchFolder.Location = new Point(1170, 38);
            btnPickBatchFolder.Size = new Size(60, 26);
            btnPickBatchFolder.Click += BtnPickBatchFolder_Click;

            btnRunBatch.Text = "开始执行";
            btnRunBatch.Location = new Point(560, 74);
            btnRunBatch.Size = new Size(100, 26);
            btnRunBatch.Click += BtnRunBatch_Click;

            progressBatch.Location = new Point(670, 80);
            progressBatch.Size = new Size(260, 18);
            lblBatchStatus.Location = new Point(940, 80);
            lblBatchStatus.AutoSize = true;
            lblBatchStatus.Text = "0 / 0";

            ConfigureBatchResultGrid(dgvBatchResult);
            dgvBatchResult.Location = new Point(12, 240);
            dgvBatchResult.Size = new Size(1320, 560);

            page.Controls.Add(lblUrl);
            page.Controls.Add(txtUrlBatch);
            page.Controls.Add(lblParams);
            page.Controls.Add(dgvParamsBatch);
            page.Controls.Add(lblFolder);
            page.Controls.Add(txtBatchFolder);
            page.Controls.Add(btnPickBatchFolder);
            page.Controls.Add(btnRunBatch);
            page.Controls.Add(progressBatch);
            page.Controls.Add(lblBatchStatus);
            page.Controls.Add(dgvBatchResult);

            return page;
        }

        private void ConfigureParamGrid(DataGridView grid)
        {
            grid.AllowUserToAddRows = true;
            grid.AllowUserToDeleteRows = true;
            grid.AllowUserToResizeRows = false;
            grid.RowHeadersVisible = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.Columns.Clear();
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "参数名", Name = "colName" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "参数值", Name = "colValue" });
        }

        private void ConfigureBatchResultGrid(DataGridView grid)
        {
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.Columns.Clear();
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "文件名", Name = "colFile" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "状态码", Name = "colStatus" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "耗时(ms)", Name = "colMs" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "OK", Name = "colOk" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "响应(截断)", Name = "colResp" });
        }

        private void BtnPickImage_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "选择图片",
                Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            byte[] bytes = File.ReadAllBytes(ofd.FileName);
            txtBase64.Text = Convert.ToBase64String(bytes);

            var old = pictureBox.Image;
            pictureBox.Image = null;
            old?.Dispose();

            using var ms = new MemoryStream(bytes);
            using var img = Image.FromStream(ms);
            pictureBox.Image = new Bitmap(img);
        }

        private async void BtnSend_Click(object sender, EventArgs e)
        {
            btnSend.Enabled = false;

            try
            {
                string url = ApiEndpoints.GetUrl(txtUrlSingle);
                if (string.IsNullOrWhiteSpace(url))
                {
                    MessageBox.Show("请输入接口 URL");
                    return;
                }

                string rawBase64 = txtBase64.Text.Trim();
                if (string.IsNullOrWhiteSpace(rawBase64))
                {
                    MessageBox.Show("请先上传图片");
                    return;
                }

                var payload = BuildPayload(rawBase64, dgvParamsSingle);
                string sendJson = JsonSerializer.Serialize(payload);

                txtRequest.Text = JsonSerializer.Serialize(
                    BuildDisplayPayload(rawBase64, dgvParamsSingle),
                    new JsonSerializerOptions { WriteIndented = true });

                var req = new HttpRequestMessage(HttpMethod.Post, url);
                req.Content = new StringContent(sendJson, Encoding.UTF8);
                req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                using var resp = await HttpClient.SendAsync(req);
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

        private void BtnPickBatchFolder_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != DialogResult.OK) return;
            txtBatchFolder.Text = fbd.SelectedPath;
        }

        private async void BtnRunBatch_Click(object sender, EventArgs e)
        {
            btnRunBatch.Enabled = false;
            dgvBatchResult.Rows.Clear();

            try
            {
                string url = ApiEndpoints.GetUrl(txtUrlBatch);
                if (string.IsNullOrWhiteSpace(url))
                {
                    MessageBox.Show("请输入接口 URL");
                    return;
                }

                string folder = txtBatchFolder.Text.Trim();
                if (!Directory.Exists(folder))
                {
                    MessageBox.Show("请选择有效的图片文件夹");
                    return;
                }

                var exts = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".webp" };
                var files = Directory.GetFiles(folder)
                    .Where(f => exts.Contains(Path.GetExtension(f).ToLower()))
                    .OrderBy(f => Path.GetFileName(f), StringComparer.OrdinalIgnoreCase)
                    .ToArray();

                if (files.Length == 0)
                {
                    MessageBox.Show("文件夹没有图片");
                    return;
                }

                progressBatch.Value = 0;
                progressBatch.Maximum = Math.Max(1, files.Length);
                lblBatchStatus.Text = $"0 / {files.Length}";

                int index = 0;

                foreach (var file in files)
                {
                    index++;
                    var sw = Stopwatch.StartNew();
                    bool ok = false;
                    int statusCode = 0;
                    string responsePreview = "";

                    try
                    {
                        byte[] img = File.ReadAllBytes(file);
                        string base64 = Convert.ToBase64String(img);
                        var payload = BuildPayload(base64, dgvParamsBatch);
                        string sendJson = JsonSerializer.Serialize(payload);

                        var req = new HttpRequestMessage(HttpMethod.Post, url);
                        req.Content = new StringContent(sendJson, Encoding.UTF8);
                        req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        using var resp = await HttpClient.SendAsync(req);
                        string respText = await resp.Content.ReadAsStringAsync();
                        ok = resp.IsSuccessStatusCode;
                        statusCode = (int)resp.StatusCode;
                        responsePreview = Truncate(FormatJson.Beautify(respText), 200);
                    }
                    catch (Exception ex)
                    {
                        responsePreview = ex.Message;
                    }
                    finally
                    {
                        sw.Stop();
                    }

                    dgvBatchResult.Rows.Add(
                        Path.GetFileName(file),
                        statusCode,
                        sw.ElapsedMilliseconds,
                        ok ? "Y" : "N",
                        responsePreview);

                    progressBatch.Value = Math.Min(progressBatch.Maximum, index);
                    lblBatchStatus.Text = $"{index} / {files.Length}";
                    Application.DoEvents();
                }
            }
            finally
            {
                btnRunBatch.Enabled = true;
            }
        }

        private static Dictionary<string, object> BuildPayload(string base64, DataGridView grid)
        {
            var payload = new Dictionary<string, object>
            {
                ["image_base64"] = base64
            };

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;
                string name = Convert.ToString(row.Cells[0].Value)?.Trim() ?? "";
                string value = Convert.ToString(row.Cells[1].Value) ?? "";
                if (string.IsNullOrWhiteSpace(name)) continue;
                if (name.Equals("image_base64", StringComparison.OrdinalIgnoreCase)) continue;
                payload[name] = value;
            }

            return payload;
        }

        private static Dictionary<string, object> BuildDisplayPayload(string base64, DataGridView grid)
        {
            var payload = BuildPayload(base64, grid);
            payload["image_base64"] = FormatJson.Beautify(base64);
            return payload;
        }

        private static string Truncate(string input, int max)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return input.Length <= max ? input : input.Substring(0, max) + "...";
        }
    }
}
