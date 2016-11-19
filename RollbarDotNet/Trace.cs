using System;
using Newtonsoft.Json;

namespace RollbarDotNet
{
    public class Trace
	{
		public Trace(Frame[] frames, Exception exception)
		{
			if (frames == null)
			{
				throw new ArgumentNullException(nameof(frames));
			}

			if (exception == null)
			{
				throw new ArgumentNullException(nameof(exception));
			}

			Frames = frames;
			Exception = exception;
		}

		public Trace(System.Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException(nameof(exception));
			}

            if (!FrameFactory.IsInitialized || FrameFactory.Get == null)
				throw new ArgumentNullException();

            Frames = FrameFactory.Get(exception);
			Exception = new Exception(exception);
		}

		[JsonProperty("frames", Required = Required.Always)]
		public Frame[] Frames { get; private set; }

		[JsonProperty("exception", Required = Required.Always)]
		public Exception Exception { get; private set; }
	}
}
