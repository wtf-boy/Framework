namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    public static class RegexHelper
    {
        public static bool IsChineseString(this string str)
        {
            return str.IsMatch(@"^[\u4e00-\u9fa5]*$");
        }

        public static bool IsDateTime(this string source)
        {
            return Regex.IsMatch(source, @"^(((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$ ");
        }

        public static bool IsDoubleChar(this string source)
        {
            return Regex.IsMatch(source, @"[^\x00-\xff]");
        }

        public static bool IsEmail(this string email)
        {
            string pattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            return email.IsMatch(pattern);
        }

        public static bool IsIP(this string ip)
        {
            return ip.IsMatch(@"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        public static bool IsLetterOrNumber(this string str)
        {
            return Regex.IsMatch(str, "[a-zA-Z0-9_]");
        }

        public static bool IsMatch(this string input, string pattern)
        {
            return input.IsMatch(pattern, RegexOptions.IgnoreCase);
        }

        public static bool IsMatch(this string input, string pattern, RegexOptions objRegexOptions)
        {
            Regex regex = new Regex(pattern, objRegexOptions);
            return regex.IsMatch(input);
        }

        public static bool IsMobilePhone(this string input)
        {
            return input.IsMatch(@"^1[34578]\d{9}$");
        }

        public static bool IsNotNagtive(this string input)
        {
            Regex regex = new Regex(@"^\d+$");
            return regex.IsMatch(input);
        }

        public static bool IsNumber(this string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                return false;
            }
            Regex regex = new Regex(@"^\d*$");
            return regex.IsMatch(number);
        }

        public static bool IsNumber(this string number, int length)
        {
            return number.IsNumber(length, false);
        }

        public static bool IsNumber(this string number, int length, bool mustEqual)
        {
            Regex regex;
            if (string.IsNullOrEmpty(number))
            {
                return false;
            }
            if (mustEqual)
            {
                regex = new Regex(@"^\d{" + length + "}$");
            }
            else
            {
                regex = new Regex(@"^\d{0," + length + "}$");
            }
            return regex.IsMatch(number);
        }

        public static bool IsPhone(this string input)
        {
            string pattern = @"^\(0\d{2}\)[- ]?\d{8}$|^0\d{2}[- ]?\d{8}$|^\(0\d{3}\)[- ]?\d{7}$|^0\d{3}[- ]?\d{7}$";
            return input.IsMatch(pattern);
        }

        public static bool IsPhysicalPath(this string Path)
        {
            if (string.IsNullOrEmpty(Path))
            {
                return false;
            }
            string pattern = @"\b[a-z]:\\.*";
            return (Regex.Matches(Path, pattern, RegexOptions.IgnoreCase).Count > 0);
        }

        public static bool IsRealNoComma(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            return str.IsMatch(@"^\d+(\.\d+)?$");
        }

        public static bool IsRealWithComma(this string str)
        {
            return (str.IsRealNoComma() || str.IsMatch(@"^(\+|-)?\d{1,3}(,\d{3})*(\.\d+)?$"));
        }

        public static bool IsSpecialChar(this string source)
        {
            Regex regex = new Regex(@"[/\<>:.?*|$]");
            return regex.IsMatch(source);
        }

        public static bool IsString(this string str)
        {
            string pattern = @"[^\w\.@-]";
            return !Regex.IsMatch(str, pattern);
        }

        public static bool IsUint(this string input)
        {
            Regex regex = new Regex("^[0-9]*[1-9][0-9]*$");
            return regex.IsMatch(input);
        }

        public static bool IsURL(this string input)
        {
            string pattern = @"^[a-zA-Z]+://(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?$";
            return input.IsMatch(pattern);
        }

        public static MatchCollection Matches(this string input, string pattern, RegexOptions objRegexOptions)
        {
            return Regex.Matches(input, pattern, objRegexOptions);
        }

        public static GroupCollection MatchGroups(this string input, string pattern, RegexOptions objRegexOptions)
        {
            Regex regex = new Regex(pattern, objRegexOptions);
            return regex.Match(input).Groups;
        }

        public static string MatchValue(this string input, string pattern)
        {
            return input.MatchValue(pattern, RegexOptions.IgnoreCase);
        }

        public static string MatchValue(this string input, string pattern, string groupName)
        {
            Match match = new Regex(pattern, RegexOptions.IgnoreCase).Match(input);
            if (match.Success)
            {
                return match.Groups[groupName].Value;
            }
            return "";
        }

        public static string MatchValue(this string input, string pattern, RegexOptions ignoreCase)
        {
            Match match = new Regex(pattern, ignoreCase).Match(input);
            if (match.Success)
            {
                return match.Value;
            }
            return "";
        }

        public static string Replace(this string input, string pattern, string replacement, RegexOptions objRegexOptions)
        {
            return Regex.Replace(input, pattern, replacement, objRegexOptions);
        }

        public static int ValidatePassword(this string pwd)
        {
            if (string.IsNullOrEmpty(pwd) || (pwd.Length < 6))
            {
                return 0;
            }
            string str = "~!@#%&_`=;':,/<>\\-\\]\\}\\.\\$\\^\\{\\[\\(\\|\\)\\*\\+\\?\\\\\"";
            string[] strArray = new string[] { "^[a-z]+$", "^[A-Z]+$", "^[0-9]+$" };
            foreach (string str2 in strArray)
            {
                if (Regex.IsMatch(pwd, str2))
                {
                    return 1;
                }
            }
            string[] strArray2 = new string[] { "^[a-zA-Z]+$", "^[a-z0-9]+$", "^[A-Z0-9]+$", "^[a-z" + str + "]+$", "^[0-9" + str + "]+$", "^[A-Z" + str + "]+$" };
            foreach (string str2 in strArray2)
            {
                if (Regex.IsMatch(pwd, str2))
                {
                    return 2;
                }
            }
            string[] strArray3 = new string[] { "^[a-zA-Z0-9]+$", "^[a-zA-Z" + str + "]+$", "^[a-z0-9" + str + "]+$", "^[0-9A-Z" + str + "]+$" };
            foreach (string str2 in strArray3)
            {
                if (Regex.IsMatch(pwd, str2))
                {
                    return 3;
                }
            }
            return 4;
        }
    }
}

