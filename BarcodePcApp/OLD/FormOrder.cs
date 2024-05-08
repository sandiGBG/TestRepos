using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Timers;
using System.Text.RegularExpressions;
       


namespace BarcodePcApp
{
    
    public partial class FormOrder : Form
    {
        private string m_sCAS;
        private string m_sStreckkod;
        private string m_sSummaformel;
        private string m_sComment;
        private System.Timers.Timer scannerTimer = new System.Timers.Timer();
        private string barcode = "";
        private string barcodeInSettings = "";
        private bool Log;
        public string datum_max = "";
        public string datum_m = "";

        private string m_sSkapat_datum = "";
        private string m_sUtgangsdatum = "";
        private string m_sRumNamn = "";
        public string m_sRoom = "";
        public string m_sCabinet = "";
        public string m_sCabinet_Name = "";
        public string init = "";


        public FormOrder()
        {
            InitializeComponent();

            // Sandi
            this.KeyPress += new KeyPressEventHandler(form_KeyPress);
            scannerTimer.Interval = 100;
            scannerTimer.Elapsed += scannerTimer_Elapsed;
            FormSettings form = new FormSettings();
            barcodeInSettings = form.loginBarcode;
            this.ActiveControl = textBoxScanCode;
            Log = FormMain.Get.LoginBarcode;

            //Skapa initialer
            string name123 = FormMain.Get.Usernamestring;
            Regex initials = new Regex(@"(\b[a-öA-Ö])[a-öA-Ö]* ?");
            init = initials.Replace(name123, "$1");

        }


        private void m_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            //this.Invoke(new MethodInvoker(delegate { FormMain.Get.Logout(); }));
        }

        private void m_btnPrint_Click(object sender, EventArgs e)
        {
            if (m_gridOrder.SelectedRows.Count == 1)
            {
                if (m_textAntal.Text.Length > 0 && FormMain.IsNumeric(m_textAntal.Text))
                {
                    int nCheckout = Convert.ToInt32(m_textAntal.Text);
                    string sComment = commentTxtBox.Text;

                    int id = Convert.ToInt32(m_gridOrder.SelectedRows[0].Cells["id"].Value);
                    int nDiff = Convert.ToInt32(m_gridOrder.SelectedRows[0].Cells["antal"].Value) - Convert.ToInt32(m_gridOrder.SelectedRows[0].Cells["levantal"].Value);
                    int gasTubes = Convert.ToInt32(m_gridOrder.SelectedRows[0].Cells["tubes"].Value);

                    if (nCheckout > nDiff)
                    {
                        MessageBox.Show("You have entered too many articles.");
                        return;
                    }

                    barcode.BarcodeService bc = FormMain.getBarcodeService();

                    try
                    {

                        string sList = bc.OrderCheckOut(FormMain.Get.Databas, FormMain.Get.KemiDB, FormMain.Get.VerkId, 1, id, nCheckout, FormMain.Get.UserId, FormMain.Get.Username, 0, FormMain.Get.BarcodePrefix, gasTubes, sComment);


                        string Cabinet_Room = bc.GetRoom_Cabinet(FormMain.Get.Databas, FormMain.Get.KemiDB, id,1);
                        DataSet ds = new DataSet();
                        StringReader stri = new StringReader(Cabinet_Room);
                        ds.ReadXml(stri);
                        DataTable dt = ds.Tables[0];
                        DataRow dr = dt.Rows[0];
                        m_sRoom = dr["StorageName"].ToString();
                        m_sCabinet = dr["Skap"].ToString();

                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(sList);

                        List<string> aStreckkod = new List<string>();

                        int nRes = Convert.ToInt32(doc.DocumentElement.Attributes["status"].Value);
                        if (nRes >= 0)
                        {
                            XmlNodeList xmlnodeList = doc.DocumentElement.SelectNodes("rad");
                            foreach (XmlNode item in xmlnodeList)
                            {
                                string s = item.Attributes["kod"].Value;
                                aStreckkod.Add(s);
                            }

                            PrintBarcode(m_gridOrder.SelectedRows[0].Cells["Cas"].Value.ToString(),
                                         m_gridOrder.SelectedRows[0].Cells["kemiskbet"].Value.ToString(),
                                         aStreckkod.ToArray());

                            if (nRes == 0)
                                m_gridOrder.SelectedRows[0].Cells["levantal"].Value = Convert.ToInt32(m_gridOrder.SelectedRows[0].Cells["levantal"].Value) + nCheckout;
                            else if (nRes == 1)
                                m_gridOrder.Rows.RemoveAt(m_gridOrder.SelectedRows[0].Index);
                        }
                        else
                        {
                            MessageBox.Show("Failed to retrieve the order from the database.");
                        }

                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("No contact with the web server!\r\n\nCheck the Internet connection.\r\n\nError message:\r\n\n" + err.Message, "GetOrderList");
                        bc.Dispose();
                        return;
                    }
                }
                else
                    MessageBox.Show("You must enter a numeric value in the print field.");
            }

            commentTxtBox.Text = "";
            textBoxScanCode.Focus();
        }

