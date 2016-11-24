using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RollbarDotNet
{
    public class RollbarImplementation : IRollbar
    {
        static RollbarConfig _config;
        static Func<Person> _personFunc;

        public RollbarImplementation(RollbarConfig config = null)
        {
            if (config == null)
                config = new RollbarConfig();

            _config = config;
        }

        public void PersonData(Func<Person> personFunc)
        {
            _personFunc = personFunc;
        }

        public async Task<Guid?> ReportAsync(Exception e, ErrorLevel? level = ErrorLevel.Error, IDictionary<string, object> custom = null)
        {
            if (!IsCanSend)
                return null;

            var payload = BuildPayload(new Body(e), level, custom);
            return await SendBodyAsync(payload);
        }

        public async Task<Guid?> ReportAsync(string message, ErrorLevel? level = ErrorLevel.Error, IDictionary<string, object> custom = null)
        {
            if (!IsCanSend)
                return null;

            var payload = BuildPayload(new Body(new Message(message)), level, custom);
            return await SendBodyAsync(payload);
        }

        public Guid? Report(Exception e, ErrorLevel? level = ErrorLevel.Error, IDictionary<string, object> custom = null)
        {
            if (!IsCanSend)
                return null;

            var payload = BuildPayload(new Body(e), level, custom);
            return SendBody(payload);
        }

        public Guid? Report(string message, ErrorLevel? level = ErrorLevel.Error, IDictionary<string, object> custom = null)
        {
            if (!IsCanSend)
                return null;

            var payload = BuildPayload(new Body(new Message(message)), level, custom);
            return SendBody(payload);
        }

        static bool IsCanSend
        {
            get { return !string.IsNullOrWhiteSpace(_config.AccessToken) && _config.Enabled == true; }
        }

        static Payload BuildPayload(Body body, ErrorLevel? level, IDictionary<string, object> custom)
        {
            var data = new Data(_config.Environment, body)
            {
                Custom = custom,
                Level = level ?? _config.LogLevel
            };

            var payload = new Payload(_config.AccessToken, data);
            payload.Data.GuidUuid = Guid.NewGuid();
            payload.Data.Person = _personFunc?.Invoke();

            return payload;
        }

        static async Task<Guid?> SendBodyAsync(Payload payload)
        {
            var client = new RollbarClient(_config);
            await client.PostItemAsync(payload);

            return payload.Data.GuidUuid;
        }

        static Guid? SendBody(Payload payload)
        {
            var client = new RollbarClient(_config);
            client.PostItem(payload);

            return payload.Data.GuidUuid;
        }
    }
}
