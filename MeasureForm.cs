using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestTool
{
    public partial class MeasureForm : Form
    {

        private void MeasureForm_Load(object sender, EventArgs e)
        {
            // 默认值（你可以按需改）
            txtUrl.Text = ApiEndpoints.GetUrl("measure");
            txtModelKey.Text = "Dummy";

        }

        private static readonly HttpClient _http = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        private readonly JsonSerializerOptions _jsonOpt = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public class MeasureRequest
        {
            [JsonPropertyName("image_base64")]
            public string ImageBase64 { get; set; } = "";

            [JsonPropertyName("model_key")]
            public string ModelKey { get; set; } = "Dummy";
        }

        public class MeasureResponse
        {
            [JsonPropertyName("measure")]
            public MeasureData Measure { get; set; } = new MeasureData();
        }

        public class MeasureData
        {
            [JsonPropertyName("horizontal")]
            public List<PointData> Horizontal { get; set; } = new List<PointData>();

            [JsonPropertyName("vertical")]
            public List<PointData> Vertical { get; set; } = new List<PointData>();
        }

        public class PointData
        {
            [JsonPropertyName("x")]
            public int X { get; set; }

            [JsonPropertyName("y")]
            public int Y { get; set; }
        }

        public MeasureForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择图片";
            ofd.Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            string path = ofd.FileName;

            // 1. 图片预览（避免文件占用）
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                pictureBoxImage.Image = System.Drawing.Image.FromStream(fs);
            }

            // 2. 转 Base64（纯 base64，不带 data:image）
            byte[] bytes = File.ReadAllBytes(path);
            string base64 = Convert.ToBase64String(bytes);

            txtBase64.Text = base64;
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            btnSend.Enabled = false;

            try
            {
                // 1) URL（界面输入，带默认值）
                string url = txtUrl.Text.Trim();
                if (string.IsNullOrWhiteSpace(url))
                {
                    MessageBox.Show("请输入接口 URL");
                    return;
                }
                if (!url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("URL 必须以 http 或 https 开头");
                    return;
                }

                // 2) model_key（界面输入，带默认值 Dummy）
                string modelKey = txtModelKey.Text.Trim();

                // 3) Base64（上传图片生成）
                string base64 = txtBase64.Text.Trim();
                if (string.IsNullOrWhiteSpace(base64))
                {
                    MessageBox.Show("请先上传图片并生成 Base64");
                    return;
                }

                // 4) 构建请求：实际发送（完整 base64）
                var reqSend = new MeasureRequest
                {
                    ImageBase64 = base64,
                    ModelKey = modelKey
                };

                // 5) 构建请求：界面展示（base64 脱敏，避免刷屏）
                var reqShow = new MeasureRequest
                {
                    ImageBase64 = FormatJson.Beautify(base64),
                    ModelKey = modelKey
                };

                string reqJsonSend = JsonSerializer.Serialize(reqSend, _jsonOpt);
                string reqJsonShow = JsonSerializer.Serialize(reqShow, new JsonSerializerOptions { WriteIndented = true });

                // ✅ 展示 Request JSON
                txtRequest.Text = FormatJson.Beautify(reqJsonShow);

                // 6) 发送
                var content = new StringContent(reqJsonSend, Encoding.UTF8, "application/json");
                using var resp = await _http.PostAsync(url, content);
                string respText = await resp.Content.ReadAsStringAsync();

                // ✅ 展示 Response JSON（格式化）
                txtResponse.Text = FormatJson.Beautify(respText);

                // 非成功状态提示
                if (!resp.IsSuccessStatusCode)
                {
                    MessageBox.Show($"接口返回错误：HTTP {(int)resp.StatusCode} {resp.StatusCode}");
                    return;
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
