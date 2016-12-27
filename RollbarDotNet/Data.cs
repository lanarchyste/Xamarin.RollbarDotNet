using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace RollbarDotNet
{
    public class Data
    {
        static readonly string NotifierAssemblyVersion = typeof(Data).GetTypeInfo().Assembly.GetName().Version.ToString(3);

        const string defaultValue = "Unknown";

        const string defaultPlatform = defaultValue;
        const string defaultFramework = defaultValue;
        const string defaultLanguage = "C#";
        const string defaultVersion = defaultValue;
        const string defaultModel = defaultValue;
        const string defaultOsVersion = defaultValue;

        static string _platform;
        static string _framework;
        static string _version;
        static string _model;
        static string _osVersion;

        public Data(string environment, Body body)
        {
            if (string.IsNullOrWhiteSpace(environment))
                throw new ArgumentNullException(nameof(environment));

            if (body == null)
                throw new ArgumentNullException(nameof(body));

            Environment = environment;
            Body = body;
            Timestamp = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            Platform = string.IsNullOrEmpty(_platform) ? defaultPlatform : _platform;
            Language = defaultLanguage;
            Framework = string.IsNullOrEmpty(_framework) ? defaultFramework : _framework;
            Model = string.IsNullOrEmpty(_model) ? defaultModel : _model;
            AppVersion = string.IsNullOrEmpty(_version) ? defaultVersion : _version;
            OSVersion = string.IsNullOrEmpty(_osVersion) ? defaultOsVersion : _osVersion;
        }

        public static void SetFramework(string framework)
        {
            _framework = framework;
        }

        public static void SetPlatform(string platform)
        {
            _platform = platform;
        }

        public static void SetModel(string model)
        {
            _model = model;
        }

        public static void SetAppVersion(string version)
        {
            _version = version;
        }

        public static void SetOSVersion(string osVersion)
        {
            _osVersion = osVersion;
        }

        [JsonProperty("environment", Required = Required.Always)]
        public string Environment { get; private set; }

        [JsonProperty("body", Required = Required.Always)]
        public Body Body { get; private set; }

        [JsonProperty("level", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ErrorLevel? Level { get; set; }

        [JsonProperty("timestamp", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long? Timestamp { get; set; }

        [JsonProperty("code_version", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string CodeVersion { get; set; }

        [JsonProperty("platform", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Platform { get; set; }

        [JsonProperty("language", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Language { get; set; }

        [JsonProperty("framework", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Framework { get; set; }

        [JsonProperty("context", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Context { get; set; }

        [JsonProperty("request", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Request Request { get; set; }

        [JsonProperty("person", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Person Person { get; set; }

        [JsonProperty("server", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Server Server { get; set; }

        [JsonProperty("client", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Client Client { get; set; }

        [JsonProperty("custom", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IDictionary<string, object> Custom { get; set; }

        [JsonProperty("fingerprint", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Fingerprint { get; set; }

        [JsonProperty("title", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("uuid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Uuid { get; set; }

        [JsonProperty("model", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Model { get; set; }

        [JsonProperty("version.app", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AppVersion { get; set; }

        [JsonProperty("version.os", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string OSVersion { get; set; }

        [JsonIgnore]
        public Guid? GuidUuid
        {
            get { return Uuid == null ? (Guid?)null : Guid.Parse(Uuid); }
            set { Uuid = value == null ? null : value.Value.ToString("N"); }
        }

        [JsonProperty("notifier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, string> Notifier
        {
            get
            {
                return new Dictionary<string, string>
                {
                    { "name", "Xamarin Rollbar.NET" },
                    { "version", NotifierAssemblyVersion },
                };
            }
        }
    }
}
