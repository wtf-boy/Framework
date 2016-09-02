namespace WTF.Framework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class WordsFilterHelper
    {
        private BitArray charCheck;
        private BitArray endCheck;
        private byte[] fastCheck;
        private byte[] fastLength;
        private HashSet<string> hash;
        private int maxWordLength;
        private int minWordLength;

        public WordsFilterHelper()
        {
            this.hash = new HashSet<string>();
            this.fastCheck = new byte[0xffff];
            this.fastLength = new byte[0xffff];
            this.charCheck = new BitArray(0xffff);
            this.endCheck = new BitArray(0xffff);
            this.maxWordLength = 0;
            this.minWordLength = 0x7fffffff;
        }

        public WordsFilterHelper(string badwords)
        {
            this.hash = new HashSet<string>();
            this.fastCheck = new byte[0xffff];
            this.fastLength = new byte[0xffff];
            this.charCheck = new BitArray(0xffff);
            this.endCheck = new BitArray(0xffff);
            this.maxWordLength = 0;
            this.minWordLength = 0x7fffffff;
            this.Init(badwords);
        }

        public WordsFilterHelper(string[] badwords)
        {
            this.hash = new HashSet<string>();
            this.fastCheck = new byte[0xffff];
            this.fastLength = new byte[0xffff];
            this.charCheck = new BitArray(0xffff);
            this.endCheck = new BitArray(0xffff);
            this.maxWordLength = 0;
            this.minWordLength = 0x7fffffff;
            this.Init(badwords);
        }

        public bool ContainsKeyword(string text)
        {
            int num2;
            for (int i = 0; i < text.Length; i += num2)
            {
                num2 = 1;
                if ((i > 0) || ((this.fastCheck[text[i]] & 1) == 0))
                {
                    while ((i < (text.Length - 1)) && ((this.fastCheck[text[++i]] & 1) == 0))
                    {
                    }
                }
                char index = text[i];
                if ((this.minWordLength == 1) && this.charCheck[index])
                {
                    return true;
                }
                for (int j = 1; j <= Math.Min(this.maxWordLength, (text.Length - i) - 1); j++)
                {
                    char ch2 = text[i + j];
                    if ((this.fastCheck[ch2] & 1) == 0)
                    {
                        num2++;
                    }
                    if ((this.fastCheck[ch2] & (((int) 1) << Math.Min(j, 7))) == 0)
                    {
                        break;
                    }
                    if (((j + 1) >= this.minWordLength) && (((this.fastLength[index] & (((int) 1) << Math.Min(j - 1, 7))) > 0) && this.endCheck[ch2]))
                    {
                        string item = text.Substring(i, j + 1);
                        if (this.hash.Contains(item))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void Init(string badwords)
        {
            this.Init(badwords.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public void Init(string[] badwords)
        {
            foreach (string str in badwords)
            {
                this.maxWordLength = Math.Max(this.maxWordLength, str.Length);
                this.minWordLength = Math.Min(this.minWordLength, str.Length);
                int num = 0;
                while ((num < 7) && (num < str.Length))
                {
                    this.fastCheck[str[num]] = (byte) (this.fastCheck[str[num]] | ((byte) (((int) 1) << num)));
                    num++;
                }
                for (num = 7; num < str.Length; num++)
                {
                    this.fastCheck[str[num]] = (byte) (this.fastCheck[str[num]] | 0x80);
                }
                if (str.Length == 1)
                {
                    this.charCheck[str[0]] = true;
                }
                else
                {
                    this.fastLength[str[0]] = (byte) (this.fastLength[str[0]] | ((byte) (((int) 1) << Math.Min(7, str.Length - 2))));
                    this.endCheck[str[str.Length - 1]] = true;
                    this.hash.Add(str);
                }
            }
        }
    }
}

