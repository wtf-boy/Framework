using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTF.Framework
{
public static class HashHelper
{
    // Methods
    public static int[] GetHashOffsetList(string value, int size)
    {
        if ((size % 8) != 0)
        {
            throw new Exception("错误的长度,不能被2整除");
        }
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException("参数value不能为空");
        }
        int[] numArray = new int[8];
        string str = Hash(value).ToString();
        int num2 = Math.Abs((int) ((Hash1(str) % (size / 8)) - 1));
        numArray[0] = num2;
        int num = Hash2(str);
        num2 = ((size / 4) - Math.Abs((int) (num % (size / 8)))) - 1;
        numArray[1] = num2;
        num2 = Math.Abs((int) ((Hash3(str) % (size / 8)) - 1)) + (size / 4);
        numArray[2] = num2;
        num = Hash4(str);
        num2 = ((size / 2) - Math.Abs((int) (num % (size / 8)))) - 1;
        numArray[3] = num2;
        num2 = Math.Abs((int) ((Hash5(str) % (size / 8)) - 1)) + (size / 2);
        numArray[4] = num2;
        num = Hash6(str);
        num2 = (((3 * size) / 4) - Math.Abs((int) (num % (size / 8)))) - 1;
        numArray[5] = num2;
        num2 = Math.Abs((int) ((Hash7(str) % (size / 8)) - 1)) + ((3 * size) / 4);
        numArray[6] = num2;
        num = Hash8(str);
        num2 = (size - Math.Abs((int) (num % (size / 8)))) - 1;
        numArray[7] = num2;
        return numArray;
    }

    public static int Hash(string val)
    {
        return val.GetHashCode();
    }

    public static int Hash1(string str)
    {
        int num = 0x83;
        int num2 = 0;
        char[] chArray = str.ToCharArray();
        for (int i = chArray.Length; i > 0; i--)
        {
            num2 = (num2 * num) + chArray[chArray.Length - i];
        }
        return (num2 & 0x7fffffff);
    }

    public static int Hash2(string str)
    {
        int num = 0;
        char[] chArray = str.ToCharArray();
        int length = chArray.Length;
        for (int i = 0; i < length; i++)
        {
            if ((i & 1) == 0)
            {
                num ^= ((num << 7) ^ chArray[i]) ^ (num >> 3);
            }
            else
            {
                num ^= ~(((num << 11) ^ chArray[i]) ^ (num >> 5));
            }
            length--;
        }
        return (num & 0x7fffffff);
    }

    public static int Hash3(string str)
    {
        int num = 0;
        char[] chArray = str.ToCharArray();
        for (int i = chArray.Length; i > 0; i--)
        {
            num = ((chArray[chArray.Length - i] + (num << 6)) + (num << 0x10)) - num;
        }
        return (num & 0x7fffffff);
    }

    public static int Hash4(string str)
    {
        int num = 0x5c6b7;
        int num2 = 0xf8c9;
        int num3 = 0;
        char[] chArray = str.ToCharArray();
        for (int i = chArray.Length; i > 0; i--)
        {
            num3 = (num3 * num2) + chArray[chArray.Length - i];
            num2 *= num;
        }
        return (num3 & 0x7fffffff);
    }

    public static int Hash5(string str)
    {
        int num = 0x4e67c6a7;
        char[] chArray = str.ToCharArray();
        for (int i = chArray.Length; i > 0; i--)
        {
            num ^= ((num << 5) + chArray[chArray.Length - i]) + (num >> 2);
        }
        return (num & 0x7fffffff);
    }

    public static int Hash6(string str)
    {
        int num = 0x20;
        int num2 = (num * 3) / 4;
        int num3 = num / 8;
        int num4 = 0;
        int num5 = ((int) (-1)) << (num - num3);
        int num6 = 0;
        char[] chArray = str.ToCharArray();
        for (int i = chArray.Length; i > 0; i--)
        {
            num4 = (num4 << num3) + chArray[chArray.Length - i];
            num6 = num4 & num5;
            if (num6 != 0)
            {
                num4 = (num4 ^ (num6 >> num2)) & ~num5;
            }
        }
        return (num4 & 0x7fffffff);
    }

    public static int Hash7(string str)
    {
        int num = 0;
        int num2 = 0;
        char[] chArray = str.ToCharArray();
        for (int i = chArray.Length; i > 0; i--)
        {
            num = (num << 4) + chArray[chArray.Length - i];
            num2 = num & -268435456;
            if (num2 != 0)
            {
                num ^= num2 >> 0x18;
                num &= ~num2;
            }
        }
        return (num & 0x7fffffff);
    }

    public static int Hash8(string str)
    {
        int num = 0x1505;
        char[] chArray = str.ToCharArray();
        for (int i = chArray.Length; i > 0; i--)
        {
            num += (num << 5) + chArray[chArray.Length - i];
        }
        return (num & 0x7fffffff);
    }
}


 

}
