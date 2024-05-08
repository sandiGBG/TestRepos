using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BarcodePcApp
{
    public partial class FormPopoup : Form
    {
        public string longurl = "";

        public FormPopoup()
        {
            InitializeComponent();
            //string longurl = "http://aqbanjaluka/alphaquest/AQKlarprod/pcapp_jme.cfm?";  //lokalt
            //string longurl = "http://aqtest.port.se/alphaquest/AQKlarprod/pcapp_jme.cfm?";  //testserver
            //string longurl = "http://aqbanjaluka/alphaquest/AQKlarprod/pcapp_prod_jme.cfm?";
            //var uriBuilder = new UriBuilder(longurl);
            //var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
            //lbl.Text = "Sandi, du har kontakt med mig! JUPPPPPPIIIIIIII:";
            //query["cas"] = "12125-02-9";
           
            ////    m_sCAS.ToString();
            //query["kemds"] = "biokem";
            //    //kemiDb;
            //query["id"] = "9167";
            //    //Convert.ToString(m_nProdid);
            //uriBuilder.Query = query.ToString();
            //longurl = uriBuilder.ToString();
            //MessageBox.Show(longurl);

            //System.Diagnostics.Process.Start(longurl);
            //form.Show(this); // if you need non-modal window
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //string longurl = "http://aqbanjaluka/alphaquest/AQKlarprod/pcapp_jme.cfm?";  //lokalt
            //string longurl = "http://aqtest.port.se/alphaquest/AQKlarprod/pcapp_jme.cfm?";  //testserver
            longurl = "http://aqbanjaluka/alphaquest/AQKlarprod/pcapp_prod_jme.cfm?";
            var uriBuilder = new UriBuilder(longurl);
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
            lbl.Text = "Sandi, du har kontakt med mig! JUPPPPPPIIIIIIII:";
            query["cas"] = "12125-02-9";

            //    m_sCAS.ToString();
            query["kemds"] = "biokem";
            //kemiDb;
            query["id"] = "9167";
            //Convert.ToString(m_nProdid);
            uriBuilder.Query = query.ToString();
            longurl = uriBuilder.ToString();
            webBrowser1.Navigate("http://aqbanjaluka/alphaquest/AQKlarprod/pcapp_prod_jme.cfm?cas=12125-02-9&kemds=BIOKEM&id=9167");
            //MessageBox.Show(longurl);

        }


        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            toolStrip1.Text = longurl.ToString();
            toolStrip1.Visible = false;

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(longurl);
        }


    }
}
