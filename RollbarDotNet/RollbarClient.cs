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
            if (response.Error > 0)
            {
                Debug.WriteLine("[RollbarDotNet] Unable to parse the response. Error : " + response.Error);
                return Guid.Empty;
            }

            Guid uuid;
            if (Guid.TryParse(response.Result.Uuid, out uuid))
                return uuid;

            return Guid.Empty;
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
                Debug.WriteLine("[RollbarDotNet] The request sent to the api failed.");
                Debug.WriteLine(ex);
                return string.Empty;
            }
        }
    }
}
