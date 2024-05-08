namespace BarcodePcApp
{
    partial class FormOverview
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.resultDataGridView = new System.Windows.Forms.DataGridView();
            this.labelOverview = new System.Windows.Forms.Label();
            this.labelCheckedIn = new System.Windows.Forms.Label();
            this.printDocumentOverview = new System.Drawing.Printing.PrintDocument();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.printDialogOverview = new System.Windows.Forms.PrintDialog();
            this.resultDataGridView2 = new System.Windows.Forms.DataGridView();
            this.buttonPrint2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelCheckedOut = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.bc = new Neodynamic.WinControls.BarcodeProfessional.BarcodeProfessional();
            this.panel2 = new System.Windows.Forms.Panel();
            this.scannerCheckBox = new System.Windows.Forms.CheckBox();
            this.buttonScanned = new System.Windows.Forms.Button();
            this.textBoxScanCode = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // resultDataGridView
            // 
            this.resultDataGridView.AllowUserToAddRows = false;
            this.resultDataGridView.AllowUserToDeleteRows = false;
            this.resultDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.resultDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.resultDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.resultDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.resultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultDataGridView.GridColor = System.Drawing.SystemColors.ControlLight;
            this.resultDataGridView.Location = new System.Drawing.Point(27, 91);
            this.resultDataGridView.MultiSelect = false;
            this.resultDataGridView.Name = "resultDataGridView";
            this.resultDataGridView.ReadOnly = true;
            this.resultDataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.resultDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.resultDataGridView.RowTemplate.Height = 24;
            this.resultDataGridView.RowTemplate.ReadOnly = true;
            this.resultDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.resultDataGridView.Size = new System.Drawing.Size(616, 184);
            this.resultDataGridView.TabIndex = 8;
            this.resultDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.resultDataGridView_CellContentClick);
            // 
            // labelOverview
            // 
            this.labelOverview.AutoSize = true;
            this.labelOverview.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOverview.Location = new System.Drawing.Point(21, 20);
            this.labelOverview.Name = "labelOverview";
            this.labelOverview.Size = new System.Drawing.Size(223, 31);
            this.labelOverview.TabIndex = 9;
            this.labelOverview.Text = "Product overview";
            // 
            // labelCheckedIn
            // 
            this.labelCheckedIn.AutoSize = true;
            this.labelCheckedIn.Location = new System.Drawing.Point(24, 73);
            this.labelCheckedIn.Name = "labelCheckedIn";
            this.labelCheckedIn.Size = new System.Drawing.Size(105, 13);
            this.labelCheckedIn.TabIndex = 14;
            this.labelCheckedIn.Text = "Registered products:";
            // 
            // printDocumentOverview
            // 
            this.printDocumentOverview.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocumentOverview_PrintPage);
            // 
            // buttonPrint
            // 
            this.buttonPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrint.Location = new System.Drawing.Point(568, 62);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(75, 23);
            this.buttonPrint.TabIndex = 15;
            this.buttonPrint.Text = "Print report";
            this.buttonPrint.UseVisualStyleBackColor = true;
            this.buttonPrint.Visible = false;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // printDialogOverview
            // 
            this.printDialogOverview.UseEXDialog = true;
            // 
            // resultDataGridView2
            // 
            this.resultDataGridView2.AllowUserToAddRows = false;
            this.resultDataGridView2.AllowUserToDeleteRows = false;
            this.resultDataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultDataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.resultDataGridView2.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.resultDataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.resultDataGridView2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.resultDataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultDataGridView2.GridColor = System.Drawing.SystemColors.ControlLight;
            this.resultDataGridView2.Location = new System.Drawing.Point(27, 353);
            this.resultDataGridView2.MultiSelect = false;
            this.resultDataGridView2.Name = "resultDataGridView2";
            this.resultDataGridView2.ReadOnly = true;
            this.resultDataGridView2.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.resultDataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.resultDataGridView2.RowTemplate.Height = 24;
            this.resultDataGridView2.RowTemplate.ReadOnly = true;
            this.resultDataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.resultDataGridView2.Size = new System.Drawing.Size(616, 172);
            this.resultDataGridView2.TabIndex = 17;
            // 
            // buttonPrint2
            // 
            this.buttonPrint2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrint2.Location = new System.Drawing.Point(568, 324);
            this.buttonPrint2.Name = "buttonPrint2";
            this.buttonPrint2.Size = new System.Drawing.Size(75, 23);
            this.buttonPrint2.TabIndex = 19;
            this.buttonPrint2.Text = "Print report";
            this.buttonPrint2.UseVisualStyleBackColor = true;
            this.buttonPrint2.Visible = false;
            this.buttonPrint2.Click += new System.EventHandler(this.buttonPrint2_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Location = new System.Drawing.Point(30, 301);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(613, 2);
            this.panel1.TabIndex = 21;
            // 
            // labelCheckedOut
            // 
            this.labelCheckedOut.AutoSize = true;
            this.labelCheckedOut.Location = new System.Drawing.Point(24, 329);
            this.labelCheckedOut.Name = "labelCheckedOut";
            this.labelCheckedOut.Size = new System.Drawing.Size(102, 13);
            this.labelCheckedOut.TabIndex = 22;
            this.labelCheckedOut.Text = "Discarded products:";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonClose.Location = new System.Drawing.Point(568, 34);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 23;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // bc
            // 
            this.bc.AztecCodeModuleSize = 7223238453.1716D;
            this.bc.BackColor = System.Drawing.Color.White;
            this.bc.BarcodePadding = new Neodynamic.WinControls.BarcodeProfessional.Margin(0D, 0D, 0D, 0D);
            this.bc.BarcodeUnit = Neodynamic.WinControls.BarcodeProfessional.BarcodeUnit.Millimeter;
            this.bc.BarHeight = 4D;
            this.bc.BarRatio = 2D;
            this.bc.BarWidth = 0.23D;
            this.bc.BarWidthAdjustment = 0D;
            this.bc.BearerBarWidth = 1.27D;
            this.bc.BorderWidth = 0D;
            this.bc.Code = "1234567890";
            this.bc.DataMatrixModuleSize = 1.059D;
            this.bc.EanUpcSupplementSeparation = 3.81D;
            this.bc.EanUpcSupplementTopMargin = 3.81D;
            this.bc.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.bc.GuardBarHeight = 6.35D;
            this.bc.IsbnSupplementCode = "0";
            this.bc.Location = new System.Drawing.Point(244, 9);
            this.bc.Margin = new System.Windows.Forms.Padding(2);
            this.bc.MICRCharHeight = 2.972D;
            this.bc.Name = "bc";
            this.bc.Pdf417AspectRatio = 0D;
            this.bc.PharmacodeBarsSpacing = 1.059D;
            this.bc.PharmacodeThickBarWidth = 1.588D;
            this.bc.PharmacodeThinBarWidth = 0.528D;
            this.bc.PlanetHeightShortBar = 2.54D;
            this.bc.PlanetHeightTallBar = 5.08D;
            this.bc.Postal4StateBarsSpacing = 0.795D;
            this.bc.Postal4StateTrackerBarHeight = 2.032D;
            this.bc.Postal4StateTrackerBarWidth = 0.528D;
            this.bc.PostnetHeightShortBar = 2.54D;
            this.bc.PostnetHeightTallBar = 5.08D;
            this.bc.QRCodeModuleSize = 1.059D;
            this.bc.QuietZoneWidth = 0.5D;
            this.bc.Size = new System.Drawing.Size(83, 33);
            this.bc.Symbology = Neodynamic.WinControls.BarcodeProfessional.Symbology.Code128;
            this.bc.TabIndex = 25;
            this.bc.Tag = "";
            this.bc.Text = "Testar";
            this.bc.TextFont = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.bc.TextForeColor = System.Drawing.Color.Black;
            this.bc.TiffCompression = Neodynamic.WinControls.BarcodeProfessional.TiffCompression.LZW;
            this.bc.Visible = false;
            this.bc.Click += new System.EventHandler(this.bc_Click);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(336, 9);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(307, 25);
            this.panel2.TabIndex = 50;
            // 
            // scannerCheckBox
            // 
            this.scannerCheckBox.AutoSize = true;
            this.scannerCheckBox.Location = new System.Drawing.Point(521, 11);
            this.scannerCheckBox.Name = "scannerCheckBox";
            this.scannerCheckBox.Size = new System.Drawing.Size(111, 17);
            this.scannerCheckBox.TabIndex = 53;
            this.scannerCheckBox.Text = "Scan && check out";
            this.scannerCheckBox.UseVisualStyleBackColor = true;
            this.scannerCheckBox.CheckedChanged += new System.EventHandler(this.scannerCheckBox_CheckedChanged);
            this.scannerCheckBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.form_KeyPress);
            // 
            // buttonScanned
            // 
            this.buttonScanned.Location = new System.Drawing.Point(442, 10);
            this.buttonScanned.Name = "buttonScanned";
            this.buttonScanned.Size = new System.Drawing.Size(64, 20);
            this.buttonScanned.TabIndex = 52;
            this.buttonScanned.Text = "Check out";
            this.buttonScanned.UseVisualStyleBackColor = true;
            // 
            // textBoxScanCode
            // 
            this.textBoxScanCode.Location = new System.Drawing.Point(338, 11);
            this.textBoxScanCode.Name = "textBoxScanCode";
            this.textBoxScanCode.Size = new System.Drawing.Size(92, 20);
            this.textBoxScanCode.TabIndex = 1;
            this.textBoxScanCode.TextChanged += new System.EventHandler(this.textBoxScanCode_TextChanged);
            // 
            // FormOverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 551);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.scannerCheckBox);
            this.Controls.Add(this.buttonScanned);
            this.Controls.Add(this.textBoxScanCode);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.labelCheckedOut);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonPrint2);
            this.Controls.Add(this.resultDataGridView2);
            this.Controls.Add(this.buttonPrint);
            this.Controls.Add(this.labelCheckedIn);
            this.Controls.Add(this.labelOverview);
            this.Controls.Add(this.resultDataGridView);
            this.Controls.Add(this.bc);
            this.MinimumSize = new System.Drawing.Size(673, 573);
            this.Name = "FormOverview";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product overview";
            this.Load += new System.EventHandler(this.FormOverview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView resultDataGridView;
        private System.Windows.Forms.Label labelOverview;
        private System.Windows.Forms.Label labelCheckedIn;
        private System.Drawing.Printing.PrintDocument printDocumentOverview;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.PrintDialog printDialogOverview;
        private System.Windows.Forms.DataGridView resultDataGridView2;
        private System.Windows.Forms.Button buttonPrint2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelCheckedOut;
        private System.Windows.Forms.Button buttonClose;
        private Neodynamic.WinControls.BarcodeProfessional.BarcodeProfessional bc;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox scannerCheckBox;
        private System.Windows.Forms.Button buttonScanned;
        private System.Windows.Forms.TextBox textBoxScanCode;
    }
}