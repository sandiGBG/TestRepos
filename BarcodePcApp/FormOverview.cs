using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using Neodynamic.WinControls.BarcodeProfessional;
using System.Timers;
using System.Text.RegularExpressions;

namespace BarcodePcApp
{
    public partial class FormOverview : Form
    {
        private int verkId;
        private int userId;
        private int orgNod;
        private int orgAr;
        private int orgid;
        private int orgar;
        private int lokalid;
        private int platsid;
        private int sprak;
        private string userName;
        private string userStringDepart;
        private string userStringName;
        private string userStringSys;
        private string databas;
        private string kemiDb;
        private string orgNamn;
        private string valAvGrid;

        private string m_sCAS;
        private string m_sStreckkod;
        private string m_sSummaformel;
        private string m_sCaptionBelow;
        private string m_sComment;
        // The class that will do the printing process.
        DataGridViewPrinter MyDataGridViewPrinter;
        private string barcodeInSettings = "";
        private System.Timers.Timer scannerTimer = new System.Timers.Timer();
        private string barcode = "";
        private bool Log;
        public string datum_max = "";
        public string datum_m = "";
        private string m_sSkapat_datum = "";
        public string m_sRoom = "";
        public string m_sCabinet = "";
        private string m_sRumNamn = "";
        public string init = "";
        public string m_sUtgDatum = "";
        public static bool lang = false;                 //ver 1.27





        public FormOverview()
        {
            InitializeComponent();
            // Sandi
            this.KeyPress += new KeyPressEventHandler(form_KeyPress);
            scannerTimer.Interval = 100;
            scannerTimer.Elapsed += scannerTimer_Elapsed;
            FormSettings form = new FormSettings();
            barcodeInSettings = form.loginBarcode;
            this.ActiveControl = textBoxScanCode;

            //Skapa initialer
            string name123 = FormMain.Get.Usernamestring;
            Regex initials = new Regex(@"(\b[a-öA-Ö])[a-öA-Ö]* ?");
            init = initials.Replace(name123, "$1");

            lang = FormMain.Get.LangChange;
            if (lang == true)
            {
                sprak = 0;
            }
            else
            {
                sprak = 1;
            }

            if (lang == true)
            {
                this.labelOverview.Text = "Produktöversikt";
                this.labelCheckedIn.Text = "Registerade produkter:";
                this.buttonPrint.Text = "Skriv ut";
                this.buttonPrint2.Text = "Skriv ut";
                this.labelCheckedOut.Text = "Kasserade produkter:";
                this.buttonClose.Text = "Stäng";
                this.scannerCheckBox.Text = "Skanna && checka ut";
                this.buttonScanned.Text = "Check out";
                this.Text = "Produktöversikt";
                sprak = 0;


            }
            else
            {
                this.labelOverview.Text = "Product overview";
                this.labelCheckedIn.Text = "Registered products:";
                this.buttonPrint.Text = "Print";
                this.buttonPrint2.Text = "Print";
                this.labelCheckedOut.Text = "Discarded products:";
                this.buttonClose.Text = "Close";
                this.scannerCheckBox.Text = "Scan && check out";
                this.buttonScanned.Text = "Check out";
                this.Text = "Product overview";
                sprak = 1;

            }

        }


        public int VerkId { set { verkId = value; } }
        public int UserId { set { userId = value; } }
        public int OrgNod { set { orgNod = value; } }
        public int OrgAr { set { orgAr = value; } }
        public string UserName { set { userName = value; } }
        public string UserStringDepart { set { userStringDepart = value; } }
        public string UserStringName { set { userStringName = value; } }
        public string UserStringSys { set { userStringSys = value; } }
        public string OrgNamn { set { orgNamn = value; } }
        public string Databas { set { databas = value; } }
        public string KemiDb { set { kemiDb = value; } }

