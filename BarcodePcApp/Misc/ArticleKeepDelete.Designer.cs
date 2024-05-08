namespace BarcodePcApp
{
    partial class ArticleKeepDelete
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
            this.labelName = new System.Windows.Forms.Label();
            this.labelAmount = new System.Windows.Forms.Label();
            this.radioButtonDelete = new System.Windows.Forms.RadioButton();
            this.radioButtonKeep = new System.Windows.Forms.RadioButton();
            this.labelBarcode = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(3, 6);
            this.labelName.MaximumSize = new System.Drawing.Size(155, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(112, 13);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Formaldehyd 2131231";
            // 
            // labelAmount
            // 
            this.labelAmount.Location = new System.Drawing.Point(164, 6);
            this.labelAmount.Name = "labelAmount";
            this.labelAmount.Size = new System.Drawing.Size(92, 13);
            this.labelAmount.TabIndex = 1;
            this.labelAmount.Text = "200 kg";
            // 
            // radioButtonDelete
            // 
            this.radioButtonDelete.AutoSize = true;
            this.radioButtonDelete.Checked = true;
            this.radioButtonDelete.Location = new System.Drawing.Point(374, 6);
            this.radioButtonDelete.Name = "radioButtonDelete";
            this.radioButtonDelete.Size = new System.Drawing.Size(14, 13);
            this.radioButtonDelete.TabIndex = 2;
            this.radioButtonDelete.TabStop = true;
            this.radioButtonDelete.UseVisualStyleBackColor = true;
            // 
            // radioButtonKeep
            // 
            this.radioButtonKeep.AutoSize = true;
            this.radioButtonKeep.Location = new System.Drawing.Point(337, 6);
            this.radioButtonKeep.Name = "radioButtonKeep";
            this.radioButtonKeep.Size = new System.Drawing.Size(14, 13);
            this.radioButtonKeep.TabIndex = 3;
            this.radioButtonKeep.TabStop = true;
            this.radioButtonKeep.UseVisualStyleBackColor = true;
            // 
            // labelBarcode
            // 
            this.labelBarcode.Location = new System.Drawing.Point(262, 6);
            this.labelBarcode.Name = "labelBarcode";
            this.labelBarcode.Size = new System.Drawing.Size(68, 13);
            this.labelBarcode.TabIndex = 4;
            this.labelBarcode.Text = "233223";
            // 
            // ArticleKeepDelete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.labelBarcode);
            this.Controls.Add(this.radioButtonKeep);
            this.Controls.Add(this.radioButtonDelete);
            this.Controls.Add(this.labelAmount);
            this.Controls.Add(this.labelName);
            this.Name = "ArticleKeepDelete";
            this.Size = new System.Drawing.Size(398, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelAmount;
        private System.Windows.Forms.RadioButton radioButtonDelete;
        private System.Windows.Forms.RadioButton radioButtonKeep;
        private System.Windows.Forms.Label labelBarcode;
    }
}
