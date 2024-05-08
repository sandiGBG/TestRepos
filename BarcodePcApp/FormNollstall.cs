using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace BarcodePcApp
{
    
    public partial class FormNollstall : Form
    {
        public bool lang = false;
        public bool ShouldBeShown { get; set; }
        public string Db { get; set; }
        public string kemidb { get; set; }
        public int Anvid { get; set; }
        public int Ar { get; set; }
        public int Verkid { get; set; }
        public string organisationsid { get; set; }
        public int code;



        public FormNollstall(string databas, string kemiDb, int userId, int orgAr, int verkId)
        {
            InitializeComponent();
            ShouldBeShown = true;  // behövs en kontroll som sätter variabel false/true
            Db = databas;
            kemidb = kemiDb;
            Anvid = userId;
            Verkid = verkId;
            Ar = orgAr;
            //panelNoll.Visible = true;
            //panelNoll.Enabled = true;


        }

        private void FormNollstall_Load(object sender, EventArgs e)
        {
            lang = FormMain.Get.LangChange;
            if(lang==true)
            {
                this.lblNollstall.Text = "Återställ inventeringar för följande avdelning";
                this.btnollstall.Text = "Återställ";
                this.Name = "FormÅterställ";
                this.Text = "FormÅterställ";
                this.btnAvbryt.Text = "Avbryt";
                this.lblPanel.Text = "Laddar...";
            }
            else
            {
                this.lblNollstall.Text = "Restore the products for the following section";
                this.btnollstall.Text = "Restore";
                this.Name = "FormRestore";
                this.Text = "FormRestore";
                this.btnAvbryt.Text = "Close";
                this.lblPanel.Text = "Load...Please wait!";
            }

 
        }
        private void grpAvd_Enter(object sender, EventArgs e)
        {
            var location = new Point(24, 0);

            try
            {
                List<Control> radioButtons = new List<Control>();
                barcode.BarcodeService bc = FormMain.getBarcodeService();
                var avdelningar = bc.GetAvd(Db, kemidb, Anvid, Ar, Verkid);
                XmlDocument doc2 = new XmlDocument();
                doc2 = new XmlDocument();
                doc2.LoadXml(avdelningar);
                XmlNodeList xmlnodeList2 = doc2.DocumentElement.SelectNodes("item");
                int antal = Convert.ToInt32(xmlnodeList2[0]["antal"].InnerText);
                //var location = new Point(24, 0);
                int i = -1;
                int pointa = 24;
                int pointb = 0;
                foreach (XmlNode item in xmlnodeList2)
                {
                    i = i + 1;
                    if (i > antal)
                    {
                        break;
                    }
                    else
                    {
                        panelNoll.Visible = true;
                        panelNoll.Enabled = true;
                        string avd = Convert.ToString(xmlnodeList2[i]["namn"].InnerText);
                        string langd = avd.Length.ToString();
                        int o_id = Convert.ToInt32(xmlnodeList2[i]["orgid"].InnerText);
                        int kolumner = antal / 20;
                        if (antal > 24)
                        {
                            pointb = ((kolumner+1) * 150) + 100;
                            //grpAvd.Size = new Size(3200, 693);

                            if (i ==20)
                            {
                                pointa= pointa + 220;
                                //pointb = pointb + 120;
                                location = new Point(pointa, 0);
                            }
                            else if (i == 40)
                            {
                                pointa = pointa + 220;
                                //pointb = pointb + 120;
                                location = new Point(pointa, 0);
                            }
                            else if (i == 60)
                            {
                                pointa = pointa + 220;
                                //pointb = pointb + 120;
                                location = new Point(pointa, 0);
                            }
                            else if (i == 80)
                            {
                                pointa = pointa + 220;
                                //pointb = pointb + 120;
                                location = new Point(pointa, 0);
                            }
                            else if (i == 100)
                            {
                                pointa = pointa + 220;
                                //pointb = pointb + 120;
                                location = new Point(pointa, 0);
                            }
                            else if (i == 120)
                            {
                                pointa = pointa + 220;
                                //pointb = pointb + 120;
                                location = new Point(pointa, 0);
                            }
                            else if (i == 140)
                            {
                                pointa = pointa + 220;
                                //pointb = pointb + 120;
                                location = new Point(pointa, 0);
                            }
                            else if (i == 160)
                            {
                                pointa = pointa + 220;
                                //pointb = pointb + 120;
                                location = new Point(pointa, 0);
                            }
                            else if (i == 180)
                            {
                                pointa = pointa + 220;
                                //pointb = pointb + 120;
                                location = new Point(pointa, 0);
                            }
                            else if (i == 200)
                            {
                                pointa = pointa + 220;
                                //pointb = pointb + 120;
                                location = new Point(pointa, 0);
                            }
                            else if (i == 220)
                            {
                                pointa = pointa + 220;
                                //pointb = pointb + 120;
                                location = new Point(pointa, 0);
                            }
                            else if (i == 240)
                            {
                                pointa = pointa + 220;
                                //pointb = pointb + 120;
                                location = new Point(pointa, 0);
                            }
                            else if (i == 260)
                            {
                                pointa = pointa + 220;
                                //pointb = pointb + 120;
                                location = new Point(pointa, 0);
                            }

                        }
                        else
                        {
                            grpTitel.Size = new Size(500, 60);
                            this.ClientSize = new System.Drawing.Size(545, 429);
                            this.grpTitel.Size = new System.Drawing.Size(500, 60);
                            this.grpAvd.Size = new System.Drawing.Size(500, 300);
                            this.btnAvbryt.Location = new System.Drawing.Point(384, 259);
                            this.btnollstall.Location = new System.Drawing.Point(384, 295);

                            if (i == 8)
                            {
                                location = new Point(174, 0);
                            }
                            else if (i == 16)
                            {
                                location = new Point(324, 0);
                            }
                            else if (i == 24)
                            {
                                location = new Point(474, 0);
                            }

                        }
                        RadioButton radiobutton = new RadioButton();
                        radiobutton.Name = "avd_" + o_id;
                        //radiobutton.Text = avd + " (" + o_id + ")";
                        radiobutton.Text = avd;
                        radiobutton.Size = new Size(150, 30);
                        location.Y = location.Y + radiobutton.Height;
                        radiobutton.Location = location;
                        radioButtons.Add(radiobutton);
                        radiobutton.Click += new System.EventHandler(radiobutton_Click);
                        //grpAvd.Controls.Add(radiobutton);
                    }

                }

                foreach (var singleControl in radioButtons)
                {
                    grpAvd.Controls.Add(singleControl);
                }
                grpAvd.Size = new Size(pointb + 50, location.Y + 50);
                int a = location.X;
                int b = location.Y;
                this.btnAvbryt.Location = new Point(a, b + 35);
                this.btnollstall.Location = new Point(a, b + 70);
                this.ClientSize = new System.Drawing.Size(a, b);
                SetFoucsOnFirstInput();
                panelNoll.Enabled = false;
                panelNoll.Visible = false;
            }
            catch (Exception err)
            {

            }
        }

        public void SetFoucsOnFirstInput()
        {

            var firstInput = this.Controls.OfType<GroupBox>().FirstOrDefault();
            if (firstInput != null)
                firstInput.Focus();
        }


        private void btnollstall_Click(object sender, EventArgs e)
        {



                int orgid = Convert.ToInt32(organisationsid);
                string databas = Db;
                int v = Verkid;
                int o = Ar;

                barcode.BarcodeService ws = FormMain.getBarcodeService();
                string klar = ws.NollstallAvd(Db, kemidb, orgid, Verkid, Anvid);
                if (klar == "ok")
                {
                    if (lang == true)
                    {
                        MessageBox.Show("Produkterna återställda");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("The products were restored");
                        this.Close();
                    }

                }
                else
                {
                    if (lang == true)
                    {
                        MessageBox.Show("Det finns inga produkter att återställa");
                        //this.Close();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("There are no products to restore");
                        //this.Close();
                        return;
                    }
                }



            //DialogResult result;
            //MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            //if (lang==true)
            //{
            //    result = MessageBox.Show("Är du säker på att du vill återställa inventeringen?", "Återställ inventeringen", buttons);
            //}
            //else
            //{
            //    result = MessageBox.Show("Are you sure you want to restore products?", "Restore", buttons);
            //}


            //if (result == System.Windows.Forms.DialogResult.Yes)
            //{

            //    //MessageBox.Show("Nollställ");
            //    int orgid = Convert.ToInt32(organisationsid);
            //    string databas = Db;
            //    int v = Verkid;
            //    int o = Ar;
            //   //MessageBox.Show("Parametrar:" + orgid +" "+ databas + " " + v + " " + o);

            //    barcode.BarcodeService ws = FormMain.getBarcodeService();
            //    string klar = ws.NollstallAvd(Db, kemidb, orgid, Verkid,Anvid);
            //    if(klar=="ok")
            //    {
            //        if(lang == true)
            //        {
            //            MessageBox.Show("Produkterna återställda");
            //            this.Close();
            //        }
            //        else
            //        {
            //            MessageBox.Show("The products were restored");
            //            this.Close();
            //        }

            //    }
            //    else
            //    {
            //        if (lang == true)
            //        {
            //            MessageBox.Show("Det finns inga produkter att återställa");
            //            this.Close();
            //        }
            //        else
            //        {
            //            MessageBox.Show("There are no products to restore");
            //            this.Close();
            //        }

            //    }


            //}
            //else
            //{
            //    this.Close();
            //}

        }

        private void radiobutton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbt = (RadioButton)sender;
            if(rbt.Checked==true)
            {
                rbt.Checked = true;
            }
            else
            {
                rbt.Checked = false;
            }

        }


        private void radiobutton_Click(object sender, EventArgs e)
        {

            string datum = "";
            string anvnamn = "";
            string antal = "";
            string ant = "";

            RadioButton rbt = (RadioButton)sender;



            string aa = rbt.Name;
            string namn = aa.Split('_')[0];
            organisationsid = aa.Split('_')[1];
            barcode.BarcodeService ws = FormMain.getBarcodeService();
            string klar = ws.CheckLogg(Db, kemidb, organisationsid);
            XmlDocument doc2 = new XmlDocument();
            doc2 = new XmlDocument();
            doc2.LoadXml(klar);
            XmlNodeList xmlnodeList2 = doc2.DocumentElement.SelectNodes("item");
            foreach (XmlNode item in xmlnodeList2)
            {
                ant = Convert.ToString(item.Attributes["antal"].Value);
            }

            antal = ant;

            if (antal == "0")
            {

            }
            else
            {
                XmlDocument doc3 = new XmlDocument();
                doc3 = new XmlDocument();
                doc3.LoadXml(klar);
                XmlNodeList xmlnodeList3 = doc3.DocumentElement.SelectNodes("item");
                foreach (XmlNode item in xmlnodeList3)
                {
                    //ant = Convert.ToString(item.Attributes["antal"].Value);
                    datum = Convert.ToString(item.Attributes["datum"].Value);
                    anvnamn = Convert.ToString(item.Attributes["anvnamn"].Value);

                }

                string datum1 = datum;
                string anvnamn1 = anvnamn;
                DialogResult result1;

                if (lang == true)
                {
                    result1 = MessageBox.Show("Inventeringsstatusen på produkterna har redan återställts av [" + anvnamn1 + " ] den [" + datum1 + "]"+Environment.NewLine + Environment.NewLine +
                                    "Vill du göra det igen? Eventuella kasserade eller flyttade produkter påverkas inte. ",
                                    "Återställ produkter", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                }
                else
                {
                   result1 =  MessageBox.Show("The status of the products has already  been restored by [" + anvnamn1 + "] on [" + datum1 + "]" + Environment.NewLine + Environment.NewLine +
                                    "Do you want to do it again? Any discarded or moved chemicals will not be affected. ",
                                    "Restore", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }


                if (result1 == System.Windows.Forms.DialogResult.OK)
                {

                    //MessageBox.Show("Nollställ");
                    int orgid = Convert.ToInt32(organisationsid);
                    string databas = Db;
                    int v = Verkid;
                    int o = Ar;
                    //MessageBox.Show("Parametrar:" + orgid + " " + db + " " + v + " " + o);

                    barcode.BarcodeService ws1 = FormMain.getBarcodeService();
                    string klar1 = ws1.NollstallAvd(Db, kemidb, orgid, Verkid, Anvid);

                    if (lang == true)
                    {
                        MessageBox.Show("Produkterna återställda");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("The products were restored");
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }

            }
        }

        private void btnAvbtyt_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
