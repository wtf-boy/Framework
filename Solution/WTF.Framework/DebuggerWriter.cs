namespace WTF.Framework
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Text;

    public class DebuggerWriter : TextWriter
    {
        private readonly string category;
        private static UnicodeEncoding encoding;
        private bool isOpen;
        private readonly int level;

        public DebuggerWriter() : this(0, Debugger.DefaultCategory)
        {
        }

        public DebuggerWriter(int level, string category) : this(level, category, CultureInfo.CurrentCulture)
        {
        }

        public DebuggerWriter(int level, string category, IFormatProvider formatProvider) : base(formatProvider)
        {
            this.level = level;
            this.category = category;
            this.isOpen = true;
        }

        protected override void Dispose(bool disposing)
        {
            this.isOpen = false;
            base.Dispose(disposing);
        }

        public override void Write(char value)
        {
            if (!this.isOpen)
            {
                throw new ObjectDisposedException(null);
            }
            Debugger.Log(this.level, this.category, value.ToString());
        }

        public override void Write(string value)
        {
            if (!this.isOpen)
            {
                throw new ObjectDisposedException(null);
            }
            if (value != null)
            {
                Debugger.Log(this.level, this.category, value);
            }
        }

        public override void Write(char[] buffer, int index, int count)
        {
            if (!this.isOpen)
            {
                throw new ObjectDisposedException(null);
            }
            if ((((buffer == null) || (index < 0)) || (count < 0)) || ((buffer.Length - index) < count))
            {
                base.Write(buffer, index, count);
            }
            Debugger.Log(this.level, this.category, new string(buffer, index, count));
        }

        public string Category
        {
            get
            {
                return this.category;
            }
        }

        public override System.Text.Encoding Encoding
        {
            get
            {
                if (encoding == null)
                {
                    encoding = new UnicodeEncoding(false, false);
                }
                return encoding;
            }
        }

        public int Level
        {
            get
            {
                return this.level;
            }
        }
    }
}

