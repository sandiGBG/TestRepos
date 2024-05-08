namespace BarcodePcApp
{
    partial class FormDiscardCheck
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DiscardButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.userLocationLabel = new System.Windows.Forms.Label();
            this.itemLocationLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // questionLabel
            // 
            this.questionLabel.AutoSize = true;
            this.questionLabel.Location = new System.Drawing.Point(13, 13);
            this.questionLabel.MaximumSize = new System.Drawing.Size(585, 0);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(479, 13);
            this.questionLabel.TabIndex = 0;
            this.questionLabel.Text = "It appears that the article you scanned belongs somewere else. Would you like to " +
    "discard it anyway?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Current location in database:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(361, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Your current location:";
            // 
            // DiscardButton
            // 
            this.DiscardButton.Location = new System.Drawing.Point(417, 202);
            this.DiscardButton.Name = "DiscardButton";
            this.DiscardButton.Size = new System.Drawing.Size(75, 23);
            this.DiscardButton.TabIndex = 14;
            this.DiscardButton.Text = "Discard";
            this.DiscardButton.UseVisualStyleBackColor = true;
            this.DiscardButton.Click += new System.EventHandler(this.DiscardButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(498, 202);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 15;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // userLocationLabel
            // 
            this.userLocationLabel.AutoSize = true;
            this.userLocationLabel.Location = new System.Drawing.Point(361, 88);
            this.userLocationLabel.Name = "userLocationLabel";
            this.userLocationLabel.Size = new System.Drawing.Size(127, 13);
            this.userLocationLabel.TabIndex = 17;
            this.userLocationLabel.Text = "Current inventory location";
            // 
            // itemLocationLabel
            // 
            this.itemLocationLabel.AutoSize = true;
            this.itemLocationLabel.Location = new System.Drawing.Point(13, 88);
            this.itemLocationLabel.Name = "itemLocationLabel";
            this.itemLocationLabel.Size = new System.Drawing.Size(99, 13);
            this.itemLocationLabel.TabIndex = 18;
            this.itemLocationLabel.Text = "Location of product";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BarcodePcApp.Properties.Resources.discard;
            this.pictureBox1.InitialImage = global::BarcodePcApp.Properties.Resources.arrow;
            this.pictureBox1.Location = new System.Drawing.Point(193, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(118, 106);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // FormDiscardCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 236);
            this.ControlBox = false;
            this.Controls.Add(this.itemLocationLabel);
            this.Controls.Add(this.userLocationLabel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.DiscardButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.questionLabel);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormDiscardCheck";
            this.Text = "Discard a product";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label questionLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button DiscardButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label userLocationLabel;
        private System.Windows.Forms.Label itemLocationLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}