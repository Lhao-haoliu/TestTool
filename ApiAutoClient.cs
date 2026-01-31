using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestTool
{
    public enum SendMode { Json, FormUrlEncoded, Multipart }

    public class TryResult
    {
        public SendMode Mode { get; set; }
        public int StatusCode { get; set; }
        public string Reason { get; set; } = "";
        public string ContentTypeSent { get; set; } = "";
        public string ResponseText { get; set; } = "";
        public bool IsSuccess { get; set; }
    }

    public static class ApiAutoClient
    {
        private static readonly HttpClient _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        public static async Task<List<TryResult>> PostAutoAsync(string url, Dictionary<string, string> fields)
        {
            var results = new List<TryResult>();

            // 按顺序尝试三种 Content-Type
            var modes = new[] { SendMode.Json, SendMode.FormUrlEncoded, SendMode.Multipart };

            foreach (var mode in modes)
            {
                var r = await PostOnceAsync(url, fields, mode);
                results.Add(r);

                if (r.IsSuccess) break; // 成功就停止
            }

            return results;
        }

        private static async Task<TryResult> PostOnceAsync(string url, Dictionary<string, string> fields, SendMode mode)
        {
            HttpResponseMessage resp;
            string sentContentType;

            if (mode == SendMode.Json)
            {
                sentContentType = "application/json";
                string sendJson = JsonSerializer.Serialize(fields);
                using var content = new StringContent(sendJson, Encoding.UTF8, sentContentType);
                resp = await _http.PostAsync(url, content);
            }
            else if (mode == SendMode.FormUrlEncoded)
            {
                sentContentType = "application/x-www-form-urlencoded";
                using var content = new FormUrlEncodedContent(fields);
                resp = await _http.PostAsync(url, content);
            }
            else
            {
                sentContentType = "multipart/form-data";
                using var content = new MultipartFormDataContent();
                foreach (var kv in fields)
                    content.Add(new StringContent(kv.Value ?? ""), kv.Key);

                resp = await _http.PostAsync(url, content);
            }

            string text = await resp.Content.ReadAsStringAsync();

            return new TryResult
            {
                Mode = mode,
                StatusCode = (int)resp.StatusCode,
                Reason = resp.ReasonPhrase ?? "",
                ContentTypeSent = sentContentType,
                ResponseText = text,
                IsSuccess = resp.IsSuccessStatusCode
            };
        }
    }
}
