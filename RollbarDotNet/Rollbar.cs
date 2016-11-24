using System;

namespace RollbarDotNet
{
    public static class Rollbar
    {
        static IRollbar _value;

        public static IRollbar Current
        {
            get
            {
                if (_value == null)
                    throw new NullReferenceException("Call Init() method");

                return _value;
            }
            set { _value = value; }
        }

        public static bool IsInitialized
        {
            get { return _value != null; }
        }
    }
}
