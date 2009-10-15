﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;

using kaede2nd.Entity;
using System.Windows.Forms;

namespace kaede2nd
{

    public class ItemsPrintDocument : PrintUtils.MyPrintDocument
    {

        public static void PrintItems(List<Item> items)
        {
            if (items == null) { throw new NullReferenceException("items"); }
            if (items.Count == 0) { return; }

            PrintDialog prid = new PrintDialog();
            prid.PrinterSettings = GlobalData.Instance.printerSettings;
            prid.UseEXDialog = true;
            prid.Document = new ItemsPrintDocument(items, GlobalData.Instance.pageSettings, GlobalData.Instance.printerSettings);
            DialogResult pdres = prid.ShowDialog();

            if (pdres != DialogResult.OK) { return; }

            prid.Document.Print();

            /*
            PrintPreviewDialog pprediag = new PrintPreviewDialog();
            pprediag.Document = new ItemsPrintDocument(items, GlobalData.Instance.pageSettings, GlobalData.Instance.printerSettings);

            try
            {
                pprediag.ShowDialog();
            }
            catch (Exception e)
            {
                MessageBox.Show("印刷が実行できませんでした: " + e.ToString());
            }
            */
        }

        public class PrintItem
        {
            public Item item;
            public uint volumeNum; /* 0...分冊なし 1...分冊あり、1冊目 2以降...分冊あり、n冊目 */

            public PrintItem(Item item, uint volumenum)
            {
                this.item = item;
                this.volumeNum = volumenum;
            }
        }

        private readonly List<PrintItem> pitems;
        
        private const int cellwidth = 90; //mm
        private const int cellheight = 36; //mm

        private DotNetBarcode barcode;

        public ItemsPrintDocument(List<Item> items, PageSettings pageSettings, PrinterSettings printerSettings)
            : base(pageSettings, printerSettings)
        {

            this.barcode = new DotNetBarcode(DotNetBarcode.Types.Jan8);
            this.barcode.PrintChar = false;
            this.barcode.AddChechDigit = true;

            //this.items = items;
            this.pitems = new List<PrintItem>();
            foreach (Item i in items)
            {
                if (i.item_volumes.HasValue && i.item_volumes.Value != 0)
                {
                    for (uint vn = 1; vn <= i.item_volumes.Value; vn++)
                    {
                        this.pitems.Add(new PrintItem(i, vn));
                    }
                }
                else
                {
                    this.pitems.Add(new PrintItem(i, 0));
                }
            }

            this.BeginPrint += new PrintEventHandler(ItemsPrintDocument_BeginPrint);
            this.PrintPage += new PrintPageEventHandler(ItemsPrintDocument_PrintPage);
        }

        private int colpp;
        private int rowpp;

        private int count;

        void ItemsPrintDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            colpp = (int)((this.printArea.Width) / cellwidth);
            rowpp = (int)((this.printArea.Height) / cellheight);

            double allpages = Math.Ceiling((double)this.pitems.Count / (colpp * rowpp));

            this.DocumentName = GlobalData.Instance.bumonName + " 全 " + ((int)allpages).ToString() + " ページ";

            this.count = 0;
            
        }

        void ItemsPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Millimeter;

            Pen cellLine = new Pen(Color.Black, 0.2f);
            //縦線
            for (int i = 1; i <= colpp; i++)
            {
                g.DrawLine(cellLine, new Point(wmargin + cellwidth * i, hmargin + 0), new Point(wmargin + cellwidth * i, hmargin + (int)this.printArea.Height));
            }

            //横線
            for (int i = 1; i <= rowpp; i++)
            {
                g.DrawLine(cellLine, new Point(wmargin + 0, hmargin + cellheight * i), new Point(wmargin + (int)this.printArea.Width, hmargin + cellheight * i));
            }

            for (int tate = 0; tate < rowpp; tate++)
            {
                for (int yoko = 0; yoko < colpp; yoko++)
                {
                    this.DrawItem(this.pitems[count], e, wmargin + cellwidth * yoko, hmargin + cellheight * tate);

                    if (this.pitems.Count - 1 <= count)
                    {
                        e.HasMorePages = false;
                        return;
                    }
                    count++;
                }
            }


