using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using BarcodePcApp.Misc;
using System.Drawing.Printing;
using System.Timers;
using NsExcel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;



namespace BarcodePcApp
{
    public partial class FormPrintBarcodes : Form
    {
        private string m_sCAS = "";
        private string m_sStreckkod = "";
        private string m_sSummaformel = "";
        private string m_sComment = "";
        private System.Timers.Timer scannerTim = new System.Timers.Timer();
        private string barcodeInSettings = "";
        private bool Log;
        private string databas;
        public int Aret;
        public string datum_max = "";
        public string Plats = "";
        //ändring 151016
        public string Rum = "";
        public string Lada = "";
        public string Skap = "";
        public string Utgangsdatum = "";
        public string Separator = " / ";
        public string m_sRoom = "";
        public string m_sCabinet = "";
        private string m_sRumNamn = "";
        private string m_sSkapat_datum = "";
        private string m_sUtg_datum = "";
        public string datum_m = "";
        public string init = "";
        public string printBC= "";


        private List<printBarcodeItem> listOfProducts;

        public FormPrintBarcodes(int orgID, int lokalID, int cabinetID, int orgAr, string nodeDB)
        {
            InitializeComponent();
            //Sandi 
            scannerTim.Interval =100;
            scannerTim.Elapsed += scannerTim_Elapsed;
            //buttonCheckIn.Visible = false;
            this.KeyPress += new KeyPressEventHandler(form_KeyPress);
            FormSettings form = new FormSettings();
            barcodeInSettings = form.loginBarcode;

            //Skapa initialer
            string name123 = FormMain.Get.Usernamestring;
            Regex initials = new Regex(@"(\b[a-öA-Ö])[a-öA-Ö]* ?");
            init = initials.Replace(name123, "$1");

            Log = FormMain.Get.PropertyCheck;

            listOfProducts = GetList(orgID, lokalID, cabinetID, orgAr, nodeDB);

            contentPanel.Controls.AddRange(listOfProducts.ToArray());
        }

      
        public List<printBarcodeItem> GetList(int orgID, int lokalID, int cabinetID, int orgAr, string nodeDB)
        {
            var printList = new List<printBarcodeItem>();
            Aret = orgAr;

            string inventoryList = "";
            barcode.BarcodeService bc = FormMain.getBarcodeService();
            double AR =Convert.ToDouble(bc.GetAr(FormMain.Get.Databas, FormMain.Get.VerkId));
            string release = bc.ReleaseRules(FormMain.Get.Databas, orgID,AR);
            string m_sRelease = release;
            try
            {
                inventoryList = bc.GetPrintingList(FormMain.Get.Databas, FormMain.Get.KemiDB, nodeDB, orgID, lokalID, cabinetID, orgAr,m_sRelease);
                DataSet ds = new DataSet();
                Plats = bc.GetStorageName(FormMain.Get.Databas, FormMain.Get.KemiDB, nodeDB, orgID, lokalID, cabinetID, orgAr);
                StringReader stri = new StringReader(Plats);
                ds.ReadXml(stri);
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];
                Rum = dr["StorageName"].ToString();
                Skap = dr["Skap"].ToString();
                Lada = dr["Lada"].ToString();


                

            }
            catch (Exception err)
            {
                bc.Dispose();
            }
            
            bc.Dispose();

            if (inventoryList != "")
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(inventoryList);
                

