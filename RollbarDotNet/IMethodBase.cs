using System;
using System.Reflection;

namespace RollbarDotNet
{
    public interface IMethodBase
    {
        Type ReflectedType { get; }

        string Name { get; }

        ParameterInfo[] GetParameters();
    }
}
