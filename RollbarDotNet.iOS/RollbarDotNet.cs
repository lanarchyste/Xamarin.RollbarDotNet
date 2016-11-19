using System.Diagnostics;
using System.Linq;
using RollbarDotNet.Abstractions;

namespace RollbarDotNet.iOS
{
    public static class RollbarDotNet
    {
        public static void Init()
        {
            FrameFactory.Init(FrameBuilder);
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
