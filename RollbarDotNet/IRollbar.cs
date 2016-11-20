using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RollbarDotNet
{
    public interface IRollbar
    {
        void PersonData(Func<Person> personFunc);

        Task<Guid?> Report(System.Exception e, ErrorLevel? level = ErrorLevel.Error, IDictionary<string, object> custom = null);

        Task<Guid?> Report(string message, ErrorLevel? level = ErrorLevel.Error, IDictionary<string, object> custom = null);
    }
}
