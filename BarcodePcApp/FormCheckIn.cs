using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Drawing.Printing;
using System.Timers;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Globalization;
using System.Text.RegularExpressions;
using BarcodePcApp.Misc;
using System.Web.UI.WebControls;

namespace BarcodePcApp
{
    public partial class FormCheckIn : Form
    {
        private int verkId;
        private int userId;
        private string userName;
        private string userStringDepart;
        private string userStringName;
        private string userStringSys;
        private int orgNod = -1;
        private int orgAr;
        private string databas;
        private string kemiDb;
        private string orgNamn;
        private int lokalId;
        private int storageId;
        private int huvudEnhetId;
        private string dateTimePickern;
        private string lts = "";
        private string manaden;
        private string dagen;
        private string m_sCAS = "";
        private string m_sStreckkod = "";
        private string m_sSummaformel = "";
        private string m_sSkapat_datum = "";
        private string m_sUtgangsdatum = "";
        private string m_sRumNamn = "";
        private string m_sComment = "";
        private int m_iGas = 0;
        private int m_EgenskapID;
        private string m_EgenskapNamn = "";
        private System.Timers.Timer scannerTim = new System.Timers.Timer();
        private string barcodeInSettings = "";
        private bool Log;
        private bool Log1;
        private string m_sUser = "";
        private int MaxLab_verkid;
        public string datum_max = "";
        public string datum_layout6 = "";
        private int levid;
        public string PropName = "";
        private int artik;
        public string dat = ""; //för layout 6
        public string init = ""; //för layout 6
        private static string lokaldb;  //LokalDB projekt laggt till lokaldb
        private bool lang;  //ver 1.27 översättning eng-sve
        private bool Imp = false;   // för importland
        public int importYes = 0;
        public int landid=0;
        private int sprak;




        DataTable m_dt = new DataTable();
        static private FormMain m_Main = null;

        //private static string xmlHead = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n";

        public int m_nProdid = -1;
        public string m_sProdnamn = "";
        public int m_lokalID = -1;
        static private FormCheckIn m_Checkin = null;
        public int m_nStorProperty;
        public int m_nProdProperty;
        public string Cas="";
        public string leverantorid = "";
        public string m_sUtgDatum = "";
        public string m_sSkDatum;  //Sandi
        //public string m_sUtgDatum;  //Sandi
        public string m_sAvancerad = "";
        public string m_sRelease = "";
        public string m_sOrange = ""; 
        public int granskad = 0;
        public int m_Leverantor;
        


        public string m_sRoom = "";
        public string m_sCabinet = "";
        //public string m_sFriText = "";
        //public string[] m_aIncheckadProdukt = { "", "", "", "", "" }; //id, produkt, rum, skåp, egen text

        public ArrayList m_aAllaIncheckade = new ArrayList();

        public struct ProdStruct
        {
            public int m_nProdid;
            public string m_sProdnamn;
            public string m_sRoom;
            public string m_sCabinet;
            public string m_sAmount;
            public string m_sUnit;
            public string m_sFriText;
            public string m_sKemsikbet;
            public int m_nStreckkodid;
            public string m_sStreckkod;
            public string m_sSummaformel;
            public string m_sCAS;
            public string m_sComment;
            public string m_sProperty;  //Sandi
            public int m_nStorProperty;
            public int m_nProdProperty;
            public int m_nlev;//Sandi

            public string m_sSkDatum;  //Sandi
            public string m_sUtgDatum;  //Sandi
            
        }

        public FormCheckIn()
        {
            InitializeComponent();


            this.KeyPress += new KeyPressEventHandler(form_KeyPress);

            scannerTim.Interval = 100;
            scannerTim.Elapsed += scannerTim_Elapsed;            

            //Skapa initialer
            string name123 = FormMain.Get.Usernamestring;
            Regex initials = new Regex(@"(\b[a-öA-Ö])[a-öA-Ö]* ?");
            init = initials.Replace(name123, "$1");

            //barcode.BarcodeService bc = FormMain.getBarcodeService();
            //barcodeInSettings = bc.GetLoginBarcode(databas, FormMain.Get.UserId);
            
            FormSettings form = new FormSettings();
            barcodeInSettings = form.loginBarcode;
            buttonCheckIn.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            cboChooseArticle.Visible = false;
            btnNewArt.Visible = false;
            txtBatch.Visible = false;
            txtBestBefore.Visible = false;
            panelViktig.Visible = false;
            btnOKViktig.Visible = false;
            txtBoxViktig.Visible = false;

            labelImpLand.Enabled = false;
            labelImpLand.Visible = false;
            cboImpLand.Enabled = false;
            cboImpLand.Visible = false;
            pictureBoxImportLand.Enabled = false;
            pictureBoxImportLand.Visible = false;
            this.lblProperty.Location = new System.Drawing.Point(26, 497);
            this.cboProperty.Location = new System.Drawing.Point(139, 497);
            pictureBox4.Location = new System.Drawing.Point(376, 498);




            lang = FormMain.Get.LangChange;

            if (lang == false)
            {
                this.labelCheckIn.Text = "Register products";
                this.Text = "Register products and print labels.";
                this.labelSearch.Text = "Search product:";
                this.label4.Text = "Choose department: *";
                this.labelRoom.Text = "Choose room:";
                this.labelCabinet.Text = "Choose cabinet:";
                this.labelSupplier.Text = "Choose supplier:";
                this.label2.Text = "Choose amount: *";
                this.label3.Text = "Number of labels: *";
                this.label1.Text = "Note:";
                this.lblProperty.Text = "Choose property:";
                this.buttonSokProd.Text = "Search";
                this.label5.Text = "         Show \r\nmolecular structure";
                this.toolTipSearch.SetToolTip(this.pictureBox2, "Select a department.");
                this.toolTipSearch.SetToolTip(this.pictureBoxRoom, "Select a room in which you store the chemical.");
                this.toolTipSearch.SetToolTip(this.pictureBoxCabinet, "Select a cabinet in which you store the chemical.");
                this.toolTipSearch.SetToolTip(this.pictureBox5, "Select a supplier");
                this.toolTipSearch.SetToolTip(this.pictureBox4, "Select a property for your product.");
                this.toolTipSearch.SetToolTip(this.pictureBox1, "Select the numbers of labels.");
                this.toolTipSearch.SetToolTip(this.pictureBoxAmount, "Select an amount and a unit.");
                this.toolTipSearch.SetToolTip(this.pictureBox3, "(Optional) Enter a comment. If you have selected Layout 4, only the first 6\r\nchar" + "acters will be printed on your label.");
                this.toolTipSearch.SetToolTip(this.pictureBoxImportLand, "Enter the country you are buying from if you are importing the product to Sweden." +
" If your country is missing from the list, contact your administrator"); 
                this.m_btnSelect.Text = "Select";
                this.buttonPrint.Text = "Print label";
                this.label6.Text = "Choose article";
                this.chkUser.Text = "Scan user and close";
                this.buttonCheckIn.Text = "Check In";
                this.btnNewArt.Text = "Add new";
                this.label7.Text = "Article missing";
                this.label8.Text = "Batch no.";
                this.label9.Text = "Use before";
                this.toolTipSearch.SetToolTip(this.pictureBoxSearch, "Search by CAS No. or by product name (minimum three characters). Search by exact match or truncated (*), search result is limited to 500 hits. Select a product from the list. If no hit please contact your administrator.");
                this.buttonClose.Text = "Close";
                this.groupBox1.Text = "Registered products";
                this.label5.Text = "         Show \r\nmolecular structure";
                this.labelImpLand.Text = "Imported from: *";


                m_dt.Columns.Add("Product", Type.GetType("System.String"));
                m_dt.Columns.Add("Barcode", Type.GetType("System.String"));
                m_dt.Columns.Add("Room", Type.GetType("System.String"));
                m_dt.Columns.Add("Cabinet", Type.GetType("System.String"));
                m_dt.Columns.Add("Amount", Type.GetType("System.String"));
                m_dt.Columns.Add("Unit", Type.GetType("System.String"));
                m_dt.Columns.Add("Date", Type.GetType("System.String"));
                m_dt.Columns.Add("Formula", Type.GetType("System.String"));
                m_dt.Columns.Add("Print", typeof(Bitmap));
                m_dt.Columns.Add("Note", Type.GetType("System.String"));
                sprak = 1;



            }
            else
            {
                this.labelCheckIn.Text = "Registrera produkter";
                this.Text = "Registrera produkter och skriv ut etiketter.";
                this.labelSearch.Text = "Sök produkter:";
                this.label4.Text = "Välj avdelning: *";
                this.labelRoom.Text = "Välj rum:";
                this.labelCabinet.Text = "Välj skåp:";
                this.labelSupplier.Text = "Välj leverantör:";
                this.label2.Text = "Välj mängd: *";
                this.label3.Text = "Antal etiketter: *";
                this.label1.Text = "Anteckningar:";
                this.lblProperty.Text = "Välj egenskaper:";
                this.buttonSokProd.Text = "Sök";
                this.label5.Text = "         Visa \r\nmolekulär struktur";
                this.toolTipSearch.SetToolTip(this.pictureBox2, "Välj avdelning.");
                this.toolTipSearch.SetToolTip(this.pictureBoxRoom, "Välj ett rum där du vill spara dina kemikalier.");
                this.toolTipSearch.SetToolTip(this.pictureBoxCabinet, "Välj ett skåp där du vill spara dina kemikalier");
                this.toolTipSearch.SetToolTip(this.pictureBox5, "Välj leverantör");
                this.toolTipSearch.SetToolTip(this.pictureBox4, "Välj produktegenskap.");
                this.toolTipSearch.SetToolTip(this.pictureBox1, "Välj antal etiketter.");
                this.toolTipSearch.SetToolTip(this.pictureBoxAmount, "Välj antal och enhet.");
                this.toolTipSearch.SetToolTip(this.pictureBox3, "Skriv en kommentar. Om du valt layout 4, bara 6 första\r\ntecken" + " kommer att skrivas ut på etiketten.");
                this.toolTipSearch.SetToolTip(this.pictureBoxImportLand, "Ange här vilket land du köper från om du importerar produkten till Sverige. " +
" Saknas ditt land i listan kontaktar du din administratör."); 
                this.m_btnSelect.Text = "Välj";
                this.buttonPrint.Text = "Skriv ut etiketter";
                this.label6.Text = "Välj en artikel";
                //this.chkUser.Text = "Skanna användare och stäng";
                this.buttonCheckIn.Text = "Checka in";
                this.btnNewArt.Text = "Lägg till ny";
                this.label7.Text = "Artikel saknas";
                this.label8.Text = "Batch nr.";
                this.label9.Text = "Bäst före \r\n     datum";
                this.toolTipSearch.SetToolTip(this.pictureBoxSearch, "Sök med CAS nr eller produktnamn (minimum tre tecken)");
                this.buttonClose.Text = "Stäng";
                this.groupBox1.Text = "Registrerade produkter";
                this.label5.Text = "         Visa \r\nmolekylär struktur";
                this.labelImpLand.Text = "Importerat från: *";




                m_dt.Columns.Add("Produkt", Type.GetType("System.String"));
                m_dt.Columns.Add("Streckkod", Type.GetType("System.String"));
                m_dt.Columns.Add("Rum", Type.GetType("System.String"));
                m_dt.Columns.Add("Skåp", Type.GetType("System.String"));
                m_dt.Columns.Add("Mängd", Type.GetType("System.String"));
                m_dt.Columns.Add("Enhet", Type.GetType("System.String"));
                m_dt.Columns.Add("Datum", Type.GetType("System.String"));
                m_dt.Columns.Add("Summaformel", Type.GetType("System.String"));
                m_dt.Columns.Add("Skriv ut", typeof(Bitmap));
                m_dt.Columns.Add("Kommentar", Type.GetType("System.String"));
                sprak = 0;



            }


            if (FormMain.Get.BarcodeLayout == 4)
            {
                this.pictureBox3.Visible = false;
                //System.Windows.Forms.ToolTip tt = new System.Windows.Forms.ToolTip();
                this.toolTipSearch.SetToolTip(this.pictureBox3, "If you have selected Layout 5, you need to fill in 'Note' according to your organisation's requirements. ");
                this.pictureBox3.Visible = true;

                //this.pictureBox3 = this.pictureBox10;
            }

            Log = FormMain.Get.PropertyCheck;
            if (Log == true)
            {
                lblProperty.Visible = true;
                cboProperty.Visible = true;
                pictureBox4.Visible = true;
            }
            else
            {
                lblProperty.Visible = false;
                cboProperty.Visible = false;
                pictureBox4.Visible = false;

            }


            comboBoxCabinet.Enabled = false;


            resultDataGridView.DataSource = m_dt;


        }

