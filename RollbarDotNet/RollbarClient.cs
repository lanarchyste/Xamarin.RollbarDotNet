using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RollbarDotNet
{
	/// <summary>
	/// Client for accessing the Rollbar API
	/// </summary>
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

		private static Guid ParseResponse(string stringResult)
		{
			var response = JsonConvert.DeserializeObject<RollbarResponse>(stringResult);
			return Guid.Parse(response.Result.Uuid);
		}

		private async Task<string> SendPostAsync<T>(string url, T payload)
		{
			var httpClient = new HttpClient();
			httpClient.BaseAddress = (new Uri(Config.EndPoint));

			var result = await httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(payload)));
			return await result.Content.ReadAsStringAsync();
		}
	}
}
