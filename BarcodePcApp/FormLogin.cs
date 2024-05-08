using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.IO;
using System.Timers;
using System.Web;
using System.Net;

namespace BarcodePcApp
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public partial class FormLogin : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private int m_nVerkID;
        private string databas;
        private int m_nUserID = 0;
        private string userName = "";
        private string m_sFullname = "";
        private int m_nOrgNod = 0;
        private int m_nOrgAr = 0;
        private string m_sKundNamn;
        private int m_nPcAppVer = 0;
        public string barcodeinsettings = "";
        public int sprak = 0;
        private bool lang;


        public FormLogin(bool loginWithBarcodeEnabled, bool loginWithBarcodeAsDefault)
        {
            InitializeComponent();


            if (loginWithBarcodeEnabled)
            {
                LoginBarcodeCheckBox.Visible = true;

                this.KeyPress += new KeyPressEventHandler(form_KeyPress);
                scannerTimer.Interval = 100;
                scannerTimer.Elapsed += scannerTimer_Elapsed;

            }
            if (loginWithBarcodeAsDefault)
            {
                LoginBarcodeCheckBox.Checked = true;
                loginBarcodePanel.Visible = true;
                textUser.Enabled = false;
                textPaswd.Enabled = false;
                buttonLogin.Enabled = false;

                
            }

            //if (VerkId == 2)
            //{
            //    chkTestserver.Visible = true;
            //    chkTestserver.Enabled = true;

            //}

            //else
            //{
            //    chkTestserver.Visible = false;
            //    chkTestserver.Enabled = false;
            //    this.ClientSize = new System.Drawing.Size(354, 246);

            //}
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.buttonLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textUser = new System.Windows.Forms.TextBox();
            this.textPaswd = new System.Windows.Forms.TextBox();
            this.checkBoxLocalhost = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LoginBarcodeCheckBox = new System.Windows.Forms.CheckBox();
            this.loginBarcodePanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.loginBarcodePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonLogin
            // 
            this.buttonLogin.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonLogin.Location = new System.Drawing.Point(148, 150);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(115, 27);
            this.buttonLogin.TabIndex = 3;
            this.buttonLogin.Text = "Log on";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(22, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "User name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(25, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textUser
            // 
            this.textUser.Location = new System.Drawing.Point(148, 90);
            this.textUser.Name = "textUser";
            this.textUser.Size = new System.Drawing.Size(206, 22);
            this.textUser.TabIndex = 1;
            // 
            // textPaswd
            // 
            this.textPaswd.Location = new System.Drawing.Point(148, 120);
            this.textPaswd.Name = "textPaswd";
            this.textPaswd.PasswordChar = '*';
            this.textPaswd.Size = new System.Drawing.Size(206, 22);
            this.textPaswd.TabIndex = 2;
            // 
            // checkBoxLocalhost
            // 
            this.checkBoxLocalhost.AutoSize = true;
            this.checkBoxLocalhost.Location = new System.Drawing.Point(85, 226);
            this.checkBoxLocalhost.Name = "checkBoxLocalhost";
            this.checkBoxLocalhost.Size = new System.Drawing.Size(281, 21);
            this.checkBoxLocalhost.TabIndex = 7;
            this.checkBoxLocalhost.Text = "Kör mot localhost istället för webservern";
            this.checkBoxLocalhost.UseVisualStyleBackColor = true;
            this.checkBoxLocalhost.Visible = false;
            this.checkBoxLocalhost.CheckedChanged += new System.EventHandler(this.checkBoxLocalhost_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(396, 45);
            this.label3.TabIndex = 40;
            this.label3.Text = "Welcome to KLARA";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoginBarcodeCheckBox
            // 
            this.LoginBarcodeCheckBox.AutoSize = true;
            this.LoginBarcodeCheckBox.Location = new System.Drawing.Point(85, 200);
            this.LoginBarcodeCheckBox.Name = "LoginBarcodeCheckBox";
            this.LoginBarcodeCheckBox.Size = new System.Drawing.Size(227, 21);
            this.LoginBarcodeCheckBox.TabIndex = 41;
            this.LoginBarcodeCheckBox.Text = "Log on using personal barcode";
            this.LoginBarcodeCheckBox.UseVisualStyleBackColor = true;
            this.LoginBarcodeCheckBox.Visible = false;
            this.LoginBarcodeCheckBox.CheckedChanged += new System.EventHandler(this.LoginBarcodeCheckBox1_CheckedChanged);
            // 
            // loginBarcodePanel
            // 
            this.loginBarcodePanel.Controls.Add(this.label4);
            this.loginBarcodePanel.Location = new System.Drawing.Point(29, 63);
            this.loginBarcodePanel.Name = "loginBarcodePanel";
            this.loginBarcodePanel.Size = new System.Drawing.Size(362, 130);
            this.loginBarcodePanel.TabIndex = 42;
            this.loginBarcodePanel.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(52, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(218, 22);
            this.label4.TabIndex = 0;
            this.label4.Text = "Please scan your barcode";
            // 
            // FormLogin
            // 
            this.AcceptButton = this.buttonLogin;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(413, 227);
            this.Controls.Add(this.loginBarcodePanel);
            this.Controls.Add(this.LoginBarcodeCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxLocalhost);
            this.Controls.Add(this.textPaswd);
            this.Controls.Add(this.textUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log on to KLARA";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.loginBarcodePanel.ResumeLayout(false);
            this.loginBarcodePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        public int UserId
        {
            get { return m_nUserID; }
            set { m_nUserID = value; }	// Behövs egentligen inte. Men annars returneras gammalt värde vid fönsterstängning.
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public int OrgNod
        {
            get { return m_nOrgNod; }
            set { m_nOrgNod = value; }
        }

        public int OrgAr
        {
            get { return m_nOrgAr; }
            set { m_nOrgAr = value; }
        }

        public string KundNamn
        {
            get { return m_sKundNamn; }
            set { m_sKundNamn = value; }
        }

        public int PcAppVer
        {
            get { return m_nPcAppVer; }
            set { m_nPcAppVer = value; }
        }

        public string Fullname
        {
            get { return m_sFullname; }
        }

        public int VerkId
        {
            get { return m_nVerkID; }
            set { m_nVerkID = value; }
        }

        public string Databas
        {
            set { databas = value; }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            this.Text = "Log on to KLARA ( v" + Convert.ToString(Math.Round(((PcAppVer / 100.0)), 2)).Replace(",", ".") + " - " + databas.ToLower() + ")";
            lang = FormMain.Get.LangChange;
            if (lang == true)
            {
                this.Text = "Logga in på KLARA ( v" + Convert.ToString(Math.Round(((PcAppVer / 100.0)), 2)).Replace(",", ".") + " - " + databas.ToLower() + ")";
                this.label3.Text = "Välkommen till KLARA";
                this.label4.Text = "Skanna din streckkod";
                this.LoginBarcodeCheckBox.Text = "Logga in med din streckkod";
                this.buttonLogin.Text = "Logga in";
                this.label1.Text = "Användarnamn:";
                this.label2.Text = "Lösenord:";
            }


#if(DEBUG)
            //-----Avmarkeras när man kör lokalt eller testserver-------//
            //checkBoxLocalhost.Visible = true;
            //checkBoxLocalhost.Checked = true;

            //______________________________________________________//


            //Sandi
            //chkTestserver.Visible = false;
            //chkTestserver.Checked = false;
#endif
            // this.Text = "Log on to " + m_sKundNamn;
            // m_labelRubrik.Text = m_sKundNamn;
        }




        private void buttonLogin_Click(object sender, System.EventArgs e)
        {
            Login(textUser.Text, textPaswd.Text,"");
        }


        private void Login(string username, string password,string streckkod)
        {
            //if (streckkod == "")
            //{ 
            
            //}
            //else
            //{ 
            //    if (username == "") return; 
            //}
            if (username == "") return;
            




#if (DEBUG)
            if (username.ToLower() == "inifil" && password.ToLower() == "inifil")
            {
                FormIniFil fi = new FormIniFil();
                fi.ShowDialog(this);
                return;
            }
#else
            if (password == "") return;
#endif
            //new location
            FormMain.localhost = checkBoxLocalhost.Checked;


            userName = username;

            string sLogin = "";
            barcode.BarcodeService bc = FormMain.getBarcodeService();
            try
            {

                if (streckkod == "") 
                {
                    sLogin = bc.BarcodeLogin(databas, username, password, streckkod);
                }
                else
                {
                    sLogin = bc.BarcodeLogin(databas, username, password, streckkod);
                }
                //sLogin = bc.BarcodeLogin(databas, username, password,streckkod);

            }
            catch(Exception err){

                DialogResult = DialogResult.Cancel;
                //MessageBox.Show("The following error message was reported:\r\n\n" + err.Message, "BarcodeLogin");  //1.27
                MessageBox.Show("No contact with the web server!\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message,"BarcodeLogin");
                bc.Dispose();
                return;
            }
            /*DataSet ds1 = new DataSet();
            
            ds1.ReadXml(new StringReader(sLogin));
            string s = ds1.Tables["GetUser"].Rows[0]["err"].ToString();*/
            // http://stackoverflow.com/questions/3793/best-way-to-get-innerxml-of-an-xelement

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sLogin);
            if( !Convert.ToBoolean(doc.DocumentElement.GetAttribute("err")) )
            {
                //XmlNodeList xmlnodeList = doc.DocumentElement.SelectNodes("user");
                //foreach (XmlNode product in xmlnodeList)
                XmlNode node = doc.DocumentElement.SelectSingleNode("user");
                if (node != null)
                {
                    m_nUserID = Convert.ToInt32(node.Attributes["id"].Value);
                    m_nVerkID = Convert.ToInt32(node.Attributes["verkid"].Value);
                    m_sFullname = node.Attributes["nam"].Value;
                }
            }
            else
            {
                if(lang==true)
                {
                    MessageBox.Show("Det angivna användarnamnet eller lösenordet var inte korrekt!\n\n(" + textUser.Text + ")");
                }
                else
                {
                    MessageBox.Show("The entered username or password was not correct!\n\n(" + textUser.Text + ")");
                }
                
                bc.Dispose();
                return;
            }

            try
            {

                string orgar_q = bc.GetOrgar(databas, m_nVerkID);
                XmlDocument doc1 = new XmlDocument();
                doc1 = new XmlDocument();
                doc1.LoadXml(orgar_q);
                XmlNodeList xmlnodeList1 = doc1.DocumentElement.SelectNodes("item");
                foreach (XmlNode item in xmlnodeList1)
                {
                    m_nOrgAr = Convert.ToInt32(item.Attributes["ar"].Value);
                }



                //m_nOrgAr = Convert.ToInt32(bc.GetOrgar(databas, m_nVerkID));
                
                string orgarID_w = (bc.GetOrgarID(databas, m_nVerkID));
                doc1.LoadXml(orgarID_w);
                XmlNodeList xmlnodeList2= doc1.DocumentElement.SelectNodes("item");
                int orgarID = 0;
                foreach (XmlNode item in xmlnodeList2)
                {
                    orgarID = Convert.ToInt32(item.Attributes["orgarid"].Value);
                }



                //int orgarID = Convert.ToInt32(bc.GetOrgarID(databas, m_nVerkID));
               //m_nOrgNod = Convert.ToInt32(bc.GetUserOrgnod(databas, m_nOrgAr, m_nUserID));
                string sOrgnod = bc.GetUserOrgnod(databas, m_nOrgAr, m_nUserID,3);               

                doc = new XmlDocument();
                doc.LoadXml(sOrgnod);

                m_nOrgNod = 0;
                XmlNodeList xmlnodeList = doc.DocumentElement.SelectNodes("item");

                // Ändring 1.27 Nollställ_behörighet
                foreach (XmlNode item in xmlnodeList)
                {
                    m_nOrgNod = Convert.ToInt32(item.Attributes["orgnod"].Value);

                    if (item.Attributes["namnkod"].Value.ToString() == "KLARA_INKOP")
                    {
                        FormMain.IsInkopare = true;
                    }
                    else if (item.Attributes["namnkod"].Value.ToString() == "KLARA_BEST")
                    {
                        FormMain.IsBestallare = true;
                    }
                    else if (item.Attributes["namnkod"].Value.ToString() == "KLARA_ADMIN")
                    {
                        bool ok = true;
                        FormMain.IsAdmin = ok;
                    }
                }

                if (m_nOrgNod == 0)
                {
                    if (lang == true)
                    {
                        MessageBox.Show("Den här användaren har inte Inköpare profilen. Kontakta administartör.");
                    }
                    else
                    {
                        MessageBox.Show("This user is not connected as a buyer. Please contact the Administrator.");
                    }
                    
                    userName = "";
                    m_nUserID = 0;
                    m_nVerkID = 0;
                    m_sFullname = "";
                    m_nOrgAr = 0;
                    return;
                }
            }
            catch (Exception err)
            {
                DialogResult = DialogResult.Cancel;
                MessageBox.Show("No contact with the web server!\r\n Please contact the administrator in order to change the start page of your webb KLARA profile");
                bc.Dispose();
                return;
            }
            //old location
            //FormMain.localhost = checkBoxLocalhost.Checked;

            DialogResult = DialogResult.OK;
            bc.Dispose();
            this.Close();
        }

        private void LoginBarcodeCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            //LoginBarcodeCheckBox.Visible = LoginBarcodeCheckBox.Checked;
            LoginBarcodeCheckBox.Checked = LoginBarcodeCheckBox.Checked;
            loginBarcodePanel.Visible = LoginBarcodeCheckBox.Checked;
            textUser.Enabled = !LoginBarcodeCheckBox.Checked;
            textPaswd.Enabled = !LoginBarcodeCheckBox.Checked;
            buttonLogin.Enabled = !LoginBarcodeCheckBox.Checked;
        }

        private System.Timers.Timer scannerTimer = new System.Timers.Timer();
        private string barcode = "";

        private void form_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 65 && e.KeyChar <= 122)) && LoginBarcodeCheckBox.Checked)
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
            loginUsingBarcode(barcode);

            barcode = "";

        }


        private void loginUsingBarcode(string loginBarcode)

        {
            string username = "";
            string password = "";
            string userDataXml = "";
            string ver = Environment.Version.ToString();

            barcode.BarcodeService bc = FormMain.getBarcodeService();
            try
            {
                userDataXml = bc.GetUserNameAndPasswordWithBarcode(databas, loginBarcode);

                if (userDataXml != "")
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(userDataXml);



                    XmlNodeList xmlnodeList = doc.DocumentElement.ChildNodes;

                    int test = xmlnodeList.Count;

                    foreach (XmlNode item in xmlnodeList)
                    {
                        if (item.Name == "username")
                        {
                            username = Convert.ToString(item.InnerText).Trim();
                        }

                        if (item.Name == "password")
                        {
                            password = Convert.ToString(item.InnerText).Trim();
                        }
                    }

                    if (loginBarcode == "")
                    {
                        this.Invoke(new MethodInvoker(delegate { Login(username, password, ""); }));
                    }
                    else
                    {
                        this.Invoke(new MethodInvoker(delegate { Login(username, password, loginBarcode); }));
                    }


                }
                else
                {
                    if (lang == true)
                    {
                        MessageBox.Show("Streckkoden är inte kopplad till någon användare. Använd ditt användarnamn och ditt lösenord.", "BarcodePCapp");
                    }
                    else
                    {
                        MessageBox.Show("This barcode is not connected to any user. Please use your username and password instead.", "BarcodePCapp");
                    }
                    
                }
            }
            catch (Exception err)
            {
                if (lang == true)
                {
                    MessageBox.Show("ingen kontakt med server" + err.Message, "BarcodeLogin");
                }
                else
                {
                    MessageBox.Show("No contact with the web server!\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "BarcodeLogin");
                }
                
                return;
            }
            bc.Dispose();
            return;
        }

        private void checkBoxLocalhost_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxLocalhost.Checked)
            {
                checkBoxLocalhost.Checked=true;
            }
            else
            {
                checkBoxLocalhost.Checked=false;

            }

        }
    }
}
