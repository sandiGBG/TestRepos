using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using System.Web;

namespace BarcodePcApp
{
    public partial class FormSettings : Form
    {
        private string m_sName = "";
        private string m_sCheck = "";
        private string m_sDatebase = "";
        public string loginBarcode = "";
        private bool showLoginBarcodeOption = false;
        private bool showPropertyCheckOption = false;  // nytt
        private bool ini = false;  // nytt

        private bool prop_fran_inifil = false;  // nytt
        private bool property_settings = false;  // nytt
        //private bool showPropertyChange = false;  // nytt
        private bool LangChange;  // nytt 1.27
        private bool lang;  // nytt 1.27

        private int m_nLayout = 0;

        public FormSettings()
        {
            InitializeComponent();
            lang = FormMain.Get.LangChange;

            if (lang == false)
            {
                this.m_btnCancel.Text = "Cancel";
                this.label1.Text = "Printer name";
                this.m_btnRestore.Text = "Restore default settings";
                this.m_btnOK.Text = "Save";
                this.label2.Text = "Paper size (w/h)";
                this.label3.Text = "Top margin (0=auto)";
                this.label4.Text = "Left margin (0=auto)";
                this.groupBox1.Text = "Barcode";
                this.m_btnShowPrinter.Text = "Show printer settings";
                this.groupBox2.Text = "Inventory";
                this.checkBoxDialog.Text = "Dialog when transferring within department";
                this.BarcodeGroupBox.Text = "Change login barcode";
                this.LoginBarcodeAsDefaultCheckBox.Text = "Use barcode as default for login";
                this.label10.Text = "Login barcode";
                this.chkProperty.Text = "Property Check";
                this.PropertyGroupBox.Text = "Property";
                this.groupBoxLang.Text = "Language";
                //this.chkLang.Text = "Change to swedish";
                this.Name = "FormSettings";
                this.Text = "Barcode settings";
                
            }
            else
            {
                this.m_btnCancel.Text = "Avbryt";
                this.label1.Text = "Skrivarnamn";
                this.m_btnRestore.Text = "Återställ standardinställningarna";
                this.m_btnOK.Text = "Spara";
                this.label2.Text = "Pappersstorlek (b/h)";
                this.label3.Text = "Övre marginal (0=auto)";
                this.label4.Text = "Vänster marginal (0=auto)";
                this.groupBox1.Text = "Streckkod";
                this.m_btnShowPrinter.Text = "Visa skrivarinställningar";
                this.groupBox2.Text = "Inventering";
                this.checkBoxDialog.Text = "Dialog vid förflyttning inom avdelning";
                this.BarcodeGroupBox.Text = "Streckkod för inloggning";
                this.LoginBarcodeAsDefaultCheckBox.Text = "Använd streckkod som standard för inloggning";
                this.label10.Text = "Logga in streckkod";
                this.chkProperty.Text = "Egenskapskontroll";
                this.PropertyGroupBox.Text = "Egenskaper";
                this.groupBoxLang.Text = "Språk";
                //this.chkLang.Text = "Ändra språk till svenska";
                this.Name = "FormSettings";
                this.Text = "Streckkodsinställningar";
            }


            m_textPrintername.Text = FormMain.Get.BarcodePrinter;
            m_textBarcodeWidth.Text = FormMain.Get.BarcodeWidth.ToString();
            m_textBarcodeHeight.Text = FormMain.Get.BarcodeHeight.ToString();
            m_textBarcodeLeft.Text = FormMain.Get.BarcodeLeft.ToString();
            m_textBarcodeTop.Text = FormMain.Get.BarcodeTop.ToString();

            checkBoxDialog.Checked = FormMain.Get.ShowDialogOnSameDepartment;
            chkProperty.Checked = FormMain.Get.PropertyCheck;
            prop_fran_inifil = FormMain.Get.PropertyCheck;
            //rbtnSve.Checked = FormMain.Get.LangChange;

           LangChange = FormMain.Get.LangChange;  // ver 1.27

            //showPropertyCheckOption = chkProperty.Checked;
            if (prop_fran_inifil == false)
            {
                //showPropertyCheckOption = false;  //nytt
                //chkProperty.Checked = false;
                //PropertyGroupBox.Enabled = false;

            }


            //showPropertyCheckOption = false;  //nytt
            //PropertyGroupBox.Enabled = true;
            //chkProperty.Enabled = false;
            //nytt 20161121
            //prop_fran_inifil = FormMain.Get.PropertyCheck;

            //if (prop_fran_inifil == false)
            //{
            //    showPropertyCheckOption = false;  //nytt
            //    chkProperty.Checked = false;
            //    PropertyGroupBox.Enabled = false;

            //}
            
            m_nLayout = FormMain.Get.BarcodeLayout;

            try
            {
                StreamReader sre = new StreamReader(FormMain.iniFilePath);
                char[] sepChr = new char[1] { '|' };
                string iniRad;
                string[] iniKol;
                while ((iniRad = sre.ReadLine()) != null)
                {
                    iniKol = iniRad.Split(sepChr);
                    switch (iniKol[0].Trim())
                    {
                        case "databas":
                            m_sDatebase = iniKol[1].Trim();
                            break;
                        case "name":
                            m_sName = iniKol[1].Trim();
                            break;
                        case "barcode_printer":
                            m_textPrintername.Text = iniKol[1].Trim();
                            break;
                        case "barcode_width":
                            m_textBarcodeWidth.Text = iniKol[1].Trim();
                            break;
                        case "barcode_height":
                            m_textBarcodeHeight.Text = iniKol[1].Trim();
                            break;
                        case "barcode_left":
                            m_textBarcodeLeft.Text = iniKol[1].Trim();
                            break;
                        case "barcode_top":
                            m_textBarcodeTop.Text = iniKol[1].Trim();
                            break;
                        case "barcode_layout":
                            m_nLayout = Convert.ToInt32(iniKol[1].Trim());
                            break;
                        case "check":
                            m_sCheck = iniKol[1].Trim();
                            break;
                        case "inventory_dialog_same_org":
                            if (iniKol[1].Trim().Equals("True"))
                            {
                                checkBoxDialog.Checked = true;
                            }
                            break;
                        case "login_barcode":
                            showLoginBarcodeOption = true;
                            BarcodeGroupBox.Enabled = true;
                            loginBarcodeTextBox.Enabled = true;
                            LoginBarcodeAsDefaultCheckBox.Enabled = true;
                            if (iniKol[1].Trim().Equals("True"))
                            {
                                LoginBarcodeAsDefaultCheckBox.Checked = true;
                            }
                            else
                            {
                                LoginBarcodeAsDefaultCheckBox.Checked = false;
                            }
                            break;
                            //ny case
                        //case "property_check":
                        //    if (iniKol[1].Trim().Equals("True"))
                        //    {
                        //        showPropertyCheckOption = true;  //nytt
                        //        if (chkProperty.Checked)
                        //        {

                        //            PropertyGroupBox.Enabled = true;
                        //            chkProperty.Checked = true;
                        //            PropertyGroupBox.Visible = true;

                        //        }
                        //        else
                        //        {
                        //            chkProperty.Checked = false;
                        //            PropertyGroupBox.Enabled = true;
                        //        }
                        //    }
                        //        break;
                        case "property_check":
                            
                            //PropertyGroupBox.Enabled = false;
                            //chkProperty.Checked = false;
                            // showPropertyCheckOption = true;
                            //PropertyGroupBox.Visible = false;
                            if (iniKol[1].Trim().Equals("True"))
                                {
                                    PropertyGroupBox.Enabled = true;
                                    chkProperty.Checked = true;
                                    showPropertyCheckOption = true;
                                    ini = true;
                                
                                }
                             else
                                {
                                    PropertyGroupBox.Enabled = true;
                                    chkProperty.Checked = false;
                                    showPropertyCheckOption = true;
                                    ini = true;
                                }
                            break;
                        case "lang_set":
                            if (iniKol[1].Trim().Equals("True"))
                            {
                                    rbtnSve.Checked = true;
                                    LangChange = true;
                                    FormMain.Get.LangChange = true;
                            }
                            else
                                {
                                    rbtnEng.Checked = true;
                                    LangChange = false;
                                    FormMain.Get.LangChange = false;
                            }
                            break;


                    } // /switch
                }  // /while
                sre.Close();
            }
            catch
            {
            }

            try
            {
                barcode.BarcodeService bc = FormMain.getBarcodeService();
                loginBarcode = bc.GetLoginBarcode(m_sDatebase, FormMain.Get.UserId);

                loginBarcodeTextBox.Text = loginBarcode;

                bc.Dispose();
            }
            catch (Exception err)
            {
                //TODO: meddelande
            }

            if (m_nLayout < 0 || m_nLayout >= m_cbLayout.Items.Count)
                m_nLayout = 0;
            m_cbLayout.SelectedIndex = m_nLayout;
        }

