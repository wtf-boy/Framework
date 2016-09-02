namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class XxteaHelper
    {
        public static string DecryptXxtea(this string str, string pass)
        {
            byte[] bytes = DecryptXxtea(Convert.FromBase64String(str), Encoding.UTF8.GetBytes(pass));
            return Encoding.UTF8.GetString(bytes);
        }

        public static byte[] DecryptXxtea(byte[] Data, byte[] Key)
        {
            if (Data.Length == 0)
            {
                return Data;
            }
            return ToByteArray(DecryptXxtea(ToUInt32Array(Data, false), ToUInt32Array(Key, false)), true);
        }

        public static uint[] DecryptXxtea(uint[] v, uint[] k)
        {
            int index = v.Length - 1;
            if (index >= 1)
            {
                if (k.Length < 4)
                {
                    uint[] array = new uint[4];
                    k.CopyTo(array, 0);
                    k = array;
                }
                uint num2 = v[index];
                uint num3 = v[0];
                uint num4 = 0x9e3779b9;
                int num8 = 6 + (0x34 / (index + 1));
                for (uint i = (uint) (num8 * num4); i != 0; i -= num4)
                {
                    uint num6 = (i >> 2) & 3;
                    int num7 = index;
                    while (num7 > 0)
                    {
                        num2 = v[num7 - 1];
                        num3 = v[num7] -= (((num2 >> 5) ^ (num3 << 2)) + ((num3 >> 3) ^ (num2 << 4))) ^ ((i ^ num3) + (k[(int) ((IntPtr) ((num7 & 3) ^ num6))] ^ num2));
                        num7--;
                    }
                    num2 = v[index];
                    num3 = v[0] -= (((num2 >> 5) ^ (num3 << 2)) + ((num3 >> 3) ^ (num2 << 4))) ^ ((i ^ num3) + (k[(int) ((IntPtr) ((num7 & 3) ^ num6))] ^ num2));
                }
            }
            return v;
        }

        public static string EncryptXxtea(this string str, string pass)
        {
            return Convert.ToBase64String(EncryptXxtea(Encoding.UTF8.GetBytes(str), Encoding.UTF8.GetBytes(pass)));
        }

        public static byte[] EncryptXxtea(byte[] Data, byte[] Key)
        {
            if (Data.Length == 0)
            {
                return Data;
            }
            return ToByteArray(EncryptXxtea(ToUInt32Array(Data, true), ToUInt32Array(Key, false)), false);
        }

        public static uint[] EncryptXxtea(uint[] v, uint[] k)
        {
            int index = v.Length - 1;
            if (index >= 1)
            {
                if (k.Length < 4)
                {
                    uint[] array = new uint[4];
                    k.CopyTo(array, 0);
                    k = array;
                }
                uint num2 = v[index];
                uint num3 = v[0];
                uint num4 = 0x9e3779b9;
                uint num5 = 0;
                int num8 = 6 + (0x34 / (index + 1));
                while (num8-- > 0)
                {
                    num5 += num4;
                    uint num6 = (num5 >> 2) & 3;
                    int num7 = 0;
                    while (num7 < index)
                    {
                        num3 = v[num7 + 1];
                        num2 = v[num7] += (((num2 >> 5) ^ (num3 << 2)) + ((num3 >> 3) ^ (num2 << 4))) ^ ((num5 ^ num3) + (k[(int) ((IntPtr) ((num7 & 3) ^ num6))] ^ num2));
                        num7++;
                    }
                    num3 = v[0];
                    num2 = v[index] += (((num2 >> 5) ^ (num3 << 2)) + ((num3 >> 3) ^ (num2 << 4))) ^ ((num5 ^ num3) + (k[(int) ((IntPtr) ((num7 & 3) ^ num6))] ^ num2));
                }
            }
            return v;
        }

        private static byte[] ToByteArray(uint[] Data, bool IncludeLength)
        {
            int num;
            if (IncludeLength)
            {
                num = (int) Data[Data.Length - 1];
            }
            else
            {
                num = Data.Length << 2;
            }
            byte[] buffer = new byte[num];
            for (int i = 0; i < num; i++)
            {
                buffer[i] = (byte) (Data[i >> 2] >> ((i & 3) << 3));
            }
            return buffer;
        }

        private static uint[] ToUInt32Array(byte[] Data, bool IncludeLength)
        {
            uint[] numArray;
            int index = ((Data.Length & 3) == 0) ? (Data.Length >> 2) : ((Data.Length >> 2) + 1);
            if (IncludeLength)
            {
                numArray = new uint[index + 1];
                numArray[index] = (uint) Data.Length;
            }
            else
            {
                numArray = new uint[index];
            }
            index = Data.Length;
            for (int i = 0; i < index; i++)
            {
                numArray[i >> 2] |= (uint) (Data[i] << ((i & 3) << 3));
            }
            return numArray;
        }
    }
}

