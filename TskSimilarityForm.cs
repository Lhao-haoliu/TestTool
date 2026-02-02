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
    public partial class TskSimilarityForm : Form
    {
        private static readonly HttpClient _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        public TskSimilarityForm()
        {
            InitializeComponent();

            pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxImage.BorderStyle = BorderStyle.FixedSingle;

            pictureBoxPublic.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPublic.BorderStyle = BorderStyle.FixedSingle;
        }

        private void TskSimilarityForm_Load(object sender, EventArgs e)
        {
            txtUrl.Text = ApiEndpoints.GetUrl("tsk similarity");
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            SelectImageTo(txtImageBase64, pictureBoxImage);
        }

        private void btnSelectPublic_Click(object sender, EventArgs e)
        {
            SelectImageTo(txtPublicBase64, pictureBoxPublic);
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

                string rawImageBase64 = txtImageBase64.Text.Trim();
                if (string.IsNullOrWhiteSpace(rawImageBase64))
                {
                    MessageBox.Show("请上传 实际图片（image_base64）");
                    return;
                }

                string rawPublicBase64 = txtPublicBase64.Text.Trim();
                if (string.IsNullOrWhiteSpace(rawPublicBase64))
                {
                    MessageBox.Show("请上传 公版图片（public_base64）");
                    return;
                }

                // 1) 发送 JSON（raw base64，不 Beautify）
                string sendJson = JsonSerializer.Serialize(new
                {
                    image_base64 = rawImageBase64,
                    public_base64 = rawPublicBase64
                });

                // 2) 界面展示 Request（仅用于展示可以 Beautify）
                txtRequest.Text = JsonSerializer.Serialize(
                    new
                    {
                        image_base64 = FormatJson.Beautify(rawImageBase64),
                        public_base64 = FormatJson.Beautify(rawPublicBase64)
                    },
                    new JsonSerializerOptions { WriteIndented = true }
                );

                // 3) 严格 Content-Type = application/json（不要 charset）
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
