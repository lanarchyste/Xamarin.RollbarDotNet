using System;
using System.Threading.Tasks;

namespace RollbarDotNet
{
    public interface IRollbarClient
    {
        Task<Guid> PostItemAsync(Payload payload);
    }
}
