using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using BarcodePcApp.Misc;
using System.Media;
using System.IO;


namespace BarcodePcApp
{
    public partial class FormTransferInventoryDialog : Form
    {
        
        string database;
        string lokaldb;
        private int orgarCode;
        private XmlNode itemLocation;
        private int destinationRoom;
        private int destinationOrg;
        private int destinationCabinet;
        private bool takeInventoryAfterMove;
        private bool lang;  //ver 1.27 översättning eng-sve

        private struct node
        {
            public int id;
            public int left;
            public string name;

            public node(int nodeID, int nodeLeft, string nodeName)
            {
                id = nodeID;
                left = nodeLeft;
                name = nodeName;
            }
        }

        public enum Property
        {
            Syra = 1,
            Bas = 2,
            Omärkt = 0
        };

         public FormTransferInventoryDialog(XmlNode itemLocation, TreeNodeWithID userlocation, string barcode, string database, int userRoomID, int orgarcode, bool insideUserDepartment)
        {
            
            this.database = database;
            this.lokaldb = userlocation.dataBase;
            this.orgarCode = orgarcode;
            this.itemLocation = itemLocation;
            destinationRoom = userRoomID;
            destinationOrg = userlocation.orgID;
            takeInventoryAfterMove = userlocation.Enabled;

            if (userlocation.nodeType == TreeNodeWithID.type.CABINET)
            {
                destinationCabinet = userlocation.ID;
            }
            else
            {
                destinationCabinet = -1;
            }

            
            InitializeComponent();

            lang = FormMain.Get.LangChange;

            if (lang == false)
            {
                this.barcodeCode.HeaderText = "Barcode";
                this.productID.HeaderText = "Klara ID";
                this.namn.HeaderText = "Name";
                this.amount.HeaderText = "Amount";
                this.unit.HeaderText = "Unit";
                this.fritext.HeaderText = "Note";
                this.label1.Text = "Article:";
                this.label2.Text = "Current location in database:";
                this.label3.Text = "Your current location:";
                this.cancelButton.Text = "Return";
                this.transferButton.Text = "Transfer";
                this.userLocationLabel.Text = "Current inventory location";
                this.itemLocationLabel.Text = "Location of product";
                this.Text = "Transfer an item";

            }
            else
            {
                //this.questionLabel.Text = "Artikeln du skannade tillhör en annan organisation. Vill du " +"flytta den till din nuvarande plats?";
                this.barcodeCode.HeaderText = "Streckkod";
                this.productID.HeaderText = "Klara ID";
                this.namn.HeaderText = "Namn";
                this.amount.HeaderText = "Mängd";
                this.unit.HeaderText = "Enhet";
                this.fritext.HeaderText = "Notering";
                this.label1.Text = "Artikel:";
                this.label2.Text = "Nuvarande plats i databasen:";
                this.label3.Text = "Din nuvarande plats:";
                this.cancelButton.Text = "Ångra";
                this.transferButton.Text = "Flytta";
                this.userLocationLabel.Text = "Aktuell plats";
                this.itemLocationLabel.Text = "Produktens placering";
                this.Text = "Flytta en produkt";

            }

            UpdateMessageLabel(insideUserDepartment);
            UpdateView(itemLocation, barcode);
            getNodeList(userlocation, userRoomID, itemLocation);
            
        }

        private void UpdateMessageLabel(bool sameDepartment)
        {
            if (sameDepartment)
            {
                System.Media.SystemSounds.Exclamation.Play();
                if (lang==true)
                {
                    questionLabel.Text = "Denna artikel (" + itemLocation["itemname"].InnerText + ") tillhör någon annan på din avdelning. Vill du lämna tillbaka den till ägaren eller överföra till den här platsen?";
                }
                else
                {
                    questionLabel.Text = "This article (" + itemLocation["itemname"].InnerText + ") appears to belong somewhere else in your department. Would you like to return it to its owner or transfer it to this location?";
                }
               
            }
            else
            {
                System.Media.SystemSounds.Hand.Play();
                if (lang == true)
                {
                    questionLabel.Text = "Denna artikel (" + itemLocation["itemname"].InnerText + ") tillhör " + itemLocation["orgnamn"].InnerText + ". Vill du lämna tillbaka den till ägaren eller överföra till den här platsen?";
                }
                else
                {
                    questionLabel.Text = "This article (" + itemLocation["itemname"].InnerText + ") belongs to " + itemLocation["orgnamn"].InnerText + ". Would you like to return it to its owner or transfer it to this location?";
                }
               
            }
        }

