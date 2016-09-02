namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;

    public static class RandomHelper
    {
        private static string _LowerChar = "abcdefghijklmnopqrstuvwxyz";
        private static string _NumberChar = "0123456789";
        private static string _UpperChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static int GetNewSeed()
        {
            byte[] data = new byte[4];
            new RNGCryptoServiceProvider().GetBytes(data);
            return BitConverter.ToInt32(data, 0);
        }

        public static string GetRandomLetterString(int resultLength)
        {
            return GetRandomString(_LowerChar + _UpperChar, resultLength);
        }

        public static string GetRandomMixString(int resultLength)
        {
            return GetRandomString(_LowerChar + _UpperChar + _NumberChar, resultLength);
        }

        public static int GetRandomNumber(int startNumber, int endNumber)
        {
            Random random = new Random(GetNewSeed());
            return random.Next(startNumber, endNumber);
        }

        public static string GetRandomNumberString(int resultLength)
        {
            return GetRandomString(_NumberChar, resultLength);
        }

        private static string GetRandomString(string source, int resultLength)
        {
            Random random = new Random(GetNewSeed());
            string str = null;
            for (int i = 0; i < resultLength; i++)
            {
                str = str + source.Substring(random.Next(0, source.Length - 1), 1);
            }
            return str;
        }

        public static string GetRandomStringByASCII(int resultLength, int startNumber, int endNumber)
        {
            Random random = new Random(GetNewSeed());
            string str = null;
            for (int i = 0; i < resultLength; i++)
            {
                str = str + ((char) random.Next(startNumber, endNumber));
            }
            return str;
        }

        public static string GetRandomSwitchString(this string RandomString, params char[] trimChars)
        {
            if (string.IsNullOrWhiteSpace(RandomString))
            {
                return RandomString;
            }
            RandomString = RandomString.Trim(trimChars);
            string[] strArray = RandomString.Split(trimChars, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length == 1)
            {
                return RandomString;
            }
            int randomNumber = GetRandomNumber(0, strArray.Length);
            return strArray[randomNumber];
        }

        public static List<int> GetRanIntList(int len, int minValue, int maxValue)
        {
            int num;
            Random random = new Random();
            List<int> list = new List<int>();
            if (len <= (maxValue - minValue))
            {
                while (list.Count < len)
                {
                    num = random.Next(minValue, maxValue);
                    int num2 = 0;
                    foreach (int num3 in list)
                    {
                        if (num == num3)
                        {
                            num2++;
                        }
                    }
                    if (num2 == 0)
                    {
                        list.Add(num);
                    }
                }
                list.Sort();
                return list;
            }
            for (num = minValue; num < maxValue; num++)
            {
                list.Add(num);
            }
            return list;
        }
    }
}

