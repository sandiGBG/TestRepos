namespace BarcodePcApp
{
    partial class FormNollstall
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
            this.lblNollstall = new System.Windows.Forms.Label();
            this.btnollstall = new System.Windows.Forms.Button();
            this.grpTitel = new System.Windows.Forms.GroupBox();
            this.grpAvd = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnAvbryt = new System.Windows.Forms.Button();
            this.panelNoll = new System.Windows.Forms.Panel();
            this.lblPanel = new System.Windows.Forms.Label();
            this.grpTitel.SuspendLayout();
            this.grpAvd.SuspendLayout();
            this.panelNoll.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNollstall
            // 
            this.lblNollstall.AutoSize = true;
            this.lblNollstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNollstall.Location = new System.Drawing.Point(26, 16);
            this.lblNollstall.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNollstall.Name = "lblNollstall";
            this.lblNollstall.Size = new System.Drawing.Size(422, 26);
            this.lblNollstall.TabIndex = 0;
            this.lblNollstall.Text = "Nollställ produkterna för följande avdelning";
            // 
            // btnollstall
            // 
            this.btnollstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnollstall.Location = new System.Drawing.Point(1055, 8041);
            this.btnollstall.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnollstall.Name = "btnollstall";
            this.btnollstall.Size = new System.Drawing.Size(100, 27);
            this.btnollstall.TabIndex = 2;
            this.btnollstall.Text = "Återställ";
            this.btnollstall.UseVisualStyleBackColor = true;
            this.btnollstall.Click += new System.EventHandler(this.btnollstall_Click);
            // 
            // grpTitel
            // 
            this.grpTitel.Controls.Add(this.lblNollstall);
            this.grpTitel.Location = new System.Drawing.Point(13, 13);
            this.grpTitel.Name = "grpTitel";
            this.grpTitel.Size = new System.Drawing.Size(500, 60);
            this.grpTitel.TabIndex = 2;
            this.grpTitel.TabStop = false;
            // 
            // grpAvd
            // 
            this.grpAvd.AutoSize = true;
            this.grpAvd.Controls.Add(this.button1);
            this.grpAvd.Controls.Add(this.button2);
            this.grpAvd.Controls.Add(this.btnAvbryt);
            this.grpAvd.Controls.Add(this.btnollstall);
            this.grpAvd.Location = new System.Drawing.Point(13, 80);
            this.grpAvd.Name = "grpAvd";
            this.grpAvd.Size = new System.Drawing.Size(1174, 8086);
            this.grpAvd.TabIndex = 3;
            this.grpAvd.TabStop = false;
            this.grpAvd.Enter += new System.EventHandler(this.grpAvd_Enter);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(537, 4836);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 27);
            this.button1.TabIndex = 6;
            this.button1.Text = "Åvbryt";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(537, 4840);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 27);
            this.button2.TabIndex = 5;
            this.button2.Text = "Återställ";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnAvbryt
            // 
            this.btnAvbryt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAvbryt.Location = new System.Drawing.Point(1055, 8011);
            this.btnAvbryt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAvbryt.Name = "btnAvbryt";
            this.btnAvbryt.Size = new System.Drawing.Size(100, 27);
            this.btnAvbryt.TabIndex = 4;
            this.btnAvbryt.Text = "Åvbryt";
            this.btnAvbryt.UseVisualStyleBackColor = true;
            this.btnAvbryt.Click += new System.EventHandler(this.btnAvbtyt_Click);
            // 
            // panelNoll
            // 
            this.panelNoll.Controls.Add(this.lblPanel);
            this.panelNoll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNoll.Location = new System.Drawing.Point(0, 0);
            this.panelNoll.Name = "panelNoll";
            this.panelNoll.Size = new System.Drawing.Size(1865, 8153);
            this.panelNoll.TabIndex = 4;
            // 
            // lblPanel
            // 
            this.lblPanel.AutoSize = true;
            this.lblPanel.Location = new System.Drawing.Point(276, 171);
            this.lblPanel.Name = "lblPanel";
            this.lblPanel.Size = new System.Drawing.Size(105, 13);
            this.lblPanel.TabIndex = 0;
            this.lblPanel.Text = "Sidan laddas...Vänta";
            // 
            // FormNollstall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1882, 534);
            this.Controls.Add(this.panelNoll);
            this.Controls.Add(this.grpAvd);
            this.Controls.Add(this.grpTitel);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximumSize = new System.Drawing.Size(1899, 573);
            this.MinimumSize = new System.Drawing.Size(545, 573);
            this.Name = "FormNollstall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "FormÅterställ";
            this.Load += new System.EventHandler(this.FormNollstall_Load);
            this.grpTitel.ResumeLayout(false);
            this.grpTitel.PerformLayout();
            this.grpAvd.ResumeLayout(false);
            this.panelNoll.ResumeLayout(false);
            this.panelNoll.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNollstall;
        private System.Windows.Forms.Button btnollstall;
        private System.Windows.Forms.GroupBox grpTitel;
        private System.Windows.Forms.GroupBox grpAvd;
        private System.Windows.Forms.Button btnAvbryt;
        private System.Windows.Forms.Panel panelNoll;
        private System.Windows.Forms.Label lblPanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}