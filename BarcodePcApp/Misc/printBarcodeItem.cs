using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BarcodePcApp.Misc
{
    public partial class printBarcodeItem : UserControl
    {
        public string barcode
        {
            get
            {
                if (barcodeLabel.Text == "-")
                {
                    return "";
                }
                else
                {
                    return barcodeLabel.Text;
                }     
            }
            set
            {
                barcodeLabel.Text = value;
            }
        }

        public bool print
        {
            get
            {
                return printCheckBox.Checked;
            }
            set
            {
                printCheckBox.Checked = value;
            }
        }

        public string note
        {
            get
            {
                return noteLabel.Text;
            }
            set
            {
                noteLabel.Text = value;
            }
        }

        public string CAS
        {
            get
            {
                if (casLabel.Text == "")
                {
                    return "";
                }
                else
                {
                    return casLabel.Text;
                }
            }
            set
            {
                casLabel.Text = value;
            }
        }

        public string Leverantor
        {
            get
            {
                if (levLabel.Text == "")
                {
                    return "";
                }
                else
                {
                    return levLabel.Text;
                }
            }
            set
            {
                levLabel.Text = value;
            }
        }

        public string Namn
        {
            get
            {
                if (nameLabel.Text == "")
                {
                    return "";
                }
                else
                {
                    return nameLabel.Text;
                }
            }
            set
            {
                nameLabel.Text = value;
            }
        }

        //public string CAS;
        public string summaFormel;
        public string lokalDB;
        public int transitionID;
        public int productID;
        public string amount;
        public string unit;

        public printBarcodeItem()
        {
            InitializeComponent();
        }

        public printBarcodeItem(string name, string barcode, string amount, string unit, string note, string cas, string summaFormel, string lokaldb, int transId, int productid,string leverantor)
        {
            InitializeComponent();
            nameLabel.Text = name;
            //TextBox text = new TextBox();
            //text.Visible = true;
            //text.Enabled = true;
            //text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            

            if (barcode == "")
            {
                barcodeLabel.Text = "-";
            }
            else
            {
                barcodeLabel.Text = barcode;
            }

            amountLabel.Text = amount + " " + unit;
            noteLabel.Text = note;
            //text.Text = noteLabel.Text;

            this.summaFormel = summaFormel;

            if (cas == "")
            {
                casLabel.Text = "";
            }
            else
            {
                casLabel.Text = cas;
            }



            if (leverantor == "")
            {
                levLabel.Text = "";
            }
            else
            {
                levLabel.Text =leverantor;
            }
            
            //this.CAS = cas;
            this.lokalDB = lokaldb;
            this.transitionID = transId;
            this.productID = productid;
            this.amount = amount;
            this.unit = unit;
        }      
    }
}
