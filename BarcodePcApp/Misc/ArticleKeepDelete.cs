using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BarcodePcApp
{
    public partial class ArticleKeepDelete : UserControl
    {
        /// <summary>
        /// The ID for the Kpinvtrans table in the database.
        /// </summary>
        public int KpinvtransID { get; set; }

        public ArticleKeepDelete(string name, double amount, string unit, string barcode, int transid)
        {
            InitializeComponent();
            KpinvtransID = transid;
            labelName.Text = name;
            labelAmount.Text = amount + " " + unit.Trim();
            labelBarcode.Text = barcode;

            if (labelName.Height > 13)
            {
                this.Height += labelName.Height - 13;
            }
        }

        /// <summary>
        /// Checks to see if the user has selected Delete or Keep on the article.
        /// </summary>
        /// <returns>True if the Delete radio button is checked, False if the Keep radio button is checked.</returns>
        public bool DeleteSelected()
        {
            return radioButtonDelete.Checked;
        }
    }
}
