namespace BarcodePcApp
{
    partial class FormOrder
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
            this.m_btnClose = new System.Windows.Forms.Button();
            this.m_gridOrder = new System.Windows.Forms.DataGridView();
            this.m_cbOrders = new System.Windows.Forms.ComboBox();
            this.m_textAntal = new System.Windows.Forms.TextBox();
            this.m_btnPrint = new System.Windows.Forms.Button();
            this.bc = new Neodynamic.WinControls.BarcodeProfessional.BarcodeProfessional();
            this.m_btnRemove = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.commentTxtBox = new System.Windows.Forms.TextBox();
            this.scannerCheckBox = new System.Windows.Forms.CheckBox();
            this.buttonScanned = new System.Windows.Forms.Button();
            this.textBoxScanCode = new System.Windows.Forms.TextBox();
            this.labelShowProduct = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btnClose
            // 
            this.m_btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnClose.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_btnClose.Location = new System.Drawing.Point(907, 366);
            this.m_btnClose.Name = "m_btnClose";
            this.m_btnClose.Size = new System.Drawing.Size(75, 23);
            this.m_btnClose.TabIndex = 24;
            this.m_btnClose.Text = "Close";
            this.m_btnClose.UseVisualStyleBackColor = true;
            this.m_btnClose.Click += new System.EventHandler(this.m_btnClose_Click);
            // 
            // m_gridOrder
            // 
            this.m_gridOrder.AllowUserToAddRows = false;
            this.m_gridOrder.AllowUserToDeleteRows = false;
            this.m_gridOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.m_gridOrder.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.m_gridOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.m_gridOrder.DefaultCellStyle = dataGridViewCellStyle2;
            this.m_gridOrder.Location = new System.Drawing.Point(12, 32);
            this.m_gridOrder.MultiSelect = false;
            this.m_gridOrder.Name = "m_gridOrder";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.m_gridOrder.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.m_gridOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_gridOrder.Size = new System.Drawing.Size(970, 323);
            this.m_gridOrder.TabIndex = 25;
            // 
            // m_cbOrders
            // 
            this.m_cbOrders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbOrders.FormattingEnabled = true;
            this.m_cbOrders.Location = new System.Drawing.Point(13, 5);
            this.m_cbOrders.Name = "m_cbOrders";
            this.m_cbOrders.Size = new System.Drawing.Size(258, 21);
            this.m_cbOrders.TabIndex = 26;
            this.m_cbOrders.SelectedIndexChanged += new System.EventHandler(this.m_cbOrders_SelectedIndexChanged);
            // 
            // m_textAntal
            // 
            this.m_textAntal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_textAntal.Location = new System.Drawing.Point(54, 366);
            this.m_textAntal.Name = "m_textAntal";
            this.m_textAntal.Size = new System.Drawing.Size(47, 20);
            this.m_textAntal.TabIndex = 27;
            // 
            // m_btnPrint
            // 
            this.m_btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnPrint.Location = new System.Drawing.Point(745, 366);
            this.m_btnPrint.Name = "m_btnPrint";
            this.m_btnPrint.Size = new System.Drawing.Size(75, 23);
            this.m_btnPrint.TabIndex = 28;
            this.m_btnPrint.Text = "Print";
            this.m_btnPrint.UseVisualStyleBackColor = true;
            this.m_btnPrint.Click += new System.EventHandler(this.m_btnPrint_Click);
            // 
            // bc
            // 
            //this.bc.AztecCodeModuleSize = 1.0592D;
            this.bc.AztecCodeModuleSize = 2.1684718557124368E+32D;
            this.bc.BackColor = System.Drawing.Color.White;
            this.bc.BarcodePadding = new Neodynamic.WinControls.BarcodeProfessional.Margin(0D, 0D, 0D, 0D);
            this.bc.BarcodeUnit = Neodynamic.WinControls.BarcodeProfessional.BarcodeUnit.Millimeter;
            this.bc.BarHeight = 4D;
            this.bc.BarRatio = 2D;
            this.bc.BarWidth = 0.225D;
            this.bc.BarWidthAdjustment = 0D;
            this.bc.BearerBarWidth = 1.27D;
            this.bc.BorderWidth = 0D;
            this.bc.Code = "919200013";
            this.bc.DataMatrixModuleSize = 1.0592D;
            this.bc.EanUpcSupplementSeparation = 3.81D;
            this.bc.EanUpcSupplementTopMargin = 3.81D;
            this.bc.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.bc.GuardBarHeight = 6.35D;
            this.bc.IsbnSupplementCode = "0";
            this.bc.Location = new System.Drawing.Point(888, 5);
            //this.bc.Margin = new System.Windows.Forms.Padding(2);
            this.bc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bc.MICRCharHeight = 2.9718D;
            this.bc.Name = "bc";
            this.bc.Pdf417AspectRatio = 0D;
            this.bc.PharmacodeBarsSpacing = 1.0592D;
            this.bc.PharmacodeThickBarWidth = 1.5875D;
            this.bc.PharmacodeThinBarWidth = 0.5283D;
            this.bc.PlanetHeightShortBar = 2.54D;
            this.bc.PlanetHeightTallBar = 5.08D;
            this.bc.Postal4StateBarsSpacing = 0.795D;
            this.bc.Postal4StateTrackerBarHeight = 2.032D;
            this.bc.Postal4StateTrackerBarWidth = 0.5283D;
            this.bc.PostnetHeightShortBar = 2.54D;
            this.bc.PostnetHeightTallBar = 5.08D;
            this.bc.QRCodeModuleSize = 1.0592D;
            this.bc.QuietZoneWidth = 0.5D;
            this.bc.Size = new System.Drawing.Size(90, 25);
            this.bc.Symbology = Neodynamic.WinControls.BarcodeProfessional.Symbology.Code128;
            this.bc.TabIndex = 40;
            this.bc.TextFont = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.bc.TextForeColor = System.Drawing.Color.Black;
            this.bc.TiffCompression = Neodynamic.WinControls.BarcodeProfessional.TiffCompression.LZW;
            this.bc.Visible = false;
            // 
            // m_btnRemove
            // 
            this.m_btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnRemove.Location = new System.Drawing.Point(826, 366);
            this.m_btnRemove.Name = "m_btnRemove";
            this.m_btnRemove.Size = new System.Drawing.Size(75, 23);
            this.m_btnRemove.TabIndex = 41;
            this.m_btnRemove.Text = "Remove";
            this.m_btnRemove.UseVisualStyleBackColor = true;
            this.m_btnRemove.Click += new System.EventHandler(this.m_btnRemove_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 368);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Labels";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 368);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "Note";
            // 
            // commentTxtBox
            // 
            this.commentTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.commentTxtBox.Location = new System.Drawing.Point(142, 366);
            this.commentTxtBox.Name = "commentTxtBox";
            this.commentTxtBox.Size = new System.Drawing.Size(142, 20);
            this.commentTxtBox.TabIndex = 44;
            // 
            // scannerCheckBox
            // 
            this.scannerCheckBox.AutoSize = true;
            this.scannerCheckBox.Location = new System.Drawing.Point(765, 8);
            this.scannerCheckBox.Name = "scannerCheckBox";
            this.scannerCheckBox.Size = new System.Drawing.Size(111, 17);
            this.scannerCheckBox.TabIndex = 47;
            this.scannerCheckBox.Text = "Scan && check out";
            this.scannerCheckBox.UseVisualStyleBackColor = true;
            this.scannerCheckBox.CheckedChanged += new System.EventHandler(this.scannerCheckBox_CheckedChanged);
            this.scannerCheckBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.form_KeyPress);
            // 
            // buttonScanned
            // 
            this.buttonScanned.Location = new System.Drawing.Point(695, 7);
            this.buttonScanned.Name = "buttonScanned";
            this.buttonScanned.Size = new System.Drawing.Size(64, 20);
            this.buttonScanned.TabIndex = 46;
            this.buttonScanned.Text = "Check out";
            this.buttonScanned.UseVisualStyleBackColor = true;
            this.buttonScanned.Click += new System.EventHandler(this.buttonScanned_Click);
            // 
            // textBoxScanCode
            // 
            this.textBoxScanCode.Location = new System.Drawing.Point(481, 8);
            this.textBoxScanCode.Name = "textBoxScanCode";
            this.textBoxScanCode.Size = new System.Drawing.Size(171, 20);
            this.textBoxScanCode.TabIndex = 1;
            this.textBoxScanCode.TextChanged += new System.EventHandler(this.textBoxScanCode_TextChanged);
            // 
            // labelShowProduct
            // 
            this.labelShowProduct.AutoSize = true;
            this.labelShowProduct.Location = new System.Drawing.Point(440, 13);
            this.labelShowProduct.Name = "labelShowProduct";
            this.labelShowProduct.Size = new System.Drawing.Size(35, 13);
            this.labelShowProduct.TabIndex = 48;
            this.labelShowProduct.Text = "label3";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(287, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(596, 25);
            this.panel1.TabIndex = 49;
            // 
            // FormOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 396);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelShowProduct);
            this.Controls.Add(this.scannerCheckBox);
            this.Controls.Add(this.buttonScanned);
            this.Controls.Add(this.textBoxScanCode);
            this.Controls.Add(this.commentTxtBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_btnRemove);
            this.Controls.Add(this.bc);
            this.Controls.Add(this.m_btnPrint);
            this.Controls.Add(this.m_textAntal);
            this.Controls.Add(this.m_cbOrders);
            this.Controls.Add(this.m_gridOrder);
            this.Controls.Add(this.m_btnClose);
            this.MinimumSize = new System.Drawing.Size(1010, 434);
            this.Name = "FormOrder";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Order";
            this.Load += new System.EventHandler(this.FormOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridOrder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_btnClose;
        private System.Windows.Forms.DataGridView m_gridOrder;
        private System.Windows.Forms.ComboBox m_cbOrders;
        private System.Windows.Forms.TextBox m_textAntal;
        private System.Windows.Forms.Button m_btnPrint;
        private Neodynamic.WinControls.BarcodeProfessional.BarcodeProfessional bc;
        private System.Windows.Forms.Button m_btnRemove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox commentTxtBox;
        private System.Windows.Forms.CheckBox scannerCheckBox;
        private System.Windows.Forms.Button buttonScanned;
        private System.Windows.Forms.TextBox textBoxScanCode;
        private System.Windows.Forms.Label labelShowProduct;
        private System.Windows.Forms.Panel panel1;
    }
}