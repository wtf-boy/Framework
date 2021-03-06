﻿namespace WTF.Logging
{
    using System;
    using System.Runtime.InteropServices;

    public class ArgumentInputNullException : Exception
    {
        private string _LogModuleType = "Application";
        private string _Message;

        public ArgumentInputNullException(string message, string logModuleType = "Application")
        {
            if (!string.IsNullOrWhiteSpace(logModuleType))
            {
                this._LogModuleType = logModuleType;
            }
            this._Message = message;
        }

        public string LogModuleType
        {
            get
            {
                return this._LogModuleType;
            }
        }

        public override string Message
        {
            get
            {
                return this._Message;
            }
        }
    }
}

