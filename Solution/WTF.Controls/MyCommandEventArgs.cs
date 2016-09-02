namespace WTF.Controls
{
    using System;

    public class MyCommandEventArgs : EventArgs
    {
        private string _commandArgument;
        private string _commandName;

        public MyCommandEventArgs()
        {
        }

        public MyCommandEventArgs(string commandName, string commandArgument)
        {
            this.CommandName = commandName;
            this.CommandArgument = commandArgument;
        }

        public string CommandArgument
        {
            get
            {
                return this._commandArgument;
            }
            set
            {
                this._commandArgument = value;
            }
        }

        public string CommandName
        {
            get
            {
                return this._commandName;
            }
            set
            {
                this._commandName = value;
            }
        }
    }
}

