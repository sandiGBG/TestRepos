using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Data;
using System.Runtime.InteropServices;
using BarcodePcApp.Misc;
using System.Threading;
using Neodynamic.WinControls.BarcodeProfessional;
using System.Timers;
using System.Net;
using Microsoft.Win32;


namespace BarcodePcApp
{
	/// <summary>
	/// Barcode streckkodsläsare. Huvudformulär.
	/// </summary>
    public class FormMain : System.Windows.Forms.Form
    {
        // Version av detta program. Används för att avgöra om automatisk uppdatering av programmet ska ske.
        //
        // Vid uppdatering gör följande:
        // 1. Ändra namnet på den nuvarande barcode.cfc till versionsnummer.cfc (ex. 113.cfc)
        // 2. Ändra version i barcode.cfc (webbservicen) och thisPcAppVer och ladda upp barcode.cfc till servern
        // 3. Ändra även versionen i filen AssemblyInfo.cs
        // 4. Ändra versionen i Setup programmet. Vid ändring av versionen i setup programet måste ProductCode också uppdateras. Annars går inte installationen att köra utan att avinstallera gamla versionen manuellt först.
        // 
        // OBS! I setup programmet måste följande flaggor vara DetectNewerInstalledVersion=true och RemovePreviousVersions=true

        public static int thisPcAppVer = 127;	// Version av detta program

        public static string pcTmpPath = "";        // Tempkatalog på pc:n - sätts till användarens tempkatalog

        //RAPI rapi;	// global RAPI object

        public static bool localhost = false;       // kör mot lokal server, inte webservern.
        //public static bool testserver = false;       // kör mot testserver, inte webservern.
        private static string databas = "";
        public static string kemiDb = "";	        // kemikaliedatabasen - "gemkem" eller motsv.
        private static string kundNamn = "";
        private static string verkNamn = "";
        private static bool isInkopare = false;
        private static bool isBestallare = false;
        private static int verkId = 0;
        private static int userId = 0;
        private static string userName = "";
        private static string userStringName = "";  // För- och efternamn
        private static string userStringDepart = "";// Forskargrupp
        private static string userStringSys = "0";   // Om systemadministratör = 1
        private static int orgNod;		            // Vald orgnod att arbeta med
        private static int orgAr;		            // Vald år att arbeta med
        public static bool forcedUpdate = true;
        public static int pcAppVer = 0;	            // Version av PCprogrammet tillgängligt på servern
        public static string iniFilePath = "";
        private System.Timers.Timer scannerTim = new System.Timers.Timer();
        public string barcodeInSettings = "";
        private static string lokaldb = "";  // Ver 1.18
        public string m_sRelease= "";
        //public bool lang;                 //ver 1.27
        public static bool lang=false;                 //ver 1.27


        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button buttonLogout;
        private Label labelKund;
        private Label label1;
        private Button buttonCheckIn;
        private Button buttonCheckOut;
        private Button buttonOverview;
        private Label labelUserInfo;
        private Label labelVerksamhet;

        private string m_sAppDataPath = "";

        private int m_nBarcodeWidth = 40;
        private int m_nBarcodeHeight = 21;
        //private string m_nBarcodeWidth = "40";
        //private string m_nBarcodeHeight = "21";
        private int m_nBarcodeLeft = 0;
        private int m_nBarcodeTop = 0;
        private int m_nBarcodeLayout = 0;
        private string m_sBarcodePrefix = "";
        private string m_sBarcodePrinter = "Okänd";
        private bool showDialogOnSameDepartment = false;
        private bool enableLoginByBarcode = false;
        private bool loginByBarcodeSelected = false;
        private bool changeRoomByBarcode = false;
        
        //Sandi
        private bool showPropertyCheck = false;
        //nytt 20161121
        private bool showPropertyChange = false;

        static private FormMain m_Main = null;

        // P/Invoke constants
        private const int WM_SYSCOMMAND = 0x112;
        private const int MF_STRING = 0x0;
        private Button m_btnOrder;
        private Button btnMove;
        private ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private Label label2;
        private Panel panel1;
        private TextBox txtBarcode;
        private CheckBox chkUser;
        private HelpProvider helpProvider1;
        private Button btnRelease;
        private Button btnInventory;
        private Button button2;
        private Button btnPrintbarcodes;
        private Button btnPrintLocBarcode;
        private Button btnReset;
        private const int MF_SEPARATOR = 0x800;

        // P/Invoke declarations
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetSubMenu(IntPtr hMenu, int uPosition);


        // ID for the About item on the system menu
        private int SYSMENU_SETTINGS_ID = 0x1;

        //private int SYSMENU_HELP_ID = 0x2;
        //private int SYSMENU_SUB_HELP_ID = 0x3;
        private int SYSMENU_SUB1_HELP_ID = 0x4;
        private int SYSMENU_SUB2_HELP_ID = 0x5;
        private int SYSMENU_SUB3_HELP_ID = 0x6;
        private int SYSMENU_SUB4_HELP_ID = 0x7;
        

        private System.Timers.Timer scannerTimer = new System.Timers.Timer();
        private string barcode = "";

