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
using System.Reflection;


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

        public static int thisPcAppVer = 127;	

        public static string pcTmpPath = "";        // Tempkatalog på pc:n - sätts till användarens tempkatalog

        public static bool localhost = false;       // kör mot lokal server, inte webservern.
        private static string databas = "";
        public static string kemiDb = "";	        // kemikaliedatabasen - "gemkem" eller motsv.
        private static string kundNamn = "";
        private static string verkNamn = "";
        private static bool isInkopare = false;
        private static bool isBestallare = false;
        private static bool IsAdministrator = false;   
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
        public static string plats = "";


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
        
        private bool showPropertyCheck = false;
        private bool showPropertyChange = false;

        static private FormMain m_Main = null;

        // P/Invoke constants
        private const int WM_SYSCOMMAND = 0x112;
        private const int MF_STRING = 0x0;
        private Button m_btnOrder;
        private Button btnMove;
        private System.Windows.Forms.Timer timer1;
        private TextBox txtBarcode;
        private CheckBox chkUser;
        private HelpProvider helpProvider1;
        private Button btnRelease;
        private Button btnInventory;
        private Button btnPrintbarcodes;
        private Button btnPrintLocBarcode;
        private Button btnReset;
        private GroupBox grpReg;
        private GroupBox grpInv;
        private Panel panel2;
        private ComboBox cboManual;
        private Button btnSettings;
        private PictureBox picBoxReg;
        private PictureBox picBoxOrder;
        private PictureBox picBoxDiscard;
        private PictureBox picBoxMove;
        private PictureBox picBoxOverview;
        private PictureBox picBoxRelease;
        private ImageList imageList1;
        private PictureBox pictureBox6;
        private ToolTip toolTip1;
        private PictureBox picBoxReset;
        private PictureBox picBoxLbarc;
        private PictureBox picBoxPbar;
        private PictureBox picBoxInv;
        private Button button2;
        private Panel panel1;
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


        private System.Timers.Timer scannerTimer = new System.Timers.Timer();
        private string barcode = "";

        public FormMain()
        {
            m_Main = this;

            InitializeComponent();
            button2.Visible = false;

            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////
            ///
            ////// Info om operativsystem --->>> till version 1.27
            ///
            /// //////////////////////////////////////////////////////////////////
            //string HKLMWinNTCurrent = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion";
            //string osType = Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit";
            //string osVers = Environment.OSVersion.Version.ToString();
            //string osName = Registry.GetValue(HKLMWinNTCurrent, "productName", "").ToString();
            //string osRelease = Registry.GetValue(HKLMWinNTCurrent, "ReleaseId", "").ToString();
            //var os = Environment.OSVersion;

            //if(!Directory.Exists(@"C:\Log"))
            //{
            //    Directory.CreateDirectory(@"C:\Log");
            //}
            //// Your program runs, you add log lines
            //string[] start = { DateTime.Now + "--" +osName +"--"+osType+ "--" + osName+ "--"+osRelease+ "--" + os+": Raden slutar\n" };
            //File.AppendAllLines(@"C:\Log\MinLoggFil.txt", start);
            // End add log lines

            //MessageBox.Show("Current OS Information:\n");
            //MessageBox.Show("OS Name: " + osName);
            //MessageBox.Show("OS Release: " + osRelease);
            //MessageBox.Show("OS Type: " + osType);
            //MessageBox.Show("OS Version: " + osVers);

            ////barcode.BarcodeService bc = getBarcodeService();
            ////string OSinfoString = "";
            ////// skicka till ws där parametrar sparas i DB
            ////OSinfoString = bc.GetOSInfo(databas, verkId, osName, osType, osRelease, osVers);


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


       
        static public FormMain Get{ get { return m_Main; } }

        public bool ShowDialogOnSameDepartment { get { return showDialogOnSameDepartment; } set { showDialogOnSameDepartment = value; } }
        //Sandi
        public bool PropertyCheck { get { return showPropertyCheck; } set { showPropertyCheck = value; } }

        public bool LangChange { get { return lang; } set { lang = value; } }

        public bool PropertyChange { get { return showPropertyChange; } set { showPropertyChange = value; } }
        //
        public int OrgNod { get { return orgNod; } }
        public int OrgAr { get { return orgAr; } }
        public int VerkId { get { return verkId; } }
        public int UserId { get { return userId; } }
        public int BarcodeWidth { get { return m_nBarcodeWidth; } set { m_nBarcodeWidth = value; } }
        public int BarcodeHeight { get { return m_nBarcodeHeight; } set { m_nBarcodeHeight = value; } }

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

        public static bool IsAdmin
        { 
            get { return FormMain.IsAdministrator; }
            set { FormMain.IsAdministrator = value; }
        }


        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Get a handle to a copy of this form's system (window) menu
            IntPtr hSysMenu = GetSystemMenu(this.Handle, false);

            // Add a separator
            AppendMenu(hSysMenu, MF_SEPARATOR, 0, string.Empty);

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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.chkUser = new System.Windows.Forms.CheckBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.btnRelease = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnPrintbarcodes = new System.Windows.Forms.Button();
            this.btnPrintLocBarcode = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.grpReg = new System.Windows.Forms.GroupBox();
            this.picBoxOrder = new System.Windows.Forms.PictureBox();
            this.picBoxDiscard = new System.Windows.Forms.PictureBox();
            this.picBoxReg = new System.Windows.Forms.PictureBox();
            this.grpInv = new System.Windows.Forms.GroupBox();
            this.picBoxLbarc = new System.Windows.Forms.PictureBox();
            this.picBoxPbar = new System.Windows.Forms.PictureBox();
            this.picBoxInv = new System.Windows.Forms.PictureBox();
            this.picBoxMove = new System.Windows.Forms.PictureBox();
            this.picBoxReset = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cboManual = new System.Windows.Forms.ComboBox();
            this.btnSettings = new System.Windows.Forms.Button();
            this.picBoxOverview = new System.Windows.Forms.PictureBox();
            this.picBoxRelease = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpReg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxDiscard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxReg)).BeginInit();
            this.grpInv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLbarc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxInv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxReset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxOverview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxRelease)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonLogout
            // 
            this.buttonLogout.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonLogout.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonLogout.Location = new System.Drawing.Point(842, 824);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(116, 35);
            this.buttonLogout.TabIndex = 12;
            this.buttonLogout.Text = "Log out";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // labelKund
            // 
            this.labelKund.Location = new System.Drawing.Point(214, 91);
            this.labelKund.Name = "labelKund";
            this.labelKund.Size = new System.Drawing.Size(413, 41);
            this.labelKund.TabIndex = 36;
            this.labelKund.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelVerksamhet
            // 
            this.labelVerksamhet.Location = new System.Drawing.Point(227, 151);
            this.labelVerksamhet.Name = "labelVerksamhet";
            this.labelVerksamhet.Size = new System.Drawing.Size(418, 29);
            this.labelVerksamhet.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(174, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(386, 46);
            this.label1.TabIndex = 38;
            this.label1.Text = "Welcome to KLARA!";
            // 
            // buttonCheckIn
            // 
            this.buttonCheckIn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonCheckIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCheckIn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonCheckIn.Location = new System.Drawing.Point(27, 41);
            this.buttonCheckIn.Name = "buttonCheckIn";
            this.buttonCheckIn.Size = new System.Drawing.Size(306, 92);
            this.buttonCheckIn.TabIndex = 1;
            this.buttonCheckIn.Text = "Register";
            this.buttonCheckIn.UseVisualStyleBackColor = false;
            this.buttonCheckIn.Click += new System.EventHandler(this.buttonCheckIn_Click);
            this.buttonCheckIn.MouseLeave += new System.EventHandler(this.buttonCheckIn_MouseLeave);
            this.buttonCheckIn.MouseHover += new System.EventHandler(this.buttonCheckIn_MouseHover);
            // 
            // buttonCheckOut
            // 
            this.buttonCheckOut.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonCheckOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonCheckOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCheckOut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonCheckOut.Location = new System.Drawing.Point(27, 142);
            this.buttonCheckOut.Name = "buttonCheckOut";
            this.buttonCheckOut.Size = new System.Drawing.Size(306, 92);
            this.buttonCheckOut.TabIndex = 2;
            this.buttonCheckOut.Text = "Discard a product";
            this.buttonCheckOut.UseVisualStyleBackColor = false;
            this.buttonCheckOut.Click += new System.EventHandler(this.buttonCheckOut_Click);
            this.buttonCheckOut.MouseLeave += new System.EventHandler(this.buttonCheckOut_MouseLeave);
            this.buttonCheckOut.MouseHover += new System.EventHandler(this.buttonCheckOut_MouseHover);
            // 
            // buttonOverview
            // 
            this.buttonOverview.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonOverview.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOverview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonOverview.Location = new System.Drawing.Point(114, 609);
            this.buttonOverview.Name = "buttonOverview";
            this.buttonOverview.Size = new System.Drawing.Size(305, 93);
            this.buttonOverview.TabIndex = 4;
            this.buttonOverview.Text = "Product overview";
            this.buttonOverview.UseVisualStyleBackColor = false;
            this.buttonOverview.Click += new System.EventHandler(this.buttonOverview_Click);
            this.buttonOverview.MouseLeave += new System.EventHandler(this.buttonOverview_MouseLeave);
            this.buttonOverview.MouseHover += new System.EventHandler(this.buttonOverview_MouseHover);
            // 
            // labelUserInfo
            // 
            this.labelUserInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelUserInfo.Location = new System.Drawing.Point(544, 833);
            this.labelUserInfo.Name = "labelUserInfo";
            this.labelUserInfo.Size = new System.Drawing.Size(288, 28);
            this.labelUserInfo.TabIndex = 42;
            this.labelUserInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_btnOrder
            // 
            this.m_btnOrder.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.m_btnOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.m_btnOrder.Location = new System.Drawing.Point(27, 243);
            this.m_btnOrder.Name = "m_btnOrder";
            this.m_btnOrder.Size = new System.Drawing.Size(306, 93);
            this.m_btnOrder.TabIndex = 3;
            this.m_btnOrder.Text = "Orders";
            this.m_btnOrder.UseVisualStyleBackColor = false;
            this.m_btnOrder.Click += new System.EventHandler(this.m_btnOrder_Click);
            this.m_btnOrder.MouseLeave += new System.EventHandler(this.m_btnOrder_MouseLeave);
            this.m_btnOrder.MouseHover += new System.EventHandler(this.m_btnOrder_MouseHover);
            // 
            // btnMove
            // 
            this.btnMove.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnMove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnMove.Location = new System.Drawing.Point(38, 41);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(308, 92);
            this.btnMove.TabIndex = 6;
            this.btnMove.Text = "Move products";
            this.btnMove.UseVisualStyleBackColor = false;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            this.btnMove.MouseLeave += new System.EventHandler(this.btnMove_MouseLeave);
            this.btnMove.MouseHover += new System.EventHandler(this.btnMove_MouseHover);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            // 
            // txtBarcode
            // 
            this.txtBarcode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtBarcode.Location = new System.Drawing.Point(667, 174);
            this.txtBarcode.MaxLength = 10;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(152, 26);
            this.txtBarcode.TabIndex = 1;
            this.txtBarcode.TextChanged += new System.EventHandler(this.txtBarcode_TextChanged);
            // 
            // chkUser
            // 
            this.chkUser.AutoSize = true;
            this.chkUser.Location = new System.Drawing.Point(835, 175);
            this.chkUser.Name = "chkUser";
            this.chkUser.Size = new System.Drawing.Size(179, 24);
            this.chkUser.TabIndex = 51;
            this.chkUser.Text = "Scan user and close";
            this.chkUser.UseVisualStyleBackColor = true;
            this.chkUser.Visible = false;
            this.chkUser.CheckedChanged += new System.EventHandler(this.chkUser_CheckedChanged);
            this.chkUser.TextChanged += new System.EventHandler(this.txtBarcode_TextChanged);
            this.chkUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.form_KeyPress);
            // 
            // btnRelease
            // 
            this.btnRelease.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnRelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelease.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnRelease.Location = new System.Drawing.Point(114, 709);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(305, 93);
            this.btnRelease.TabIndex = 5;
            this.btnRelease.Text = "Release of products";
            this.btnRelease.UseVisualStyleBackColor = false;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            this.btnRelease.MouseLeave += new System.EventHandler(this.btnRelease_MouseLeave);
            this.btnRelease.MouseHover += new System.EventHandler(this.btnRelease_MouseHover);
            // 
            // btnInventory
            // 
            this.btnInventory.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnInventory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnInventory.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInventory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnInventory.Location = new System.Drawing.Point(40, 142);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(307, 92);
            this.btnInventory.TabIndex = 7;
            this.btnInventory.Text = "Inventory products";
            this.btnInventory.UseVisualStyleBackColor = false;
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            this.btnInventory.MouseLeave += new System.EventHandler(this.btnInventory_MouseLeave);
            this.btnInventory.MouseHover += new System.EventHandler(this.btnInventory_MouseHover);
            // 
            // btnPrintbarcodes
            // 
            this.btnPrintbarcodes.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPrintbarcodes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPrintbarcodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintbarcodes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnPrintbarcodes.Location = new System.Drawing.Point(40, 243);
            this.btnPrintbarcodes.Name = "btnPrintbarcodes";
            this.btnPrintbarcodes.Size = new System.Drawing.Size(307, 93);
            this.btnPrintbarcodes.TabIndex = 8;
            this.btnPrintbarcodes.Text = "Print barcodes";
            this.btnPrintbarcodes.UseVisualStyleBackColor = false;
            this.btnPrintbarcodes.Click += new System.EventHandler(this.btnPrintbarcodes_Click);
            this.btnPrintbarcodes.MouseLeave += new System.EventHandler(this.btnPrintbarcodes_MouseLeave);
            this.btnPrintbarcodes.MouseHover += new System.EventHandler(this.btnPrintbarcodes_MouseHover);
            // 
            // btnPrintLocBarcode
            // 
            this.btnPrintLocBarcode.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPrintLocBarcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPrintLocBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintLocBarcode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnPrintLocBarcode.Location = new System.Drawing.Point(40, 342);
            this.btnPrintLocBarcode.Name = "btnPrintLocBarcode";
            this.btnPrintLocBarcode.Size = new System.Drawing.Size(307, 92);
            this.btnPrintLocBarcode.TabIndex = 9;
            this.btnPrintLocBarcode.Text = "Print location barcodes";
            this.btnPrintLocBarcode.UseVisualStyleBackColor = false;
            this.btnPrintLocBarcode.Click += new System.EventHandler(this.btnPrintLocBarcode_Click);
            this.btnPrintLocBarcode.MouseLeave += new System.EventHandler(this.btnPrintLocBarcode_MouseLeave);
            this.btnPrintLocBarcode.MouseHover += new System.EventHandler(this.btnPrintLocBarcode_MouseHover);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnReset.Location = new System.Drawing.Point(40, 441);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(307, 94);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "Restore inventory";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            this.btnReset.MouseLeave += new System.EventHandler(this.btnReset_MouseLeave);
            this.btnReset.MouseHover += new System.EventHandler(this.btnReset_MouseHover);
            // 
            // grpReg
            // 
            this.grpReg.Controls.Add(this.picBoxOrder);
            this.grpReg.Controls.Add(this.picBoxDiscard);
            this.grpReg.Controls.Add(this.picBoxReg);
            this.grpReg.Controls.Add(this.buttonCheckIn);
            this.grpReg.Controls.Add(this.buttonCheckOut);
            this.grpReg.Controls.Add(this.m_btnOrder);
            this.grpReg.Location = new System.Drawing.Point(86, 227);
            this.grpReg.Name = "grpReg";
            this.grpReg.Size = new System.Drawing.Size(408, 375);
            this.grpReg.TabIndex = 53;
            this.grpReg.TabStop = false;
            this.grpReg.Text = "Register";
            // 
            // picBoxOrder
            // 
            this.picBoxOrder.Image = global::BarcodePcApp.Properties.Resources.instruction3;
            this.picBoxOrder.Location = new System.Drawing.Point(350, 278);
            this.picBoxOrder.Name = "picBoxOrder";
            this.picBoxOrder.Size = new System.Drawing.Size(40, 30);
            this.picBoxOrder.TabIndex = 46;
            this.picBoxOrder.TabStop = false;
            this.toolTip1.SetToolTip(this.picBoxOrder, "Beskrivning Beställning");
            // 
            // picBoxDiscard
            // 
            this.picBoxDiscard.Image = global::BarcodePcApp.Properties.Resources.instruction3;
            this.picBoxDiscard.Location = new System.Drawing.Point(350, 178);
            this.picBoxDiscard.Name = "picBoxDiscard";
            this.picBoxDiscard.Size = new System.Drawing.Size(40, 31);
            this.picBoxDiscard.TabIndex = 45;
            this.picBoxDiscard.TabStop = false;
            this.toolTip1.SetToolTip(this.picBoxDiscard, "Beskrivning Kassera produkter");
            // 
            // picBoxReg
            // 
            this.picBoxReg.BackColor = System.Drawing.Color.Transparent;
            this.picBoxReg.Image = global::BarcodePcApp.Properties.Resources.instruction3;
            this.picBoxReg.Location = new System.Drawing.Point(350, 72);
            this.picBoxReg.Name = "picBoxReg";
            this.picBoxReg.Size = new System.Drawing.Size(40, 29);
            this.picBoxReg.TabIndex = 44;
            this.picBoxReg.TabStop = false;
            this.toolTip1.SetToolTip(this.picBoxReg, "Information - Registrera produkter");
            // 
            // grpInv
            // 
            this.grpInv.Controls.Add(this.picBoxLbarc);
            this.grpInv.Controls.Add(this.picBoxPbar);
            this.grpInv.Controls.Add(this.picBoxInv);
            this.grpInv.Controls.Add(this.picBoxMove);
            this.grpInv.Controls.Add(this.btnMove);
            this.grpInv.Controls.Add(this.btnInventory);
            this.grpInv.Controls.Add(this.btnPrintLocBarcode);
            this.grpInv.Controls.Add(this.btnPrintbarcodes);
            this.grpInv.Controls.Add(this.btnReset);
            this.grpInv.Location = new System.Drawing.Point(526, 227);
            this.grpInv.Name = "grpInv";
            this.grpInv.Size = new System.Drawing.Size(432, 575);
            this.grpInv.TabIndex = 54;
            this.grpInv.TabStop = false;
            this.grpInv.Text = "Inventory";
            // 
            // picBoxLbarc
            // 
            this.picBoxLbarc.Image = global::BarcodePcApp.Properties.Resources.instruction3;
            this.picBoxLbarc.Location = new System.Drawing.Point(366, 367);
            this.picBoxLbarc.Name = "picBoxLbarc";
            this.picBoxLbarc.Size = new System.Drawing.Size(40, 31);
            this.picBoxLbarc.TabIndex = 62;
            this.picBoxLbarc.TabStop = false;
            // 
            // picBoxPbar
            // 
            this.picBoxPbar.Image = global::BarcodePcApp.Properties.Resources.instruction3;
            this.picBoxPbar.Location = new System.Drawing.Point(366, 278);
            this.picBoxPbar.Name = "picBoxPbar";
            this.picBoxPbar.Size = new System.Drawing.Size(40, 30);
            this.picBoxPbar.TabIndex = 61;
            this.picBoxPbar.TabStop = false;
            // 
            // picBoxInv
            // 
            this.picBoxInv.Image = global::BarcodePcApp.Properties.Resources.instruction3;
            this.picBoxInv.Location = new System.Drawing.Point(366, 178);
            this.picBoxInv.Name = "picBoxInv";
            this.picBoxInv.Size = new System.Drawing.Size(40, 31);
            this.picBoxInv.TabIndex = 60;
            this.picBoxInv.TabStop = false;
            // 
            // picBoxMove
            // 
            this.picBoxMove.Image = global::BarcodePcApp.Properties.Resources.instruction3;
            this.picBoxMove.Location = new System.Drawing.Point(366, 72);
            this.picBoxMove.Name = "picBoxMove";
            this.picBoxMove.Size = new System.Drawing.Size(40, 29);
            this.picBoxMove.TabIndex = 59;
            this.picBoxMove.TabStop = false;
            // 
            // picBoxReset
            // 
            this.picBoxReset.Image = global::BarcodePcApp.Properties.Resources.instruction3;
            this.picBoxReset.Location = new System.Drawing.Point(893, 694);
            this.picBoxReset.Name = "picBoxReset";
            this.picBoxReset.Size = new System.Drawing.Size(40, 31);
            this.picBoxReset.TabIndex = 63;
            this.picBoxReset.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(603, 171);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(429, 47);
            this.panel2.TabIndex = 55;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // cboManual
            // 
            this.cboManual.FormattingEnabled = true;
            this.cboManual.Location = new System.Drawing.Point(810, 64);
            this.cboManual.Name = "cboManual";
            this.cboManual.Size = new System.Drawing.Size(228, 28);
            this.cboManual.TabIndex = 57;
            this.cboManual.Text = "Manual";
            this.cboManual.SelectedIndexChanged += new System.EventHandler(this.cboManual_SelectedIndexChanged);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(810, 18);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(176, 39);
            this.btnSettings.TabIndex = 11;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // picBoxOverview
            // 
            this.picBoxOverview.Image = global::BarcodePcApp.Properties.Resources.instruction3;
            this.picBoxOverview.Location = new System.Drawing.Point(437, 646);
            this.picBoxOverview.Name = "picBoxOverview";
            this.picBoxOverview.Size = new System.Drawing.Size(40, 31);
            this.picBoxOverview.TabIndex = 47;
            this.picBoxOverview.TabStop = false;
            // 
            // picBoxRelease
            // 
            this.picBoxRelease.Image = global::BarcodePcApp.Properties.Resources.instruction3;
            this.picBoxRelease.Location = new System.Drawing.Point(437, 734);
            this.picBoxRelease.Name = "picBoxRelease";
            this.picBoxRelease.Size = new System.Drawing.Size(40, 30);
            this.picBoxRelease.TabIndex = 58;
            this.picBoxRelease.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox6.Image = global::BarcodePcApp.Properties.Resources.FAQ1;
            this.pictureBox6.Location = new System.Drawing.Point(994, 18);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(59, 40);
            this.pictureBox6.TabIndex = 59;
            this.pictureBox6.TabStop = false;
            this.pictureBox6.Click += new System.EventHandler(this.pictureBox6_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "Information";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button2.Location = new System.Drawing.Point(32, 127);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(131, 53);
            this.button2.TabIndex = 44;
            this.button2.Text = "Inventory";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(16, 115);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(162, 65);
            this.panel1.TabIndex = 60;
            // 
            // FormMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.CancelButton = this.buttonLogout;
            this.ClientSize = new System.Drawing.Size(1069, 900);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.picBoxReset);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.picBoxRelease);
            this.Controls.Add(this.picBoxOverview);
            this.Controls.Add(this.cboManual);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.grpInv);
            this.Controls.Add(this.chkUser);
            this.Controls.Add(this.grpReg);
            this.Controls.Add(this.btnRelease);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.labelUserInfo);
            this.Controls.Add(this.buttonOverview);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelVerksamhet);
            this.Controls.Add(this.labelKund);
            this.Controls.Add(this.buttonLogout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1320, 1013);
            this.MinimumSize = new System.Drawing.Size(1091, 956);
            this.Name = "FormMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KLARA registration";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.grpReg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxDiscard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxReg)).EndInit();
            this.grpInv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLbarc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxInv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxReset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxOverview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxRelease)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
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
            btnMove.Enabled = false;
            btnInventory.Enabled = false;
            btnPrintbarcodes.Enabled = false;
            btnPrintLocBarcode.Enabled = false;
            btnReset.Enabled = false;


            pcTmpPath = Path.GetTempPath().ToString() + "BarcodePcApp\\";

            cboManual.Items.Add("klara_barcode_v1.26");
            cboManual.Items.Add("klara_barcode_v1.26_eng");
            cboManual.Items.Add("klara_barcode_v1.27");
            cboManual.Items.Add("klara_barcode_v1.27_eng");
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

            buttonLogin_Click(this, e); // Visa logindialogen

            lang = FormMain.Get.LangChange;

            if (lang == true)
            {
                this.label1.Text = "Välkommen till KLARA!";
                this.buttonLogout.Text = "Logga ut";
                this.buttonCheckIn.Text = "Registrera nya produkter";
                this.buttonCheckOut.Text = "Kassera produkter";
                this.buttonOverview.Text = "Produktöversikt";
                this.m_btnOrder.Text = "Ordrar";
                this.btnMove.Text = "Flytta produkter";
                this.btnInventory.Text = "Inventera produkter";
                this.btnPrintbarcodes.Text = "Skriv ut streckkoder";
                this.btnPrintLocBarcode.Text = "Positionsstreckkoder";
                this.btnReset.Text = "Återställ";
                //this.chkUser.Text = "Skanna användare och stäng";
                this.btnRelease.Text = "Frisläppande av produkter";
                this.Text = "KLARA registrering";
                this.btnSettings.Text = "Inställningar";
                this.grpInv.Text = "Flytta / Inventera";
                this.grpReg.Text = "Lägg till / Ta bort";
                this.toolTip1.SetToolTip(this.picBoxReg, "Utskrift av nya etiketter, unika för varje enskild flaska. De registreras som inköp i webbKLARA.");
                this.toolTip1.SetToolTip(this.picBoxDiscard, "Tomma flaskor kasseras och placeras i avdelningens lista över kasserade produkter..");
                this.toolTip1.SetToolTip(this.picBoxOrder, "Utskrift av streckkodsetiketter för kemikalier som tas emot efter att en beställning har lagts och hanterats i webbKLARA.");
                this.toolTip1.SetToolTip(this.picBoxOverview, "De senaste etiketterna som har registrerats eller kasserats visas.");
                this.toolTip1.SetToolTip(this.picBoxRelease, "Funktionen hanterar krav gällande frisläppande av produkter innan streckkoder/produkter kan hanteras korrekt.");
                this.toolTip1.SetToolTip(this.picBoxMove, "Flytta flaskorna till en vald plats. Du får en varning om det kommer från en annan avdelning eller om platsen inte motsvarar produktens egenskaper.");
                this.toolTip1.SetToolTip(this.picBoxInv, "Verifiera ett innehåll i ett rum/skåp. En lista över oskannade objekt kommer att presenteras när det är klart.");
                this.toolTip1.SetToolTip(this.picBoxPbar, "Skapa kopior av etiketter eller generera nya till kemikalieinnehav som sedan tidigare registrerats");
                this.toolTip1.SetToolTip(this.picBoxLbarc, "Skapa en etikett som är unik för en position (skåp eller hylla).");
                this.toolTip1.SetToolTip(this.picBoxReset, "Återställ inventeringsstatus på produkterna i valt hel avdelning.Detta innebär att om en restlista tidigare genererats så kan du efter en återställning få ut en ny." + Environment.NewLine + "Produkter som i mellantiden skapats, flyttats eller kasserats påverkas inte.");
                if(plats == "lokal")
                {
                    this.label1.Text = "Välkommen till KLARA! ( lokal )";
                    this.Text = "KLARA registrering v" + Convert.ToString(Math.Round(((thisPcAppVer / 100.0)), 2)).Replace(",", ".");
                }
                else if(plats == "test")
                {
                    this.label1.Text = "Välkommen till KLARA! ( test )";
                    this.Text = "KLARA registrering v" + Convert.ToString(Math.Round(((thisPcAppVer / 100.0)), 2)).Replace(",", ".");
                }

                else
                {

                }

            }
            else
            {
                this.label1.Text = "Welcome to KLARA!";
                this.buttonLogout.Text = "Log out";
                this.buttonCheckIn.Text = "Register new products";
                this.buttonCheckOut.Text = "Discard products";
                this.buttonOverview.Text = "Product overview";
                this.m_btnOrder.Text = "Orders";
                this.btnMove.Text = "Move products";
                this.btnInventory.Text = "Inventory products";
                this.btnPrintbarcodes.Text = "Reprint barcodes";
                this.btnPrintLocBarcode.Text = "Location barcode";
                this.btnReset.Text = "Restore inventory";
                //this.chkUser.Text = "Scan user and close";
                this.btnRelease.Text = "Release of product";
                this.Text = "KLARA registration";
                m_Main.btnSettings.Text = "Settings";
                this.grpInv.Text = "Move / Inventory";
                this.grpReg.Text = "Add / Remove";
                this.toolTip1.SetToolTip(this.picBoxReg, "Printing new labels, unique to each individual bottle. They are recorded as purchases in webKLARA.");
                this.toolTip1.SetToolTip(this.picBoxDiscard, "Empty bottles are discarded and placed in the department’s list of discarded products.");
                this.toolTip1.SetToolTip(this.picBoxOrder, "Printing of barcode labels for chemicals received after an order has been placed and handled in the webKLARA.");
                this.toolTip1.SetToolTip(this.picBoxOverview, "The latest labels which have been registered or discarded are shown.");
                this.toolTip1.SetToolTip(this.picBoxRelease, "The function handles requirements regarding release of products before barcodes/products can be handled properly.");
                this.toolTip1.SetToolTip(this.picBoxMove, "Move bottles to a selected location. You will get a warning if it originates from another department or if the location does not correspond to the products characteristics.");
                this.toolTip1.SetToolTip(this.picBoxInv, "Verifying a content in a room/cabinet. A list of un-scanned items will be presented when done.");
                this.toolTip1.SetToolTip(this.picBoxPbar, "Create copies of labels or generate new ones for chemical that have previously been registered in the webKLARA.");
                this.toolTip1.SetToolTip(this.picBoxLbarc, "Generate a label unique to a position (cabinet or shelf).");
                this.toolTip1.SetToolTip(this.picBoxReset, "Restore inventory status of the products in a selected, entire department. This means that if a residual list has previously been generated, you can get a new one after a reset." + Environment.NewLine + "Products that have been created, moved or discarded in the meantime are not affected.");
                if (plats == "lokal")
                {
                    this.label1.Text = "Welcome to KLARA! ( local )";
                    this.Text = "KLARA registration v" + Convert.ToString(Math.Round(((thisPcAppVer / 100.0)), 2)).Replace(",", ".");
                }
                else if (plats == "test")
                {
                    this.label1.Text = "Welcome to KLARA! ( test )";
                    this.Text = "KLARA registration v" + Convert.ToString(Math.Round(((thisPcAppVer / 100.0)), 2)).Replace(",", ".");
                }

                else
                {

                }



            }

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
            {
                m_btnOrder.Visible = true;
                m_btnOrder.Enabled = true;
                this.picBoxOrder.Enabled = true;
                this.picBoxOrder.Visible = true;
            }                
            else
            {
                m_btnOrder.Visible = false;
                this.picBoxOrder.Visible = false;
                this.picBoxOrder.Enabled = false;

                //grpReg.Size = new Size(293, 205);
                grpReg.Size = new Size(293, 205);
                //    this.buttonOverview.Location = new System.Drawing.Point(85, 384);
                //    this.btnRelease.Location = new System.Drawing.Point(85, 464);
                //    this.picBoxOverview.Location = new System.Drawing.Point(328, 401);
                //    this.picBoxRelease.Location = new System.Drawing.Point(328, 476);


            }


            if (FormMain.IsAdmin)
            { 
                btnReset.Visible = true;
                btnReset.Enabled = true;
                this.picBoxReset.Enabled = true;
                this.picBoxReset.Visible = true;
                grpInv.Size = new Size(310, 410);
            }

            else
            {
                btnReset.Visible = false;
                //grpInv.Size = new Size(324, 362);
                grpInv.Size = new Size(310, 340);
                //    //this.buttonLogout.Location = new System.Drawing.Point(631,547);21203

                //    //this.labelUserInfo.Location = new System.Drawing.Point(409,552);
                //    this.Size = new Size(680, 600);
                this.picBoxReset.Enabled = false;
                this.picBoxReset.Visible = false;
            }

            if (m_sRelease == "1")
            {
                btnRelease.Visible = true;
                btnRelease.Enabled = true;
            }
            else
            {
                btnRelease.Visible = false;
                btnRelease.Enabled = false;
                this.picBoxRelease.Enabled = false;
                this.picBoxRelease.Visible = false;
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
                btnMove.Enabled = true;
                btnInventory.Enabled = true;
                btnPrintbarcodes.Enabled = true;
                btnPrintLocBarcode.Enabled = true;
                //btnReset.Enabled = true;
            }
            this.Show();
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
            string serverPcAppVer_q = bc.GetNewVersion();
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
               //bc.Url = "https://secure.port.se/ws/127.cfc";  // Test mot skarpa versionen som är inte aktiv
               //bc.Url = "https://secure.port.se/ws/barcode.cfc";  // när anders vill testa skarpt
                //bc.Url = "https://nu.klaratest.port.se/ws/barcode.cfc"; // aktiveras när man vill koppla upp sig mot testserver AQTEST
            }


            if (thisPcAppVer < pcAppVer && !forcedUpdate) //Check if updates are enforced and that there is a new version, if they aren't change the address to the barcode service with the same version number.
            {
                bc.Url = bc.Url.Replace("barcode.cfc", FormMain.thisPcAppVer + ".cfc");
            }

            if ((bc.Url == "http://aq201510/ws/barcode.cfc") || (bc.Url == "http://ny/ws/barcode.cfc"))
            {
                //m_Main.BackColor = Color.Lavender;
                //if(lang == true)
                //{
                //    m_Main.label1.Text = "Välkommen till KLARA! ( lokal )";
                //    m_Main.Text = "KLARA registrering v" + Convert.ToString(Math.Round(((thisPcAppVer / 100.0)), 2)).Replace(",", ".");
                //}
                //else
                //{
                //    //m_Main.label1.Text = "Welcome to KLARA! ( local )";
                //    //m_Main.Text = "KLARA registration v" + Convert.ToString(Math.Round(((thisPcAppVer / 100.0)), 2)).Replace(",", ".");
                //}

                plats = "lokal";
                FormMain.localhost = true;
                //MessageBox.Show(bc.Url);
                //m_Main.label1.Text = bc.Url;


            }
            else if (bc.Url == "https://nu.klaratest.port.se/ws/barcode.cfc")
            {
                //this.BackColor = Color.Cyan;

                //if (lang == true)
                //{
                //    m_Main.label1.Text = "Välkommen till KLARA! ( test )";

                //}
                //else
                //{
                //    m_Main.label1.Text = "Welcome to KLARA! ( test )";
                //}
                plats = "test";
                // MessageBox.Show(bc.Url);

            }
            else if (bc.Url == "https://secure.port.se/ws/127.cfc")
            {
                m_Main.BackColor = Color.Beige;
                plats = "skarpa";
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
            barcode = "";
            //this.Hide();
            //this.Close();

            m_Main.Dispose();
            //Application.Restart();
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

                txtBarcode.Enabled = true;
                txtBarcode.Focus();
            }
            else
            {
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
                if(lang==true)
                {
                    MessageBox.Show("Du har inte inventarieåtkomst till några avdelningar. Om du behöver komma åt inventeringsfunktionen, kontakta din administratör..", "Åtkomst nekad");
                }
                else
                {
                    MessageBox.Show("You do not have inventory access to any deparments. If you need to access to the inventory feature, please contact your administrator.", "Access Denied");
                }
                
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
                if (lang == true)
                {
                    MessageBox.Show("Du har inte inventarieåtkomst till några avdelningar. Om du behöver komma åt inventeringsfunktionen, kontakta din administratör..", "Åtkomst nekad");
                }
                else
                {
                    MessageBox.Show("You do not have inventory access to any deparments. If you need to access to the inventory feature, please contact your administrator.", "Access Denied");
                }
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
                if (lang == true)
                {
                    MessageBox.Show("Du har inte inventarieåtkomst till några avdelningar. Om du behöver komma åt inventeringsfunktionen, kontakta din administratör..", "Åtkomst nekad");
                }
                else
                {
                    MessageBox.Show("You do not have inventory access to any deparments. If you need to access to the inventory feature, please contact your administrator.", "Access Denied");
                }
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
                if (lang == true)
                {
                    MessageBox.Show("Du har inte inventarieåtkomst till några avdelningar. Om du behöver komma åt inventeringsfunktionen, kontakta din administratör..", "Åtkomst nekad");
                }
                else
                {
                    MessageBox.Show("You do not have inventory access to any deparments. If you need to access to the inventory feature, please contact your administrator.", "Access Denied");
                }
                lang = FormMain.Get.LangChange;
                inventory.Dispose();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            string choice = "reset";
            FormNollstall nolls = new FormNollstall(databas,kemiDb,userId, orgAr,verkId);
            if(nolls.ShouldBeShown)
            {                
                nolls.ShowDialog();
            }
            else
            {
                MessageBox.Show("You do not have access to reset.", "Access Denied");
                lang = FormMain.Get.LangChange;
                nolls.Dispose();

            }
            //FormInventory inventory = new FormInventory(verkId, databas, kemiDb, choice);


            //if (inventory.shouldBeShown)
            //{
            //    inventory.ShowDialog(this);

            //}
            //else
            //{
            //    MessageBox.Show("You do not have inventory access to any deparments. If you need to access to the inventory feature, please contact your administrator.", "Access Denied");
            //    lang = FormMain.Get.LangChange;
            //    inventory.Dispose();
            //}
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            FormSettings fi = new FormSettings();
            fi.ShowDialog();
            lang = FormMain.Get.LangChange;

            if (lang == true)
            {
                m_Main.label1.Text = "Välkommen till KLARA!";
                m_Main.buttonLogout.Text = "Logga ut";
                m_Main.buttonCheckIn.Text = "Registrera nya produkter";
                this.buttonCheckOut.Text = "Kassera produkter";
                this.buttonOverview.Text = "Produktöversikt";
                this.m_btnOrder.Text = "Ordrar";
                this.btnMove.Text = "Flytta produkter";
                this.btnInventory.Text = "Inventera produkter";
                this.btnPrintbarcodes.Text = "Skriv ut streckkoder";
                this.btnPrintLocBarcode.Text = "Positionsstreckkoder";
                this.btnReset.Text = "Återställ";
                //this.chkUser.Text = "Skanna användare och stäng";
                this.btnRelease.Text = "Frisläppande av produkter";
                this.Text = "KLARA registrering";
                m_Main.btnSettings.Text = "Inställningar";
                this.grpInv.Text = "Flytta / Inventera";
                this.grpReg.Text = "Lägg till / Ta bort";
                this.toolTip1.SetToolTip(this.picBoxReg, "Utskrift av nya etiketter, unika för varje enskild flaska. De registreras som inköp i webbKLARA.");
                this.toolTip1.SetToolTip(this.picBoxDiscard, "Tomma flaskor kasseras och placeras i avdelningens lista över kasserade produkter..");
                this.toolTip1.SetToolTip(this.picBoxOrder, "Utskrift av streckkodsetiketter för kemikalier som tas emot efter att en beställning har lagts och hanterats i webbKLARA.");
                this.toolTip1.SetToolTip(this.picBoxOverview, "De senaste etiketterna som har registrerats eller kasserats visas.");
                this.toolTip1.SetToolTip(this.picBoxRelease, "Funktionen hanterar krav gällande frisläppande av produkter innan streckkoder/produkter kan hanteras korrekt.");
                this.toolTip1.SetToolTip(this.picBoxMove, "Flytta flaskorna till en vald plats. Du får en varning om det kommer från en annan avdelning eller om platsen inte motsvarar produktens egenskaper.");
                this.toolTip1.SetToolTip(this.picBoxInv, "Verifiera ett innehåll i ett rum/skåp. En lista över oskannade objekt kommer att presenteras när det är klart.");
                this.toolTip1.SetToolTip(this.picBoxPbar, "Skapa kopior av etiketter eller generera nya till kemikalieinnehav som sedan tidigare registrerats");
                this.toolTip1.SetToolTip(this.picBoxLbarc, "Skapa en etikett som är unik för en position (skåp eller hylla).");
                this.toolTip1.SetToolTip(this.picBoxReset, "Återställ inventeringsstatus på produkterna i valt hel avdelning.Detta innebär att om en restlista tidigare genererats så kan du efter en återställning få ut en ny."+Environment.NewLine+"Produkter som i mellantiden skapats, flyttats eller kasserats påverkas inte.");
                if (plats == "lokal")
                {
                    this.label1.Text = "Välkommen till KLARA! ( lokal )";
                    this.Text = "KLARA registrering v" + Convert.ToString(Math.Round(((thisPcAppVer / 100.0)), 2)).Replace(",", ".");
                }
                else if (plats == "test")
                {
                    this.label1.Text = "Välkommen till KLARA! ( test )";
                    this.Text = "KLARA registrering v" + Convert.ToString(Math.Round(((thisPcAppVer / 100.0)), 2)).Replace(",", ".");
                }

                else
                {

                }
            }
            else
            {
                m_Main.label1.Text = "Welcome to KLARA!";
                m_Main.buttonLogout.Text = "Log out";
                m_Main.buttonCheckIn.Text = "Register new products";
                this.buttonCheckOut.Text = "Discard products";
                this.buttonOverview.Text = "Product overview";
                this.m_btnOrder.Text = "Orders";
                this.btnMove.Text = "Move products";
                this.btnInventory.Text = "Inventory products";
                this.btnPrintbarcodes.Text = "Reprint barcodes";
                this.btnPrintLocBarcode.Text = "Location barcode";
                this.btnReset.Text = "Restore inventory";
                //this.chkUser.Text = "Scan user and close";
                this.btnRelease.Text = "Release of product";
                this.Text = "KLARA registration";
                m_Main.btnSettings.Text = "Settings";
                this.grpInv.Text = "Move / Inventory";
                this.grpReg.Text = "Add / Remove";
                this.toolTip1.SetToolTip(this.picBoxReg, "Printing new labels, unique to each individual bottle. They are recorded as purchases in webKLARA.");
                this.toolTip1.SetToolTip(this.picBoxDiscard, "Empty bottles are discarded and placed in the department’s list of discarded products.");
                this.toolTip1.SetToolTip(this.picBoxOrder, "Printing of barcode labels for chemicals received after an order has been placed and handled in the webKLARA.");
                this.toolTip1.SetToolTip(this.picBoxOverview, "The latest labels which have been registered or discarded are shown.");
                this.toolTip1.SetToolTip(this.picBoxRelease, "The function handles requirements regarding release of products before barcodes/products can be handled properly.");
                this.toolTip1.SetToolTip(this.picBoxMove, "Move bottles to a selected location. You will get a warning if it originates from another department or if the location does not correspond to the products characteristics.");
                this.toolTip1.SetToolTip(this.picBoxInv, "Verifying a content in a room/cabinet. A list of un-scanned items will be presented when done.");
                this.toolTip1.SetToolTip(this.picBoxPbar, "Create copies of labels or generate new ones for chemical that have previously been registered in the webKLARA.");
                this.toolTip1.SetToolTip(this.picBoxLbarc, "Generate a label unique to a position (cabinet or shelf).");
                this.toolTip1.SetToolTip(this.picBoxReset, "Restore inventory status of the products in a selected, entire department. This means that if a residual list has previously been generated, you can get a new one after a reset." + Environment.NewLine + "Products that have been created, moved or discarded in the meantime are not affected.");
                if (plats == "lokal")
                {
                    this.label1.Text = "Welcome to KLARA! ( local )";
                    this.Text = "KLARA registration v" + Convert.ToString(Math.Round(((thisPcAppVer / 100.0)), 2)).Replace(",", ".");
                }
                else if (plats == "test")
                {
                    this.label1.Text = "Welcome to KLARA! ( test )";
                    this.Text = "KLARA registration v" + Convert.ToString(Math.Round(((thisPcAppVer / 100.0)), 2)).Replace(",", ".");
                }

                else
                {

                }
            }

        }

        private void cboManual_SelectedIndexChanged(object sender, EventArgs e)
        {
            string longurl = "";
            if (cboManual.SelectedIndex == 0)
            {
                longurl = "https://secure.port.se/dokument/Manualer/lathund_klara_barcode_v1.26.pdf";
            }
            else if (cboManual.SelectedIndex == 1)
            {
                 longurl = "https://secure.port.se/dokument/Manualer/lathund_klara_barcode_v1.26_eng.pdf";
            }
            else if (cboManual.SelectedIndex ==2)
            {
                longurl = "https://secure.port.se/dokument/Manualer/lathund_klara_barcode_v1.27.pdf";
            }

            else if (cboManual.SelectedIndex == 3)
            {               
                longurl = "https://secure.port.se/dokument/Manualer/lathund_klara_barcode_v1.27_eng.pdf";
            }
            System.Diagnostics.Process.Start(longurl);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            string longurl = "";

                if (lang == true)
                {
                    longurl = "https://secure.port.se/dokument/FAQ/FAQ KLARA Barcode_C_SWE.pdf";
                }
                else
                {
                    longurl = "https://secure.port.se/dokument/FAQ/FAQ KLARA Barcode_C_ENG.pdf";
                }       
                                     
            System.Diagnostics.Process.Start(longurl);
        }

        private void buttonCheckIn_MouseHover(object sender, EventArgs e)
        {
            buttonCheckIn.BackColor = System.Drawing.SystemColors.ControlDark;
        }

        private void buttonCheckIn_MouseLeave(object sender, EventArgs e)
        {
            buttonCheckIn.BackColor = System.Drawing.SystemColors.ControlLight;
        }

        private void buttonCheckOut_MouseHover(object sender, EventArgs e)
        {
            buttonCheckOut.BackColor = System.Drawing.SystemColors.ControlDark;
        }

        private void buttonCheckOut_MouseLeave(object sender, EventArgs e)
        {
            buttonCheckOut.BackColor = System.Drawing.SystemColors.ControlLight;
        }

        private void m_btnOrder_MouseHover(object sender, EventArgs e)
        {
            m_btnOrder.BackColor = System.Drawing.SystemColors.ControlDark;
        }

        private void m_btnOrder_MouseLeave(object sender, EventArgs e)
        {
            m_btnOrder.BackColor = System.Drawing.SystemColors.ControlLight;
        }

        private void buttonOverview_MouseHover(object sender, EventArgs e)
        {
            buttonOverview.BackColor = System.Drawing.SystemColors.ControlDark;
        }

        private void buttonOverview_MouseLeave(object sender, EventArgs e)
        {
            buttonOverview.BackColor = System.Drawing.SystemColors.ControlLight;
        }

        private void btnRelease_MouseHover(object sender, EventArgs e)
        {
            btnRelease.BackColor = System.Drawing.SystemColors.ControlDark;
        }

        private void btnRelease_MouseLeave(object sender, EventArgs e)
        {
            btnRelease.BackColor = System.Drawing.SystemColors.ControlLight;
        }

        private void btnMove_MouseHover(object sender, EventArgs e)
        {
            btnMove.BackColor = System.Drawing.SystemColors.ControlDark;
        }

        private void btnMove_MouseLeave(object sender, EventArgs e)
        {
            btnMove.BackColor = System.Drawing.SystemColors.ControlLight;
        }

        private void btnInventory_MouseHover(object sender, EventArgs e)
        {
            btnInventory.BackColor = System.Drawing.SystemColors.ControlDark;
        }

        private void btnInventory_MouseLeave(object sender, EventArgs e)
        {
            btnInventory.BackColor = System.Drawing.SystemColors.ControlLight;
        }

        private void btnPrintbarcodes_MouseHover(object sender, EventArgs e)
        {
            btnPrintbarcodes.BackColor = System.Drawing.SystemColors.ControlDark;
        }

        private void btnPrintbarcodes_MouseLeave(object sender, EventArgs e)
        {
            btnPrintbarcodes.BackColor = System.Drawing.SystemColors.ControlLight;
        }

        private void btnPrintLocBarcode_MouseHover(object sender, EventArgs e)
        {
            btnPrintLocBarcode.BackColor = System.Drawing.SystemColors.ControlDark;
        }

        private void btnPrintLocBarcode_MouseLeave(object sender, EventArgs e)
        {
            btnPrintLocBarcode.BackColor = System.Drawing.SystemColors.ControlLight;
        }

        private void btnReset_MouseHover(object sender, EventArgs e)
        {
            btnReset.BackColor = System.Drawing.SystemColors.ControlDark;
        }

        private void btnReset_MouseLeave(object sender, EventArgs e)
        {
            btnReset.BackColor = System.Drawing.SystemColors.ControlLight;
        }
    }
}