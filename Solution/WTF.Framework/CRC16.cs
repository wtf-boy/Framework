namespace WTF.Framework
{
    using System;

    public class CRC16 : ICRC
    {
        private ushort crc = 0;
        private readonly ushort CrcSeed = 0xffff;
        public ushort[] lowercrctab = new ushort[] { 0, 0x1021, 0x2042, 0x3063, 0x4084, 0x50a5, 0x60c6, 0x70e7, 0x8108, 0x9129, 0xa14a, 0xb16b, 0xc18c, 0xd1ad, 0xe1ce, 0xf1ef };
        public ushort[] uppercrctab = new ushort[] { 0, 0x1231, 0x2462, 0x3653, 0x48c4, 0x5af5, 0x6ca6, 0x7e97, 0x9188, 0x83b9, 0xb5ea, 0xa7db, 0xd94c, 0xcb7d, 0xfd2e, 0xef1f };

        public uint ComputeCrc(ushort oldCrc, byte bval)
        {
            this.crc = oldCrc;
            return this.Get_Crc(bval);
        }

        private ushort Get_Crc(int bval)
        {
            ushort num = (ushort) ((this.crc >> 12) & 15);
            ushort num2 = (ushort) ((this.crc >> 8) & 15);
            ushort num3 = (ushort) (((this.crc & 0xff) << 8) | bval);
            num3 = (ushort) (num3 ^ (this.uppercrctab[(num - 1) + 1] ^ this.lowercrctab[(num2 - 1) + 1]));
            this.crc = num3;
            return this.crc;
        }

        public override long GetCrc(int bval)
        {
            this.crc = this.CrcSeed;
            this.crc = this.Get_Crc(bval);
            this.crc = (ushort) (this.crc ^ this.CrcSeed);
            return (long) this.crc;
        }

        public override long GetCrc(byte[] buffer, int offset, int count)
        {
            if ((((buffer == null) || (buffer.Length < 1)) || ((offset < 0) || (offset >= buffer.Length))) || (count < 1))
            {
                return -1L;
            }
            this.crc = this.CrcSeed;
            for (int i = offset; i < count; i++)
            {
                this.Get_Crc(buffer[i]);
            }
            this.crc = (ushort) (this.crc ^ this.CrcSeed);
            return (long) this.crc;
        }
    }
}