        public FormMain()
        {
            m_Main = this;

            InitializeComponent();
            //Progressbar
            progressBar1.Visible = false;
            label2.Visible = false;
            button2.Visible = false;
            //this.KeyPress += new KeyPressEventHandler(form_KeyPress1);

            //scannerTim.Interval = 0;
            //scannerTim.Elapsed += scannerTim_Elapsed;

            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////
            ///
            ////// Info om operativsystem --->>> till version 1.27
            ///
            /// //////////////////////////////////////////////////////////////////
            ////string HKLMWinNTCurrent = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion";
            ////string osType = Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit";
            ////string osVers = Environment.OSVersion.Version.ToString();
            ////string osName = Registry.GetValue(HKLMWinNTCurrent, "productName", "").ToString();
            ////string osRelease = Registry.GetValue(HKLMWinNTCurrent, "ReleaseId", "").ToString();
            ////var os = Environment.OSVersion;
            ////MessageBox.Show("Current OS Information:\n");
            ////MessageBox.Show("OS Name: " + osName);
            ////MessageBox.Show("OS Release: " + osRelease);
            ////MessageBox.Show("OS Type: " + osType );
            ////MessageBox.Show("OS Version: " + osVers);

            ////barcode.BarcodeService bc = getBarcodeService();
            ////string OSinfoString = "";
            ////// skicka till ws där parametrar sparas i DB
            ////OSinfoString = bc.GetOSInfo(databas, verkId, osName, osType, osRelease, osVers);


            lang = FormMain.Get.LangChange;

            if (lang == true)
            {
                m_Main.label1.Text = "Välkommen till KLARA13!";
                m_Main.buttonLogout.Text = "Logga ut";
                m_Main.buttonCheckIn.Text = "Registrera produkter13";
                this.buttonCheckOut.Text = "Kasera produkter";
                this.buttonOverview.Text = "Översikt";
                this.m_btnOrder.Text = "Beställning";
                this.btnMove.Text = "Flytta produkter";
                this.btnInventory.Text = "Inventera produkter";
                this.btnPrintbarcodes.Text = "Prodstreckkoder";
                this.btnPrintLocBarcode.Text = "Utrstreckkoder";
                this.btnReset.Text = "Nollställ produkter";
                //this.chkUser.Text = "Skanna användare och stäng";
                this.btnRelease.Text = "Frisläpp produkter";
                this.Text = "KLARA registrering";


            }
            else
            {
                m_Main.label1.Text = "Welcome to KLARA13!";
                m_Main.buttonLogout.Text = "Log out";
                m_Main.buttonCheckIn.Text = "Register a product13";
                this.buttonCheckOut.Text = "Discard a product";
                this.buttonOverview.Text = "Product overview";
                this.m_btnOrder.Text = "Orders";
                this.btnMove.Text = "Move products";
                this.btnInventory.Text = "Inventory products";
                this.btnPrintbarcodes.Text = "Print Barcodes";
                this.btnPrintLocBarcode.Text = "Location barcode";
                this.btnReset.Text = "Reset products";
                //this.chkUser.Text = "Scan user and close";
                this.btnRelease.Text = "Release of products";
                this.Text = "KLARA registration";

            }
            //label1.Text = "Welcome to KLARA!";

            //FormSettings form = new FormSettings();
            //barcodeInSettings = form.loginBarcode;   //Fel ingen barcodeInSettings visas

            //this.ActiveControl = txtBarcode;
            //txtBarcode.Focus();

            BarcodeProfessional.LicenseOwner = "Nordic Port AB-Standard Edition-Developer License";
            BarcodeProfessional.LicenseKey = "R5CT6WMW6FCDRDNG5HXCNPA9LRC92L5JD7YKCYZLFV62T6JMZG9Q";

            this.Text = "KLARA registration v" + Convert.ToString(Math.Round(((thisPcAppVer / 100.0)), 2)).Replace(",", ".");
            //this.Text = "KLARA registration v 1.20";

            m_sAppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Nordic_Port_AB");
            iniFilePath = Path.Combine(m_sAppDataPath, "BarcodePcApp.ini");

            this.Visible = false;
            this.KeyPress += new KeyPressEventHandler(form_KeyPress);
            scannerTimer.Interval = 1000;
            scannerTimer.Elapsed += scannerTimer_Elapsed;   // OBS!!!!!  Inaktiv tills vi löst problemet med ruminloggning från main

        }


        // OBS!!!! private void scannerTimer_Elapsed är Inaktiv tills vi löst problemet med ruminloggning från main

        //private bool inventoryLoading = false;
        //private void scannerTimer_Elapsed(object source, ElapsedEventArgs e)
        //{
        //    scannerTimer.Enabled = false;
        //    scannerTimer.Stop(); 

        ////    MessageBox.Show("Main: scannerTimer_Elapsed: INNAN if " + barcode);  // flaggor för test 
        ////    if (barcode.StartsWith("r") && barcode.Length > 1 && changeRoomByBarcode)

        ////        MessageBox.Show("Main: scannerTimer_Elapsed: INNE I IF " + barcode);  // flaggor för test 
        ////    {
        ////        if (!inventoryLoading)
        ////        {

        ////            MessageBox.Show("Main:  if (!inventoryLoading)");  // flaggor för test 
        ////            inventoryLoading = true;
        ////            FormInventory inventory = new FormInventory(verkId, databas, kemiDb, barcode);

        ////            if (inventory.shouldBeShown)
        ////            {

        ////                MessageBox.Show("Main:   if (inventory.shouldBeShown)");  // flaggor för test 
        ////                this.Invoke(new MethodInvoker(delegate
        ////                {
        ////                    inventory.ShowDialog(this);
        ////                    //inventory.ShowDialog();
        ////                }));

        ////            }
        ////            else
        ////            {
        ////                MessageBox.Show("You do not have inventory access to any deparments. If you need to access to the inventory feature, please contact your administrator.", "Access Denied");
        ////            }

        ////            inventory.Dispose();
        ////            inventoryLoading = false;
        ////        }
        ////        //}
        ////        else if (barcodeInSettings == barcode)
        ////        {
        ////            this.Invoke(new MethodInvoker(delegate { FormMain.Get.Logout(); })); 
        ////            //this.Close();
        ////            //this.Owner.Close();
        ////        }



        ////        barcode = "";
        //       MessageBox.Show("You need to choose a function");  // flaggor för test 

        ////    }
        //}

       
        static public FormMain Get{ get { return m_Main; } }

        public bool ShowDialogOnSameDepartment { get { return showDialogOnSameDepartment; } set { showDialogOnSameDepartment = value; } }
        //Sandi
        public bool PropertyCheck { get { return showPropertyCheck; } set { showPropertyCheck = value; } }

        public bool LangChange { get { return lang; } set { lang = value; } }
        //public bool LangChange { get { return FormMain.lang; } set { FormMain.lang = value; } }
        //20161121
        public bool PropertyChange { get { return showPropertyChange; } set { showPropertyChange = value; } }
        //
        public int OrgNod { get { return orgNod; } }
        public int OrgAr { get { return orgAr; } }
        public int VerkId { get { return verkId; } }
        public int UserId { get { return userId; } }
        public int BarcodeWidth { get { return m_nBarcodeWidth; } set { m_nBarcodeWidth = value; } }
        public int BarcodeHeight { get { return m_nBarcodeHeight; } set { m_nBarcodeHeight = value; } }
        //public string BarcodeWidth { get { return m_nBarcodeWidth; } set { m_nBarcodeWidth = value; } }
        //public string BarcodeHeight { get { return m_nBarcodeHeight; } set { m_nBarcodeHeight = value; } }
        public int BarcodeLeft { get { return m_nBarcodeLeft; } set { m_nBarcodeLeft = value; } }
        public int BarcodeTop { get { return m_nBarcodeTop; } set { m_nBarcodeTop = value; } }
        public int BarcodeLayout { get { return m_nBarcodeLayout; } set { m_nBarcodeLayout = value; } }
        public string BarcodePrefix { get { return m_sBarcodePrefix; }}
        public string BarcodePrinter { get { return m_sBarcodePrinter; } set { m_sBarcodePrinter = value; } }
        public string Username { get { return userName; } }
        public string Usernamestring { get { return userStringName; } } //Sandi
        public string Databas { get { return databas; } }
        public string KemiDB { get { return kemiDb; } }
        public string AppDataPath { get { return m_sAppDataPath; } }
        public bool ScanRoomsEnabled { get { return changeRoomByBarcode; } }
        //Sandi
        public bool LoginBarcode { get { return loginByBarcodeSelected; } }
        //
        public static bool IsInkopare
        {
            get { return FormMain.isInkopare; }
            set { FormMain.isInkopare = value; }
        }
        public static bool IsBestallare
        {
            get { return FormMain.isBestallare; }
            set { FormMain.isBestallare = value; }
        }

