namespace BarcodePcApp
{
    partial class FormPrintBarcodes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrintBarcodes));
            this.contentPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.printButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.printCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.barcodeObject = new Neodynamic.WinControls.BarcodeProfessional.BarcodeProfessional();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.chkUser = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.printGenerateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // contentPanel
            // 
            this.contentPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contentPanel.AutoScroll = true;
            this.contentPanel.Location = new System.Drawing.Point(24, 36);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(777, 357);
            this.contentPanel.TabIndex = 0;
            // 
            // printButton
            // 
            this.printButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.printButton.Location = new System.Drawing.Point(629, 408);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(88, 23);
            this.printButton.TabIndex = 1;
            this.printButton.Text = "Print Barcodes";
            this.printButton.UseVisualStyleBackColor = true;
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(723, 408);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // printCheckBox
            // 
            this.printCheckBox.AutoSize = true;
            this.printCheckBox.Location = new System.Drawing.Point(24, 16);
            this.printCheckBox.Name = "printCheckBox";
            this.printCheckBox.Size = new System.Drawing.Size(15, 14);
            this.printCheckBox.TabIndex = 3;
            this.printCheckBox.UseVisualStyleBackColor = true;
            this.printCheckBox.CheckedChanged += new System.EventHandler(this.printCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Barcode";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(138, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Product";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(626, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Amount";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(730, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Note";
            // 
            // barcodeObject
            // 
            this.barcodeObject.AztecCodeModuleSize = 5.2087864363864783E+26D;
            this.barcodeObject.BackColor = System.Drawing.Color.White;
            this.barcodeObject.BarcodePadding = new Neodynamic.WinControls.BarcodeProfessional.Margin(0D, 0D, 0D, 0D);
            this.barcodeObject.BarcodeUnit = Neodynamic.WinControls.BarcodeProfessional.BarcodeUnit.Millimeter;
            this.barcodeObject.BarHeight = 4D;
            this.barcodeObject.BarRatio = 2D;
            this.barcodeObject.BarWidth = 0.23D;
            this.barcodeObject.BarWidthAdjustment = 0D;
            this.barcodeObject.BearerBarWidth = 1.27D;
            this.barcodeObject.BorderWidth = 0D;
            this.barcodeObject.Code = "1234567890";
            this.barcodeObject.DataMatrixModuleSize = 1.059D;
            this.barcodeObject.EanUpcSupplementSeparation = 3.81D;
            this.barcodeObject.EanUpcSupplementTopMargin = 3.81D;
            this.barcodeObject.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.barcodeObject.GuardBarHeight = 6.35D;
            this.barcodeObject.IsbnSupplementCode = "0";
            this.barcodeObject.Location = new System.Drawing.Point(301, 398);
            this.barcodeObject.Margin = new System.Windows.Forms.Padding(2);
            this.barcodeObject.MICRCharHeight = 2.972D;
            this.barcodeObject.Name = "barcodeObject";
            this.barcodeObject.Pdf417AspectRatio = 0D;
            this.barcodeObject.PharmacodeBarsSpacing = 1.059D;
            this.barcodeObject.PharmacodeThickBarWidth = 1.588D;
            this.barcodeObject.PharmacodeThinBarWidth = 0.528D;
            this.barcodeObject.PlanetHeightShortBar = 2.54D;
            this.barcodeObject.PlanetHeightTallBar = 5.08D;
            this.barcodeObject.Postal4StateBarsSpacing = 0.795D;
            this.barcodeObject.Postal4StateTrackerBarHeight = 2.032D;
            this.barcodeObject.Postal4StateTrackerBarWidth = 0.528D;
            this.barcodeObject.PostnetHeightShortBar = 2.54D;
            this.barcodeObject.PostnetHeightTallBar = 5.08D;
            this.barcodeObject.QRCodeModuleSize = 1.059D;
            this.barcodeObject.QuietZoneWidth = 0.5D;
            this.barcodeObject.Size = new System.Drawing.Size(83, 25);
            this.barcodeObject.Symbology = Neodynamic.WinControls.BarcodeProfessional.Symbology.Code128;
            this.barcodeObject.TabIndex = 26;
            this.barcodeObject.Tag = "";
            this.barcodeObject.TextFont = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.barcodeObject.TextForeColor = System.Drawing.Color.Black;
            this.barcodeObject.TiffCompression = Neodynamic.WinControls.BarcodeProfessional.TiffCompression.LZW;
            this.barcodeObject.Visible = false;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBarcode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtBarcode.Location = new System.Drawing.Point(12, 410);
            this.txtBarcode.MaxLength = 10;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(95, 20);
            this.txtBarcode.TabIndex = 0;
            this.txtBarcode.TextChanged += new System.EventHandler(this.txtBarcode_TextChanged);
            // 
            // chkUser
            // 
            this.chkUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUser.AutoSize = true;
            this.chkUser.Checked = true;
            this.chkUser.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUser.Location = new System.Drawing.Point(113, 410);
            this.chkUser.Name = "chkUser";
            this.chkUser.Size = new System.Drawing.Size(123, 17);
            this.chkUser.TabIndex = 46;
            this.chkUser.Text = "Scan user and close";
            this.chkUser.UseVisualStyleBackColor = true;
            this.chkUser.CheckedChanged += new System.EventHandler(this.chkUser_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(12, 408);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(248, 28);
            this.panel1.TabIndex = 49;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(490, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 50;
            this.label5.Text = "CAS";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(286, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 51;
            this.label6.Text = "Supplier";
            // 
            // printGenerateButton
            // 
            this.printGenerateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.printGenerateButton.Location = new System.Drawing.Point(448, 408);
            this.printGenerateButton.Name = "printGenerateButton";
            this.printGenerateButton.Size = new System.Drawing.Size(175, 23);
            this.printGenerateButton.TabIndex = 52;
            this.printGenerateButton.Text = "Print and Generate XLS";
            this.printGenerateButton.UseVisualStyleBackColor = true;
            this.printGenerateButton.Click += new System.EventHandler(this.printGenerateButton_Click);
            // 
            // FormPrintBarcodes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 440);
            this.Controls.Add(this.printGenerateButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.chkUser);
            this.Controls.Add(this.barcodeObject);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.printCheckBox);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.contentPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPrintBarcodes";
            this.Text = "FormPrintBarcodes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel contentPanel;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox printCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Neodynamic.WinControls.BarcodeProfessional.BarcodeProfessional barcodeObject;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.CheckBox chkUser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button printGenerateButton;
    }
}