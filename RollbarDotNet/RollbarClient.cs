using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RollbarDotNet
{
    public class RollbarClient : IRollbarClient
    {
        public RollbarConfig Config { get; }

        public RollbarClient(RollbarConfig config)
        {
            Config = config;
        }

        public async Task<Guid> PostItemAsync(Payload payload)
        {
            var stringResult = await SendPostAsync("item/", payload);
            return ParseResponse(stringResult);
        }

        static Guid ParseResponse(string stringResult)
        {
            if (string.IsNullOrEmpty(stringResult))
                return Guid.Empty;

            var response = JsonConvert.DeserializeObject<RollbarResponse>(stringResult);
            return Guid.Parse(response.Result.Uuid);
        }

        async Task<string> SendPostAsync<T>(string url, T payload)
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.BaseAddress = (new Uri(Config.EndPoint));

                var result = await httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(payload)));
                return await result.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[RollbarDotNet] Unable to post report");
                Debug.WriteLine(ex);
                return string.Empty;
            }
        }
    }
}
