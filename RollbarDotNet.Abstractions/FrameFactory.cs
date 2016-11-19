using System;

namespace RollbarDotNet.Abstractions
{
    public static class FrameFactory
    {
        public static void Init(Func<Exception, Frame[]> frameFactory)
        {
            Get = frameFactory;
            IsInitialized = true;
        }

        public static bool IsInitialized { get; private set; }

        public static Func<Exception, Frame[]> Get { get; private set; }
    }
}