        private void m_btnOK_Click(object sender, EventArgs e)
        {
            string tal = m_textBarcodeWidth.Text.Split(',')[0].Trim();
            string tal1 = m_textBarcodeHeight.Text.Split(',')[0].Trim();
            m_textBarcodeHeight.Text = tal1;
            m_textBarcodeWidth.Text = tal;
            if (!FormMain.IsNumeric(m_textBarcodeWidth.Text))
            {
                if (lang == false)
                {

                    MessageBox.Show("Barcode width must be numeric");
                }
                else
                {

                    MessageBox.Show("Streckkodens bredd måste vara numerisk");
                }

            }
            else if (!FormMain.IsNumeric(m_textBarcodeHeight.Text))
            {
                if (lang == false)
                {
                    MessageBox.Show("Barcode height must be numeric");
                }
                else
                {
                    MessageBox.Show("Streckkodens höjd måste vara numerisk");
                }

                
            }
            else if (!FormMain.IsNumeric(m_textBarcodeLeft.Text))
            {
                if (lang == false)
                {
                    MessageBox.Show("Barcode left margin must be numeric");
                }
                else
                {
                    MessageBox.Show("Streckkodens vänster marginal måste vara numerisk");
                }

               
            }
            else if (!FormMain.IsNumeric(m_textBarcodeTop.Text))
            {
                if (lang == false)
                {
                    MessageBox.Show("Barcode top margin must be numeric");
                }
                else
                {
                    MessageBox.Show("Streckkodens övre marginal måste vara numerisk");
                }

                
            }
            else
            {
                m_nLayout = Convert.ToInt32(m_cbLayout.Text.Replace("Layout ",""))-1;

                FormMain.Get.BarcodePrinter = m_textPrintername.Text;
                FormMain.Get.BarcodeWidth = Convert.ToInt32(m_textBarcodeWidth.Text);
                FormMain.Get.BarcodeHeight = Convert.ToInt32(m_textBarcodeHeight.Text);
                //FormMain.Get.BarcodeWidth = m_textBarcodeWidth.Text;
                //FormMain.Get.BarcodeHeight = m_textBarcodeHeight.Text;
                FormMain.Get.BarcodeTop = Convert.ToInt32(m_textBarcodeTop.Text);
                FormMain.Get.BarcodeLeft = Convert.ToInt32(m_textBarcodeLeft.Text);
                FormMain.Get.BarcodeLayout = m_nLayout;
                FormMain.Get.ShowDialogOnSameDepartment = checkBoxDialog.Checked;
                //Sandi
                //FormMain.Get.PropertyCheck = chkProperty.Checked;
                FormMain.Get.barcodeInSettings = loginBarcode;
                if(rbtnSve.Checked)
                {
                    FormMain.Get.LangChange =true;
                    lang = FormMain.Get.LangChange;
                    rbtnSve.Checked = true;
                    rbtnEng.Checked = false;
                }
                else
                {
                    FormMain.Get.LangChange = false;
                    lang = FormMain.Get.LangChange;
                    rbtnEng.Checked = true;
                    rbtnSve.Checked = false;
                }


                //Sandi
                bool a = FormMain.Get.PropertyCheck;
                bool b = chkProperty.Checked;

                //if (a)
                //{
                //    sw.WriteLine("property_check | " + a);
                //}

                //if (b)
                //{
                //    sw.WriteLine("property_change | " + b);
                //}
                //else
                //{
                //    sw.WriteLine("property_change | " + b);
                //}


                StreamWriter sw = new StreamWriter(FormMain.iniFilePath);
                sw.WriteLine("databas | " + m_sDatebase);
                sw.WriteLine("name | " + m_sName);
                sw.WriteLine("barcode_printer | " + m_textPrintername.Text);
                sw.WriteLine("barcode_width | " + m_textBarcodeWidth.Text);
                sw.WriteLine("barcode_height | " + m_textBarcodeHeight.Text);
                sw.WriteLine("barcode_left | " + m_textBarcodeLeft.Text);
                sw.WriteLine("barcode_top | " + m_textBarcodeTop.Text);
                sw.WriteLine("barcode_layout | " + m_nLayout);
                sw.WriteLine("forced_update | " + FormMain.forcedUpdate.ToString());
                sw.WriteLine("inventory_dialog_same_org | " + checkBoxDialog.Checked.ToString());

                if (rbtnSve.Checked)
                {
                    sw.WriteLine("lang_set | " + rbtnSve.Checked.ToString());
                }
                

                if (showPropertyCheckOption && ini)
                {
                    sw.WriteLine("property_check | " + b.ToString());
                }

                if (showLoginBarcodeOption)
                {
                    sw.WriteLine("login_barcode | " + LoginBarcodeAsDefaultCheckBox.Checked.ToString());
                }

                if (FormMain.Get.ScanRoomsEnabled)
                {
                    sw.WriteLine("scan_rooms | " + FormMain.Get.ScanRoomsEnabled.ToString());
                }
                sw.WriteLine("check | " + m_sCheck);
                sw.Close();



                bool barcodeSaveResult = true;
                //Spara din nya barcode
                if (loginBarcodeTextBox.Text != loginBarcode)
                {
                    try
                    {
                        barcode.BarcodeService bc = FormMain.getBarcodeService();

                        if (bc.SaveLoginBarcode(m_sDatebase, FormMain.Get.UserId, loginBarcodeTextBox.Text.Trim()) == "false")
                        {
                            barcodeSaveResult = false;
                        }
                        

                        bc.Dispose();
                    }
                    catch (Exception err)
                    {
                        //TODO: meddelande
                    }
                }

                if (barcodeSaveResult)
                {
                    this.Close();
                }
                else
                {
                    if (lang == false)
                    {
                        MessageBox.Show("This barcode is already in use. Please select another one.");
                    }
                    else
                    {
                        MessageBox.Show("Den här streckkoden används redan. Välj en annan streckkod");
                    }

                    
                }
                
            }

        }


