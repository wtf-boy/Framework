namespace WTF.Framework
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    public class VirtualScreen : IDisposable
    {
        private bool _changedScreen;
        private int _cursorx0;
        private int _cursory0;
        private readonly int _offsetx;
        private readonly int _offsety;
        private string _screenString;
        private string _screenStringLower;
        private int _visibleAreaY0Bottom;
        private int _visibleAreaY0Top;
        private byte[,] _vs;
        public const byte Space = 0x20;

        public VirtualScreen(int width, int height) : this(width, height, 0, 0)
        {
        }

        public VirtualScreen(int width, int height, int xOffset, int yOffset)
        {
            this._offsetx = xOffset;
            this._offsety = yOffset;
            this._vs = new byte[width, height];
            this.CleanScreen();
            this._changedScreen = false;
            this._visibleAreaY0Top = 0;
            this._visibleAreaY0Bottom = height - 1;
            this.CursorReset();
        }

        public void CleanFromCursor()
        {
            int cursorY = this.CursorY;
            if (cursorY <= (this._visibleAreaY0Bottom + this._offsety))
            {
                this.CleanScreen(this.CursorXLeft, cursorY, this.CursorXRight, this._visibleAreaY0Bottom + this._offsety);
            }
            this.CleanLine(this.CursorX, this.CursorXRight);
            this._changedScreen = true;
        }

        public void CleanLine(int xStart, int xEnd)
        {
            int num = xStart - this._offsetx;
            int num2 = xEnd - this._offsetx;
            if (xStart >= xEnd)
            {
                if (num < 0)
                {
                    num = 0;
                }
                if (num2 >= this.Width)
                {
                    num2 = this.Width - 1;
                }
                int num3 = this._cursory0 - this._visibleAreaY0Top;
                if (((this._vs != null) && (num3 >= 0)) && (num3 <= this._vs.GetLength(1)))
                {
                    for (int i = num; i <= num2; i++)
                    {
                        this._vs[i, num3] = 0x20;
                    }
                    this._changedScreen = true;
                }
            }
        }

        public void CleanScreen()
        {
            int length = this._vs.GetLength(0);
            int num2 = this._vs.GetLength(1);
            for (int i = 0; i < num2; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    this._vs[j, i] = 0x20;
                }
            }
            this.CursorReset();
            this._changedScreen = true;
            this._visibleAreaY0Top = 0;
            this._visibleAreaY0Bottom = this.Height - 1;
        }

        public void CleanScreen(int xstart, int ystart, int xend, int yend)
        {
            if (((((this._vs != null) && (xend > xstart)) && ((yend > ystart) && (xstart >= this._offsetx))) && ((xend >= this._offsetx) && (ystart >= this._offsety))) && (yend >= this._offsety))
            {
                int num = xstart - this._offsetx;
                int num2 = (ystart - this._offsety) - this._visibleAreaY0Top;
                if (num2 < 0)
                {
                    num2 = 0;
                }
                int num3 = xend - this._offsetx;
                int num4 = (yend - this._offsety) - this._visibleAreaY0Top;
                if (num4 >= 0)
                {
                    int length = this._vs.GetLength(0);
                    int num6 = this._vs.GetLength(1);
                    if (num3 >= length)
                    {
                        num3 = length - 1;
                    }
                    if (num4 >= num6)
                    {
                        num4 = num6 - 1;
                    }
                    for (int i = num2; i <= num4; i++)
                    {
                        for (int j = num; j <= num3; j++)
                        {
                            this._vs[j, i] = 0x20;
                        }
                    }
                    this._changedScreen = true;
                }
            }
        }

        public void CleanToCursor()
        {
            int yend = this.CursorY - 1;
            if (yend >= this._offsety)
            {
                this.CleanScreen(this.CursorXLeft, this._offsety, this.CursorXRight, yend);
            }
            this.CleanLine(this.CursorXLeft, this.CursorX);
            this._changedScreen = true;
        }

        public void CursorNextLine()
        {
            this.Write("\n\r");
        }

        public void CursorPosition(int x, int y)
        {
            this.CursorX = x;
            this.CursorY = y;
        }

        public void CursorReset()
        {
            this.CursorY0 = 0;
            this.CursorX0 = 0;
        }

        public void Dispose()
        {
            this._vs = null;
            this._screenString = null;
            this._screenStringLower = null;
        }

        public string FindOnScreen(string findString, bool caseSensitive)
        {
            if (((this._vs == null) || (findString == null)) || (findString.Length < 1))
            {
                return null;
            }
            try
            {
                string str = caseSensitive ? this.Hardcopy() : this.Hardcopy(true);
                int startIndex = caseSensitive ? str.IndexOf(findString) : str.IndexOf(findString.ToLower());
                if (startIndex < 0)
                {
                    return null;
                }
                return (caseSensitive ? findString : this.Hardcopy().Substring(startIndex, findString.Length));
            }
            catch
            {
                return null;
            }
        }

        public string FindRegExOnScreen(string regExp)
        {
            return this.FindRegExOnScreen(regExp, false);
        }

        public Match FindRegExOnScreen(Regex regExp)
        {
            if ((this._vs == null) || (regExp == null))
            {
                return null;
            }
            Match match = regExp.Match(this.Hardcopy());
            return (match.Success ? match : null);
        }

        public string FindRegExOnScreen(string regExp, bool caseSensitive)
        {
            if (((this._vs == null) || (regExp == null)) || (regExp.Length < 1))
            {
                return null;
            }
            Match match = (caseSensitive ? new Regex(regExp) : new Regex(regExp, RegexOptions.IgnoreCase)).Match(this.Hardcopy());
            return (match.Success ? match.Value : null);
        }

        public string GetLine(int yPosition)
        {
            int num = yPosition - this._offsety;
            if (((this._vs == null) || (num >= this.Height)) || (this.Width < 1))
            {
                return null;
            }
            byte[] bytes = new byte[this.Width];
            for (int i = 0; i < this.Width; i++)
            {
                bytes[i] = this._vs[i, num];
            }
            return Encoding.ASCII.GetString(bytes, 0, bytes.Length);
        }

        public string Hardcopy()
        {
            return this.Hardcopy(false);
        }

        public string Hardcopy(bool lowercase)
        {
            if (this._vs == null)
            {
                return null;
            }
            if (this._changedScreen || (this._screenString == null))
            {
                int capacity = this.Width * this.Height;
                StringBuilder builder = new StringBuilder(capacity);
                for (int i = 0; i < this.Height; i++)
                {
                    if (i > 0)
                    {
                        builder.Append('\n');
                    }
                    builder.Append(this.GetLine(i + this._offsety));
                }
                this._screenString = builder.ToString();
                this._changedScreen = false;
                if (!lowercase)
                {
                    return this._screenString;
                }
                this._screenStringLower = this._screenString.ToLower();
                return this._screenStringLower;
            }
            if (lowercase)
            {
                return (this._screenStringLower ?? (this._screenStringLower = this._screenString.ToLower()));
            }
            return this._screenString;
        }

        public void MoveCursor(int positions)
        {
            if (positions != 0)
            {
                int num = positions / this.Width;
                int num2 = positions - (num * this.Width);
                if (num2 >= 0)
                {
                    if ((this.CursorX0 + num2) >= this.Width)
                    {
                        num++;
                        num2 -= this.Width;
                    }
                }
                else if ((this.CursorX0 + num2) < 0)
                {
                    num--;
                    num2 -= this.Width;
                }
                int num3 = this.CursorY0 + num;
                int num4 = this.CursorX0 + num2;
                if (num3 > this._visibleAreaY0Bottom)
                {
                    int lines = num3 - this._visibleAreaY0Bottom;
                    this.ScrollUp(lines);
                    this._visibleAreaY0Bottom += lines;
                    this._visibleAreaY0Top = (this._visibleAreaY0Bottom - this.Height) - 1;
                }
                this.CursorY0 = num3;
                this.CursorX0 = num4;
            }
        }

        public bool MoveCursorTo(int xPos, int yPos)
        {
            int num = xPos - this._offsetx;
            int num2 = yPos - this._offsety;
            if ((num < 0) || (num2 < 0))
            {
                return false;
            }
            int num3 = num / this.Width;
            if (num3 > 0)
            {
                num2 += num3;
                num -= num3 * this.Width;
            }
            if (num2 > this._visibleAreaY0Bottom)
            {
                int lines = num2 - this._visibleAreaY0Bottom;
                this.ScrollUp(lines);
                this._visibleAreaY0Bottom = num2 + lines;
                this._visibleAreaY0Top = (this._visibleAreaY0Bottom - this.Height) - 1;
            }
            this.CursorX0 = num;
            this.CursorY0 = num2;
            return true;
        }

        public void MoveCursorVertical(int lines)
        {
            this.MoveCursor(lines * this.Width);
        }

        public int ScrollUp(int lines)
        {
            if (lines < 1)
            {
                return 0;
            }
            int length = this._vs.GetLength(0);
            int num2 = this._vs.GetLength(1);
            if (lines >= num2)
            {
                int num3 = this._visibleAreaY0Top;
                int num4 = this._visibleAreaY0Bottom;
                this.CleanScreen();
                this._visibleAreaY0Top = num3;
                this._visibleAreaY0Bottom = num4;
            }
            else
            {
                for (int i = lines; i < num2; i++)
                {
                    int num6 = i - lines;
                    for (int j = 0; j < length; j++)
                    {
                        this._vs[j, num6] = this._vs[j, i];
                    }
                }
                this.CleanScreen(this._offsetx, num2 - lines, length + this._offsetx, (num2 - 1) + this._offsety);
            }
            this._changedScreen = true;
            return lines;
        }

        public override string ToString()
        {
            return string.Concat(new object[] { base.GetType().FullName, " ", this.Width, " | ", this.Height, " | changed ", this._changedScreen });
        }

        public bool Write(char c)
        {
            return this.Write(new string(c, 1));
        }

        public bool Write(string s)
        {
            return ((s != null) && this.WriteByte(Encoding.ASCII.GetBytes(s)));
        }

        public bool WriteByte(byte writeByte)
        {
            return this.WriteByte(writeByte, true);
        }

        public bool WriteByte(byte[] writeBytes)
        {
            if ((writeBytes == null) || (writeBytes.Length < 1))
            {
                return false;
            }
            foreach (byte num in writeBytes)
            {
                if (!this.WriteByte(num, true))
                {
                    return false;
                }
            }
            return true;
        }

        public bool WriteByte(byte writeByte, bool moveCursor)
        {
            if (this._vs == null)
            {
                return false;
            }
            switch (writeByte)
            {
                case 10:
                    this.CursorY0++;
                    break;

                case 13:
                    this.CursorX0 = 0;
                    break;

                default:
                {
                    int num = this.CursorY0;
                    if (this._visibleAreaY0Top > 0)
                    {
                        num -= this._visibleAreaY0Top;
                    }
                    if (num >= 0)
                    {
                        try
                        {
                            this._vs[this.CursorX0, num] = writeByte;
                        }
                        catch
                        {
                        }
                    }
                    if (moveCursor)
                    {
                        this.MoveCursor(1);
                    }
                    break;
                }
            }
            this._changedScreen = true;
            return true;
        }

        public bool WriteLine(string s)
        {
            return ((s != null) && this.Write(s + "\n\r"));
        }

        public bool ChangedScreen
        {
            get
            {
                return this._changedScreen;
            }
        }

        public int CursorX
        {
            get
            {
                return (this.CursorX0 + this._offsetx);
            }
            set
            {
                this.CursorX0 = value - this._offsetx;
            }
        }

        private int CursorX0
        {
            get
            {
                return this._cursorx0;
            }
            set
            {
                if (value <= 0)
                {
                    this._cursorx0 = 0;
                }
                else if (value >= this.Width)
                {
                    this._cursorx0 = this.Width - 1;
                }
                else
                {
                    this._cursorx0 = value;
                }
            }
        }

        public int CursorXLeft
        {
            get
            {
                return this._offsetx;
            }
        }

        public int CursorXRight
        {
            get
            {
                return ((this.Width - 1) + this._offsetx);
            }
        }

        public int CursorY
        {
            get
            {
                return (this.CursorY0 + this._offsety);
            }
            set
            {
                this.CursorY0 = value - this._offsety;
            }
        }

        private int CursorY0
        {
            get
            {
                return this._cursory0;
            }
            set
            {
                this._cursory0 = (value <= 0) ? 0 : value;
            }
        }

        public int CursorYMax
        {
            get
            {
                return ((this.Height - 1) + this._offsety);
            }
        }

        public int CursorYMin
        {
            get
            {
                return this._offsety;
            }
        }

        public int Height
        {
            get
            {
                if (this._vs == null)
                {
                    return 0;
                }
                return this._vs.GetLength(1);
            }
        }

        public int Width
        {
            get
            {
                return ((this._vs == null) ? 0 : this._vs.GetLength(0));
            }
        }
    }
}