        public static string LokalDB 
        {
            get { return FormMain.lokaldb; }
            set { FormMain.lokaldb = value; }
        }


        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Get a handle to a copy of this form's system (window) menu
            IntPtr hSysMenu = GetSystemMenu(this.Handle, false);

            // Add a separator
            AppendMenu(hSysMenu, MF_SEPARATOR, 0, string.Empty);

            // Add the About menu item
            AppendMenu(hSysMenu, MF_STRING, SYSMENU_SETTINGS_ID, "&Settings");

            // Add the Help menu item
            AppendMenu(hSysMenu, MF_STRING, SYSMENU_SUB1_HELP_ID, "&Help Version 1_25 swedish");
            AppendMenu(hSysMenu, MF_STRING, SYSMENU_SUB2_HELP_ID, "&Help Version 1_25 english");
            AppendMenu(hSysMenu, MF_STRING, SYSMENU_SUB3_HELP_ID, "&Help Version 1_26 swedish");
            AppendMenu(hSysMenu, MF_STRING, SYSMENU_SUB4_HELP_ID, "&Help Version 1_26 english");

        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Test if the About item was selected from the system menu
            if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SYSMENU_SETTINGS_ID))
            {
                FormSettings fi = new FormSettings();
                fi.ShowDialog();

            }


            //// If the Help item was selected from the system menu, open the pdf file...

            if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SYSMENU_SUB1_HELP_ID))
            {
                String openPDFFile = @"c:\windows\temp\pdfName.pdf";
                System.IO.File.WriteAllBytes(openPDFFile, global::BarcodePcApp.Properties.Resources.lathund_klara_barcode_v1_25);
                System.Diagnostics.Process.Start(openPDFFile);
            }

            if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SYSMENU_SUB2_HELP_ID))
            {
                String openPDFFile = @"c:\windows\temp\pdfName1.pdf";
                System.IO.File.WriteAllBytes(openPDFFile, global::BarcodePcApp.Properties.Resources.lathund_klara_barcode_v1_25_eng);
                System.Diagnostics.Process.Start(openPDFFile);
            }

            if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SYSMENU_SUB3_HELP_ID))
            {
                String openPDFFile = @"c:\windows\temp\pdfName2.pdf";
                System.IO.File.WriteAllBytes(openPDFFile, global::BarcodePcApp.Properties.Resources.lathund_klara_barcode_v1_26);
                System.Diagnostics.Process.Start(openPDFFile);
            }

            if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SYSMENU_SUB4_HELP_ID))
            {
                String openPDFFile = @"c:\windows\temp\pdfName3.pdf";
                System.IO.File.WriteAllBytes(openPDFFile, global::BarcodePcApp.Properties.Resources.lathund_klara_barcode_v1_26_eng);
                System.Diagnostics.Process.Start(openPDFFile);
            }

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.buttonLogout = new System.Windows.Forms.Button();
            this.labelKund = new System.Windows.Forms.Label();
            this.labelVerksamhet = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCheckIn = new System.Windows.Forms.Button();
            this.buttonCheckOut = new System.Windows.Forms.Button();
            this.buttonOverview = new System.Windows.Forms.Button();
            this.labelUserInfo = new System.Windows.Forms.Label();
            this.m_btnOrder = new System.Windows.Forms.Button();
            this.btnMove = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.chkUser = new System.Windows.Forms.CheckBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.btnRelease = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnPrintbarcodes = new System.Windows.Forms.Button();
            this.btnPrintLocBarcode = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonLogout
            // 
            this.buttonLogout.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonLogout.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonLogout.Location = new System.Drawing.Point(647, 306);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(73, 23);
            this.buttonLogout.TabIndex = 2;
            this.buttonLogout.Text = "Log out";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // labelKund
            // 
            this.labelKund.Location = new System.Drawing.Point(212, 56);
            this.labelKund.Name = "labelKund";
            this.labelKund.Size = new System.Drawing.Size(258, 29);
            this.labelKund.TabIndex = 36;
            this.labelKund.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelVerksamhet
            // 
            this.labelVerksamhet.Location = new System.Drawing.Point(209, 71);
            this.labelVerksamhet.Name = "labelVerksamhet";
            this.labelVerksamhet.Size = new System.Drawing.Size(261, 20);
            this.labelVerksamhet.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(206, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 31);
            this.label1.TabIndex = 38;
            this.label1.Text = "Welcome to KLARA!";
            // 
            // buttonCheckIn
            // 
            this.buttonCheckIn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonCheckIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCheckIn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonCheckIn.Location = new System.Drawing.Point(46, 93);
            this.buttonCheckIn.Name = "buttonCheckIn";
            this.buttonCheckIn.Size = new System.Drawing.Size(190, 63);
            this.buttonCheckIn.TabIndex = 39;
            this.buttonCheckIn.Text = "Register";
            this.buttonCheckIn.UseVisualStyleBackColor = false;
            this.buttonCheckIn.Click += new System.EventHandler(this.buttonCheckIn_Click);
            // 
            // buttonCheckOut
            // 
            this.buttonCheckOut.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonCheckOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonCheckOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCheckOut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonCheckOut.Location = new System.Drawing.Point(242, 93);
            this.buttonCheckOut.Name = "buttonCheckOut";
            this.buttonCheckOut.Size = new System.Drawing.Size(190, 63);
            this.buttonCheckOut.TabIndex = 40;
            this.buttonCheckOut.Text = "Discard a product";
            this.buttonCheckOut.UseVisualStyleBackColor = false;
            this.buttonCheckOut.Click += new System.EventHandler(this.buttonCheckOut_Click);
            // 
            // buttonOverview
            // 
            this.buttonOverview.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonOverview.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOverview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonOverview.Location = new System.Drawing.Point(46, 228);
            this.buttonOverview.Name = "buttonOverview";
            this.buttonOverview.Size = new System.Drawing.Size(386, 37);
            this.buttonOverview.TabIndex = 41;
            this.buttonOverview.Text = "Product overview";
            this.buttonOverview.UseVisualStyleBackColor = false;
            this.buttonOverview.Click += new System.EventHandler(this.buttonOverview_Click);
            // 
            // labelUserInfo
            // 
            this.labelUserInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelUserInfo.Location = new System.Drawing.Point(337, 308);
            this.labelUserInfo.Name = "labelUserInfo";
            this.labelUserInfo.Size = new System.Drawing.Size(304, 19);
            this.labelUserInfo.TabIndex = 42;
            this.labelUserInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_btnOrder
            // 
            this.m_btnOrder.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.m_btnOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.m_btnOrder.Location = new System.Drawing.Point(438, 93);
            this.m_btnOrder.Name = "m_btnOrder";
            this.m_btnOrder.Size = new System.Drawing.Size(190, 63);
            this.m_btnOrder.TabIndex = 43;
            this.m_btnOrder.Text = "Orders";
            this.m_btnOrder.UseVisualStyleBackColor = false;
            this.m_btnOrder.Click += new System.EventHandler(this.m_btnOrder_Click);
            // 
            // btnMove
            // 
            this.btnMove.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnMove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnMove.Location = new System.Drawing.Point(46, 162);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(130, 60);
            this.btnMove.TabIndex = 44;
            this.btnMove.Text = "Move products";
            this.btnMove.UseVisualStyleBackColor = false;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.Lime;
            this.progressBar1.Location = new System.Drawing.Point(17, 277);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(118, 17);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 45;
            this.progressBar1.Value = 30;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 281);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Please wait...";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(10, 297);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(246, 28);
            this.panel1.TabIndex = 49;
            // 
            // txtBarcode
            // 
            this.txtBarcode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtBarcode.Location = new System.Drawing.Point(12, 297);
            this.txtBarcode.MaxLength = 10;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(95, 20);
            this.txtBarcode.TabIndex = 1;
            this.txtBarcode.TextChanged += new System.EventHandler(this.txtBarcode_TextChanged);
            // 
            // chkUser
            // 
            this.chkUser.AutoSize = true;
            this.chkUser.Location = new System.Drawing.Point(113, 297);
            this.chkUser.Name = "chkUser";
            this.chkUser.Size = new System.Drawing.Size(123, 17);
            this.chkUser.TabIndex = 51;
            this.chkUser.Text = "Scan user and close";
            this.chkUser.UseVisualStyleBackColor = true;
            this.chkUser.CheckedChanged += new System.EventHandler(this.chkUser_CheckedChanged);
            this.chkUser.TextChanged += new System.EventHandler(this.txtBarcode_TextChanged);
            this.chkUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.form_KeyPress);
            // 
            // btnRelease
            // 
            this.btnRelease.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnRelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelease.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnRelease.Location = new System.Drawing.Point(438, 228);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(282, 37);
            this.btnRelease.TabIndex = 52;
            this.btnRelease.Text = "Release of products";
            this.btnRelease.UseVisualStyleBackColor = false;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // btnInventory
            // 
            this.btnInventory.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnInventory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnInventory.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInventory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnInventory.Location = new System.Drawing.Point(182, 162);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(130, 60);
            this.btnInventory.TabIndex = 44;
            this.btnInventory.Text = "Inventory products";
            this.btnInventory.UseVisualStyleBackColor = false;
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button2.Location = new System.Drawing.Point(540, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(190, 63);
            this.button2.TabIndex = 44;
            this.button2.Text = "Inventory";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // btnPrintbarcodes
            // 
            this.btnPrintbarcodes.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPrintbarcodes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPrintbarcodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintbarcodes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnPrintbarcodes.Location = new System.Drawing.Point(318, 162);
            this.btnPrintbarcodes.Name = "btnPrintbarcodes";
            this.btnPrintbarcodes.Size = new System.Drawing.Size(130, 60);
            this.btnPrintbarcodes.TabIndex = 44;
            this.btnPrintbarcodes.Text = "Print barcodes";
            this.btnPrintbarcodes.UseVisualStyleBackColor = false;
            this.btnPrintbarcodes.Click += new System.EventHandler(this.btnPrintbarcodes_Click);
            // 
            // btnPrintLocBarcode
            // 
            this.btnPrintLocBarcode.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPrintLocBarcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPrintLocBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintLocBarcode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnPrintLocBarcode.Location = new System.Drawing.Point(454, 162);
            this.btnPrintLocBarcode.Name = "btnPrintLocBarcode";
            this.btnPrintLocBarcode.Size = new System.Drawing.Size(130, 60);
            this.btnPrintLocBarcode.TabIndex = 44;
            this.btnPrintLocBarcode.Text = "Print location barcodes";
            this.btnPrintLocBarcode.UseVisualStyleBackColor = false;
            this.btnPrintLocBarcode.Click += new System.EventHandler(this.btnPrintLocBarcode_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnReset.Location = new System.Drawing.Point(590, 162);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(130, 60);
            this.btnReset.TabIndex = 44;
            this.btnReset.Text = "Reset products";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // FormMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.CancelButton = this.buttonLogout;
            this.ClientSize = new System.Drawing.Size(893, 389);
            this.Controls.Add(this.btnRelease);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chkUser);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnPrintLocBarcode);
            this.Controls.Add(this.btnPrintbarcodes);
            this.Controls.Add(this.btnInventory);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnMove);
            this.Controls.Add(this.m_btnOrder);
            this.Controls.Add(this.labelUserInfo);
            this.Controls.Add(this.buttonOverview);
            this.Controls.Add(this.buttonCheckOut);
            this.Controls.Add(this.buttonCheckIn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelVerksamhet);
            this.Controls.Add(this.labelKund);
            this.Controls.Add(this.buttonLogout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KLARA registration";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.Run(new FormMain());
        }

        public static bool IsNumeric(string str)
        {
            try
            {
 
                double d = double.Parse(str, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.CurrentInfo);

                //double e = double.Parse(str, System.Globalization.CultureInfo.GetCultureInfo("sv-SE"));

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void FormMain_Load(object sender, System.EventArgs e)
        {
            buttonCheckIn.Enabled = false;
            buttonCheckOut.Enabled = false;
            buttonOverview.Enabled = false;
            m_btnOrder.Enabled = false;
            btnRelease.Visible = false;
            btnRelease.Enabled = false;


            lang = FormMain.Get.LangChange;


            if (lang == true)
            {
                m_Main.label1.Text = "Välkommen till KLARA12!";
                m_Main.buttonLogout.Text = "Logga ut";
                m_Main.buttonCheckIn.Text = "Registrera produkter12";
                this.buttonCheckOut.Text = "Kasera produkter";
                this.buttonOverview.Text = "Översikt";
                this.m_btnOrder.Text = "Beställning";
                this.btnMove.Text = "Flytta produkter";
                this.btnInventory.Text = "Inventera produkter";
                this.btnPrintbarcodes.Text = "Prodstreckkoder";
                this.btnPrintLocBarcode.Text = "Utrstreckkoder";
                this.btnReset.Text = "Nollställ produkter";
                //this.chkUser.Text = "Skanna användare och stäng";
                this.btnRelease.Text = "Frisläpp produkter";
                this.Text = "KLARA registrering";


            }
            else
            {
                m_Main.label1.Text = "Welcome to KLARA12!";
                m_Main.buttonLogout.Text = "Log out";
                m_Main.buttonCheckIn.Text = "Register a product12";
                this.buttonCheckOut.Text = "Discard a product";
                this.buttonOverview.Text = "Product overview";
                this.m_btnOrder.Text = "Orders";
                this.btnMove.Text = "Move products";
                this.btnInventory.Text = "Inventory products";
                this.btnPrintbarcodes.Text = "Print Barcodes";
                this.btnPrintLocBarcode.Text = "Location barcode";
                this.btnReset.Text = "Reset products";
                //this.chkUser.Text = "Scan user and close";
                this.btnRelease.Text = "Release of products";
                this.Text = "KLARA registration";

            }



            pcTmpPath = Path.GetTempPath().ToString() + "BarcodePcApp\\";

            // Skapa kataloger om de saknas
            try
            {
                if (!Directory.Exists(pcTmpPath))
                {
                    Directory.CreateDirectory(pcTmpPath);
                }
            }
            catch
            {
                MessageBox.Show("Kunde inte skapa katalog");
                Application.Exit();
            }

            // Läs in data från inifilen
            try
            {

            string appPath = iniFilePath;

                if (File.Exists(appPath))
                {
                    //MessageBox.Show("finns: " + appPath);
                    readIniFile(appPath);
                }
                else
                {
                    //MessageBox.Show("finns inte: " + appPath);
                    if (File.Exists("BarcodePcApp.ini"))
                    {
                        DirectoryInfo dir = new DirectoryInfo(m_sAppDataPath); 
                        if (!dir.Exists) 
                        { 
                            dir.Create(); 
                        }
                        File.Copy("BarcodePcApp.ini", appPath);
                        readIniFile(appPath);
                    }
                }

            }
            catch
            {
#if (DEBUG)
                FormIniFil fi = new FormIniFil();	// Skapar ny inifil
                fi.ShowDialog();
#else
				MessageBox.Show("Fel! - Inifil saknas.\r\n\nKontakta Nordic Port för att åtgärda detta.");
				Application.Exit();
#endif
            }
            if (checkPCVersion())
            {
                this.Close();
                return;
            }

            buttonLogin_Click(this, e);	// Visa logindialogen

            if (userName.Length == 0)
            {
                this.Close();
                return;    // Loggade aldrig in
            }

            barcode.BarcodeService bc = getBarcodeService();

            // Hämta kundinformation, kemidatabas mm från webservern
            string infoString = "";
            try
            {
                infoString = bc.GetInfo(databas, verkId);
            }
            catch (Exception err)
            {
                MessageBox.Show("No contact with the web server!\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message);
                Application.Exit();
                return;
            }
            DataSet ds = new DataSet();
            StringReader sri = new StringReader(infoString);
            ds.ReadXml(sri);
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.Rows[0];

            kundNamn = dr["KundNamn"].ToString();
            verkNamn = dr["VerkNamn"].ToString();
            kemiDb = dr["GemKemDB"].ToString().ToUpper();
            m_sBarcodePrefix = dr["BarcodePrefix"].ToString().ToUpper();
            m_sRelease = dr["release"].ToString().ToUpper();


            dt.Dispose();
            ds.Dispose();
            sri.Close();            


            //FOI har xxxxxx som kundnamn. Visa inte deras namn.
            if (kundNamn != "xxxxxx")
            {
                labelKund.Text = kundNamn;
            }
            else
            {
                labelKund.Text = "";
            }
            
            if (Convert.ToInt32(userStringSys) == 1)
                this.buttonCheckIn.Enabled = true;

            if (isBestallare)
                m_btnOrder.Visible = true;
            else
                m_btnOrder.Visible = false;



            if (m_sRelease=="1")
            {
                btnRelease.Visible = true;
                btnRelease.Enabled = true;
            }

            if (m_sBarcodePrefix == "BENÄMNING SAKNAS")
            {
                MessageBox.Show("Barcode prefix is missing. Please contact your administrator.");
            }
            else
            {
                buttonCheckIn.Enabled = true;
                buttonCheckOut.Enabled = true;
                buttonOverview.Enabled = true;
                m_btnOrder.Enabled = true;
            }
            this.Show();

            //OpenRelease();

            //OpenInventory(); //flagga test
            //OpenCheckOut(); //flagga test för ver 1.19
            //OpenOrder(); //flagga test för ver 1.19
            //OpenCheckIn(); //flagga test för ver 1.19
        }


        private void readIniFile(string iniFilePath)
        {
            StreamReader sr = new StreamReader(iniFilePath);
            string crc = "";
            char[] sepChr = new char[1] { '|' };
            string iniRad;
            string[] iniKol;
            while ((iniRad = sr.ReadLine()) != null)
            {
                iniKol = iniRad.Split(sepChr);
                switch (iniKol[0].Trim())
                {
                    case "databas":
                        databas = iniKol[1].Trim();
                        break;
                    case "name":
                        kundNamn = iniKol[1].Trim();
                        break;
                    case "barcode_printer":
                        m_sBarcodePrinter = iniKol[1].Trim();
                        break;
                    case "barcode_width":
                        m_nBarcodeWidth = Convert.ToInt32(iniKol[1].Trim());
                        break;
                    case "barcode_height":
                        m_nBarcodeHeight = Convert.ToInt32(iniKol[1].Trim());
                        break;
                    case "barcode_left":
                        m_nBarcodeLeft = Convert.ToInt32(iniKol[1].Trim());
                        break;
                    case "barcode_top":
                        m_nBarcodeTop = Convert.ToInt32(iniKol[1].Trim());
                        break;
                    case "barcode_layout":
                        int nTmp = Convert.ToInt32(iniKol[1].Trim());
                        if (nTmp < 0 || nTmp > 10)
                            nTmp = 0;
                        m_nBarcodeLayout = nTmp;
                        break;
                    case "check":
                        crc = iniKol[1].Trim();
                        break;
                    case "forced_update":
                        if (iniKol[1].Trim().Equals("False"))
                        {
                            forcedUpdate = false;
                        }
                        break;
                    case "inventory_dialog_same_org":
                        if (iniKol[1].Trim().Equals("True"))
                        {
                            showDialogOnSameDepartment = true;
                        }
                        break;
                    case "login_barcode":
                        enableLoginByBarcode = true;
                        if (iniKol[1].Trim().Equals("True"))
                        {
                            loginByBarcodeSelected = true;
                        }
                        break;
                    case "scan_rooms":
                        if (iniKol[1].Trim().Equals("True"))
                        {
                            changeRoomByBarcode = true;
                        }
                        break;
                        //Sandi
                    case "property_check":
                        if (iniKol[1].Trim().Equals("True"))
                        {
                            //changeRoomByBarcode = true;
                            showPropertyCheck = true;
                            //showPropertyChange = FormMain.Get.PropertyChange;
                        }
                        break;
                    case "lang_set":
                        if (iniKol[1].Trim().Equals("True"))
                        {
                            LangChange = true;
                            FormMain.Get.LangChange = true;
                        }
                        break;

                        //Sandi 161121
                        //case "property_change":
                        //    if (iniKol[1].Trim().Equals("True"))
                        //    {
                        //        //changeRoomByBarcode = true;
                        //        //showPropertyCheck = true;
                        //        showPropertyChange = FormMain.Get.PropertyChange;
                        //    }
                        //    break;

                }
            }
            sr.Close();

            string chk = "Databas: " + databas + " Verkid: "; // Sträng att kryptera
            Misc.SymCryptography cryptic = new Misc.SymCryptography();
            cryptic.Key = "wqdj~yriu!@*k0_^fa7431%p$#=@hd+&";
            chk = cryptic.Encrypt(chk);
            #if (! DEBUG)
				if(chk != crc) {
					MessageBox.Show("Fel! - Inifil är felaktig.\r\n\nKontakta Nordic Port för att åtgärda detta.");
					Application.Exit();
					return;
				}
            #endif
        }

        private bool checkPCVersion()
        {

#if DEBUG
//#if RELEASE //( endast för test i lokalmiljö innan ny version )
            return false;
#else

                
                barcode.BarcodeService bc = getBarcodeService();
            //OBS!!!! här smäller det när man tappar kontakt med ws
            // sätt en kontroll här
            string serverPcAppVer_q = bc.GetVersion();
            XmlDocument doc1 = new XmlDocument();
            doc1 = new XmlDocument();
            doc1.LoadXml(serverPcAppVer_q);
            XmlNodeList xmlnodeList1 = doc1.DocumentElement.SelectNodes("item");
            int serverPcAppVer = 0;
            foreach (XmlNode item in xmlnodeList1)
            {
                serverPcAppVer = Convert.ToInt32(item.Attributes["id"].Value);
            }

            //int serverPcAppVer = Convert.ToInt32(bc.GetVersion());



                if (thisPcAppVer < serverPcAppVer && forcedUpdate)
                {  // Finns ny version av programmet? && user HAS TO update their client software

                    FormNyVersion fn = new FormNyVersion();
                    fn.pcAppVer = thisPcAppVer;
                    fn.serverPcAppVer = serverPcAppVer;
                    fn.ShowDialog(this);

                    return true;
                }
                pcAppVer = serverPcAppVer;
                if (thisPcAppVer < pcAppVer && !forcedUpdate) //Check if updates are enforced and that there is a new version, if they aren't change the address to the barcode service with the same version number.
                {
                    this.Text = "KLARA registration v" + Convert.ToString(Math.Round(((thisPcAppVer / 100.0)), 2)).Replace(",", ".") + "(UPDATE AVAILABLE)";
                }
                return false;


#endif
        }

        /// <summary>
        /// Returns a BarcodeService object, with the correct URL path. Takes debug and forced updates into consideration.
        /// </summary>
        /// <returns>BarcodeService object with correct URL</returns>
        public static barcode.BarcodeService getBarcodeService()
        {
            
            barcode.BarcodeService bc = new barcode.BarcodeService(); //Create webservice object
            //MessageBox.Show("URL: " + bc.Url);
            if (FormMain.localhost) //Check if program is in debug mode, if so make call to Localhost instead of www.port.se
            {

                bc.Url = bc.Url.Replace("www.port.se", "localhost");
                //MessageBox.Show("Nu är jag inne i FormMain.localhost if, url = " + bc.Url);
                 
            }
            else
            {
                //bc.Url = "http://www.port.se/ws/barcode.cfc";
                //bc.Url = "https://secure.port.se/ws/barcode.cfc";  // när anders vill testa skarpt på SSL
                //bc.Url = "http://nu.klaratest.port.se/ws/barcode.cfc"; // aktiveras när man vill koppla upp sig mot testserver AQTEST
                //MessageBox.Show("Nu är jag inne i FormMain.localhost else, url = " + bc.Url);
            }



            if (thisPcAppVer < pcAppVer && !forcedUpdate) //Check if updates are enforced and that there is a new version, if they aren't change the address to the barcode service with the same version number.
            {
                //bc.Url = bc.Url.Replace("barcode.cfc", FormMain.thisPcAppVer + ".cfc");
                bc.Url = bc.Url.Replace("barcode.cfc", FormMain.thisPcAppVer + ".cfc");
            }
            //MessageBox.Show("URL_ut:    " + bc.Url);
            //MessageBox.Show("thisPC: " + thisPcAppVer + " server: " + pcAppVer);

            if ((bc.Url == "http://aq201510/ws/barcode.cfc") || (bc.Url == "http://ny/ws/barcode.cfc"))
            {
                m_Main.BackColor = Color.Lavender;
                if(lang == true)
                {
                    m_Main.label1.Text = "Välkommen till KLARA123! ( lokal )";
                }
                else
                {
                    m_Main.label1.Text = "Welcome to KLARA123! ( lokal )";
                }
                
                
                //FormMain.localhost = true;
                //MessageBox.Show(bc.Url);
                //m_Main.label1.Text = bc.Url;


            }
            else if (bc.Url == "http://nu.klaratest.port.se/ws/barcode.cfc")
            {
                m_Main.BackColor = Color.LightBlue;
                m_Main.label1.Text = "Welcome to KLARA! ( test )";
               // MessageBox.Show(bc.Url);

            }
            else if (bc.Url == "https://aqtest.port.se/alphaquest/app_su/barcode.cfc")
            {
                m_Main.BackColor = Color.LightBlue;
                m_Main.label1.Text = "Welcome to KLARA! ( SSO_su )";

            }
            else if (bc.Url == "https://aqtest.port.se/alphaquest/app_kikem/barcode.cfc")
            {
                m_Main.BackColor = Color.LightBlue;
                m_Main.label1.Text = "Welcome to KLARA! ( SSO_kikem )";

            }
            else if (bc.Url == "https://secure.port.se/ws/abc.cfc")
            {
                m_Main.BackColor = Color.LightBlue;
                m_Main.label1.Text = "Welcome to KLARA! ( Skarpa test )";

            }
            else 
            {
            
            }

            return bc;


        }

        private void buttonLogin_Click(object sender, System.EventArgs e)
        {

            if (databas == "")
            {
                labelUserInfo.Text = "The ini-file is missing or not correct! (BarcodePcApp.ini) ";
                return;
            }

            

            FormLogin fl = new FormLogin(enableLoginByBarcode, loginByBarcodeSelected);
            fl.Databas = databas;
            fl.KundNamn = kundNamn;
            fl.PcAppVer = thisPcAppVer;

            //Kontroll på att angiven databas har streckkod
            //barcode.BarcodeService ws = FormMain.getBarcodeService();
            //string dbString = "";
            //try
            //{
            //    dbString = ws.GetDBs();
            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show("Detta felmedelande rapporterades:\r\n\n" + err.Message, "GetDBs" + " ADRESS: " + ws.Url);
            //    ws.Dispose();
            //    return;
            //}

            //



            if (fl.ShowDialog(this) != DialogResult.OK) return;

            if (fl.UserName.ToString() != "0" || fl.UserName.ToString() != "-1")
            {
                orgAr = fl.OrgAr;
                orgNod = fl.OrgNod;
                verkId = fl.VerkId;
                userId = fl.UserId;
                userName = fl.UserName;
                userStringName = fl.Fullname;
                labelUserInfo.Text = userStringName;
                

                //userStringDepart = split[1];
                //userStringSys = split[2];

                barcode.BarcodeService bc = getBarcodeService();

                if (labelKund.Text != "")
                { // Hämta kundinformation, kemidatabas mm från webservern
                    string infoString = "";
                    try
                    {
                        infoString = bc.GetInfo(databas, verkId);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("No contact with the web server!\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message);
                        Application.Exit();
                        return;
                    }
                    DataSet ds2 = new DataSet();
                    StringReader sr2 = new StringReader(infoString);
                    ds2.ReadXml(sr2);
                    DataTable dt2 = ds2.Tables[0];
                    DataRow dr2 = dt2.Rows[0];

                    kundNamn = dr2["KundNamn"].ToString();
                    verkNamn = dr2["VerkNamn"].ToString();
                    kemiDb = dr2["GemKemDB"].ToString().ToUpper();
                    dt2.Dispose();
                    ds2.Dispose();
                    sr2.Close();

                    labelKund.Text = kundNamn;
                    labelVerksamhet.Text = verkNamn;
                }
            }
            else return;
        }

        //Sandi 140603
        public void Logout()
        {
            userId = 0;
            orgNod = 0;
            orgAr = 0;
            userName = "";
            userStringName = "";
            userStringSys = "";
            userStringDepart = "";
            labelUserInfo.Text = "";
            //this.Hide();
            //this.Close();

            m_Main.Dispose();
            Application.Restart();
        }

        //Sandi 140603
        //public void OpenInventory()
        //{
        //    FormInventory inventory = new FormInventory(verkId, databas, kemiDb);

        //    if (inventory.shouldBeShown)
        //    {
        //        inventory.ShowDialog(this);

        //    }
        //    else
        //    {
        //        MessageBox.Show("You do not have inventory access to any deparments. If you need to access to the inventory feature, please contact your administrator.", "Access Denied");

        //        inventory.Dispose();
        //    }

        //    //m_Main.Dispose();
        //    //Application.Restart();
        //}

        //Sandi 140603
        public void OpenRelease()
        {
            FormRelease release = new FormRelease();
            release.Databas = databas;
            release.KemiDb = kemiDb;
            release.OrgAr = orgAr;
            release.OrgNod = orgNod;
            release.VerkId = verkId;
            release.UserId = userId;
            release.UserName = userName;
            release.UserStringDepart = userStringDepart;
            release.UserStringName = userStringName;
            release.UserStringSys = userStringSys;
            release.ShowDialog(this);
            release.Dispose();

        }

        //Sandi 150622 - för version 1.19
        //public void OpenCheckOut()
        //{
        //    FormCheckOut checkout = new FormCheckOut();
        //    checkout.Databas = databas;
        //    checkout.KemiDb = kemiDb;
        //    checkout.OrgAr = orgAr;
        //    checkout.OrgNod = orgNod;
        //    checkout.VerkId = verkId;
        //    checkout.UserId = userId;
        //    checkout.UserName = userName;
        //    checkout.UserStringDepart = userStringDepart;
        //    checkout.UserStringName = userStringName;
        //    checkout.UserStringSys = userStringSys;
        //    //checkout.OrgNamn = ((StringIntObject)listBoxOrgnod.SelectedItem).m_sData;
        //    //checkout.OrgNamn = "OrgNamn, hämta denna senare...";

        //    checkout.ShowDialog(this);
        //    checkout.Dispose();
        //}


        private void buttonLogout_Click(object sender, System.EventArgs e)
        {
            userId = 0;
            orgNod = 0;
            orgAr = 0;
            userName = "";
            userStringName = "";
            userStringSys = "";
            userStringDepart = "";
            labelUserInfo.Text = "";

            m_Main.Dispose();

            Application.Restart();
            //this.Close();


        }

        public const short FILE_ATTRIBUTE_NORMAL = 0x80;
        public const uint GENERIC_READ = 0x80000000;
        public const short CREATE_ALWAYS = 2;
        [DllImport("rapi.dll", CharSet = CharSet.Unicode)]
        public static extern int CeCloseHandle(int hObject);
        [DllImport("rapi.dll", CharSet = CharSet.Unicode)]
        public static extern int CeCreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode, int lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplateFile);

        private void buttonCheckIn_Click(object sender, EventArgs e)
        {
            FormCheckIn checkin = new FormCheckIn();
            checkin.Databas = databas;
            checkin.KemiDb = kemiDb;
            checkin.OrgAr = orgAr;
            //checkin.OrgNod = orgNod;
            checkin.VerkId = verkId;
            checkin.UserId = userId;
            checkin.UserName = userName;
            checkin.UserStringDepart = userStringDepart;
            checkin.UserStringName = userStringName;
            checkin.UserStringSys = userStringSys;
            checkin.LokalDB = lokaldb;
                //checkin.OrgNamn = ((StringIntObject)listBoxOrgnod.SelectedItem).m_sData;
            //checkin.OrgNamn = "OrgNamn, hämta denna senare...";
            checkin.ShowDialog(this);
            checkin.Dispose();
        }

        private void buttonCheckOut_Click(object sender, EventArgs e)
        {
            FormCheckOut checkout = new FormCheckOut();
            checkout.Databas = databas;
            checkout.KemiDb = kemiDb;
            checkout.OrgAr = orgAr;
            checkout.OrgNod = orgNod;
            checkout.VerkId = verkId;
            checkout.UserId = userId;
            checkout.UserName = userName;
            checkout.UserStringDepart = userStringDepart;
            checkout.UserStringName = userStringName;
            checkout.UserStringSys = userStringSys;
            //checkout.OrgNamn = ((StringIntObject)listBoxOrgnod.SelectedItem).m_sData;
            //checkout.OrgNamn = "OrgNamn, hämta denna senare...";
            checkout.ShowDialog(this);
            checkout.Dispose();
        }

        private void buttonOverview_Click(object sender, EventArgs e)
        {
            FormOverview overview = new FormOverview();
            overview.Databas = databas;
            overview.KemiDb = kemiDb;
            overview.OrgAr = orgAr;
            overview.OrgNod = orgNod;
            overview.VerkId = verkId;
            overview.UserId = userId;
            overview.UserName = userName;
            overview.UserStringDepart = userStringDepart;
            overview.UserStringName = userStringName;
            overview.UserStringSys = userStringSys;
            //overview.OrgNamn = ((StringIntObject)listBoxOrgnod.SelectedItem).m_sData;
            //overview.OrgNamn = "OrgNamn, hämta denna senare...";

            overview.ShowDialog(this);
            overview.Dispose();
        }

        private void m_btnOrder_Click(object sender, EventArgs e)
        {
            FormOrder order = new FormOrder();
            order.ShowDialog(this);
            order.Dispose();
        }


        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.I))
            {
                string s = "Version: " + Convert.ToString(Math.Round(((thisPcAppVer / 100.0)), 2)).Replace(",", ".") + "\n\r";
                s = s + "Databas: " + databas + "\n\r";
                s = s + "Kemidatabas: " + KemiDB + "\n\r";
                s = s + "Aktuellt år: " + orgAr + "\n\r";
                s = s + "Orgnod: " + orgNod + "\n\r";
                s = s + "Verkid: " + verkId + "\n\r";
                s = s + "Användarid: " + userId + "\n\r";
                s = s + "Användare: " + userName + "\n\r";
                s = s + "Forskargrupp: " + userStringDepart + "\n\r";
                s = s + "För- och efternamn: " + userStringName + "\n\r";
                s = s + "Inköpare: " + (isInkopare?"Ja":"Nej") + "\n\r";
                s = s + "Beställare: " + (isBestallare?"Ja":"Nej") + "\n\r";
                s = s + "Systemadministratör: " + (userStringSys=="1"?"Ja":"Nej") + "\n\r\n\r";
                s = s + "Användarens tempkatalog: " + pcTmpPath + "\n\r";
                
                MessageBox.Show(s, "Debug information");
            }
        }

        //private void buttonInventory_Click(object sender, EventArgs e)
        //{

        //    FormInventory inventory = new FormInventory(verkId, databas, kemiDb);

        //    if (inventory.shouldBeShown)
        //    {
        //        inventory.ShowDialog(this);

        //    }
        //    else
        //    {
        //        MessageBox.Show("You do not have inventory access to any deparments. If you need to access to the inventory feature, please contact your administrator.", "Access Denied");
        //        lang = FormMain.Get.LangChange;
        //        inventory.Dispose();
        //    }

        //}

        //Sandi_barcode
        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
            //barcode.BarcodeService bc = new barcode.BarcodeService(); //Create webservice object
            //barcodeInSettings = bc.GetLoginBarcode(databas, userId);
                FormSettings form1 = new FormSettings();
                barcodeInSettings = form1.loginBarcode;
           
            if (barcode == barcodeInSettings)
            {
                FormMain form = new FormMain();
                form.Dispose();
                //this.Invoke(new MethodInvoker(delegate { FormMain.Get.Logout(); })); 
                this.Close();
                buttonLogout_Click(this, e);	// Visa logindialogen
                //this.Close();
                //this.Owner.Close();
            }
            }
                
            catch(Exception err)
            {
            }
           
        }

        //private string checkinbarcode = "";
        private void form_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 65 && e.KeyChar <= 122)) && loginByBarcodeSelected)
                {
                    //scannerTimer.Enabled = false;
                    scannerTimer.Stop();

                    barcode += e.KeyChar;
                    //barcode = txtBarcode.Text;

                    //scannerTimer.Enabled = true;
                    scannerTimer.Start();
                }
            }
            catch (Exception)
            { }
        }

        // Check if user want to use scan 
        private void chkUser_CheckedChanged(object sender, EventArgs e)
        {
            txtBarcode.Clear();
            if (chkUser.CheckState == CheckState.Checked)
            {
                panel1.Visible = true;
                txtBarcode.Enabled = true;
                txtBarcode.Focus();
            }
            else
            {
                panel1.Visible = false;
                txtBarcode.Focus();
                txtBarcode.Enabled = true;
            }
        }




        //private bool inventoryLoading = false;
        private void scannerTimer_Elapsed(object source, ElapsedEventArgs e)
        {
           try
            {
                barcode.BarcodeService bc = new barcode.BarcodeService(); //Create webservice object
                barcodeInSettings = bc.GetLoginBarcode(databas, FormMain.Get.UserId);

                //scannerTimer.Enabled = false; 
                scannerTimer.Stop();

                if (barcode.StartsWith("r") && barcode.Length > 1 && changeRoomByBarcode)
                {
                    MessageBox.Show("You need to choose one function first!");
                    barcode = "";
                    txtBarcode.Clear();
                    txtBarcode.Focus();
                    //        if (!inventoryLoading)
                    //        {
                    //            inventoryLoading = true;
                    //            FormInventory inventory = new FormInventory(verkId, databas, kemiDb, barcode);

                    //            if (inventory.shouldBeShown)
                    //            {
                    //                this.Invoke(new MethodInvoker(delegate
                    //                {
                    //                    inventory.ShowDialog(this);
                    //                }));

                    //            }
                    //            else
                    //            {
                    //                MessageBox.Show("You do not have inventory access to any deparments. If you need to access to the inventory feature, please contact your administrator.", "Access Denied");
                    //            }

                    //            inventory.Dispose();
                    //            inventoryLoading = false;
                    //        }



                }
                else if (barcodeInSettings == barcode)
                {
                    this.Close();
                    buttonLogout_Click(this, e);	// Visa logindialogen
                    this.Owner.Close();
                }
            }
            catch (Exception err)
            { }

            //barcode = "";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void btnRelease_Click(object sender, EventArgs e)
        {
            FormRelease release = new FormRelease();
            release.Databas = databas;
            release.KemiDb = kemiDb;
            release.OrgAr = orgAr;
            release.OrgNod = orgNod;
            release.VerkId = verkId;
            release.UserId = userId;
            release.UserName = userName;
            release.UserStringDepart = userStringDepart;
            release.UserStringName = userStringName;
            release.UserStringSys = userStringSys;
            release.ShowDialog(this);
            release.Dispose();

        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            string choice = "move";
            FormInventory inventory = new FormInventory(verkId, databas, kemiDb,choice);
            

            if (inventory.shouldBeShown)
            {
                inventory.ShowDialog(this);

            }
            else
            {
                MessageBox.Show("You do not have inventory access to any deparments. If you need to access to the inventory feature, please contact your administrator.", "Access Denied");
                lang = FormMain.Get.LangChange;
                inventory.Dispose();
            }

        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            string choice = "inv";
            FormInventory inventory = new FormInventory(verkId, databas, kemiDb, choice);


            if (inventory.shouldBeShown)
            {
                inventory.ShowDialog(this);

            }
            else
            {
                MessageBox.Show("You do not have inventory access to any deparments. If you need to access to the inventory feature, please contact your administrator.", "Access Denied");
                lang = FormMain.Get.LangChange;
                inventory.Dispose();
            }

        }

        private void btnPrintbarcodes_Click(object sender, EventArgs e)
        {
            string choice = "pbarc";
            FormInventory inventory = new FormInventory(verkId, databas, kemiDb, choice);


            if (inventory.shouldBeShown)
            {
                inventory.ShowDialog(this);

            }
            else
            {
                MessageBox.Show("You do not have inventory access to any deparments. If you need to access to the inventory feature, please contact your administrator.", "Access Denied");
                lang = FormMain.Get.LangChange;
                inventory.Dispose();
            }

        }

        private void btnPrintLocBarcode_Click(object sender, EventArgs e)
        {
            string choice = "lbarc";
            FormInventory inventory = new FormInventory(verkId, databas, kemiDb, choice);


            if (inventory.shouldBeShown)
            {
                inventory.ShowDialog(this);

            }
            else
            {
                MessageBox.Show("You do not have inventory access to any deparments. If you need to access to the inventory feature, please contact your administrator.", "Access Denied");
                lang = FormMain.Get.LangChange;
                inventory.Dispose();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            string choice = "reset";
            FormInventory inventory = new FormInventory(verkId, databas, kemiDb, choice);


            if (inventory.shouldBeShown)
            {
                inventory.ShowDialog(this);

            }
            else
            {
                MessageBox.Show("You do not have inventory access to any deparments. If you need to access to the inventory feature, please contact your administrator.", "Access Denied");
                lang = FormMain.Get.LangChange;
                inventory.Dispose();
            }
        }
    }
}