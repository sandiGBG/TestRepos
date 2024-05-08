namespace BarcodePcApp.Misc
{
    partial class printBarcodeItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.printCheckBox = new System.Windows.Forms.CheckBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.amountLabel = new System.Windows.Forms.Label();
            //this.noteLabel = new System.Windows.Forms.Label();
            this.noteLabel = new System.Windows.Forms.TextBox();
            this.barcodeLabel = new System.Windows.Forms.Label();
            this.casLabel = new System.Windows.Forms.Label();
            this.levLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // printCheckBox
            // 
            this.printCheckBox.AutoSize = true;
            this.printCheckBox.Location = new System.Drawing.Point(8, 9);
            this.printCheckBox.Name = "printCheckBox";
            this.printCheckBox.Size = new System.Drawing.Size(15, 14);
            this.printCheckBox.TabIndex = 0;
            this.printCheckBox.UseVisualStyleBackColor = true;
            // 
            // barcodeLabel
            // 
            this.barcodeLabel.AutoSize = true;
            this.barcodeLabel.Location = new System.Drawing.Point(29, 10);
            this.barcodeLabel.Name = "barcodeLabel";
            this.barcodeLabel.Size = new System.Drawing.Size(47, 13);
            this.barcodeLabel.TabIndex = 4;
            this.barcodeLabel.Text = "Barcode";
            //
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(100, 10);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(100, 13);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Name";
            // 
            // levLabel
            // 
            this.levLabel.AutoSize = true;
            this.levLabel.Location = new System.Drawing.Point(270, 10);
            this.levLabel.Name = "levLabel";
            this.levLabel.Size = new System.Drawing.Size(80, 13);
            this.levLabel.TabIndex = 3;
            this.levLabel.Text = "Lev";
            // 
            // casLabel
            // 
            this.casLabel.AutoSize = true;
            this.casLabel.Location = new System.Drawing.Point(470, 10);
            this.casLabel.Name = "casLabel";
            this.casLabel.Size = new System.Drawing.Size(43, 13);
            this.casLabel.TabIndex = 5;
            this.casLabel.Text = "Cas";
            // 
            // amountLabel
            // 
            this.amountLabel.AutoSize = true;
            this.amountLabel.Location = new System.Drawing.Point(600, 10);
            this.amountLabel.Name = "amountLabel";
            this.amountLabel.Size = new System.Drawing.Size(43, 13);
            this.amountLabel.TabIndex = 2;
            this.amountLabel.Text = "Amount";
            //

            // noteLabel
            // 
            this.noteLabel.AutoSize = true;
            this.noteLabel.Location = new System.Drawing.Point(690, 10);
            this.noteLabel.Name = "noteLabel";
            this.noteLabel.Size = new System.Drawing.Size(80, 13);
            this.noteLabel.TabIndex = 3;
            this.noteLabel.Text = "Note";
            //  
            // printBarcodeItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.barcodeLabel);
            this.Controls.Add(this.noteLabel);
            this.Controls.Add(this.amountLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.casLabel);
            this.Controls.Add(this.levLabel);
            this.Controls.Add(this.printCheckBox);
            this.Name = "printBarcodeItem";
            this.Size = new System.Drawing.Size(777, 32);
            this.ResumeLayout(false);
            this.PerformLayout();



        }

        #endregion

        private System.Windows.Forms.CheckBox printCheckBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label amountLabel;
        //private System.Windows.Forms.Label noteLabel;
        private System.Windows.Forms.Label barcodeLabel;
        private System.Windows.Forms.TextBox noteLabel;
        private System.Windows.Forms.Label casLabel;
        private System.Windows.Forms.Label levLabel;
    }
}
