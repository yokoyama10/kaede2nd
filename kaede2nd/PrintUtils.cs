using System;
using System.Drawing;
using System.Drawing.Printing;

namespace PrintUtils
{
    public partial class DeviceCaps : IDisposable
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern bool DeleteDC(IntPtr hdc);

        private bool isDeleteDeviceCaps;
        private IntPtr printerHandle;
        public DeviceCaps(IntPtr printerHandle)
        {
            this.isDeleteDeviceCaps = false;
            this.printerHandle = printerHandle;
        }
        public DeviceCaps(PrinterSettings printerSettings) : this(printerSettings.PrinterName) { }
        public DeviceCaps(string printerName)
        {
            this.isDeleteDeviceCaps = false;

            if (printerName == null || printerName == String.Empty) throw new ArgumentNullException("PrinterName");
            this.printerHandle = CreateDC(@"WINSPOOL", printerName, null, IntPtr.Zero);
            if (this.printerHandle == IntPtr.Zero) throw new InvalidOperationException("プリンタハンドル取得失敗");

            this.isDeleteDeviceCaps = true; // 解放の必要がある!!
        }

        public int GetData(DeviceCapsCommands command) { return GetDeviceCaps(this.printerHandle, (int)command); }

        public void Dispose() { if (this.isDeleteDeviceCaps) DeleteDC(this.printerHandle); }
    }

    partial class DeviceCaps
    {
        /// <summary>1ミリメートルに対するピクセル数を取得します</summary>
        public SizeF MillimeterPerPixels
        {
            get
            {
                if (this._MillimeterPerPixels == null)
                {
                    this._MillimeterPerPixels = new SizeF(
                      this.GetData(DeviceCapsCommands.HORZRES) / this.GetData(DeviceCapsCommands.HORZSIZE),
                      this.GetData(DeviceCapsCommands.VERTRES) / this.GetData(DeviceCapsCommands.VERTSIZE)
                    );
                }
                return this._MillimeterPerPixels ?? new SizeF();
            }
        }
        private SizeF? _MillimeterPerPixels = null;

        /// <summary>プリンタで印刷可能な左上の位置をミリメートル単位で取得します</summary>
        public PointF PrintMillimeterLocation
        {
            get
            {
                if (this._PrintMillimeterLocation == null)
                {
                    this._PrintMillimeterLocation = new PointF(
                      this.GetData(DeviceCapsCommands.PHYSICALOFFSETX) / this.MillimeterPerPixels.Width,
                      this.GetData(DeviceCapsCommands.PHYSICALOFFSETY) / this.MillimeterPerPixels.Height
                    );
                }
                return this._PrintMillimeterLocation ?? new PointF();
            }
        }
        private PointF? _PrintMillimeterLocation = null;

        /// <summary>プリンタで印刷可能なサイズをミリメートル単位で取得します</summary>
        public SizeF PrintMillimeterSize
        {
            get
            {
                if (this._PrintMillimeterSize == null)
                {
                    var m = this.MillimeterPerPixels;
                    var s = this.PrintMillimeterLocation;
                    this._PrintMillimeterSize = new SizeF(
                      this.GetData(DeviceCapsCommands.PHYSICALWIDTH) / m.Width - s.X,
                      this.GetData(DeviceCapsCommands.PHYSICALHEIGHT) / m.Height - s.Y
                    );
                }
                return this._PrintMillimeterSize ?? new SizeF();
            }
        }
        private SizeF? _PrintMillimeterSize = null;

        /// <summary>プリンタの印刷可能領域をミリメートル単位で取得します</summary>
        public RectangleF PrintMillimeterArea
        {
            get { return new RectangleF(this.PrintMillimeterLocation, this.PrintMillimeterSize); }
        }
    }

    public enum DeviceCapsCommands
    {
        DRIVERVERSION = 0,
        TECHNOLOGY = 2,
        HORZSIZE = 4,
        VERTSIZE = 6,
        HORZRES = 8,
        VERTRES = 10,
        BITSPIXEL = 12,
        PLANES = 14,
        NUMBRUSHES = 16,
        NUMPENS = 18,
        NUMMARKERS = 20,
        NUMFONTS = 22,
        NUMCOLORS = 24,
        PDEVICESIZE = 26,
        CURVECAPS = 28,
        LINECAPS = 30,
        POLYGONALCAPS = 32,
        TEXTCAPS = 34,
        CLIPCAPS = 36,
        RASTERCAPS = 38,
        ASPECTX = 40,
        ASPECTY = 42,
        ASPECTXY = 44,
        LOGPIXELSX = 88,
        LOGPIXELSY = 90,
        SIZEPALETTE = 104,
        NUMRESERVED = 106,
        COLORRES = 108,
        PHYSICALWIDTH = 110,
        PHYSICALHEIGHT = 111,
        PHYSICALOFFSETX = 112,
        PHYSICALOFFSETY = 113,
        SCALINGFACTORX = 114,
        SCALINGFACTORY = 115,
        VREFRESH = 116,
        DESKTOPVERTRES = 117,
        DESKTOPHORZRES = 118,
        BLTALIGNMENT = 119,
        SHADEBLENDCAPS = 120,
        COLORMGMTCAPS = 121,
    }


    public class MyPrintDocument : PrintDocument
    {
        protected readonly RectangleF printArea;
        protected int wmargin = 0; //mm
        protected int hmargin = 0; //mm

        public MyPrintDocument() : base() { }

        public MyPrintDocument(PageSettings pageSettings, PrinterSettings printerSettings)
        {
            this.DefaultPageSettings = (PageSettings)pageSettings.Clone();
            this.PrinterSettings = (PrinterSettings)printerSettings.Clone();

            /*
            using (var dcap = new PrintUtils.DeviceCaps(this.PrinterSettings))
            {
                this.printArea = dcap.PrintMillimeterArea;
            }
            */

            RectangleF rf = this.DefaultPageSettings.PrintableArea;
            this.printArea = new RectangleF(rf.X * 0.254f, rf.Y * 0.254f, rf.Width * 0.254f, rf.Height * 0.254f);

            this.OriginAtMargins = false;
        }
    }
}
