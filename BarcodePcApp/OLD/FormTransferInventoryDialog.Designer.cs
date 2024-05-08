namespace BarcodePcApp
{
    partial class FormTransferInventoryDialog
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
            this.questionLabel = new System.Windows.Forms.Label();
            this.dgwTransferItem = new System.Windows.Forms.DataGridView();
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.transferButton = new System.Windows.Forms.Button();
            this.userLocationLabel = new System.Windows.Forms.Label();
            this.itemLocationLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgwTransferItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // questionLabel
            // 
            this.questionLabel.AutoSize = true;
            this.questionLabel.Location = new System.Drawing.Point(13, 13);
            this.questionLabel.MaximumSize = new System.Drawing.Size(585, 0);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(552, 13);
            this.questionLabel.TabIndex = 0;
            this.questionLabel.Text = "It appears that the article you scanned belongs somewere else. Would you like to " +
                "transfer it to your current location?";
            // 
            // dgwTransferItem
            // 
            this.dgwTransferItem.AllowUserToAddRows = false;
            this.dgwTransferItem.AllowUserToDeleteRows = false;
            this.dgwTransferItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwTransferItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.barcodeCode,
            this.productID,
            this.namn,
            this.amount,
            this.unit,
            this.fritext,
            this.TransID,
            this.inventoryID,
            this.vem,
            this.nar});
            this.dgwTransferItem.Location = new System.Drawing.Point(12, 72);
            this.dgwTransferItem.MultiSelect = false;
            this.dgwTransferItem.Name = "dgwTransferItem";
            this.dgwTransferItem.ReadOnly = true;
            this.dgwTransferItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwTransferItem.Size = new System.Drawing.Size(586, 41);
            this.dgwTransferItem.TabIndex = 10;
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
            this.productID.Width = 83;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Article:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Current location in database:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(360, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Your current location:";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(433, 293);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 14;
            this.cancelButton.Text = "Return";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // transferButton
            // 
            this.transferButton.Location = new System.Drawing.Point(514, 293);
            this.transferButton.Name = "transferButton";
            this.transferButton.Size = new System.Drawing.Size(75, 23);
            this.transferButton.TabIndex = 15;
            this.transferButton.Text = "Transfer";
            this.transferButton.UseVisualStyleBackColor = true;
            this.transferButton.Click += new System.EventHandler(this.transferButton_Click);
            // 
            // userLocationLabel
            // 
            this.userLocationLabel.AutoSize = true;
            this.userLocationLabel.Location = new System.Drawing.Point(361, 162);
            this.userLocationLabel.Name = "userLocationLabel";
            this.userLocationLabel.Size = new System.Drawing.Size(127, 13);
            this.userLocationLabel.TabIndex = 17;
            this.userLocationLabel.Text = "Current inventory location";
            // 
            // itemLocationLabel
            // 
            this.itemLocationLabel.AutoSize = true;
            this.itemLocationLabel.Location = new System.Drawing.Point(19, 162);
            this.itemLocationLabel.Name = "itemLocationLabel";
            this.itemLocationLabel.Size = new System.Drawing.Size(99, 13);
            this.itemLocationLabel.TabIndex = 18;
            this.itemLocationLabel.Text = "Location of product";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BarcodePcApp.Properties.Resources.arrow;
            this.pictureBox1.InitialImage = global::BarcodePcApp.Properties.Resources.arrow;
            this.pictureBox1.Location = new System.Drawing.Point(211, 153);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(118, 106);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // FormTransferInventoryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 353);
            this.ControlBox = false;
            this.Controls.Add(this.itemLocationLabel);
            this.Controls.Add(this.userLocationLabel);
            this.Controls.Add(this.transferButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgwTransferItem);
            this.Controls.Add(this.questionLabel);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormTransferInventoryDialog";
            this.Text = "Transfer an item";
            ((System.ComponentModel.ISupportInitialize)(this.dgwTransferItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label questionLabel;
        private System.Windows.Forms.DataGridView dgwTransferItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button transferButton;
        private System.Windows.Forms.Label userLocationLabel;
        private System.Windows.Forms.Label itemLocationLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
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
    }
}