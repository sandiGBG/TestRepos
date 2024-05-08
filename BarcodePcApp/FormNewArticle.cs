using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace BarcodePcApp
{
    public partial class FormNewArticle : Form
    {
        public int Aret;
        public string supplier = "";
        public string supp;
        public string kemi;
        public string artikel;
        public int enhid;
        public int levid;
        public int prod;
        public string databas;
        public string kemidb;
        public static bool lang = false;                 //ver 1.27




        public FormNewArticle(string supp,string kemi,int enhid,int lev,int prodid,string db)
        {

            InitializeComponent();
            levid = lev;
            prod = prodid;
            databas = db;
            supplier = supp.ToString();
            txtSupplier.Text = supplier;
            kemidb = kemi;


            cboKval.DataSource = null;
            cboKval.DisplayMember = "";
            cboKval.ValueMember = "";
            cboKval.Text = "";

            cboEnhet.DataSource = null;
            cboEnhet.DisplayMember = "";
            cboEnhet.ValueMember = "";
            cboEnhet.Text = "";

            lang = FormMain.Get.LangChange;

            if (lang == true)
            {
                this.lblArticle.Text = "Ny artikel";
                this.buttonClose.Text = "Stäng";
                this.label1.Text = "Leverantör";
                this.label2.Text = "Art nr";
                this.label3.Text = "Förpackningsstorlek";
                this.label4.Text = "Kvalitet";
                this.label5.Text = "Koncentration";
                this.buttonSave.Text = "Spara";


            }
            else
            {
                this.lblArticle.Text = "New article";
                this.buttonClose.Text = "Close";
                this.label1.Text = "Supplier";
                this.label2.Text = "Art-no";
                this.label3.Text = "Pack-size";
                this.label4.Text = "Quality";
                this.label5.Text = "Conc.";
                this.buttonSave.Text = "Save";

            }

            barcode.BarcodeService bc = FormMain.getBarcodeService();

            XmlTextReader xtr = null;
            StringReader str = new StringReader(bc.GetQuality(kemidb));
            xtr = new XmlTextReader(str);
            DataSet dts = new DataSet();
            dts.ReadXml(xtr);
            cboKval.DataSource = dts.Tables["Quality"]; // OBS! Här aktiveras metoden comboBoxSupplier_SelectedIndexChanged()
            cboKval.DisplayMember = "txt";   
            
            if (lang == true)
            {
                cboKval.Text = "--- Välj ---";
            }
            else
            {
                cboKval.Text = "--- Choose ---";
            }
            cboKval.Text = "--- Choose ---";


            barcode.BarcodeService bc1 = FormMain.getBarcodeService(); 
            XmlTextReader xabc = null;
            StringReader str1 = new StringReader(bc1.GetUnit(kemidb,enhid));
            xabc = new XmlTextReader(str1);
            DataSet dts1 = new DataSet();
            dts1.ReadXml(xabc);
            cboEnhet.DataSource = dts1.Tables["Unit"];
            cboEnhet.DisplayMember = "Namn";
            cboEnhet.ValueMember = "Id";


            dts.Dispose();
            bc.Dispose();

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            artikel = txtArticle.Text.ToString();
            string lev = levid.ToString();

            if (artikel=="")
            {
                if (lang ==true)
                {
                    MessageBox.Show("Artikel måste ha ett värde.");
                }
                else
                {
                    MessageBox.Show("You must enter a value in the article field.");
                }
                
            }
            else if (txtFRP.Text =="")
            {
                if (lang == true)
                {
                    MessageBox.Show("Ange ett värde i fältet Paketstorlek.");
                }
                else
                {
                    MessageBox.Show("You must enter a value in the FRP field.");
                }
                
                txtFRP.Focus();
           
            }
            else if (cboEnhet.Text == "")
            {
                if (lang == true)
                {
                    MessageBox.Show("Ange enhet.");
                }
                else
                {
                    MessageBox.Show("You must select a unit.");
                }
                
                cboEnhet.Focus();
            }

            else {


                barcode.BarcodeService ws = FormMain.getBarcodeService();
                XmlTextReader xtr = null;
                if (txtKonc.Text == "")
                {
                    txtKonc.Text = "0";

                }

                if (txtFRP.Text == "")
                {
                    txtFRP.Text = "0";

                }

                StringReader str = new StringReader(ws.InsertArticle(kemidb, databas, Convert.ToDouble(prod), Convert.ToDouble(lev), artikel, cboEnhet.Text, cboKval.Text,
                    Convert.ToDouble(txtFRP.Text), Convert.ToDouble(txtKonc.Text), Convert.ToDouble(cboEnhet.SelectedValue)));
                if (lang == true)
                {
                    MessageBox.Show("Artikelnummer tillagd!");
                }
                else
                {
                    MessageBox.Show("The articlenumber was succesfull added!");
                }
               
                this.Close();
            }
            

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSupplier_TextChanged(object sender, EventArgs e)
        {
   

        }

        private void txtArticle_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFRP_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboEnhet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void txtKonc_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboKval_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cboKval.DataSource = null;
            //cboKval.DisplayMember = "";
            //cboKval.ValueMember = "";
            //cboKval.Text = "";

            //barcode.BarcodeService bc = FormMain.getBarcodeService();

            //XmlTextReader xtr = null;
            //StringReader str = new StringReader(bc.GetQuality(kemi));
            //xtr = new XmlTextReader(str);
            //DataSet dts = new DataSet();
            //dts.ReadXml(xtr);
            //cboKval.DataSource = dts.Tables["Quality"]; // OBS! Här aktiveras metoden comboBoxSupplier_SelectedIndexChanged()
            //cboKval.DisplayMember = "txt";
            //cboKval.Text = "--- Choose ---";

            //dts.Dispose();
            //bc.Dispose();
        }



    }
}
