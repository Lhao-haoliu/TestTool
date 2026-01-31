using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace TestTool
{
    public partial class DetectForm : Form
    {
        private static readonly HttpClient _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        private readonly JsonSerializerOptions _jsonOpt = new JsonSerializerOptions
        {
            WriteIndented = false
        };

        public DetectForm()
        {
            InitializeComponent();
            pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxImage.BorderStyle = BorderStyle.FixedSingle;
        }

        private void DetectForm_Load(object sender, EventArgs e)
        {
            txtUrl.Text = ApiEndpoints.GetUrl("detect");
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
            txtBase64.Text = Convert.ToBase64String(bytes);

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

                string base64 = txtBase64.Text.Trim();
                if (string.IsNullOrWhiteSpace(base64))
                {
                    MessageBox.Show("请先上传图片");
                    return;
                }

                // 实际发送
                var sendObj = new DetectRequest
                {
                    ImageBase64 = base64
                };

                // 界面展示
                var showObj = new DetectRequest
                {
                    ImageBase64 = FormatJson.Beautify(base64)
                };

                string sendJson = JsonSerializer.Serialize(sendObj, _jsonOpt);
                string showJson = JsonSerializer.Serialize(showObj, new JsonSerializerOptions { WriteIndented = true });

                txtRequest.Text = showJson;

                var content = new StringContent(sendJson, Encoding.UTF8, "application/json");
                using var resp = await _http.PostAsync(url, content);
                string respText = await resp.Content.ReadAsStringAsync();

                txtResponse.Text = FormatJson.Beautify(respText);

                if (!resp.IsSuccessStatusCode)
                {
                    MessageBox.Show($"HTTP {(int)resp.StatusCode} {resp.StatusCode}");
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

    public class DetectRequest
    {
        [JsonPropertyName("image_base64")]
        public string ImageBase64 { get; set; } = "";
    }

}