                XmlNodeList xNodeList = xDoc.SelectNodes("//item");
                foreach (XmlNode xNode in xNodeList)
                {
                    //test try-catch
                    try
                    {

                        string namn = xNode["namn"].InnerText;
                        string am = xNode["amount"].InnerText;

                        if (namn.Length > 20)
                        {
                            // Förkorta namnet om längre än 20 tecken
                            string kortnamn = xNode["namn"].InnerText.Substring(0, 20);
                            xNode["namn"].InnerText = kortnamn;
                            printBarcodeItem item = new printBarcodeItem(xNode["namn"].InnerText, xNode["barcode"].InnerText, (xNode["amount"].InnerText.Replace('.', ',')), xNode["unit"].InnerText.Trim(), xNode["kommentar"].InnerText, xNode["Cas"].InnerText, xNode["Kemiskbet"].InnerText.Trim(), xNode["lokaldb"].InnerText.Trim(), Convert.ToInt32(xNode["transid"].InnerText), Convert.ToInt32(xNode["prodid"].InnerText), xNode["Leverantor"].InnerText);
                            printList.Add(item);

                        }
                        else
                        {

                            //printBarcodeItem item = new printBarcodeItem(xNode["namn"].InnerText, xNode["barcode"].InnerText, Convert.ToDouble(xNode["amount"].InnerText.Replace('.', ',')), xNode["unit"].InnerText.Trim(), xNode["kommentar"].InnerText, xNode["Cas"].InnerText.Trim(), xNode["Kemiskbet"].InnerText.Trim(), xNode["lokaldb"].InnerText.Trim(), Convert.ToInt32(xNode["transid"].InnerText), Convert.ToInt32(xNode["prodid"].InnerText));
                            //printBarcodeItem item = new printBarcodeItem(xNode["namn"].InnerText, xNode["barcode"].InnerText, Convert.ToDouble(xNode["amount"].InnerText.Replace('.', ',')), xNode["unit"].InnerText.Trim(), xNode["kommentar"].InnerText, xNode["Cas"].InnerText.Trim(), xNode["Kemiskbet"].InnerText.Trim(), xNode["lokaldb"].InnerText.Trim(), Convert.ToInt32(xNode["transid"].InnerText), Convert.ToInt32(xNode["prodid"].InnerText));
                            printBarcodeItem item = new printBarcodeItem(xNode["namn"].InnerText, xNode["barcode"].InnerText, (xNode["amount"].InnerText.Replace('.', ',')), xNode["unit"].InnerText.Trim(), xNode["kommentar"].InnerText, xNode["Cas"].InnerText, xNode["Kemiskbet"].InnerText.Trim(), xNode["lokaldb"].InnerText.Trim(), Convert.ToInt32(xNode["transid"].InnerText), Convert.ToInt32(xNode["prodid"].InnerText), xNode["Leverantor"].InnerText);
                           
                            printList.Add(item);
                        }
                    }
                    catch (Exception err)
                    {
                        bc.Dispose();
                    }

                }

                
            }
            

