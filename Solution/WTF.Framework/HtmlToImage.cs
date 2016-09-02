namespace WTF.Framework
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    public class HtmlToImage
    {
        private Bitmap m_Bitmap;
        private int m_BrowserHeight;
        private int m_BrowserWidth;
        private int m_ThumbnailHeight;
        private int m_ThumbnailWidth;
        private string m_Url;

        public HtmlToImage(string Url, int BrowserWidth, int BrowserHeight, int ThumbnailWidth, int ThumbnailHeight)
        {
            this.m_Url = Url;
            this.m_BrowserHeight = BrowserHeight;
            this.m_BrowserWidth = BrowserWidth;
            this.m_ThumbnailWidth = ThumbnailWidth;
            this.m_ThumbnailHeight = ThumbnailHeight;
        }

        private void _GenerateHtmlToImage()
        {
            WebBrowser browser = new WebBrowser();
            try
            {
                browser.ScriptErrorsSuppressed = false;
                browser.ScrollBarsEnabled = false;
                browser.Navigate(this.m_Url);
                browser.Size = new Size(this.m_BrowserWidth, this.m_BrowserHeight);
                browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.WebBrowser_DocumentCompleted);
                while (browser.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }
                browser.Stop();
                if (browser.ActiveXInstance == null)
                {
                    throw new Exception("实例不能为空");
                }
                this.m_Bitmap = this.TakeSnapshot(browser.ActiveXInstance, new Rectangle(0, 0, this.m_ThumbnailWidth, this.m_ThumbnailHeight));
                browser.Dispose();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                browser.Dispose();
                throw exception;
            }
        }

        public Bitmap GenerateHtmlToImage()
        {
            Thread thread = new Thread(new ThreadStart(this._GenerateHtmlToImage));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            return this.m_Bitmap;
        }

        public static Bitmap GetHtmlToImage(string Url, int BrowserWidth, int BrowserHeight, int ThumbnailWidth, int ThumbnailHeight)
        {
            HtmlToImage image = new HtmlToImage(Url, BrowserWidth, BrowserHeight, ThumbnailWidth, ThumbnailHeight);
            return image.GenerateHtmlToImage();
        }

        public Bitmap TakeSnapshot(object pUnknown, Rectangle bmpRect)
        {
            if (pUnknown == null)
            {
                return null;
            }
            if (!Marshal.IsComObject(pUnknown))
            {
                return null;
            }
            IntPtr zero = IntPtr.Zero;
            Bitmap image = new Bitmap(bmpRect.Width, bmpRect.Height);
            Graphics graphics = Graphics.FromImage(image);
            object obj3 = Marshal.QueryInterface(Marshal.GetIUnknownForObject(pUnknown), ref WTF.Framework.UnsafeNativeMethods.IID_IViewObject, out zero);
            try
            {
                (Marshal.GetTypedObjectForIUnknown(zero, typeof(WTF.Framework.UnsafeNativeMethods.IViewObject)) as WTF.Framework.UnsafeNativeMethods.IViewObject).Draw(1, -1, IntPtr.Zero, null, IntPtr.Zero, graphics.GetHdc(), new WTF.Framework.NativeMethods.COMRECT(bmpRect), null, IntPtr.Zero, 0);
                Marshal.Release(zero);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            graphics.Dispose();
            return image;
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }
    }
}