        #region Print barcode
        private void PrintBarcode(string pCAS, string pSummaformel, string[] pStreckkod)
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
            datum_max =d.ToString("yy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            datum_m = init + "/" + d.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);


            if (FormMain.Get.BarcodeLayout == 0)
            {
                m_sComment = "";
                m_sSummaformel = "";
                if (pCAS.Length > 0)
                    m_sCAS = "CAS: " + pCAS.Trim();
                else
                    m_sCAS = "";
                bc.DisplayCode = true;
            }
            else if (FormMain.Get.BarcodeLayout == 1)
            {
                m_sComment = "";
                m_sSummaformel = pSummaformel;
                if (pCAS.Length > 0)
                    m_sCAS = pCAS.Trim();
                else
                    m_sCAS = "";

                bc.Text = "";
                bc.BarHeight = 4;
                bc.DisplayCode = true;
            }
            else if (FormMain.Get.BarcodeLayout == 2)
            {
                m_sComment = "";
                m_sSummaformel = pSummaformel;
                if (pCAS.Length > 0)
                    m_sCAS = pCAS.Trim();
                else
                    m_sCAS = "";

                bc.Text = "";
                bc.BarHeight = 4;
                bc.DisplayCode = false;
                bc.CodeAlignment = Neodynamic.WinControls.BarcodeProfessional.Alignment.AboveLeft;

                //bc.Text = pSummaformel;
                //bc.TextAlignment = Neodynamic.WinControls.BarcodeProfessional.Alignment.BelowCenter;
            }
            else if (FormMain.Get.BarcodeLayout == 3)
            {
                m_sComment = commentTxtBox.Text;
                if (m_sComment.Length >= 6)
                {
                    m_sComment = m_sComment.Substring(0, 6);
                }

                m_sSummaformel = pSummaformel;
                if (pCAS.Length > 0)
                    m_sCAS = pCAS.Trim();
                else
                    m_sCAS = "";

                bc.Text = "";
                bc.BarHeight = 4;
                bc.DisplayCode = false;
                bc.CodeAlignment = Neodynamic.WinControls.BarcodeProfessional.Alignment.AboveLeft;
            }

            else if (FormMain.Get.BarcodeLayout == 4)
            {
                m_sComment = commentTxtBox.Text;
                if (m_sComment.Length >= 6)
                {
                    m_sComment = m_sComment.Substring(0, 6);
                }

                m_sSummaformel = datum_max;
                if (pCAS.Length > 0)
                    m_sCAS = pCAS.Trim();
                else
                    m_sCAS = "";

                bc.Text = "";
                bc.BarHeight = 4;
                bc.DisplayCode = false;
                bc.CodeAlignment = Neodynamic.WinControls.BarcodeProfessional.Alignment.AboveLeft;
            }

            else if (FormMain.Get.BarcodeLayout == 5)
            {
                
                m_sComment = commentTxtBox.Text;
                if (m_sComment.Length >= 6)
                {
                    m_sComment = m_sComment.Substring(0, 6);
                }

                m_sSummaformel = datum_max;
                if (pCAS.Length > 0)
                    m_sCAS = pCAS.Trim();
                else
                    m_sCAS = "";

                m_sSkapat_datum = datum_m;
                m_sRumNamn= m_sRoom;
                m_sCabinet_Name = m_sCabinet;

                bc.Text = "";
                bc.BarHeight = 4;
                bc.DisplayCode = false;
                bc.CodeAlignment = Neodynamic.WinControls.BarcodeProfessional.Alignment.AboveLeft;
            }

            foreach (string streckkod in pStreckkod)
            {
                m_sStreckkod = streckkod;
                bc.Code = streckkod;

                pd.Print();
            }
        }

