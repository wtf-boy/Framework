namespace WTF.Framework
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Net;
    using System.Runtime.InteropServices;

    public class ImageValueHelper
    {
        private string[] ArrayList = new string[] { 
            "00011100011111110110001111000001110000011100000111000001110000011100000111000001011000110111111100011100", "00111000111110001111100000011000000110000001100000011000000110000001100000011000000110001111111111111111", "01111100111111101000001100000011000000110000011000001100000110000011000001100000110000001111111111111111", "01111100111111111000001100000011000001100111100001111110000001110000001100000011100001111111111001111100", "00001100000111000001110000111100011011000110110010001100110011001111111110111111000011000000110000001100", "11111111111111111100000011000000110000001111100011111110000001110000001100000011100001111111111001111100", "00011110001111110110000101100000110000001101111011101111111000111100000101000001011000110110111100011110", "01111010001111110000000100000000000000110000011000000100000011000000100000011000000110000011000000110000", "00111110011111110110001101100011011100100011111000111110011001111100000111000001111000110111111100111110", "00011100011110110110001111000001110000011010001101111111001111010000000100000001010000110101110000111100", "00111000111111101000010010000011100000111000001110000011000000111000001110000011110001001111011000111000", "00001100011111000111110000001100000011000000110000001100000011000000110000001100000011000111110101111111", "11111000110111000000010000000110000001100000110000011000001100000110000011000000100000000101111011111110", "10111000111111100000001000000110000011001111000011110100000011100000011000000110000011101111110011111000", "00000110000011100000111000001110000101100011011000100110011001001111111111111111000001000000011000000110", "11111110111111101000000010000000100000001111000011110100000011100000011000000110000011101111110011111000", 
            "00111100011111101100001011000000100000001011110011111110110001111000001110000011110001101111111000111100", "11101111111111110000001100000010000001100000010000001000000110000001000000110000001100000110000001100000", "01111100111111101100011011000110110001000111110001111100110011101000001110000011110001101101111001111100", "01111000111111101100011010000011100000111100011111111111011110110000001100000100100001101111110001111000", "00111100011111111100001110000001110000011110001101111111001101010000000000000011010000010111111000111100", "00111000111111001100011010000011100000011000001100000011100000111000001010000011110001101111010000101000", "00111000010111010110001111000001110000011110001101111111001111010000000100000011010000100111011000111100", "00000110000010100000111000010110001101100011011001000110011001101011101111111111000001100000011000000110", "00011110001011110110000101000000110000001101101011111101011000111100000110000001010000110101111000011110", "00111100011111101110001101000001110000011110001101111111001111010000000100000011000000110101101000111100", "11111000111100000000011000000100000000100000110000011000001100000110000001000000100000001111110011111110"
         };

        public string GetImageValues(string url)
        {
            Bitmap pic = new Bitmap(WebRequest.Create(url).GetResponse().GetResponseStream(), false);
            UnCodebase codebase = new UnCodebase(pic);
            codebase.GrayByPixels();
            Bitmap[] bitmapArray = codebase.readMap();
            int[] numArray = new int[4];
            for (int i = 0; i < 4; i++)
            {
                numArray[i] = codebase.GetSingleDgGrayValue(bitmapArray[i]);
            }
            string[] arr = new string[4];
            for (int j = 0; j < 4; j++)
            {
                arr[j] = codebase.GetSingleBmpCode(bitmapArray[j], numArray[j]);
            }
            return this.getPicnums(arr);
        }

        public string getPicnums(string[] arr)
        {
            string str = "";
            for (int i = 0; i < 4; i++)
            {
                string str2 = arr[i];
                for (int j = 0; j < this.ArrayList.Length; j++)
                {
                    int num3 = 0;
                    if (this.ArrayList[j].Equals(str2))
                    {
                        num3 = 0;
                        if (j > 9)
                        {
                            if (((j == 20) || (j == 0x16)) || (j == 0x19))
                            {
                                str = str + "9";
                            }
                            else if (j == 0x15)
                            {
                                str = str + "0";
                            }
                            else if (j == 0x17)
                            {
                                str = str + "4";
                            }
                            else if (j == 0x18)
                            {
                                str = str + "6";
                            }
                            else if (j == 0x1a)
                            {
                                str = str + "2";
                            }
                            else
                            {
                                int num5 = j - 10;
                                str = str + num5.ToString();
                            }
                        }
                        else
                        {
                            str = str + j.ToString();
                        }
                        break;
                    }
                    for (int k = 0; k < str2.Length; k++)
                    {
                        char ch = arr[i][k];
                        char ch2 = this.ArrayList[j][k];
                        if (ch != ch2)
                        {
                            num3++;
                        }
                    }
                    if (num3 < 10)
                    {
                        if (j > 9)
                        {
                            if (((j == 20) || (j == 0x16)) || (j == 0x19))
                            {
                                str = str + "9";
                            }
                            else if (j == 0x15)
                            {
                                str = str + "0";
                            }
                            else if (j == 0x17)
                            {
                                str = str + "4";
                            }
                            else if (j == 0x18)
                            {
                                str = str + "6";
                            }
                            else if (j == 0x1a)
                            {
                                str = str + "2";
                            }
                            else
                            {
                                str = str + ((j - 10)).ToString();
                            }
                        }
                        else
                        {
                            str = str + j.ToString();
                        }
                        break;
                    }
                }
            }
            return str;
        }

        private class UnCodebase
        {
            public Bitmap bmpobj;

            public UnCodebase(Bitmap pic)
            {
                this.bmpobj = new Bitmap(pic);
            }

            public void ClearPicBorder(int borderWidth)
            {
                for (int i = 0; i < this.bmpobj.Height; i++)
                {
                    for (int j = 0; j < this.bmpobj.Width; j++)
                    {
                        if ((((i < borderWidth) || (j < borderWidth)) || (j > ((this.bmpobj.Width - 1) - borderWidth))) || (i > ((this.bmpobj.Height - 1) - borderWidth)))
                        {
                            this.bmpobj.SetPixel(j, i, Color.FromArgb(0xff, 0xff, 0xff));
                        }
                    }
                }
            }

            public int GetDgGrayValue()
            {
                double num8;
                int num11;
                int[] numArray = new int[0x100];
                int num14 = 1;
                for (int i = 0; i < this.bmpobj.Width; i++)
                {
                    for (int j = 0; j < this.bmpobj.Height; j++)
                    {
                        numArray[this.bmpobj.GetPixel(i, j).R]++;
                    }
                }
                for (num11 = 0; num11 <= 0xff; num11++)
                {
                    int num4 = 0;
                    for (int k = -2; k <= 2; k++)
                    {
                        int index = num11 + k;
                        if (index < 0)
                        {
                            index = 0;
                        }
                        if (index > 0xff)
                        {
                            index = 0xff;
                        }
                        num4 += numArray[index];
                    }
                    numArray[num11] = (int) ((((double) num4) / 5.0) + 0.5);
                }
                double num7 = num8 = 0.0;
                int num = 0;
                for (num11 = 0; num11 <= 0xff; num11++)
                {
                    num7 += num11 * numArray[num11];
                    num += numArray[num11];
                }
                double num9 = -1.0;
                int num2 = 0;
                for (num11 = 0; num11 < 0x100; num11++)
                {
                    num2 += numArray[num11];
                    if (num2 != 0)
                    {
                        int num3 = num - num2;
                        if (num3 == 0)
                        {
                            return num14;
                        }
                        num8 += num11 * numArray[num11];
                        double num5 = num8 / ((double) num2);
                        double num6 = (num7 - num8) / ((double) num3);
                        double num10 = ((num2 * num3) * (num5 - num6)) * (num5 - num6);
                        if (num10 > num9)
                        {
                            num9 = num10;
                            num14 = num11;
                        }
                    }
                }
                return num14;
            }

            private int GetGrayNumColor(Color posClr)
            {
                return ((((posClr.R * 0x4c8b) + (posClr.G * 0x9645)) + (posClr.B * 0x1d30)) >> 0x10);
            }

            public void GetPicValidByValue(int dgGrayValue)
            {
                int width = this.bmpobj.Width;
                int height = this.bmpobj.Height;
                int num3 = 0;
                int num4 = 0;
                for (int i = 0; i < this.bmpobj.Height; i++)
                {
                    for (int j = 0; j < this.bmpobj.Width; j++)
                    {
                        if (this.bmpobj.GetPixel(j, i).R < dgGrayValue)
                        {
                            if (width > j)
                            {
                                width = j;
                            }
                            if (height > i)
                            {
                                height = i;
                            }
                            if (num3 < j)
                            {
                                num3 = j;
                            }
                            if (num4 < i)
                            {
                                num4 = i;
                            }
                        }
                    }
                }
                Rectangle rect = new Rectangle(width, height, (num3 - width) + 1, (num4 - height) + 1);
                this.bmpobj = this.bmpobj.Clone(rect, this.bmpobj.PixelFormat);
            }

            public Bitmap GetPicValidByValue(Bitmap singlepic, int dgGrayValue)
            {
                int width = singlepic.Width;
                int height = singlepic.Height;
                int num3 = 0;
                int num4 = 0;
                for (int i = 0; i < singlepic.Height; i++)
                {
                    for (int j = 0; j < singlepic.Width; j++)
                    {
                        if (singlepic.GetPixel(j, i).R < dgGrayValue)
                        {
                            if (width > j)
                            {
                                width = j;
                            }
                            if (height > i)
                            {
                                height = i;
                            }
                            if (num3 < j)
                            {
                                num3 = j;
                            }
                            if (num4 < i)
                            {
                                num4 = i;
                            }
                        }
                    }
                }
                Rectangle rect = new Rectangle(width, height, (num3 - width) + 1, (num4 - height) + 1);
                return singlepic.Clone(rect, singlepic.PixelFormat);
            }

            public void GetPicValidByValue(int dgGrayValue, int CharsCount)
            {
                int width = this.bmpobj.Width;
                int height = this.bmpobj.Height;
                int num3 = 0;
                int num4 = 0;
                for (int i = 0; i < this.bmpobj.Height; i++)
                {
                    for (int j = 0; j < this.bmpobj.Width; j++)
                    {
                        if (this.bmpobj.GetPixel(j, i).R < dgGrayValue)
                        {
                            if (width > j)
                            {
                                width = j;
                            }
                            if (height > i)
                            {
                                height = i;
                            }
                            if (num3 < j)
                            {
                                num3 = j;
                            }
                            if (num4 < i)
                            {
                                num4 = i;
                            }
                        }
                    }
                }
                int num8 = CharsCount - (((num3 - width) + 1) % CharsCount);
                if (num8 < CharsCount)
                {
                    int num9 = num8 / 2;
                    if (width > num9)
                    {
                        width -= num9;
                    }
                    if (((num3 + num8) - num9) < this.bmpobj.Width)
                    {
                        num3 = (num3 + num8) - num9;
                    }
                }
                Rectangle rect = new Rectangle(width, height, (num3 - width) + 1, (num4 - height) + 1);
                this.bmpobj = this.bmpobj.Clone(rect, this.bmpobj.PixelFormat);
            }

            public string GetSingleBmpCode(Bitmap singlepic, int dgGrayValue)
            {
                string str = "";
                for (int i = 0; i < singlepic.Height; i++)
                {
                    for (int j = 0; j < singlepic.Width; j++)
                    {
                        if (singlepic.GetPixel(j, i).R < dgGrayValue)
                        {
                            str = str + "1";
                        }
                        else
                        {
                            str = str + "0";
                        }
                    }
                }
                return str;
            }

            public int GetSingleDgGrayValue(Bitmap singlepic)
            {
                double num8;
                int num11;
                int[] numArray = new int[0x100];
                int num14 = 1;
                for (int i = 0; i < singlepic.Width; i++)
                {
                    for (int j = 0; j < singlepic.Height; j++)
                    {
                        numArray[singlepic.GetPixel(i, j).R]++;
                    }
                }
                for (num11 = 0; num11 <= 0xff; num11++)
                {
                    int num4 = 0;
                    for (int k = -2; k <= 2; k++)
                    {
                        int index = num11 + k;
                        if (index < 0)
                        {
                            index = 0;
                        }
                        if (index > 0xff)
                        {
                            index = 0xff;
                        }
                        num4 += numArray[index];
                    }
                    numArray[num11] = (int) ((((double) num4) / 5.0) + 0.5);
                }
                double num7 = num8 = 0.0;
                int num = 0;
                for (num11 = 0; num11 <= 0xff; num11++)
                {
                    num7 += num11 * numArray[num11];
                    num += numArray[num11];
                }
                double num9 = -1.0;
                int num2 = 0;
                for (num11 = 0; num11 < 0x100; num11++)
                {
                    num2 += numArray[num11];
                    if (num2 != 0)
                    {
                        int num3 = num - num2;
                        if (num3 == 0)
                        {
                            return num14;
                        }
                        num8 += num11 * numArray[num11];
                        double num5 = num8 / ((double) num2);
                        double num6 = (num7 - num8) / ((double) num3);
                        double num10 = ((num2 * num3) * (num5 - num6)) * (num5 - num6);
                        if (num10 > num9)
                        {
                            num9 = num10;
                            num14 = num11;
                        }
                    }
                }
                return num14;
            }

            public Bitmap[] GetSplitPics(int RowNum, int ColNum)
            {
                if ((RowNum == 0) || (ColNum == 0))
                {
                    return null;
                }
                int width = this.bmpobj.Width / RowNum;
                int height = this.bmpobj.Height / ColNum;
                Bitmap[] bitmapArray = new Bitmap[RowNum * ColNum];
                for (int i = 0; i < ColNum; i++)
                {
                    for (int j = 0; j < RowNum; j++)
                    {
                        Rectangle rect = new Rectangle(j * width, i * height, width, height);
                        bitmapArray[(i * RowNum) + j] = this.bmpobj.Clone(rect, this.bmpobj.PixelFormat);
                    }
                }
                return bitmapArray;
            }

            public void GrayByLine()
            {
                Rectangle rect = new Rectangle(0, 0, this.bmpobj.Width, this.bmpobj.Height);
                BitmapData bitmapdata = this.bmpobj.LockBits(rect, ImageLockMode.ReadWrite, this.bmpobj.PixelFormat);
                IntPtr source = bitmapdata.Scan0;
                int length = this.bmpobj.Width * this.bmpobj.Height;
                int[] destination = new int[length];
                Marshal.Copy(source, destination, 0, length);
                int red = 0;
                for (int i = 0; i < length; i++)
                {
                    red = this.GetGrayNumColor(Color.FromArgb(destination[i]));
                    destination[i] = (byte) Color.FromArgb(red, red, red).ToArgb();
                }
                this.bmpobj.UnlockBits(bitmapdata);
            }

            public void GrayByPixels()
            {
                for (int i = 0; i < this.bmpobj.Height; i++)
                {
                    for (int j = 0; j < this.bmpobj.Width; j++)
                    {
                        int grayNumColor = this.GetGrayNumColor(this.bmpobj.GetPixel(j, i));
                        this.bmpobj.SetPixel(j, i, Color.FromArgb(grayNumColor, grayNumColor, grayNumColor));
                    }
                }
            }

            public Bitmap[] readMap()
            {
                RectangleF[] efArray = new RectangleF[] { new Rectangle(7, 3, 8, 13), new Rectangle(20, 3, 8, 13), new Rectangle(0x21, 3, 8, 13), new Rectangle(0x2f, 3, 8, 13) };
                return new Bitmap[] { this.bmpobj.Clone(efArray[0], PixelFormat.Undefined), this.bmpobj.Clone(efArray[1], PixelFormat.Undefined), this.bmpobj.Clone(efArray[2], PixelFormat.Undefined), this.bmpobj.Clone(efArray[3], PixelFormat.Undefined) };
            }
        }
    }
}

