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
    public partial class TskDetectForm : Form
    {
        private static readonly HttpClient _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        public TskDetectForm()
        {
            InitializeComponent();

            // 缩略图显示
            pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxImage.BorderStyle = BorderStyle.FixedSingle;
        }

        private void TskDetectForm_Load(object sender, EventArgs e)
        {
            txtUrl.Text = ApiEndpoints.GetUrl("tsk detect");
            txtModelKey.Text = "multiple";
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "选择图片",
                Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            byte[] bytes = File.ReadAllBytes(ofd.FileName);

            // base64（raw）
            string base64 = Convert.ToBase64String(bytes);
            txtBase64.Text = base64;

            // 预览缩略图（不锁文件）
            var old = pictureBoxImage.Image;
            pictureBoxImage.Image = null;
            old?.Dispose();

            using var ms = new MemoryStream(bytes);
            using var img = Image.FromStream(ms);
            pictureBoxImage.Image = new Bitmap(img);
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

                string rawBase64 = txtBase64.Text.Trim();
                if (string.IsNullOrWhiteSpace(rawBase64))
                {
                    MessageBox.Show("请先上传图片");
                    return;
                }

                string modelKey = txtModelKey.Text.Trim();

                // 1) 发送 JSON（不缩进）
                string sendJson = JsonSerializer.Serialize(new
                {
                    image_base64 = rawBase64,
                    model_key = modelKey
                });

                // 2) 界面展示 Request（仅展示可 Beautify base64）
                txtRequest.Text = JsonSerializer.Serialize(
                    new
                    {
                        image_base64 = FormatJson.Beautify(rawBase64),
                        model_key = modelKey
                    },
                    new JsonSerializerOptions { WriteIndented = true }
                );

                // 3) 关键：严格 Content-Type = application/json（不要 charset）
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
