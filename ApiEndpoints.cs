using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace TestTool
{
    public static class ApiEndpoints
    {
        public const string DefaultConfigFileName = "api_endpoints.json";

        public sealed class ApiEndpoint
        {
            public string Group { get; }
            public string Name { get; }
            public string Url { get; }
            public string DisplayName => string.IsNullOrWhiteSpace(Group) ? Name : $"{Group} / {Name}";

            public ApiEndpoint(string group, string name, string url)
            {
                Group = group ?? "";
                Name = name ?? "";
                Url = url ?? "";
            }
        }

        private static readonly StringComparer NameComparer = StringComparer.OrdinalIgnoreCase;

        // 统一维护：相似接口放在同一分组（默认内置）
        private static readonly List<ApiEndpoint> DefaultItems = new List<ApiEndpoint>
        {
            new ApiEndpoint("基础", "detect", "http://10.53.192.100:3884/detect"),
            new ApiEndpoint("基础", "measure", "http://10.53.192.100:3884/measure"),
            new ApiEndpoint("基础", "detect_smooth", "http://10.53.192.100:3888/detect_smooth"),
            new ApiEndpoint("基础", "color", "http://10.53.192.100:3889/color"),

            new ApiEndpoint("Mirror", "mirror detect", "http://10.53.192.100:3883/detect"),
            new ApiEndpoint("Mirror", "mirror", "http://10.53.192.100:3883/mirror"),

            new ApiEndpoint("OCR", "ocr", "http://10.53.192.100:3881/ocr"),
            new ApiEndpoint("OCR", "ocr_vl", "http://10.53.192.100:3885/ocr_vl"),

            new ApiEndpoint("TSK", "tsk detect", "http://10.53.192.100:3886/detect"),
            new ApiEndpoint("TSK", "tsk etch", "http://10.53.192.100:3886/ETCH"),
            new ApiEndpoint("TSK", "tsk similarity", "http://10.53.192.100:3886/classify-similarity"),

            new ApiEndpoint("整合", "zh detect-ocr", "http://10.53.192.100:3882/detect-ocr"),
            new ApiEndpoint("整合", "zh similarity-check", "http://localhost:3882/similarity-check"),

            new ApiEndpoint("黄光", "hg detect-ocr", "http://localhost:3880/detect-ocr"),
            new ApiEndpoint("黄光", "hg similarity-check", "http://localhost:3880/similarity-check"),

            new ApiEndpoint("其他", "splice", "http://10.53.192.100:3887/splice"),
        };

        private static readonly List<ApiEndpoint> Items = new List<ApiEndpoint>(DefaultItems);
        private static Dictionary<string, string> _urlByName = BuildUrlIndex(Items);

        static ApiEndpoints()
        {
            TryLoadFromFile(ConfigPath);
        }

        public static string ConfigPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DefaultConfigFileName);

        public static IReadOnlyList<ApiEndpoint> All => Items;

        public static string GetUrl(string name, string fallback = "")
        {
            if (string.IsNullOrWhiteSpace(name))
                return fallback;

            return _urlByName.TryGetValue(name.Trim(), out var url) ? url : fallback;
        }

        public static bool TryLoadFromFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                return false;

            try
            {
                string json = File.ReadAllText(filePath);
                var payload = JsonSerializer.Deserialize<ApiEndpointConfig>(
                    json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (payload?.Endpoints == null || payload.Endpoints.Count == 0)
                    return false;

                ApplyOverrides(payload.Endpoints);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void BindTo(System.Windows.Forms.ComboBox cb, string defaultUrl, string group = "")
        {
            cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown; // ✅ 可输入
            cb.DisplayMember = "DisplayName";
            cb.ValueMember = "Url";

            IEnumerable<ApiEndpoint> source = Items;
            if (!string.IsNullOrWhiteSpace(group))
            {
                source = Items.Where(item => NameComparer.Equals(item.Group, group));
            }

            cb.DataSource = new List<ApiEndpoint>(source);

            // ✅ 输入联想
            cb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            cb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;

            // 默认值：优先匹配列表；找不到就直接填（允许手动输入的 URL）
            bool matched = false;
            foreach (var kv in Items)
            {
                if (string.Equals(kv.Url, defaultUrl, StringComparison.OrdinalIgnoreCase))
                {
                    cb.SelectedValue = defaultUrl;
                    matched = true;
                    break;
                }
            }
            if (!matched)
                cb.Text = defaultUrl;
        }

        public static void BindToUrls(System.Windows.Forms.ComboBox cb, string defaultUrl, string group = "")
        {
            cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown; // ✅ 可输入
            cb.DisplayMember = "Url";
            cb.ValueMember = "Url";

            IEnumerable<ApiEndpoint> source = Items;
            if (!string.IsNullOrWhiteSpace(group))
            {
                source = Items.Where(item => NameComparer.Equals(item.Group, group));
            }

            cb.DataSource = new List<ApiEndpoint>(source);

            // ✅ 输入联想
            cb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            cb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;

            // 默认值：优先匹配列表；找不到就直接填（允许手动输入的 URL）
            bool matched = false;
            foreach (var kv in Items)
            {
                if (string.Equals(kv.Url, defaultUrl, StringComparison.OrdinalIgnoreCase))
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

        private static Dictionary<string, string> BuildUrlIndex(IEnumerable<ApiEndpoint> endpoints)
        {
            return endpoints
                .Where(item => !string.IsNullOrWhiteSpace(item.Name))
                .ToDictionary(item => item.Name, item => item.Url, NameComparer);
        }

        private static void ApplyOverrides(IEnumerable<ApiEndpointEntry> entries)
        {
            var lookup = Items.ToDictionary(item => item.Name, NameComparer);

            foreach (var entry in entries)
            {
                if (string.IsNullOrWhiteSpace(entry.Name) || string.IsNullOrWhiteSpace(entry.Url))
                    continue;

                var group = entry.Group ?? "";
                var name = entry.Name.Trim();
                var url = entry.Url.Trim();

                if (lookup.TryGetValue(name, out var existing))
                {
                    Items.Remove(existing);
                }

                var endpoint = new ApiEndpoint(group, name, url);
                Items.Add(endpoint);
                lookup[name] = endpoint;
            }

            _urlByName = BuildUrlIndex(Items);
        }

        private sealed class ApiEndpointConfig
        {
            public List<ApiEndpointEntry> Endpoints { get; set; } = new List<ApiEndpointEntry>();
        }

        private sealed class ApiEndpointEntry
        {
            public string Group { get; set; } = "";
            public string Name { get; set; } = "";
            public string Url { get; set; } = "";
        }
    }
}