        private void FormOverview_Load(object sender, EventArgs e)
        {
            // Hämta den inloggade personens produkter:
            barcode.BarcodeService bc = FormMain.getBarcodeService();
            string prod = "";

            try
            {
                //prod = bc.GetAllProducts(databas, kemiDb, userId, userName, userStringSys, FormMain.Get.BarcodePrefix,lang);
                prod = bc.GetAllProducts(databas, kemiDb, userId, userName, userStringSys, FormMain.Get.BarcodePrefix,sprak);
            }
            catch (Exception err)
            {
                MessageBox.Show("No contact with the web server!\r\n\nCheck the Internet connection.\r\n\nError message:\r\n\n" + err.Message,"GetAllProducts");
                bc.Dispose();
                return;
            }
            XmlTextReader xtr = null;
            StringReader sr = new StringReader(prod);

            xtr = new XmlTextReader(sr);
            DataSet ds = new DataSet();
            ds.ReadXml(xtr);
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.Rows[0];

            lokalid =Convert.ToInt32(dr["lokalid"].ToString());
            orgid = Convert.ToInt32(dr["orgid"].ToString());
            orgar = Convert.ToInt32(dr["orgar"].ToString());
            platsid = Convert.ToInt32(dr["platsid"].ToString());

            DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();
            iconColumn.Image = BarcodePcApp.Properties.Resources.prnt;
            DataGridViewImageColumn iconColumn2 = new DataGridViewImageColumn();
            iconColumn2.Image = BarcodePcApp.Properties.Resources.prnt;

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Prodnam", Type.GetType("System.String"));      // Produktnamn
            dt2.Columns.Add("Cas", Type.GetType("System.String"));          // CASnr
            dt2.Columns.Add("Mangd", Type.GetType("System.String"));        // Mängd (till exempel 200)
            dt2.Columns.Add("EnhBen", Type.GetType("System.String"));       // Enhet (till exempel kg)
            dt2.Columns.Add("BarCodeTxt", Type.GetType("System.String"));   // Planned end date
            dt2.Columns.Add("Slutdat", Type.GetType("System.String"));      // Discarded products date
            dt2.Columns.Add("Streckkodsid", Type.GetType("System.String")); // Streckkodsid (till exempel MAX43)
            dt2.Columns.Add("Kemiskbet", Type.GetType("System.String"));
            //dt2.Columns.Add("Note", Type.GetType("System.String"));
            dt2.Columns.Add("Kommentar", Type.GetType("System.String"));
            dt2.Columns.Add("bastfore", Type.GetType("System.String"));

            DataTable dt3 = new DataTable();
            dt3.Columns.Add("Prodnam", Type.GetType("System.String"));
            dt3.Columns.Add("Cas", Type.GetType("System.String"));
            dt3.Columns.Add("Mangd", Type.GetType("System.String"));
            dt3.Columns.Add("EnhBen", Type.GetType("System.String"));
            dt3.Columns.Add("BarCodeTxt", Type.GetType("System.String"));
            dt3.Columns.Add("Slutdat", Type.GetType("System.String"));
            dt3.Columns.Add("Streckkodsid", Type.GetType("System.String"));
            dt3.Columns.Add("Kemiskbet", Type.GetType("System.String"));            
            dt3.Columns.Add("Kommentar", Type.GetType("System.String"));
            //dt3.Columns.Add("Note", Type.GetType("System.String"));
            //dt3.Columns.Add("Skriv ut igen", typeof(Bitmap));


            if (Convert.ToInt32(ds.Tables["ProdId"].Rows[0]["id"]) > 0)
            {
                this.buttonPrint.Visible = true;
                this.buttonPrint2.Visible = true;
                this.buttonPrint.Enabled = false;
                this.buttonPrint2.Enabled = false;


                // ******************** Tabell 1 ********************
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["Slutdat"]) == 0)
                    {
                        dt2.ImportRow(dt.Rows[i]);
                    }
                }
                if (dt2.Rows.Count > 0)
                {
                    buttonPrint.Enabled = true;
                    resultDataGridView.DataSource = dt2;
                    resultDataGridView.Columns.Insert(6, iconColumn);

                    if (lang == true)
                    {
                        resultDataGridView.CurrentRow.Selected = false;
                        resultDataGridView.Columns[0].HeaderText = "Produkt";
                        resultDataGridView.Columns[0].Width = 100;
                        resultDataGridView.Columns[1].HeaderText = "CAS";
                        resultDataGridView.Columns[1].Width = 60;
                        resultDataGridView.Columns[2].HeaderText = "Mängd";
                        resultDataGridView.Columns[2].Width = 40;
                        resultDataGridView.Columns[3].HeaderText = "Enhet";
                        resultDataGridView.Columns[3].Width = 30;
                        resultDataGridView.Columns[4].HeaderText = "Planerat slutdatum";
                        resultDataGridView.Columns[4].Width = 100;
                        resultDataGridView.Columns[4].Visible = false;
                        resultDataGridView.Columns[5].HeaderText = "Kasserat datum";
                        resultDataGridView.Columns[5].Width = 100;
                        resultDataGridView.Columns[5].Visible = false;
                        resultDataGridView.Columns[6].Name = "Skriv ut etikett";
                        resultDataGridView.Columns[6].Width = 50;
                        resultDataGridView.Columns[7].HeaderText = "Streckkod";
                        resultDataGridView.Columns[7].Width = 70;
                        resultDataGridView.Columns[8].HeaderText = "Summaformel";
                        resultDataGridView.Columns[8].Width = 70;
                        resultDataGridView.Columns[8].Visible = false;
                        resultDataGridView.Columns[9].HeaderText = "Notering";
                        resultDataGridView.Columns[9].Width = 70;
                        resultDataGridView.Columns[10].HeaderText = "Förfallodatum";
                        resultDataGridView.Columns[10].Width = 70;
                        resultDataGridView.Columns[10].Visible = false;//Orkar inte ta bort den...
                        //resultDataGridView.Columns[9].Visible = false;
                    }
                    else
                    {
                        resultDataGridView.CurrentRow.Selected = false;
                        resultDataGridView.Columns[0].HeaderText = "Product";
                        resultDataGridView.Columns[0].Width = 100;
                        resultDataGridView.Columns[1].HeaderText = "CAS";
                        resultDataGridView.Columns[1].Width = 60;
                        resultDataGridView.Columns[2].HeaderText = "Amount";
                        resultDataGridView.Columns[2].Width = 40;
                        resultDataGridView.Columns[3].HeaderText = "Unit";
                        resultDataGridView.Columns[3].Width = 30;
                        resultDataGridView.Columns[4].HeaderText = "Planned end date";
                        resultDataGridView.Columns[4].Width = 100;
                        resultDataGridView.Columns[4].Visible = false;
                        resultDataGridView.Columns[5].HeaderText = "Discarded date";
                        resultDataGridView.Columns[5].Width = 100;
                        resultDataGridView.Columns[5].Visible = false;
                        resultDataGridView.Columns[6].Name = "Print label";
                        resultDataGridView.Columns[6].Width = 50;
                        resultDataGridView.Columns[7].HeaderText = "Barcode";
                        resultDataGridView.Columns[7].Width = 70;
                        resultDataGridView.Columns[8].HeaderText = "Summaformel";
                        resultDataGridView.Columns[8].Width = 70;
                        resultDataGridView.Columns[8].Visible = false;
                        resultDataGridView.Columns[9].HeaderText = "Note";
                        resultDataGridView.Columns[9].Width = 70;
                        resultDataGridView.Columns[10].HeaderText = "bastfore";
                        resultDataGridView.Columns[10].Width = 70;
                        resultDataGridView.Columns[10].Visible = false;//Orkar inte ta bort den...
                                                                       //resultDataGridView.Columns[9].Visible = false;

                    }


                }

