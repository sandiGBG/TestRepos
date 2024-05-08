﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BarcodePcApp
{
    public partial class FormInventoryLeftovers : Form
    {
        private int inventoryPeriod;
        private bool lang;  //ver 1.27 översättning eng-sve
        public FormInventoryLeftovers(List<DataGridViewRow> rowList, int inventoryPeriod)
        {
            this.inventoryPeriod = inventoryPeriod;

            InitializeComponent();
            lang = FormMain.Get.LangChange;
            if (lang == true)
            {
                this.label1.Text = "Vissa objekt skannades inte. Välj vad du vill göra med dem.";
                this.buttonCancel.Text = "Avbryt";
                this.label2.Text = "Namn";
                this.label3.Text = "Mängd";
                this.label4.Text = "Streckkod";
                this.label5.Text = "Kassera";
                this.label6.Text = "Behåll";
                this.Name = "FormInventoryLeftovers";
                this.Text = "Artiklar kvar";

            }
            else
            {
                this.label1.Text = "Some items were not scanned. Please choose what to do with them.";
                this.buttonCancel.Text = "Cancel";
                this.label2.Text = "Name";
                this.label3.Text = "Amount";
                this.label4.Text = "Barcode";
                this.label5.Text = "Discard";
                this.label6.Text = "Keep";
                this.Name = "FormInventoryLeftovers";
                this.Text = "Articles left";
            }

            AddArticles(rowList);
        }

        private void AddArticles(List<DataGridViewRow> rowList)
        {
            foreach (DataGridViewRow row in rowList)
            {
                string namn = row.Cells["namn"].Value.ToString();
                double amount = double.Parse(row.Cells["amount"].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                string unit = row.Cells["unit"].Value.ToString();
                string barcode = row.Cells["barcodeCode"].Value.ToString();

                int id = Convert.ToInt32(row.Cells["transID"].Value);

                panelArticles.Controls.Add(new ArticleKeepDelete(namn, amount, unit, barcode, id));
            }
        }


        private void PrepareAndUploadData()
        {
            List<string> toDelete = new List<string>();
            List<string> toKeep = new List<string>();

            foreach (ArticleKeepDelete akd in panelArticles.Controls)
            {
                if (akd.DeleteSelected())
                {
                    toDelete.Add(akd.KpinvtransID.ToString());
                    if (toDelete.Count > 25)
                    {
                        Upload(toDelete, true);
                        toDelete.Clear();
                    }
                }
                else
                {
                    toKeep.Add(akd.KpinvtransID.ToString());
                    if (toKeep.Count > 25)
                    {
                        Upload(toKeep, false);
                        toKeep.Clear();
                    }
                }    
            }

            if (toDelete.Count > 0)
            {
                Upload(toDelete, true);
            }
            if (toKeep.Count > 0)
            {
                Upload(toKeep, false);
            }
        }

        private void Upload(List<string> idList, bool delete)
        {
            string commaDelimitedList = "";

            foreach (string id in idList)
            {
                commaDelimitedList += id + ',';
            }

            barcode.BarcodeService bc = FormMain.getBarcodeService();


            try
            {
                if (delete)
                {
                    bc.MoveToPapperskorg(commaDelimitedList, FormMain.Get.Username, FormMain.Get.Databas);
                }
                else
                {
                    bc.TakeInventory(commaDelimitedList, FormMain.Get.Username, inventoryPeriod, FormMain.Get.Databas);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Error while updating the database.\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "Upload");
            }
            
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            PrepareAndUploadData();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void panelArticles_MouseEnter(object sender, EventArgs e)
        {
            panelArticles.Focus();
        }
    }
}
