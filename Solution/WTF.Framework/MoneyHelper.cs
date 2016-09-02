namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;

    public static class MoneyHelper
    {
        private static string GetFloat(string n)
        {
            if (n.Replace("0", "").Replace("1", "").Replace("2", "").Replace("3", "").Replace("4", "").Replace("5", "").Replace("6", "").Replace("7", "").Replace("8", "").Replace("9", "") == "")
            {
                n = n + ".0";
            }
            return n.Split(new char[] { '.' })[1];
        }

        private static string GetInteger(this string source)
        {
            return source.Split(new char[] { '.' })[0];
        }

        public static string GetMoney(this string price)
        {
            if (price.Length == 1)
            {
                price = price + ".00";
                return price;
            }
            price = price.Substring(0, price.IndexOf('.') + 3);
            return price;
        }

        private static string GetNum3(int n)
        {
            string str = n.ToString();
            string[] strArray = new string[3];
            string str2 = "";
            if ((n >= 100) & (n < 0x3e8))
            {
                strArray[0] = str.Substring(0, 1).Replace("0", "").Replace("1", "one hundred ").Replace("2", "two hundred ").Replace("3", "three hundred ").Replace("4", "four hundred ").Replace("5", "five hundred ").Replace("6", "six hundred ").Replace("7", "seven hundred ").Replace("8", "eight hundred ").Replace("9", "nine hundred ");
                if (str.Substring(1, 1) == "1")
                {
                    str2 = str.Substring(1, 2).Replace("10", "and ten ").Replace("11", "and eleven ").Replace("12", "and twelve ").Replace("13", "and thirteen ").Replace("14", "and fourteen ").Replace("15", "and fifteen ").Replace("16", "and sixteen ").Replace("17", "and seventeen ").Replace("18", "and eighteen ").Replace("19", "and nineteen ");
                }
                else if (str.Substring(1, 1) == "0")
                {
                    str2 = str.Substring(1, 2).Replace("00", "").Replace("01", "and one ").Replace("02", "and two ").Replace("03", "and three ").Replace("04", "and four ").Replace("05", "and five ").Replace("06", "and six ").Replace("07", "and seven ").Replace("08", "and eight ").Replace("09", "and nine ");
                }
                else
                {
                    strArray[1] = str.Substring(1, 1).Replace("2", "and twenty ").Replace("3", "and thirty ").Replace("4", "and forty ").Replace("5", "and fifty ").Replace("6", "and sixty ").Replace("7", "and seventy ").Replace("8", "and eighty ").Replace("9", "and ninty ");
                    strArray[2] = str.Substring(2, 1).Replace("0", "").Replace("1", "-one ").Replace("2", "-two ").Replace("3", "-three ").Replace("4", "-four ").Replace("5", "-five ").Replace("6", "-six ").Replace("7", "-seven ").Replace("8", "-eight ").Replace("9", "-nine ");
                    str2 = strArray[1] + strArray[2];
                }
            }
            else if ((n < 100) & (n >= 10))
            {
                strArray[0] = "";
                if (str.Substring(0, 1) == "1")
                {
                    str2 = str.Substring(0, 2).Replace("10", "ten ").Replace("11", "eleven ").Replace("12", "twelve ").Replace("13", "thirteen ").Replace("14", "fourteen ").Replace("15", "fifteen ").Replace("16", "sixteen ").Replace("17", "seventeen ").Replace("18", "eighteen ").Replace("19", "nineteen ");
                }
                else
                {
                    strArray[1] = str.Substring(0, 1).Replace("2", "twenty ").Replace("3", "thirty ").Replace("4", "forty ").Replace("5", "fifty ").Replace("6", "sixty ").Replace("7", "seventy ").Replace("8", "eighty ").Replace("9", "ninty ");
                    strArray[2] = str.Substring(1, 1).Replace("0", "").Replace("1", "-one ").Replace("2", "-two ").Replace("3", "-three ").Replace("4", "-four ").Replace("5", "-five ").Replace("6", "-six ").Replace("7", "-seven ").Replace("8", "-eight ").Replace("9", "-nine ");
                    str2 = strArray[1] + strArray[2];
                }
            }
            else if ((n >= 0) & (n < 10))
            {
                strArray[0] = "";
                strArray[1] = "";
                strArray[2] = str.Substring(0, 1).Replace("0", "").Replace("1", " one ").Replace("2", " two ").Replace("3", " three ").Replace("4", " four ").Replace("5", " five ").Replace("6", " six ").Replace("7", " seven ").Replace("8", " eight ").Replace("9", " nine ");
                str2 = strArray[1] + strArray[2];
            }
            return (strArray[0] + str2);
        }

        public static string MoneyToChinese(this double inputString)
        {
            string str = "零壹贰叁肆伍陆柒捌玖";
            string str2 = "分角元拾佰仟万拾佰仟亿拾佰仟万";
            double num = inputString;
            string str3 = null;
            if (num > 9999999999999.99)
            {
                return "超出范围的人民币值";
            }
            string str4 = Convert.ToInt64((double) (num * 100.0)).ToString();
            int length = str4.Length;
            for (int i = 0; i < length; i++)
            {
                int startIndex = int.Parse(str4.Substring(i, 1));
                string str5 = str.Substring(startIndex, 1);
                string str6 = str2.Substring((length - i) - 1, 1);
                if (str5 != "零")
                {
                    str3 = str3 + str5 + str6;
                }
                else
                {
                    switch (str6)
                    {
                        case "亿":
                        case "万":
                        case "元":
                        case "零":
                            while (str3.EndsWith("零"))
                            {
                                str3 = str3.Substring(0, str3.Length - 1);
                            }
                            break;
                    }
                    if (((str6 == "亿") || ((str6 == "万") && !str3.EndsWith("亿"))) || (str6 == "元"))
                    {
                        str3 = str3 + str6;
                    }
                    else
                    {
                        bool flag = str3.EndsWith("亿");
                        bool flag2 = str3.EndsWith("零");
                        if (str3.Length > 1)
                        {
                            bool flag3 = str3.Substring(str3.Length - 2, 2).StartsWith("零");
                            if (!(flag2 || (!flag3 && flag)))
                            {
                                str3 = str3 + str5;
                            }
                        }
                        else if (!(flag2 || flag))
                        {
                            str3 = str3 + str5;
                        }
                    }
                }
            }
            while (str3.EndsWith("零"))
            {
                str3 = str3.Substring(0, str3.Length - 1);
            }
            while (str3.EndsWith("元"))
            {
                str3 = str3 + "整";
            }
            return str3;
        }

        public static string MoneyToChinese(this float money)
        {
            string str = "";
            string str4 = "";
            string str2 = decimal.Round((decimal) money, 2).ToString("0.00");
            int length = str2.Length;
            for (int i = 1; i < (length + 1); i++)
            {
                string str3 = str2.Substring(length - i, i).Substring(0, 1);
                if (str3 != ".")
                {
                    switch (str3)
                    {
                        case "0":
                            str3 = "零";
                            break;

                        case "1":
                            str3 = "壹";
                            break;

                        case "2":
                            str3 = "贰";
                            break;

                        case "3":
                            str3 = "叁";
                            break;

                        case "4":
                            str3 = "肆";
                            break;

                        case "5":
                            str3 = "伍";
                            break;

                        case "6":
                            str3 = "陆";
                            break;

                        case "7":
                            str3 = "柒";
                            break;

                        case "8":
                            str3 = "捌";
                            break;

                        case "9":
                            str3 = "玖";
                            break;
                    }
                    switch (i)
                    {
                        case 1:
                            str4 = "分整";
                            break;

                        case 2:
                            str4 = "角";
                            break;

                        case 3:
                            str4 = "";
                            break;

                        case 4:
                            str4 = "元";
                            break;

                        case 5:
                            str4 = "拾";
                            break;

                        case 6:
                            str4 = "佰";
                            break;

                        case 7:
                            str4 = "仟";
                            break;

                        case 8:
                            str4 = "万";
                            break;

                        case 9:
                            str4 = "拾";
                            break;

                        case 10:
                            str4 = "佰";
                            break;

                        case 11:
                            str4 = "仟";
                            break;

                        case 12:
                            str4 = "亿";
                            break;

                        case 13:
                            str4 = "拾";
                            break;

                        case 14:
                            str4 = "佰";
                            break;

                        case 15:
                            str4 = "仟";
                            break;
                    }
                    str = str3 + str4 + str;
                }
            }
            return str;
        }

        public static string MoneyToEnglish(this string source)
        {
            int num3;
            string @float = GetFloat(source);
            string str2 = " point" + @float.Replace("0", " zero").Replace("1", " one").Replace("2", " two").Replace("3", " three").Replace("4", " four").Replace("5", " five").Replace("6", " six").Replace("7", " seven").Replace("8", " eight").Replace("9", " nine");
            string[] strArray = new string[6];
            int index = 0;
            double num2 = double.Parse(source);
            if (source.GetInteger() == "0")
            {
                strArray[0] = "zero";
                goto Label_03AE;
            }
        Label_00E4:
            num3 = int.Parse(num2.ToString().GetInteger()) % 0x3e8;
            strArray[index] = GetNum3(num3);
            num2 /= 1000.0;
            if (!((int.Parse(num2.ToString().GetInteger()) < 0x3e8) & (int.Parse(num2.ToString().GetInteger()) > 0)))
            {
                if (int.Parse(num2.ToString().GetInteger()) >= 0x3e8)
                {
                    switch (index)
                    {
                        case 0:
                            strArray[index] = " thousand  " + strArray[index];
                            goto Label_038F;

                        case 1:
                            strArray[index] = " million  " + strArray[index];
                            goto Label_038F;

                        case 2:
                            strArray[index] = " billion  " + strArray[index];
                            goto Label_038F;

                        case 3:
                            strArray[index] = " trillion  " + strArray[index];
                            goto Label_038F;
                    }
                    strArray[index] = " the number is too large!!!";
                }
            }
            else
            {
                switch (index)
                {
                    case 0:
                        if (num3 == 0)
                        {
                            strArray[index] = GetNum3(int.Parse(num2.ToString().GetInteger())) + " thousand " + strArray[index];
                            break;
                        }
                        strArray[index] = GetNum3(int.Parse(num2.ToString().GetInteger())) + " thousand and " + strArray[index];
                        break;

                    case 1:
                        if (num3 == 0)
                        {
                            strArray[index] = GetNum3(int.Parse(num2.ToString().GetInteger())) + " million " + strArray[index];
                            break;
                        }
                        strArray[index] = GetNum3(int.Parse(num2.ToString().GetInteger())) + " million and " + strArray[index];
                        break;

                    case 2:
                        if (num3 == 0)
                        {
                            strArray[index] = GetNum3(int.Parse(num2.ToString().GetInteger())) + " billion " + strArray[index];
                            break;
                        }
                        strArray[index] = GetNum3(int.Parse(num2.ToString().GetInteger())) + " billion and " + strArray[index];
                        break;

                    case 3:
                        if (num3 == 0)
                        {
                            strArray[index] = GetNum3(int.Parse(num2.ToString().GetInteger())) + " trillion " + strArray[index];
                            break;
                        }
                        strArray[index] = GetNum3(int.Parse(num2.ToString().GetInteger())) + " trillion and " + strArray[index];
                        break;

                    default:
                        strArray[index] = " the number is too large!!!";
                        break;
                }
            }
        Label_038F:
            index++;
            if (num2 >= 1000.0)
            {
                goto Label_00E4;
            }
        Label_03AE:;
            return (strArray[5] + strArray[4] + strArray[3] + strArray[2] + strArray[1] + strArray[0] + str2);
        }
    }
}

