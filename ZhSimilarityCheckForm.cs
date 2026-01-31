using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace TestTool
{
    public partial class ZhSimilarityCheckForm : Form
    {
        private static readonly HttpClient _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        public ZhSimilarityCheckForm()
        {
            InitializeComponent();

            pictureBoxPublic.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPublic.BorderStyle = BorderStyle.FixedSingle;

            pictureBoxTarget.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxTarget.BorderStyle = BorderStyle.FixedSingle;
        }

        private void ZhSimilarityCheckForm_Load(object sender, EventArgs e)
        {
            txtUrl.Text = ApiEndpoints.GetUrl("zh similarity-check");

            // model_key 下拉（可手动输入）
            cmbModelKey.Items.Clear();
            cmbModelKey.Items.AddRange(new object[]
            {
                "layer Mark",
                "OM Mark",
                "SEM Mark",
                "VC Mark",
                "Dummy",
                "ETCH Box",
                "CD Bar"
            });

            cmbModelKey.Text = "Dummy";
        }

        private void btnSelectPublic_Click(object sender, EventArgs e)
        {
            SelectImageTo(txtPublicBase64, pictureBoxPublic);
        }

        private void btnSelectTarget_Click(object sender, EventArgs e)
        {
            SelectImageTo(txtTargetBase64, pictureBoxTarget);
        }

        private void SelectImageTo(TextBox base64Box, PictureBox pic)
        {
            using OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "选择图片",
                Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            byte[] bytes = File.ReadAllBytes(ofd.FileName);

            string base64 = Convert.ToBase64String(bytes);
            base64Box.Text = base64;

            var old = pic.Image;
            pic.Image = null;
            old?.Dispose();

            using var ms = new MemoryStream(bytes);
            using var img = Image.FromStream(ms);
            pic.Image = new Bitmap(img);
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
                //if (string.IsNullOrWhiteSpace(modelKey))
                //{
                //    MessageBox.Show("请选择或输入 model_key");
                //    return;
                //}

                string rawPublic = txtPublicBase64.Text.Trim();
                if (string.IsNullOrWhiteSpace(rawPublic))
                {
                    MessageBox.Show("请上传 公版图片（public_base64）");
                    return;
                }

                string rawTarget = txtTargetBase64.Text.Trim();
                if (string.IsNullOrWhiteSpace(rawTarget))
                {
                    MessageBox.Show("请上传 目标图片（target_base64）");
                    return;
                }

                // 1) 发送 JSON（raw base64）
                string sendJson = JsonSerializer.Serialize(new
                {
                    model_key = modelKey,
                    public_base64 = rawPublic,
                    target_base64 = rawTarget
                });

                // 2) 展示 Request（base64 仅展示 Beautify）
                txtRequest.Text = JsonSerializer.Serialize(
                    new
                    {
                        model_key = modelKey,
                        public_base64 = FormatJson.Beautify(rawPublic),
                        target_base64 = FormatJson.Beautify(rawTarget)
                    },
                    new JsonSerializerOptions { WriteIndented = true }
                );

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
