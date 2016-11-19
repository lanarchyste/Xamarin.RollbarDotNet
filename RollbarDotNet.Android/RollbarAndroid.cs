using System.Diagnostics;
using System.Linq;

namespace RollbarDotNet.Android
{
    public static class RollbarAndroid
    {
        public static void Init(RollbarConfig config = null)
        {
            FrameFactory.Init(FrameBuilder);
            Rollbar.Init(config);
            Data.DefaultPlatform = "Android";
        }

        static Frame[] FrameBuilder(System.Exception exception)
        {
            var frames = new StackTrace(exception, true).GetFrames() ?? new StackFrame[0];
            return frames
                .Select(f => new StackFrameWrapper(f))
                .Select(f => new Frame(f))
                .ToArray();
        }
    }
}
