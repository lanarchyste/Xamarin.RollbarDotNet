using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace RollbarDotNet
{
    public class Frame
    {
        public Frame(string filename)
        {
            FileName = filename;
        }

        public Frame(IStackFrame frame)
        {
            var method = frame.GetMethod();

            FileName = GetFileName(frame, method);
            LineNo = GetLineNumber(frame);
            ColNo = LineNo.HasValue ? GetFileColumnNumber(frame) : null;

            Method = GetMethod(method);
        }

        [JsonProperty("filename", Required = Required.Always)]
        public string FileName { get; private set; }

        [JsonProperty("lineno", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? LineNo { get; set; }

        [JsonProperty("colno", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? ColNo { get; set; }

        [JsonProperty("method", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Method { get; set; }

        #region Unautomatable
        // These properties cannot be automated w/ normal C# StackFrames.
        // You may be able to fill this out from another .NET language.
        // They're there in case you're awesome.

        [JsonProperty("code", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Code { get; set; }

        [JsonProperty("context", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public CodeContext Context { get; set; }

        [JsonProperty("args", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string[] Args { get; set; }

        [JsonProperty("kwargs", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, object> Kwargs { get; set; }

        #endregion

        private static string GetFileName(IStackFrame frame, IMethodBase method)
        {
            var returnVal = frame.GetFileName();
            if (!string.IsNullOrWhiteSpace(returnVal))
            {
                return returnVal;
            }

            return method.ReflectedType != null ? method.ReflectedType.FullName : "(unknown)";
        }

        private static int? GetLineNumber(IStackFrame frame)
        {
            var lineNo = frame.GetFileLineNumber();
            if (lineNo != 0)
            {
                return lineNo;
            }

            lineNo = frame.GetILOffset();
            if (lineNo != -1)
            {
                return lineNo;
            }

            lineNo = frame.GetNativeOffset();
            return lineNo == -1 ? (int?)null : lineNo;
        }

        private static int? GetFileColumnNumber(IStackFrame frame)
        {
            return frame.GetFileColumnNumber();
        }

        private static string GetMethod(IMethodBase method)
        {
            var methodName = string.IsNullOrEmpty(method.Name) ? "unknown" : method.Name;
            if (method.ReflectedType != null)
            {
                methodName = string.Format("{0}.{1}", method.ReflectedType.FullName, methodName);
            }

            var parameters = method.GetParameters();
            if (parameters.Length > 0)
            {
                return string.Format("{0}({1})", methodName, string.Join(", ", parameters.Select(p => string.Format("{0} {1}", p.ParameterType, p.Name))));
            }

            return string.Format("{0}()", methodName);
        }
    }
}
