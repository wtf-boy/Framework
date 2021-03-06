﻿namespace WTF.Logging
{
    using System;

    public class InfoHintException : Exception
    {
        private string _Message;

        public InfoHintException(string message)
        {
            this._Message = message;
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

