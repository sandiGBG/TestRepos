namespace BarcodePcApp
{
    partial class FormSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.m_textPrintername = new System.Windows.Forms.TextBox();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.m_btnRestore = new System.Windows.Forms.Button();
            this.m_btnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.m_textBarcodeWidth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_textBarcodeTop = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_textBarcodeLeft = new System.Windows.Forms.TextBox();
            this.m_textBarcodeHeight = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_cbLayout = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.m_btnShowPrinter = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxDialog = new System.Windows.Forms.CheckBox();
            this.BarcodeGroupBox = new System.Windows.Forms.GroupBox();
            this.LoginBarcodeAsDefaultCheckBox = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.loginBarcodeTextBox = new System.Windows.Forms.TextBox();
            this.chkProperty = new System.Windows.Forms.CheckBox();
            this.PropertyGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.BarcodeGroupBox.SuspendLayout();
            this.PropertyGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_textPrintername
            // 
            this.m_textPrintername.Location = new System.Drawing.Point(93, 15);
            this.m_textPrintername.Name = "m_textPrintername";
            this.m_textPrintername.Size = new System.Drawing.Size(225, 20);
            this.m_textPrintername.TabIndex = 1;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Location = new System.Drawing.Point(165, 482);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(97, 23);
            this.m_btnCancel.TabIndex = 6;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Printer name";
            // 
            // m_btnRestore
            // 
            this.m_btnRestore.Location = new System.Drawing.Point(64, 452);
            this.m_btnRestore.Name = "m_btnRestore";
            this.m_btnRestore.Size = new System.Drawing.Size(198, 23);
            this.m_btnRestore.TabIndex = 4;
            this.m_btnRestore.Text = "Restore default settings";
            this.m_btnRestore.UseVisualStyleBackColor = true;
            this.m_btnRestore.Click += new System.EventHandler(this.m_btnRestore_Click);
            // 
            // m_btnOK
            // 
            this.m_btnOK.Location = new System.Drawing.Point(64, 482);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(97, 23);
            this.m_btnOK.TabIndex = 5;
            this.m_btnOK.Text = "Save";
            this.m_btnOK.UseVisualStyleBackColor = true;
            this.m_btnOK.Click += new System.EventHandler(this.m_btnOK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Paper size (w/h)";
            // 
            // m_textBarcodeWidth
            // 
            this.m_textBarcodeWidth.Location = new System.Drawing.Point(152, 19);
            this.m_textBarcodeWidth.Name = "m_textBarcodeWidth";
            this.m_textBarcodeWidth.Size = new System.Drawing.Size(31, 20);
            this.m_textBarcodeWidth.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Top margin (0=auto)";
            // 
            // m_textBarcodeTop
            // 
            this.m_textBarcodeTop.Location = new System.Drawing.Point(152, 45);
            this.m_textBarcodeTop.Name = "m_textBarcodeTop";
            this.m_textBarcodeTop.Size = new System.Drawing.Size(100, 20);
            this.m_textBarcodeTop.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Left margin (0=auto)";
            // 
            // m_textBarcodeLeft
            // 
            this.m_textBarcodeLeft.Location = new System.Drawing.Point(152, 71);
            this.m_textBarcodeLeft.Name = "m_textBarcodeLeft";
            this.m_textBarcodeLeft.Size = new System.Drawing.Size(100, 20);
            this.m_textBarcodeLeft.TabIndex = 9;
            // 
            // m_textBarcodeHeight
            // 
            this.m_textBarcodeHeight.Location = new System.Drawing.Point(220, 19);
            this.m_textBarcodeHeight.Name = "m_textBarcodeHeight";
            this.m_textBarcodeHeight.Size = new System.Drawing.Size(32, 20);
            this.m_textBarcodeHeight.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(183, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "mm";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(253, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "mm";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(253, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "mm";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(253, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "mm";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_cbLayout);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.m_textBarcodeWidth);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.m_textBarcodeTop);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.m_textBarcodeLeft);
            this.groupBox1.Controls.Add(this.m_textBarcodeHeight);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(20, 44);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(298, 141);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Barcode";
            // 
            // m_cbLayout
            // 
            this.m_cbLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbLayout.FormattingEnabled = true;
            this.m_cbLayout.Items.AddRange(new object[] {
            "Layout 1",
            "Layout 2",
            "Layout 3",
            "Layout 4",
            "Layout 5",
            "Layout 6"});
            this.m_cbLayout.Location = new System.Drawing.Point(152, 98);
            this.m_cbLayout.Name = "m_cbLayout";
            this.m_cbLayout.Size = new System.Drawing.Size(100, 21);
            this.m_cbLayout.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 102);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Layout";
            // 
            // m_btnShowPrinter
            // 
            this.m_btnShowPrinter.Location = new System.Drawing.Point(64, 423);
            this.m_btnShowPrinter.Name = "m_btnShowPrinter";
            this.m_btnShowPrinter.Size = new System.Drawing.Size(198, 23);
            this.m_btnShowPrinter.TabIndex = 3;
            this.m_btnShowPrinter.Text = "Show printer settings";
            this.m_btnShowPrinter.UseVisualStyleBackColor = true;
            this.m_btnShowPrinter.Click += new System.EventHandler(this.m_btnShowPrinter_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxDialog);
            this.groupBox2.Location = new System.Drawing.Point(20, 190);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(298, 49);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Inventory";
            // 
            // checkBoxDialog
            // 
            this.checkBoxDialog.AutoSize = true;
            this.checkBoxDialog.Location = new System.Drawing.Point(19, 19);
            this.checkBoxDialog.Name = "checkBoxDialog";
            this.checkBoxDialog.Size = new System.Drawing.Size(226, 17);
            this.checkBoxDialog.TabIndex = 0;
            this.checkBoxDialog.Text = "Dialog when transferring within department";
            this.checkBoxDialog.UseVisualStyleBackColor = true;
            // 
            // BarcodeGroupBox
            // 
            this.BarcodeGroupBox.Controls.Add(this.LoginBarcodeAsDefaultCheckBox);
            this.BarcodeGroupBox.Controls.Add(this.label10);
            this.BarcodeGroupBox.Controls.Add(this.loginBarcodeTextBox);
            this.BarcodeGroupBox.Enabled = false;
            this.BarcodeGroupBox.Location = new System.Drawing.Point(20, 320);
            this.BarcodeGroupBox.Name = "BarcodeGroupBox";
            this.BarcodeGroupBox.Size = new System.Drawing.Size(298, 97);
            this.BarcodeGroupBox.TabIndex = 8;
            this.BarcodeGroupBox.TabStop = false;
            this.BarcodeGroupBox.Text = "Change login barcode";
            // 
            // LoginBarcodeAsDefaultCheckBox
            // 
            this.LoginBarcodeAsDefaultCheckBox.AutoSize = true;
            this.LoginBarcodeAsDefaultCheckBox.Enabled = false;
            this.LoginBarcodeAsDefaultCheckBox.Location = new System.Drawing.Point(19, 62);
            this.LoginBarcodeAsDefaultCheckBox.Name = "LoginBarcodeAsDefaultCheckBox";
            this.LoginBarcodeAsDefaultCheckBox.Size = new System.Drawing.Size(176, 17);
            this.LoginBarcodeAsDefaultCheckBox.TabIndex = 18;
            this.LoginBarcodeAsDefaultCheckBox.Text = "Use barcode as default for login";
            this.LoginBarcodeAsDefaultCheckBox.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Login barcode";
            // 
            // loginBarcodeTextBox
            // 
            this.loginBarcodeTextBox.Enabled = false;
            this.loginBarcodeTextBox.Location = new System.Drawing.Point(118, 23);
            this.loginBarcodeTextBox.Name = "loginBarcodeTextBox";
            this.loginBarcodeTextBox.Size = new System.Drawing.Size(155, 20);
            this.loginBarcodeTextBox.TabIndex = 2;
            // 
            // chkProperty
            // 
            this.chkProperty.AutoSize = true;
            this.chkProperty.Location = new System.Drawing.Point(19, 19);
            this.chkProperty.Name = "chkProperty";
            this.chkProperty.Size = new System.Drawing.Size(99, 17);
            this.chkProperty.TabIndex = 0;
            this.chkProperty.Text = "Property Check";
            this.chkProperty.UseVisualStyleBackColor = true;
            this.chkProperty.CheckedChanged += new System.EventHandler(this.chkProperty_CheckedChanged);
            // 
            // PropertyGroupBox
            // 
            this.PropertyGroupBox.Controls.Add(this.chkProperty);
            this.PropertyGroupBox.Location = new System.Drawing.Point(23, 245);
            this.PropertyGroupBox.Name = "PropertyGroupBox";
            this.PropertyGroupBox.Size = new System.Drawing.Size(298, 49);
            this.PropertyGroupBox.TabIndex = 8;
            this.PropertyGroupBox.TabStop = false;
            this.PropertyGroupBox.Text = "Property";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 563);
            this.Controls.Add(this.PropertyGroupBox);
            this.Controls.Add(this.BarcodeGroupBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.m_btnShowPrinter);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_btnOK);
            this.Controls.Add(this.m_btnRestore);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_textPrintername);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Barcode settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.BarcodeGroupBox.ResumeLayout(false);
            this.BarcodeGroupBox.PerformLayout();
            this.PropertyGroupBox.ResumeLayout(false);
            this.PropertyGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_textPrintername;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button m_btnRestore;
        private System.Windows.Forms.Button m_btnOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_textBarcodeWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_textBarcodeTop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_textBarcodeLeft;
        private System.Windows.Forms.TextBox m_textBarcodeHeight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button m_btnShowPrinter;
        private System.Windows.Forms.ComboBox m_cbLayout;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxDialog;
        private System.Windows.Forms.GroupBox BarcodeGroupBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox loginBarcodeTextBox;
        private System.Windows.Forms.CheckBox LoginBarcodeAsDefaultCheckBox;
        private System.Windows.Forms.CheckBox chkProperty;
        private System.Windows.Forms.GroupBox PropertyGroupBox;
    }
}