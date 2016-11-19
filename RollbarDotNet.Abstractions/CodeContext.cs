using Newtonsoft.Json;

namespace RollbarDotNet.Abstractions
{
	public class CodeContext
	{
		[JsonProperty("pre", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string[] Pre { get; set; }

		[JsonProperty("post", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string[] Post { get; set; }
	}
}
