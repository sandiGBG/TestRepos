namespace BarcodePcApp
{
    partial class FormCheckOut
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelCheckOut = new System.Windows.Forms.Label();
            this.labelShowProduct = new System.Windows.Forms.Label();
            this.textBoxScanCode = new System.Windows.Forms.TextBox();
            this.labelInfoScan = new System.Windows.Forms.Label();
            this.buttonScanned = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.resultDataGridView = new System.Windows.Forms.DataGridView();
            this.buttonClose = new System.Windows.Forms.Button();
            this.scannerCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txttradlos = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelCheckOut
            // 
            this.labelCheckOut.AutoSize = true;
            this.labelCheckOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCheckOut.Location = new System.Drawing.Point(21, 20);
            this.labelCheckOut.Name = "labelCheckOut";
            this.labelCheckOut.Size = new System.Drawing.Size(219, 31);
            this.labelCheckOut.TabIndex = 1;
            this.labelCheckOut.Text = "Discard products";
            // 
            // labelShowProduct
            // 
            this.labelShowProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShowProduct.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelShowProduct.Location = new System.Drawing.Point(360, 9);
            this.labelShowProduct.Name = "labelShowProduct";
            this.labelShowProduct.Size = new System.Drawing.Size(229, 20);
            this.labelShowProduct.TabIndex = 0;
            this.labelShowProduct.Visible = false;
            // 
            // textBoxScanCode
            // 
            this.textBoxScanCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxScanCode.Location = new System.Drawing.Point(27, 76);
            this.textBoxScanCode.Name = "textBoxScanCode";
            this.textBoxScanCode.Size = new System.Drawing.Size(377, 20);
            this.textBoxScanCode.TabIndex = 1;
            this.textBoxScanCode.TextChanged += new System.EventHandler(this.textBoxScanCode_TextChanged);
            // 
            // labelInfoScan
            // 
            this.labelInfoScan.Location = new System.Drawing.Point(24, 56);
            this.labelInfoScan.Name = "labelInfoScan";
            this.labelInfoScan.Size = new System.Drawing.Size(466, 18);
            this.labelInfoScan.TabIndex = 9;
            this.labelInfoScan.Text = "Scan product or enter the barcode and click the Discard button:";
            // 
            // buttonScanned
            // 
            this.buttonScanned.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonScanned.Location = new System.Drawing.Point(410, 77);
            this.buttonScanned.Name = "buttonScanned";
            this.buttonScanned.Size = new System.Drawing.Size(80, 20);
            this.buttonScanned.TabIndex = 3;
            this.buttonScanned.Text = "Discard";
            this.buttonScanned.UseVisualStyleBackColor = true;
            this.buttonScanned.Click += new System.EventHandler(this.buttonScanned_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Location = new System.Drawing.Point(27, 113);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(635, 2);
            this.panel1.TabIndex = 18;
            // 
            // resultDataGridView
            // 
            this.resultDataGridView.AllowUserToAddRows = false;
            this.resultDataGridView.AllowUserToDeleteRows = false;
            this.resultDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.resultDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.resultDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.resultDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.resultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultDataGridView.GridColor = System.Drawing.SystemColors.ControlLight;
            this.resultDataGridView.Location = new System.Drawing.Point(6, 30);
            this.resultDataGridView.MultiSelect = false;
            this.resultDataGridView.Name = "resultDataGridView";
            this.resultDataGridView.ReadOnly = true;
            this.resultDataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.resultDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.resultDataGridView.RowTemplate.ReadOnly = true;
            this.resultDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.resultDataGridView.Size = new System.Drawing.Size(621, 114);
            this.resultDataGridView.TabIndex = 16;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonClose.Location = new System.Drawing.Point(585, 306);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 24;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // scannerCheckBox
            // 
            this.scannerCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scannerCheckBox.AutoSize = true;
            this.scannerCheckBox.Checked = true;
            this.scannerCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.scannerCheckBox.Location = new System.Drawing.Point(521, 78);
            this.scannerCheckBox.Name = "scannerCheckBox";
            this.scannerCheckBox.Size = new System.Drawing.Size(97, 17);
            this.scannerCheckBox.TabIndex = 25;
            this.scannerCheckBox.Text = "Scan && discard";
            this.scannerCheckBox.UseVisualStyleBackColor = true;
            this.scannerCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.resultDataGridView);
            this.groupBox1.Location = new System.Drawing.Point(27, 137);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(633, 163);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Discard products";
            // 
            // txttradlos
            // 
            this.txttradlos.Location = new System.Drawing.Point(394, 8);
            this.txttradlos.Name = "txttradlos";
            this.txttradlos.Size = new System.Drawing.Size(155, 20);
            this.txttradlos.TabIndex = 27;
            this.txttradlos.TextChanged += new System.EventHandler(this.txttradlos_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(394, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(171, 30);
            this.panel2.TabIndex = 28;
            // 
            // FormCheckOut
            // 
            this.AcceptButton = this.buttonScanned;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(672, 336);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txttradlos);
            this.Controls.Add(this.scannerCheckBox);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonScanned);
            this.Controls.Add(this.labelInfoScan);
            this.Controls.Add(this.textBoxScanCode);
            this.Controls.Add(this.labelShowProduct);
            this.Controls.Add(this.labelCheckOut);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(667, 334);
            this.Name = "FormCheckOut";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Discard products";
            this.Load += new System.EventHandler(this.FormCheckOut_Load);
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCheckOut;
        private System.Windows.Forms.Label labelShowProduct;
        private System.Windows.Forms.TextBox textBoxScanCode;
        private System.Windows.Forms.Label labelInfoScan;
        private System.Windows.Forms.Button buttonScanned;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView resultDataGridView;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.CheckBox scannerCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txttradlos;
        private System.Windows.Forms.Panel panel2;
    }
}