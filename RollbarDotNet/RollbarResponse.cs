using Newtonsoft.Json;

namespace RollbarDotNet
{
	public class RollbarResponse
	{
        [JsonConstructor]
        public RollbarResponse()
        {
            Error = 1;
            Result = null;
        }

        [JsonProperty("err")]
		public int Error { get; set; }

		public RollbarResult Result { get; set; }
	}
}
