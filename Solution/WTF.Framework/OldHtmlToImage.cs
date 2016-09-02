namespace WTF.Framework
{
    using System;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class OldHtmlToImage
    {
        private Bitmap m_Bitmap;
        private int m_BrowserHeight;
        private int m_BrowserWidth;
        private int m_ThumbnailHeight;
        private int m_ThumbnailWidth;
        private string m_Url;

        public OldHtmlToImage(string Url, int BrowserWidth, int BrowserHeight, int ThumbnailWidth, int ThumbnailHeight)
        {
            this.m_Url = Url;
            this.m_BrowserHeight = BrowserHeight;
            this.m_BrowserWidth = BrowserWidth;
            this.m_ThumbnailWidth = ThumbnailWidth;
            this.m_ThumbnailHeight = ThumbnailHeight;
        }

        private void _GenerateHtmlToImage()
        {
            WebBrowser browser = new WebBrowser {
                ScrollBarsEnabled = false
            };
            browser.Navigate(this.m_Url);
            browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.WebBrowser_DocumentCompleted);
            while (browser.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }
            browser.Dispose();
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

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = (WebBrowser) sender;
            browser.ClientSize = new Size(this.m_BrowserWidth, this.m_BrowserHeight);
            browser.ScrollBarsEnabled = false;
            this.m_Bitmap = new Bitmap(browser.Bounds.Width, browser.Bounds.Height);
            browser.BringToFront();
            browser.DrawToBitmap(this.m_Bitmap, browser.Bounds);
            this.m_Bitmap = (Bitmap) this.m_Bitmap.GetThumbnailImage(this.m_ThumbnailWidth, this.m_ThumbnailHeight, null, IntPtr.Zero);
        }
    }
}

