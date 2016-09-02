namespace WTF.Framework
{
    using System;

    public class TerminalException : ApplicationException
    {
        public TerminalException(string message) : base(message)
        {
        }
    }
}