        private void getNodeList(TreeNodeWithID userLocation, int roomID, XmlNode itemLocation)
        {
            barcode.BarcodeService bc = FormMain.getBarcodeService();
            string parents = "";

            //START OF: Get parents for users current location

            try
            {
                if (userLocation.nodeType == TreeNodeWithID.type.CABINET)
                {
                    parents = bc.GetParents(userLocation.orgID, roomID, userLocation.ID, database, FormMain.Get.OrgAr, orgarCode,itemLocation["lokaldb"].InnerText );
                }
                else if (userLocation.nodeType == TreeNodeWithID.type.ROOM)
                {
                    parents = bc.GetParents(userLocation.orgID, userLocation.ID, -1, database, FormMain.Get.OrgAr, orgarCode,itemLocation["lokaldb"].InnerText);
                }
            }
            catch (Exception err)
            {
                if (lang == true)
                {
                    MessageBox.Show("Error while getting transfer information.\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "GetNodeList");
                }
                else
                {
                    MessageBox.Show("Error while getting transfer information.\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "GetNodeList");
                }
                
            }

            //System.Diagnostics.Debug.Write(parents);

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(parents);
            XmlNode xParentsNode = xDoc.SelectSingleNode("parents");

            List<node> userOrgList = GetNodeList(xParentsNode["orgparents"]);
            List<node> userLokalList = GetNodeList(xParentsNode["lokalparents"]);
            List<node> userCabinets = new List<node>();

            if (userLocation.nodeType == TreeNodeWithID.type.CABINET)
            {     
                userCabinets.Add(new node(userLocation.ID, userLocation.parentID, userLocation.Name));
                userCabinets.AddRange(GetCabinetList(xParentsNode["cabinets"], userLocation.parentID, userLocation.ID));
            }

            //END OF: Get parents for users current location

            try
            {
            //START OF: Get parents for items current location
                if (itemLocation["storageid"].InnerText != null)
                //if (itemLocation["storageid"].InnerText !="0")
                {
                    parents = bc.GetParents(Convert.ToInt32(itemLocation["orgid"].InnerText), Convert.ToInt32(itemLocation["lokalid"].InnerText), Convert.ToInt32(itemLocation["storageid"].InnerText), database, FormMain.Get.OrgAr, orgarCode, itemLocation["lokaldb"].InnerText);
                    //parents = bc.GetParents(Convert.ToInt32(itemLocation["orgid"].InnerText), Convert.ToInt32(itemLocation["lokalid"].InnerText), Convert.ToInt32(itemLocation["storageid"].InnerText), database, FormMain.Get.OrgAr, orgarCode);
                }
                else
                {
                    parents = bc.GetParents(Convert.ToInt32(itemLocation["orgid"].InnerText), Convert.ToInt32(itemLocation["lokalid"].InnerText), -1, database, FormMain.Get.OrgAr, orgarCode,itemLocation["lokaldb"].InnerText);
                }
            }
            catch (Exception err)
            {
                if (lang == true)
                {
                    MessageBox.Show("Error while getting transfer information.\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "GetNodeList");
                }
                else
                {
                    MessageBox.Show("Error while getting transfer information.\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "GetNodeList");
                }
                
            }
            //System.Diagnostics.Debug.Write(parents);

            xDoc = new XmlDocument();
            xDoc.LoadXml(parents);
            xParentsNode = xDoc.SelectSingleNode("parents");

            List<node> itemOrgList = GetNodeList(xParentsNode["orgparents"]);
            List<node> itemLokalList = GetNodeList(xParentsNode["lokalparents"]);
            List<node> itemCabinets = new List<node>();

            if (itemLocation["storageid"].InnerText != null)
            {
                int parentID = 0;
                foreach (XmlNode xn in xParentsNode["cabinets"].ChildNodes)
                {
                    if (itemLocation["id"].InnerText == xn["id"].InnerText)
                    {
                        parentID = Convert.ToInt32(xn["pappa"].InnerText);
                    }
                }

                itemCabinets.Add(new node(Convert.ToInt32(itemLocation["id"].InnerText), parentID, itemLocation["storage"].InnerText));
                itemCabinets.AddRange(GetCabinetList(xParentsNode["cabinets"], parentID, Convert.ToInt32(itemLocation["id"].InnerText)));
            }
            //END OF: Get parents for items current location


            GetClosestParentsAndPrintToLabels(itemOrgList, itemLokalList, itemCabinets, userOrgList, userLokalList, userCabinets);

            bc.Dispose();
        }


        private List<node> GetNodeList(XmlNode containerNode)
        {
            List<node> resultList = new List<node>();

            foreach (XmlNode xn in containerNode.ChildNodes)
            {
                resultList.Add(new node(Convert.ToInt32(xn["id"].InnerText), Convert.ToInt32(xn["left"].InnerText), xn["name"].InnerText.Trim()));
            }
           
            return resultList;
        }
        
        private List<node> GetCabinetList(XmlNode xCabinetsNode, int parent, int id)
        {
            List<node> cabinets = new List<node>();

            while(parent != 0)
            {
                foreach (XmlNode xn in xCabinetsNode.ChildNodes)
                {
                    if (Convert.ToInt32(xn["id"].InnerText) == parent && parent != 0)
                    {
                        parent = Convert.ToInt32(xn["pappa"].InnerText);
                        id = Convert.ToInt32(xn["id"].InnerText);

                        cabinets.Add(new node(Convert.ToInt32(xn["id"].InnerText), Convert.ToInt32(xn["pappa"].InnerText), xn["name"].InnerText.Trim()));
                    }
                }
            }
            return cabinets;
        }


        private void GetClosestParentsAndPrintToLabels(List<node> itemOrg, List<node> itemLok, List<node> itemCab, List<node> userOrg, List<node> userLok, List<node> userCab)
        {
            node closestMutualParent = new node();
            
            
            for (int i = 0; i < Math.Min(itemOrg.Count, userOrg.Count); i++)
            {
                if (itemOrg[i].id == userOrg[i].id)
                {
                    closestMutualParent = itemOrg[i];
                    itemOrg.RemoveAt(i);
                    userOrg.RemoveAt(i);
                    i--;
                }
                else
                {
                    break;
                }
            }
            if (itemOrg.Count == 0 && userOrg.Count == 0)
            {
                for (int i = 0; i < Math.Min(itemLok.Count, userLok.Count); i++)
                {
                    if (itemLok[i].id == userLok[i].id)
                    {
                        closestMutualParent = itemLok[i];
                        itemLok.RemoveAt(i);
                        userLok.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        break;
                    }
                }

                if (itemLok.Count == 0 && userLok.Count == 0)
                {
                    for (int i = 0; i < Math.Min(itemCab.Count, userCab.Count); i++)
                    {
                        if (itemCab[i].id == userCab[i].id)
                        {
                            closestMutualParent = itemCab[i];
                            itemCab.RemoveAt(i);
                            userCab.RemoveAt(i);
                            i--;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }


            itemOrg.AddRange(itemLok);
            itemOrg.AddRange(itemCab);
            itemOrg.Insert(0, closestMutualParent);
            UpdateLabel(itemLocationLabel, itemOrg);

            userOrg.AddRange(userLok);
            userOrg.AddRange(userCab);
            userOrg.Insert(0, closestMutualParent);
            UpdateLabel(userLocationLabel, userOrg);


       }

        private void UpdateLabel(Label destination, List<node> content)
        {
            string text = "";
            string spacing = "";
            foreach (node n in content)
            {
                text += spacing + n.name.Trim() + "\n";
                spacing += " ";
            }

            destination.Text = text;
        }
        
        private void UpdateView(XmlNode itemLocation, string barcode)
        {
            string[] content = new string[6];

            content[0] = barcode;
            content[1] = itemLocation["klaraid"].InnerText;
            content[2] = itemLocation["itemname"].InnerText;
            content[3] = itemLocation["amount"].InnerText;
            content[4] = itemLocation["unit"].InnerText;
            content[5] = itemLocation["note"].InnerText;
            

            dgwTransferItem.Rows.Add(content); 
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void transferButton_Click(object sender, EventArgs e)
        {

            barcode.BarcodeService bc = FormMain.getBarcodeService();
            //Sandi
            int lokal = destinationRoom;
            string lokaldb = this.lokaldb;

            string StorageProperty_q = bc.GetRoomProp(database, destinationRoom, destinationCabinet,lokaldb);
            XmlDocument doc = new XmlDocument();
            doc = new XmlDocument();
            doc.LoadXml(StorageProperty_q);
            XmlNodeList xmlnodeList = doc.DocumentElement.SelectNodes("item");
            int StorageProperty = 0;
            foreach (XmlNode item in xmlnodeList)
            {
                StorageProperty = Convert.ToInt32(item.Attributes["id"].Value);
            }

            //int StorageProperty = Convert.ToInt32(bc.GetRoomProp(database, destinationRoom, destinationCabinet));            
            if (StorageProperty == 0)
            {
                string StoragePropertyParent_q = bc.GetStorageParent(database, destinationRoom, destinationCabinet, lokaldb);
                XmlDocument doc2 = new XmlDocument();
                doc2 = new XmlDocument();
                doc2.LoadXml(StoragePropertyParent_q);
                XmlNodeList xmlnodeList2 = doc2.DocumentElement.SelectNodes("item");
                int StoragePropertyParent = 0;
                foreach (XmlNode item in xmlnodeList2)
                {
                    StoragePropertyParent = Convert.ToInt32(item.Attributes["id"].Value);
                    StorageProperty = StoragePropertyParent;
                }
                //StorageProperty = Convert.ToInt32(bc.GetStorageParent(database, destinationRoom, destinationCabinet));
            }
            else
            {

            }
            int klaraid = Convert.ToInt32(itemLocation["klaraid"].InnerText);
            int n_transid = Convert.ToInt32(itemLocation["transid"].InnerText);
            string ProdPropert_q = bc.GetPropName(database, n_transid);
            XmlDocument doc1 = new XmlDocument();
            doc1 = new XmlDocument();
            doc1.LoadXml(ProdPropert_q);
            XmlNodeList xmlnodeList1 = doc1.DocumentElement.SelectNodes("item");
            int ProdProperty = 0;
            foreach (XmlNode item in xmlnodeList1)
            {
                ProdProperty = Convert.ToInt32(item.Attributes["id"].Value);
            }
            //int ProdProperty = Convert.ToInt32(bc.GetPropName(database, n_transid)); 

//--------------------------------------------------------------------------------------------------------------------
          if (FormMain.Get.PropertyCheck == true)
          {

            if ((StorageProperty == ProdProperty) || (StorageProperty == Convert.ToInt32(Property.Omärkt)))
            {
                try
                {
                        //MessageBox.Show("transid: " + Convert.ToInt32(itemLocation["transid"].InnerText) + " id: " + Convert.ToInt32(itemLocation["id"].InnerText) + " desitnationCab: " + destinationCabinet + " destinationRoom: " + destinationRoom + " destinationOrg: " + destinationOrg + " Period: " + int.Parse(itemLocation["status"].InnerText) + " orgarCode: " + orgarCode + " username: " + FormMain.Get.Username + " type: " + int.Parse(itemLocation["type"].InnerText) + " db: " + database + " takeInvAfterMove: " + takeInventoryAfterMove);
                        bc.TransferArticle(Convert.ToInt32(itemLocation["transid"].InnerText), Convert.ToInt32(itemLocation["id"].InnerText), destinationCabinet, destinationRoom, destinationOrg, Convert.ToInt32(itemLocation["status"].InnerText), orgarCode, FormMain.Get.Username, int.Parse(itemLocation["type"].InnerText), database, takeInventoryAfterMove, this.lokaldb);
                        System.Media.SystemSounds.Asterisk.Play();

                    }
                catch (Exception err)
                {
                        if (lang == true)
                        {
                            MessageBox.Show("Error while transfering article.\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "Transfer");
                        }
                        else
                        {
                            MessageBox.Show("Error while transfering article.\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "Transfer");
                        }
                        
                }

                bc.Dispose();
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            }
          
            else
            {
                DataSet ds = new DataSet();
                barcode.BarcodeService bst = FormMain.getBarcodeService();
                string PropName = bst.GetProductPropertyName(database, n_transid);
                StringReader stri = new StringReader(PropName);
                ds.ReadXml(stri);
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];
                if (dr["egenskapid"].ToString() == "0")
                {
                    //System.Media.SystemSounds.Exclamation.Play();
                    SoundPlayer pl = new SoundPlayer(BarcodePcApp.Properties.Resources.WrongLocation);
                    pl.Play();
                        if (lang == true)
                        {
                            MessageBox.Show("Egenskap på produkt och plats stämmer inte överens." + Environment.NewLine + "Produkten har ingen egenskap");
                        }
                        else
                        {
                            MessageBox.Show("Property on product and location does not match." + Environment.NewLine + "The product has not any property");
                        }
                        
                    this.DialogResult = System.Windows.Forms.DialogResult.No;
                }
                else
                {
                    string ProdPropName = dr["nameEng"].ToString();
                    SoundPlayer pl = new SoundPlayer(BarcodePcApp.Properties.Resources.WrongLocation);
                    pl.Play();
                        //System.Media.SystemSounds.Exclamation.Play();
                        if (lang == true)
                        {
                            MessageBox.Show("Egenskap på produkt och plats stämmer inte överens." + Environment.NewLine + "Produkten anses vara [ " + ProdPropName + " ]");
                        }
                        else
                        {
                            MessageBox.Show(" Property on product and location does not match." + Environment.NewLine + "The product is considered to be [ " + ProdPropName + " ]");
                        }
                        
                    this.DialogResult = System.Windows.Forms.DialogResult.No;
                }
                
                //System.Media.SystemSounds.Exclamation.Play();
                //MessageBox.Show("Property on product and location does not match.");
            }
          }
              else{
                  bc.TransferArticle(Convert.ToInt32(itemLocation["transid"].InnerText), Convert.ToInt32(itemLocation["id"].InnerText), destinationCabinet, destinationRoom, destinationOrg, Convert.ToInt32(itemLocation["status"].InnerText), orgarCode, FormMain.Get.Username, int.Parse(itemLocation["type"].InnerText), database, takeInventoryAfterMove, this.lokaldb);
                  this.DialogResult = System.Windows.Forms.DialogResult.Yes;
          }

//--------------------------------------------------------------------------------------------------------------------

           
            //try
            //{
            //    //MessageBox.Show("transid: " + Convert.ToInt32(itemLocation["transid"].InnerText) + " id: " + Convert.ToInt32(itemLocation["id"].InnerText) + " desitnationCab: " + destinationCabinet + " destinationRoom: " + destinationRoom + " destinationOrg: " + destinationOrg + " Period: " + Convert.ToInt32(itemLocation["period"].InnerText) + " orgarCode: " + orgarCode + " username: " + FormMain.Get.Username + " type: " + int.Parse(itemLocation["type"].InnerText) + " db: " + database + " takeInvAfterMove: " + takeInventoryAfterMove);
            //    bc.TransferArticle(Convert.ToInt32(itemLocation["transid"].InnerText), Convert.ToInt32(itemLocation["id"].InnerText), destinationCabinet, destinationRoom, destinationOrg, Convert.ToInt32(itemLocation["period"].InnerText), orgarCode, FormMain.Get.Username, int.Parse(itemLocation["type"].InnerText), database, takeInventoryAfterMove);
            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show("Error while traferring article.\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "Transfer");
            //}

            //bc.Dispose();

            //this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        
    }
}