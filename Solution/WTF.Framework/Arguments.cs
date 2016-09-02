using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WTF.Framework
{

    public static class Arguments
    {
        [DebuggerStepThrough]
        public static void Argument(bool condition, string parameterName, string message)
        {
            if (!condition)
            {
                throw new ArgumentException(message, parameterName);
            }
        }

        [DebuggerStepThrough]
        public static void Argument(bool condition, string parameterName, string message, object arg1)
        {
            if (!condition)
            {
                throw new ArgumentException(string.Format(message, arg1), parameterName);
            }
        }

        [DebuggerStepThrough]
        public static void Argument(bool condition, string parameterName, string message, params object[] args)
        {
            if (!condition)
            {
                throw new ArgumentException(string.Format(message, args), parameterName);
            }
        }

        [DebuggerStepThrough]
        public static void Argument(bool condition, string parameterName, string message, object arg1, object arg2)
        {
            if (!condition)
            {
                throw new ArgumentException(string.Format(message, arg1, arg2), parameterName);
            }
        }

        [DebuggerStepThrough]
        public static Exception Fail(string message)
        {
            throw new ArgumentException(message);
        }

        [DebuggerStepThrough]
        public static Exception Fail(string unformattedMessage, params object[] args)
        {
            throw Fail(string.Format(unformattedMessage, args));
        }

        [DebuggerStepThrough]
        public static Exception Fail(Exception innerException, string unformattedMessage, params object[] args)
        {
            throw new ArgumentException(string.Format(unformattedMessage, args), innerException);
        }

        [DebuggerStepThrough]
        public static Exception FailRange(string parameterName, string message = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
            throw new ArgumentOutOfRangeException(parameterName, message);
        }

        [DebuggerStepThrough]
        public static IntPtr NotNull(IntPtr value, string parameterName)
        {
            if (value == IntPtr.Zero)
            {
                throw new ArgumentNullException(parameterName);
            }
            return value;
        }

        [DebuggerStepThrough]
        public static T NotNull<T>(T value, string parameterName) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
            return value;
        }

        [DebuggerStepThrough]
        public static void NotNullEmptyOrNullElements<T>(IEnumerable<T> values, string parameterName) where T : class
        {
            NotNull<IEnumerable<T>>(values, parameterName);
            bool flag = false;
            foreach (T local in values)
            {
                flag = true;
                if (local == null)
                {
                    throw new ArgumentException(string.Format("'{0}' cannot contain a null element.", parameterName), parameterName);
                }
            }
            if (!flag)
            {
                throw new ArgumentException(string.Format("'{0}' must contain at least one element.", parameterName), parameterName);
            }
        }

        [DebuggerStepThrough]
        public static void NotNullOrEmpty(IEnumerable values, string parameterName)
        {
            if (values == null)
            {
                throw new ArgumentNullException(parameterName);
            }
            bool flag = false;
            IEnumerator enumerator = values.GetEnumerator();
            if (enumerator.MoveNext())
            {
                object current = enumerator.Current;
                flag = true;
            }

            if (!flag)
            {
                throw new ArgumentException(string.Format("'{0}' must contain at least one element.", parameterName), parameterName);
            }
        }

        [DebuggerStepThrough]
        public static void NotNullOrEmpty(string value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
            if ((value.Length == 0) || (value[0] == '\0'))
            {
                throw new ArgumentException(string.Format("'{0}' cannot be an empty string or start with the null character.", parameterName), parameterName);
            }
        }

        [DebuggerStepThrough]
        public static void NotNullOrWhiteSpace(string value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
            if ((value.Length == 0) || (value[0] == '\0'))
            {
                throw new ArgumentException(string.Format("'{0}' cannot be an empty string or start with the null character.", parameterName), parameterName);
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(string.Format("The parameter '{0}' cannot consist entirely of white space characters.", parameterName));
            }
        }

        [DebuggerStepThrough]
        public static void NullOrNotNullElements<T>(IEnumerable<T> values, string parameterName)
        {
            if (values != null)
            {
                foreach (T local in values)
                {
                    if (local == null)
                    {
                        throw new ArgumentException(string.Format("'{0}' cannot contain a null element.", parameterName), parameterName);
                    }
                }
            }
        }

        [DebuggerStepThrough]
        public static void Range(bool condition, string parameterName, string message = null)
        {
            if (!condition)
            {
                FailRange(parameterName, message);
            }
        }

        [DebuggerStepThrough]
        public static void That(bool condition, string parameterName, string unformattedMessage, params object[] args)
        {
            if (!condition)
            {
                throw new ArgumentException(string.Format(unformattedMessage, args), parameterName);
            }
        }

        [DebuggerStepThrough]
        public static void ValidState(bool condition, string message)
        {
            if (!condition)
            {
                throw new InvalidOperationException(message);
            }
        }
    }
}

