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
            this.panelPrintBarcodes = new System.Windows.Forms.Panel();
            this.lblRbarcodes = new System.Windows.Forms.Label();
            this.lblPrintBarcodes = new System.Windows.Forms.Label();
            this.printBarcodesButton = new System.Windows.Forms.Button();
            this.locationBarcodeButton = new System.Windows.Forms.Button();
            this.panelBegin = new System.Windows.Forms.Panel();
            this.barcodeObject = new Neodynamic.WinControls.BarcodeProfessional.BarcodeProfessional();
            this.labelStorageName = new System.Windows.Forms.Label();
            this.txtScannedBarcode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtScannedList = new System.Windows.Forms.TextBox();
            this.panelVälj = new System.Windows.Forms.Panel();
            this.btnBegin = new System.Windows.Forms.Button();
            this.rbtNollställ = new System.Windows.Forms.RadioButton();
            this.rbtUtrStre = new System.Windows.Forms.RadioButton();
            this.rbtProdStr = new System.Windows.Forms.RadioButton();
            this.lblTitel = new System.Windows.Forms.Label();
            this.rbtInventera = new System.Windows.Forms.RadioButton();
            this.rbtFlytta = new System.Windows.Forms.RadioButton();
            this.panelNollställ = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnNollstall = new System.Windows.Forms.Button();
            this.lblBeskrivning = new System.Windows.Forms.Label();
            this.lblNollställ = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.panelNoll = new System.Windows.Forms.Panel();
            this.btnNoll = new System.Windows.Forms.Button();
            this.btnHuvudMeny = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwInventoryItems)).BeginInit();
            this.hideInventoryPanel.SuspendLayout();
            this.panelPrintBarcodes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelNoll.SuspendLayout();
            this.SuspendLayout();
            // 
            // codeInputBox
            // 
            this.codeInputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.codeInputBox.Enabled = false;
            this.codeInputBox.Location = new System.Drawing.Point(523, 49);
            this.codeInputBox.Margin = new System.Windows.Forms.Padding(4);
            this.codeInputBox.Name = "codeInputBox";
            this.codeInputBox.Size = new System.Drawing.Size(564, 22);
            this.codeInputBox.TabIndex = 2;
            this.codeInputBox.TextChanged += new System.EventHandler(this.codeInputBox_TextChanged);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Enabled = false;
            this.addButton.Location = new System.Drawing.Point(1096, 46);
            this.addButton.Margin = new System.Windows.Forms.Padding(4);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(100, 28);
            this.addButton.TabIndex = 3;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            this.addButton.Enter += new System.EventHandler(this.addButton_Click);
            // 
            // scannerCheckBox
            // 
            this.scannerCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scannerCheckBox.AutoSize = true;
            this.scannerCheckBox.Checked = true;
            this.scannerCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.scannerCheckBox.Enabled = false;
            this.scannerCheckBox.Location = new System.Drawing.Point(1209, 50);
            this.scannerCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.scannerCheckBox.Name = "scannerCheckBox";
            this.scannerCheckBox.Size = new System.Drawing.Size(104, 21);
            this.scannerCheckBox.TabIndex = 4;
            this.scannerCheckBox.Text = "Scan && Add";
            this.scannerCheckBox.UseVisualStyleBackColor = true;
            this.scannerCheckBox.CheckedChanged += new System.EventHandler(this.scannerCheckBox_CheckedChanged);
            // 
            // allDoneButton
            // 
            this.allDoneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.allDoneButton.Enabled = false;
            this.allDoneButton.Location = new System.Drawing.Point(1201, 722);
            this.allDoneButton.Margin = new System.Windows.Forms.Padding(4);
            this.allDoneButton.Name = "allDoneButton";
            this.allDoneButton.Size = new System.Drawing.Size(100, 28);
            this.allDoneButton.TabIndex = 5;
            this.allDoneButton.Text = "Done";
            this.allDoneButton.UseVisualStyleBackColor = true;
            this.allDoneButton.Click += new System.EventHandler(this.allDoneButton_Click);
            // 
            // partlyDoneButton
            // 
            this.partlyDoneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.partlyDoneButton.Enabled = false;
            this.partlyDoneButton.Location = new System.Drawing.Point(1096, 722);
            this.partlyDoneButton.Margin = new System.Windows.Forms.Padding(4);
            this.partlyDoneButton.Name = "partlyDoneButton";
            this.partlyDoneButton.Size = new System.Drawing.Size(100, 28);
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
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(499, 743);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1. Select a location";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // lockerTreeView
            // 
            this.lockerTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lockerTreeView.Location = new System.Drawing.Point(8, 20);
            this.lockerTreeView.Margin = new System.Windows.Forms.Padding(4);
            this.lockerTreeView.Name = "lockerTreeView";
            this.lockerTreeView.ShowNodeToolTips = true;
            this.lockerTreeView.Size = new System.Drawing.Size(481, 712);
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
            this.dgwInventoryItems.Location = new System.Drawing.Point(523, 81);
            this.dgwInventoryItems.Margin = new System.Windows.Forms.Padding(4);
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
            this.dgwInventoryItems.RowHeadersWidth = 51;
            this.dgwInventoryItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwInventoryItems.Size = new System.Drawing.Size(779, 636);
            this.dgwInventoryItems.TabIndex = 9;
            // 
            // barcodeCode
            // 
            this.barcodeCode.HeaderText = "Barcode";
            this.barcodeCode.MinimumWidth = 6;
            this.barcodeCode.Name = "barcodeCode";
            this.barcodeCode.ReadOnly = true;
            this.barcodeCode.Width = 60;
            // 
            // productID
            // 
            this.productID.HeaderText = "Klara ID";
            this.productID.MinimumWidth = 6;
            this.productID.Name = "productID";
            this.productID.ReadOnly = true;
            this.productID.Width = 70;
            // 
            // namn
            // 
            this.namn.HeaderText = "Name";
            this.namn.MinimumWidth = 6;
            this.namn.Name = "namn";
            this.namn.ReadOnly = true;
            this.namn.Width = 170;
            // 
            // amount
            // 
            this.amount.HeaderText = "Amount";
            this.amount.MinimumWidth = 6;
            this.amount.Name = "amount";
            this.amount.ReadOnly = true;
            this.amount.Width = 60;
            // 
            // unit
            // 
            this.unit.HeaderText = "Unit";
            this.unit.MinimumWidth = 6;
            this.unit.Name = "unit";
            this.unit.ReadOnly = true;
            this.unit.Width = 50;
            // 
            // fritext
            // 
            this.fritext.HeaderText = "Note";
            this.fritext.MinimumWidth = 6;
            this.fritext.Name = "fritext";
            this.fritext.ReadOnly = true;
            this.fritext.Width = 120;
            // 
            // TransID
            // 
            this.TransID.HeaderText = "TransID";
            this.TransID.MinimumWidth = 6;
            this.TransID.Name = "TransID";
            this.TransID.ReadOnly = true;
            this.TransID.Visible = false;
            this.TransID.Width = 125;
            // 
            // inventoryID
            // 
            this.inventoryID.HeaderText = "inventoryID";
            this.inventoryID.MinimumWidth = 6;
            this.inventoryID.Name = "inventoryID";
            this.inventoryID.ReadOnly = true;
            this.inventoryID.Visible = false;
            this.inventoryID.Width = 125;
            // 
            // vem
            // 
            this.vem.HeaderText = "vem";
            this.vem.MinimumWidth = 6;
            this.vem.Name = "vem";
            this.vem.ReadOnly = true;
            this.vem.Visible = false;
            this.vem.Width = 125;
            // 
            // nar
            // 
            this.nar.HeaderText = "nar";
            this.nar.MinimumWidth = 6;
            this.nar.Name = "nar";
            this.nar.ReadOnly = true;
            this.nar.Visible = false;
            this.nar.Width = 125;
            // 
            // inventorydone
            // 
            this.inventorydone.HeaderText = "inventorydone";
            this.inventorydone.MinimumWidth = 6;
            this.inventorydone.Name = "inventorydone";
            this.inventorydone.ReadOnly = true;
            this.inventorydone.Width = 125;
            // 
            // inventoryInstructionLabel
            // 
            this.inventoryInstructionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inventoryInstructionLabel.AutoSize = true;
            this.inventoryInstructionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inventoryInstructionLabel.Location = new System.Drawing.Point(19, 325);
            this.inventoryInstructionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.inventoryInstructionLabel.MaximumSize = new System.Drawing.Size(763, 148);
            this.inventoryInstructionLabel.MinimumSize = new System.Drawing.Size(763, 0);
            this.inventoryInstructionLabel.Name = "inventoryInstructionLabel";
            this.inventoryInstructionLabel.Size = new System.Drawing.Size(763, 25);
            this.inventoryInstructionLabel.TabIndex = 0;
            this.inventoryInstructionLabel.Text = "Please select a room or cabinet.";
            this.inventoryInstructionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // startInventoryButton
            // 
            this.startInventoryButton.Enabled = false;
            this.startInventoryButton.Location = new System.Drawing.Point(336, 415);
            this.startInventoryButton.Margin = new System.Windows.Forms.Padding(4);
            this.startInventoryButton.Name = "startInventoryButton";
            this.startInventoryButton.Size = new System.Drawing.Size(73, 28);
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
            this.hideInventoryPanel.Controls.Add(this.panelPrintBarcodes);
            this.hideInventoryPanel.Controls.Add(this.panelBegin);
            this.hideInventoryPanel.Controls.Add(this.startInventoryButton);
            this.hideInventoryPanel.Controls.Add(this.inventoryInstructionLabel);
            this.hideInventoryPanel.Controls.Add(this.barcodeObject);
            this.hideInventoryPanel.Location = new System.Drawing.Point(523, 15);
            this.hideInventoryPanel.Margin = new System.Windows.Forms.Padding(4);
            this.hideInventoryPanel.Name = "hideInventoryPanel";
            this.hideInventoryPanel.Size = new System.Drawing.Size(789, 743);
            this.hideInventoryPanel.TabIndex = 19;
            // 
            // panelPrintBarcodes
            // 
            this.panelPrintBarcodes.Controls.Add(this.lblRbarcodes);
            this.panelPrintBarcodes.Controls.Add(this.lblPrintBarcodes);
            this.panelPrintBarcodes.Controls.Add(this.printBarcodesButton);
            this.panelPrintBarcodes.Controls.Add(this.locationBarcodeButton);
            this.panelPrintBarcodes.Location = new System.Drawing.Point(115, 4);
            this.panelPrintBarcodes.Margin = new System.Windows.Forms.Padding(4);
            this.panelPrintBarcodes.Name = "panelPrintBarcodes";
            this.panelPrintBarcodes.Size = new System.Drawing.Size(595, 666);
            this.panelPrintBarcodes.TabIndex = 28;
            // 
            // lblRbarcodes
            // 
            this.lblRbarcodes.AutoSize = true;
            this.lblRbarcodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRbarcodes.Location = new System.Drawing.Point(91, 190);
            this.lblRbarcodes.Name = "lblRbarcodes";
            this.lblRbarcodes.Size = new System.Drawing.Size(64, 25);
            this.lblRbarcodes.TabIndex = 5;
            this.lblRbarcodes.Text = "label2";
            // 
            // lblPrintBarcodes
            // 
            this.lblPrintBarcodes.AutoSize = true;
            this.lblPrintBarcodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrintBarcodes.Location = new System.Drawing.Point(91, 146);
            this.lblPrintBarcodes.Name = "lblPrintBarcodes";
            this.lblPrintBarcodes.Size = new System.Drawing.Size(64, 25);
            this.lblPrintBarcodes.TabIndex = 4;
            this.lblPrintBarcodes.Text = "label2";
            // 
            // printBarcodesButton
            // 
            this.printBarcodesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.printBarcodesButton.Location = new System.Drawing.Point(129, 260);
            this.printBarcodesButton.Margin = new System.Windows.Forms.Padding(4);
            this.printBarcodesButton.Name = "printBarcodesButton";
            this.printBarcodesButton.Size = new System.Drawing.Size(165, 28);
            this.printBarcodesButton.TabIndex = 2;
            this.printBarcodesButton.Text = "Print barcodes";
            this.printBarcodesButton.UseMnemonic = false;
            this.printBarcodesButton.UseVisualStyleBackColor = true;
            this.printBarcodesButton.Click += new System.EventHandler(this.printBarcodesButton_Click);
            // 
            // locationBarcodeButton
            // 
            this.locationBarcodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.locationBarcodeButton.Location = new System.Drawing.Point(111, 313);
            this.locationBarcodeButton.Margin = new System.Windows.Forms.Padding(4);
            this.locationBarcodeButton.Name = "locationBarcodeButton";
            this.locationBarcodeButton.Size = new System.Drawing.Size(212, 28);
            this.locationBarcodeButton.TabIndex = 3;
            this.locationBarcodeButton.Text = "Print location barcode";
            this.locationBarcodeButton.UseMnemonic = false;
            this.locationBarcodeButton.UseVisualStyleBackColor = true;
            this.locationBarcodeButton.Click += new System.EventHandler(this.locationBarcodeButton_Click);
            // 
            // panelBegin
            // 
            this.panelBegin.Location = new System.Drawing.Point(277, 415);
            this.panelBegin.Margin = new System.Windows.Forms.Padding(4);
            this.panelBegin.Name = "panelBegin";
            this.panelBegin.Size = new System.Drawing.Size(183, 55);
            this.panelBegin.TabIndex = 28;
            // 
            // barcodeObject
            // 
            this.barcodeObject.AztecCodeModuleSize = 4.6926210376994347E+71D;
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
            this.barcodeObject.Location = new System.Drawing.Point(667, 651);
            this.barcodeObject.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.barcodeObject.Size = new System.Drawing.Size(111, 41);
            this.barcodeObject.Symbology = Neodynamic.WinControls.BarcodeProfessional.Symbology.Code128;
            this.barcodeObject.TabIndex = 27;
            this.barcodeObject.Tag = "";
            this.barcodeObject.Text = "Testar";
            this.barcodeObject.TextFont = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.barcodeObject.TextForeColor = System.Drawing.Color.Black;
            this.barcodeObject.TiffCompression = Neodynamic.WinControls.BarcodeProfessional.TiffCompression.LZW;
            this.barcodeObject.Visible = false;
            // 
            // labelStorageName
            // 
            this.labelStorageName.AutoSize = true;
            this.labelStorageName.Location = new System.Drawing.Point(533, 15);
            this.labelStorageName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStorageName.Name = "labelStorageName";
            this.labelStorageName.Size = new System.Drawing.Size(46, 17);
            this.labelStorageName.TabIndex = 10;
            this.labelStorageName.Text = "label1";
            // 
            // txtScannedBarcode
            // 
            this.txtScannedBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScannedBarcode.Enabled = false;
            this.txtScannedBarcode.Location = new System.Drawing.Point(685, 725);
            this.txtScannedBarcode.Margin = new System.Windows.Forms.Padding(4);
            this.txtScannedBarcode.MaximumSize = new System.Drawing.Size(265, 20);
            this.txtScannedBarcode.Name = "txtScannedBarcode";
            this.txtScannedBarcode.Size = new System.Drawing.Size(147, 22);
            this.txtScannedBarcode.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(523, 729);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 17);
            this.label1.TabIndex = 28;
            this.label1.Text = "Last scanned barcodes:";
            // 
            // txtScannedList
            // 
            this.txtScannedList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScannedList.Location = new System.Drawing.Point(676, 723);
            this.txtScannedList.Margin = new System.Windows.Forms.Padding(4);
            this.txtScannedList.Multiline = true;
            this.txtScannedList.Name = "txtScannedList";
            this.txtScannedList.Size = new System.Drawing.Size(297, 27);
            this.txtScannedList.TabIndex = 29;
            // 
            // panelVälj
            // 
            this.panelVälj.Location = new System.Drawing.Point(0, 0);
            this.panelVälj.Name = "panelVälj";
            this.panelVälj.Size = new System.Drawing.Size(200, 100);
            this.panelVälj.TabIndex = 0;
            // 
            // btnBegin
            // 
            this.btnBegin.Location = new System.Drawing.Point(0, 0);
            this.btnBegin.Name = "btnBegin";
            this.btnBegin.Size = new System.Drawing.Size(75, 23);
            this.btnBegin.TabIndex = 0;
            // 
            // rbtNollställ
            // 
            this.rbtNollställ.Location = new System.Drawing.Point(0, 0);
            this.rbtNollställ.Name = "rbtNollställ";
            this.rbtNollställ.Size = new System.Drawing.Size(104, 24);
            this.rbtNollställ.TabIndex = 0;
            // 
            // rbtUtrStre
            // 
            this.rbtUtrStre.Location = new System.Drawing.Point(0, 0);
            this.rbtUtrStre.Name = "rbtUtrStre";
            this.rbtUtrStre.Size = new System.Drawing.Size(104, 24);
            this.rbtUtrStre.TabIndex = 0;
            // 
            // rbtProdStr
            // 
            this.rbtProdStr.Location = new System.Drawing.Point(0, 0);
            this.rbtProdStr.Name = "rbtProdStr";
            this.rbtProdStr.Size = new System.Drawing.Size(104, 24);
            this.rbtProdStr.TabIndex = 0;
            // 
            // lblTitel
            // 
            this.lblTitel.Location = new System.Drawing.Point(0, 0);
            this.lblTitel.Name = "lblTitel";
            this.lblTitel.Size = new System.Drawing.Size(100, 23);
            this.lblTitel.TabIndex = 0;
            // 
            // rbtInventera
            // 
            this.rbtInventera.Location = new System.Drawing.Point(0, 0);
            this.rbtInventera.Name = "rbtInventera";
            this.rbtInventera.Size = new System.Drawing.Size(104, 24);
            this.rbtInventera.TabIndex = 0;
            // 
            // rbtFlytta
            // 
            this.rbtFlytta.Location = new System.Drawing.Point(0, 0);
            this.rbtFlytta.Name = "rbtFlytta";
            this.rbtFlytta.Size = new System.Drawing.Size(104, 24);
            this.rbtFlytta.TabIndex = 0;
            // 
            // panelNollställ
            // 
            this.panelNollställ.Location = new System.Drawing.Point(0, 0);
            this.panelNollställ.Name = "panelNollställ";
            this.panelNollställ.Size = new System.Drawing.Size(200, 100);
            this.panelNollställ.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnNollstall
            // 
            this.btnNollstall.Location = new System.Drawing.Point(0, 0);
            this.btnNollstall.Name = "btnNollstall";
            this.btnNollstall.Size = new System.Drawing.Size(75, 23);
            this.btnNollstall.TabIndex = 0;
            // 
            // lblBeskrivning
            // 
            this.lblBeskrivning.Location = new System.Drawing.Point(0, 0);
            this.lblBeskrivning.Name = "lblBeskrivning";
            this.lblBeskrivning.Size = new System.Drawing.Size(100, 23);
            this.lblBeskrivning.TabIndex = 0;
            // 
            // lblNollställ
            // 
            this.lblNollställ.Location = new System.Drawing.Point(0, 0);
            this.lblNollställ.Name = "lblNollställ";
            this.lblNollställ.Size = new System.Drawing.Size(100, 23);
            this.lblNollställ.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(0, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(0, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 0;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(0, 0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 0;
            // 
            // radioButton3
            // 
            this.radioButton3.Location = new System.Drawing.Point(0, 0);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(104, 24);
            this.radioButton3.TabIndex = 0;
            // 
            // radioButton4
            // 
            this.radioButton4.Location = new System.Drawing.Point(0, 0);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(104, 24);
            this.radioButton4.TabIndex = 0;
            // 
            // radioButton5
            // 
            this.radioButton5.Location = new System.Drawing.Point(0, 0);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(104, 24);
            this.radioButton5.TabIndex = 0;
            // 
            // panelNoll
            // 
            this.panelNoll.Controls.Add(this.btnNoll);
            this.panelNoll.Location = new System.Drawing.Point(-1, 1);
            this.panelNoll.Margin = new System.Windows.Forms.Padding(4);
            this.panelNoll.Name = "panelNoll";
            this.panelNoll.Size = new System.Drawing.Size(1327, 774);
            this.panelNoll.TabIndex = 29;
            // 
            // btnNoll
            // 
            this.btnNoll.Location = new System.Drawing.Point(1148, 687);
            this.btnNoll.Margin = new System.Windows.Forms.Padding(4);
            this.btnNoll.Name = "btnNoll";
            this.btnNoll.Size = new System.Drawing.Size(100, 28);
            this.btnNoll.TabIndex = 1;
            this.btnNoll.Text = "Nollställ";
            this.btnNoll.UseVisualStyleBackColor = true;
            this.btnNoll.Click += new System.EventHandler(this.btnNoll_Click);
            // 
            // btnHuvudMeny
            // 
            this.btnHuvudMeny.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuvudMeny.Location = new System.Drawing.Point(991, 724);
            this.btnHuvudMeny.Margin = new System.Windows.Forms.Padding(4);
            this.btnHuvudMeny.Name = "btnHuvudMeny";
            this.btnHuvudMeny.Size = new System.Drawing.Size(100, 28);
            this.btnHuvudMeny.TabIndex = 30;
            this.btnHuvudMeny.Text = "Menu";
            this.btnHuvudMeny.UseVisualStyleBackColor = true;
            this.btnHuvudMeny.Click += new System.EventHandler(this.btnHuvudMeny_Click);
            // 
            // FormInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1325, 773);
            this.Controls.Add(this.btnHuvudMeny);
            this.Controls.Add(this.panelNoll);
            this.Controls.Add(this.txtScannedList);
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
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1341, 806);
            this.Name = "FormInventory";
            this.Text = "Inventory";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwInventoryItems)).EndInit();
            this.hideInventoryPanel.ResumeLayout(false);
            this.hideInventoryPanel.PerformLayout();
            this.panelPrintBarcodes.ResumeLayout(false);
            this.panelPrintBarcodes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelNoll.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panelVälj;
        private System.Windows.Forms.TextBox txtScannedList;
        private System.Windows.Forms.RadioButton rbtNollställ;
        private System.Windows.Forms.RadioButton rbtUtrStre;
        private System.Windows.Forms.RadioButton rbtProdStr;
        private System.Windows.Forms.Label lblTitel;
        private System.Windows.Forms.RadioButton rbtInventera;
        private System.Windows.Forms.RadioButton rbtFlytta;
        private System.Windows.Forms.Button btnBegin;
        private System.Windows.Forms.Panel panelNollställ;
        private System.Windows.Forms.Label lblNollställ;
        private System.Windows.Forms.Button btnNollstall;
        private System.Windows.Forms.Label lblBeskrivning;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.Panel panelPrintBarcodes;
        private System.Windows.Forms.Panel panelNoll;
        private System.Windows.Forms.Button btnNoll;
        private System.Windows.Forms.Button btnHuvudMeny;
        private System.Windows.Forms.Panel panelBegin;
        private System.Windows.Forms.Label lblRbarcodes;
        private System.Windows.Forms.Label lblPrintBarcodes;
    }
}