            return printList;
            
        }

        private void printCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (printBarcodeItem item in listOfProducts)
            {
                item.print = printCheckBox.Checked;
                

            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }


        private void PrintBarcode(string pCAS, string pSummaformel, string pStreckkod, string comment)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = "Fel";

            foreach (string prt in PrinterSettings.InstalledPrinters)
            {	// Installerade skrivare
                if (prt.StartsWith(FormMain.Get.BarcodePrinter, true, null))
                {
                    pd.PrinterSettings.PrinterName = prt;
                    break;
                }
            }
            if (!pd.PrinterSettings.IsValid)
            {
                MessageBox.Show("No label printer is installed");
                return;
            }

            pd.DocumentName = "Barcode for the product";
            pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocumentOnPrintPage);

            DateTime currentDate = DateTime.Now;
            DateTime d = (DateTime)currentDate;
            datum_max = d.ToString("yy-MM-dd",System.Globalization.CultureInfo.InvariantCulture);
            datum_m = init + "/" + d.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

            barcode.BarcodeService bc1 = FormMain.getBarcodeService();
            printBC = bc1.GetPrintBF(FormMain.Get.Databas, FormMain.Get.KemiDB, pStreckkod);
            DataSet ds1 = new DataSet();
            StringReader st = new StringReader(printBC);
            ds1.ReadXml(st);
            DataTable dt1 = ds1.Tables[0];
            DataRow dr1 = dt1.Rows[0];
            Utgangsdatum = dr1["bastfore"].ToString();

            if (FormMain.Get.BarcodeLayout == 0)
            {
                m_sComment = "";
                m_sSummaformel = "";
                if (pCAS.Length > 0)
                    m_sCAS = "CAS: " + pCAS.Trim();
                else
                    m_sCAS = "";
                barcodeObject.DisplayCode = true;
            }
            else if (FormMain.Get.BarcodeLayout == 1)
            {
                m_sComment = "";
                m_sSummaformel = pSummaformel;
                if (pCAS.Length > 0)
                    m_sCAS = pCAS.Trim();
                else
                    m_sCAS = "";

                barcodeObject.Text = "";
                barcodeObject.BarHeight = 4;
                barcodeObject.DisplayCode = true;
            }
            else if (FormMain.Get.BarcodeLayout == 2)
            {
                m_sComment = "";
                m_sSummaformel = pSummaformel;
                if (pCAS.Length > 0)
                    m_sCAS = pCAS.Trim();
                else
                    m_sCAS = "";

                barcodeObject.Text = "";
                barcodeObject.BarHeight = 4;
                barcodeObject.DisplayCode = false;
                barcodeObject.CodeAlignment = Neodynamic.WinControls.BarcodeProfessional.Alignment.AboveLeft;

                //bc.Text = pSummaformel;
                //bc.TextAlignment = Neodynamic.WinControls.BarcodeProfessional.Alignment.BelowCenter;
            }
            else if (FormMain.Get.BarcodeLayout == 3)
            {
                if (comment.Length >= 6)
                {
                    m_sComment = comment.Substring(0, 6);
                }
                else
                {
                    m_sComment = comment;
                }
                
                m_sSummaformel = pSummaformel;
                if (pCAS.Length > 0)
                    m_sCAS = pCAS.Trim();
                else
                    m_sCAS = "";

                barcodeObject.Text = "";
                barcodeObject.BarHeight = 4;
                barcodeObject.DisplayCode = false;
                barcodeObject.CodeAlignment = Neodynamic.WinControls.BarcodeProfessional.Alignment.AboveLeft;
            }            

            ////Gäller för MaxLab
            else if (FormMain.Get.BarcodeLayout == 4)
            {
                m_sStreckkod = pStreckkod;
                m_sSummaformel = datum_max;

                if (pCAS.Length > 0)
                    m_sCAS = pCAS.Trim();
                else
                    m_sCAS = "";

                if (comment.Length >= 6)
                {
                    m_sComment = comment.Substring(0, 6);
                }
                else
                {
                    m_sComment = comment;
                }

                barcodeObject.Text = "";
                barcodeObject.BarHeight = 4;
                barcodeObject.DisplayCode = false;
            }

            else if (FormMain.Get.BarcodeLayout == 5)
            {
                if (comment.Length >= 6)
                {
                    m_sComment = comment.Substring(0, 6);
                }
                else
                {
                    m_sComment = comment;
                }

                if (pCAS.Length > 0)
                    m_sCAS = pCAS.Trim();
                else
                    m_sCAS = "";

                m_sSkapat_datum = datum_m;
                m_sRumNamn = Rum;
                m_sCabinet = Skap;
                m_sUtg_datum = Utgangsdatum;
                //m_sCabinet = comboBoxCabinet.Text; //hylla
                //m_sRumNamn = comboBoxRoom.Text; //Rum   
                //m_sUtgangsdatum = txtBestBefore.Text; // Utgångsdatum

                barcodeObject.Text = "";
                barcodeObject.BarHeight = 4;
                barcodeObject.DisplayCode = false;
                barcodeObject.CodeAlignment = Neodynamic.WinControls.BarcodeProfessional.Alignment.AboveLeft;
            }

            m_sStreckkod = pStreckkod;
            barcodeObject.Code = pStreckkod;
            pd.Print();
            
        }

        private void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs ppea)
        {
            //TODO: add proper note
            BarcodeLayout.PrintDocumentOnPrintPage(sender, ppea, barcodeObject, m_sCAS, m_sSummaformel, m_sStreckkod, m_sComment, datum_max, m_sSkapat_datum, m_sUtg_datum, FormMain.Get.Username, m_sCabinet, m_sRumNamn);

        }


        private string createBarcode(printBarcodeItem item)
        {
            string newBarcode = "";
            barcode.BarcodeService bc = FormMain.getBarcodeService();

            try
            {
                //MessageBox.Show("db: " + a + ", transID: " + b + ", verk: " + c + ", user: " + d + ", Username: " + e + ", prodID: " + f + ", amount: " + g + ", unit: " + h);
                newBarcode = bc.AssignBarcode(FormMain.Get.Databas,item.lokalDB, item.transitionID, FormMain.Get.VerkId, FormMain.Get.UserId, FormMain.Get.Username, item.productID, item.amount, item.unit);
            }
            catch (Exception err)
            {
                //bc.Dispose();
            }

            bc.Dispose();

            return newBarcode;
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            //Print selected barcodes
            
            foreach (printBarcodeItem item in listOfProducts)
            {
                if (item.print)
                {
                    if (item.barcode == "")
                    {
                        item.barcode = createBarcode(item);
                    }
                    if (item.CAS == "")
                    {
                        item.CAS = "-";
                    }

                    printBarcodeItem prt = new printBarcodeItem();
                    prt.note = item.note;


                    if (item.note !="")
                    {
                        item.note = prt.note;
                    }
                    
                    PrintBarcode(item.CAS, item.summaFormel, item.barcode, prt.note);
                    //Uppdatera KPIinventory med ändrad kommentar
                    SaveComment(FormMain.Get.Databas,item.lokalDB, item.barcode, item.note,Aret);
                } 
               

            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            //this.Close();
        }  
  
        //Sandi - spara ändrade kommentarer
        private void SaveComment(string databas, string lokaldb, string barcode,string comment, int ar)  
        {
            string newComment = "";
            barcode.BarcodeService bc = FormMain.getBarcodeService();
            try
            {
                
                   newComment = bc.SaveNewComment(databas,lokaldb,barcode,comment,ar);
            }
            catch (Exception err)
            {
             //bc.Dispose();
            }

        bc.Dispose();

        return;
        }


        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            if (Log == true)
            {
                if (txtBarcode.Text != "")
                {
                    if (txtBarcode.Text == barcodeInSettings)
                    {
                        this.Invoke(new MethodInvoker(delegate { FormMain.Get.Logout(); }));
                        //labelShowProduct.Text = textBoxScanCode.Text;
                        //this.Close();
                        //this.Owner.Close();
                    }
                    //labelShowProduct.Text = textBoxScanCode.Text;

                }
            }
        }

        // Check if user want to use scan 
        private void chkUser_CheckedChanged(object sender, EventArgs e)
        {
            txtBarcode.Clear();
            if (chkUser.CheckState == CheckState.Checked)
            {
                panel1.Visible = true;
                //buttonCheckIn.Visible = false;
                txtBarcode.Enabled = true;
                txtBarcode.Focus();
            }
            else
            {
                panel1.Visible = false;
                //buttonCheckIn.Visible = true;
                txtBarcode.Focus();
                txtBarcode.Enabled = true;
            }
        }

        private string checkinbarcode = "";

        private void scannerTim_Elapsed(object source, ElapsedEventArgs e)
        {

            //scannerTimer.Enabled = false;
            scannerTim.Stop();
            getUser(checkinbarcode);

            checkinbarcode = "";
        }

        private void getUser(string streckkod)
        {
            // Hämta användare med den inskannade streckkoden:
            barcode.BarcodeService bc = FormMain.getBarcodeService();

            string user = "";

            try
            {
                user = bc.GetUserNameAndPasswordWithBarcode(databas, streckkod);
                if (user == "")
                {
                    MessageBox.Show("This is not KLARA user");
                    bc.Dispose();
                    return;

                }
                else
                {
                    this.Close();
                    this.Owner.Close();

                }
            }
            catch (Exception err)
            {
                System.Media.SystemSounds.Hand.Play();
                MessageBox.Show("No contact with the web server!\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "GetCheckedInProd");
                bc.Dispose();
                return;
            }

        }

        private void form_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Log == false)
            {


            }
            else if (((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 65 && e.KeyChar <= 122)) && chkUser.Checked)
            {
                //scannerTimer.Enabled = false;
                scannerTim.Stop();
                checkinbarcode += e.KeyChar;

                //scannerTimer.Enabled = true;
                scannerTim.Start();
            }
            else
            {
            }
        }

        private void printGenerateButton_Click(object sender, EventArgs e)
        {
           //Print selected barcodes

            foreach (printBarcodeItem item in listOfProducts)
            {
                if (item.print)
                {
                    if (item.barcode == "")
                    {
                        item.barcode = createBarcode(item);
                    }
                    if (item.CAS == "")
                    {
                        item.CAS = "-";
                    }

                    printBarcodeItem prt = new printBarcodeItem();
                    prt.note = item.note;


                    if (item.note != "")
                    {
                        item.note = prt.note;
                    }


                    //PrintBarcode(item.CAS, item.summaFormel, item.barcode, item.note);
                    PrintBarcode(item.CAS, item.summaFormel, item.barcode, prt.note);

                    //Uppdatera KPIinventory med ändrad kommentar
                    SaveComment( FormMain.Get.Databas,item.lokalDB, item.barcode, item.note, Aret);
                    
                }

               
            }
            //
            ExportToExcel(listOfProducts);

        }


        public void ExportToExcel(List<printBarcodeItem> list)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            






                // Load Excel application
                NsExcel.Application excel = new Microsoft.Office.Interop.Excel.Application();

                // Create empty workbook
                NsExcel.Workbook wb = excel.Workbooks.Add(NsExcel.XlWBATemplate.xlWBATWorksheet);

                // Create Worksheet
                NsExcel.Worksheet workSheet = (NsExcel.Worksheet)wb.Worksheets[1];

                //Format header
                NsExcel.Range formatRange;
                formatRange = workSheet.get_Range("a1");
                formatRange.EntireRow.Font.Bold = true;
                //workSheet.Cells[1, 1] = "Bold";


                //Format Excel cells to store values as text
                NsExcel.Range formatRange1;
                formatRange1 = workSheet.get_Range("a5", "f1000");
                formatRange1.NumberFormat = "@";





                // I created Application and Worksheet objects before try/catch,
                // so that i can close them in finnaly block.
                // It's IMPORTANT to release these COM objects!!
                try
                {
                    // ------------------------------------------------
                    // Creation of header cells
                    // ------------------------------------------------
                    workSheet.Cells[1, "A"] = "PRINTED BARCODES";
                    if (Skap == "0" || Lada == "0" ) 
                    {
                        workSheet.Cells[2, "A"] = "Barcodes in " + Rum;
                    } 
                    else 
                    {
                        workSheet.Cells[2, "A"] = "Barcodes in " + Rum + Separator + Skap + Separator + Lada;
                    }


                    workSheet.Cells[4, "A"] = "Barcode";
                    workSheet.Cells[4, "B"] = "Product";
                    workSheet.Cells[4, "C"] = "Supplier";
                    workSheet.Cells[4, "D"] = "CAS";
                    workSheet.Cells[4, "E"] = "Amount";
                    workSheet.Cells[4, "F"] = "Note";

                    // ------------------------------------------------
                    // Populate sheet with some real data from list
                    // ------------------------------------------------
                    int row = 5; // start row (in row 1 are header cells)
                    foreach (var item in list)
                    {
                        if (item.print)
                        {
                            workSheet.Cells[row, "A"] = item.barcode;
                            workSheet.Cells[row, "B"] = item.Namn;
                            workSheet.Cells[row, "C"] = item.Leverantor;
                            workSheet.Cells[row, "D"] = ' ' + item.CAS;
                            workSheet.Cells[row, "E"] = item.amount + " " + item.unit;
                            workSheet.Cells[row, "F"] = item.note;
                        }
                        else
                        {
                            row--;
                        }

                        row++;
                    }

                    // Apply some predefined styles for data to look nicely :)
                    workSheet.Range["A4"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1);

                    // Define filename
                    string fileName = string.Format(@"{0}\ExcelData.xlsx", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
                    //@"c:\temp\pdfName.pdf"

                    // Save this data as a file
                    workSheet.SaveAs(fileName);

                    // Display SUCCESS message
                    MessageBox.Show(string.Format("The file '{0}' is saved successfully!", fileName));
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Exception",
                        "There was a PROBLEM saving Excel file!\n" + exception.Message,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Quit Excel application
                    excel.Quit();

                    // Release COM objects (very important!)
                    if (excel != null)
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);

                    if (workSheet != null)
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);

                    // Empty variables
                    excel = null;
                    workSheet = null;

                    // Force garbage collector cleaning
                    GC.Collect();
                }
            }

        }
        
    }
