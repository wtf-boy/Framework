namespace WTF.Framework
{
    using System;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct ShortTime
    {
        public int Hour;
        public int Minute;
        public int Second;
        public static ShortTime TimeParse(string timeInt)
        {
            string s = timeInt;
            if (timeInt.Length < 6)
            {
                for (int i = 0; i < (6 - timeInt.Length); i++)
                {
                    s = "0" + s;
                }
            }
            ShortTime time = new ShortTime();
            int startIndex = ((s.Length - 2) >= 0) ? (s.Length - 2) : 0;
            int length = ((s.Length - 2) >= 0) ? 2 : s.Length;
            time.Second = int.Parse(s.Substring(startIndex, length));
            s = s.Remove(startIndex, length);
            startIndex = ((s.Length - 2) >= 0) ? (s.Length - 2) : 0;
            length = ((s.Length - 2) >= 0) ? 2 : s.Length;
            time.Minute = int.Parse(s.Substring(startIndex, length));
            s = s.Remove(startIndex, length);
            time.Hour = int.Parse(s);
            return time;
        }

        public ShortTime(int paramHour, int paramMinute, int paramSecond)
        {
            this.Hour = paramHour;
            this.Minute = paramMinute;
            this.Second = paramSecond;
        }

        public bool IsNull()
        {
            return (((this.Hour == 0) && (this.Minute == 0)) && (this.Second == 0));
        }

        public int TimeToInt()
        {
            return int.Parse(this.Hour.ToString() + this.Minute.ToString() + this.Second.ToString());
        }
    }
}

