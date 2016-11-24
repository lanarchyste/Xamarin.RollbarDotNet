using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RollbarDotNet
{
    public interface IRollbar
    {
        void PersonData(Func<Person> personFunc);

        Task<Guid?> ReportAsync(Exception e, ErrorLevel? level = ErrorLevel.Error, IDictionary<string, object> custom = null);

        Task<Guid?> ReportAsync(string message, ErrorLevel? level = ErrorLevel.Error, IDictionary<string, object> custom = null);

        Guid? Report(Exception e, ErrorLevel? level = ErrorLevel.Error, IDictionary<string, object> custom = null);

        Guid? Report(string message, ErrorLevel? level = ErrorLevel.Error, IDictionary<string, object> custom = null);
    }
}
