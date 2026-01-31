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
    public partial class ZhDetectOcrForm : Form
    {
        private static readonly HttpClient _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        public ZhDetectOcrForm()
        {
            InitializeComponent();

            pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxImage.BorderStyle = BorderStyle.FixedSingle;
        }

        private void ZhDetectOcrForm_Load(object sender, EventArgs e)
        {
            txtUrl.Text = "http://10.53.192.100:3882/detect-ocr";

            // 下拉选项（允许手动输入）
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

            // 默认值（你可改）
            cmbModelKey.Text = "Dummy";
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

            // raw base64（发送必须用这个）
            string base64 = Convert.ToBase64String(bytes);
            txtBase64.Text = base64;

            // 缩略图预览（不锁文件）
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

                string modelKey = cmbModelKey.Text.Trim();
                if (string.IsNullOrWhiteSpace(modelKey))
                {
                    MessageBox.Show("请选择或输入 model_key");
                    return;
                }

                // 1) 发送 JSON（raw base64，不 Beautify）
                string sendJson = JsonSerializer.Serialize(new
                {
                    image_base64 = rawBase64,
                    model_key = modelKey
                });

                // 2) Request 展示（仅展示可 Beautify）
                txtRequest.Text = JsonSerializer.Serialize(
                    new
                    {
                        image_base64 = FormatJson.Beautify(rawBase64),
                        model_key = modelKey
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
