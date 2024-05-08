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
    public partial class FormRelease : Form
    {
        private int verkId;
        private int userId;
        private int orgNod;
        private int orgAr;
        private int orgid;
        private int orgar;
        private int lokalid;
        private int platsid;
        private int invid;
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

        private const int MAX_CHECKS = 5;
        private List<FormRelease> listOfProducts;
        private string uppd = "";

        DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
        DataGridViewCheckBoxCell ch2 = new DataGridViewCheckBoxCell();
        DataGridViewCheckBoxCell a = new DataGridViewCheckBoxCell();
        DataGridViewCheckBoxCell b = new DataGridViewCheckBoxCell();



        //public FormRelease(string databas,string kemiDb,int userId,string userName,string userStringSys,string FormMain.Get.BarcodePrefix)
        public FormRelease()
        {
            InitializeComponent();
            ListView list = new System.Windows.Forms.ListView();
            // Sandi
            //this.KeyPress += new KeyPressEventHandler(form_KeyPress);
            //scannerTimer.Interval = 100;
            //scannerTimer.Elapsed += scannerTimer_Elapsed;
            FormSettings form = new FormSettings();
            barcodeInSettings = form.loginBarcode;
            //this.ActiveControl = textBoxScanCode;

            //Skapa initialer
            string name123 = FormMain.Get.Usernamestring;
            Regex initials = new Regex(@"(\b[a-öA-Ö])[a-öA-Ö]* ?");
            init = initials.Replace(name123, "$1");

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

        

        private void FormRelease_Load(object sender, EventArgs e)
        {
            LoadData(sender,e);

            //// Hämta den inloggade personens produkter:
            //barcode.BarcodeService bc = FormMain.getBarcodeService();
            //string prod = "";


            //try
            //{
            //    prod = bc.GetReleaseProducts(databas, kemiDb, userId, orgid, verkId);
            //    //prod = bc.GetAllProducts(databas, kemiDb, userId, userName, userStringSys, FormMain.Get.BarcodePrefix);
            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show("No contact with the web server!\r\n\nCheck the Internet connection.\r\n\nError message:\r\n\n" + err.Message, "GetReleaseProducts");
            //    bc.Dispose();
            //    return;
            //}
            //XmlTextReader xtr = null;
            //StringReader sr = new StringReader(prod);

            //xtr = new XmlTextReader(sr);
            //DataSet ds = new DataSet();
            //ds.ReadXml(xtr);
            //DataTable dt = ds.Tables[0];
            //DataRow dr = dt.Rows[0];

            //lokalid = Convert.ToInt32(dr["lokalid"].ToString());
            //orgid = Convert.ToInt32(dr["orgid"].ToString());
            //orgar = Convert.ToInt32(dr["orgar"].ToString());
            //platsid = Convert.ToInt32(dr["platsid"].ToString());


            //DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();
            //iconColumn.Image = BarcodePcApp.Properties.Resources.prnt;
            //DataGridViewImageColumn iconColumn2 = new DataGridViewImageColumn();
            //iconColumn2.Image = BarcodePcApp.Properties.Resources.prnt;

            ////DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn();
            ////DataGridViewCheckBoxColumn check1 = new DataGridViewCheckBoxColumn();
            //ch1.ValueType = typeof(bool);
            //ch1.TrueValue = true;
            //ch1.FalseValue = false;
            //ch1.ThreeState = false;
    


            //ch2.ValueType = typeof(bool);
            //ch2.TrueValue = "true";
            //ch2.FalseValue = "false";
            //ch2.ThreeState = false;




            //DataTable dt2 = new DataTable();
            ////dt2.Columns.Add(new DataColumn("NOK", typeof(bool))); //this will show checkboxes
            ////dt2.Columns.Add(new DataColumn("OK", typeof(bool))); //this will show checkboxes

            //dt2.Columns.Add("Streckkodsid", Type.GetType("System.String")); // Streckkodsid (till exempel MAX43)
            //dt2.Columns.Add("Prodnam", Type.GetType("System.String"));      // Produktnamn
            //dt2.Columns.Add("Cas", Type.GetType("System.String"));          // CASnr
            //dt2.Columns.Add("Leverantor", Type.GetType("System.String"));
            //dt2.Columns.Add("ArtNr", Type.GetType("System.String"));
            //dt2.Columns.Add("Batch", Type.GetType("System.String"));
            //dt2.Columns.Add("Mangd", Type.GetType("System.String"));        // Mängd (till exempel 200)
            //dt2.Columns.Add("EnhBen", Type.GetType("System.String"));       // Enhet (till exempel kg)
            //dt2.Columns.Add("Registreringsdatum", Type.GetType("System.String"));
            //dt2.Columns.Add("bastfore", Type.GetType("System.String"));
            ////ReleaseDataGridView.Columns.Add(myCheckedColumn);
            ////ReleaseDataGridView.Columns.Add(myCheckedColumn1);
            //dt2.Columns.Add("NOK", typeof(bool)); //this will show checkboxes
            //dt2.Columns.Add("OK", typeof(bool)); //this will show checkboxes
            //dt2.Columns.Add("Invtransid", Type.GetType("System.String"));





            //DataTable dt3 = new DataTable();
            //dt3.Columns.Add("Prodnam", Type.GetType("System.String"));
            //dt3.Columns.Add("Cas", Type.GetType("System.String"));
            //dt3.Columns.Add("Leverantor", Type.GetType("System.String"));
            //dt3.Columns.Add("ArtNr", Type.GetType("System.String"));
            //dt3.Columns.Add("Batch", Type.GetType("System.String"));
            //dt3.Columns.Add("Mangd", Type.GetType("System.String"));        // Mängd (till exempel 200)
            //dt3.Columns.Add("EnhBen", Type.GetType("System.String"));       // Enhet (till exempel kg)
            //dt3.Columns.Add("Registreringsdatum", Type.GetType("System.String"));
            //dt3.Columns.Add("bastfore", Type.GetType("System.String"));


            //if (Convert.ToInt32(ds.Tables["ProdId"].Rows[0]["id"]) > 0)
            //{
            //    this.btnExecute.Visible = true;
            //    this.btnPrint2.Enabled = false;
            //    this.btnPrint2.Enabled = false;


            //    //    // ******************** Tabell 1 ********************
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        if (Convert.ToInt32(dt.Rows[i]["status"]) == -1)
            //        {
            //            dt2.ImportRow(dt.Rows[i]);
            //        }
            //    }
            //    if (dt2.Rows.Count > 0)
            //    {
            //        btnExecute.Enabled = true;
            //        ReleaseDataGridView.DataSource = dt2;
            //        //ReleaseDataGridView.Columns.Insert(7, iconColumn);




            //        ReleaseDataGridView.Columns[0].HeaderText = "Barcode";
            //        ReleaseDataGridView.Columns[0].Width = 50;
            //        ReleaseDataGridView.Columns[1].HeaderText = "Product";
            //        ReleaseDataGridView.Columns[1].Width = 100;
            //        ReleaseDataGridView.Columns[2].HeaderText = "CAS";
            //        ReleaseDataGridView.Columns[2].Width = 80;
            //        ReleaseDataGridView.Columns[3].HeaderText = "Supplier";
            //        ReleaseDataGridView.Columns[3].Width = 50;
            //        ReleaseDataGridView.Columns[4].HeaderText = "Art no";
            //        ReleaseDataGridView.Columns[4].Width = 50;
            //        ReleaseDataGridView.Columns[5].HeaderText = "Batch nr";
            //        ReleaseDataGridView.Columns[5].Width = 50;
            //        ReleaseDataGridView.Columns[6].HeaderText = "Amount";
            //        ReleaseDataGridView.Columns[6].Width = 40;
            //        ReleaseDataGridView.Columns[7].HeaderText = "Unit";
            //        ReleaseDataGridView.Columns[7].Width = 30;
            //        //ReleaseDataGridView.Columns[7].Name = "Print label";
            //        //ReleaseDataGridView.Columns[7].Width = 50;
            //        ReleaseDataGridView.Columns[8].HeaderText = "Registration date";
            //        ReleaseDataGridView.Columns[8].Width = 50;
            //        ReleaseDataGridView.Columns[9].HeaderText = "Use before";
            //        ReleaseDataGridView.Columns[9].Width = 50;
            //        //resultDataGridView.CurrentRow.Selected = false;
            //        ReleaseDataGridView.Columns[10].HeaderText = "NOK";
            //        ReleaseDataGridView.Columns[10].Width = 30;
            //        ReleaseDataGridView.Columns[10].ReadOnly = false;
            //        ReleaseDataGridView.Columns[11].HeaderText = "OK";
            //        ReleaseDataGridView.Columns[11].Width = 30;
            //        ReleaseDataGridView.Columns[11].ReadOnly = false;
            //        ReleaseDataGridView.Columns[12].HeaderText = "id";
            //        ReleaseDataGridView.Columns[12].Width = 50;

            //        ReleaseDataGridView.Columns[12].Visible = false;

            //    }

            //    // ******************** Tabell 2 ********************
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        if (Convert.ToInt32(dt.Rows[i]["status"]) == -2)
            //        {
            //            dt3.ImportRow(dt.Rows[i]);
            //        }
            //    }
            //    if (dt3.Rows.Count > 0)
            //    {
            //        btnPrint2.Enabled = true;


            //        DismissedDataGridView.DataSource = dt3;
            //        DismissedDataGridView.Columns[0].HeaderText = "Product";
            //        DismissedDataGridView.Columns[0].Width = 100;
            //        DismissedDataGridView.Columns[1].HeaderText = "CAS";
            //        DismissedDataGridView.Columns[1].Width = 50;
            //        DismissedDataGridView.Columns[2].HeaderText = "Supplier";
            //        DismissedDataGridView.Columns[2].Width = 100;
            //        DismissedDataGridView.Columns[3].HeaderText = "Art no";
            //        DismissedDataGridView.Columns[3].Width = 50;
            //        DismissedDataGridView.Columns[4].HeaderText = "Batch nr";
            //        DismissedDataGridView.Columns[4].Width = 50;
            //        DismissedDataGridView.Columns[5].HeaderText = "Amount";
            //        DismissedDataGridView.Columns[5].Width = 40;
            //        DismissedDataGridView.Columns[6].HeaderText = "Unit";
            //        DismissedDataGridView.Columns[6].Width = 30;
            //        //ReleaseDataGridView.Columns[7].Name = "Print label";
            //        //ReleaseDataGridView.Columns[7].Width = 50;
            //        DismissedDataGridView.Columns[7].HeaderText = "Registration date";
            //        DismissedDataGridView.Columns[7].Width = 50;
            //        DismissedDataGridView.Columns[8].HeaderText = "Use before";
            //        DismissedDataGridView.Columns[8].Width = 50;

            //    }
            //}
        }

        private void LoadData(object sender, EventArgs e)
        {

            // Hämta den inloggade personens produkter:
            barcode.BarcodeService bc = FormMain.getBarcodeService();
            string prod = "";


            try
            {
                prod = bc.GetReleaseProducts(databas, kemiDb, userId, orgid, verkId);
                //prod = bc.GetAllProducts(databas, kemiDb, userId, userName, userStringSys, FormMain.Get.BarcodePrefix);
            }
            catch (Exception err)
            {
                MessageBox.Show("No contact with the web server!\r\n\nCheck the Internet connection.\r\n\nError message:\r\n\n" + err.Message, "GetReleaseProducts");
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

            //XmlDocument doc = new XmlDocument();
            //    doc.LoadXml(prod);
            //    if (Convert.ToInt32(doc.DocumentElement.Attributes["antal"].Value) > 0)
            //    {

            //lokalid = Convert.ToInt32(dr["lokalid"].ToString());
            //orgid = Convert.ToInt32(dr["orgid"].ToString());
            //orgar = Convert.ToInt32(dr["orgar"].ToString());
            //platsid = Convert.ToInt32(dr["platsid"].ToString());
        //}

            DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();
            iconColumn.Image = BarcodePcApp.Properties.Resources.prnt;
            DataGridViewImageColumn iconColumn2 = new DataGridViewImageColumn();
            iconColumn2.Image = BarcodePcApp.Properties.Resources.prnt;

            ch1.ValueType = typeof(bool);
            ch1.TrueValue = true;
            ch1.FalseValue = false;
            ch1.ThreeState = false;
            ch1.ReadOnly = false;

            ch2.ValueType = typeof(bool);
            ch2.TrueValue = true;
            ch2.FalseValue = false;
            ch2.ThreeState = false;
            ch2.ReadOnly = false;




            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Streckkodsid", Type.GetType("System.String")); // Streckkodsid (till exempel MAX43)
            dt2.Columns.Add("Prodnam", Type.GetType("System.String"));      // Produktnamn
            dt2.Columns.Add("Cas", Type.GetType("System.String"));          // CASnr
            dt2.Columns.Add("Leverantor", Type.GetType("System.String"));
            dt2.Columns.Add("ArtNr", Type.GetType("System.String"));
            dt2.Columns.Add("Batch", Type.GetType("System.String"));
            dt2.Columns.Add("Mangd", Type.GetType("System.String"));        // Mängd (till exempel 200)
            dt2.Columns.Add("EnhBen", Type.GetType("System.String"));       // Enhet (till exempel kg)
            dt2.Columns.Add("Registreringsdatum", Type.GetType("System.String"));
            dt2.Columns.Add("bastfore", Type.GetType("System.String"));
            dt2.Columns.Add("NOK", typeof(bool)); //this will show checkboxes
            dt2.Columns.Add("OK", typeof(bool)); //this will show checkboxes
            dt2.Columns.Add("Invtransid", Type.GetType("System.String"));





            DataTable dt3 = new DataTable();
            dt3.Columns.Add("Prodnam", Type.GetType("System.String"));
            dt3.Columns.Add("Cas", Type.GetType("System.String"));
            dt3.Columns.Add("Leverantor", Type.GetType("System.String"));
            dt3.Columns.Add("ArtNr", Type.GetType("System.String"));
            dt3.Columns.Add("Batch", Type.GetType("System.String"));
            dt3.Columns.Add("Mangd", Type.GetType("System.String"));        // Mängd (till exempel 200)
            dt3.Columns.Add("EnhBen", Type.GetType("System.String"));       // Enhet (till exempel kg)
            dt3.Columns.Add("Registreringsdatum", Type.GetType("System.String"));
            dt3.Columns.Add("bastfore", Type.GetType("System.String"));


            if (Convert.ToInt32(ds.Tables["ProdId"].Rows[0]["id"]) > 0)
            {
                this.btnExecute.Visible = true;
                this.btnPrint2.Enabled = false;
                this.btnPrint2.Enabled = false;


                //    // ******************** Tabell 1 ********************
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["status"]) == -1)
                    {
                        dt2.ImportRow(dt.Rows[i]);
                    }
                }
                if (dt2.Rows.Count > 0)
                {
                    btnExecute.Enabled = true;
                    ReleaseDataGridView.DataSource = dt2;
                    //ReleaseDataGridView.Columns.Insert(7, iconColumn);




                    ReleaseDataGridView.Columns[0].HeaderText = "Barcode";
                    ReleaseDataGridView.Columns[0].Width = 50;
                    ReleaseDataGridView.Columns[1].HeaderText = "Product";
                    ReleaseDataGridView.Columns[1].Width = 100;
                    ReleaseDataGridView.Columns[2].HeaderText = "CAS";
                    ReleaseDataGridView.Columns[2].Width = 80;
                    ReleaseDataGridView.Columns[3].HeaderText = "Supplier";
                    ReleaseDataGridView.Columns[3].Width = 50;
                    ReleaseDataGridView.Columns[4].HeaderText = "Art no";
                    ReleaseDataGridView.Columns[4].Width = 50;
                    ReleaseDataGridView.Columns[5].HeaderText = "Batch nr";
                    ReleaseDataGridView.Columns[5].Width = 50;
                    ReleaseDataGridView.Columns[6].HeaderText = "Amount";
                    ReleaseDataGridView.Columns[6].Width = 40;
                    ReleaseDataGridView.Columns[7].HeaderText = "Unit";
                    ReleaseDataGridView.Columns[7].Width = 30;
                    //ReleaseDataGridView.Columns[7].Name = "Print label";
                    //ReleaseDataGridView.Columns[7].Width = 50;
                    ReleaseDataGridView.Columns[8].HeaderText = "Registration date";
                    ReleaseDataGridView.Columns[8].Width = 50;
                    ReleaseDataGridView.Columns[9].HeaderText = "Use before";
                    ReleaseDataGridView.Columns[9].Width = 50;
                    //resultDataGridView.CurrentRow.Selected = false;
                    ReleaseDataGridView.Columns[10].HeaderText = "NOK";
                    ReleaseDataGridView.Columns[10].Width = 30;
                    ReleaseDataGridView.Columns[10].ReadOnly = false;
                    ReleaseDataGridView.Columns[11].HeaderText = "OK";
                    ReleaseDataGridView.Columns[11].Width = 30;
                    ReleaseDataGridView.Columns[11].ReadOnly = false;
                    ReleaseDataGridView.Columns[12].HeaderText = "id";
                    ReleaseDataGridView.Columns[12].Width = 50;

                    ReleaseDataGridView.Columns[12].Visible = false;

                }

                // ******************** Tabell 2 ********************
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["status"]) == -2)
                    {
                        dt3.ImportRow(dt.Rows[i]);
                    }
                }
                if (dt3.Rows.Count > 0)
                {
                    btnPrint2.Enabled = true;


                    DismissedDataGridView.DataSource = dt3;
                    DismissedDataGridView.Columns[0].HeaderText = "Product";
                    DismissedDataGridView.Columns[0].Width = 100;
                    DismissedDataGridView.Columns[1].HeaderText = "CAS";
                    DismissedDataGridView.Columns[1].Width = 50;
                    DismissedDataGridView.Columns[2].HeaderText = "Supplier";
                    DismissedDataGridView.Columns[2].Width = 100;
                    DismissedDataGridView.Columns[3].HeaderText = "Art no";
                    DismissedDataGridView.Columns[3].Width = 50;
                    DismissedDataGridView.Columns[4].HeaderText = "Batch nr";
                    DismissedDataGridView.Columns[4].Width = 50;
                    DismissedDataGridView.Columns[5].HeaderText = "Amount";
                    DismissedDataGridView.Columns[5].Width = 40;
                    DismissedDataGridView.Columns[6].HeaderText = "Unit";
                    DismissedDataGridView.Columns[6].Width = 30;
                    //ReleaseDataGridView.Columns[7].Name = "Print label";
                    //ReleaseDataGridView.Columns[7].Width = 50;
                    DismissedDataGridView.Columns[7].HeaderText = "Registration date";
                    DismissedDataGridView.Columns[7].Width = 50;
                    DismissedDataGridView.Columns[8].HeaderText = "Use before";
                    DismissedDataGridView.Columns[8].Width = 50;

                }
            }
        }


        //        // The printing setup function
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

            printDocumentOverview.DocumentName = "Products Report";
            printDocumentOverview.PrinterSettings =
                                printDialogOverview.PrinterSettings;
            printDocumentOverview.DefaultPageSettings =
            printDialogOverview.PrinterSettings.DefaultPageSettings;
            printDocumentOverview.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);
            if (valAvGrid == "ReleaseDataGridView")
            {
                if (MessageBox.Show("Do you want the report to be centered on the page",
                    "InvoiceManager - Center on Page", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                    MyDataGridViewPrinter = new DataGridViewPrinter(ReleaseDataGridView,
                    printDocumentOverview, true, true, "Registered Products", new Font("Tahoma", 18,
                    FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
                else
                    MyDataGridViewPrinter = new DataGridViewPrinter(ReleaseDataGridView,
                    printDocumentOverview, false, true, "Registered Products", new Font("Tahoma", 18,
                    FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
            }
            else if (valAvGrid == "DismissedDataGridView")
            {
                if (MessageBox.Show("Do you want the report to be centered on the page",
                    "InvoiceManager - Center on Page", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                    MyDataGridViewPrinter = new DataGridViewPrinter(DismissedDataGridView,
                    printDocumentOverview, true, true, "Discarded Products", new Font("Tahoma", 18,
                    FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
                else
                    MyDataGridViewPrinter = new DataGridViewPrinter(DismissedDataGridView,
                    printDocumentOverview, false, true, "Discarded Products", new Font("Tahoma", 18,
                    FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
            }

            return true;
        }




        private void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs ppea)
        {
            BarcodeLayout.PrintDocumentOnPrintPage(sender, ppea, bc, m_sCAS, m_sSummaformel, m_sStreckkod, m_sComment, datum_max, m_sSkapat_datum, m_sUtgDatum, FormMain.Get.Username, m_sCabinet, m_sRoom);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void ReleaseDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void ReleaseDataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }


        private void ReleaseDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 10)
            {

                ch1.Value = ReleaseDataGridView.CurrentCell.Value;
                ch2.Value = ReleaseDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex+1].Value;
                if (ch1.Value.Equals(ch1.TrueValue))
                {
                    ReleaseDataGridView.CurrentCell.Value = false;                    
                    ch1.Value = ReleaseDataGridView.CurrentCell.Value;
                    ReleaseDataGridView.Rows[e.RowIndex].Cells[10].Value = System.DBNull.Value;
                    ReleaseDataGridView.Rows[e.RowIndex].Cells[11].Value = System.DBNull.Value;
                   
                }
                else
                {
                    ReleaseDataGridView.CurrentCell.Value = true;
                    ch1.Value = ReleaseDataGridView.CurrentCell.Value;
                    if (Convert.ToBoolean(ReleaseDataGridView.Rows[e.RowIndex].Cells[10].Value))
                        ReleaseDataGridView.Rows[e.RowIndex].Cells[11].Value = false;
                }

            }
            else
            {
                ch2.Value = ReleaseDataGridView.CurrentCell.Value;
                if (ch2.Value.Equals(ch2.TrueValue))
                {
                    ReleaseDataGridView.CurrentCell.Value = false;
                    ch2.Value = ReleaseDataGridView.CurrentCell.Value;
                    ReleaseDataGridView.Rows[e.RowIndex].Cells[11].Value = System.DBNull.Value;
                    ReleaseDataGridView.Rows[e.RowIndex].Cells[10].Value = System.DBNull.Value;
                }
                else
                {
                    ReleaseDataGridView.CurrentCell.Value = true;
                    ch2.Value = ReleaseDataGridView.CurrentCell.Value;
                    if (Convert.ToBoolean(ReleaseDataGridView.Rows[e.RowIndex].Cells[11].Value))
                        ReleaseDataGridView.Rows[e.RowIndex].Cells[10].Value = false;
                }
            }


        }

        private void ReleaseDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            barcode.BarcodeService ws = FormMain.getBarcodeService();
            this.ReleaseDataGridView.CurrentCell = this.ReleaseDataGridView[10, 0];
            foreach (DataGridViewRow row in ReleaseDataGridView.Rows)
            {

                if (row.Cells[10].Value.Equals(System.DBNull.Value) && row.Cells[11].Value.Equals(System.DBNull.Value))
                    {

                    }
                else if( row.Cells[10].Value.Equals(ch1.TrueValue))
                    {
                        string barcode = row.Cells[0].Value.ToString();
                        string invid = row.Cells[12].Value.ToString();
                        string aaa = row.Cells[10].Value.ToString();
                        bool uppd = Convert.ToBoolean(ws.NOT_Kpinvtrans(databas, kemiDb, invid, barcode, userId,userStringName));
                    }
                else
                {
                    string barcode = row.Cells[0].Value.ToString();
                    string invid = row.Cells[12].Value.ToString();
                    string aaa = row.Cells[10].Value.ToString();
                    bool uppd = Convert.ToBoolean(ws.UpdateKpinvtrans(databas, kemiDb, invid, barcode, userId, userStringName));
                }



            }
            this.Close();
        }
    }
}