        public enum Property
        {
            Syra = 1,
            Bas = 2,
            Omärkt = 0
        };

        public int VerkId { set { verkId = value; } }
        public int UserId { set { userId = value; } }
        public string UserName { set { userName = value; } }
        public string UserStringDepart { set { userStringDepart = value; } }
        public string UserStringName { set { userStringName = value; } }
        public string UserStringSys { set { userStringSys = value; } }
        public int OrgNod { set { orgNod = value; } }
        public int OrgAr { set { orgAr = value; } }
        public string OrgNamn { set { orgNamn = value; } }
        public string Databas { set { databas = value; } }
        public string KemiDb { set { kemiDb = value; } }
        public int LokalId { set { lokalId = value; } }
        public int StorageId { set { storageId = value; } }
        public int HuvudEnhetId { set { huvudEnhetId = value; } }
        public string DateTimePickern { set { dateTimePickern = value; } }
        public string Lts { set { lts = value; } }
        public string Manaden { set { manaden = value; } }
        public string Dagen { set { dagen = value; } }
        public int Levid { set { levid = value; } } //Sandi
        public int artikel { set { artik = value; } } //Sandi
        //public string LokalDB { get { return lokaldb; } }//LokalDB projekt laggt till lokaldb
        public string LokalDB { set { lokaldb = value; } }//LokalDB projekt laggt till lokaldb






        private void buttonSokProd_Click(object sender, System.EventArgs e)
        {
            comboBoxAmount.DataSource = null;
            comboBoxAmount.DisplayMember = "";
            comboBoxAmount.ValueMember = "";
            comboBoxAmount.Text = "";

            textSokProd.Text = textSokProd.Text.TrimEnd();
            textSokProd.Text = textSokProd.Text.TrimStart();

            if (textSokProd.Text.Length < 3)
            {
                if (lang ==false)
                {
                    MessageBox.Show("Type in at least three letters as a search criteria.");
                }
                else
                {
                    MessageBox.Show("Skriv in minst 3 tecken i sökfältet.");
                }
                
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            barcode.BarcodeService bc = FormMain.getBarcodeService();
            string prodString = "";
            try
            {
                prodString = bc.SokProd(textSokProd.Text, kemiDb, databas,verkId);

            }
            catch (Exception err)
            {
                MessageBox.Show("No contact with the web server!\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "SokProd");
                bc.Dispose();
                return;
            }

            DataSet ds = new DataSet();
            StringReader sr = new StringReader(prodString);
            ds.ReadXml(sr);


            DataTable dt = ds.Tables[0];
            //DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();
            //iconColumn.Image = BarcodePcApp.Properties.Resources.Molekyl_col;
            ////dt.Columns.Add(new DataColumn("MolStrukt", typeof(Bitmap)));

            //DataTable dt = new DataTable();
            //DataColumn dc = new DataColumn("Molekyl1");
            //dc.DataType = System.Type.GetType("System.Byte[]");
            //dt.Columns.Add(dc);
            //DataRow row = dt.NewRow();
            //row["Molekyl1"] = Convert.ToByte(iconColumn);
            //dt.Rows.Add(row);
            //this.dataGridProd.DataSource = dt;

            //Image img = new System.Drawing.Bitmap(BarcodePcApp.Properties.Resources.Molekyl_col);
            //var dcol = new DataColumn("ImageColumn");
            //dcol.DataType = System.Type.GetType("System.Drawing.Bitmap");
            //dt.Columns.Add(dcol);

            //DataRow row = dt.NewRow();
            //row["ImageColumn"] = img;
            //dt.Rows.Add(row);
            //this.dataGridProd.DataSource = dt;
            //System.Drawing.Image image = BarcodePcApp.Properties.Resources.Molekyl_col;
            //dataGridProd[ = image; //make sure 1 represents the index of ImageColumn


            DataRow dr = dt.Rows[0];
            //dataGridProd.CaptionText = "Search result for products";
            if (Convert.ToInt32(dr["Id"].ToString()) < 1)
            {
                dataGridProd.DataSource = null;
                MessageBox.Show(dr["Prodnam"].ToString());
                return;
            }
            dataGridProd.SetDataBinding(dt, "");
            dataGridProd.Name = "SokProd";

            dataGridProd.TableStyles.Clear();
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = "Produkt";
            ts.AllowSorting = false;
            ts.ReadOnly = true;

            if (lang == false)
            {
                dataGridProd.CaptionText = "Search result for products";
                dataGridProd.TableStyles.Add(ts);
                //dataGridProd.TableStyles[0].GridColumnStyles["Molstrukt"].Width = 70; //nytt
                //dataGridProd.TableStyles[0].GridColumnStyles["Molstrukt"].HeaderText = "Molstrukt"; 
                dataGridProd.TableStyles[0].GridColumnStyles["Prodnam"].Width = 220;
                dataGridProd.TableStyles[0].GridColumnStyles["Prodnam"].HeaderText = "Product name";
                dataGridProd.TableStyles[0].GridColumnStyles["Synonym"].Width = 220;
                dataGridProd.TableStyles[0].GridColumnStyles["Cas"].Width = 70;
                dataGridProd.TableStyles[0].GridColumnStyles["EnhBen"].Width = 60;
                dataGridProd.TableStyles[0].GridColumnStyles["EnhBen"].HeaderText = "Unit";
                dataGridProd.TableStyles[0].GridColumnStyles["Enh"].Width = 0;
                dataGridProd.TableStyles[0].GridColumnStyles["Kemiskbet"].HeaderText = "Formula";
                dataGridProd.TableStyles[0].GridColumnStyles["gastub"].Width = 0;
                dataGridProd.TableStyles[0].GridColumnStyles["Id"].Width = 60;
            }
            else
            {
                dataGridProd.CaptionText = "Sökresultat för produkter";
                dataGridProd.TableStyles.Add(ts);
                //dataGridProd.TableStyles[0].GridColumnStyles["Molstrukt"].Width = 70; //nytt
                //dataGridProd.TableStyles[0].GridColumnStyles["Molstrukt"].HeaderText = "Molstrukt"; 
                dataGridProd.TableStyles[0].GridColumnStyles["Prodnam"].Width = 220;
                dataGridProd.TableStyles[0].GridColumnStyles["Prodnam"].HeaderText = "Produktnamn";
                dataGridProd.TableStyles[0].GridColumnStyles["Synonym"].Width = 220;
                dataGridProd.TableStyles[0].GridColumnStyles["Cas"].Width = 70;
                dataGridProd.TableStyles[0].GridColumnStyles["EnhBen"].Width = 60;
                dataGridProd.TableStyles[0].GridColumnStyles["EnhBen"].HeaderText = "Enhet";
                dataGridProd.TableStyles[0].GridColumnStyles["Enh"].Width = 0;
                dataGridProd.TableStyles[0].GridColumnStyles["Kemiskbet"].HeaderText = "Summaformel";
                dataGridProd.TableStyles[0].GridColumnStyles["gastub"].Width = 0;
                dataGridProd.TableStyles[0].GridColumnStyles["Id"].Width = 60;

            }
            ds.Dispose();
            bc.Dispose();
        }

        private void dataGridProd_Click(object sender, System.EventArgs e)
        {
            barcode.BarcodeService bc = FormMain.getBarcodeService();
            int molfinns=0;

            comboBoxAmount.DataSource = null;
            comboBoxAmount.DisplayMember = "";
            comboBoxAmount.ValueMember = "";
            comboBoxAmount.Text = "";

            int row = dataGridProd.CurrentRowIndex;
            if (row < 0) return;
            m_sProdnamn = Convert.ToString(dataGridProd.TableStyles[0].DataGrid[row, 0]);
            m_sCAS = Convert.ToString(dataGridProd.TableStyles[0].DataGrid[row, 2]);
            m_sSummaformel = Convert.ToString(dataGridProd.TableStyles[0].DataGrid[row, 5]);
            m_iGas = Convert.ToInt32(dataGridProd.TableStyles[0].DataGrid[row, 6]);
            m_nProdid = Convert.ToInt32(dataGridProd.TableStyles[0].DataGrid[row, 7]);
            if (dataGridProd.TableStyles[0].DataGrid[row, 4].ToString().Length > 0)
                huvudEnhetId = Convert.ToInt32(dataGridProd.TableStyles[0].DataGrid[row, 4]);
            else
                huvudEnhetId = 0;

            string finnsprod_q = bc.FinnsProd(databas, kemiDb, m_sCAS, m_nProdid,verkId,sprak);
            int OK = 0;
            XmlDocument doc1 = new XmlDocument();
            doc1 = new XmlDocument();
            doc1.LoadXml(finnsprod_q);
            XmlNodeList xmlnodeList1 = doc1.DocumentElement.SelectNodes("item");
            foreach (XmlNode item in xmlnodeList1)
            {
                OK = Convert.ToInt32(item.Attributes["finns"].Value);
            }

            if (OK == 1)
            {
                panelViktig.Visible = true;
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                if (lang == false)
                {
                    result = MessageBox.Show("This product is subject to certain regulatory and/or internal requirements.\r\nNew approval by Chemical Board will therefore be required  prior to next purchase.\r\n\nIf you have any questions, please contact Chemical Board.", "BarcodePcApp", buttons);
                }
                else
                {
                    result = MessageBox.Show("Denna produkt omfattas av lagkrav eller interna krav för användning.\r\nEtt nytt godkännade bör därför inhämtas från Chemical Board före nästa inköp.\r\n\nVid frågor kring detta, kontakta Cemical Board.", "BarcodePcApp", buttons);
                }

                //result = MessageBox.Show("This product is subject to certain regulatory and/or internal requirements.\r\nNew approval by Chemical Board will therefore be required  prior to next purchase.\r\n\nIf you have any questions, please contact Chemical Board.", "BarcodePcApp", buttons);
                //this.Close();
                //this.Owner.Close();
                panelViktig.Visible = false;
                //btnOKViktig.Visible = true;
                //MessageBox.Show("Visa info om viktig");
                //

            }


            DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();
            iconColumn.Image = BarcodePcApp.Properties.Resources.Molekyl_col;

            try
            {
                string molfinns_q = (bc.FinnsMolstrukt(databas, kemiDb, m_sCAS));
                XmlDocument doc2 = new XmlDocument();
                doc2= new XmlDocument();
                doc2.LoadXml(molfinns_q);
                XmlNodeList xmlnodeList2 = doc2.DocumentElement.SelectNodes("item");
                foreach (XmlNode item in xmlnodeList2)
                {
                    molfinns = Convert.ToInt32(item.Attributes["molstrukt"].Value);
                }

                //Om vald produkt saknar molstruktur, visa inte knappen Molstrukt
                if (molfinns == 0)
                {
                    groupBox2.Visible = false;
                }
                else
                {
                    groupBox2.Visible = true;
                }

            }
            catch (Exception err)
            {
                MessageBox.Show("No contact with the web server!\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "SokProd");
                bc.Dispose();
                return;
            }

            //PopulateProperties(databas, kemiDb); 
            if (Log == true)
            {
                PopulateProperties(databas, kemiDb, m_nProdid);
            }

            PopulateSupplier(databas, kemiDb, m_nProdid); 
            UpdateAmount();
            // Här kollas regel för ImportLand
            //XmlDocument doc12 = new XmlDocument();
            //string imp_q = bc.ImportLandRegel(databas, kemiDb, orgNod, orgar);
            //doc12 = new XmlDocument();
            //doc12.LoadXml(imp_q);
            //XmlNodeList xmlnodeList12 = doc12.DocumentElement.SelectNodes("item");
            //int antal = 0;
            //foreach (XmlNode item in xmlnodeList12)
            //{
            //    antal = Convert.ToInt32(item.Attributes["antal"].Value);
            //}
            //importYes = antal;
            //if (importYes>0)
            //{
                PopulateImpLand(databas, kemiDb, lokaldb, lang);
            //}

        }

        private void comboBoxRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxRoom.ValueMember.Length > 0)
            {
                
                UpdateCabinet();
                //Kolla om egenskaperna är lika
                storageId = Convert.ToInt32(comboBoxCabinet.SelectedValue);
                if (Log == true)
                {
                    //CheckProduct(databas, m_nProdid, m_EgenskapNamn, m_nProdProperty, lokalId, storageId);  
                    //cboProperty.Focus();
                }
                else
                {
                    textBoxAmount.Focus();
                }
            }

            this.txtBarcode.Focus();
            //txtBarcode.Focus();

        }


        private void comboBoxCabinet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCabinet.ValueMember.Length > 0)
            {
                if (Convert.ToInt32(comboBoxCabinet.SelectedValue) > 0)
                {
                    storageId = Convert.ToInt32(comboBoxCabinet.SelectedValue);
                    if (Log == true)
                    {
                        //CheckProduct(databas, m_nProdid, m_EgenskapNamn, m_nProdProperty, lokalId, storageId);
                        //cboProperty.Focus();
                        textBoxAmount.Focus();
                    }
                    else
                    {
                        textBoxAmount.Focus();
                    }
                }
            }
            this.txtBarcode.Focus();
        }

