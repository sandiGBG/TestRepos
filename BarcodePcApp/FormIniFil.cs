using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using System.IO;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace BarcodePcApp
{
	/// <summary>
	/// Summary description for FormIniFil.
	/// </summary>
	public class FormIniFil : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button buttonSpara;
		private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label3;

        private string m_sName = "";
        private int m_nBarcodeWidth = FormMain.Get.BarcodeWidth;
        private int m_nBarcodeHeight = FormMain.Get.BarcodeHeight;
        private int m_nBarcodeLeft = FormMain.Get.BarcodeLeft;
        private int m_nBarcodeTop = FormMain.Get.BarcodeTop;
        private string m_sBarcodePrinter = FormMain.Get.BarcodePrinter;
        private CheckBox forceUpdateCheckBox;
        private CheckBox LoginBarcodeCheckBox;
        private CheckBox roomBarcodeCheckBox;
        private CheckBox PropertyCheckBox;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormIniFil()	{
			InitializeComponent();
		}

       
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing )	{
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIniFil));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonSpara = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.forceUpdateCheckBox = new System.Windows.Forms.CheckBox();
            this.LoginBarcodeCheckBox = new System.Windows.Forms.CheckBox();
            this.roomBarcodeCheckBox = new System.Windows.Forms.CheckBox();
            this.PropertyCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nuvarande databas:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(156, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(218, 22);
            this.textBox1.TabIndex = 2;
            // 
            // buttonSpara
            // 
            this.buttonSpara.Location = new System.Drawing.Point(98, 597);
            this.buttonSpara.Name = "buttonSpara";
            this.buttonSpara.Size = new System.Drawing.Size(96, 26);
            this.buttonSpara.TabIndex = 4;
            this.buttonSpara.Text = "Create";
            this.buttonSpara.Click += new System.EventHandler(this.buttonSpara_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(202, 597);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(96, 26);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // listBox1
            // 
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(19, 77);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(355, 356);
            this.listBox1.TabIndex = 8;
            this.listBox1.Click += new System.EventHandler(this.listBox1_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(19, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(279, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "Välj först databas här:";
            // 
            // forceUpdateCheckBox
            // 
            this.forceUpdateCheckBox.AutoSize = true;
            this.forceUpdateCheckBox.Checked = true;
            this.forceUpdateCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.forceUpdateCheckBox.Location = new System.Drawing.Point(23, 479);
            this.forceUpdateCheckBox.Name = "forceUpdateCheckBox";
            this.forceUpdateCheckBox.Size = new System.Drawing.Size(129, 21);
            this.forceUpdateCheckBox.TabIndex = 11;
            this.forceUpdateCheckBox.Text = "Forced updates";
            this.forceUpdateCheckBox.UseVisualStyleBackColor = true;
            // 
            // LoginBarcodeCheckBox
            // 
            this.LoginBarcodeCheckBox.AutoSize = true;
            this.LoginBarcodeCheckBox.Location = new System.Drawing.Point(23, 505);
            this.LoginBarcodeCheckBox.Name = "LoginBarcodeCheckBox";
            this.LoginBarcodeCheckBox.Size = new System.Drawing.Size(164, 21);
            this.LoginBarcodeCheckBox.TabIndex = 12;
            this.LoginBarcodeCheckBox.Text = "Enable login barcode";
            this.LoginBarcodeCheckBox.UseVisualStyleBackColor = true;
            // 
            // roomBarcodeCheckBox
            // 
            this.roomBarcodeCheckBox.AutoSize = true;
            this.roomBarcodeCheckBox.Location = new System.Drawing.Point(23, 532);
            this.roomBarcodeCheckBox.Name = "roomBarcodeCheckBox";
            this.roomBarcodeCheckBox.Size = new System.Drawing.Size(173, 21);
            this.roomBarcodeCheckBox.TabIndex = 13;
            this.roomBarcodeCheckBox.Text = "Enable room barcodes";
            this.roomBarcodeCheckBox.UseVisualStyleBackColor = true;
            // 
            // PropertyCheckBox
            // 
            this.PropertyCheckBox.AutoSize = true;
            this.PropertyCheckBox.Location = new System.Drawing.Point(23, 558);
            this.PropertyCheckBox.Name = "PropertyCheckBox";
            this.PropertyCheckBox.Size = new System.Drawing.Size(172, 21);
            this.PropertyCheckBox.TabIndex = 14;
            this.PropertyCheckBox.Text = "Enable property check";
            this.PropertyCheckBox.UseVisualStyleBackColor = true;
            // 
            // FormIniFil
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(422, 637);
            this.ControlBox = false;
            this.Controls.Add(this.PropertyCheckBox);
            this.Controls.Add(this.roomBarcodeCheckBox);
            this.Controls.Add(this.LoginBarcodeCheckBox);
            this.Controls.Add(this.forceUpdateCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonSpara);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormIniFil";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Klara - Systemadministration";
            this.Load += new System.EventHandler(this.FormIniFil_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void FormIniFil_Load(object sender, System.EventArgs e) {
			try {
                StreamReader sre = new StreamReader(FormMain.iniFilePath);
				char[] sepChr= new char[1]{'|'};
				string iniRad;
				string[] iniKol;
				while ((iniRad= sre.ReadLine()) != null) {
					iniKol= iniRad.Split(sepChr);
					switch(iniKol[0].Trim()) {
						case "databas" :
							textBox1.Text= iniKol[1].Trim();
							break;
                        case "name":
                            m_sName = iniKol[1].Trim();
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
                    }
				}
				sre.Close();
			}
			catch {
				textBox1.Text= "-";
			}

            barcode.BarcodeService bc = FormMain.getBarcodeService();
            string dbString = "";
			try {
				dbString= bc.GetDBs();
			}
			catch (Exception err) {
				MessageBox.Show("Detta felmedelande rapporterades:\r\n\n" + err.Message,"GetDBs" + " ADRESS: " + bc.Url);
				bc.Dispose();
				return;
			}

			DataSet ds = new DataSet();
			StringReader sr = new StringReader(dbString);
			ds.ReadXml(sr);
			bc.Dispose();

			DataTable dt = ds.Tables["GetDBs"];

			listBox1.DataSource= ds.Tables["Db"];
			listBox1.DisplayMember= "VisaStr";
			listBox1.ValueMember= "DbName";
		}

		private void listBox1_Click(object sender, System.EventArgs e) {
		}

		private void buttonSpara_Click(object sender, System.EventArgs e) {
			if(listBox1.SelectedIndex < 0) {
				MessageBox.Show("Välj först databas.");
				return;
			}

            string chk = "Databas: " + listBox1.SelectedValue.ToString().ToUpper() + " Verkid: "; // Sträng att kryptera

			Misc.SymCryptography cryptic = new Misc.SymCryptography();
			cryptic.Key = "wqdj~yriu!@*k0_^fa7431%p$#=@hd+&";
			chk = cryptic.Encrypt(chk);

            //FormMain.m_sAppDataPath 
            StreamWriter sw = new StreamWriter(FormMain.iniFilePath);
            sw.WriteLine("databas | " + listBox1.SelectedValue.ToString().ToUpper());
            string sName = listBox1.Text.Substring(listBox1.Text.IndexOf("-")+2);
            sw.WriteLine("name | " + sName);
            sw.WriteLine("barcode_printer | " + m_sBarcodePrinter);
            sw.WriteLine("barcode_width | " + 26);
            sw.WriteLine("barcode_height | " + 12);
            sw.WriteLine("barcode_left | " + m_nBarcodeLeft.ToString());
            sw.WriteLine("barcode_top | " + m_nBarcodeTop.ToString());
            sw.WriteLine("barcode_layout | " + 3);

            sw.WriteLine("forced_update | " + forceUpdateCheckBox.Checked.ToString());
            
            if (LoginBarcodeCheckBox.Checked)
            {
                //sw.WriteLine("login_barcode | False");
                sw.WriteLine("login_barcode |" + LoginBarcodeCheckBox.Checked.ToString());
            }

            if(roomBarcodeCheckBox.Checked)
            {
            sw.WriteLine("scan_rooms | " + roomBarcodeCheckBox.Checked.ToString());
            }

            if (PropertyCheckBox.Checked)
            {
                sw.WriteLine("property_check | " + PropertyCheckBox.Checked.ToString());
            }


            sw.WriteLine("check | " + chk);

			sw.Close();
			MessageBox.Show("Inställningar sparade. \n\nProgrammet måste startas om.");
			Application.Exit();
		}

		private void buttonCancel_Click(object sender, System.EventArgs e) {
			MessageBox.Show("Åtgärden avbruten. \n\nProgrammet måste startas om.");
			Application.Exit();
		}

	}
}
