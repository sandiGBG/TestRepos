namespace BarcodePcApp
{
    partial class FormRelease
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ReleaseDataGridView = new System.Windows.Forms.DataGridView();
            this.lblRelease = new System.Windows.Forms.Label();
            this.printDocumentOverview = new System.Drawing.Printing.PrintDocument();
            this.printDialogOverview = new System.Windows.Forms.PrintDialog();
            this.btnExecute = new System.Windows.Forms.Button();
            this.DismissedDataGridView = new System.Windows.Forms.DataGridView();
            this.btnPrint2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDismissed = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.bc = new Neodynamic.WinControls.BarcodeProfessional.BarcodeProfessional();
            this.panel2 = new System.Windows.Forms.Panel();
            this.scannerCheckBox = new System.Windows.Forms.CheckBox();
            this.buttonScanned = new System.Windows.Forms.Button();
            this.textBoxScanCode = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ReleaseDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DismissedDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ReleaseDataGridView
            // 
            this.ReleaseDataGridView.AllowUserToAddRows = false;
            this.ReleaseDataGridView.AllowUserToDeleteRows = false;
            this.ReleaseDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReleaseDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ReleaseDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.ReleaseDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ReleaseDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ReleaseDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ReleaseDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ReleaseDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.ReleaseDataGridView.GridColor = System.Drawing.SystemColors.ControlLight;
            this.ReleaseDataGridView.Location = new System.Drawing.Point(36, 112);
            this.ReleaseDataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ReleaseDataGridView.MultiSelect = false;
            this.ReleaseDataGridView.Name = "ReleaseDataGridView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ReleaseDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.ReleaseDataGridView.RowHeadersVisible = false;
            this.ReleaseDataGridView.RowHeadersWidth = 51;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ReleaseDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.ReleaseDataGridView.RowTemplate.Height = 24;
            this.ReleaseDataGridView.RowTemplate.ReadOnly = true;
            this.ReleaseDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ReleaseDataGridView.Size = new System.Drawing.Size(1348, 226);
            this.ReleaseDataGridView.TabIndex = 8;
            this.ReleaseDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ReleaseDataGridView_CellClick);
            this.ReleaseDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ReleaseDataGridView_CellContentClick);
            this.ReleaseDataGridView.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ReleaseDataGridView_CellMouseUp);
            this.ReleaseDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ReleaseDataGridView_CellValueChanged);
            // 
            // lblRelease
            // 
            this.lblRelease.AutoSize = true;
            this.lblRelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblRelease.Location = new System.Drawing.Point(33, 71);
            this.lblRelease.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRelease.Name = "lblRelease";
            this.lblRelease.Size = new System.Drawing.Size(256, 31);
            this.lblRelease.TabIndex = 9;
            this.lblRelease.Text = "Release of products";
            // 
            // printDialogOverview
            // 
            this.printDialogOverview.UseEXDialog = true;
            // 
            // btnExecute
            // 
            this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecute.Location = new System.Drawing.Point(1143, 76);
            this.btnExecute.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(133, 28);
            this.btnExecute.TabIndex = 16;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Visible = false;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // DismissedDataGridView
            // 
            this.DismissedDataGridView.AllowUserToAddRows = false;
            this.DismissedDataGridView.AllowUserToDeleteRows = false;
            this.DismissedDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DismissedDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DismissedDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.DismissedDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DismissedDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DismissedDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.DismissedDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DismissedDataGridView.DefaultCellStyle = dataGridViewCellStyle6;
            this.DismissedDataGridView.GridColor = System.Drawing.SystemColors.ControlLight;
            this.DismissedDataGridView.Location = new System.Drawing.Point(36, 434);
            this.DismissedDataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DismissedDataGridView.MultiSelect = false;
            this.DismissedDataGridView.Name = "DismissedDataGridView";
            this.DismissedDataGridView.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DismissedDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.DismissedDataGridView.RowHeadersVisible = false;
            this.DismissedDataGridView.RowHeadersWidth = 51;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.ControlLight;
            this.DismissedDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.DismissedDataGridView.RowTemplate.Height = 24;
            this.DismissedDataGridView.RowTemplate.ReadOnly = true;
            this.DismissedDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DismissedDataGridView.Size = new System.Drawing.Size(1348, 212);
            this.DismissedDataGridView.TabIndex = 17;
            // 
            // btnPrint2
            // 
            this.btnPrint2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint2.Location = new System.Drawing.Point(1284, 399);
            this.btnPrint2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPrint2.Name = "btnPrint2";
            this.btnPrint2.Size = new System.Drawing.Size(100, 28);
            this.btnPrint2.TabIndex = 19;
            this.btnPrint2.Text = "Print report";
            this.btnPrint2.UseVisualStyleBackColor = true;
            this.btnPrint2.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Location = new System.Drawing.Point(40, 370);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1344, 2);
            this.panel1.TabIndex = 21;
            // 
            // lblDismissed
            // 
            this.lblDismissed.AutoSize = true;
            this.lblDismissed.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDismissed.Location = new System.Drawing.Point(32, 395);
            this.lblDismissed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDismissed.Name = "lblDismissed";
            this.lblDismissed.Size = new System.Drawing.Size(260, 31);
            this.lblDismissed.TabIndex = 22;
            this.lblDismissed.Text = "Dismissed products:";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClose.Location = new System.Drawing.Point(1284, 76);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // bc
            // 
            this.bc.AztecCodeModuleSize = 3.5528147874660465E+36D;
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
            this.bc.Location = new System.Drawing.Point(448, 44);
            this.bc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.bc.Size = new System.Drawing.Size(111, 41);
            this.bc.Symbology = Neodynamic.WinControls.BarcodeProfessional.Symbology.Code128;
            this.bc.TabIndex = 25;
            this.bc.Tag = "";
            this.bc.Text = "Testar";
            this.bc.TextFont = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.bc.TextForeColor = System.Drawing.Color.Black;
            this.bc.TiffCompression = Neodynamic.WinControls.BarcodeProfessional.TiffCompression.LZW;
            this.bc.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(448, 7);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(409, 31);
            this.panel2.TabIndex = 50;
            // 
            // scannerCheckBox
            // 
            this.scannerCheckBox.AutoSize = true;
            this.scannerCheckBox.Location = new System.Drawing.Point(695, 14);
            this.scannerCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.scannerCheckBox.Name = "scannerCheckBox";
            this.scannerCheckBox.Size = new System.Drawing.Size(140, 21);
            this.scannerCheckBox.TabIndex = 53;
            this.scannerCheckBox.Text = "Scan && check out";
            this.scannerCheckBox.UseVisualStyleBackColor = true;
            // 
            // buttonScanned
            // 
            this.buttonScanned.Location = new System.Drawing.Point(589, 12);
            this.buttonScanned.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonScanned.Name = "buttonScanned";
            this.buttonScanned.Size = new System.Drawing.Size(85, 25);
            this.buttonScanned.TabIndex = 52;
            this.buttonScanned.Text = "Check out";
            this.buttonScanned.UseVisualStyleBackColor = true;
            // 
            // textBoxScanCode
            // 
            this.textBoxScanCode.Location = new System.Drawing.Point(451, 14);
            this.textBoxScanCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxScanCode.Name = "textBoxScanCode";
            this.textBoxScanCode.Size = new System.Drawing.Size(121, 22);
            this.textBoxScanCode.TabIndex = 1;
            // 
            // FormRelease
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1403, 689);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.scannerCheckBox);
            this.Controls.Add(this.buttonScanned);
            this.Controls.Add(this.textBoxScanCode);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblDismissed);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnPrint2);
            this.Controls.Add(this.DismissedDataGridView);
            this.Controls.Add(this.lblRelease);
            this.Controls.Add(this.ReleaseDataGridView);
            this.Controls.Add(this.bc);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1418, 725);
            this.Name = "FormRelease";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Release of products";
            this.Load += new System.EventHandler(this.FormRelease_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReleaseDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DismissedDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ReleaseDataGridView;
        private System.Windows.Forms.Label lblRelease;
        private System.Drawing.Printing.PrintDocument printDocumentOverview;
        private System.Windows.Forms.PrintDialog printDialogOverview;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.DataGridView DismissedDataGridView;
        private System.Windows.Forms.Button btnPrint2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDismissed;
        private System.Windows.Forms.Button btnClose;
        private Neodynamic.WinControls.BarcodeProfessional.BarcodeProfessional bc;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox scannerCheckBox;
        private System.Windows.Forms.Button buttonScanned;
        private System.Windows.Forms.TextBox textBoxScanCode;
    }
}