namespace WTF.Framework
{
    using System;
    using System.Text;

    public abstract class ICRC
    {
        protected ICRC()
        {
        }

        public abstract long GetCrc(int bval);
        public long GetCrc(string value)
        {
            return this.GetCrc(value, Encoding.UTF8);
        }

        public long GetCrc(byte[] buffer)
        {
            return this.GetCrc(buffer, 0, buffer.Length);
        }

        public long GetCrc(string value, Encoding encoding)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value不能为空值");
            }
            return this.GetCrc(encoding.GetBytes(value));
        }

        public abstract long GetCrc(byte[] buffer, int offset, int count);
    }
}