        private void m_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_btnRestore_Click(object sender, EventArgs e)
        {
            m_textBarcodeWidth.Text = "40";
            m_textBarcodeHeight.Text = "21";
            m_textBarcodeTop.Text = "0";
            m_textBarcodeLeft.Text = "0";
            m_textPrintername.Text = "Okänd";
            m_cbLayout.SelectedIndex = 0;
            checkBoxDialog.Checked = false;
        }

        private void m_btnShowPrinter_Click(object sender, EventArgs e)
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
                if (lang == false)
                {
                    MessageBox.Show("No label printer is installed.");
                }
                else
                {
                    MessageBox.Show("Skrivaren är inte installerad");
                }


                return;
            }

            if (lang == false)
            {
                string s = "Printer name: " + pd.DefaultPageSettings.PrinterSettings.PrinterName;
                s = s + "\r\nResolution DPI (x,y): " + pd.DefaultPageSettings.PrinterResolution.X + ", " + pd.DefaultPageSettings.PrinterResolution.Y;
                s = s + "\r\nLandscape: " + pd.DefaultPageSettings.Landscape;
                s = s + "\r\n\r\nPaper name: " + pd.DefaultPageSettings.PaperSize.PaperName;
                s = s + "\r\nPaper kind: " + pd.DefaultPageSettings.PaperSize.Kind;
                s = s + "\r\nPaper width (pixel): " + pd.DefaultPageSettings.PaperSize.Width;
                s = s + "\r\nPaper height (pixel): " + pd.DefaultPageSettings.PaperSize.Height;
                //s = s + "\r\nPaper height (pixel): " + pd.PrinterSettings.PaperSizes.

                MessageBox.Show(s, "Printer and paper settings");
            }
            else
            {
                string s = "Skrivarnamn: " + pd.DefaultPageSettings.PrinterSettings.PrinterName;
                s = s + "\r\nUpplösning DPI (x,y): " + pd.DefaultPageSettings.PrinterResolution.X + ", " + pd.DefaultPageSettings.PrinterResolution.Y;
                s = s + "\r\nLiggande: " + pd.DefaultPageSettings.Landscape;
                s = s + "\r\n\r\nPappersnamn: " + pd.DefaultPageSettings.PaperSize.PaperName;
                s = s + "\r\nPaperstyp: " + pd.DefaultPageSettings.PaperSize.Kind;
                s = s + "\r\nPappersbredd (pixel): " + pd.DefaultPageSettings.PaperSize.Width;
                s = s + "\r\nPappershöjd (pixel): " + pd.DefaultPageSettings.PaperSize.Height;
                //s = s + "\r\nPaper height (pixel): " + pd.PrinterSettings.PaperSizes.

                MessageBox.Show(s, "Inställningar för skrivare och papper");
            }
        }

        private void chkProperty_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProperty.CheckState == CheckState.Checked && ini)
            {
                //chkProperty.Checked = true;
                chkProperty.CheckState = CheckState.Checked;
                //property_settings = chkProperty.Checked;
                FormMain.Get.PropertyCheck = true;
                showPropertyCheckOption = true;

            }
            else if (chkProperty.CheckState == CheckState.Unchecked && ini)
            {
                //chkProperty.Checked = false;
                chkProperty.CheckState = CheckState.Unchecked;
                //property_settings = chkProperty.Checked;
                FormMain.Get.PropertyCheck = false;
                showPropertyCheckOption = true;
            }
            else
            {
                //chkProperty.CheckState = CheckState.Unchecked;
                //FormMain.Get.PropertyCheck = false;
                //showPropertyCheckOption = false;
            
            }

        }

        private void rbtnSve_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnSve.Checked)
            {
                FormMain.Get.LangChange = true;
                rbtnSve.Checked = true;
                rbtnEng.Checked = false;
            }
            else
            {
                FormMain.Get.LangChange = false;
                rbtnSve.Checked = false;
                rbtnEng.Checked = true;
            }
        }

        private void rbtnEng_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbtnEng.Checked)
            //{
            //    FormMain.Get.LangChange =false;
            //    rbtnSve.Checked = false;
            //    rbtnEng.Checked = true;
            //}
            //else
            //{
            //    FormMain.Get.LangChange = true;
            //    rbtnSve.Checked = true;
            //    rbtnEng.Checked = false;
            //}
            if (rbtnEng.Checked)
            {
                FormMain.Get.LangChange = false;
                rbtnSve.Checked = false;
                rbtnEng.Checked = true;
            }
            else
            {
                FormMain.Get.LangChange = true;
                rbtnSve.Checked = true;
                rbtnEng.Checked = false;
            }
        }


        private void checkBoxDialog_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDialog.Checked)
            {
                checkBoxDialog.Checked = true;
                FormMain.Get.ShowDialogOnSameDepartment = true;

            }
            else
            {
                checkBoxDialog.Checked = false;
                FormMain.Get.ShowDialogOnSameDepartment = false;

            }

        }
    }
}