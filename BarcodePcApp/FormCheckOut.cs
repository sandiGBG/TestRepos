using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Timers;

namespace BarcodePcApp
{
    public partial class FormCheckOut : Form
    {
        //private int verkId;
        public int verkId;
        private int userId;
        private int orgNod;
        private int orgAr;
        private string userName;
        private string userStringDepart;
        private string userStringName;
        private string userStringSys;
        private string databas;
        private string kemiDb;
        private string orgNamn;
        public ArrayList m_aAllaUtcheckade = new ArrayList();
        private System.Timers.Timer scannerTimer = new System.Timers.Timer();
        private string barcode = "";
        private string barcodeInSettings = "";
        private bool Log;
        private bool userBarcode;
        private int m_nOrgNod = 0;
        private int m_nOrgNod1 = 0;
        private string lokaldb;
        private bool lang;

        public struct ProdStruct
        {
            //public int m_nStructProdid;
            public string m_sStructProdnamn;
            public string m_sStructCas;
            public string m_sStructSlutdat;
            public string m_sStructFriText;
            public string m_sStructMangd;
            public string m_sStructEnhet;
        }
        public ProdStruct pStruct = new ProdStruct();

        public FormCheckOut()
        {
            InitializeComponent();
            lang = FormMain.Get.LangChange;

            if (lang == false)
            {
                this.Text = "Discard products"; 
                this.labelCheckOut.Text = "Discard products";
                this.labelInfoScan.Text = "Scan product or enter the barcode and click the Discard button:";
                this.buttonScanned.Text = "Discard";
                this.buttonClose.Text = "Close";
                this.scannerCheckBox.Text = "Scan && discard";
                this.groupBox1.Text = "Discarded products";
                
            }
            else
            {
                this.Text = "Kassera produkter";
                this.labelCheckOut.Text = "Kassera produkter";
                this.labelInfoScan.Text = "Skanna streckkoden eller skriv in streckkoden och tryck på knappen Kassera:";
                this.buttonScanned.Text = "Kassera";
                this.buttonClose.Text = "Stäng";
                this.scannerCheckBox.Text = "skanna och kassera";
                this.groupBox1.Text = "Kasserade produkter";



            }

            this.KeyPress += new KeyPressEventHandler(form_KeyPress);
            scannerTimer.Interval =100;
            scannerTimer.Elapsed += scannerTimer_Elapsed;

            textBoxScanCode.Enabled = false;
            buttonScanned.Enabled = false;
            FormSettings form = new FormSettings();
            barcodeInSettings = form.loginBarcode;
            txttradlos.Enabled = false;
  

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
        public string LokalDb { set { lokaldb = value; } }

        private void FormCheckOut_Load(object sender, EventArgs e)
        {
            textBoxScanCode.Focus();
            txttradlos.Select();
        }

        //private void FormCheckOut_Shown(object sender, EventArgs e)
        //{
        //    txttradlos.Focus();

        //}

        private void buttonScanned_Click(object sender, EventArgs e)
        {
            Log = FormMain.Get.PropertyCheck;
            userBarcode = FormMain.Get.LoginBarcode;
            if (textBoxScanCode.Text == "" && barcodeInSettings.Trim() == "")
            {
                barcodeInSettings = "0";

            }
            if (userBarcode == true)
            {
                if (textBoxScanCode.Text == barcodeInSettings)
                {
                    //logga ut user
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    if (lang == false)
                    {
                        result = MessageBox.Show("Product has the same barcode as user.\n\n If you want to discard product - enter YES \n If you want to logout user - enter NO", "BarcodePcApp", buttons);
                    }
                    else
                    {
                        result = MessageBox.Show("Produkten har samma streckkod som användare.\n\n . Vill du kassera en produkt - ange JA \n Om du vill logga ut - ange NEJ", "BarcodePcApp", buttons);
                    }

                    
                    this.Close();
                    this.Owner.Close();

                }
                else
                {
                    barcode=textBoxScanCode.Text;
                    getProduct(barcode);
                    textBoxScanCode.Clear();
                    labelShowProduct.Text = "";
                    textBoxScanCode.Focus();
                    return;
                }
            }
            barcode = textBoxScanCode.Text;
            getProduct(barcode);
            textBoxScanCode.Clear();
            labelShowProduct.Text = "";
            textBoxScanCode.Focus();

        }

        private void getProduct(string streckkod)
        {
            // Hämta produkten med den inskannade streckkoden:
            barcode.BarcodeService bc = FormMain.getBarcodeService();

            this.Invoke(new MethodInvoker(delegate
            {

                //****** KI anpassning v 1.18 *****//
                string barcodeEX = "";


                if (barcode.StartsWith("bb") || (barcode.StartsWith("BB")))
                {
                    barcodeEX = barcode.Substring(1, barcode.Length - 1);
                    barcode = barcodeEX.ToString();
                }

                if (barcode.StartsWith("`") || barcode.StartsWith("+"))
                {
                    barcodeEX = barcode.Substring(2, barcode.Length - 2);
                    barcode = barcodeEX.ToString();

                }

                if (barcode.EndsWith("¤") || barcode.EndsWith("$"))
                {
                    string barcodeEX1 = barcode.Remove(barcode.Length - 1);
                    barcode = barcodeEX1.ToString();

                }

                //****** slut KI anpassning v 1.18 *****//

                string prod = "";

                //if (!(streckkod.Length > 0 && streckkod.Length < 16))
                if (!(barcode.Length > 0 && barcode.Length < 16))
                {
                    //System.Media.SystemSounds.Hand.Play();
                    if (lang == false)
                    {
                        MessageBox.Show("This is not a KLARA product.");
                    }
                    else
                    {
                        MessageBox.Show("Detta är inte en KLARA produkt.");
                    }

                    

                    bc.Dispose();
                    return;
                }

                try
                {
                    prod = bc.GetCheckedInProd(databas, kemiDb, barcode, userId, userName, userStringSys, FormMain.Get.BarcodePrefix);
                    //MessageBox.Show(streckkod);
                }
                catch (Exception err)
                {
                    System.Media.SystemSounds.Hand.Play();
                    MessageBox.Show("No contact with the web server!\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "GetCheckedInProd");
                    bc.Dispose();
                    return;
                }

                //-------------------------------------------------------------
                XmlTextReader xtr = null;
                StringReader sr = new StringReader(prod);
                xtr = new XmlTextReader(sr);
                DataSet ds = new DataSet();
                ds.ReadXml(xtr);

                if (ds.Tables["ProdId"].Rows.Count > 0)
                {
                    if (ds.Tables["ProdId"].Rows[0]["Prodnam"].ToString() == "No product found for this barcode." || ds.Tables["ProdId"].Rows[0]["Prodnam"].ToString() == "The product has already been checked out/discarded.")
                    {
                        System.Media.SystemSounds.Asterisk.Play();
                        MessageBox.Show("" + ds.Tables["ProdId"].Rows[0]["Prodnam"].ToString());
                    }
                    else if (ds.Tables["ProdId"].Rows[0]["Prodnam"].ToString() == "This product has not yet been released and your request is denied.")
                    {
                        System.Media.SystemSounds.Asterisk.Play();
                        if(lang == true)
                        {
                            MessageBox.Show("Produkten har inte ännu blivit frisläppt och kasseras därför inte.");
                        }
                        else
                        {
                            MessageBox.Show("This product has not yet been released and your request is denied.");
                        }
                        //MessageBox.Show("" + ds.Tables["ProdId"].Rows[0]["Prodnam"].ToString());
                    }
                    else
                    {
                        string dubb_q = bc.CheckDuplicate(databas, streckkod, orgAr);
                        XmlDocument doc1 = new XmlDocument();
                        doc1 = new XmlDocument();
                        doc1.LoadXml(dubb_q);
                        XmlNodeList xmlnodeList1 = doc1.DocumentElement.SelectNodes("item");
                        int dubb = 0;
                        foreach (XmlNode item in xmlnodeList1)
                        {
                            dubb = Convert.ToInt32(item.Attributes["id"].Value);
                        }
                        //int dubb = Convert.ToInt32(bc.CheckDuplicate(databas, streckkod, orgAr));

                        if (dubb == 1)
                        {
                            if (lang == false)
                            {
                                MessageBox.Show("The barcode was not discarded. A duplicate exist, please discard the item in the web application.", "Discard a product");
                            }
                            else
                            {
                                MessageBox.Show("Streckkoden kasserades inte. En dubblett förekommer och kassera den därför från webbmiljön.", "Kassera en produkt");
                            }

                           
                        }
                        else
                        {
                            //20171101 Kontrollera om produkten tillhör egen verksamhet//
                            if (!scannerCheckBox.Checked)
                            {
                                string orgarID_w = (bc.GetOrgarID(databas, verkId));
                                doc1.LoadXml(orgarID_w);
                                XmlNodeList xmlnodeList2 = doc1.DocumentElement.SelectNodes("item");
                                int orgarID = 0;
                                int ar_w = 0;
                                foreach (XmlNode item in xmlnodeList2)
                                {
                                    orgarID = Convert.ToInt32(item.Attributes["orgarid"].Value);
                                    ar_w = Convert.ToInt32(item.Attributes["ar"].Value);

                                }

                                //int orgarID = Convert.ToInt32(bc.GetOrgarID(databas, verkId));
                                string xHit = bc.CheckIfBarcodeExists(databas, streckkod, orgarID, kemiDb, orgNod);
                                XmlDocument xDoc = new XmlDocument();
                                xDoc.LoadXml(xHit);
                                //XmlNode xHitNode = xDoc.SelectSingleNode("itemlocation");
                                XmlNode xHitNode = xDoc.SelectSingleNode("itemlocation");

                                bc.Dispose();
                                int xHitar = (Convert.ToInt32(xHitNode["orgar"].InnerText));
                                string product = (Convert.ToString(xHitNode["itemname"].InnerText));
                                int roomID = (Convert.ToInt32(xHitNode["lokalid"].InnerText));
                                int streckkodsOrgID = Convert.ToInt32(xHitNode["orgid"].InnerText);

                                bool sameDepartment = false;
                                //int m_nOrgAr = Convert.ToInt32(bc.GetOrgar(databas, verkId));
                                //string sOrgnod = bc.GetUserOrgnod(databas, m_nOrgAr, FormMain.Get.UserId);
                                string sOrgnod = bc.GetUserOrgnod(databas, ar_w, FormMain.Get.UserId,2);
                                XmlDocument doc = new XmlDocument();
                                doc.LoadXml(sOrgnod);

                                m_nOrgNod = 0;
                                XmlNodeList xmlnodeList = doc.DocumentElement.SelectNodes("item");
                                foreach (XmlNode item in xmlnodeList)
                                {
                                    m_nOrgNod = (Convert.ToInt32(item.Attributes["orgnod"].Value));
                                    int m = m_nOrgNod;
                                    //string OrgNod = (item.Attributes["orgnod"].Value);

                                    //------Stoppa kassera om produkten ej tillhör din organisation-------
                                    if (streckkodsOrgID == m_nOrgNod)
                                    {
                                        sameDepartment = true;
                                        break;
                                    }
                                    else
                                    {
                                        sameDepartment = false;
                                    }
                                    ////----------------------------------------------------------------

                                }
                                bool sameDepartment1 = sameDepartment;

                                if (sameDepartment1 == false)
                                {
                                    sameDepartment = false;
                                    Form discard = new FormDiscardCheck(xHitNode, sameDepartment, databas, streckkod, kemiDb, m_nOrgNod, orgarID, product, roomID, streckkodsOrgID);
                                    DialogResult discardResult = new DialogResult();

                                    discardResult = discard.ShowDialog(this.Owner);
                                    if (discardResult != DialogResult.OK) return;
                                    discard.Dispose();
                                }
                                else
                                {
                                    sameDepartment = true;
                                }



                            }
                            //slut


                            //int updateInvent1 = Convert.ToInt32(bc.CheckOutKpinvTrans(kemiDb, databas, barcode, userName, 0));
                            string updateInvent1_q = bc.CheckOutKpinvTrans(kemiDb, databas, barcode, userName, 0);
                            XmlDocument doc3 = new XmlDocument();
                            doc3 = new XmlDocument();
                            doc3.LoadXml(updateInvent1_q);
                            XmlNodeList xmlnodeList3 = doc3.DocumentElement.SelectNodes("item");
                            int updateInvent1 = 0;
                            foreach (XmlNode item in xmlnodeList3)
                            {
                                updateInvent1 = Convert.ToInt32(item.Attributes["streckkodsId"].Value);
                            }


                            pStruct.m_sStructProdnamn = ds.Tables["ProdId"].Rows[0]["Prodnam"].ToString();
                            pStruct.m_sStructCas = ds.Tables["ProdId"].Rows[0]["Cas"].ToString();
                            pStruct.m_sStructMangd = ds.Tables["ProdId"].Rows[0]["Mangd"].ToString();
                            pStruct.m_sStructEnhet = ds.Tables["ProdId"].Rows[0]["Ben"].ToString();
                            pStruct.m_sStructFriText = ds.Tables["ProdId"].Rows[0]["BarCodeTxt"].ToString();
                            pStruct.m_sStructSlutdat = ds.Tables["ProdId"].Rows[0]["Slutdat"].ToString();

                            m_aAllaUtcheckade.Add(pStruct);

                            DataTable dt = new DataTable();
                            if (lang == false)
                            {
                                //dt.Columns.Add("ProdId_Id", Type.GetType("System.String"));
                                dt.Columns.Add("Prodnam", Type.GetType("System.String"));
                                dt.Columns.Add("Cas", Type.GetType("System.String"));
                                dt.Columns.Add("Mangd", Type.GetType("System.String"));
                                dt.Columns.Add("Ben", Type.GetType("System.String"));
                                dt.Columns.Add("BarCodeTxt", Type.GetType("System.String"));
                                dt.Columns.Add("Slutdat", Type.GetType("System.String")); // 140919
                            }
                            else
                            {
                                //dt.Columns.Add("ProdId_Id", Type.GetType("System.String"));
                                dt.Columns.Add("Prodnam", Type.GetType("System.String"));
                                dt.Columns.Add("Cas", Type.GetType("System.String"));
                                dt.Columns.Add("Mangd", Type.GetType("System.String"));
                                dt.Columns.Add("Ben", Type.GetType("System.String"));
                                dt.Columns.Add("BarCodeTxt", Type.GetType("System.String"));
                                dt.Columns.Add("Slutdat", Type.GetType("System.String")); // 140919
                            }

                            foreach (ProdStruct pS in m_aAllaUtcheckade)
                            {

                                dt.Rows.Add(new object[] { pS.m_sStructProdnamn, pS.m_sStructCas, pS.m_sStructMangd, pS.m_sStructEnhet, pS.m_sStructFriText, pS.m_sStructSlutdat });
                            }
                            ds.Tables.Add(dt);

                            try
                            {
                                if (lang == true)
                                {
                                    resultDataGridView.DataSource = dt;
                                    resultDataGridView.CurrentRow.Selected = false;
                                    resultDataGridView.Columns[0].HeaderText = "Produktnamn";
                                    resultDataGridView.Columns[0].Width = 150;
                                    resultDataGridView.Columns[1].HeaderText = "CAS";
                                    resultDataGridView.Columns[1].Width = 60;
                                    resultDataGridView.Columns[2].HeaderText = "Mängd";
                                    resultDataGridView.Columns[2].Width = 40;
                                    resultDataGridView.Columns[3].HeaderText = "Enhet";
                                    resultDataGridView.Columns[3].Width = 30;
                                    resultDataGridView.Columns[4].HeaderText = "Streckkod";
                                    resultDataGridView.Columns[4].Width = 80;
                                    resultDataGridView.Columns[5].HeaderText = "Datum";
                                    resultDataGridView.Columns[5].Width = 150;
                                }
                                else
                                {
                                    resultDataGridView.DataSource = dt;
                                    resultDataGridView.CurrentRow.Selected = false;
                                    resultDataGridView.Columns[0].HeaderText = "Product";
                                    resultDataGridView.Columns[0].Width = 150;
                                    resultDataGridView.Columns[1].HeaderText = "CAS";
                                    resultDataGridView.Columns[1].Width = 60;
                                    resultDataGridView.Columns[2].HeaderText = "Amount";
                                    resultDataGridView.Columns[2].Width = 40;
                                    resultDataGridView.Columns[3].HeaderText = "Unit";
                                    resultDataGridView.Columns[3].Width = 30;
                                    resultDataGridView.Columns[4].HeaderText = "Barcode";
                                    resultDataGridView.Columns[4].Width = 80;
                                    resultDataGridView.Columns[5].HeaderText = "Date";
                                    resultDataGridView.Columns[5].Width = 150;

                                }
                                resultDataGridView.Refresh();
                                this.txttradlos.Clear();
                            }
                            //catch (Exception err)
                            catch (Exception exception)
                            {
                                MessageBox.Show("Exception", "There was a PROBLEM. \n" + exception.Message,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                    }

                    //här slutar if//
                }

            }));

        }

   

        private void textBoxScanCode_TextChanged(object sender, EventArgs e)
        {
            if (textBoxScanCode.Text != "")
            {
                labelShowProduct.Text = textBoxScanCode.Text;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBoxScanCode.Clear();
            if (scannerCheckBox.CheckState == CheckState.Checked)
            {
                textBoxScanCode.Enabled = false;
                buttonScanned.Enabled = false;
                //txttradlos.Enabled = true;
                //txttradlos.Focus();
            }
            else
            {
                textBoxScanCode.Enabled = true;
                buttonScanned.Enabled = true;
                textBoxScanCode.Focus();
                //txttradlos.Enabled = false;
            }
        }


        //  sandi - Read in products barcode or users barcode
        private void form_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 65 && e.KeyChar <= 122)) && scannerCheckBox.Checked)
            {
                //scannerTimer.Enabled = false;
                scannerTimer.Stop();

                barcode += e.KeyChar;
                //barcode = txttradlos.Text;

                //scannerTimer.Enabled = true;
                scannerTimer.Start();
            }
      
        }



        //Sandi infört styrning från inifil
        private void scannerTimer_Elapsed(object source, ElapsedEventArgs e)
        {

    //scannerTimer.Enabled = false;
    scannerTimer.Stop();
            //
            if(barcode == "" && barcodeInSettings.Trim() == "")
            {
                barcodeInSettings = "0";
            
            }

    Log = FormMain.Get.PropertyCheck;

        //User barcode and scan barcode is the same, check out user and close
        if (barcodeInSettings == barcode)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
                if (lang == false)
                {
                    result = MessageBox.Show("Product has the same barcode as user.\n\n If you want to discard product - enter YES \n If you want to logout user - enter NO", "BarcodePcApp", buttons);
                }
                else
                {
                    result = MessageBox.Show("Produkten har samma streckkod som användare.\n\n . Vill du kassera en produkt - ange JA \n Om du vill logga ut - ange NEJ", "BarcodePcApp", buttons);
                }                                                                                            

           
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                getProduct(barcode);
                barcode = "";
            }
            else if (result == System.Windows.Forms.DialogResult.No)
            {
                //this.Close();
                //this.Owner.Close();
                this.Invoke(new MethodInvoker(delegate { FormMain.Get.Logout(); }));
            }
            else
            { }

        }
        else if (barcodeInSettings != barcode)
        {
            getProduct(barcode);
            barcode = "";

        }

        else
        {
        }


        }
        

        // Sandi - Send barcode to db and get back username
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
                        MessageBox.Show("Detta är inte en KLARA användare");
                    }

                    
                    bc.Dispose();
                    return;

                }
                else
                {
                    this.Close();
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

        private void txttradlos_TextChanged(object sender, EventArgs e)
        {
            if (txttradlos.Text != "")
            {
               
            }

        }
        
    }
}