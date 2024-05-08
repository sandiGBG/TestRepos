using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BarcodePcApp
{
    public partial class UCEtikettArt : UserControl
    {
        FormMain m_Parent = null;

        public UCEtikettArt()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Apa");
            //m_Parent.Spara();
        }

        public void SetParent(FormMain p_Parent)
        {
            m_Parent = p_Parent;
        }

        public void SetButtonText(string p_sText)
        {
            this.button1.Text = p_sText;
        }
    }
}
