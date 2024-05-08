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
            this.questionLabel.Location = new System.Drawing.Point(17, 16);
            this.questionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.questionLabel.MaximumSize = new System.Drawing.Size(780, 0);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(638, 17);
            this.questionLabel.TabIndex = 0;
            this.questionLabel.Text = "It appears that the article you scanned belongs somewere else. Would you like to " +
    "discard it anyway?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 75);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Current location in database:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(481, 75);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "Your current location:";
            // 
            // DiscardButton
            // 
            this.DiscardButton.Location = new System.Drawing.Point(556, 271);
            this.DiscardButton.Margin = new System.Windows.Forms.Padding(4);
            this.DiscardButton.Name = "DiscardButton";
            this.DiscardButton.Size = new System.Drawing.Size(100, 28);
            this.DiscardButton.TabIndex = 14;
            this.DiscardButton.Text = "Discard";
            this.DiscardButton.UseVisualStyleBackColor = true;
            this.DiscardButton.Click += new System.EventHandler(this.DiscardButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(664, 271);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(100, 28);
            this.CancelButton.TabIndex = 15;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // userLocationLabel
            // 
            this.userLocationLabel.AutoSize = true;
            this.userLocationLabel.Location = new System.Drawing.Point(481, 108);
            this.userLocationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.userLocationLabel.Name = "userLocationLabel";
            this.userLocationLabel.Size = new System.Drawing.Size(170, 17);
            this.userLocationLabel.TabIndex = 17;
            this.userLocationLabel.Text = "Current inventory location";
            // 
            // itemLocationLabel
            // 
            this.itemLocationLabel.AutoSize = true;
            this.itemLocationLabel.Location = new System.Drawing.Point(17, 108);
            this.itemLocationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.itemLocationLabel.Name = "itemLocationLabel";
            this.itemLocationLabel.Size = new System.Drawing.Size(130, 17);
            this.itemLocationLabel.TabIndex = 18;
            this.itemLocationLabel.Text = "Location of product";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BarcodePcApp.Properties.Resources.kassera;
            this.pictureBox1.InitialImage = global::BarcodePcApp.Properties.Resources.arrow;
            this.pictureBox1.Location = new System.Drawing.Point(257, 47);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(157, 130);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // FormDiscardCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 348);
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
            this.Margin = new System.Windows.Forms.Padding(4);
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