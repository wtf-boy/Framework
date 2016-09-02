namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class CRCHelper
    {
        public static ICRC GetCRC(this CRCType cRCType)
        {
            if (cRCType == CRCType.CRC8)
            {
                return new CRC8();
            }
            if (cRCType == CRCType.CRC16)
            {
                return new CRC16();
            }
            return new CRC32();
        }

        public static long GetCRC(this string value, CRCType CRCType)
        {
            return CRCType.GetCRC().GetCrc(value);
        }

        public static long GetCRC(this string value, CRCType CRCType, Encoding encoding)
        {
            return CRCType.GetCRC().GetCrc(value, encoding);
        }
    }
}

