namespace BarcodePcApp
{
    partial class FormInventory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInventory));
            this.codeInputBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.scannerCheckBox = new System.Windows.Forms.CheckBox();
            this.allDoneButton = new System.Windows.Forms.Button();
            this.partlyDoneButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lockerTreeView = new System.Windows.Forms.TreeView();
            this.dgwInventoryItems = new System.Windows.Forms.DataGridView();
            this.barcodeCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.namn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fritext = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inventoryID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inventorydone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inventoryInstructionLabel = new System.Windows.Forms.Label();
            this.startInventoryButton = new System.Windows.Forms.Button();
            this.hideInventoryPanel = new System.Windows.Forms.Panel();
            this.barcodeObject = new Neodynamic.WinControls.BarcodeProfessional.BarcodeProfessional();
            this.locationBarcodeButton = new System.Windows.Forms.Button();
            this.printBarcodesButton = new System.Windows.Forms.Button();
            this.labelStorageName = new System.Windows.Forms.Label();
            this.txtScannedBarcode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwInventoryItems)).BeginInit();
            this.hideInventoryPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // codeInputBox
            // 
            this.codeInputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.codeInputBox.Enabled = false;
            this.codeInputBox.Location = new System.Drawing.Point(392, 40);
            this.codeInputBox.Name = "codeInputBox";
            this.codeInputBox.Size = new System.Drawing.Size(424, 20);
            this.codeInputBox.TabIndex = 2;
            this.codeInputBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.codeInputBox_KeyPress);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Enabled = false;
            this.addButton.Location = new System.Drawing.Point(822, 37);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 3;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // scannerCheckBox
            // 
            this.scannerCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scannerCheckBox.AutoSize = true;
            this.scannerCheckBox.Checked = true;
            this.scannerCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.scannerCheckBox.Enabled = false;
            this.scannerCheckBox.Location = new System.Drawing.Point(903, 41);
            this.scannerCheckBox.Name = "scannerCheckBox";
            this.scannerCheckBox.Size = new System.Drawing.Size(82, 17);
            this.scannerCheckBox.TabIndex = 4;
            this.scannerCheckBox.Text = "Scan && Add";
            this.scannerCheckBox.UseVisualStyleBackColor = true;
            this.scannerCheckBox.CheckedChanged += new System.EventHandler(this.scannerCheckBox_CheckedChanged);
            // 
            // allDoneButton
            // 
            this.allDoneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.allDoneButton.Enabled = false;
            this.allDoneButton.Location = new System.Drawing.Point(901, 588);
            this.allDoneButton.Name = "allDoneButton";
            this.allDoneButton.Size = new System.Drawing.Size(75, 23);
            this.allDoneButton.TabIndex = 5;
            this.allDoneButton.Text = "Done";
            this.allDoneButton.UseVisualStyleBackColor = true;
            this.allDoneButton.Click += new System.EventHandler(this.allDoneButton_Click);
            // 
            // partlyDoneButton
            // 
            this.partlyDoneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.partlyDoneButton.Enabled = false;
            this.partlyDoneButton.Location = new System.Drawing.Point(822, 588);
            this.partlyDoneButton.Name = "partlyDoneButton";
            this.partlyDoneButton.Size = new System.Drawing.Size(75, 23);
            this.partlyDoneButton.TabIndex = 6;
            this.partlyDoneButton.Text = "Partly Done";
            this.partlyDoneButton.UseVisualStyleBackColor = true;
            this.partlyDoneButton.Click += new System.EventHandler(this.partlyDoneButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.lockerTreeView);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 605);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1. Select a location";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // lockerTreeView
            // 
            this.lockerTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lockerTreeView.Location = new System.Drawing.Point(6, 16);
            this.lockerTreeView.Name = "lockerTreeView";
            this.lockerTreeView.ShowNodeToolTips = true;
            this.lockerTreeView.Size = new System.Drawing.Size(362, 580);
            this.lockerTreeView.TabIndex = 2;
            this.lockerTreeView.TabStop = false;
            this.lockerTreeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.lockerTreeView_BeforeSelect);
            this.lockerTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.lockerTreeView_AfterSelect);
            // 
            // dgwInventoryItems
            // 
            this.dgwInventoryItems.AllowUserToAddRows = false;
            this.dgwInventoryItems.AllowUserToDeleteRows = false;
            this.dgwInventoryItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwInventoryItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgwInventoryItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwInventoryItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.barcodeCode,
            this.productID,
            this.namn,
            this.amount,
            this.unit,
            this.fritext,
            this.TransID,
            this.inventoryID,
            this.vem,
            this.nar,
            this.inventorydone});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgwInventoryItems.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgwInventoryItems.Location = new System.Drawing.Point(392, 66);
            this.dgwInventoryItems.Name = "dgwInventoryItems";
            this.dgwInventoryItems.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwInventoryItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgwInventoryItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwInventoryItems.Size = new System.Drawing.Size(584, 518);
            this.dgwInventoryItems.TabIndex = 9;
            // 
            // barcodeCode
            // 
            this.barcodeCode.HeaderText = "Barcode";
            this.barcodeCode.Name = "barcodeCode";
            this.barcodeCode.ReadOnly = true;
            this.barcodeCode.Width = 60;
            // 
            // productID
            // 
            this.productID.HeaderText = "Klara ID";
            this.productID.Name = "productID";
            this.productID.ReadOnly = true;
            this.productID.Width = 70;
            // 
            // namn
            // 
            this.namn.HeaderText = "Name";
            this.namn.Name = "namn";
            this.namn.ReadOnly = true;
            this.namn.Width = 170;
            // 
            // amount
            // 
            this.amount.HeaderText = "Amount";
            this.amount.Name = "amount";
            this.amount.ReadOnly = true;
            this.amount.Width = 60;
            // 
            // unit
            // 
            this.unit.HeaderText = "Unit";
            this.unit.Name = "unit";
            this.unit.ReadOnly = true;
            this.unit.Width = 50;
            // 
            // fritext
            // 
            this.fritext.HeaderText = "Note";
            this.fritext.Name = "fritext";
            this.fritext.ReadOnly = true;
            this.fritext.Width = 120;
            // 
            // TransID
            // 
            this.TransID.HeaderText = "TransID";
            this.TransID.Name = "TransID";
            this.TransID.ReadOnly = true;
            this.TransID.Visible = false;
            // 
            // inventoryID
            // 
            this.inventoryID.HeaderText = "inventoryID";
            this.inventoryID.Name = "inventoryID";
            this.inventoryID.ReadOnly = true;
            this.inventoryID.Visible = false;
            // 
            // vem
            // 
            this.vem.HeaderText = "vem";
            this.vem.Name = "vem";
            this.vem.ReadOnly = true;
            this.vem.Visible = false;
            // 
            // nar
            // 
            this.nar.HeaderText = "nar";
            this.nar.Name = "nar";
            this.nar.ReadOnly = true;
            this.nar.Visible = false;
            // 
            // inventorydone
            // 
            this.inventorydone.HeaderText = "inventorydone";
            this.inventorydone.Name = "inventorydone";
            this.inventorydone.ReadOnly = true;
            // 
            // inventoryInstructionLabel
            // 
            this.inventoryInstructionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inventoryInstructionLabel.AutoSize = true;
            this.inventoryInstructionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inventoryInstructionLabel.Location = new System.Drawing.Point(14, 264);
            this.inventoryInstructionLabel.MaximumSize = new System.Drawing.Size(572, 120);
            this.inventoryInstructionLabel.MinimumSize = new System.Drawing.Size(572, 0);
            this.inventoryInstructionLabel.Name = "inventoryInstructionLabel";
            this.inventoryInstructionLabel.Size = new System.Drawing.Size(572, 20);
            this.inventoryInstructionLabel.TabIndex = 0;
            this.inventoryInstructionLabel.Text = "Please select a room or cabinet.";
            this.inventoryInstructionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // startInventoryButton
            // 
            this.startInventoryButton.Enabled = false;
            this.startInventoryButton.Location = new System.Drawing.Point(239, 334);
            this.startInventoryButton.Name = "startInventoryButton";
            this.startInventoryButton.Size = new System.Drawing.Size(55, 23);
            this.startInventoryButton.TabIndex = 1;
            this.startInventoryButton.Text = "Begin";
            this.startInventoryButton.UseMnemonic = false;
            this.startInventoryButton.UseVisualStyleBackColor = true;
            this.startInventoryButton.Visible = false;
            this.startInventoryButton.Click += new System.EventHandler(this.startInventoryButton_Click);
            // 
            // hideInventoryPanel
            // 
            this.hideInventoryPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hideInventoryPanel.Controls.Add(this.barcodeObject);
            this.hideInventoryPanel.Controls.Add(this.locationBarcodeButton);
            this.hideInventoryPanel.Controls.Add(this.printBarcodesButton);
            this.hideInventoryPanel.Controls.Add(this.startInventoryButton);
            this.hideInventoryPanel.Controls.Add(this.inventoryInstructionLabel);
            this.hideInventoryPanel.Location = new System.Drawing.Point(392, 12);
            this.hideInventoryPanel.Name = "hideInventoryPanel";
            this.hideInventoryPanel.Size = new System.Drawing.Size(593, 605);
            this.hideInventoryPanel.TabIndex = 19;
            // 
            // barcodeObject
            // 
            this.barcodeObject.AztecCodeModuleSize = 1.5634212743937781E+49D;
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
            this.barcodeObject.Location = new System.Drawing.Point(500, 529);
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
            this.barcodeObject.Size = new System.Drawing.Size(83, 33);
            this.barcodeObject.Symbology = Neodynamic.WinControls.BarcodeProfessional.Symbology.Code128;
            this.barcodeObject.TabIndex = 27;
            this.barcodeObject.Tag = "";
            this.barcodeObject.Text = "Testar";
            this.barcodeObject.TextFont = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.barcodeObject.TextForeColor = System.Drawing.Color.Black;
            this.barcodeObject.TiffCompression = Neodynamic.WinControls.BarcodeProfessional.TiffCompression.LZW;
            this.barcodeObject.Visible = false;
            // 
            // locationBarcodeButton
            // 
            this.locationBarcodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.locationBarcodeButton.Location = new System.Drawing.Point(267, 573);
            this.locationBarcodeButton.Name = "locationBarcodeButton";
            this.locationBarcodeButton.Size = new System.Drawing.Size(159, 23);
            this.locationBarcodeButton.TabIndex = 3;
            this.locationBarcodeButton.Text = "Print location barcode";
            this.locationBarcodeButton.UseMnemonic = false;
            this.locationBarcodeButton.UseVisualStyleBackColor = true;
            this.locationBarcodeButton.Click += new System.EventHandler(this.locationBarcodeButton_Click);
            // 
            // printBarcodesButton
            // 
            this.printBarcodesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.printBarcodesButton.Location = new System.Drawing.Point(432, 573);
            this.printBarcodesButton.Name = "printBarcodesButton";
            this.printBarcodesButton.Size = new System.Drawing.Size(124, 23);
            this.printBarcodesButton.TabIndex = 2;
            this.printBarcodesButton.Text = "Print barcodes";
            this.printBarcodesButton.UseMnemonic = false;
            this.printBarcodesButton.UseVisualStyleBackColor = true;
            this.printBarcodesButton.Click += new System.EventHandler(this.printBarcodesButton_Click);
            // 
            // labelStorageName
            // 
            this.labelStorageName.AutoSize = true;
            this.labelStorageName.Location = new System.Drawing.Point(400, 12);
            this.labelStorageName.Name = "labelStorageName";
            this.labelStorageName.Size = new System.Drawing.Size(35, 13);
            this.labelStorageName.TabIndex = 10;
            this.labelStorageName.Text = "label1";
            // 
            // txtScannedBarcode
            // 
            this.txtScannedBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScannedBarcode.Enabled = false;
            this.txtScannedBarcode.Location = new System.Drawing.Point(514, 590);
            this.txtScannedBarcode.MaximumSize = new System.Drawing.Size(200, 20);
            this.txtScannedBarcode.Name = "txtScannedBarcode";
            this.txtScannedBarcode.Size = new System.Drawing.Size(111, 20);
            this.txtScannedBarcode.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(392, 593);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Last scanned barcode:";
            // 
            // FormInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 629);
            this.Controls.Add(this.hideInventoryPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelStorageName);
            this.Controls.Add(this.txtScannedBarcode);
            this.Controls.Add(this.dgwInventoryItems);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.partlyDoneButton);
            this.Controls.Add(this.allDoneButton);
            this.Controls.Add(this.scannerCheckBox);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.codeInputBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1010, 667);
            this.Name = "FormInventory";
            this.Text = "Inventory";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwInventoryItems)).EndInit();
            this.hideInventoryPanel.ResumeLayout(false);
            this.hideInventoryPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox codeInputBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.CheckBox scannerCheckBox;
        private System.Windows.Forms.Button allDoneButton;
        private System.Windows.Forms.Button partlyDoneButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView lockerTreeView;
        private System.Windows.Forms.DataGridView dgwInventoryItems;
        private System.Windows.Forms.Label inventoryInstructionLabel;
        private System.Windows.Forms.Button startInventoryButton;
        private System.Windows.Forms.Panel hideInventoryPanel;
        private System.Windows.Forms.Label labelStorageName;
        private System.Windows.Forms.Button printBarcodesButton;
        private System.Windows.Forms.Button locationBarcodeButton;
        private Neodynamic.WinControls.BarcodeProfessional.BarcodeProfessional barcodeObject;
        private System.Windows.Forms.TextBox txtScannedBarcode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn barcodeCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn productID;
        private System.Windows.Forms.DataGridViewTextBoxColumn namn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn fritext;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransID;
        private System.Windows.Forms.DataGridViewTextBoxColumn inventoryID;
        private System.Windows.Forms.DataGridViewTextBoxColumn vem;
        private System.Windows.Forms.DataGridViewTextBoxColumn nar;
        private System.Windows.Forms.DataGridViewTextBoxColumn inventorydone;
    }
}