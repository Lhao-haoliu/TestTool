using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace TestTool
{
    public partial class HgDetectOcrForm : Form
    {
        private static readonly HttpClient _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        public HgDetectOcrForm()
        {
            InitializeComponent();

            pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxImage.BorderStyle = BorderStyle.FixedSingle;
        }

        private void HgDetectOcrForm_Load(object sender, EventArgs e)
        {
            txtUrl.Text = "http://localhost:3880/detect-ocr";
            txtModelKey.Text = "";
            txtModel1Conf.Text = "0.5";
            txtModel2Conf.Text = "0.5";
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

            // base64（raw，发送必须用这个，不要 Beautify）
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

        private static double ParseDoubleOrDefault(string s, double def)
        {
            if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out double v))
                return v;
            if (double.TryParse(s, NumberStyles.Float, CultureInfo.CurrentCulture, out v))
                return v;
            return def;
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

                string modelKey = txtModelKey.Text.Trim();
                if (string.IsNullOrWhiteSpace(modelKey))
                {
                    MessageBox.Show("请输入 model_key（大类）");
                    return;
                }

                string rawBase64 = txtBase64.Text.Trim();
                if (string.IsNullOrWhiteSpace(rawBase64))
                {
                    MessageBox.Show("请先上传图片");
                    return;
                }

                double model1Conf = ParseDoubleOrDefault(txtModel1Conf.Text.Trim(), 0.5);
                double model2Conf = ParseDoubleOrDefault(txtModel2Conf.Text.Trim(), 0.5);

                // 1) 发送 JSON（raw base64，不 Beautify）
                var sendObj = new
                {
                    model_key = modelKey,
                    large_base64 = rawBase64,
                    model1_conf = model1Conf,
                    model2_conf = model2Conf
                };

                string sendJson = JsonSerializer.Serialize(sendObj);

                // 2) 界面展示 Request（仅展示可 Beautify base64）
                txtRequest.Text = JsonSerializer.Serialize(
                    new
                    {
                        model_key = modelKey,
                        large_base64 = FormatJson.Beautify(rawBase64),
                        model1_conf = model1Conf,
                        model2_conf = model2Conf
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