        private void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs ppea)
        {
            //TODO: add proper note
            BarcodeLayout.PrintDocumentOnPrintPage(sender, ppea, bc, m_sCAS, m_sSummaformel, m_sStreckkod, m_sComment, datum_max, m_sSkapat_datum, m_sUtgangsdatum, "", m_sCabinet, m_sRumNamn);// saknas datum, utgångsdatum,user,skåp,rum
        }
        #endregion

        private void FormOrder_Load(object sender, EventArgs e)
        {
            RefreshOrderlist();
            textBoxScanCode.Focus();
        }

        private void m_cbOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshOrderlist()
        {
            m_cbOrders.BeginUpdate();

            m_gridOrder.DataSource = null;

            m_cbOrders.Items.Clear();

            int nPos = m_cbOrders.Items.Add(new ComboboxValue(0, "All"));

            barcode.BarcodeService bc = FormMain.getBarcodeService();

            try
            {
                string sList = bc.GetOrderList(FormMain.Get.Databas, FormMain.Get.UserId, FormMain.Get.OrgAr);
                //MessageBox.Show("aktar: " + FormMain.Get.OrgAr + " userid: " + FormMain.Get.UserId);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(sList);

                XmlNodeList xmlnodeList = doc.DocumentElement.SelectNodes("rad");
                foreach (XmlNode item in xmlnodeList)
                {
                    string sRow = Convert.ToString(item.SelectSingleNode("datum").InnerText);
                    if (Convert.ToInt32(item.SelectSingleNode("orderbekraftnr").InnerText) > 0)
                        sRow = sRow + " " + Convert.ToString(item.SelectSingleNode("orderbekraftnr").InnerText);
                    m_cbOrders.Items.Add(new ComboboxValue(Convert.ToInt32(item.SelectSingleNode("id").InnerText), sRow));
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("No contact with the web server!\r\n\nCheck the Internet connection.\r\n\nError message:\r\n\n" + err.Message, "GetOrderList");
                bc.Dispose();
                return;
            }
            m_cbOrders.EndUpdate();

            m_cbOrders.SelectedIndex = nPos;
        }

        private void RefreshGrid()
        {
            int nOrderID = 0;

            if (m_cbOrders.SelectedItem != null)
            {
                nOrderID = ((ComboboxValue)m_cbOrders.SelectedItem).Id;
            }

            barcode.BarcodeService bc = FormMain.getBarcodeService();

            try
            {
                System.Diagnostics.Debug.Write("Verksamhet: "+ FormMain.Get.VerkId +"\ndatabas: " + FormMain.Get.Databas + "\nKemidb: " + FormMain.Get.KemiDB + "\n orderID: " + nOrderID + "\n userid: " +FormMain.Get.UserId + "\nOrgar: " + FormMain.Get.OrgAr);
                string sOrder = bc.GetOrder(FormMain.Get.Databas, FormMain.Get.KemiDB, 1, nOrderID, FormMain.Get.UserId, FormMain.Get.OrgAr);

                System.Diagnostics.Debug.Write(sOrder);

                sOrder = sOrder.Replace("<sub>", "");
                sOrder = sOrder.Replace("</sub>", "");
                sOrder = sOrder.Replace("<sup>", "");
                sOrder = sOrder.Replace("</sup>", "");

                XmlTextReader xtr = new XmlTextReader(new StringReader(sOrder));
                DataSet ds = new DataSet();
                ds.ReadXml(xtr);

                if (ds.Tables.Count > 0)
                {
                    m_gridOrder.DataSource = ds.Tables[0];
                    if (m_gridOrder.CurrentRow != null)
                        m_gridOrder.CurrentRow.Selected = false;

                    m_gridOrder.Columns["Id"].Visible = false;
                    m_gridOrder.Columns["orderID"].Visible = false;
                    m_gridOrder.Columns["refnr"].Visible = false;
                    m_gridOrder.Columns["status"].Visible = false;

                    m_gridOrder.Columns["datum"].HeaderText = "Order date";
                    m_gridOrder.Columns["vem"].HeaderText = "Client";
                    m_gridOrder.Columns["cas"].HeaderText = "CAS";
                    m_gridOrder.Columns["kemiskbet"].HeaderText = "Formula";
                    m_gridOrder.Columns["prodnamn"].HeaderText = "Product";
                    m_gridOrder.Columns["prodnamn"].Width = 130;
                    m_gridOrder.Columns["artnr"].HeaderText = "Article number";
                    m_gridOrder.Columns["mangd"].HeaderText = "Pack size";
                    m_gridOrder.Columns["mangd"].Width = 80;

                    m_gridOrder.Columns["antal"].HeaderText = "Number";
                    m_gridOrder.Columns["antal"].Width = 55;
                    m_gridOrder.Columns["levantal"].HeaderText = "Delivered";
                    m_gridOrder.Columns["levantal"].Width = 60;
                    m_gridOrder.Columns["leverantor"].HeaderText = "Supplier";
                    m_gridOrder.Columns["leverantor"].Width = 75;

                    m_gridOrder.Columns["tubes"].Visible = false;
                }
                else
                    m_gridOrder.DataSource = null;
            }
            catch (Exception err)
            {
                MessageBox.Show("No contact with the web server!\r\n\nCheck the Internet connection.\r\n\nError message:\r\n\n" + err.Message, "GetOrder");
                bc.Dispose();
                return;
            }
        }

        private void m_btnRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the order line?", "Remove", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (m_gridOrder.SelectedRows.Count == 1)
                {
                    int id = Convert.ToInt32(m_gridOrder.SelectedRows[0].Cells["Id"].Value);

                    barcode.BarcodeService bc = FormMain.getBarcodeService();

                    try
                    {
                        double dRet =Convert.ToDouble(bc.OrderDeleterow(FormMain.Get.Databas, id));
                        if(dRet==1)
                            RefreshOrderlist();
                        else
                            RefreshGrid();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("No contact with the web server!\r\n\nCheck the Internet connection.\r\n\nError message:\r\n\n" + err.Message, "GetOrder");
                        bc.Dispose();
                        return;
                    }
                }
            }
        }

        //Sandi
        private void textBoxScanCode_TextChanged(object sender, EventArgs e)
        {
            if(Log==true)
            {
            if (textBoxScanCode.Text != "")
                {
                    if (textBoxScanCode.Text == barcodeInSettings)
                    {
                        labelShowProduct.Text = textBoxScanCode.Text;
                        //this.Close();
                        //this.Owner.Close();
                        this.Invoke(new MethodInvoker(delegate { FormMain.Get.Logout(); }));
                    }
                    //labelShowProduct.Text = textBoxScanCode.Text;

                }
            }
            else
            {

            }
        }
       
       
        //Sandi
        //private void checkBox1_CheckedChanged(object sender, EventArgs e)
        private void scannerCheckBox_CheckedChanged(object sender, EventArgs e)    
        {
            textBoxScanCode.Clear();
            if (scannerCheckBox.CheckState == CheckState.Checked)
            {
                panel1.Visible = true; 
                buttonScanned.Visible = false;
                textBoxScanCode.Enabled = true;
                textBoxScanCode.Focus();
                m_btnClose.Visible = false;
            }
            else
            {
                panel1.Visible = false;
                buttonScanned.Visible = true;
                textBoxScanCode.Enabled = true;
                m_btnClose.Visible = true;
                textBoxScanCode.Focus();
            }
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


        //Sandi
        private void scannerTimer_Elapsed(object source, ElapsedEventArgs e)
        {
            //scannerTimer.Enabled = false;
            scannerTimer.Stop();

            if (Log == true)
            {
                //User barcode and scan barcode is the same, check out user and close
                if (barcodeInSettings == barcode)
                {
                    this.Close();
                    this.Owner.Close();
                }
            }
        }

        private void buttonScanned_Click(object sender, EventArgs e)
        {
            if (textBoxScanCode.Text == barcodeInSettings)
            {
                this.Close();
            }
            else
            {

                MessageBox.Show("You need to scan your barcode!");
                textBoxScanCode.Focus();

            }
        }


        //private void button_webb_Click(object sender, EventArgs e)
        //{
        //    System.Diagnostics.Process.Start("http://www.google.se");

        //}

        

    }

    class ComboboxValue
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public ComboboxValue(int id, string name) { Id = id; Name = name; }
        public override string ToString() { return Name; }
    }

    


}
