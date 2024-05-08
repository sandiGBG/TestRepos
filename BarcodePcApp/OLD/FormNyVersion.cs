using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Diagnostics;


namespace BarcodePcApp
{
    public partial class FormNyVersion : Form
    {
        public int pcAppVer = 0;
        public int serverPcAppVer = 0;

        public FormNyVersion()
        {
            InitializeComponent();
        }

        private void buttonJa_Click(object sender, EventArgs e)
        {
            // Hämta installationsfilen från webbservern till pc:n
            barcode.BarcodeService bc = FormMain.getBarcodeService();

            try
            {
                byte[] decbuff = Convert.FromBase64String(bc.GetPcAppInst());   // Läs från webbservice och konvertera från base64
                FileStream outFile = new FileStream(FormMain.pcTmpPath + "BarcodePcAppSetup.msi", FileMode.Create, FileAccess.Write);
                outFile.Write(decbuff, 0, decbuff.Length);  // skriv datan till filen
                outFile.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show("Ingen kontakt med webbservern!\r\n\nKontrollera att internetanslutningen fungerar.\r\n\nDetta felmeddelande rapporterades:\r\n\n" + err.Message,"GetPcAppInst");
                if (!FormMain.localhost)
                {
                    Application.Exit();
                }
                return;
            }

            // Starta installationsprogrammet
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = FormMain.pcTmpPath + "BarcodePcAppSetup.msi";
                proc.Start();
            }
            catch (Exception err)
            {
                MessageBox.Show("Det gick inte att köra installationsprogrammet.\r\n\nDetta felmeddelande rapporterades:\r\n\n" + err.Message, "Fel vid installation.");
                if (!FormMain.localhost)
                {
                    Application.Exit();
                }
                return;
            }

            // Stoppa programmet
            Application.Exit();

        }


        private void buttonNej_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormNyVersion_Load(object sender, EventArgs e)
        {
            m_labelNuvarandeVer.Text = Convert.ToString(Math.Round(((pcAppVer / 100.0)), 2)).Replace(",", ".");
            m_labelNyVer.Text = Convert.ToString(Math.Round(((serverPcAppVer / 100.0)), 2)).Replace(",", ".");
        }

    }
}