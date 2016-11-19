namespace RollbarDotNet.Abstractions
{
	public interface IStackFrame
	{
		string GetFileName();

		IMethodBase GetMethod();

		int GetFileColumnNumber();

		int GetFileLineNumber();

		int GetILOffset();

		int GetNativeOffset();
	}
}