            e.HasMorePages = true;
        }


        /* volumeNum: 0...分冊なし 1...分冊あり、1冊目 2以降...分冊あり、n冊目 */
        private void DrawItem(PrintItem printit, PrintPageEventArgs e, int x, int y)
        {

            Item it = printit.item;

            bool isan = false;

            e.Graphics.Clip = new Region(new Rectangle(x, y, cellwidth, cellheight));
            Font fnt = new Font("MS Gothic", 3.5f, FontStyle.Regular, GraphicsUnit.Millimeter);

            if (isan)
            {
                e.Graphics.DrawString("10円", new Font("MS Gothic", 10.0f, FontStyle.Regular, GraphicsUnit.Millimeter),
                    Brushes.Black, x + 5, y + 3.5f);

                e.Graphics.DrawString("値引: ○", fnt, Brushes.Black, x + 50, y + 3.5f + (5.25f) * 2);

            }
            else
            {



                //e.Graphics.DrawString("*-*-*-`-" + it.item_id.ToString("0000"), fnt, Brushes.Black, x + 2, y + 3.5f);
                e.Graphics.DrawString("品名: " + it.item_name, fnt, Brushes.Black, x + 2, y + 3.5f + (5.25f) * 0);
                e.Graphics.DrawString("コメント: " + it.item_comment, fnt, Brushes.Black, x + 2, y + 3.5f + (5.25f) * 1);

                e.Graphics.DrawString("価格: " + it.item_tagprice.ToString("#,##0") + "円",
                    fnt, Brushes.Black, x + 2, y + 3.5f + (5.25f) * 2);


                /*
                if (!it.item_tataki.HasValue)
                {
                    e.Graphics.DrawString("値引: ？", fnt, new SolidBrush(Color.Cyan), x + 30, y + 3.5f + (5.25f) * 2);
                }
                else */
                if (it.item_return/*.Value*/ == false)
                {
                    e.Graphics.DrawString("値引: ○", fnt, Brushes.Black, x + 30, y + 3.5f + (5.25f) * 2);
                }
                else
                {
                    e.Graphics.DrawString("値引: ×", fnt, new SolidBrush(Color.Magenta), x + 30, y + 3.5f + (5.25f) * 2);
                }

                e.Graphics.DrawString("品番: " + it.item_id.ToString("00000"), fnt, Brushes.Black, x + 2, y + 3.5f + (5.25f) * 3 + 10.0f);


                string s = "";

                switch (it.item__Receipt.receipt_seller)
                {
                    case kaede2nd.Entity.Receipt.seller_EXT:
                        {
                            s += "EX";
                            break;
                        }
                    case kaede2nd.Entity.Receipt.seller_LAGACY:
                        {
                            s += "LG";
                            break;
                        }
                    case kaede2nd.Entity.Receipt.seller_DONATE:
                        {
                            s += "DN";
                            break;
                        }
                    default:
                        {
                            string kumi = it.item__Receipt.receipt_seller.Substring(1, 1);

                            if (Globals.isChugaku(kumi)) { s += "C"; }
                            else { s += "K"; }

                            s += it.item__Receipt.receipt_seller.Substring(0, 1);

                            break;
                        }
                }

                s += it.item_receipt_id.ToString("'-R'0000");


                e.Graphics.DrawString(s, new Font("MS Gothic", 3.0f, FontStyle.Italic, GraphicsUnit.Millimeter),
                    Brushes.Black, x + 30, y + 3.5f + (5.25f) * 3 + 10.0f + 0.4f);

            }


            e.Graphics.DrawString("筑駒58期縁日班" + GlobalData.Instance.bumonName, new Font("MS Mincho", 2.2f, FontStyle.Regular, GraphicsUnit.Millimeter),
                Brushes.Black, x + 46, y + 3.5f + (5.25f) * 3 + 10.9f);


            if (printit.volumeNum < 2)
            {

                int barcodesize = 0;
                if (printit.volumeNum == 1)
                {
                    e.Graphics.DrawString("分売不可 1 / " + it.item_volumes.ToString(), fnt, Brushes.Red, x + 50, y + 3.5f + (5.25f) * 2);
                    barcodesize = 10;
                }
                else
                {
                    barcodesize = 12;
                }

                //バーコード
                this.barcode.WriteBar(GlobalData.Instance.barcodePrefix + it.item_id.ToString("00000"), x + 50, y + (29 - barcodesize), 25, barcodesize, e.Graphics);


                //販売価格
                Pen nedanPen = new Pen(Color.Black, 0.2f);
                nedanPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                nedanPen.DashOffset = 1.0f;
                e.Graphics.DrawLine(nedanPen, new PointF(x + 3.0f, y + 27.0f), new PointF(x + 35.0f, y + 27.0f));
                e.Graphics.DrawString("円", new Font("MS Gothic", 5.5f, FontStyle.Regular, GraphicsUnit.Millimeter),
                               Brushes.Black, x + 36.0f, y + 21.5f);
                e.Graphics.DrawString("販売価格", new Font("MS Gothic", 2.0f, FontStyle.Regular, GraphicsUnit.Millimeter), Brushes.Black, x + 3, y + 3.5f + (5.25f) * 3);
            }
            else
            {
                e.Graphics.DrawString("分売不可  " + printit.volumeNum.ToString() + " / " + it.item_volumes.ToString(),
                    new Font("MS Gothic", 5.5f, FontStyle.Regular, GraphicsUnit.Millimeter), Brushes.Red, x + 19.0f, y + 20.25f);
            }
        }
    }



    public class ReceiptPrintDocument : PrintUtils.MyPrintDocument
    {
        private readonly Receipt receipt;
        private readonly List<Item> items;

        private const int rowheight = 6; //mm
        private const float fontheight = 4f; //mm

        public ReceiptPrintDocument(Receipt receipt, PageSettings pageSettings, PrinterSettings printerSettings)
            : base(pageSettings, printerSettings)
        {

            if (receipt == null) { throw new NullReferenceException("receipt"); }

            this.receipt = receipt;
            var itemDao = GlobalData.getIDao<kaede2nd.Dao.IItemDao>();
            items = itemDao.GetReceiptItem(this.receipt.receipt_id);
            

            this.BeginPrint += new PrintEventHandler(ReceiptPrintDocument_BeginPrint);
            this.PrintPage += new PrintPageEventHandler(ReceiptPrintDocument_PrintPage);
        }


        private int itemperpage = 1;
        private int allpages = 1;
        private int count = 0;
        private int pagecount = 1;

        private int tableWidth;

        void ReceiptPrintDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            if (items.Count == 0) { e.Cancel = true; }
            itemperpage = (int)((this.printArea.Height - 80) / rowheight);
            allpages = (int)(items.Count / itemperpage) + 1;

            tableWidth = ((int)this.printArea.Width - 10) - (wmargin + 30);

            this.DocumentName = GlobalData.Instance.bumonName + " 全 " + allpages.ToString() + " ページ";

            this.pagecount = 1;
            this.count = 0;
        }

        void ReceiptPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Millimeter;

            g.DrawString("出品票",
                new Font("MS Gothic", 8.0f, FontStyle.Regular, GraphicsUnit.Millimeter), Brushes.Black,
                new PointF(wmargin + 15, 7));

            g.DrawString("(" + GlobalData.Instance.bumonName + ") " + this.pagecount.ToString() + " / " + allpages.ToString() + " ページ",
                new Font("MS Gothic", 6.0f, FontStyle.Regular, GraphicsUnit.Millimeter), Brushes.Black,
                new PointF(wmargin + 15 + 35, 9));

            Font font = new Font("MS Gothic", fontheight, FontStyle.Regular, GraphicsUnit.Millimeter);

            g.DrawString("出品者: " + this.receipt.getSellerString(),
                new Font("MS Gothic", 6.0f, FontStyle.Regular, GraphicsUnit.Millimeter)
                , Brushes.Black, new PointF(wmargin + 20, 20));

            g.DrawString(this.receipt.receipt_id.ToString("'R'0000"),
                new Font("MS Gothic", 10.0f, FontStyle.Regular, GraphicsUnit.Millimeter), Brushes.Black,
                new PointF(this.printArea.Width - 40, this.printArea.Height - 25)
            );

            Pen pline = new Pen(Color.Black, 0.1f);

            for (int i = 0; i <= itemperpage; i++)
            {
                int y = hmargin + 40 + rowheight * i;
                g.DrawLine(pline, new Point(wmargin + 30, y), new Point(wmargin + 30 + tableWidth, y));
            }

            Region infClip = g.Clip;

            for (int i = 0; i < itemperpage; i++)
            {
                Item it = this.items[count];
                this.count++;

                int x = wmargin + 30;
                int y = hmargin + 40 + rowheight * i + 1;

                g.DrawString(String.Format("{0,2}", count) + ".", font, Brushes.Black, new PointF(x + 2, y));

                g.SetClip(new Rectangle(x + 10, y, tableWidth - 36 - 10, rowheight));
                g.DrawString(it.item_name, font, Brushes.Black, new PointF(x + 10, y));
                g.Clip = infClip;

                g.DrawString(it.item_tagprice.ToString("#,##0").PadLeft(6, ' ') + "円", font, Brushes.Black, new PointF(x + tableWidth - 34, y));
                if (it.item_return == true)
                {
                    g.DrawString("返品", font, Brushes.Black, new PointF(x + tableWidth - 14, y));
                }

                if (this.count >= this.items.Count)
                {
                    e.HasMorePages = false;
                    return;
                }
            }

            this.pagecount++;
            e.HasMorePages = true;
        }

    }
}
