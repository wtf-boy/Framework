namespace WTF.Framework
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;

    public sealed class NativeMethods
    {
        internal const int CONTEXT_E_NOCONTEXT = -2147164156;
        internal const uint DACL_SECURITY_INFORMATION = 4;
        internal const int E_NOINTERFACE = -2147467262;
        internal const uint GROUP_SECURITY_INFORMATION = 2;
        internal const uint OWNER_SECURITY_INFORMATION = 1;
        internal const uint SACL_SECURITY_INFORMATION = 8;

        private NativeMethods()
        {
        }

        [DllImport("kernel32.dll")]
        internal static extern IntPtr GetCurrentProcess();
        [DllImport("kernel32.dll")]
        public static extern int GetCurrentProcessId();
        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();
        [DllImport("kernel32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
        public static extern int GetModuleFileName([In] IntPtr hModule, [Out] StringBuilder lpFilename, [In, MarshalAs(UnmanagedType.U4)] int nSize);
        [DllImport("kernel32.dll", CharSet=CharSet.Unicode)]
        public static extern IntPtr GetModuleHandle(string moduleName);
        [DllImport("mtxex.dll", CallingConvention=CallingConvention.Cdecl)]
        internal static extern int GetObjectContext([MarshalAs(UnmanagedType.Interface)] out IObjectContext pCtx);
        [DllImport("advapi32.dll")]
        internal static extern int GetSecurityInfo(IntPtr handle, SE_OBJECT_TYPE objectType, uint securityInformation, ref IntPtr ppSidOwner, ref IntPtr ppSidGroup, ref IntPtr ppDacl, ref IntPtr ppSacl, out IntPtr ppSecurityDescriptor);
        [return: MarshalAs(UnmanagedType.I1)]
        [DllImport("secur32.dll", EntryPoint="GetUserNameExW", CharSet=CharSet.Unicode, SetLastError=true)]
        internal static extern bool GetUserNameEx([In] ExtendedNameFormat nameFormat, StringBuilder nameBuffer, ref uint size);
        [DllImport("advapi32.dll", CharSet=CharSet.Unicode)]
        internal static extern bool LookupAccountSid(IntPtr systemName, IntPtr sid, StringBuilder accountName, ref uint accountNameLength, StringBuilder domainName, ref uint domainNameLength, out int sidType);
        [DllImport("kernel32.dll")]
        internal static extern int QueryPerformanceCounter(out long lpPerformanceCount);
        [DllImport("kernel32.dll")]
        internal static extern int QueryPerformanceFrequency(out long lpPerformanceCount);

        [StructLayout(LayoutKind.Sequential)]
        public class COMRECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
            public COMRECT()
            {
            }

            public COMRECT(Rectangle r)
            {
                this.left = r.X;
                this.top = r.Y;
                this.right = r.Right;
                this.bottom = r.Bottom;
            }

            public COMRECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public static WTF.Framework.NativeMethods.COMRECT FromXYWH(int x, int y, int width, int height)
            {
                return new WTF.Framework.NativeMethods.COMRECT(x, y, x + width, y + height);
            }

            public override string ToString()
            {
                return string.Concat(new object[] { "Left = ", this.left, " Top ", this.top, " Right = ", this.right, " Bottom = ", this.bottom });
            }
        }

        internal enum ExtendedNameFormat
        {
            NameCanonical = 7,
            NameCanonicalEx = 9,
            NameDisplay = 3,
            NameDnsDomain = 12,
            NameFullyQualifiedDN = 1,
            NameSamCompatible = 2,
            NameServicePrincipal = 10,
            NameUniqueId = 6,
            NameUnknown = 0,
            NameUserPrincipal = 8
        }

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("51372AE0-CAE7-11CF-BE81-00AA00A2FA25")]
        internal interface IObjectContext
        {
            [return: MarshalAs(UnmanagedType.Interface)]
            object CreateInstance([MarshalAs(UnmanagedType.LPStruct)] Guid rclsid, [MarshalAs(UnmanagedType.LPStruct)] Guid riid);
            void SetComplete();
            void SetAbort();
            void EnableCommit();
            void DisableCommit();
            [return: MarshalAs(UnmanagedType.Bool)]
            [PreserveSig]
            bool IsInTransaction();
            [return: MarshalAs(UnmanagedType.Bool)]
            [PreserveSig]
            bool IsSecurityEnabled();
            [return: MarshalAs(UnmanagedType.Bool)]
            bool IsCallerInRole([In, MarshalAs(UnmanagedType.BStr)] string role);
        }

        internal enum SE_OBJECT_TYPE
        {
            SE_UNKNOWN_OBJECT_TYPE,
            SE_FILE_OBJECT,
            SE_SERVICE,
            SE_PRINTER,
            SE_REGISTRY_KEY,
            SE_LMSHARE,
            SE_KERNEL_OBJECT,
            SE_WINDOW_OBJECT,
            SE_DS_OBJECT,
            SE_DS_OBJECT_ALL,
            SE_PROVIDER_DEFINED_OBJECT,
            SE_WMIGUID_OBJECT
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagDVTARGETDEVICE
        {
            [MarshalAs(UnmanagedType.U4)]
            public int tdSize;
            [MarshalAs(UnmanagedType.U2)]
            public short tdDriverNameOffset;
            [MarshalAs(UnmanagedType.U2)]
            public short tdDeviceNameOffset;
            [MarshalAs(UnmanagedType.U2)]
            public short tdPortNameOffset;
            [MarshalAs(UnmanagedType.U2)]
            public short tdExtDevmodeOffset;
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagLOGPALETTE
        {
            [MarshalAs(UnmanagedType.U2)]
            public short palVersion;
            [MarshalAs(UnmanagedType.U2)]
            public short palNumEntries;
        }
    }
}

