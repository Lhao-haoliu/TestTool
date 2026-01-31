using System.Collections.Generic;

namespace TestTool
{
    public static class ApiEndpoints
    {
        // 统一维护：显示名 -> URL
        public static readonly List<KeyValuePair<string, string>> Items =
            new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("detect",        "http://10.53.192.100:3884/detect"),
                new KeyValuePair<string, string>("measure",       "http://10.53.192.100:3884/measure"),
                new KeyValuePair<string, string>("detect_smooth", "http://10.53.192.100:3888/detect_smooth"),
                new KeyValuePair<string, string>("color",         "http://10.53.192.100:3889/color"),
                new KeyValuePair<string, string>("mirror detect", "http://10.53.192.100:3883/detect"),
                new KeyValuePair<string, string>("mirror",        "http://10.53.192.100:3883/mirror"),
                new KeyValuePair<string, string>("ocr",           "http://10.53.192.100:3881/ocr"),
                new KeyValuePair<string, string>("ocr_vl",        "http://10.53.192.100:3885/ocr_vl"),
                new KeyValuePair<string, string>("splice",        "http://10.53.192.100:3887/splice"),
                new KeyValuePair<string, string>("tsk detect",    "http://10.53.192.100:3886/detect"),
                new KeyValuePair<string, string>("tsk etch",      "http://10.53.192.100:3886/ETCH"),
                new KeyValuePair<string, string>("tsk similarity","http://10.53.192.100:3886/classify-similarity"),
                new KeyValuePair<string, string>("zh detect-ocr",  "http://10.53.192.100:3882/detect-ocr"),
            };

        public static void BindTo(System.Windows.Forms.ComboBox cb, string defaultUrl)
        {
            cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown; // ✅ 可输入
            cb.DisplayMember = "Key";
            cb.ValueMember = "Value";
            cb.DataSource = new List<KeyValuePair<string, string>>(Items);

            // ✅ 输入联想
            cb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            cb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;

            // 默认值：优先匹配列表；找不到就直接填（允许手动输入的 URL）
            bool matched = false;
            foreach (var kv in Items)
            {
                if (string.Equals(kv.Value, defaultUrl))
                {
                    cb.SelectedValue = defaultUrl;
                    matched = true;
                    break;
                }
            }
            if (!matched)
                cb.Text = defaultUrl;
        }

        // 获取用户最终输入的URL（选中或手输）
        public static string GetUrl(System.Windows.Forms.ComboBox cb)
        {
            // 选中列表项：SelectedValue 有值
            if (cb.SelectedValue != null)
                return cb.SelectedValue.ToString();

            // 手动输入：取 Text
            return (cb.Text ?? "").Trim();
        }
    }
}
