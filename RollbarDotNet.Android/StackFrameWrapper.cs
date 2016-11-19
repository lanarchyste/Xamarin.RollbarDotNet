using System.Diagnostics;
using RollbarDotNet.Abstractions;

namespace RollbarDotNet.Android
{
	public class StackFrameWrapper : IStackFrame
	{
		readonly StackFrame _stackFrame;

		public StackFrameWrapper(StackFrame stackFrame)
		{
			_stackFrame = stackFrame;
		}

		public int GetFileColumnNumber()
		{
			return _stackFrame.GetFileColumnNumber();
		}

		public int GetFileLineNumber()
		{
			return _stackFrame.GetFileLineNumber();
		}

		public string GetFileName()
		{
			return _stackFrame.GetFileName();
		}

		public int GetILOffset()
		{
			return _stackFrame.GetILOffset();
		}

		public IMethodBase GetMethod()
		{
			return new MethodBaseWrapper(_stackFrame.GetMethod());
		}

		public int GetNativeOffset()
		{
			return _stackFrame.GetNativeOffset();
		}
	}
}
