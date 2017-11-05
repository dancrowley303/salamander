using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace com.defrobo.salamander
{
    public class InfoService
    {
        private readonly string apiKey;
        private readonly string apiSecret;
        private readonly HttpClient httpClient;
        private readonly Uri endpointUri = new Uri("https://api.bitflyer.jp");

        public InfoService()
        {
            apiKey = Environment.GetEnvironmentVariable("BITFLYER_API_KEY");
            apiSecret = Environment.GetEnvironmentVariable("BITFLYER_API_SECRET");
            httpClient = new HttpClient();
            httpClient.BaseAddress = endpointUri;
        }

        public void Close()
        {
            if (httpClient != null)
                httpClient.Dispose();
        }

        public async Task<Dictionary<Currency, Balance>> GetBalances()
        { 
            var method = "GET";
            var path = "/v1/me/getbalance";
            var query = "";

            var httpResponse = await DoHttpRequest(method, path, query);
            var stringResult = await httpResponse.Content.ReadAsStringAsync();
            var rawBalances = JsonConvert.DeserializeObject<List<Balance>>(stringResult);

            var balances = new Dictionary<Currency, Balance>();

            foreach(var rawBalance in rawBalances)
            {
                balances.Add(rawBalance.Currency, rawBalance);
            }

            return balances;
        }

        private async Task<HttpResponseMessage> DoHttpRequest(string method, string path, string query)
        {
            using (var request = new HttpRequestMessage(new HttpMethod(method), path + query))
            {
                var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
                var data = timestamp + method + path + query;
                var hash = SignWithHMACSHA256(data, apiSecret);
                request.Headers.Add("ACCESS-KEY", apiKey);
                request.Headers.Add("ACCESS-TIMESTAMP", timestamp);
                request.Headers.Add("ACCESS-SIGN", hash);

                return await httpClient.SendAsync(request);
            }
        }

        private static string SignWithHMACSHA256(string data, string secret)
        {
            using (var encoder = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                var hash = encoder.ComputeHash(Encoding.UTF8.GetBytes(data));
                return ToHexString(hash);
            }
        }

        private static string ToHexString(byte[] bytes)
        {
            var sb = new StringBuilder(bytes.Length * 2);
            foreach(var b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
