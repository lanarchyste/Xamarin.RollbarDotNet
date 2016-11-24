using System;
using System.Reflection;

namespace RollbarDotNet.iOS
{
    public class MethodBaseWrapper : IMethodBase
    {
        readonly MethodBase _methodBase;

        public MethodBaseWrapper(MethodBase methodBase)
        {
            _methodBase = methodBase;
        }

        public string Name
        {
            get { return _methodBase?.Name; }
        }

        public Type ReflectedType
        {
            get { return _methodBase?.ReflectedType; }
        }

        public ParameterInfo[] GetParameters()
        {
            if (_methodBase == null)
                return new ParameterInfo[0];

            return _methodBase.GetParameters();
        }
    }
}
