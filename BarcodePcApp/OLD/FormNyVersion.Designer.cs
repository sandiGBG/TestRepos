namespace BarcodePcApp
{
    partial class FormNyVersion
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonJa = new System.Windows.Forms.Button();
            this.buttonNej = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_labelNuvarande = new System.Windows.Forms.Label();
            this.m_labelNy = new System.Windows.Forms.Label();
            this.m_labelNuvarandeVer = new System.Windows.Forms.Label();
            this.m_labelNyVer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ny version av programmet måste installeras.";
            // 
            // buttonJa
            // 
            this.buttonJa.Location = new System.Drawing.Point(29, 147);
            this.buttonJa.Name = "buttonJa";
            this.buttonJa.Size = new System.Drawing.Size(75, 23);
            this.buttonJa.TabIndex = 1;
            this.buttonJa.Text = "Ja";
            this.buttonJa.UseVisualStyleBackColor = true;
            this.buttonJa.Click += new System.EventHandler(this.buttonJa_Click);
            // 
            // buttonNej
            // 
            this.buttonNej.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonNej.Location = new System.Drawing.Point(128, 147);
            this.buttonNej.Name = "buttonNej";
            this.buttonNej.Size = new System.Drawing.Size(75, 23);
            this.buttonNej.TabIndex = 2;
            this.buttonNej.Text = "Nej";
            this.buttonNej.UseVisualStyleBackColor = true;
            this.buttonNej.Click += new System.EventHandler(this.buttonNej_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Vill prova att köra automatisk installation?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(221, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "OBS. Administratörsrättigheter krävs för detta.";
            // 
            // m_labelNuvarande
            // 
            this.m_labelNuvarande.AutoSize = true;
            this.m_labelNuvarande.Location = new System.Drawing.Point(56, 49);
            this.m_labelNuvarande.Name = "m_labelNuvarande";
            this.m_labelNuvarande.Size = new System.Drawing.Size(100, 13);
            this.m_labelNuvarande.TabIndex = 7;
            this.m_labelNuvarande.Text = "Nuvarande version:";
            // 
            // m_labelNy
            // 
            this.m_labelNy.AutoSize = true;
            this.m_labelNy.Location = new System.Drawing.Point(96, 67);
            this.m_labelNy.Name = "m_labelNy";
            this.m_labelNy.Size = new System.Drawing.Size(60, 13);
            this.m_labelNy.TabIndex = 8;
            this.m_labelNy.Text = "Ny version:";
            // 
            // m_labelNuvarandeVer
            // 
            this.m_labelNuvarandeVer.AutoSize = true;
            this.m_labelNuvarandeVer.Location = new System.Drawing.Point(156, 49);
            this.m_labelNuvarandeVer.Name = "m_labelNuvarandeVer";
            this.m_labelNuvarandeVer.Size = new System.Drawing.Size(0, 13);
            this.m_labelNuvarandeVer.TabIndex = 9;
            // 
            // m_labelNyVer
            // 
            this.m_labelNyVer.AutoSize = true;
            this.m_labelNyVer.Location = new System.Drawing.Point(156, 67);
            this.m_labelNyVer.Name = "m_labelNyVer";
            this.m_labelNyVer.Size = new System.Drawing.Size(0, 13);
            this.m_labelNyVer.TabIndex = 10;
            // 
            // FormNyVersion
            // 
            this.AcceptButton = this.buttonJa;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonNej;
            this.ClientSize = new System.Drawing.Size(263, 238);
            this.ControlBox = false;
            this.Controls.Add(this.m_labelNyVer);
            this.Controls.Add(this.m_labelNuvarandeVer);
            this.Controls.Add(this.m_labelNuvarande);
            this.Controls.Add(this.m_labelNy);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonNej);
            this.Controls.Add(this.buttonJa);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNyVersion";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ny version av programmet.";
            this.Load += new System.EventHandler(this.FormNyVersion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonJa;
        private System.Windows.Forms.Button buttonNej;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label m_labelNuvarande;
        private System.Windows.Forms.Label m_labelNy;
        private System.Windows.Forms.Label m_labelNuvarandeVer;
        private System.Windows.Forms.Label m_labelNyVer;
    }
}