using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;


namespace BarcodePcApp
    {

    
    class BarcodeLayout
    {
        public string room123="";
        public string room12="";

        static public void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs ppea, Neodynamic.WinControls.BarcodeProfessional.BarcodeProfessional bc, string sCAS, string sSummaformel, string sStreckkod, string comment, string sDatum,string dat,string UtgDatum,string user,string skap,string room)  // gäller även MaxLab
        //static public void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs ppea, Neodynamic.WinControls.BarcodeProfessional.BarcodeProfessional bc, string sCAS, string sSummaformel, string sStreckkod, string comment)
        {
            float fX = (float)Convert.ToDouble(ppea.Graphics.VisibleClipBounds.Width / FormMain.Get.BarcodeWidth);
            float fY = (float)Convert.ToDouble(ppea.Graphics.VisibleClipBounds.Height / FormMain.Get.BarcodeHeight);
            float nLeft = (float)Convert.ToDouble(((ppea.Graphics.VisibleClipBounds.Width - bc.Image.Width) / 2) / fX) - (float)0.5;

            sStreckkod = sStreckkod.ToUpper();
            if (sStreckkod.Contains("FOI"))
            {
                sStreckkod = sStreckkod.Replace("FOI", "");
            }
            else if (sStreckkod.Contains("NPF"))
            {
                sStreckkod = sStreckkod.Replace("NPF", "");
            }

            if (nLeft < 0) nLeft = 0;

            if (FormMain.Get.BarcodeLeft > 0) nLeft = FormMain.Get.BarcodeLeft;

            int nTop = FormMain.Get.BarcodeTop;
            if (sCAS.Length > 0)
                if (nTop < 4)
                    nTop = 4;

            if (FormMain.Get.BarcodeLayout == 0)  //Layout1
            {
                bc.DrawOnCanvas(ppea.Graphics, new PointF(nLeft, nTop));

                StringFormat drawFormat = new StringFormat();
                if (FormMain.Get.BarcodeLeft > 0)
                {
                    drawFormat.Alignment = StringAlignment.Near;
                }
                else
                {
                    nLeft = 0;
                    drawFormat.Alignment = StringAlignment.Center;
                }

                using (Font fnt1 = new Font("Arial", 8f, FontStyle.Regular))
                {
                    if (sCAS.Length > 0)
                    {
                        ppea.Graphics.DrawString(sCAS, fnt1, Brushes.Black, new RectangleF(nLeft * fX, (nTop - 3) * fY, ppea.Graphics.VisibleClipBounds.Width - nLeft * fX, fY * 3), drawFormat);
                    }
                }
            }
            else if (FormMain.Get.BarcodeLayout == 1) //Layout2
            {
                bc.DrawOnCanvas(ppea.Graphics, new PointF(nLeft, nTop));

                StringFormat drawFormat = new StringFormat();
                using (Font fnt1 = new Font("Arial", 6f, FontStyle.Bold))
                {
                    if (sCAS.Length > 0)
                    {
                        drawFormat.Alignment = StringAlignment.Near;
                        ppea.Graphics.DrawString(sSummaformel, fnt1, Brushes.Black, new RectangleF(nLeft * fX, (nTop - 3) * fY + 1, (ppea.Graphics.VisibleClipBounds.Width - nLeft * fX) / 2, fY * 3 + 1), drawFormat);
                        drawFormat.Alignment = StringAlignment.Far;
                        ppea.Graphics.DrawString(sCAS, fnt1, Brushes.Black, new RectangleF(nLeft * fX + (ppea.Graphics.VisibleClipBounds.Width - nLeft * fX) / 2, (nTop - 3) * fY + 1, (ppea.Graphics.VisibleClipBounds.Width - nLeft * fX) / 2, fY * 3 + 1), drawFormat);
                    }
                }
            }
            else if (FormMain.Get.BarcodeLayout == 2)  //Layout3
            {
                bc.DrawOnCanvas(ppea.Graphics, new PointF(nLeft, 4f));

                StringFormat drawFormat = new StringFormat();

                Font fnt2 = new Font("FreesiaUPC", 8f, FontStyle.Bold);
                Font fnt3 = new Font("Arial Narrow", 7f, FontStyle.Bold);

                using (Font fnt1 = new Font("Arial Narrow", 7f, FontStyle.Bold))
                {
                    //ppea.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, (int)ppea.Graphics.VisibleClipBounds.Width, (int)ppea.Graphics.VisibleClipBounds.Height));

                    if (sCAS.Length > 0)
                    {
                        drawFormat.Alignment = StringAlignment.Center;
                        drawFormat.LineAlignment = StringAlignment.Center;
                        /*g.DrawRectangle(Pens.Black, new Rectangle((int)(fX),
                                                                              (int)(fY + fY * 3 + 1 + bc.Image.Height),
                                                                              (int)(ppea.Graphics.VisibleClipBounds.Width - (fX * 2)),
                                                                              (int)(fY * 3 + 1)));*/
                        ppea.Graphics.DrawString(sCAS, fnt1, Brushes.Black, new RectangleF(fX,
                                                                                                   fY + fY * 3 + 1 + bc.Image.Height,
                                                                                                   ppea.Graphics.VisibleClipBounds.Width - (fX * 2),
                                                                                                   fY * 3 + 1), drawFormat);
                    }

                    if (sStreckkod.Length > 0)
                    {
                        drawFormat.Alignment = StringAlignment.Near;
                        drawFormat.LineAlignment = StringAlignment.Far;
                        /*g.DrawRectangle(Pens.Black, new Rectangle((int)(fX),
                                                                              (int)(fY),
                                                                              (int)((ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2),
                                                                              (int)(fY * 3 + 1)));*/
                        ppea.Graphics.DrawString(sStreckkod, fnt1, Brushes.Black, new RectangleF(fX,
                                                                                                 fY,
                                                                                                 (ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2,
                                                                                                 fY * 3 + 1), drawFormat);
                    }

                    if (sSummaformel.Length > 0)
                    {
                        drawFormat.Alignment = StringAlignment.Far;
                        drawFormat.LineAlignment = StringAlignment.Far;
                        /*g.DrawRectangle(Pens.Black, new Rectangle((int)(fX + (ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2),
                                                                              (int)(fY),
                                                                              (int)((ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2),
                                                                              (int)(fY * 3 + 1)));*/
                        ppea.Graphics.DrawString(sSummaformel, fnt1, Brushes.Black, new RectangleF(fX + (ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2,
                                                                                           fY,
                                                                                           (ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2,
                                                                                           fY * 3 + 1), drawFormat);
                    }

                }
            }
            else if (FormMain.Get.BarcodeLayout == 3)  //Layout4
            {
                bc.DrawOnCanvas(ppea.Graphics, new PointF(nLeft, 4f));

                StringFormat drawFormat = new StringFormat();

                Font fnt2 = new Font("FreesiaUPC", 8f, FontStyle.Bold);
                Font fnt3 = new Font("Arial Narrow", 7f, FontStyle.Bold);

                using (Font fnt1 = new Font("Arial Narrow", 7f, FontStyle.Bold))
                {
                    //ppea.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, (int)ppea.Graphics.VisibleClipBounds.Width, (int)ppea.Graphics.VisibleClipBounds.Height));

                    if (sCAS.Length > 0)
                    {
                        drawFormat.Alignment = StringAlignment.Far;
                        drawFormat.LineAlignment = StringAlignment.Near;
                        /*g.DrawRectangle(Pens.Black, new Rectangle((int)(fX),
                                                                              (int)(fY + fY * 3 + 1 + bc.Image.Height),
                                                                              (int)(ppea.Graphics.VisibleClipBounds.Width - (fX * 2)),
                                                                              (int)(fY * 3 + 1)));*/
                        ppea.Graphics.DrawString(sCAS, fnt1, Brushes.Black, new RectangleF(fX, fY + fY * 3 + 1 + bc.Image.Height, ppea.Graphics.VisibleClipBounds.Width - (fX * 2), fY * 3 + 1), drawFormat);
                    }

                    if (sStreckkod.Length > 0)
                    {
                        drawFormat.Alignment = StringAlignment.Near;
                        drawFormat.LineAlignment = StringAlignment.Far;
                        /*g.DrawRectangle(Pens.Black, new Rectangle((int)(fX),
                                                                              (int)(fY),
                                                                              (int)((ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2),
                                                                              (int)(fY * 3 + 1)));*/
                        ppea.Graphics.DrawString(sStreckkod, fnt1, Brushes.Black, new RectangleF(fX, fY, (ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2, fY * 3 + 1), drawFormat);
                    }

                    if (sSummaformel.Length > 0)
                    {
                        drawFormat.Alignment = StringAlignment.Far;
                        drawFormat.LineAlignment = StringAlignment.Far;
                        /*g.DrawRectangle(Pens.Black, new Rectangle((int)(fX + (ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2),
                                                                              (int)(fY),
                                                                              (int)((ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2),
                                                                              (int)(fY * 3 + 1)));*/
                        ppea.Graphics.DrawString(sSummaformel, fnt1, Brushes.Black, new RectangleF(fX + (ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2, fY, (ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2, fY * 3 + 1), drawFormat);
                    }

                    if (comment.Length > 0)
                    {
                        if (comment.Length > 6)
                        {
                            comment = comment.Substring(0, 6);
                        }

                        drawFormat.Alignment = StringAlignment.Near;
                        drawFormat.LineAlignment = StringAlignment.Far;
                        /*g.DrawRectangle(Pens.Black, new Rectangle((int)(fX),
                                                                              (int)(fY + fY * 3 + 1 + bc.Image.Height),
                                                                              (int)(ppea.Graphics.VisibleClipBounds.Width - (fX * 2)),
                                                                              (int)(fY * 3 + 1)));*/
                        ppea.Graphics.DrawString(comment, fnt1, Brushes.Black, new RectangleF(fX, fY + fY * 3 + 1 + bc.Image.Height, ppea.Graphics.VisibleClipBounds.Width - (fX * 2), fY * 3 + 1), drawFormat);
                    }
                }
            }

                //MaxLab
                else if (FormMain.Get.BarcodeLayout == 4)  //Layout5
            {
                bc.DrawOnCanvas(ppea.Graphics, new PointF(nLeft, 4f));

                StringFormat drawFormat = new StringFormat();

                Font fnt2 = new Font("FreesiaUPC", 8f, FontStyle.Bold);
                Font fnt3 = new Font("Arial Narrow", 7f, FontStyle.Bold);

                using (Font fnt1 = new Font("Arial Narrow", 7f, FontStyle.Bold))
                {
                    //ppea.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, (int)ppea.Graphics.VisibleClipBounds.Width, (int)ppea.Graphics.VisibleClipBounds.Height));

                    if (sCAS.Length > 0)
                    {
                        drawFormat.Alignment = StringAlignment.Far;
                        drawFormat.LineAlignment = StringAlignment.Near;
                        /*g.DrawRectangle(Pens.Black, new Rectangle((int)(fX),
                                                                              (int)(fY + fY * 3 + 1 + bc.Image.Height),
                                                                              (int)(ppea.Graphics.VisibleClipBounds.Width - (fX * 2)),
                                                                              (int)(fY * 3 + 1)));*/
                        ppea.Graphics.DrawString(sCAS, fnt1, Brushes.Black, new RectangleF(fX, fY + fY * 3 + 1 + bc.Image.Height, ppea.Graphics.VisibleClipBounds.Width - (fX * 2), fY * 3 + 1), drawFormat);
                    }

                    if (sStreckkod.Length > 0)
                    {
                        drawFormat.Alignment = StringAlignment.Near;
                        drawFormat.LineAlignment = StringAlignment.Far;
                        /*g.DrawRectangle(Pens.Black, new Rectangle((int)(fX),
                                                                              (int)(fY),
                                                                              (int)((ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2),
                                                                              (int)(fY * 3 + 1)));*/
                        ppea.Graphics.DrawString(sStreckkod, fnt1, Brushes.Black, new RectangleF(fX, fY, (ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2, fY * 3 + 1), drawFormat);
                    }

                    if (sDatum.Length > 0)
                    {
                        drawFormat.Alignment = StringAlignment.Far;
                        drawFormat.LineAlignment = StringAlignment.Far;
                        /*g.DrawRectangle(Pens.Black, new Rectangle((int)(fX + (ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2),
                                                                              (int)(fY),
                                                                              (int)((ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2),
                                                                              (int)(fY * 3 + 1)));*/
                        ppea.Graphics.DrawString(sDatum, fnt1, Brushes.Black, new RectangleF(fX + (ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2, fY, (ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2, fY * 3 + 1), drawFormat);
                    }

                    if (comment.Length > 0)
                    {
                        if (comment.Length > 6)
                        {
                            comment = comment.Substring(0, 6);
                        }

                        drawFormat.Alignment = StringAlignment.Near;
                        drawFormat.LineAlignment = StringAlignment.Far;
                        /*g.DrawRectangle(Pens.Black, new Rectangle((int)(fX),
                                                                              (int)(fY + fY * 3 + 1 + bc.Image.Height),
                                                                              (int)(ppea.Graphics.VisibleClipBounds.Width - (fX * 2)),
                                                                              (int)(fY * 3 + 1)));*/
                        ppea.Graphics.DrawString(comment, fnt1, Brushes.Black, new RectangleF(fX, fY + fY * 3 + 1 + bc.Image.Height, ppea.Graphics.VisibleClipBounds.Width - (fX * 2), fY * 3 + 1), drawFormat);
                    }
                }
            }

             //Ny layout
            else if (FormMain.Get.BarcodeLayout == 5)  //Layout6
            {                
                bc.DrawOnCanvas(ppea.Graphics, new PointF(nLeft, 4f));

                StringFormat drawFormat = new StringFormat();

                Font fnt2 = new Font("FreesiaUPC", 8f, FontStyle.Bold);
                Font fnt3 = new Font("Arial Narrow", 7f, FontStyle.Bold);

                using (Font fnt1 = new Font("Arial Narrow", 7f, FontStyle.Bold))
                {
                    string room123;
                    if (skap == "0" || skap=="")
                    {
                        string room12 = room;
                        room123 = room;
                    }
                    else
                    {
                        string room12 = room + "/" + skap;
                        room123 = room12;
                    }
                    string room1234 = room123;
                    Size sizeOfString = new Size();
                    sizeOfString = TextRenderer.MeasureText(room1234, fnt1); //storlek på rum och skåp
                    Size sizeOfString1 = new Size();
                    sizeOfString1 = TextRenderer.MeasureText(skap, fnt1);   //storlek på skåp
                    Size sizeOfString2 = new Size();
                    sizeOfString2 = TextRenderer.MeasureText(room, fnt1);   //storlek på rum

                    if (sizeOfString.Width > 70)
                    {
                        //if (skap != "0" && sizeOfString1.Width<60)
                        if (skap != "0" && skap!="")
                        {

                            if (sizeOfString1.Width > 60)
                            {
                                string kortnamn = skap.Substring(0, 10);
                                room = kortnamn;
                            }
                            else
                            {
                                string rum = skap;
                                room = rum;
                            }
                        }
                        else
                        {
                            string kortnamn = room.Substring(0, 10);            
                            room = kortnamn;
                        }
                    }
                    else
                    {
                        if (room != "" && (skap != "0" && skap!=""))
                        {
                            string rum = room + "/" + skap;
                            room = rum;

                        }
                        else
                        {
                            if (skap != "0")
                            {
                                string rum = skap;
                                room = rum;

                            }
                            else {
                                string rum = room;
                                room = rum;
                            }

                        }

                    }

                    if (room.Length > 0)// rum+skåp
                    {
                        drawFormat.Alignment = StringAlignment.Far;
                        drawFormat.LineAlignment = StringAlignment.Near;
                        /*g.DrawRectangle(Pens.Black, new Rectangle((int)(fX),
                                                                              (int)(fY + fY * 3 + 1 + bc.Image.Height),
                                                                              (int)(ppea.Graphics.VisibleClipBounds.Width - (fX * 2)),
                                                                              (int)(fY * 3 + 1)));*/
                        ppea.Graphics.DrawString(room, fnt1, Brushes.Black, new RectangleF(fX, fY + fY * 3 + 1 + bc.Image.Height, ppea.Graphics.VisibleClipBounds.Width - (fX * 2), fY * 3 + 1), drawFormat);
                    }

                    if (dat.Length > 0) // användare/skapat datum
                    {
                        drawFormat.Alignment = StringAlignment.Near;
                        drawFormat.LineAlignment = StringAlignment.Far;
                        /*g.DrawRectangle(Pens.Black, new Rectangle((int)(fX),
                                                                              (int)(fY),
                                                                              (int)((ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2),
                                                                              (int)(fY * 3 + 1)));*/

                        ppea.Graphics.DrawString(dat, fnt1, Brushes.Black, new RectangleF(fX, fY, (ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2, fY * 3 + 1), drawFormat);
                    }

                    if (sStreckkod.Length > 0)
                    {
                        drawFormat.Alignment = StringAlignment.Far;
                        drawFormat.LineAlignment = StringAlignment.Far;
                        /*g.DrawRectangle(Pens.Black, new Rectangle((int)(fX + (ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2),
                                                                              (int)(fY),
                                                                              (int)((ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2),
                                                                              (int)(fY * 3 + 1)));*/
                        ppea.Graphics.DrawString(sStreckkod, fnt1, Brushes.Black, new RectangleF(fX + (ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2, fY, (ppea.Graphics.VisibleClipBounds.Width - fX * 2) / 2, fY * 3 + 1), drawFormat);
                    }

                    if (UtgDatum.Length > 0)
                    {

                        drawFormat.Alignment = StringAlignment.Near;
                        drawFormat.LineAlignment = StringAlignment.Far;
                        /*g.DrawRectangle(Pens.Black, new Rectangle((int)(fX),
                                                                              (int)(fY + fY * 3 + 1 + bc.Image.Height),
                                                                              (int)(ppea.Graphics.VisibleClipBounds.Width - (fX * 2)),
                                                                              (int)(fY * 3 + 1)));*/
                        ppea.Graphics.DrawString(UtgDatum, fnt1, Brushes.Black, new RectangleF(fX, fY + fY * 3 + 1 + bc.Image.Height, ppea.Graphics.VisibleClipBounds.Width - (fX * 2), fY * 3 + 1), drawFormat);
                    }
                }
            }
          }

        private static void elseif(bool p)
        {
            throw new NotImplementedException();
        }
        }
    }

