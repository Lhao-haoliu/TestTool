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
    public sealed class ExpectSingleForm : Form
    {
        private static readonly HttpClient HttpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        private readonly ComboBox txtUrl = new ComboBox();
        private readonly TextBox txtModelKey = new TextBox();
        private readonly TextBox txtExpect = new TextBox();
        private readonly Button btnSelectImage = new Button();
        private readonly PictureBox pictureBoxImage = new PictureBox();
        private readonly TextBox txtBase64 = new TextBox();
        private readonly Button btnSend = new Button();
        private readonly TextBox txtRequest = new TextBox();
        private readonly TextBox txtResponse = new TextBox();

        public ExpectSingleForm()
        {
            Text = "单图 + Expect";
            Width = 1100;
            Height = 860;
            StartPosition = FormStartPosition.CenterScreen;

            BuildLayout();

            Load += ExpectSingleForm_Load;
            btnSelectImage.Click += BtnSelectImage_Click;
            btnSend.Click += BtnSend_Click;
        }

        private void ExpectSingleForm_Load(object sender, EventArgs e)
        {
            ApiEndpoints.BindToUrls(txtUrl, ApiEndpoints.GetUrl("expect", ""), "");
        }

        private void BuildLayout()
        {
            var lblUrl = new Label
            {
                Text = "接口 URL：",
                Location = new Point(12, 14),
                AutoSize = true
            };
            txtUrl.Location = new Point(80, 10);
            txtUrl.Width = 900;

            var lblModelKey = new Label
            {
                Text = "model_key：",
                Location = new Point(12, 44),
                AutoSize = true
            };
            txtModelKey.Location = new Point(80, 40);
            txtModelKey.Width = 200;

            var lblExpect = new Label
            {
                Text = "expect：",
                Location = new Point(300, 44),
                AutoSize = true
            };
            txtExpect.Location = new Point(360, 40);
            txtExpect.Width = 200;

            btnSelectImage.Text = "上传图片";
            btnSelectImage.Location = new Point(12, 74);
            btnSelectImage.Size = new Size(100, 26);

            pictureBoxImage.Location = new Point(12, 110);
            pictureBoxImage.Size = new Size(320, 240);
            pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxImage.BorderStyle = BorderStyle.FixedSingle;

            txtBase64.Location = new Point(12, 360);
            txtBase64.Size = new Size(520, 140);
            txtBase64.Multiline = true;
            txtBase64.ScrollBars = ScrollBars.Both;
            txtBase64.WordWrap = false;

            btnSend.Text = "发送";
            btnSend.Location = new Point(12, 510);
            btnSend.Size = new Size(100, 26);

            txtRequest.Location = new Point(560, 110);
            txtRequest.Size = new Size(500, 300);
            txtRequest.Multiline = true;
            txtRequest.ScrollBars = ScrollBars.Both;
            txtRequest.WordWrap = false;

            txtResponse.Location = new Point(560, 430);
            txtResponse.Size = new Size(500, 300);
            txtResponse.Multiline = true;
            txtResponse.ScrollBars = ScrollBars.Both;
            txtResponse.WordWrap = false;

            var lblRequest = new Label
            {
                Text = "Request",
                Location = new Point(560, 90),
                AutoSize = true
            };
            var lblResponse = new Label
            {
                Text = "Response",
                Location = new Point(560, 410),
                AutoSize = true
            };

            Controls.Add(lblUrl);
            Controls.Add(txtUrl);
            Controls.Add(lblModelKey);
            Controls.Add(txtModelKey);
            Controls.Add(lblExpect);
            Controls.Add(txtExpect);
            Controls.Add(btnSelectImage);
            Controls.Add(pictureBoxImage);
            Controls.Add(txtBase64);
            Controls.Add(btnSend);
            Controls.Add(lblRequest);
            Controls.Add(txtRequest);
            Controls.Add(lblResponse);
            Controls.Add(txtResponse);
        }

        private void BtnSelectImage_Click(object sender, EventArgs e)
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

        private async void BtnSend_Click(object sender, EventArgs e)
        {
            btnSend.Enabled = false;

            try
            {
                string url = ApiEndpoints.GetUrl(txtUrl);
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
                string expect = txtExpect.Text.Trim();

                var sendObj = new
                {
                    image_base64 = rawBase64,
                    model_key = modelKey,
                    expect = expect
                };

                string sendJson = JsonSerializer.Serialize(sendObj);

                txtRequest.Text = JsonSerializer.Serialize(
                    new
                    {
                        image_base64 = FormatJson.Beautify(rawBase64),
                        model_key = modelKey,
                        expect = expect
                    },
                    new JsonSerializerOptions { WriteIndented = true }
                );

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
    }
}
