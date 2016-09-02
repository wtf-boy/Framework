namespace WTF.Framework
{
    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;

    public class Telnet : IDisposable
    {
        private byte[] _buffer;
        private AsyncCallback _callBackReceive;
        private AsyncCallback _callBackSend;
        private bool _clientInitNaws;
        private bool _firstResponse;
        private bool _forceLogout;
        private readonly string _hostName;
        private bool _nawsNegotiated;
        private readonly int _port;
        private bool _serverEcho;
        private TcpClient _tcpClient;
        private readonly int _timeoutReceive;
        private readonly int _timeoutSend;
        private WTF.Framework.VirtualScreen _virtualScreen;
        private readonly int _vsHeight;
        private readonly int _vsWidth;
        private const byte Cr = 13;
        private const string Endofline = "\r\n";
        private const byte Esc = 0x1b;
        private const string F1 = "\033OP";
        private const string F10 = "\033[21~";
        private const string F11 = "\033[23~";
        private const string F12 = "\033[24~";
        private const string F2 = "\033OQ";
        private const string F3 = "\033OR";
        private const string F4 = "\033OS";
        private const string F5 = "\033[15~";
        private const string F6 = "\033[17~";
        private const string F7 = "\033[18~";
        private const string F8 = "\033[19~";
        private const string F9 = "\033[20~";
        private const byte Lf = 10;
        private const int ReceiveBufferSize = 0x2800;
        private static readonly Regex RegExpCursorLeft = new Regex(@"\[\d*D", RegexOptions.Compiled);
        private static readonly Regex RegExpCursorPosition = new Regex(@"\[\d*;\d*[Hf]", RegexOptions.Compiled);
        private static readonly Regex RegExpCursorRight = new Regex(@"\[\d*C", RegexOptions.Compiled);
        private static readonly Regex RegExpCursorXPosition = new Regex(@";\d+[Hf]", RegexOptions.Compiled);
        private static readonly Regex RegExpCursorYPosition = new Regex(@"\[\d+;", RegexOptions.Compiled);
        private static readonly Regex RegExpIp = new Regex(@"\d?\d?\d\.\d?\d?\d\.\d?\d?\d\.\d?\d?\d", RegexOptions.Compiled);
        private static readonly Regex RegExpNumber = new Regex(@"\d+", RegexOptions.Compiled);
        private static readonly Regex RegExpScrollingRegion = new Regex(@"\[\d*;\d*r", RegexOptions.Compiled);
        private const int ScreenXNullcoordinate = 0;
        private const int ScreenYNullCoordinate = 0;
        private const int SendBufferSize = 0x19;
        private const byte TncDo = 0xfd;
        private const byte TncDont = 0xfe;
        private const byte TncIac = 0xff;
        private const byte TncSb = 250;
        private const byte TncSe = 240;
        private const byte TncWill = 0xfb;
        private const byte TncWont = 0xfc;
        private const byte TnoEcho = 1;
        private const byte TnoLogout = 0x12;
        private const byte TnoNaws = 0x1f;
        private const byte TnoNewenv = 0x27;
        private const byte TnoRemoteflow = 0x21;
        private const byte TnoTermspeed = 0x20;
        private const byte TnoTermtype = 0x18;
        private const byte TnoXdisplay = 0x23;
        private const byte TnxIs = 0;
        private const int Trails = 0x19;
        public const string Version = "0.74";

        public Telnet(string hostName) : this(hostName, 0x17, 10, 80, 40)
        {
        }

        public Telnet(string hostName, int port, int timeoutSeconds, int virtualScreenWidth, int virtualScreenHeight)
        {
            this._firstResponse = true;
            this._hostName = hostName;
            this._port = port;
            this._timeoutReceive = timeoutSeconds;
            this._timeoutSend = timeoutSeconds;
            this._serverEcho = false;
            this._clientInitNaws = false;
            this._firstResponse = true;
            this._nawsNegotiated = false;
            this._forceLogout = false;
            this._vsHeight = virtualScreenHeight;
            this._vsWidth = virtualScreenWidth;
        }

        private void CleanBuffer(int bytesRead)
        {
            if (this._buffer != null)
            {
                for (int i = 0; (i < bytesRead) && (i < this._buffer.Length); i++)
                {
                    this._buffer[i] = 0;
                }
            }
        }

        public void Close()
        {
            if (this._tcpClient != null)
            {
                try
                {
                    this._tcpClient.GetStream().Close();
                    this._tcpClient.Close();
                    this._tcpClient = null;
                }
                catch
                {
                    this._tcpClient = null;
                }
            }
            this._virtualScreen = null;
            this._buffer = null;
            this._callBackReceive = null;
            this._callBackSend = null;
            this._forceLogout = false;
        }

        public bool Connect()
        {
            if (this._buffer == null)
            {
                this._buffer = new byte[0x2800];
            }
            if (this._virtualScreen == null)
            {
                this._virtualScreen = new WTF.Framework.VirtualScreen(this._vsWidth, this._vsHeight, 1, 1);
            }
            if (this._callBackReceive == null)
            {
                this._callBackReceive = new AsyncCallback(this.ReadFromStream);
            }
            if (this._callBackSend == null)
            {
                this._callBackSend = new AsyncCallback(this.WriteToStream);
            }
            this._serverEcho = false;
            this._clientInitNaws = false;
            this._firstResponse = true;
            this._nawsNegotiated = false;
            this._forceLogout = false;
            if (this._tcpClient != null)
            {
                return true;
            }
            try
            {
                TcpClient client = new TcpClient(this._hostName, this._port) {
                    ReceiveTimeout = this._timeoutReceive,
                    SendTimeout = this._timeoutSend,
                    NoDelay = true
                };
                this._tcpClient = client;
                this._tcpClient.GetStream().BeginRead(this._buffer, 0, this._buffer.Length, this._callBackReceive, null);
                return true;
            }
            catch
            {
                this._tcpClient = null;
                return false;
            }
        }

        public void Dispose()
        {
            this.Close();
        }

        ~Telnet()
        {
            this.Close();
        }

        public static string FindIpAddress(string candidate)
        {
            if (candidate == null)
            {
                return null;
            }
            Match match = RegExpIp.Match(candidate);
            return (match.Success ? match.Value : null);
        }

        private int GetWaitSleepTimeMs(int timeoutSeconds)
        {
            return ((this.GetWaitTimeout(timeoutSeconds) * 0x3e8) / 0x19);
        }

        private int GetWaitTimeout(int timeoutSeconds)
        {
            if ((timeoutSeconds < 0) && (this._timeoutReceive < 0))
            {
                return 0;
            }
            if (timeoutSeconds < 0)
            {
                return this._timeoutReceive;
            }
            return ((timeoutSeconds >= this._timeoutReceive) ? timeoutSeconds : this._timeoutReceive);
        }

        private int MatchRegExp(int bufferCounter, Regex r)
        {
            if (((r != null) && (this._buffer != null)) && (this._buffer.Length >= bufferCounter))
            {
                string str = (this._buffer.Length >= (bufferCounter + 10)) ? Encoding.ASCII.GetString(this._buffer, bufferCounter, 10) : Encoding.ASCII.GetString(this._buffer, bufferCounter, this._buffer.Length - bufferCounter);
                if (string.IsNullOrEmpty(str))
                {
                    return -1;
                }
                Match match = r.Match(str);
                if (match.Success && (match.Index == 0))
                {
                    return match.Length;
                }
            }
            return -1;
        }

        private int MatchSequence(int bufferCounter, string sequence)
        {
            if (sequence == null)
            {
                return -1;
            }
            return this.MatchSequence(bufferCounter, Encoding.ASCII.GetBytes(sequence), null);
        }

        private int MatchSequence(int bufferCounter, byte[] sequence, int[] ignoreIndex = null)
        {
            if ((sequence == null) || (this._buffer == null))
            {
                return -1;
            }
            if (this._buffer.Length < (bufferCounter + sequence.Length))
            {
                return -1;
            }
            for (int i = 0; i < sequence.Length; i++)
            {
                if (this._buffer[bufferCounter + i] == sequence[i])
                {
                    continue;
                }
                if ((ignoreIndex == null) || (ignoreIndex.Length < 1))
                {
                    return -1;
                }
                bool flag = false;
                foreach (int num2 in ignoreIndex)
                {
                    if (num2 == i)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    return -1;
                }
            }
            return sequence.Length;
        }

        private static byte[] MemoryStreamToByte(MemoryStream ms)
        {
            if (ms == null)
            {
                return null;
            }
            if (ms.Position < 2L)
            {
                return new byte[0];
            }
            byte[] buffer = new byte[ms.Position];
            byte[] buffer2 = ms.ToArray();
            for (int i = 0; (i < buffer.Length) && (i < buffer2.Length); i++)
            {
                buffer[i] = buffer2[i];
            }
            return buffer;
        }

        private static int NewCursorXPosition(string escSequence)
        {
            if (escSequence == null)
            {
                return -1;
            }
            Match match = RegExpCursorXPosition.Match(escSequence);
            if (match.Success)
            {
                match = RegExpNumber.Match(match.Value);
                if (match.Success)
                {
                    try
                    {
                        return int.Parse(match.Value);
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
            return 0;
        }

        private static int NewCursorYPosition(string escSequence)
        {
            if (escSequence == null)
            {
                return -1;
            }
            Match match = RegExpCursorYPosition.Match(escSequence);
            if (match.Success)
            {
                match = RegExpNumber.Match(match.Value);
                if (match.Success)
                {
                    try
                    {
                        return int.Parse(match.Value);
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
            return 0;
        }

        private void ParseAndRespondServerStream(int bytesRead)
        {
            MemoryStream response = new MemoryStream(0x19);
            int index = 0;
            while (((this._buffer != null) && (index < bytesRead)) && (index < this._buffer.Length))
            {
                try
                {
                    byte num2 = this._buffer[index];
                    switch (num2)
                    {
                        case 0x1b:
                            index = this.ParseEscSequence(index, response);
                            goto Label_02BA;

                        case 0xff:
                            index++;
                            num2 = this._buffer[index];
                            switch (num2)
                            {
                                case 250:
                                    index++;
                                    num2 = this._buffer[index];
                                    switch (num2)
                                    {
                                        case 0x18:
                                            goto Label_01CD;

                                        case 0x1f:
                                            goto Label_01DA;
                                    }
                                    goto Label_020C;

                                case 0xfb:
                                    index++;
                                    num2 = this._buffer[index];
                                    switch (num2)
                                    {
                                        case 1:
                                            goto Label_024C;

                                        case 0x12:
                                            goto Label_0264;
                                    }
                                    goto Label_026D;

                                case 0xfc:
                                    goto Label_0280;

                                case 0xfd:
                                    index++;
                                    num2 = this._buffer[index];
                                    if (num2 > 0x12)
                                    {
                                        goto Label_00CD;
                                    }
                                    switch (num2)
                                    {
                                        case 1:
                                            goto Label_016E;

                                        case 0x12:
                                            goto Label_0183;
                                    }
                                    goto Label_0195;

                                case 0xfe:
                                    index++;
                                    break;
                            }
                            goto Label_02BA;

                        case 10:
                            this._virtualScreen.WriteByte((byte) 10);
                            goto Label_02BA;

                        case 13:
                            this._virtualScreen.WriteByte((byte) 13);
                            goto Label_02BA;

                        default:
                            this._virtualScreen.WriteByte(this._buffer[index]);
                            goto Label_02BA;
                    }
                Label_00CD:
                    switch (num2)
                    {
                        case 0x1f:
                            if (!this._clientInitNaws)
                            {
                                TelnetWill(0x1f, response);
                            }
                            TelnetSubNaws(this._virtualScreen.Width, this._virtualScreen.Height, response);
                            this._nawsNegotiated = true;
                            goto Label_02BA;

                        case 0x20:
                            TelnetWont(0x20, response);
                            goto Label_02BA;

                        case 0x21:
                            TelnetWont(0x21, response);
                            goto Label_02BA;

                        case 0x23:
                            TelnetWont(0x23, response);
                            goto Label_02BA;

                        case 0x27:
                            TelnetWont(0x27, response);
                            goto Label_02BA;

                        case 0x18:
                            TelnetWill(0x18, response);
                            goto Label_02BA;

                        default:
                            goto Label_0195;
                    }
                Label_016E:
                    TelnetWont(1, response);
                    goto Label_02BA;
                Label_0183:
                    TelnetWill(0x12, response);
                    this._forceLogout = true;
                    goto Label_02BA;
                Label_0195:
                    TelnetWont(this._buffer[index], response);
                    goto Label_02BA;
                Label_01CD:
                    index++;
                    TelnetSubIsAnsi(response);
                    goto Label_02BA;
                Label_01DA:
                    index++;
                    TelnetSubNaws(this._virtualScreen.Width, this._virtualScreen.Height, response);
                    this._nawsNegotiated = true;
                    goto Label_02BA;
                Label_0206:
                    index++;
                Label_020C:
                    if ((this._buffer[index] != 240) && (index < this._buffer.Length))
                    {
                        goto Label_0206;
                    }
                    goto Label_02BA;
                Label_024C:
                    this._serverEcho = true;
                    TelnetDo(this._buffer[index], response);
                    goto Label_02BA;
                Label_0264:
                    this._forceLogout = true;
                    goto Label_02BA;
                Label_026D:
                    TelnetDo(this._buffer[index], response);
                    goto Label_02BA;
                Label_0280:
                    index++;
                    index++;
                    num2 = this._buffer[index];
                    if (num2 == 1)
                    {
                        this._serverEcho = false;
                    }
                Label_02BA:
                    index++;
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }
            if (this._firstResponse && !this._nawsNegotiated)
            {
                TelnetWill(0x1f, response);
                this._clientInitNaws = true;
            }
            if (response.Position > 0L)
            {
                byte[] buffer = MemoryStreamToByte(response);
                if ((buffer != null) && (buffer.Length > 0))
                {
                    this._tcpClient.GetStream().Write(buffer, 0, buffer.Length);
                    this._tcpClient.GetStream().Flush();
                    this._firstResponse = false;
                }
            }
            try
            {
                response.Close();
            }
            catch
            {
            }
        }

        private int ParseEscSequence(int bc, MemoryStream response)
        {
            if (this._buffer != null)
            {
                if (this._buffer[bc] == 0x1b)
                {
                    bc++;
                }
                int count = this.MatchSequence(bc, "D");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "E");
                if (count > -1)
                {
                    this._virtualScreen.CursorNextLine();
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "M");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "7");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "8");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchRegExp(bc, RegExpCursorLeft);
                if (count > -1)
                {
                    this._virtualScreen.MoveCursor(-1);
                    return ((bc + count) - 1);
                }
                count = this.MatchRegExp(bc, RegExpCursorRight);
                if (count > -1)
                {
                    this._virtualScreen.MoveCursor(1);
                    return ((bc + count) - 1);
                }
                count = this.MatchRegExp(bc, RegExpCursorPosition);
                if (count > -1)
                {
                    string escSequence = Encoding.ASCII.GetString(this._buffer, bc, count);
                    int xPos = NewCursorXPosition(escSequence);
                    int yPos = NewCursorYPosition(escSequence);
                    this._virtualScreen.MoveCursorTo(xPos, yPos);
                    return ((bc + count) - 1);
                }
                count = this.MatchRegExp(bc, RegExpScrollingRegion);
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "#3");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "#4");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "#5");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "#6");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[K");
                if (count > -1)
                {
                    this._virtualScreen.CleanLine(this._virtualScreen.CursorX, this._virtualScreen.CursorXRight);
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[0K");
                if (count > -1)
                {
                    this._virtualScreen.CleanLine(this._virtualScreen.CursorX, this._virtualScreen.CursorXRight);
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[1K");
                if (count > -1)
                {
                    this._virtualScreen.CleanLine(this._virtualScreen.CursorXLeft, this._virtualScreen.CursorX);
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[2K");
                if (count > -1)
                {
                    this._virtualScreen.CleanLine(this._virtualScreen.CursorXLeft, this._virtualScreen.CursorXRight);
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[J");
                if (count > -1)
                {
                    this._virtualScreen.CleanFromCursor();
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[0J");
                if (count > -1)
                {
                    this._virtualScreen.CleanFromCursor();
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[1J");
                if (count > -1)
                {
                    this._virtualScreen.CleanToCursor();
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[2J");
                if (count > -1)
                {
                    this._virtualScreen.CleanScreen();
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "#7");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "1");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "2");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "H");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[g");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[0g");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[3g");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[20h");
                if (count > -1)
                {
                    this._virtualScreen.CursorNextLine();
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[20l");
                if (count > -1)
                {
                    response.WriteByte(10);
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[?1h");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[?1l");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[?3h");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[?3l");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[?4h");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[?4l");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[?5h");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[?5l");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[?6h");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[?6l");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[?7h");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[?7l");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[?8h");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[?8l");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[?9h");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "[?9l");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "1");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "2");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "=");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, ">");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "A");
                if (count > -1)
                {
                    this._virtualScreen.MoveCursorVertical(-1);
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "B");
                if (count > -1)
                {
                    this._virtualScreen.MoveCursorVertical(1);
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "C");
                if (count > -1)
                {
                    this._virtualScreen.MoveCursorVertical(1);
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "D");
                if (count > -1)
                {
                    this._virtualScreen.MoveCursorVertical(-1);
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "F");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "G");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "H");
                if (count > -1)
                {
                    this._virtualScreen.CursorReset();
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "I");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "J");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "K");
                if (count > -1)
                {
                    this._virtualScreen.CleanLine(this._virtualScreen.CursorX, this._virtualScreen.CursorXRight);
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "Ylc");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "Z");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "=");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, "<");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
                count = this.MatchSequence(bc, ">");
                if (count > -1)
                {
                    return ((bc + count) - 1);
                }
            }
            return bc;
        }

        private void ReadFromStream(IAsyncResult asyncResult)
        {
            if ((asyncResult == null) || (this._tcpClient == null))
            {
                this.Close();
            }
            else
            {
                try
                {
                    int bytesRead = this._tcpClient.GetStream().EndRead(asyncResult);
                    if (bytesRead > 0)
                    {
                        lock (this._virtualScreen)
                        {
                            this.ParseAndRespondServerStream(bytesRead);
                        }
                        this.CleanBuffer(bytesRead);
                        if (this._forceLogout)
                        {
                            this.Close();
                        }
                        else
                        {
                            this._tcpClient.GetStream().BeginRead(this._buffer, 0, this._buffer.Length, this._callBackReceive, null);
                        }
                    }
                    else
                    {
                        this.Close();
                    }
                }
                catch
                {
                    this.Close();
                }
            }
        }

        public bool SendLogout()
        {
            return this.SendLogout(true);
        }

        public bool SendLogout(bool synchronous)
        {
            byte[] buffer = new byte[] { 0xff, 0xfd, 0x12 };
            try
            {
                if (synchronous)
                {
                    this._tcpClient.GetStream().Write(buffer, 0, buffer.Length);
                }
                else
                {
                    this._tcpClient.GetStream().BeginWrite(buffer, 0, buffer.Length, this._callBackSend, null);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SendResponse(string response, bool endLine)
        {
            try
            {
                if (!(this.IsOpenConnection && (this._tcpClient != null)))
                {
                    return false;
                }
                if (!string.IsNullOrEmpty(response))
                {
                    byte[] buffer = endLine ? Encoding.ASCII.GetBytes(response + "\r\n") : Encoding.ASCII.GetBytes(response);
                    if (buffer.Length < 1)
                    {
                        return false;
                    }
                    this._tcpClient.GetStream().BeginWrite(buffer, 0, buffer.Length, this._callBackSend, null);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SendResponseFunctionKey(int key)
        {
            if ((key >= 1) && (key <= 12))
            {
                switch (key)
                {
                    case 1:
                        return this.SendResponse("\033OP", false);

                    case 2:
                        return this.SendResponse("\033OQ", false);

                    case 3:
                        return this.SendResponse("\033OR", false);

                    case 4:
                        return this.SendResponse("\033OS", false);

                    case 5:
                        return this.SendResponse("\033[15~", false);

                    case 6:
                        return this.SendResponse("\033[17~", false);

                    case 7:
                        return this.SendResponse("\033[18~", false);

                    case 8:
                        return this.SendResponse("\033[19~", false);

                    case 9:
                        return this.SendResponse("\033[20~", false);

                    case 10:
                        return this.SendResponse("\033[21~", false);

                    case 11:
                        return this.SendResponse("\033[23~", false);

                    case 12:
                        return this.SendResponse("\033[24~", false);
                }
            }
            return false;
        }

        private static void TelnetDo(byte doWhat, MemoryStream response)
        {
            response.WriteByte(0xff);
            response.WriteByte(0xfd);
            response.WriteByte(doWhat);
        }

        private static void TelnetSubIsAnsi(MemoryStream response)
        {
            response.WriteByte(0xff);
            response.WriteByte(250);
            response.WriteByte(0x18);
            response.WriteByte(0);
            response.WriteByte(0x41);
            response.WriteByte(0x4e);
            response.WriteByte(0x53);
            response.WriteByte(0x49);
            response.WriteByte(0xff);
            response.WriteByte(240);
        }

        private static void TelnetSubNaws(int w, int h, MemoryStream response)
        {
            byte num = (byte) (0xff & w);
            byte num2 = (byte) (0xff00 & w);
            byte num3 = (byte) (0xff & h);
            byte num4 = (byte) (0xff00 & h);
            response.WriteByte(0xff);
            response.WriteByte(250);
            response.WriteByte(0x1f);
            response.WriteByte(num2);
            response.WriteByte(num);
            response.WriteByte(num4);
            response.WriteByte(num3);
            response.WriteByte(0xff);
            response.WriteByte(240);
        }

        private static void TelnetWill(byte willDoWhat, MemoryStream response)
        {
            response.WriteByte(0xff);
            response.WriteByte(0xfb);
            response.WriteByte(willDoWhat);
        }

        private static void TelnetWont(byte wontDoWhat, MemoryStream response)
        {
            response.WriteByte(0xff);
            response.WriteByte(0xfc);
            response.WriteByte(wontDoWhat);
        }

        private DateTime TimeoutAbsoluteTime(int timeoutSeconds)
        {
            return DateTime.Now.AddSeconds((double) this.GetWaitTimeout(timeoutSeconds));
        }

        public void Wait(int seconds)
        {
            if (seconds > 0)
            {
                Thread.Sleep((int) (seconds * 0x3e8));
            }
        }

        public bool WaitForChangedScreen()
        {
            return this.WaitForChangedScreen(this._timeoutReceive);
        }

        public bool WaitForChangedScreen(int timeoutSeconds)
        {
            if ((this._virtualScreen != null) && (timeoutSeconds >= 0))
            {
                if (this._virtualScreen.ChangedScreen)
                {
                    this._virtualScreen.Hardcopy(false);
                }
                if (timeoutSeconds <= 0)
                {
                    return false;
                }
                int waitSleepTimeMs = this.GetWaitSleepTimeMs(timeoutSeconds);
                DateTime time = this.TimeoutAbsoluteTime(timeoutSeconds);
                do
                {
                    lock (this._virtualScreen)
                    {
                        if (this._virtualScreen.ChangedScreen)
                        {
                            return true;
                        }
                    }
                    Thread.Sleep(waitSleepTimeMs);
                }
                while (DateTime.Now <= time);
            }
            return false;
        }

        public string WaitForRegEx(string regEx)
        {
            return this.WaitForRegEx(regEx, this._timeoutReceive);
        }

        public string WaitForRegEx(string regEx, int timeoutSeconds)
        {
            if (((this._virtualScreen != null) && (regEx != null)) && (regEx.Length >= 1))
            {
                int waitSleepTimeMs = this.GetWaitSleepTimeMs(timeoutSeconds);
                DateTime time = this.TimeoutAbsoluteTime(timeoutSeconds);
                do
                {
                    string str;
                    lock (this._virtualScreen)
                    {
                        str = this._virtualScreen.FindRegExOnScreen(regEx);
                    }
                    if (str != null)
                    {
                        return str;
                    }
                    Thread.Sleep(waitSleepTimeMs);
                }
                while (DateTime.Now <= time);
            }
            return null;
        }

        public string WaitForString(string searchFor)
        {
            return this.WaitForString(searchFor, false, this._timeoutReceive);
        }

        public string WaitForString(string searchFor, bool caseSensitive, int timeoutSeconds)
        {
            if (((this._virtualScreen != null) && (searchFor != null)) && (searchFor.Length >= 1))
            {
                int waitSleepTimeMs = this.GetWaitSleepTimeMs(timeoutSeconds);
                DateTime time = this.TimeoutAbsoluteTime(timeoutSeconds);
                do
                {
                    string str;
                    lock (this._virtualScreen)
                    {
                        str = this._virtualScreen.FindOnScreen(searchFor, caseSensitive);
                    }
                    if (str != null)
                    {
                        return str;
                    }
                    Thread.Sleep(waitSleepTimeMs);
                }
                while (DateTime.Now <= time);
            }
            return null;
        }

        private void WriteToStream(IAsyncResult asyncResult)
        {
            if ((asyncResult == null) || (this._tcpClient == null))
            {
                this.Close();
            }
            else
            {
                try
                {
                    this._tcpClient.GetStream().EndWrite(asyncResult);
                }
                catch
                {
                    this.Close();
                }
            }
        }

        public bool EchoOn
        {
            get
            {
                return this._serverEcho;
            }
        }

        public bool IsOpenConnection
        {
            get
            {
                return (this._tcpClient != null);
            }
        }

        public WTF.Framework.VirtualScreen VirtualScreen
        {
            get
            {
                return this._virtualScreen;
            }
        }
    }
}