                // ******************** Tabell 2 ********************
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["Slutdat"]) > 0)
                    {
                        dt3.ImportRow(dt.Rows[i]);
                    }
                }
                if (dt3.Rows.Count > 0) 
                {

                    this.buttonPrint2.Enabled = true;
                    if (lang == true)
                    {
                        resultDataGridView2.DataSource = dt3;
                        resultDataGridView2.Columns.Insert(6, iconColumn2);
                        resultDataGridView2.Columns[4].Visible = false;//Orkar inte ta bort den...
                        resultDataGridView2.Columns[6].Visible = false;//Orkar inte ta bort den...
                        resultDataGridView2.Columns[8].Visible = false;
                        resultDataGridView2.CurrentRow.Selected = false;
                        resultDataGridView2.Columns[0].HeaderText = "Produkt";
                        resultDataGridView2.Columns[0].Width = 100;
                        resultDataGridView2.Columns[1].HeaderText = "CAS";
                        resultDataGridView2.Columns[1].Width = 60;
                        resultDataGridView2.Columns[2].HeaderText = "Mängd";
                        resultDataGridView2.Columns[2].Width = 40;
                        resultDataGridView2.Columns[3].HeaderText = "Enhet";
                        resultDataGridView2.Columns[3].Width = 30;
                        resultDataGridView2.Columns[4].HeaderText = "Planerat slutdatum";
                        resultDataGridView2.Columns[4].Width = 100;
                        resultDataGridView2.Columns[5].HeaderText = "datum";
                        resultDataGridView2.Columns[5].Width = 100;
                        resultDataGridView2.Columns[6].Name = "Skriv ut etikett";
                        resultDataGridView2.Columns[6].Width = 50;
                        resultDataGridView2.Columns[7].HeaderText = "Streckkod";
                        resultDataGridView2.Columns[7].Width = 70;
                        resultDataGridView2.Columns[8].HeaderText = "Summaformel";
                        resultDataGridView2.Columns[8].Width = 70;
                        resultDataGridView2.Columns[9].HeaderText = "Notering";
                        resultDataGridView2.Columns[9].Width = 70;
                        //resultDataGridView2.Columns[10].HeaderText = "Förfallodatum";
                        //resultDataGridView2.Columns[10].Width = 70;
                       // resultDataGridView2.Columns[10].Visible = false;//Orkar inte ta bort den...
                    }
                    else
                    {
                        resultDataGridView2.DataSource = dt3;
                        resultDataGridView2.Columns.Insert(6, iconColumn2);
                        resultDataGridView2.Columns[4].Visible = false;//Orkar inte ta bort den...
                        resultDataGridView2.Columns[6].Visible = false;//Orkar inte ta bort den...
                        resultDataGridView2.Columns[8].Visible = false;
                        resultDataGridView2.CurrentRow.Selected = false;
                        resultDataGridView2.Columns[0].HeaderText = "Product";
                        resultDataGridView2.Columns[0].Width = 100;
                        resultDataGridView2.Columns[1].HeaderText = "CAS";
                        resultDataGridView2.Columns[1].Width = 60;
                        resultDataGridView2.Columns[2].HeaderText = "Amount";
                        resultDataGridView2.Columns[2].Width = 40;
                        resultDataGridView2.Columns[3].HeaderText = "Unit";
                        resultDataGridView2.Columns[3].Width = 30;
                        resultDataGridView2.Columns[4].HeaderText = "Planned end date";
                        resultDataGridView2.Columns[4].Width = 100;
                        resultDataGridView2.Columns[5].HeaderText = "Date";
                        resultDataGridView2.Columns[5].Width = 100;
                        resultDataGridView2.Columns[6].Name = "Print label";
                        resultDataGridView2.Columns[6].Width = 50;
                        resultDataGridView2.Columns[7].HeaderText = "Barcode";
                        resultDataGridView2.Columns[7].Width = 70;
                        resultDataGridView2.Columns[8].HeaderText = "Summaformel";
                        resultDataGridView2.Columns[8].Width = 70;
                        resultDataGridView2.Columns[9].HeaderText = "Note";
                        resultDataGridView2.Columns[9].Width = 70;
                        //resultDataGridView2.Columns[10].HeaderText = "bastfore";
                        //resultDataGridView2.Columns[10].Width = 70;
                        //resultDataGridView2.Columns[10].Visible = false;//Orkar inte ta bort den...


                    }

                }
            }
        }


        // The printing setup function
        private bool SetupThePrinting()
        {
            //PrintDialog MyPrintDialog = new PrintDialog();
            printDialogOverview.AllowCurrentPage = false;
            printDialogOverview.AllowPrintToFile = false;
            printDialogOverview.AllowSelection = false;
            printDialogOverview.AllowSomePages = false;
            printDialogOverview.PrintToFile = false;
            printDialogOverview.ShowHelp = false;
            printDialogOverview.ShowNetwork = false;

            if (printDialogOverview.ShowDialog() != DialogResult.OK)
                return false;
            if(lang==true)
            {
                printDocumentOverview.DocumentName = "Produktrapport";
            }
            else
            {
                printDocumentOverview.DocumentName = "Products Report";
            }
            printDocumentOverview.PrinterSettings =
                                printDialogOverview.PrinterSettings;
            printDocumentOverview.DefaultPageSettings =
            printDialogOverview.PrinterSettings.DefaultPageSettings;
            printDocumentOverview.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);
            string text;
            string text1;
            string text2;
            string text3;
            if (lang == true)
            {
                text = "Vill du att rapporten ska centreras på sidan";
                text1 = "InvoiceManager - Centrera på sidan";
                text2 = "Registrerade produkter";
                text3 = "Kasserade produkter";

            }
            else
            {
                text = "Do you want the report to be centered on the page";
                text1 = "InvoiceManager - Center on Page";
                text2 = "Registered Products";
                text3 = "Discarded Products";

            }
            if (valAvGrid == "resultDataGridView")
            {
                if (MessageBox.Show(text,text1, MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                    MyDataGridViewPrinter = new DataGridViewPrinter(resultDataGridView,
                    printDocumentOverview, true, true, text2, new Font("Tahoma", 18,
                    FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
                else
                    MyDataGridViewPrinter = new DataGridViewPrinter(resultDataGridView,
                    printDocumentOverview, false, true, text2, new Font("Tahoma", 18,
                    FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
            }
            else if (valAvGrid == "resultDataGridView2")
            {
                if (MessageBox.Show(text,text1, MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                    MyDataGridViewPrinter = new DataGridViewPrinter(resultDataGridView2,
                    printDocumentOverview, true, true, text3, new Font("Tahoma", 18,
                    FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
                else
                    MyDataGridViewPrinter = new DataGridViewPrinter(resultDataGridView2,
                    printDocumentOverview, false, true, text3, new Font("Tahoma", 18,
                    FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
            }

            return true;
        }

        // The Print Button, båda två...
        private void buttonPrint_Click(object sender, EventArgs e)
        {
           valAvGrid = "resultDataGridView";
            if(lang==true)
            {
                resultDataGridView.Columns["Skriv ut etikett"].Visible = false;
            }
            else
            {
                resultDataGridView.Columns["Print label"].Visible = false;
            }
             
            if (SetupThePrinting()) 
                printDocumentOverview.Print();
            if (lang == true)
            {
                resultDataGridView.Columns["Skriv ut etikett"].Visible = true;
            }
            else
            {
                resultDataGridView.Columns["Print label"].Visible = true;
            }
            textBoxScanCode.Focus();

        }
        private void buttonPrint2_Click(object sender, EventArgs e)
        {
            valAvGrid = "resultDataGridView2";
            if (lang == true)
            {
                resultDataGridView2.Columns["Skriv ut etikett"].Visible = false;
            }
            else
            {
                resultDataGridView2.Columns["Print label"].Visible = false;
            }
            if (SetupThePrinting())
                printDocumentOverview.Print();
            if (lang == true)
            {
                resultDataGridView2.Columns["Skriv ut etikett"].Visible = true;
            }
            else
            {
                resultDataGridView2.Columns["Print label"].Visible = true;
            }
            textBoxScanCode.Focus();
        }

        // The PrintPage action for the PrintDocument control
        private void printDocumentOverview_PrintPage(object sender,
            System.Drawing.Printing.PrintPageEventArgs e)
        {
            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }

        // The Print Preview Button, båda två...
        private void buttonPrintPreview_Click(object sender, EventArgs e)
        {
            valAvGrid = "resultDataGridView";
            if (lang == true)
                {
                    resultDataGridView.Columns["Skriv ut etikett"].Visible = false;
                }
            else
                {
                    resultDataGridView.Columns["Print label"].Visible = false;
                }
            if (SetupThePrinting())
                {
                    PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
                    MyPrintPreviewDialog.Document = printDocumentOverview;
                    MyPrintPreviewDialog.ShowDialog();
                }
            if (lang == true)
                {
                    resultDataGridView.Columns["Skriv ut etikett"].Visible = true;
                }
            else
                {
                    resultDataGridView.Columns["Print label"].Visible = true;
                }
            textBoxScanCode.Focus();
        }
        private void buttonPrintPreview2_Click(object sender, EventArgs e)
        {
            valAvGrid = "resultDataGridView2";
            if (lang == true)
            {
                resultDataGridView2.Columns["Skriv ut etikett"].Visible = false;
            }
            else
            {
                resultDataGridView2.Columns["Print label"].Visible = false;
            }
            if (SetupThePrinting())
            {
                PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
                MyPrintPreviewDialog.Document = printDocumentOverview;
                MyPrintPreviewDialog.ShowDialog();
            }
            if (lang == true)
            {
                resultDataGridView2.Columns["Skriv ut etikett"].Visible = true;
            }
            else
            {
                resultDataGridView2.Columns["Print label"].Visible = true;
            }
            textBoxScanCode.Focus();
        }

        private void resultDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                if (lang==true)
                {
                    MessageBox.Show("Notera att detta är en kopia av streckkodsetiketten, inte en ny streckkodsetikett!", "Information");
                }
                else
                {
                    MessageBox.Show("Note, this is a copy of the barcode label, not a new registration!", "Information");
                }
                

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
                    if(lang==true)
                    {
                        MessageBox.Show("Skrivaren är inte installerad");
                    }
                    else
                    {
                        MessageBox.Show("No label printer is installed");
                    }
                   
                    return;
                }
                /*
                                string s = "Skrivare: " + pd.PrinterSettings.PrinterName + "\r\n";
                                s = s + "Resolution: " + barcode.Resolution + "\r\n";
                                s = s + "ResolutionDPI: " + barcode.ResolutionCustomDPI + "\r\n";
                                s = s + "XDimensionCM: " + barcode.XDimensionCM + "\r\n";
                                s = s + "XDimensionMILS: " + barcode.XDimensionMILS + "\r\n";
                                s = s + "BarHeightCM: " + barcode.BarHeightCM + "\r\n";
                                MessageBox.Show(s);
                */
                pd.DocumentName = "Label for the product";
                pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocumentOnPrintPage);

                //barcode.CaptionAbove = resultDataGridView.Rows[e.RowIndex].Cells["Cas"].Value.ToString();
                //barcode.DataToEncode = resultDataGridView.Rows[e.RowIndex].Cells["Streckkodsid"].Value.ToString();
                //barcode.CaptionBelow = userStringName + ", " + resultDataGridView.Rows[e.RowIndex].Cells["BarCodeTxt"].Value.ToString().Trim();

                DateTime currentDate = DateTime.Now;
                DateTime d = (DateTime)currentDate;
                string datum_max = d.ToString("yy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                datum_m = init + "/" + d.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

                if (FormMain.Get.BarcodeLayout == 0)
                {
                    bc.Text = "";
                    bc.BarHeight = 7;
                    m_sComment = "";
                    if (resultDataGridView.Rows[e.RowIndex].Cells["Cas"].Value.ToString().Length > 0)
                        m_sCAS = "CAS: " + resultDataGridView.Rows[e.RowIndex].Cells["Cas"].Value.ToString();
                    else
                        m_sCAS = "";
                    bc.Code = resultDataGridView.Rows[e.RowIndex].Cells["Streckkodsid"].Value.ToString();
                    m_sCaptionBelow = userStringName + ", " + resultDataGridView.Rows[e.RowIndex].Cells["BarCodeTxt"].Value.ToString().Trim();
                    bc.DisplayCode = false;
                }
                else if (FormMain.Get.BarcodeLayout == 1)
                {
                    bc.Text = "";
                    bc.BarHeight = 4;
                    m_sComment = "";
                    if (resultDataGridView.Rows[e.RowIndex].Cells["Cas"].Value.ToString().Length > 0)
                        m_sCAS = resultDataGridView.Rows[e.RowIndex].Cells["Cas"].Value.ToString();
                    else
                        m_sCAS = "";
                    if (resultDataGridView.Rows[e.RowIndex].Cells["Kemiskbet"].Value.ToString().Length > 0)
                        m_sSummaformel = resultDataGridView.Rows[e.RowIndex].Cells["Kemiskbet"].Value.ToString();
                    else
                        m_sSummaformel = "";
                    bc.Code = resultDataGridView.Rows[e.RowIndex].Cells["Streckkodsid"].Value.ToString();
                    m_sCaptionBelow = resultDataGridView.Rows[e.RowIndex].Cells["BarCodeTxt"].Value.ToString().Trim();
                    bc.DisplayCode = false;
                }
                else if (FormMain.Get.BarcodeLayout == 2)
                {
                    m_sStreckkod = resultDataGridView.Rows[e.RowIndex].Cells["Streckkodsid"].Value.ToString();
                    m_sComment = "";
                    bc.Code = m_sStreckkod;
                    if (resultDataGridView.Rows[e.RowIndex].Cells["Kemiskbet"].Value.ToString().Length > 0)
                        m_sSummaformel = resultDataGridView.Rows[e.RowIndex].Cells["Kemiskbet"].Value.ToString();
                    else
                        m_sSummaformel = "";
                    if (resultDataGridView.Rows[e.RowIndex].Cells["Cas"].Value.ToString().Length > 0)
                        m_sCAS = resultDataGridView.Rows[e.RowIndex].Cells["Cas"].Value.ToString();
                    else
                        m_sCAS = "";

                    bc.Text = "";
                    bc.BarHeight = 4;
                    bc.DisplayCode = false;
                    bc.CodeAlignment = Neodynamic.WinControls.BarcodeProfessional.Alignment.AboveLeft;
                }

                else if (FormMain.Get.BarcodeLayout == 3)
                {
                    m_sComment = resultDataGridView.Rows[e.RowIndex].Cells["kommentar"].Value.ToString();
                    m_sStreckkod = resultDataGridView.Rows[e.RowIndex].Cells["Streckkodsid"].Value.ToString();
                    bc.Code = m_sStreckkod;
                    if (resultDataGridView.Rows[e.RowIndex].Cells["Kemiskbet"].Value.ToString().Length > 0)
                        m_sSummaformel = resultDataGridView.Rows[e.RowIndex].Cells["Kemiskbet"].Value.ToString();
                    else
                        m_sSummaformel = "";
                    if (resultDataGridView.Rows[e.RowIndex].Cells["Cas"].Value.ToString().Length > 0)
                        m_sCAS = resultDataGridView.Rows[e.RowIndex].Cells["Cas"].Value.ToString();
                    else
                        m_sCAS = "";

                    bc.Text = "";
                    bc.BarHeight = 4;
                    bc.DisplayCode = false;
                    bc.CodeAlignment = Neodynamic.WinControls.BarcodeProfessional.Alignment.AboveLeft;
                }

                else if (FormMain.Get.BarcodeLayout == 4)
                {
                    m_sComment = resultDataGridView.Rows[e.RowIndex].Cells["kommentar"].Value.ToString();
                    m_sStreckkod = resultDataGridView.Rows[e.RowIndex].Cells["Streckkodsid"].Value.ToString();
                    bc.Code = m_sStreckkod;
                    //if (resultDataGridView.Rows[e.RowIndex].Cells["Kemiskbet"].Value.ToString().Length > 0)
                    //    m_sSummaformel = resultDataGridView.Rows[e.RowIndex].Cells["Kemiskbet"].Value.ToString();
                    //else
                    //    m_sSummaformel = "";
                    m_sSummaformel = datum_max;
                    if (resultDataGridView.Rows[e.RowIndex].Cells["Cas"].Value.ToString().Length > 0)
                        m_sCAS = resultDataGridView.Rows[e.RowIndex].Cells["Cas"].Value.ToString();
                    else
                        m_sCAS = "";

                    bc.Text = "";
                    bc.BarHeight = 4;
                    bc.DisplayCode = false;
                    bc.CodeAlignment = Neodynamic.WinControls.BarcodeProfessional.Alignment.AboveLeft;
                }

                else if (FormMain.Get.BarcodeLayout == 5)
                {
                    m_sStreckkod = resultDataGridView.Rows[e.RowIndex].Cells["Streckkodsid"].Value.ToString();

                    barcode.BarcodeService bc1 = FormMain.getBarcodeService();
                    string Cabinet_Room = bc1.GetStorageNameOverview(FormMain.Get.Databas, FormMain.Get.KemiDB, FormMain.Get.Databas,m_sStreckkod);
                    if (Cabinet_Room != "0")
                    {    
                        StringReader sr1 = new StringReader(Cabinet_Room);
                        XmlTextReader xtr2 = null;
                        xtr2 = new XmlTextReader(sr1);
                 

                        DataSet ds1 = new DataSet();
                        ds1.ReadXml(xtr2);
                
                        DataTable dt1 = ds1.Tables[0];
                        DataRow dr1 = dt1.Rows[0];

                        string Rum = dr1["StorageName"].ToString();
                        string Skap = dr1["Skap"].ToString();
                        string Lada = dr1["Lada"].ToString();
                        m_sRoom = Rum;
                        m_sCabinet = Skap;
                    }
                    else
                    {
                        string Rum = "";
                        string Skap = "";
                        string Lada = "";
                        m_sRoom = Rum;
                        m_sCabinet = Skap;
                    }
                    if (resultDataGridView.Rows[e.RowIndex].Cells["Cas"].Value.ToString().Length > 0)
                        m_sCAS = resultDataGridView.Rows[e.RowIndex].Cells["Cas"].Value.ToString();
                    else
                        m_sCAS = "";

                    
                    bc.Code = m_sStreckkod;
                    m_sSkapat_datum = datum_m;
                    //m_sRoom =Rum;
                    //m_sCabinet = Skap;
                    m_sUtgDatum = resultDataGridView.Rows[e.RowIndex].Cells["bastfore"].Value.ToString();
                    if (m_sUtgDatum == "0")
                    {
                        m_sUtgDatum = "";
                    }

                    bc.Text = "";
                    bc.BarHeight = 4;
                    bc.DisplayCode = false;
                    bc.CodeAlignment = Neodynamic.WinControls.BarcodeProfessional.Alignment.AboveLeft;
                }

                //bc.Save("layout.gif", System.Drawing.Imaging.ImageFormat.Gif);
                pd.Print();
                textBoxScanCode.Focus();
                pd.Dispose();
            }
        }

        private void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs ppea)
        {
            BarcodeLayout.PrintDocumentOnPrintPage(sender, ppea, bc, m_sCAS, m_sSummaformel, m_sStreckkod, m_sComment, datum_max, m_sSkapat_datum, m_sUtgDatum, FormMain.Get.Username, m_sCabinet, m_sRoom);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bc_Click(object sender, EventArgs e)
        {

        }

        //Sandi - logga ut med barcode
        private void textBoxScanCode_TextChanged(object sender, EventArgs e)
        {
            Log = FormMain.Get.PropertyCheck;

            //if (Log == true)
            //{
            //    if (textBoxScanCode.Text != "")
            //    {
            //        if (textBoxScanCode.Text == barcodeInSettings)
            //        {
            //            this.Invoke(new MethodInvoker(delegate { FormMain.Get.Logout(); })); 
            //            //this.Close();
            //            //this.Owner.Close();
            //        }
            //        //labelShowProduct.Text = textBoxScanCode.Text;

            //    }
            //}

        }

        private void form_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 65 && e.KeyChar <= 122)) && scannerCheckBox.Checked)
            {
                //scannerTimer.Enabled = false;
                scannerTimer.Stop();

                barcode += e.KeyChar;

                //scannerTimer.Enabled = true;
                scannerTimer.Start();
            }

        }

        private void scannerTimer_Elapsed(object source, ElapsedEventArgs e)
        {
            //scannerTimer.Enabled = false;
            scannerTimer.Stop();

            //User barcode and scan barcode is the same, check out user and close
            if (barcodeInSettings == barcode)
            {
                this.Close();
                this.Owner.Close();
            }
        }

        private void scannerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            textBoxScanCode.Clear();
            if (scannerCheckBox.CheckState == CheckState.Checked)
            {
                panel1.Visible = true;
                buttonScanned.Visible = false;
                textBoxScanCode.Enabled = true;
                textBoxScanCode.Focus();
                
            }
            else
            {
                panel1.Visible = false;
                buttonScanned.Visible = true;
                textBoxScanCode.Enabled = true;
                textBoxScanCode.Focus();
            }
        }

    }
}