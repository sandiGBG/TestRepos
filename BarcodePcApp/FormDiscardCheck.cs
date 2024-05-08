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
using System.Media;
using System.IO;


namespace BarcodePcApp
{
    public partial class FormDiscardCheck: Form
    {
        
        //private string database;
        private int orgarCode;
        private string itemLocation;
        private string barcodeLocation;
        private string yourCurrentLocation;
        private string prodName;
        private string room;
        private int destinationRoom;
        private int destinationOrg;
        private int destinationCabinet;
        private int yourOrg;
        private int barcodeNodeID;
        private bool takeInventoryAfterMove;
        private bool lang;  //ver 1.27 översättning eng-sve

        private struct node
        {
            public int id;
            public int left;
            public string name;

            public node(int nodeID, int nodeLeft, string nodeName)
            {
                id = nodeID;
                left = nodeLeft;
                name = nodeName;
            }
        }

        public enum Property
        {
            Syra = 1,
            Bas = 2,
            Omärkt = 0
        };

        public FormDiscardCheck(XmlNode itemLocation, bool samedepartment, string databas, string streckkod, string kemiDb, int orgNod, int orgarID, string productName, int roomID,int barcodeNode)
        {
            
            //this.database = databas;
            this.orgarCode = orgarID;
            this.itemLocation = streckkod;
            this.prodName = productName;
            destinationRoom = roomID;
            yourOrg = orgNod;
            barcodeNodeID = barcodeNode;
            

            barcode.BarcodeService bs1 = FormMain.getBarcodeService();
            string Cabinet_Room = bs1.GetStorageNameOverview(databas, kemiDb, databas, streckkod);
            if(Cabinet_Room=="0")
            {

            }
            else
            {
                StringReader sr1 = new StringReader(Cabinet_Room);
                XmlTextReader xtr2 = null;
                xtr2 = new XmlTextReader(sr1);

                DataSet ds1 = new DataSet();
                ds1.ReadXml(xtr2);
                DataTable dt1 = ds1.Tables[0];
                DataRow dr1 = dt1.Rows[0];

                string Rum = dr1["StorageName"].ToString();
                room = Rum;
                string Skap = dr1["Skap"].ToString();
                string Lada = dr1["Lada"].ToString();

                barcode.BarcodeService bs2 = FormMain.getBarcodeService();
                string a = bs2.GetOrgnamn(databas, kemiDb, yourOrg);
                StringReader sr2 = new StringReader(a);
                XmlTextReader xtr3 = null;
                xtr3 = new XmlTextReader(sr2);


                DataSet ds2 = new DataSet();
                ds2.ReadXml(xtr3);

                DataTable dt2 = ds2.Tables[0];
                DataRow dr2 = dt2.Rows[0];
                string yourOrganisation = dr2["OrgnodsNamn"].ToString();
                yourCurrentLocation = yourOrganisation;


                string b = bs2.GetOrgnamn(databas, kemiDb, barcodeNodeID);
                StringReader sr3 = new StringReader(b);
                XmlTextReader xtr4 = null;
                xtr4 = new XmlTextReader(sr3);


                DataSet ds3 = new DataSet();
                ds3.ReadXml(xtr4);

                DataTable dt3 = ds3.Tables[0];
                DataRow dr3 = dt3.Rows[0];
                string barcodeOrg = dr3["OrgnodsNamn"].ToString();
                barcodeLocation = barcodeOrg;

            }
            //StringReader sr1 = new StringReader(Cabinet_Room);
            //XmlTextReader xtr2 = null;
            //xtr2 = new XmlTextReader(sr1);

            //DataSet ds1 = new DataSet();
            //ds1.ReadXml(xtr2);
            //DataTable dt1 = ds1.Tables[0];
            //DataRow dr1 = dt1.Rows[0];

            //string Rum = dr1["StorageName"].ToString();
            //room = Rum;
            //string Skap = dr1["Skap"].ToString();
            //string Lada = dr1["Lada"].ToString();

            //barcode.BarcodeService bs2 = FormMain.getBarcodeService();
            //string a = bs2.GetOrgnamn(databas, kemiDb, yourOrg);
            //StringReader sr2 = new StringReader(a);
            //XmlTextReader xtr3 = null;
            //xtr3 = new XmlTextReader(sr2);


            //DataSet ds2 = new DataSet();
            //ds2.ReadXml(xtr3);

            //DataTable dt2 = ds2.Tables[0];
            //DataRow dr2 = dt2.Rows[0];
            //string yourOrganisation = dr2["OrgnodsNamn"].ToString();
            //yourCurrentLocation = yourOrganisation;


            //string b = bs2.GetOrgnamn(databas, kemiDb, barcodeNodeID);
            //StringReader sr3 = new StringReader(b);
            //XmlTextReader xtr4 = null;
            //xtr4= new XmlTextReader(sr3);


            //DataSet ds3 = new DataSet();
            //ds3.ReadXml(xtr4);

            //DataTable dt3 = ds3.Tables[0];
            //DataRow dr3 = dt3.Rows[0];
            //string barcodeOrg = dr3["OrgnodsNamn"].ToString();
            //barcodeLocation = barcodeOrg;


            InitializeComponent();

            lang = FormMain.Get.LangChange;

            if (lang == false)
            {
                this.questionLabel.Text = "It appears that the article you scanned belongs somewere else. Would you like to " +
"discard it anyway?";
                this.label2.Text = "Current location in database:";
                this.label3.Text = "Your current location:";
                this.DiscardButton.Text = "Discard";
                this.CancelButton.Text = "Cancel";
                this.userLocationLabel.Text = "Current inventory location";
                this.itemLocationLabel.Text = "Location of product";
                this.Text = "Discard a product";
                this.pictureBox1.Image = global::BarcodePcApp.Properties.Resources.discard;
            }
            else
            {
                this.questionLabel.Text = "Produkten tillhör någon annan. Vill du " +
"kassera den ändå?";
                this.label2.Text = "Nuvarande plats i databasen:";
                this.label3.Text = "Din nuvarande plats:";
                this.DiscardButton.Text = "Kassera";
                this.CancelButton.Text = "Avbryt";
                this.userLocationLabel.Text = "Nuvarande inventeringsplats";
                this.itemLocationLabel.Text = "Produktens placering";
                this.Text = "Kassera produkten";
                this.pictureBox1.Image = global::BarcodePcApp.Properties.Resources.kassera;
            }

            UpdateMessageLabel(samedepartment);
            UpdateLabel(itemLocationLabel, barcodeLocation);
            UpdateLabel(userLocationLabel, yourCurrentLocation);
            
        }

        private void UpdateMessageLabel(bool sameDepartment)
        {
            if (sameDepartment)
            {
                System.Media.SystemSounds.Exclamation.Play();
                if (lang==true)
                {

                }
                else
                {
                    questionLabel.Text = "This article (" + prodName + ") appears to belong somewhere else in your department. Would you like to discard it anyway?";
                }
                
            }
            else
            {
                System.Media.SystemSounds.Hand.Play();
                if (lang == true)
                {

                }
                else
                {
                    questionLabel.Text = "This article (" + prodName + ") belongs to somewhere else in your department. Would you like to discard it anyway?";
                }
                
            }
        }

       

        private void UpdateLabel(Label destination, string content)
        {
            string text = "";
            string spacing = "";

                text += spacing + content + "\n";
                spacing += " ";


            destination.Text = text;
        }
        

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            this.Close();
        }

        private void DiscardButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

        }



        
    }
}