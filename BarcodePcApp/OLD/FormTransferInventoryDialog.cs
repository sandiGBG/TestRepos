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
        private int orgarCode;
        private XmlNode itemLocation;
        private int destinationRoom;
        private int destinationOrg;
        private int destinationCabinet;
        private bool takeInventoryAfterMove;

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

            UpdateMessageLabel(insideUserDepartment);
            UpdateView(itemLocation, barcode);
            getNodeList(userlocation, userRoomID, itemLocation);
            
        }

        private void UpdateMessageLabel(bool sameDepartment)
        {
            if (sameDepartment)
            {
                System.Media.SystemSounds.Exclamation.Play();
                questionLabel.Text = "This article (" + itemLocation["itemname"].InnerText + ") appears to belong somewhere else in your department. Would you like to return it to its owner or transfer it to this location?";
            }
            else
            {
                System.Media.SystemSounds.Hand.Play();
                questionLabel.Text = "This article (" + itemLocation["itemname"].InnerText + ") belongs to " + itemLocation["orgnamn"].InnerText + ". Would you like to return it to its owner or transfer it to this location?";
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
                MessageBox.Show("Error while getting transfer information.\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "GetNodeList");
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
                MessageBox.Show("Error while getting transfer information.\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "GetNodeList");
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
            content[1] = itemLocation["id"].InnerText;
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

            int StorageProperty = Convert.ToInt32(bc.GetRoomProp(database, destinationRoom, destinationCabinet));            
            if (StorageProperty == 0)
            {
                StorageProperty = Convert.ToInt32(bc.GetStorageParent(database, destinationRoom, destinationCabinet));
            }
            else
            {

            }
            int klaraid = Convert.ToInt32(itemLocation["klaraid"].InnerText);
            int n_transid = Convert.ToInt32(itemLocation["transid"].InnerText);
            int ProdProperty = Convert.ToInt32(bc.GetPropName(database, n_transid)); 

//--------------------------------------------------------------------------------------------------------------------
          if (FormMain.Get.PropertyCheck == true)
          {

            if ((StorageProperty == ProdProperty) || (StorageProperty == Convert.ToInt32(Property.Omärkt)))
            {
                try
                {
                    //MessageBox.Show("transid: " + Convert.ToInt32(itemLocation["transid"].InnerText) + " id: " + Convert.ToInt32(itemLocation["id"].InnerText) + " desitnationCab: " + destinationCabinet + " destinationRoom: " + destinationRoom + " destinationOrg: " + destinationOrg + " Period: " + Convert.ToInt32(itemLocation["period"].InnerText) + " orgarCode: " + orgarCode + " username: " + FormMain.Get.Username + " type: " + int.Parse(itemLocation["type"].InnerText) + " db: " + database + " takeInvAfterMove: " + takeInventoryAfterMove);
                    bc.TransferArticle(Convert.ToInt32(itemLocation["transid"].InnerText), Convert.ToInt32(itemLocation["id"].InnerText), destinationCabinet, destinationRoom, destinationOrg, Convert.ToInt32(itemLocation["period"].InnerText), orgarCode, FormMain.Get.Username, int.Parse(itemLocation["type"].InnerText), database, takeInventoryAfterMove);
                    System.Media.SystemSounds.Asterisk.Play();
                }
                catch (Exception err)
                {
                    MessageBox.Show("Error while transfering article.\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "Transfer");
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
                    MessageBox.Show("Property on product and location does not match." + Environment.NewLine + "The product has not any property");
                    this.DialogResult = System.Windows.Forms.DialogResult.No;
                }
                else
                {
                    string ProdPropName = dr["nameEng"].ToString();
                    SoundPlayer pl = new SoundPlayer(BarcodePcApp.Properties.Resources.WrongLocation);
                    pl.Play();
                    //System.Media.SystemSounds.Exclamation.Play();
                    MessageBox.Show(" Property on product and location does not match." + Environment.NewLine +   "The product is considered to be [ " + ProdPropName + " ]");
                    this.DialogResult = System.Windows.Forms.DialogResult.No;
                }
                
                //System.Media.SystemSounds.Exclamation.Play();
                //MessageBox.Show("Property on product and location does not match.");
            }
          }
              else{
                  bc.TransferArticle(Convert.ToInt32(itemLocation["transid"].InnerText), Convert.ToInt32(itemLocation["id"].InnerText), destinationCabinet, destinationRoom, destinationOrg, Convert.ToInt32(itemLocation["period"].InnerText), orgarCode, FormMain.Get.Username, int.Parse(itemLocation["type"].InnerText), database, takeInventoryAfterMove);
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