        #region Utskrift till etikettskrivaren

       

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            // Själva utskriften
            //-----------------------------------------------------------------
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
                if (lang == false)
                {
                    MessageBox.Show("No label printer is installed");
                }
                else
                {
                    MessageBox.Show("Skrivaren är inte installerad");
                }


                //MessageBox.Show("No label printer is installed");
                return;
            }

            m_sComment = m_Comment.Text;
            if (lang == false)
            {
                pd.DocumentName = "Barcode for the product";
            }
            else
            {
                pd.DocumentName = "Streckkod för produkten";
            }

            //pd.DocumentName = "Barcode for the product";
            pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocumentOnPrintPage);

            if (textBoxAmount.Text.Contains("."))
            {
                textBoxAmount.Text = textBoxAmount.Text.Replace(".", ",");
            }

            if (dataGridProd.CurrentRowIndex < 0)
            {
            }
            else if (m_nProdid <= 0)
            {
                if (lang == false)
                {
                    MessageBox.Show("You must select a product.");
                }
                else
                {
                    MessageBox.Show("Du måste markera en produkt.");
                }

                
            }
            else if (orgNod <= 0)
            {
                if (lang == false)
                {
                    MessageBox.Show("You must select a department.");
                }
                else
                {
                    MessageBox.Show("Du måste välja en avdelning.");
                }

            }
            else if (textBoxAmount.Text.ToString().Length > 0 && (comboBoxAmount.Text == ""))
            {
                if (lang == false)
                {
                    //comboBoxAmount.BackColor = Color.Red;
                    MessageBox.Show("If an amount has been entered, please choose a unit");
                }
                else
                {
                    //comboBoxAmount.BackColor = Color.Red;
                    MessageBox.Show("Om mängden finns så måste enhet anges");
                }

            }
            else if (importYes > 0 && cboImpLand.SelectedIndex < 1)
            {
                if (lang == false)
                {
                    cboImpLand.BackColor = Color.FromArgb(255, 204, 204);
                    MessageBox.Show("You must select an import country");
                }
                else
                {
                    cboImpLand.BackColor = Color.FromArgb(255, 204, 204);
                    MessageBox.Show("Du måste välja ett importland");
                }

            }
            else if (textBoxAmount.Text.ToString().Length == 0 || !FormMain.IsNumeric(textBoxAmount.Text))
            {
                if (lang == false)
                {
                    System.Drawing.Font newFont = new Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 178, false);
                    label2.Font = new Font(newFont, FontStyle.Bold);
                    label2.ForeColor = Color.Red;
                    //MessageBox.Show("You must enter a numeric value in the amount field.");
                    textBoxAmount.BackColor = Color.FromArgb(255, 204, 204);
                    textBoxAmount.Focus();
                }
                else
                {
                    //MessageBox.Show("Du måste ange ett numeriskt värde i mängdfältet.");
                    System.Drawing.Font newFont = new Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 178, false);
                    label2.Font = new Font(newFont, FontStyle.Bold);
                    label2.ForeColor = Color.Red;
                    textBoxAmount.BackColor = Color.FromArgb(255, 204, 204);
                    textBoxAmount.Focus();
                }

            }
            else if (textBoxAmount.Text.Length == 0 || double.Parse(textBoxAmount.Text) == 0)
            {
                if (lang == false)
                {
                    //MessageBox.Show("Please enter an amount.");
                    textBoxAmount.BackColor = Color.FromArgb(255, 204, 204);
                    textBoxAmount.Focus();
                }
                else
                {
                    //MessageBox.Show("Ange mängden.");
                    textBoxAmount.BackColor = Color.FromArgb(255, 204, 204);
                    textBoxAmount.Focus();
                }

                
            }
            else if (m_textNumber.Text.ToString().Length == 0 || !FormMain.IsNumeric(m_textNumber.Text))
            {
                if (lang == false)
                {
                    //MessageBox.Show("You must specify the number of labels.");
                    m_textNumber.BackColor = Color.FromArgb(255, 204, 204);
                    m_textNumber.Focus();
                }
                else
                {
                    //MessageBox.Show("Du måste ange antalet etiketter.");
                    m_textNumber.BackColor = Color.FromArgb(255, 204, 204);
                    m_textNumber.Focus();
                }

               
            }
            else if (Convert.ToInt32(m_textNumber.Text) > 100)
            {
                if (lang == false)
                {
                    //MessageBox.Show("To many labels on one entry (max 100).");
                    System.Drawing.Font newFont = new Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 178, false);
                    label3.Font = new Font(newFont, FontStyle.Bold);
                    label3.ForeColor = Color.Red;
                    m_textNumber.BackColor = Color.FromArgb(255, 204, 204);
                    m_textNumber.Text = "To many labels on one entry (max 100).";
                    //m_textNumber.Focus();
                }
                else
                {
                    System.Drawing.Font newFont = new Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 178, false);
                    label3.Font = new Font(newFont, FontStyle.Bold);
                    label3.ForeColor = Color.Red;
                    m_textNumber.BackColor = Color.FromArgb(255, 204, 204);
                    m_textNumber.Text = "För många etiketter (max 100).";
                    //m_textNumber.Focus();
                    //MessageBox.Show("För många etiketter (max 100).");
                }

                
            }
            else if (m_sComment.Length >= 255)
            {
                if (lang == false)
                {
                    MessageBox.Show("Comment field is too long, please enter a comment shorten than 255 characters.");
                }
                else
                {
                    MessageBox.Show("Kommentarsfältet är för långt. Ange en kommentar som är mindre än 255 tecken.");
                }

                
            }
            else if (m_sComment.Length <= 0 && FormMain.Get.BarcodeLayout == 4)
            {
                if (lang == false)
                {
                    MessageBox.Show("If you have selected Layout 5, you need to fill in 'Note' according to your organisation's requirements.");
                }
                else
                {
                    MessageBox.Show("Om du valt layout 5, måste du fylla i Anteckningar enligt din organisations krav");
                }

                
                m_Comment.Focus();
            }
            else if ( txtBestBefore.Text.ToString().Length > 8 )
            {
                if (lang == false)
                {
                    MessageBox.Show("Best Before field is too long, use YYYYMMDD.");
                }
                else
                {
                    MessageBox.Show("Bäst före datum är för lång, använd format YYYYMMDD");
                }

                
                txtBestBefore.Focus();
            }

            else
            {
                if (dateTimePicker1.Value.Date.Month.ToString().Length == 1)
                {
                    manaden = "0" + dateTimePicker1.Value.Date.Month.ToString();
                }
                else
                {
                    manaden = dateTimePicker1.Value.Date.Month.ToString();
                }
                if (dateTimePicker1.Value.Date.Day.ToString().Length == 1)
                {
                    dagen = "0" + dateTimePicker1.Value.Date.Day.ToString();
                }
                else
                {
                    dagen = dateTimePicker1.Value.Date.Day.ToString();
                }


                dateTimePickern = dateTimePicker1.Value.Date.Year.ToString();
                dateTimePickern = dateTimePickern + manaden;
                dateTimePickern = dateTimePickern + dagen;

                int pRow = dataGridProd.CurrentRowIndex;
                if (pRow < 0) return;

                BindingManagerBase bmProd = this.dataGridProd.BindingContext[this.dataGridProd.DataSource, this.dataGridProd.DataMember];
                DataRow drProd = ((DataRowView)bmProd.Current).Row;

                string cas = dataGridProd.TableStyles[0].DataGrid[pRow, 2].ToString().Trim();

                string drProden = drProd["id"].ToString().Trim(); //Varför är denna string??
                int drProdid = Convert.ToInt32(drProd["id"]);
                string drProdnam = drProd["Prodnam"].ToString().Trim();
                string drEnhBen = drProd["EnhBen"].ToString().Trim(); //Huvudenheten
                string kemiskbet = drProd["Kemiskbet"].ToString().Trim();
                //string cas2 = dataGridProd.TableStyles[0].DataGrid[pRow, 7].ToString().Trim(); //nytt
                int drEnh = 0;
                if (drProd["Enh"].ToString().Length > 0)
                    drEnh = Convert.ToInt32(drProd["Enh"]);

                barcode.BarcodeService bs = FormMain.getBarcodeService();

                string datumet = "";
                /*
                if (lts.Length > 0)
                    datumet = lts;
                else
                    datumet = dateTimePickern;
                */

                ////// Test datumKonvertering för MaxLab (fungerar). Datumet blir 14-10-06
                string dat = dateTimePickern;

                m_sSkapat_datum = init+"/"+dat;
                m_sUtgangsdatum = txtBestBefore.Text;
                //DateTime s = DateTime.ParseExact(txtBestBefore.Text, "yyyymmdd", System.Globalization.CultureInfo.InvariantCulture);
                //m_sUtgangsdatum = s.ToString("yyyymmdd");
                 DateTime d = DateTime.ParseExact(dat, "yyyymmdd", System.Globalization.CultureInfo.InvariantCulture);
                datum_max = d.ToString("yy-mm-dd");
                ////////////////////////////////////////////////                

                double mangden = 0;
                if (textBoxAmount.Text.Length > 0)
                    mangden = Convert.ToDouble(textBoxAmount.Text);

                int antal = Convert.ToInt32(m_textNumber.Text);
                //string kommentar = Convert.ToString(m_Comment); //test 

                string enheten = comboBoxAmount.Text; //Den valda enheten i text
                int enhetId = Convert.ToInt32(comboBoxAmount.SelectedValue); //Den valda enhetens id
                if (comboBoxSupplier.SelectedIndex == -1)
                {
                    int Leverantor = 0;
                    m_Leverantor = Leverantor;
                }
                else
                {
                    int Leverantor = Convert.ToInt32(comboBoxSupplier.SelectedValue);
                    m_Leverantor = Leverantor;
                }

                int LEV = m_Leverantor;

                //landid = Convert.ToInt32(cboImpLand.SelectedValue); //landets id




                // Hantering som gäller för Produktegenskaper
                if (Log == true)
                {

                    m_EgenskapNamn = Convert.ToString(cboProperty.Text);

                    string egid_q = bs.GetPropID(databas, m_EgenskapNamn);
                    XmlDocument doc5 = new XmlDocument();
                    doc5 = new XmlDocument();
                    doc5.LoadXml(egid_q);
                    XmlNodeList xmlnodeList5 = doc5.DocumentElement.SelectNodes("item");
                    int egenskapsid = 0;
                    foreach (XmlNode item in xmlnodeList5)
                    {
                        egenskapsid = Convert.ToInt32(item.Attributes["id"].Value);
                    }
                    m_EgenskapID = egenskapsid;
                    //m_EgenskapID = Convert.ToInt32(bs.GetPropID(databas, m_EgenskapNamn));

                    //int ProdProperty = Convert.ToInt32(bs.GetPropName(databas, m_nProdid));
                    m_nProdProperty = m_EgenskapID;

                    //////////////////////////////////////////
                    string StorageProperty_q = bs.GetRoomProp(databas, lokalId, storageId,lokaldb);
                    XmlDocument doc1 = new XmlDocument();
                    doc1 = new XmlDocument();
                    doc1.LoadXml(StorageProperty_q);
                    XmlNodeList xmlnodeList1 = doc1.DocumentElement.SelectNodes("item");
                    int StorageProperty=0;
                    foreach (XmlNode item in xmlnodeList1)
                    {
                        StorageProperty = Convert.ToInt32(item.Attributes["id"].Value);
                    }
                    m_nStorProperty = StorageProperty;

                    //CheckProduct(databas, m_nProdid, m_EgenskapNamn, m_nProdProperty, lokalId, storageId);

                    //// Skriv inte ut om egenskaperna ej identiska
                    if ((m_nStorProperty == m_nProdProperty) || (m_nStorProperty == Convert.ToInt32(Property.Omärkt)))
                    {

                        string xmlBarcode = "";
                        try
                        {
                            xmlBarcode = bs.CheckInAQBarCode(kemiDb, databas, verkId, userId, drProden, datumet, userName, mangden, enheten, antal, FormMain.Get.BarcodePrefix);
                            bc.Dispose(); 
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show("No contact with the web server!\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "CheckInAQBarCode");
                            bc.Dispose();
                        }

                        storageId = 0;
                        if (comboBoxCabinet.SelectedItem != null)
                            storageId = Convert.ToInt32(comboBoxCabinet.SelectedValue);

                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(xmlBarcode);
                        XmlNodeList xmlnodeList = doc.DocumentElement.SelectNodes("streckKodsId");

                        foreach (XmlNode node in xmlnodeList)
                        {
                            int streckkodsid = Convert.ToInt32(node.Attributes["id"].Value);
                            string streckkod = FormMain.Get.BarcodePrefix + streckkodsid;
                            int artid = Convert.ToInt32(cboChooseArticle.SelectedValue);
                            if (txtBestBefore.Text == "" || txtBestBefore.Text == "YYYYMMDD")
                            {
                                txtBestBefore.Clear();
                                txtBestBefore.Text = "0";
                            }
                            try
                            {
                                //LokalDB projekt laggt till lokaldb
                                string sRet = bs.CheckInKpinventory(kemiDb, databas,lokaldb, verkId, userId, drProdid, drProdnam, orgNod, lokalId, storageId, userName,enhetId, streckkodsid, mangden, drEnh, streckkod, m_sComment, m_iGas,m_Leverantor,m_EgenskapID,txtBatch.Text,Convert.ToDouble(txtBestBefore.Text),artid,m_sRelease,landid);                         
                                //string sRet = bs.CheckInKpinventory(kemiDb, databas, verkId, userId, drProdid, drProdnam, orgNod, lokalId, storageId, userName, enhetId, streckkodsid, mangden, drEnh, streckkod, m_sComment, m_iGas, Leverantor);

                            }
                            catch (Exception err)
                            {
                                MessageBox.Show("No contact with the web server!\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "CheckInKpinventory");
                                bc.Dispose();
                            }

                            if (FormMain.Get.BarcodeLayout == 0)
                            {
                                if (cas.Length > 0)
                                    m_sCAS = "CAS: " + cas.Trim();
                                else
                                    m_sCAS = "";
                                bc.DisplayCode = true;
                            }
                            else if (FormMain.Get.BarcodeLayout == 1)
                            {
                                if (cas.Length > 0)
                                    m_sCAS = cas.Trim();
                                else
                                    m_sCAS = "";

                                bc.Text = "";
                                bc.BarHeight = 4;
                                bc.DisplayCode = true;
                            }
                            else if (FormMain.Get.BarcodeLayout == 2)
                            {
                                m_sStreckkod = streckkod;
                                m_sSummaformel = kemiskbet;
                                if (cas.Length > 0)
                                    m_sCAS = cas.Trim();
                                else
                                    m_sCAS = "";

                                bc.Text = "";
                                bc.BarHeight = 4;
                                bc.DisplayCode = false;
                            }
                            else if (FormMain.Get.BarcodeLayout == 3)
                            {
                                m_sStreckkod = streckkod;
                                m_sSummaformel = kemiskbet;
                                if (cas.Length > 0)
                                    m_sCAS = cas.Trim();
                                else
                                    m_sCAS = "";

                                bc.Text = "";
                                bc.BarHeight = 4;
                                bc.DisplayCode = false;
                            }

                            //Gäller för MaxLab
                            else if (FormMain.Get.BarcodeLayout == 4)
                            {
                                m_sStreckkod = streckkod;
                                m_sSummaformel = datum_max;

                                if (cas.Length > 0)
                                    m_sCAS = cas.Trim();
                                else
                                    m_sCAS = "";

                                bc.Text = "";
                                bc.BarHeight = 4;
                                bc.DisplayCode = false;
                            }

                            //Etikettlayout 6
                            else if (FormMain.Get.BarcodeLayout == 5)
                            {
                                m_sStreckkod = streckkod;
                                m_sSkDatum=m_sSkapat_datum;
                                if (comboBoxCabinet.Text == "No cabinet")
                                {
                                    m_sCabinet = "0"; 
                                }
                                else
                                {
                                    m_sCabinet = comboBoxCabinet.Text; //hylla
                                }
                                m_sRumNamn = comboBoxRoom.Text; //Rum   
                                m_sUtgangsdatum = txtBestBefore.Text; // Utgångsdatum
                                if (m_sUtgangsdatum == "0")
                                {
                                    m_sUtgangsdatum = "";                                
                                }

                                bc.Text = "";
                                bc.BarHeight = 4;
                                bc.DisplayCode = false;
                            }
                            bc.Code = streckkod;

                            //
                            // Skapa ny streckkod i AQBarcode och AQBarKopl
                            //-----------------------------------------------------------------                    

                            pd.Print();
                            pd.Dispose(); //Lagt till

                            //Uppdatera om hantering av produktegenskap vald i inifil
                            if (Log == true)
                            {
                                UpdateProdEgenskaper(databas, m_nProdid, m_EgenskapNamn);
                            }
                            UpdateSupplier(kemiDb, m_nProdid, m_Leverantor);

                            // Lägger in all inmatad data i resultDataGridView
                            //-----------------------------------------------------------------
                            ProdStruct pStruct = new ProdStruct();
                            pStruct.m_nProdid = m_nProdid;
                            pStruct.m_sProdnamn = m_sProdnamn;
                            pStruct.m_sRoom = comboBoxRoom.Text;
                            pStruct.m_sCabinet = comboBoxCabinet.Text;
                            if (lts.Length > 0)
                                pStruct.m_sFriText = lts;
                            else
                                pStruct.m_sFriText = dateTimePickern;
                            pStruct.m_sStreckkod = streckkod;
                            pStruct.m_nStreckkodid = streckkodsid;
                            pStruct.m_sAmount = textBoxAmount.Text;
                            pStruct.m_sUnit = comboBoxAmount.Text;
                            pStruct.m_sKemsikbet = kemiskbet;
                            pStruct.m_sSummaformel = m_sSummaformel;
                            pStruct.m_sCAS = m_sCAS;
                            pStruct.m_sComment = m_Comment.Text;
                            pStruct.m_sSkDatum = m_sSkapat_datum;
                            pStruct.m_sUtgDatum = m_sUtgangsdatum;

                            ////// Test datumKonvertering för MaxLab (fungerar). Datumet blir 14-10-06
                            //string dat = pStruct.m_sFriText;
                            //DateTime d = DateTime.ParseExact(dat, "yyyymmdd", System.Globalization.CultureInfo.InvariantCulture);
                            //string datum111 = d.ToString("yy-mm-dd");
                            ////////////////////////////////////////////////


                            DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();
                            iconColumn.Image = BarcodePcApp.Properties.Resources.prnt;

                            //m_dt.Rows.Add(new object[] { pStruct.m_sProdnamn, pStruct.m_sStreckkod, pStruct.m_sRoom, pStruct.m_sCabinet, pStruct.m_sAmount, pStruct.m_sUnit, pStruct.m_sFriText, pStruct.m_sKemsikbet, iconColumn.Image, pStruct.m_sComment,pStruct.m_sCabinet,pStruct.m_sRoom,pStruct.m_sUtgDatum,pStruct.m_sSkDatum });
                            m_dt.Rows.Add(new object[] { pStruct.m_sProdnamn, pStruct.m_sStreckkod, pStruct.m_sRoom, pStruct.m_sCabinet, pStruct.m_sAmount, pStruct.m_sUnit, pStruct.m_sFriText, pStruct.m_sKemsikbet, iconColumn.Image, pStruct.m_sComment });

                            if (resultDataGridView.Rows.Count == 1)
                            {
                                resultDataGridView.Columns[0].Width = 150;
                                resultDataGridView.Columns[1].Width = 70;
                                resultDataGridView.Columns[2].Width = 80;
                                resultDataGridView.Columns[3].Width = 80;
                                resultDataGridView.Columns[4].Width = 50;
                                resultDataGridView.Columns[5].Width = 50;
                                resultDataGridView.Columns[6].Width = 70;
                                resultDataGridView.Columns[7].Width = 80;
                                resultDataGridView.Columns[8].Width = 60;

                            }

                            m_aAllaIncheckade.Add(pStruct);
                        }
                        pd.Dispose();
                        RensaFalt();
                        this.AcceptButton = buttonSokProd;
                    }
                    else
                    {
                        CheckProduct(databas, m_nProdid, m_EgenskapNamn, m_nProdProperty, lokalId, storageId);
                        cboProperty.Focus();
                    }
                }
                else
                {
                    string xmlBarcode = "";
                    try
                    {
                        xmlBarcode = bs.CheckInAQBarCode(kemiDb, databas, verkId, userId, drProden, datumet, userName, mangden, enheten, antal, FormMain.Get.BarcodePrefix);
                        bc.Dispose(); // lagt till
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("No contact with the web server!\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "CheckInAQBarCode");
                        bc.Dispose();
                    }

                    storageId = 0;
                    if (comboBoxCabinet.SelectedItem != null)
                        storageId = Convert.ToInt32(comboBoxCabinet.SelectedValue);

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlBarcode);
                    XmlNodeList xmlnodeList = doc.DocumentElement.SelectNodes("streckKodsId");

                    foreach (XmlNode node in xmlnodeList)
                    {
                        int streckkodsid = Convert.ToInt32(node.Attributes["id"].Value);
                        string streckkod = FormMain.Get.BarcodePrefix + streckkodsid;
                        int artid = Convert.ToInt32(cboChooseArticle.SelectedValue);
                        if (txtBestBefore.Text == "" || txtBestBefore.Text == "YYYYMMDD")
                        {
                            txtBestBefore.Clear();
                            txtBestBefore.Text = "0";
                        }

                        try
                        {
                            //LokalDB projekt laggt till lokaldb
                            string sRet = bs.CheckInKpinventory(kemiDb, databas, lokaldb, verkId, userId, drProdid, drProdnam, orgNod, lokalId, storageId, userName,enhetId, streckkodsid, mangden, drEnh, streckkod, m_sComment, m_iGas, m_Leverantor, m_EgenskapID, txtBatch.Text, Convert.ToDouble(txtBestBefore.Text), artid,m_sRelease,landid); 
                            //string sRet = bs.CheckInKpinventory(kemiDb, databas, verkId, userId, drProdid, drProdnam, orgNod, lokalId, storageId, userName, enhetId, streckkodsid, mangden, drEnh, streckkod, m_sComment, m_iGas, Leverantor);

                        }
                        catch (Exception err)
                        {
                            MessageBox.Show("No contact with the web server!\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "CheckInKpinventory");
                            bc.Dispose();
                        }

                        if (FormMain.Get.BarcodeLayout == 0)
                        {
                            if (cas.Length > 0)
                                m_sCAS = "CAS: " + cas.Trim();
                            else
                                m_sCAS = "";
                            bc.DisplayCode = true;
                        }
                        else if (FormMain.Get.BarcodeLayout == 1)
                        {
                            if (cas.Length > 0)
                                m_sCAS = cas.Trim();
                            else
                                m_sCAS = "";

                            bc.Text = "";
                            bc.BarHeight = 4;
                            bc.DisplayCode = true;
                        }
                        else if (FormMain.Get.BarcodeLayout == 2)
                        {
                            m_sStreckkod = streckkod;
                            m_sSummaformel = kemiskbet;
                            if (cas.Length > 0)
                                m_sCAS = cas.Trim();
                            else
                                m_sCAS = "";

                            bc.Text = "";
                            bc.BarHeight = 4;
                            bc.DisplayCode = false;
                        }
                        else if (FormMain.Get.BarcodeLayout == 3)
                        {
                            m_sStreckkod = streckkod;
                            m_sSummaformel = kemiskbet;
                            if (cas.Length > 0)
                                m_sCAS = cas.Trim();
                            else
                                m_sCAS = "";

                            bc.Text = "";
                            bc.BarHeight = 4;
                            bc.DisplayCode = false;
                        }

                        //Gäller MaxLab

                        else if (FormMain.Get.BarcodeLayout == 4)
                        {
                            m_sStreckkod = streckkod;
                            m_sSummaformel = datum_max;
                            if (cas.Length > 0)
                                m_sCAS = cas.Trim();
                            else
                                m_sCAS = "";

                            bc.Text = "";
                            bc.BarHeight = 4;
                            bc.DisplayCode = false;
                        }

                         //Etikettlayout 6
                        else if (FormMain.Get.BarcodeLayout == 5)
                        {
                            m_sStreckkod = streckkod;
                            m_sSkapat_datum = m_sSkapat_datum;
                            m_sCabinet = comboBoxCabinet.Text; //hylla
                            m_sRumNamn = comboBoxRoom.Text; //Rum

                            m_sUtgangsdatum = txtBestBefore.Text;
                            if (m_sUtgangsdatum == "0")
                            {
                                m_sUtgangsdatum = "";
                            }

                            bc.Text = "";
                            bc.BarHeight = 4;
                            bc.DisplayCode = false;
                        }
                        bc.Code = streckkod;

                        //
                        // Skapa ny streckkod i AQBarcode och AQBarKopl
                        //-----------------------------------------------------------------                    

                        pd.Print();
                        pd.Dispose(); //Lagt till

                        //Uppdatera om hantering av produktegenskap vald i inifil
                        if (Log == true)
                        {
                            UpdateProdEgenskaper(databas, m_nProdid, m_EgenskapNamn);
                        }

                        UpdateSupplier(kemiDb, m_nProdid, m_Leverantor);

                        // Lägger in all inmatad data i resultDataGridView
                        //-----------------------------------------------------------------
                        ProdStruct pStruct = new ProdStruct();
                        pStruct.m_nProdid = m_nProdid;
                        pStruct.m_sProdnamn = m_sProdnamn;
                        pStruct.m_sRoom = comboBoxRoom.Text;
                        pStruct.m_sCabinet = comboBoxCabinet.Text;
                        if (lts.Length > 0)
                            pStruct.m_sFriText = lts;
                        else
                            pStruct.m_sFriText = dateTimePickern;
                        pStruct.m_sStreckkod = streckkod;
                        pStruct.m_nStreckkodid = streckkodsid;
                        pStruct.m_sAmount = textBoxAmount.Text;
                        pStruct.m_sUnit = comboBoxAmount.Text;
                        pStruct.m_sKemsikbet = kemiskbet;
                        pStruct.m_sSummaformel = m_sSummaformel;
                        pStruct.m_sCAS = m_sCAS;
                        pStruct.m_sComment = m_Comment.Text;
                        pStruct.m_sSkDatum = m_sSkapat_datum;
                        pStruct.m_sUtgDatum = m_sUtgangsdatum;



                        DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();
                        iconColumn.Image = BarcodePcApp.Properties.Resources.prnt;

                        //m_dt.Rows.Add(new object[] { pStruct.m_sProdnamn, pStruct.m_sStreckkod, pStruct.m_sRoom, pStruct.m_sCabinet, pStruct.m_sAmount, pStruct.m_sUnit, pStruct.m_sFriText, pStruct.m_sKemsikbet, iconColumn.Image, pStruct.m_sComment, pStruct.m_sCabinet, pStruct.m_sRoom, pStruct.m_sUtgDatum, pStruct.m_sSkDatum });
                        m_dt.Rows.Add(new object[] { pStruct.m_sProdnamn, pStruct.m_sStreckkod, pStruct.m_sRoom, pStruct.m_sCabinet, pStruct.m_sAmount, pStruct.m_sUnit, pStruct.m_sFriText, pStruct.m_sKemsikbet, iconColumn.Image, pStruct.m_sComment });
                        if (resultDataGridView.Rows.Count == 1)
                        {

                            resultDataGridView.Columns[0].Width = 150;
                            resultDataGridView.Columns[1].Width = 70;
                            resultDataGridView.Columns[2].Width = 80;
                            resultDataGridView.Columns[3].Width = 80;
                            resultDataGridView.Columns[4].Width = 50;
                            resultDataGridView.Columns[5].Width = 50;
                            resultDataGridView.Columns[6].Width = 70;
                            resultDataGridView.Columns[7].Width = 80;
                            resultDataGridView.Columns[8].Width = 60;

                        }

                        m_aAllaIncheckade.Add(pStruct);
                    }
                    pd.Dispose();
                    RensaFalt();
                    this.AcceptButton = buttonSokProd;
                }
            }
        }

        private void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs ppea)
        {
            BarcodeLayout.PrintDocumentOnPrintPage(sender, ppea, bc, m_sCAS, m_sSummaformel, m_sStreckkod, m_sComment, datum_max, m_sSkapat_datum,m_sUtgangsdatum,userName,m_sCabinet,m_sRumNamn); // gäller Max_Lab
        }
        #endregion

        private void resultDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (lang == false)
                {
                    MessageBox.Show("Note, this is a copy of the barcode label, not a new registration!", "Information");
                }
                else
                {
                    MessageBox.Show("Observera att detta är en kopia av streckkodsetiketten, inte en ny registrering !", "Information");
                }                

                PrintDocument pd = new PrintDocument();
                pd.PrinterSettings.PrinterName = "Fel";
                foreach (string prt in PrinterSettings.InstalledPrinters)
                {	// Installerade skrivare
                    if (prt.StartsWith(FormMain.Get.BarcodePrinter))
                    {
                        pd.PrinterSettings.PrinterName = prt;
                        break;
                    }
                }
                if (!pd.PrinterSettings.IsValid)
                {
                    if (lang == false)
                    {
                        MessageBox.Show("No label printer is installed");
                    }
                    else
                    {
                        MessageBox.Show("Skrivaren är inte installerad");
                    }
                    
                    return;
                }
                if (lang == false)
                {
                    pd.DocumentName = "Barcode for the product";
                }
                else
                {
                    pd.DocumentName = "Streckkod för produkten";
                }
                
                pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocumentOnPrintPage);
                bc.Code = ((ProdStruct)m_aAllaIncheckade[e.RowIndex]).m_sStreckkod;
                m_sCAS = ((ProdStruct)m_aAllaIncheckade[e.RowIndex]).m_sCAS;
                m_sSummaformel = ((ProdStruct)m_aAllaIncheckade[e.RowIndex]).m_sSummaformel;
                m_sStreckkod = ((ProdStruct)m_aAllaIncheckade[e.RowIndex]).m_sStreckkod;
                m_sComment = ((ProdStruct)m_aAllaIncheckade[e.RowIndex]).m_sComment;
                m_sSkapat_datum = ((ProdStruct)m_aAllaIncheckade[e.RowIndex]).m_sSkDatum;
                m_sCabinet = ((ProdStruct)m_aAllaIncheckade[e.RowIndex]).m_sCabinet;
                m_sRumNamn = ((ProdStruct)m_aAllaIncheckade[e.RowIndex]).m_sRoom;
                m_sUtgangsdatum = ((ProdStruct)m_aAllaIncheckade[e.RowIndex]).m_sUtgDatum;

                pd.Print();
                pd.Dispose();
                m_sComment = "";
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Datum med LTS
        private void checkBoxLTS_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLTS.Checked)
            {
                lts = "LTS";
                dateTimePicker1.Enabled = false;
            }
            else
            {
                lts = "";
                dateTimePicker1.Enabled = true;
            }
        }
        #endregion

        private void m_btnSelect_Click(object sender, EventArgs e)
        {
            FormOrgtrad fo = new FormOrgtrad();
            if (fo.ShowDialog(this) != DialogResult.OK) return;

            m_textDepartment.Text = fo.m_nOrgnamn;
            orgNod = fo.m_nOrgnod;
            string dat = databas;//nytt
            int org = orgAr;

            //------Stoppa inloggningen om produkterna ej kopierade från föregående inventeringsperiod-------
 
            barcode.BarcodeService bc1 = FormMain.getBarcodeService();
            XmlDocument doc1 = new XmlDocument();
            string orgar_q = bc1.GetOrgar(databas, verkId);
            doc1 = new XmlDocument();
            doc1.LoadXml(orgar_q);
            XmlNodeList xmlnodeList1 = doc1.DocumentElement.SelectNodes("item");
            int orgar = 0;
            foreach (XmlNode item in xmlnodeList1)
            {
                orgar= Convert.ToInt32(item.Attributes["orgar"].Value);
            }

            string status = bc1.GetInventoryOrgStatus(databas, orgNod, 0, orgar);

            if (status != "" && status !="Inventering avslutad")
            {
                string orgstatus = bc1.GetNotStartedOrg(databas, orgNod, 0);
                StringReader stri = new StringReader(orgstatus);
                DataSet ds1 = new DataSet();
                ds1.ReadXml(stri);
                DataTable dt = ds1.Tables[0];
                DataRow dr = dt.Rows[0];
                string orgnod_ejstartad = dr["namn"].ToString();

                if(lang == true)
                {
                    MessageBox.Show("Nytt inventeringsår har startat."+ Environment.NewLine + "Aktivering av det nya året som innebär kopiering av föregående års data sker i webbmiljön. ");
                }
                else
                {
                    MessageBox.Show("New year of inventory has been started." + Environment.NewLine + "Please, enter the web interface and copy all products from last inventory in (" + orgnod_ejstartad + "). ");
                }
                //resetInventoryViewWithText("New year of inventory" + orgAr + " has been started. Please, copy all products and amounts from last inventory, and start to invent. ");
                m_textDepartment.Clear();
                return;
            }
            else if(status == "Inventering avslutad")
            { 
            
            }
            else
            {

            }
             barcode.BarcodeService bc = FormMain.getBarcodeService();
            DataSet ds = new DataSet();
            string avancerad = bc.AvanceradNiva(databas, orgNod, org);
            m_sAvancerad = avancerad;

            if (m_sAvancerad == "21")
            {
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                cboChooseArticle.Visible = true;
                btnNewArt.Visible = true;
                txtBatch.Visible = true;
                txtBestBefore.Enabled = true;
                txtBestBefore.Visible = true;
                System.Drawing.Font newFont = new Font("Verdana", 8f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 178, false);
                txtBestBefore.Text = "YYYYMMDD";
                txtBestBefore.Font = new Font(newFont, FontStyle.Italic);
                txtBestBefore.BackColor = Color.White;
            }
            else
            {
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                cboChooseArticle.Visible = false;
                btnNewArt.Visible = false;
                txtBatch.Visible = false;
                txtBestBefore.Visible = false;
            }

            string release = bc.ReleaseRules(databas, orgNod,org);
            m_sRelease = release;

            string orange = bc.CheckIfOrange(databas,kemiDb,orgNod, org,m_nProdid);
            m_sOrange = orange;
            if (m_sOrange == "false")
            {
                if (lang == false)
                {
                    MessageBox.Show("You can not register this product because it is an orange product.");
                }
                else
                {
                    MessageBox.Show("Du kan inte registrera denna produkt eftersom det är en orange produkt.");
                }                
                textSokProd.Focus();
            }
            else
            {
                UpdateRoom();
            }
            // Här kollas regel för ImportLand
            XmlDocument doc12 = new XmlDocument();
            string imp_q = bc.ImportLandRegel(databas, kemiDb, orgNod, orgar);
            doc12 = new XmlDocument();
            doc12.LoadXml(imp_q);
            XmlNodeList xmlnodeList12 = doc12.DocumentElement.SelectNodes("item");
            int antal = 0;
            foreach (XmlNode item in xmlnodeList12)
            {
                antal = Convert.ToInt32(item.Attributes["antal"].Value);
            }
            importYes = antal;

            if (antal > 0)
            {
                labelImpLand.Enabled = true;
                labelImpLand.Visible = true;
                cboImpLand.Enabled = true;
                cboImpLand.Visible = true;
                pictureBoxImportLand.Enabled = true;
                pictureBoxImportLand.Visible = true;
                pictureBoxImportLand.Location = new System.Drawing.Point(376, 498);
                pictureBox4.Location = new System.Drawing.Point(376, 526);
                this.labelImpLand.Location = new System.Drawing.Point(26, 497);
                this.lblProperty.Location = new System.Drawing.Point(26, 528);
                this.cboImpLand.Location = new System.Drawing.Point(139, 497);
                this.cboProperty.Location = new System.Drawing.Point(139, 525);
                PopulateImpLand(databas, kemiDb,lokaldb,lang);
            }
            else
            {
                labelImpLand.Enabled = false;
                labelImpLand.Visible = false;
                cboImpLand.Enabled = false;
                cboImpLand.Visible = false;
                pictureBoxImportLand.Enabled = false;
                pictureBoxImportLand.Visible = false;
                this.lblProperty.Location = new System.Drawing.Point(26, 497);
                this.cboProperty.Location = new System.Drawing.Point(139, 497);
                pictureBox4.Location = new System.Drawing.Point(376, 498);
            }

        }

        private void UpdateRoom()
        {
            comboBoxRoom.DataSource = null;
            comboBoxRoom.DisplayMember = "";
            comboBoxRoom.ValueMember = "";
            comboBoxRoom.Text = "";
            barcode.BarcodeService bc = FormMain.getBarcodeService();

            XmlTextReader xtr = null;
            StringReader sr = new StringReader(bc.LokFast(databas, verkId, orgNod));
            xtr = new XmlTextReader(sr);
            DataSet ds = new DataSet();
            ds.ReadXml(xtr);
            comboBoxRoom.DataSource = ds.Tables["OrgLokalFast"]; // OBS! Här aktiveras metoden comboBoxRoom_SelectedIndexChanged()
            comboBoxRoom.DisplayMember = "Namn";
            comboBoxRoom.ValueMember = "Id";

            if(lang== true)
            {
                this.comboBoxRoom.Text = "--- Välj ---";
            }
            else
            {
                this.comboBoxRoom.Text = "--- Choose ---";
            }
            ds.Dispose();
            bc.Dispose();

            UpdateCabinet();
        }

        private void RensaFalt()
        {
            // Rensar alla fält
            textSokProd.Text = "";
            txtBarcode.Focus();
            dataGridProd.DataSource = null;

            comboBoxAmount.DataSource = null;
            comboBoxAmount.DisplayMember = "";
            comboBoxAmount.ValueMember = "";
            m_Comment.Text = "";

            cboImpLand.SelectedIndex = -1;
            cboImpLand.BackColor = Color.FromArgb(255, 255, 255);
            textBoxAmount.Text = "";
            m_textNumber.Text = "1";
            txtBatch.Text = "";
            txtBestBefore.Text = "";
            m_nProdid = -1;

            System.Drawing.Font newFont = new Font("Microsoft Sans Serif", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 178, false);
            label2.Font = new Font(newFont, FontStyle.Regular);
            label2.ForeColor = Color.Black;
            label3.Font = new Font(newFont, FontStyle.Regular);
            label3.ForeColor = Color.Black;
            textBoxAmount.BackColor = Color.White;
            m_Comment.BackColor = Color.White;
            m_textNumber.BackColor = Color.White;
        }

        private void UpdateCabinet()

        {
            comboBoxCabinet.DataSource = null;
            comboBoxCabinet.DisplayMember = "";
            comboBoxCabinet.ValueMember = "";
            comboBoxCabinet.Text = " ";

            if (comboBoxRoom.SelectedValue !=null)
            {
                string a =Convert.ToString(comboBoxRoom.SelectedValue);
                string lokid = a.Split('/')[0];
                if (lokid == "0")
                { 
                
                }
                else
                { 
                    lokaldb = a.Split('/')[1]; 
                }        
                
                m_lokalID =Convert.ToInt32(lokid);
                lokalId = Convert.ToInt32(lokid);

                if (lokalId > 0)
                {
                    comboBoxCabinet.Enabled = true;
                    barcode.BarcodeService bc = FormMain.getBarcodeService();
                    System.Diagnostics.Debug.WriteLine(bc.GetStorage(databas, lokalId, lokaldb));
                    XmlTextReader xtr = null;
                    StringReader sr = new StringReader(bc.GetStorage(databas, lokalId, lokaldb));
                    xtr = new XmlTextReader(sr);
                    DataSet ds = new DataSet();
                    ds.ReadXml(xtr);
                    comboBoxCabinet.DataSource = null;
                    comboBoxCabinet.DataSource = ds.Tables["Storage"];
                    comboBoxCabinet.DisplayMember = "Namn";
                    comboBoxCabinet.ValueMember = "Id";
                    if (lang == true)
                    {
                        this.comboBoxCabinet.Text = "--- Välj ---";
                    }
                    else
                    {
                        this.comboBoxCabinet.Text = "--- Choose ---";
                    }
                    //comboBoxCabinet.Text = "--- Choose ---";
                    //lokaldb = Convert.ToString(ds.Tables[1].Rows[0]["LokalDb"]);
                    ds.Dispose();
                    bc.Dispose();
                }
                else
                {
                    comboBoxCabinet.Enabled = false;
                }
                ////Kontrollera om produkten finns på verksamheten
                //barcode.BarcodeService bca = FormMain.getBarcodeService();
                //double s = Convert.ToDouble(bca.FindProduct(kemiDb, databas, m_nProdid, verkId,orgNod));
                //if (s == 0)
                //{
                //    MessageBox.Show("The chemical cannot be found. Please use the webb-application of KLARA in order to register a new one.");
                //    textSokProd.Focus();
                //}

            }
        //   else comboBoxCabinet.Enabled = false;
        }

        //Ny funktion som kontrollerar om man kan ställa en produkt i en hylla
        private void CheckProduct(string databas, int m_nProdid, string m_EgenskapNamn, int m_prodproperty, int lokalId, int storageId)
        {
            barcode.BarcodeService bc = FormMain.getBarcodeService();
            //UpdateProdEgenskaper(databas, m_nProdid, m_EgenskapNamn);  //lagt till 20140816
            //int StorageProperty = Convert.ToInt32(bc.GetRoomProp(databas, lokalId, storageId));
            int StorageProperty = m_nStorProperty;
            m_nStorProperty = StorageProperty;
            // If storage not defined, get the father's property to child
            if (StorageProperty == 0)
            {
                string StorageProperty_q = bc.GetStorageParent(databas, lokalId, storageId,lokaldb);
                XmlDocument doc1 = new XmlDocument();
                doc1 = new XmlDocument();
                doc1.LoadXml(StorageProperty_q);
                XmlNodeList xmlnodeList1 = doc1.DocumentElement.SelectNodes("item");
                foreach (XmlNode item in xmlnodeList1)
                {
                    StorageProperty = Convert.ToInt32(item.Attributes["id"].Value);
                }
            }
            else
            {

            }

            int klaraid = Convert.ToInt32(m_nProdid);
            m_nProdProperty = m_prodproperty;

            if ((StorageProperty == m_nProdProperty) || (StorageProperty == Convert.ToInt32(Property.Omärkt)))
            {
                return;
            }
            else
            {
                barcode.BarcodeService bst = FormMain.getBarcodeService();
                //string StoragePropName = bst.GetPropNameString(databas, lokalId, storageId);
                DataSet ds = new DataSet();
                string StoragePropName = bst.GetStorageParentCheck(databas, lokalId, StorageProperty,lokaldb);
                StringReader stri = new StringReader(StoragePropName);
                ds.ReadXml(stri);
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];
                PropName = dr["nameEng"].ToString();
                //string PropName = dr["nameEng"].ToString();
                if (lang == false)
                {
                    MessageBox.Show("Property on product and location do not match. Please, change property of product! Set property to " + PropName + " if you want to register a product in this room");
                }
                else
                {
                    MessageBox.Show("Produktens egenskap i förhållande till dess placering överensstämmer inte. Vänligen byt egenskap på produkten! Egenskapen " + PropName + " gäller om du vill registrera produkter till denna lokal");
                }
                return;
            }

        }

        private void UpdateAmount()
        {
            textBoxAmount.Text = "";

            barcode.BarcodeService bc = FormMain.getBarcodeService();
            StringReader sr;

            XmlTextReader xtr = null;
            sr = new StringReader(bc.GetUnit(kemiDb, huvudEnhetId));
            xtr = new XmlTextReader(sr);
            DataSet ds = new DataSet();
            ds.ReadXml(xtr);
            comboBoxAmount.DataSource = null;
            comboBoxAmount.DataSource = ds.Tables["Unit"];
            comboBoxAmount.DisplayMember = "Namn";
            comboBoxAmount.ValueMember = "Id";
            if (lang == true)
            {
                comboBoxAmount.Text = "--- Välj enhet ---";
            }
            else
            {
                comboBoxAmount.Text = "--- Choose amount ---";
            }
            

            ds.Dispose();
            bc.Dispose();
        }

        // Sandi
        private string checkinbarcode = "";

        // Check if user want to use scan 
        private void chkUser_CheckedChanged(object sender, EventArgs e)
        {
            txtBarcode.Clear();
            if (chkUser.CheckState == CheckState.Checked)
            {
                panel1.Visible = true;
                buttonCheckIn.Visible = false;
                txtBarcode.Enabled = true;
                txtBarcode.Focus();
            }
            else
            {
                panel1.Visible = false;
                buttonCheckIn.Visible = true;
                txtBarcode.Focus();
                txtBarcode.Enabled = true;
            }
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            if (txtBarcode.Text != "")
            {
                lblBarcode.Text = txtBarcode.Text;
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


        private void scannerTim_Elapsed(object source, ElapsedEventArgs e)
        {

            //scannerTimer.Enabled = false;
            scannerTim.Stop();
            getUser(checkinbarcode);

            checkinbarcode = "";
        }

        private void buttonCheckIn_Click(object sender, EventArgs e)
        {
            getUser(txtBarcode.Text);
            txtBarcode.Clear();
            txtBarcode.Focus();
        }

        // Send barcode to db and get back username
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
                    if (lang == false)
                    {
                        MessageBox.Show("This is not KLARA user");
                    }
                    else
                    {
                        MessageBox.Show("Detta är inte KLARA användare");
                    }

                    
                    bc.Dispose();
                    return;

                }
                else
                {
                    //this.Close();
                    //this.Invoke(new MethodInvoker(delegate { FormMain.Get.Logout(); }));
                    MessageBox.Show("Checkin, funktion getuser()");




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

        private void cboProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = 0;
            index = cboProperty.SelectedIndex;
            //Sandi
            if (cboProperty.ValueMember.Length > 0)
            {
                barcode.BarcodeService bst = FormMain.getBarcodeService();
                DataSet ds = new DataSet();
                string PropName = bst.GetPropertyName(databas, m_nProdid);
                StringReader stri = new StringReader(PropName);
                ds.ReadXml(stri);
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];
                PropName = dr["nameEng"].ToString();


            }
            else
            {
                //m_EgenskapNamn = cboProperty.Text;
                index = cboProperty.SelectedIndex;
            }


            //Ändring 
            barcode.BarcodeService bc = FormMain.getBarcodeService();
            //int StorageProperty = Convert.ToInt32(bc.GetRoomProp(databas, lokalId, storageId));
            int StorageProperty = m_nStorProperty;
            // If storage not defined, get the father's property to child
            if (StorageProperty == 0)
            {
                string StorageProperty_q = bc.GetStorageParent(databas, lokalId, storageId,lokaldb);
                XmlDocument doc1 = new XmlDocument();
                doc1 = new XmlDocument();
                doc1.LoadXml(StorageProperty_q);
                XmlNodeList xmlnodeList1 = doc1.DocumentElement.SelectNodes("item");
                foreach (XmlNode item in xmlnodeList1)
                {
                    StorageProperty = Convert.ToInt32(item.Attributes["id"].Value);
                }
                //StorageProperty = Convert.ToInt32(bc.GetStorageParent(databas, lokalId, storageId));
            }
            else
            {


            }

            int klaraid = Convert.ToInt32(m_nProdid);

        }

        //Sandi
        private void PopulateProperties(string databas, string kemidatabas, int prodId)
        {

            cboProperty.DataSource = null;
            cboProperty.DisplayMember = "";
            cboProperty.ValueMember = "";
            cboProperty.Text = "";

            barcode.BarcodeService bc = FormMain.getBarcodeService();

            XmlTextReader xtr = null;
            //Kontroll mot KPInvtrans, kolumnen EgenskapID - (ev. version 1.19)
            StringReader str = new StringReader(bc.GetProdProperties2(databas, kemiDb, m_nProdid));

            xtr = new XmlTextReader(str);
            DataSet dts = new DataSet();
            dts.ReadXml(xtr);
            cboProperty.DataSource = dts.Tables["Property"]; // OBS! Här aktiveras metoden comboBoxRoom_SelectedIndexChanged()
            if(lang ==true)
            {
                cboProperty.DisplayMember = "nameSwe";
            }
            else
            {
                cboProperty.DisplayMember = "nameEng";
            }
           
            cboProperty.ValueMember = "id";
            cboProperty.Text = "--- Choose ---";

            string egid_q = bc.GetPropID(databas, m_EgenskapNamn);
            XmlDocument doc6 = new XmlDocument();
            doc6 = new XmlDocument();
            doc6.LoadXml(egid_q);
            XmlNodeList xmlnodeList6 = doc6.DocumentElement.SelectNodes("item");
            int egenskapsid = 0;
            foreach (XmlNode item in xmlnodeList6)
            {
                egenskapsid = Convert.ToInt32(item.Attributes["id"].Value);
            }
            m_EgenskapID = egenskapsid;
            //m_EgenskapID = Convert.ToInt32(bc.GetPropID(databas, m_EgenskapNamn));
            ///
            dts.Dispose();
            bc.Dispose();

        }

        private void PopulateArticles(string databas, string kemidatabas, string lev)
        {

            cboChooseArticle.DataSource = null;
            cboChooseArticle.DisplayMember = "";
            cboChooseArticle.ValueMember = "";
            cboChooseArticle.Text = "---";

            barcode.BarcodeService bc = FormMain.getBarcodeService();

            XmlTextReader xtr = null;
            //Kontroll mot KPInvtrans, kolumnen EgenskapID - (ev. version 1.19)
            StringReader str = new StringReader(bc.GetArtNr(databas, kemiDb, lev, m_nProdid));
            xtr = new XmlTextReader(str);
            DataSet dts = new DataSet();
            dts.ReadXml(xtr);
            cboChooseArticle.DataSource = dts.Tables["Art"]; // OBS! Här aktiveras metoden cboChooseArticle_SelectedIndexChanged()
            cboChooseArticle.DisplayMember = "Artnr";
            cboChooseArticle.ValueMember = "Id";
            cboChooseArticle.Text = "---No articles---";

            //string aaaaaa = bc.GetArtNr(databas, kemiDb, lev, m_nProdid);
            //XmlDocument doc70 = new XmlDocument();
            //doc70 = new XmlDocument();
            //doc70.LoadXml(aaaaaa);
            //XmlNodeList xmlnodeList70 = doc70.DocumentElement.SelectNodes("levid");
            //string artnr;
            //foreach (XmlNode item in xmlnodeList70)
            //{
            //    artnr = item.Attributes["artnr"].Value;
            //}

            //    ////
            //    m_EgenskapID = Convert.ToInt32(bc.GetPropID(databas, m_EgenskapNamn));
            //    ///
            dts.Dispose();
            bc.Dispose();

        }

        private void PopulateImpLand(string databas,string kemidb,string lokaldb, bool sprak)
        {
            string language="";
            if(sprak ==true)
            {
                language ="0";
            }
            else
            {
                language ="1";
            }

            cboImpLand.DataSource = null;
            cboImpLand.DisplayMember = "";
            cboImpLand.ValueMember = "";
            cboImpLand.Text = "";

            barcode.BarcodeService bc = FormMain.getBarcodeService();

            XmlTextReader xtr = null;
            ////Kontroll mot KPInvtrans, kolumnen EgenskapID - (ev. version 1.19)
            StringReader str = new StringReader(bc.GetCountries(databas, kemidb, lokaldb,language));

            xtr = new XmlTextReader(str);
           DataSet dts = new DataSet();
            dts.ReadXml(xtr);
            cboImpLand.DataSource = dts.Tables["ImpLand"]; // OBS! Här aktiveras metoden cboImpLand_SelectedIndexChanged()
            if (lang == true)
            {
                cboImpLand.DisplayMember = "namnsve";
                cboImpLand.ValueMember = "id";
                cboImpLand.Text = "--- Välj land ---";

            }
            else
            {
                cboImpLand.DisplayMember = "nameeng";
                cboImpLand.ValueMember = "id";
                cboImpLand.Text = "--- Choose country ---";

            }

            //cboImpLand.ValueMember = "id";


            dts.Dispose();
            bc.Dispose();

        }


        //Sandi
        private void UpdateProdEgenskaper(string databas, int prodId, string egenskapNamn)
        {


            barcode.BarcodeService bc = FormMain.getBarcodeService();
            string egid_q = bc.GetPropID(databas, m_EgenskapNamn);
            XmlDocument doc7 = new XmlDocument();
            doc7 = new XmlDocument();
            doc7.LoadXml(egid_q);
            XmlNodeList xmlnodeList7 = doc7.DocumentElement.SelectNodes("item");
            int egenskapsid = 0;
            foreach (XmlNode item in xmlnodeList7)
            {
                egenskapsid = Convert.ToInt32(item.Attributes["id"].Value);
            }
            m_EgenskapID = egenskapsid;
            //m_EgenskapID = Convert.ToInt32(bc.GetPropID(databas, m_EgenskapNamn));

            XmlTextReader xtr = null;
            //Uppdatera produktegenskap
            StringReader str = new StringReader(bc.ProductProperty(databas, m_nProdid, m_EgenskapID));

            //int ProdProperty = Convert.ToInt32(bc.GetPropName(databas, prodId));
            //int ProdProperty = Convert.ToInt32(bc.GetPropName(databas,n_));
            //m_nProdProperty = ProdProperty;
            //xtr = new XmlTextReader(str);
            //DataSet dts = new DataSet();
            //dts.ReadXml(xtr);

            //dts.Dispose();
            //bc.Dispose();

        }

        private void buttonLogout_Click(object sender, System.EventArgs e)
        {
            userId = 0;
            orgNod = 0;
            orgAr = 0;
            userName = "";
            userStringName = "";
            userStringSys = "";
            userStringDepart = "";
            this.Close();
            Application.Restart();
        }

        private void FormCheckIn_Load(object sender, EventArgs e)
        {

        }

        //Sandi
        private void PopulateSupplier(string databas, string kemidatabas, int prodId)
        {


            comboBoxSupplier.DataSource = null;
            comboBoxSupplier.DisplayMember = "";
            comboBoxSupplier.ValueMember = "";
            comboBoxSupplier.Text = "";

            barcode.BarcodeService bc = FormMain.getBarcodeService();

            XmlTextReader xtr = null;
            StringReader str = new StringReader(bc.GetProdSupplier(databas, kemiDb, m_nProdid));
            xtr = new XmlTextReader(str);
            DataSet dts = new DataSet();
            dts.ReadXml(xtr);
            comboBoxSupplier.DataSource = dts.Tables["Property"]; // OBS! Här aktiveras metoden comboBoxSupplier_SelectedIndexChanged()
            comboBoxSupplier.DisplayMember = "namn";
            comboBoxSupplier.ValueMember = "id";
            if(lang==true)
            {
                comboBoxSupplier.Text = "--- Välj leverantör---";
            }
            else
            {
                comboBoxSupplier.Text = "--- Choose supplier---";
            }
            


            dts.Dispose();
            bc.Dispose();

        }

        private void comboBoxSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index;

           


            //Sandi
            if (comboBoxSupplier.ValueMember.Length > 0)
            {
                index = comboBoxSupplier.SelectedIndex;
                if (index == -1)
                    { 
                    index = comboBoxSupplier.SelectedIndex;
                    PopulateArticles(databas, kemiDb, "0");
                    }
                else
                    {

                barcode.BarcodeService bst = FormMain.getBarcodeService();
                DataSet ds = new DataSet();
                string leverantorid = comboBoxSupplier.SelectedValue.ToString();

                string leverant = comboBoxSupplier.Text.ToString();
                //Fyll combobox ArtNr med artikelnummer kopplade till vald leverantör
                PopulateArticles(databas, kemiDb, leverantorid);


                //ändring gällande dropbox som kan söka på 1,2,3 första boks

                //PopulateSupplier(databas, kemiDb,m_nProdid);
                //Slut ändring

                //string PropName = bst.GetPropertyName(databas, m_nProdid);
                //StringReader stri = new StringReader(PropName);
                //ds.ReadXml(stri);
                //DataTable dt = ds.Tables[0];
                //DataRow dr = dt.Rows[0];
                //PropName = dr["nameEng"].ToString();
                    }

            }
                
            else
            {
                comboBoxSupplier.SelectedIndex = -1;
                index = comboBoxSupplier.SelectedIndex;
            }


        }

        private void comboBoxSupplier_TextChanged(object sender, EventArgs e)
        {
            //comboBoxSupplier.DataSource = null;
            //comboBoxSupplier.DisplayMember = "";
            //comboBoxSupplier.ValueMember = "";
            //comboBoxSupplier.Text = "";

            //barcode.BarcodeService bc = FormMain.getBarcodeService();
            //string leverant = comboBoxSupplier.Text.ToString();
            //XmlTextReader xtr = null;
            //StringReader str = new StringReader(bc.GetProdSupplierText(databas, kemiDb, leverant));
            //xtr = new XmlTextReader(str);
            //DataSet dts = new DataSet();
            //dts.ReadXml(xtr);
            //comboBoxSupplier.DataSource = dts.Tables["Property"]; // OBS! Här aktiveras metoden comboBoxSupplier_SelectedIndexChanged()
            //comboBoxSupplier.DisplayMember = "namn";
            //comboBoxSupplier.ValueMember = "id";
            //comboBoxSupplier.Text = "--- Choose ---";


            //dts.Dispose();
            //bc.Dispose();

             //Fyll combobox ArtNr med artikelnummer kopplade till vald leverantör

            //ändring gällande dropbox som kan söka på 1,2,3 första boks

            //PopulateSupplier(databas, kemiDb,m_nProdid);
            //Slut ändring
        }

        //Sandi
        private void UpdateSupplier(string kemiDb, int prodId, int levid)
        {


            barcode.BarcodeService bc = FormMain.getBarcodeService();
            //Uppdatera supplier
            StringReader str = new StringReader(bc.InsertProdSupplier(kemiDb, m_nProdid, levid));


            bc.Dispose();

        }

        private void buttonPrint_Enter(object sender, EventArgs e)
        {
            this.Close();
        }

           private void pictureBox6_Click(object sender, EventArgs e)
        {
            //var form = new FormPopoup();
            //form.Show(this); // if you need non-modal window




            //string longurl = "http://ny/alphaquest/AQKlarprod/pcapp_jme.cfm?";           //lokalt
            //string longurl = "http://aqtest.port.se/alphaquest/AQKlarprod/pcapp_jme.cfm?";        //testserver
            string longurl = "http://ny/alphaquest/AQKlarprod/pcapp_prod_jme.cfm?";      //lokalt
            //string longurl = "http://aqtest.port.se/alphaquest/AQKlarprod/pcapp_prod_jme.cfm?";   //testserver
            //string longurl = "http://www.port.se/alphaquest/AQKlarprod/pcapp_prod_jme.cfm?";   //skarpt
            var uriBuilder = new UriBuilder(longurl);
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
            query["cas"] = m_sCAS.ToString();
            query["kemds"] = kemiDb;
            query["id"] = Convert.ToString(m_nProdid);
            uriBuilder.Query = query.ToString();
            longurl = uriBuilder.ToString();

            System.Diagnostics.Process.Start(longurl);
        }


        
        private void btnNewArt_Click(object sender, EventArgs e)
        {
            if (comboBoxSupplier.Text == "--- Choose ---" || comboBoxSupplier.Text == "--- Välj leverantör---")
            {
                if (lang == false)
                {
                    MessageBox.Show("You need to select a supplier!");
                }
                else
                {
                    MessageBox.Show("Du måste välja en leverantör!");
                }

                
                comboBoxSupplier.Focus();
            }
            else
            {
                int Leverantor = Convert.ToInt32(comboBoxSupplier.SelectedValue);
                string levid = comboBoxSupplier.SelectedValue.ToString();

                FormNewArticle art = new FormNewArticle(comboBoxSupplier.Text, kemiDb, huvudEnhetId, Leverantor, m_nProdid, databas);
                DialogResult result = art.ShowDialog(this.Owner);
                PopulateArticles(databas, kemiDb, levid);
                art.Dispose();
                textBoxAmount.Focus();
            }
        }

        private void cboChooseArticle_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtBatch_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBestBefore_TextChanged(object sender, EventArgs e)
        {
        //    if (txtBestBefore.Text == "")
        //    {
        //        ChangeTextBoxtoWatermark();
        //    }
        }

        private void txtBestBefore_Click(object sender, EventArgs e)
        {
            if (txtBestBefore.Text == "YYYYMMDD")
            {
                txtBestBefore.Text = "";
                txtBestBefore.Font = new Font(this.Font, FontStyle.Regular);
                txtBestBefore.BackColor = Color.White;
            }
            //else
            //{

            //    ChangeTextBoxtoWatermark();
            //}
        }

        //private void txtBestBefore_MouseLeave(object sender, EventArgs e)
        //{
        //    if (txtBestBefore.Text == "")
        //        ChangeTextBoxtoWatermark();
        //}

        private void ChangeTextBoxtoWatermark()
        {
            txtBestBefore.Font = new Font(this.Font, FontStyle.Italic);
            txtBestBefore.BackColor = Color.White;
            txtBestBefore.Text = "YYYYMMDD";
        }

        private void bc_Click(object sender, EventArgs e)
        {

        }

        private void btnOKViktig_Click(object sender, EventArgs e)
        {
            panelViktig.Visible = false;
        }

        private void pictureBoxSearch_Click(object sender, EventArgs e)
        {

        }

        private void cboImpLand_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index;
            if (cboImpLand.ValueMember.Length > 0)
            {
                index = cboImpLand.SelectedIndex;
                if (index > 0)
                {
                    landid = Convert.ToInt32(cboImpLand.SelectedValue);
                }
                else
                {
                    landid = -1;
                }
            }            
        }

        private void textBoxAmount_Click(object sender, EventArgs e)
        {
            textBoxAmount.Text="";
        }

        private void m_textNumber_Click(object sender, EventArgs e)
        {
            m_textNumber.Clear();
        }

        //private void ChangeTextBoxtoWatermark()
        //{
        //    txtBestBefore.Font = new Font(this.Font, FontStyle.Italic);
        //    txtBestBefore.Text = "YYYYMMDD";
        //}

        //private void txtBestBefore_Click(object sender, EventArgs e)
        //{
        //    if (txtBestBefore.Text == "YYYYMMDD")
        //    {
        //        txtBestBefore.Text = "";
        //        txtBestBefore.Font = new Font(this.Font, FontStyle.Italic);
        //        txtBestBefore.BackColor = Color.White;
        //    }

        //}




        //private void buttonCheckIn_Click(object sender, EventArgs e)
        //{
        //    FormCheckIn checkin = new FormCheckIn();
        //    checkin.Databas = databas;
        //    checkin.KemiDb = kemiDb;
        //    checkin.OrgAr = orgAr;
        //    //checkin.OrgNod = orgNod;
        //    checkin.VerkId = verkId;
        //    checkin.UserId = userId;
        //    checkin.UserName = userName;
        //    checkin.UserStringDepart = userStringDepart;
        //    checkin.UserStringName = userStringName;
        //    checkin.UserStringSys = userStringSys;
        //    //checkin.OrgNamn = ((StringIntObject)listBoxOrgnod.SelectedItem).m_sData;
        //    //checkin.OrgNamn = "OrgNamn, hämta denna senare...";
        //    checkin.ShowDialog(this);
        //    checkin.Dispose();
        //}
    }
}