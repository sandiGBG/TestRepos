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
using System.IO;
using System.Timers;
using System.Drawing.Printing;
using System.Media;

namespace BarcodePcApp
{
    public partial class FormInventory : Form
    {
        private int verkID;
        private int orgAr;
        private string database;
        private string kemiDB;
        private string barcode = "";
        private List<int> inventoryList = new List<int>();
        private bool scanEnabled = false;
        private bool hasChanged = false;
        private bool Log;

        public int KlaraID;
        public int avd;
        private string barcodeInSettings = "";
        public bool tree_expanded = false;
        public int last_barcode;
        public bool scanRoom = false;
        public int ejstartad_org;
        public string m_sRelease = "";
        public int m_Status;

        

        private const string done = " (done)";
        private const string partlyDone = " (partly done)";

        private Font underLine;

        public bool shouldBeShown { get; set; }

        private TreeNodeWithID selectedNode;

        private System.Timers.Timer scannerTimer = new System.Timers.Timer();

        public FormInventory(int verkID, string database, string kemiDB)
        {
            FormSettings form = new FormSettings();
            barcodeInSettings = form.loginBarcode;  

            setUp(verkID, database, kemiDB);
        }

        public FormInventory(int verkID, string database, string kemiDB, string roomBarcode)
        {
            FormSettings form = new FormSettings();
            barcodeInSettings = form.loginBarcode;

            setUp(verkID, database, kemiDB);

            barcode = roomBarcode;
            this.Shown += new System.EventHandler(this.FormInventory_Shown);    
    
        }

        private void FormInventory_Shown(object sender, EventArgs e)
        {
            changeRoomWithBarcode();
            barcode = "";
        }

        public enum Property
        {
            Syra = 1,
            Bas = 2,
            Omärkt = 0
        };

        private void setUp(int verkID, string database, string kemiDB)
        {
            this.Visible = false;
            this.KeyPress += new KeyPressEventHandler(form_KeyPress);
            scannerTimer.Interval = 100;
            scannerTimer.Elapsed += scannerTimer_Elapsed;


            this.verkID = verkID;
            this.database = database;
            this.kemiDB = kemiDB;

            shouldBeShown = false;
            scanRoom = FormMain.Get.ScanRoomsEnabled;
            //this.locationBarcodeButton.Visible = false;


            try
            {
                barcode.BarcodeService bc = FormMain.getBarcodeService();

                orgAr = Convert.ToInt32(bc.GetOrgarCode(database, verkID));
                //orgAr = 12;
                //for running offline
            }
            catch (Exception err)
            {
                //TODO: meddelande
            }

            this.FormClosing += new FormClosingEventHandler(Inventory_FormClosing);

            InitializeComponent();

                this.locationBarcodeButton.Visible = false;
                this.printBarcodesButton.Visible = false;
                //this.txtScannedBarcode.Visible = false;
                this.label1.Visible = true;
                this.label1.Enabled = true;


            underLine = new Font(lockerTreeView.Font, FontStyle.Underline);

            updateTreeView();
        }

        private void scannerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            codeInputBox.Clear();
            if (scannerCheckBox.CheckState == CheckState.Checked)
            {
                codeInputBox.Enabled = false;
                addButton.Enabled = false;
            }
            else
            {
                codeInputBox.Enabled = true;
                addButton.Enabled = true;
                codeInputBox.Focus();
                //scanEnabled = false;
            }
        }

        private void lockerTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
        }




        private void lockerTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selectedNode = (TreeNodeWithID)lockerTreeView.SelectedNode;
            barcode.BarcodeService bc = FormMain.getBarcodeService();
            string status = bc.GetInventoryOrgStatus(database, selectedNode.orgID, 0, orgAr);


            if (status == "")
            {
                
            }

            else
            {
                // Listan id=735
                ejstartad_org = selectedNode.orgID;
                string ej=selectedNode.Text;
                //
                //MessageBox.Show("New year of inventory has been started. Please, enter the web interface and copy all products from last inventory in . This application will close. ");
                MessageBox.Show("New year of inventory has been started. Please, enter the web interface and copy all products from "+ej +". This application will close. ") ;
                //resetInventoryViewWithText("New year of inventory" + orgAr + " has been started. Please, copy all products and amounts from last inventory, and start to invent. ");
                startInventoryButton.Enabled = false;
                startInventoryButton.Visible = false;
                printBarcodesButton.Visible = false;
                locationBarcodeButton.Visible = false;
                this.Invoke(new MethodInvoker(delegate { FormMain.Get.Logout(); })); 
                return;
                


            }


            ////Nytt
            if (selectedNode.nodeType == TreeNodeWithID.type.DEPARTMENT)
            {
                if (!selectedNode.IsExpanded)
                {
                    addRooms(selectedNode);
                    selectedNode.Expand();
                }
            }

            ////
            if(selectedNode.nodeType == TreeNodeWithID.type.CABINET || selectedNode.nodeType == TreeNodeWithID.type.ROOM)
            {

                string date = "Last inventory activity: ";
                if (selectedNode.date.Year != 1)
                {
                    date += selectedNode.date.ToShortDateString();
                }
                else
                {
                    date += "-";
                }

                if (selectedNode.Enabled)
                {
                    inventoryInstructionLabel.Text = "Start taking inventory for " + selectedNode.Text + " in " + selectedNode.Parent.Text + "?" + Environment.NewLine + date;
                }
                else
                {
                    inventoryInstructionLabel.Text = "Move items to " + selectedNode.Text + " in " + selectedNode.Parent.Text + "?" + Environment.NewLine + date;
                }
                    
                //Place label in the center of hideInventoryPanel
                //inventoryInstructionLabel.Left = hideInventoryPanel.Width / 2 - inventoryInstructionLabel.Width / 2;

                labelStorageName.Text = "2. Start scanning barcodes in: " + selectedNode.Parent.Text +" / "+ selectedNode.Text; ;

                startInventoryButton.Visible = true;
                startInventoryButton.Enabled = true;
                printBarcodesButton.Visible = true;
                if (FormMain.Get.ScanRoomsEnabled)
                {
                    locationBarcodeButton.Visible = true;
                }


                
            }
            else
            {
                resetInventoryViewWithText("Please select a room or a cabinet.");

                startInventoryButton.Enabled = false;
                startInventoryButton.Visible = false;
                printBarcodesButton.Visible = false;
                locationBarcodeButton.Visible = false;
            }

         

        }

        private void fillDataGrid(string xmlString)
        {
            //System.Diagnostics.Debug.Write(xmlString);
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xmlString);

            XmlNodeList xNodeList = xDoc.SelectNodes("//item");
            do
            {
                foreach (DataGridViewRow row in dgwInventoryItems.Rows)
                {
                    try
                    {
                        dgwInventoryItems.Rows.Remove(row);
                    }
                    catch (Exception) { }
                }
            }while (dgwInventoryItems.Rows.Count > 0);


            dgwInventoryItems.DefaultCellStyle.Font = new Font(dgwInventoryItems.DefaultCellStyle.Font, FontStyle.Italic);
            foreach (XmlNode xNode in xNodeList)
            {
                int indexOfRow = dgwInventoryItems.Rows.Add(createRow(xNode));

                //check to see if item has been added to inventory yet. If it has, make it bold. 
                if (((string)dgwInventoryItems.Rows[indexOfRow].Cells["vem"].Value != "0" && (string)dgwInventoryItems.Rows[indexOfRow].Cells["vem"].Value != "") && ((string)dgwInventoryItems.Rows[indexOfRow].Cells["nar"].Value != "" && (string)dgwInventoryItems.Rows[indexOfRow].Cells["nar"].Value != "0"))
                {
                    makeRowBold(indexOfRow);
                    dgwInventoryItems.Rows[indexOfRow].Cells["inventorydone"].Value = "yes";
                }
                else
                {
                    dgwInventoryItems.Rows[indexOfRow].Cells["inventorydone"].Value = "no";
                }
            }

        }

        private void makeRowBold(int index)
        {
            foreach (DataGridViewCell cell in dgwInventoryItems.Rows[index].Cells)
            {
                cell.Style.Font = new Font(dgwInventoryItems.DefaultCellStyle.Font, FontStyle.Bold);
            }
        }

        private string[] createRow(XmlNode xNode)
        {

            //The reason this works: 
            //The first elements in the XML is the ones corresponing to the columns in the datagrid.
            string[] row = new string[dgwInventoryItems.Columns.Count];
            for (int i = 0; i < row.Length; i++)
            {
                if (xNode.ChildNodes[i] != null)
                {
                    row[i] = xNode.ChildNodes[i].InnerText;
                }
                else
                {
                    row[i] = "N/A";
                }
            }

            return row;
        }

        //Methods for updating the treeView
        private void updateTreeView()
        {
            barcode.BarcodeService bc = FormMain.getBarcodeService();


            try
            {
                string sTree = bc.GetOrgtradForInventory(FormMain.Get.Databas, DateTime.Now.Year, FormMain.Get.UserId, orgAr,FormMain.Get.OrgNod);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(sTree);
                System.Diagnostics.Debug.Write(doc.OuterXml);
                if (Convert.ToInt32(doc.DocumentElement.Attributes["antal"].Value) > 0)
                {
                    shouldBeShown = true;
                    lockerTreeView.BeginUpdate();
                    XmlNode node = doc.DocumentElement.SelectSingleNode("item");
                    //XmlNodeList xmlnodeList1 = doc.DocumentElement.SelectNodes("item1");
                    if (node != null)
                    {
                        int nRootnode = Convert.ToInt32(node.Attributes["pappa"].Value);

                        XmlNodeList xmlnodeList = doc.DocumentElement.SelectNodes("item");


                        lockerTreeView.Nodes.AddRange(createTree(xmlnodeList));
                    }
                    lockerTreeView.EndUpdate();

                    lockerTreeView.Nodes[0].Expand();
                }
                else
                {
                    return;
                }
            }
            catch (Exception err)
            {
                DialogResult = DialogResult.Cancel;
                MessageBox.Show("Failed when getting your inventory locations.\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "updateTreeView");
                bc.Dispose();
                return;
            }
        }

        private TreeNodeWithID[] createTree(XmlNodeList xnl)
        {
            DateTime currentDate = DateTime.Now;

            TreeNodeWithID startNode = new TreeNodeWithID();
            List<TreeNodeWithID> nodeList = new List<TreeNodeWithID>();


            startNode.Text = xnl[0].Attributes["namn"].Value.ToString();
            startNode.orgID = Convert.ToInt32(xnl[0].Attributes["orgnod"].Value);
            startNode.ID = Convert.ToInt32(xnl[0].Attributes["id"].Value);
            startNode.nodeType = TreeNodeWithID.type.DEPARTMENT;
            startNode.inventoryPeriod = Convert.ToInt32(xnl[0]["period"].InnerText);

            if (DatabaseStringToDateTime(xnl[0]["startdate"].InnerText) < currentDate && currentDate < DatabaseStringToDateTime(xnl[0]["enddate"].InnerText))
            {
                startNode.Enabled = true;
                startNode.NodeFont = underLine;
                
            }
            else
            {
                startNode.Enabled = false;
            }

            ///////addRooms(startNode);

            for (int i = 1; i < xnl.Count; i++)
            {
                TreeNodeWithID tn = new TreeNodeWithID(xnl[i].Attributes["namn"].Value.ToString());
                tn.orgID = Convert.ToInt32(xnl[i].Attributes["orgnod"].Value);
                //org = tn.orgID;
                tn.ID = Convert.ToInt32(xnl[i].Attributes["id"].Value);
                tn.parentID = Convert.ToInt32(xnl[i].Attributes["pappa"].Value);
                tn.nodeType = TreeNodeWithID.type.DEPARTMENT;
                tn.inventoryPeriod = Convert.ToInt32(xnl[0]["period"].InnerText);

                if (DatabaseStringToDateTime(xnl[i]["startdate"].InnerText) < currentDate && currentDate < DatabaseStringToDateTime(xnl[i]["enddate"].InnerText))
                {
                    tn.Enabled = true;
                    tn.NodeFont = underLine;
                }
                else
                {
                    //TODO: Change this to indicate it's ok to move articles, but not to do inventory.
                    tn.Enabled = false;
                }

                nodeList.Add(tn);
            }


            foreach (TreeNodeWithID node in nodeList)
            {
                //if treenode contains a node with the id of pappa, add to that node
                if (!AddTreeNodeToParent(startNode.Nodes, node))
                {
                    
                    startNode.Nodes.Add(node);
                }
                
                ///////////addRooms(node);
                

                //else add directly to treenode
            }

            List<TreeNodeWithID> trees = new List<TreeNodeWithID>();

            for (int i = startNode.Nodes.Count - 1; i >= 0; i--)
            {
                TreeNodeWithID temp = (TreeNodeWithID)startNode.Nodes[i];
                if (temp.parentID != startNode.ID && temp.nodeType == TreeNodeWithID.type.DEPARTMENT)
                {
                    //addRooms(temp);
                    trees.Add(temp);
                    startNode.Nodes.Remove(temp);
                    //i--;
                }
            }


            trees.Add(startNode);

            return trees.ToArray();
        }


        private bool AddTreeNodeToParent(TreeNodeCollection nodes, TreeNodeWithID newNode)
        {
            foreach (TreeNodeWithID node in nodes)
            {
                if (AddTreeNodeToParent(node.Nodes, newNode))
                {
                    return true;
                }
                else if (node.ID == newNode.parentID && node.nodeType == newNode.nodeType)
                {
                    node.Nodes.Add(newNode);
                    return true;
                }

            }
            return false;
        }

        private void addRooms(TreeNodeWithID departmentNode)
        {
            barcode.BarcodeService bc = FormMain.getBarcodeService();

            try
            {
                string roomXmlString = bc.GetRooms(database, verkID, Convert.ToInt32(departmentNode.orgID), orgAr);

                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(roomXmlString);
                System.Diagnostics.Debug.Write(roomXmlString);


                XmlNode fastInfo = xDoc.SelectSingleNode("Fastinfo");
                if (Convert.ToInt32(fastInfo.Attributes["antal"].Value) > 0)
                {
                    int orgnod = -1;

                    if (fastInfo != null)
                    {
                        orgnod = Convert.ToInt32(fastInfo.Attributes["Orgnod"].Value);
                    }


                    List<TreeNodeWithID> nodeList = new List<TreeNodeWithID>();
                    XmlNodeList xRoomList = fastInfo.ChildNodes;
                    for (int i = 0; i < xRoomList.Count; i++)
                    {
                        TreeNodeWithID tn = new TreeNodeWithID(xRoomList[i]["Namn"].InnerText);
                        tn.orgID = orgnod;
                        tn.ID = Convert.ToInt32(xRoomList[i]["Id"].InnerText);
                        tn.parentID = Convert.ToInt32(xRoomList[i]["Pappa"].InnerText);
                        tn.inventoryPeriod = departmentNode.inventoryPeriod;

                        int roomType = Convert.ToInt32(xRoomList[i]["Typ"].InnerText);
                        switch (roomType)
                        {
                            case 6:
                                tn.nodeType = TreeNodeWithID.type.ROOM;
                                break;
                            case 5:
                                tn.nodeType = TreeNodeWithID.type.FLOOR;
                                break;
                            case 3:
                                tn.nodeType = TreeNodeWithID.type.BUILDING;
                                break;
                        }
                        tn.dataBase = xRoomList[i]["LokalDb"].InnerText;

                        if (departmentNode.Enabled)
                        {
                            //tn.ForeColor = Color.FromArgb(0, 51, 195, 224);
                            if (roomType == 6)
                            {
                                string sDate = xRoomList[i]["datum"].InnerText;
                                if (sDate != "0")
                                {
                                    tn.date = DatabaseStringToDateTime(sDate);
                                    tn.ToolTipText = tn.date.ToShortDateString();
                                }

                                int status = Convert.ToInt32(xRoomList[i]["status"].InnerText);
                                switch (status)
                                {
                                    case 1:
                                        tn.Text += partlyDone;
                                        tn.NodeFont = new Font(lockerTreeView.Font, FontStyle.Bold);
                                        break;
                                    case 2:
                                        tn.Text += done;
                                        break;
                                    case 0:
                                        tn.NodeFont = new Font(lockerTreeView.Font, FontStyle.Bold);
                                        break;
                                }
                            }
                            
                        }
                        else
                        {
                            tn.Enabled = false;
                            //tn.ForeColor = Color.Gray;
                        }
                        addCabinet(tn);

                        nodeList.Add(tn);

                        if (roomType == 3)
                        {
                            departmentNode.Nodes.Add(tn);
                        }
                    }

                    foreach (TreeNodeWithID node in nodeList)
                    {
                        foreach (TreeNodeWithID parentNode in nodeList)
                        {
                            if (parentNode.ID == node.parentID)
                            {
                                parentNode.Nodes.Add(node);
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Error while getting room");
            }
        }

        private void addCabinet(TreeNodeWithID room)
        {

            barcode.BarcodeService bc = FormMain.getBarcodeService();

            try
            {
                string xCabString = bc.GetInventoryCabinets(database, room.ID, room.orgID, orgAr);
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xCabString);
                //System.Diagnostics.Debug.Write(xCabString);

                XmlNode StorageInfo = xDoc.SelectSingleNode("Storageinfo");
                //start by adding the nodes.
                if (Convert.ToInt32(StorageInfo.Attributes["antal"].Value) > 0)
                {
                    XmlNodeList xCabinetList = StorageInfo.ChildNodes;

                    for (int i = 0; i < xCabinetList.Count; i++)
                    {
                        TreeNodeWithID tn = new TreeNodeWithID(xCabinetList[i]["Namn"].InnerText);
                        tn.orgID = room.orgID;
                        //org = tn.orgID;
                        tn.ID = Convert.ToInt32(xCabinetList[i]["Id"].InnerText);
                        tn.parentID = Convert.ToInt32(xCabinetList[i]["Pappa"].InnerText);
                        tn.nodeType = TreeNodeWithID.type.CABINET;
                        tn.dataBase = room.dataBase;
                        tn.inventoryPeriod = room.inventoryPeriod;

                        string sDate = xCabinetList[i]["datum"].InnerText;
                        if (sDate != "0")
                        {
                            tn.date = DatabaseStringToDateTime(sDate);
                            tn.ToolTipText = tn.date.ToShortDateString();
                        }

                        //TODO:Ask anders about bold, italic and nomral

                        if (room.Enabled)
                        {
                            //tn.ForeColor = Color.FromArgb(0, 51, 195, 224);
                            //tn.NodeFont = underLine;

                            int status = Convert.ToInt32(xCabinetList[i]["status"].InnerText);
                            if (status == 1)
                            {
                                tn.Text += partlyDone;
                                tn.NodeFont = new Font(lockerTreeView.Font, FontStyle.Bold);
                            }
                            else if (status == 2)
                            {
                                tn.Text += done;
                            }
                            else if (status == 0)
                            {
                                tn.NodeFont = new Font(lockerTreeView.Font, FontStyle.Bold);
                            }
                        }
                        else
                        {
                            tn.Enabled = false;
                            //tn.ForeColor = Color.Gray;
                        }


                        if (!AddTreeNodeToParent(room.Nodes, tn))
                        {
                            //room.Nodes.Add(tn);
                            room.Nodes.Add(tn);

                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Error while getting cabinet");
            }
        }

        private DateTime DatabaseStringToDateTime(string time)
        {
            try
            {
                int year = int.Parse(time.Substring(0, 4));
                int month = int.Parse(time.Substring(4, 2));
                int day = int.Parse(time.Substring(6, 2));
                return new DateTime(year, month, day);
            }
            catch
            {
                return new DateTime();
            }
            
        }

        private TreeNodeWithID getRoomForCabinet(TreeNodeWithID cabinetNode)
        {

            while (cabinetNode.Parent != null)
            {
                if (cabinetNode.nodeType == TreeNodeWithID.type.ROOM)
                {
                    return cabinetNode;
                }
                else
                {
                    cabinetNode = (TreeNodeWithID)cabinetNode.Parent;
                }
            }

            return null;
        }


        //End of Methods for updating the treeView

        private void form_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 65 && e.KeyChar <= 122)))
            {
                //scannerTimer.Enabled = false;
                scannerTimer.Stop();

                barcode += e.KeyChar;

                //scannerTimer.Enabled = true;
                scannerTimer.Start();
            }
        }


        private void startInventoryButton_Click(object sender, EventArgs e)
        {
            startInventory();
        }

        private void startInventory()
        {
            lockerTreeView.Enabled = false;
            hideInventoryPanel.Visible = false;
            scannerCheckBox.Enabled = true;

            if (!scannerCheckBox.Checked)
            {
                codeInputBox.Enabled = true;
                addButton.Enabled = true;
            }

            barcode.BarcodeService bc = FormMain.getBarcodeService();

            int cabinetID = -1;
            int orgID = selectedNode.orgID;
            int lokalID;

            if (selectedNode.nodeType == TreeNodeWithID.type.CABINET)
            {
                lokalID = getRoomForCabinet(selectedNode).ID;
                cabinetID = selectedNode.ID;
            }
            else
            {
                lokalID = selectedNode.ID;
            }


            string xInventoryList = "";
            //string xInventoryList = "";
            try
            {
                //System.Diagnostics.Debug.Write(database + "\n" + "cabinetID:" + cabinetID + "\n" + "orgID: " + orgID + "\n" + "lokalID:" + lokalID + "\n" + "OrgAr: " + orgAr + "\n");
                xInventoryList = bc.GetInventoryList(database, kemiDB, selectedNode.dataBase, orgID, lokalID, cabinetID, orgAr);

            }
            catch (Exception err)
            {
                MessageBox.Show("Problem getting room. ID: " + lokalID + "\n orgID: " + orgID + "\ncabinetID: " + cabinetID + "\nOrgar: " + orgAr);
                bc.Dispose();
                return;
            }


            fillDataGrid(xInventoryList);

            allDoneButton.Enabled = true;
            partlyDoneButton.Enabled = true;
            scanEnabled = true;
            hasChanged = false;

            dgwInventoryItems.ClearSelection();

            dgwInventoryItems.Focus();
        }


        private DataGridViewRow getMatch()
        {
            if (barcode.StartsWith("FOI") || barcode.StartsWith("NPF") || barcode.StartsWith("foi") || barcode.StartsWith("npf"))
            {
                barcode = barcode.Remove(0,3); 
            }

            // hantering av BB koder som måste införas i version 1.26

            //if (barcode.StartsWith("bb") || barcode.StartsWith("BB") )
            //{
            //    barcode = barcode.Remove(0, 1);
            //}

            foreach (DataGridViewRow row in dgwInventoryItems.Rows)
            {
                string code = (string)row.Cells["barcodeCode"].Value;

                if (code.StartsWith("FOI") || code.StartsWith("NPF"))
                {
                    code = code.Remove(0, 3);
                }
                if (string.Equals(code, barcode, StringComparison.CurrentCultureIgnoreCase))
                {
                    return row;
                }
            }
            return null;
        }

        private void Inventory_FormClosing(object sender, FormClosingEventArgs e)
        {
            //TODO: handle unwanted closings
            if (inventoryList.Count > 0)
            {
                e.Cancel = true;
                MessageBox.Show("If you close now your work will be lost. Please finish taking inventory of the room by clicking Partly Done", "Warning");
            }
        }

        private void allDoneButton_Click(object sender, EventArgs e)
        {
            if (inventoryList.Count > 0)
            {
                UpdateInvetoryInDatabase();
                inventoryList.Clear();
            }

            List<DataGridViewRow> rowList = new List<DataGridViewRow>();
            Font bold = new Font(dgwInventoryItems.DefaultCellStyle.Font, FontStyle.Bold);
            foreach (DataGridViewRow row in dgwInventoryItems.Rows)
            {
                if (row.Cells["inventorydone"].Value != "yes")
                {
                    rowList.Add(row);
                }
                //else
                //{
                //bold = new Font(dgwInventoryItems.DefaultCellStyle.Font, FontStyle.Italic);
                //}
            }

            bool allDone = true;
            if (rowList.Count > 0)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    Form leftovers = new FormInventoryLeftovers(rowList, selectedNode.inventoryPeriod);
                    DialogResult dia = leftovers.ShowDialog(this.Owner);

                    if (dia == System.Windows.Forms.DialogResult.Cancel)
                    {
                        allDone = false;
                    }

                    leftovers.Dispose();
                }));
            }

            if (allDone)
            {
                if (hasChanged)
                {
                    resetInventoryViewWithText("Articles have been updated." + Environment.NewLine + "Inventory has been completed for " + lockerTreeView.SelectedNode.Text);
                }
                else
                {
                    resetInventoryViewWithText("Inventory has been completed for " + lockerTreeView.SelectedNode.Text);
                }

                AppendStatusTextToNode(selectedNode, done);

                selectedNode.NodeFont = new Font(lockerTreeView.Font, FontStyle.Regular);
            }
            else
            {
                if (hasChanged)
                {
                    resetInventoryViewWithText("Final step was aborted." + Environment.NewLine + "Status set to partly done for " + lockerTreeView.SelectedNode.Text + Environment.NewLine + "Articles have been updated.");


                    AppendStatusTextToNode(selectedNode, "(partly done)");
                    
                }
                else
                {
                    resetInventoryViewWithText("Final step was aborted." + Environment.NewLine + "No articles updated in" + lockerTreeView.SelectedNode.Text);
                }


            }
            updateKpinvStatusTable((TreeNodeWithID)lockerTreeView.SelectedNode);
        }


        private void AppendStatusTextToNode(TreeNodeWithID node, string text)
        {
            node.Text = node.Text.Replace(partlyDone, string.Empty);
            node.Text = node.Text.Replace(done, string.Empty);

            node.Text += text;
        }

        private void partlyDoneButton_Click(object sender, EventArgs e)
        {
            makePartlyDone();
        }


        private void makePartlyDone()
        {
            if (!hasChanged)
            {
                resetInventoryViewWithText("Nothing was changed in " + lockerTreeView.SelectedNode.Text + Environment.NewLine + "No articles have been updated.");
                return;
            }


            bool allDone = true;
            foreach (DataGridViewRow row in dgwInventoryItems.Rows)
            {
                if (row.Cells["inventorydone"].Value.ToString() != "yes")
                {
                    allDone = false;
                    break;
                }
            }

            if (inventoryList.Count > 0)
            {
                UpdateInvetoryInDatabase();
                inventoryList.Clear();
            }

            resetInventoryViewWithText("Articles have been updated." + Environment.NewLine + "Status set to partly done for " + lockerTreeView.SelectedNode.Text);


            if (allDone)
            {
                AppendStatusTextToNode(selectedNode, done);
            }
            else
            {
                AppendStatusTextToNode(selectedNode, partlyDone);
            }

            updateKpinvStatusTable(selectedNode);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            barcode = codeInputBox.Text;

            takeInventoryOfBarcode();
            barcode = "";
            codeInputBox.SelectionStart = 0;
            codeInputBox.SelectionLength = codeInputBox.Text.Length;
        }

        private void scannerTimer_Elapsed(object source, ElapsedEventArgs e)
        {
            //scannerTimer.Enabled = false;
            scannerTimer.Stop();
            Log = FormMain.Get.PropertyCheck;

            ////exempel

            //if (barcode.StartsWith("2"))
            //{
            //    string barcodeEX = barcode.Substring(2, barcode.Length - 2);
            //    barcode = barcodeEX;

            //}

            //if (barcode.EndsWith("0"))
            //{
            //    string barcodeEX = barcode.Remove(barcode.Length - 1);
            //    barcode = barcodeEX;

            //}


            if ((barcodeInSettings == barcode) && (FormMain.Get.LoginBarcode))
            {
                //kolla om barcode är samma som inloggningsbarcode
                // Sant=logga ut och stäng programmet
                this.Invoke(new MethodInvoker(delegate { FormMain.Get.Logout(); }));
                //this.Close();
                //this.Owner.Close();
            }

            if ((barcode.StartsWith("r") || barcode.StartsWith("SL") || barcode.StartsWith("sl") || barcode.StartsWith("R")) && FormMain.Get.ScanRoomsEnabled)
            {
                changeRoomWithBarcode();
            }
            else if (scannerCheckBox.Checked && scanEnabled)
            {
                takeInventoryOfBarcode();
            }
            //Sandi
            //else if(scannerCheckBox.Checked && Log==true )
            //{
            //    if (barcodeInSettings == barcode)            
            //    {
            //    //kolla om barcode är samma som inloggningsbarcode
            //    // Sant=logga ut och stäng programmet
            //        this.Invoke(new MethodInvoker(delegate { FormMain.Get.Logout(); })); 
            //        //this.Close();
            //        //this.Owner.Close();
            //    }

                
               
            //}
            
            barcode = "";
        }

        private void changeRoomWithBarcode()
        {
            barcode.BarcodeService bc = FormMain.getBarcodeService();
            //string orgID = Convert.ToString(selectedNode.orgID);
            string locationString = bc.GetLocationWithBarcode(database, barcode);
            bc.Dispose();


            //Borttagen rumbarcode??
            if (locationString == "")
            {
                System.Media.SystemSounds.Hand.Play(); 
                MessageBox.Show("Location barcode " + barcode + " could not be found in the database.");
                barcode = "";
                
            }

            System.Diagnostics.Debug.Write("----------" + locationString + "--------------\n");
            //System.Diagnostics.Debug.Write(locationString.Split(',')[0] + "    " + locationString.Split(',')[1] + "\n");
            System.Diagnostics.Debug.Write(locationString.Split(',')[0] + "    " + locationString.Split(',')[1] + locationString.Split(',')[2] + "\n");

            iterateLocation = Convert.ToInt32(locationString.Split(',')[0]);
            iterateType = Convert.ToInt32(locationString.Split(',')[1]);
            iterateOrg = Convert.ToInt32(locationString.Split(',')[2]);


            //Nytt
            if (last_barcode == iterateLocation)
            {
                //MessageBox.Show("You scanned the same room");
                return;
            }
            else
            {

                this.Invoke(new MethodInvoker(delegate
                {
                    int i = 0;
                    System.Diagnostics.Debug.Write(0 + "\n");
                    if (lockerTreeView.Enabled == false)
                    {
                        makePartlyDone();
                    }

                    System.Diagnostics.Debug.Write(1 + "\n");

                    ////////Nytt
                    //if(barcode==old_barcode)
                    //{

                    for (i = 0; i < lockerTreeView.Nodes.Count; i++)
                    {
                        selectedNode = (TreeNodeWithID)lockerTreeView.Nodes[i];
                        selectedNode.nodeType = TreeNodeWithID.type.DEPARTMENT;
                        if (selectedNode.nodeType == TreeNodeWithID.type.DEPARTMENT)
                        {
                            if (selectedNode.orgID == iterateOrg)
                            {
                                if (!selectedNode.IsExpanded)
                                {
                                    addRooms(selectedNode);
                                    selectedNode.Expand();
                                    if (iterateType == 0)
                                    {
                                        selectedNode.nodeType = TreeNodeWithID.type.ROOM;
                                        last_barcode = iterateLocation;
                                    }
                                    else
                                    {
                                        selectedNode.nodeType = TreeNodeWithID.type.CABINET;
                                        last_barcode = iterateLocation;
                                    }

                                    iterateTree((TreeNodeWithID)lockerTreeView.Nodes[i]);
                                    startInventoryButton.PerformClick();
                                    break;
                                }

                                else
                                {
                                    selectedNode = (TreeNodeWithID)lockerTreeView.Nodes[i];
                                    if (iterateType == 0)
                                    {
                                        selectedNode.nodeType = TreeNodeWithID.type.ROOM;
                                        last_barcode = iterateLocation;
                                    }
                                    else
                                    {
                                        selectedNode.nodeType = TreeNodeWithID.type.CABINET;
                                        last_barcode = iterateLocation;
                                    }
                                    iterateTree((TreeNodeWithID)lockerTreeView.Nodes[i]);
                                    startInventoryButton.PerformClick();
                                    break;
                                }

                            }
                            else
                            {
                                //break;
                            }


                            //    ////// slut nytt

                            //    // iterateTree((TreeNodeWithID)lockerTreeView.Nodes[i]); // ändring 141001
                            //    //iterateTree((TreeNodeWithID)lockerTreeView.SelectedNode);20470

                        }

                    }

                    //}
                    //else { return; }
                    //iterateTree((TreeNodeWithID)lockerTreeView.Nodes[i]);
                    //iterateTree((TreeNodeWithID)lockerTreeView.SelectedNode);
                    //    System.Diagnostics.Debug.Write(2 + "\n");20348
                    //    startInventoryButton.PerformClick();
                    //    System.Diagnostics.Debug.Write(3 + "\n");

                }));
            }
        }


        //These three must be set first!
        private int iterateLocation;
        private int iterateType;
        private int iterateOrg;

        private void iterateTree(TreeNodeWithID node)
        {
            System.Diagnostics.Debug.Write(node.ID + "\n");
            if ((node.ID == iterateLocation) && ((iterateType == 0 && node.nodeType == TreeNodeWithID.type.ROOM) || (iterateType == 1 && node.nodeType == TreeNodeWithID.type.CABINET)))
            {
                System.Diagnostics.Debug.Write(1 + "\n");
                this.Invoke(new MethodInvoker(delegate
                {
                    
                    lockerTreeView.SelectedNode = node;

                }));
                System.Diagnostics.Debug.Write(2 + "\n");
                return;   
            }

            //if (node.Nodes.Count > 0)
            //{
            //    foreach (TreeNodeWithID tn in node.Nodes)
            //    {
            //        iterateTree(tn);
            //    }
            //}

            else if (node.Nodes.Count > 0)
            {
                foreach (TreeNodeWithID tn in node.Nodes)
                {
                    iterateTree(tn);
                }
            }

            else { return; }
        }

        private void takeInventoryOfBarcode()
        {
            DataGridViewRow row = getMatch();
            //label1.Enabled = true;
            //label1.Visible = true;

            if (row != null)
            {
                //TODO: Take inventory of row
                row.Cells["inventorydone"].Value = "yes";
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.Font = new Font(dgwInventoryItems.DefaultCellStyle.Font, FontStyle.Bold);
                    
                }

                if (selectedNode.Enabled)
                {
                    inventoryList.Add(Convert.ToInt32(row.Cells["TransID"].Value));
                    hasChanged = true;
                }
                
                if (inventoryList.Count > 25)
                {
                    UpdateInvetoryInDatabase();
                    inventoryList.Clear();
                }


                this.Invoke(new MethodInvoker(delegate
                {
                    partlyDoneButton.Text = "Partly Done";
                }));
            }
            else
            {
                //Ändring ver 1.18
                if (barcode != "")
                {
                    //TODO: Kolla om streckkoden finns i databasen, och i så fall: fråga om användaren vill flytta produkten.
                    //MessageBox.Show(barcode);
                    checkForBarcodeInDatabase(barcode);
                }
                else {
                    MessageBox.Show("You need to scan your barcode!");
                
                }
                }

        }

        private void UpdateInvetoryInDatabase()
        {
            string CommaDelimitedList = "";

            foreach (int id in inventoryList)
            {
                CommaDelimitedList += id + ",";
            }
            //Remove last comma
            CommaDelimitedList = CommaDelimitedList.Remove(CommaDelimitedList.Length - 1);

            barcode.BarcodeService bc = FormMain.getBarcodeService();

            try
            {
                bc.TakeInventory(CommaDelimitedList, FormMain.Get.Username, selectedNode.inventoryPeriod, database);

                bc.Dispose();
            }
            catch (Exception err)
            {
                MessageBox.Show("Failed while updating database.\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "UpdateInventoryInDatabase");
                bc.Dispose();
            }
        }

        private void checkForBarcodeInDatabase(string unknownBarcode)
        {
            barcode.BarcodeService bc = FormMain.getBarcodeService();

            this.Invoke(new MethodInvoker(delegate
                   {
                       //****** KI anpassning v 1.18 *****//

                       string barcodeEX = "";
                   
                       txtScannedBarcode.Text = unknownBarcode;
                       if (unknownBarcode.StartsWith("bb") || (unknownBarcode.StartsWith("BB")))
                       {
                           barcodeEX = unknownBarcode.Substring(1, unknownBarcode.Length - 1);
                           unknownBarcode = barcodeEX.ToString();
                       }

                       if (unknownBarcode.StartsWith("`") || unknownBarcode.StartsWith("+"))
                       {
                           barcodeEX = unknownBarcode.Substring(2, unknownBarcode.Length - 2);
                           unknownBarcode = barcodeEX.ToString();

                       }

                       if (unknownBarcode.EndsWith("¤") || unknownBarcode.EndsWith("$"))
                       {
                           string barcodeEX1 = unknownBarcode.Remove(unknownBarcode.Length - 1);
                           unknownBarcode = barcodeEX1.ToString();

                       }


                       //****** slut KI anpassning v 1.18 *****//

                       try
                       {
                           string xHit = bc.CheckIfBarcodeExists(database, unknownBarcode, orgAr, kemiDB, selectedNode.orgID);

                           //System.Diagnostics.Debug.Write(xHit);
                           XmlDocument xDoc = new XmlDocument();
                           xDoc.LoadXml(xHit);
                           XmlNode xHitNode = xDoc.SelectSingleNode("itemlocation");

                           bc.Dispose();
                           if (Convert.ToInt32(xHitNode.Attributes["hit"].Value)==0)
                           {
                               m_Status = 0;
                           }
                           else
                           {
                               m_Status = Convert.ToInt32(xHitNode["status"].InnerText);
                           }

                           //ändring 20160817

                           ///// om det finns dubbletter - skriv felmeddelande /////

                           if (Convert.ToInt32(xHitNode.Attributes["hit"].Value) == 5)
                           {
                               MessageBox.Show("The barcode was not moved. A duplicate exist, pls move the item in the web application.", "Inventory");
                           }
                           else if (Convert.ToInt32(xHitNode.Attributes["hit"].Value) == 5)
                           {

                           }
                           else
                           {
                               //

                               if (Convert.ToInt32(xHitNode.Attributes["hit"].Value) > 0 && Convert.ToInt32(xHitNode["status"].InnerText)==0)
                               {
                                   //Kolla året som kommer från sql
                                   int xHitar = (Convert.ToInt32(xHitNode["orgar"].InnerText));

                                   bool sameDepartment;
                                   if (Convert.ToInt32(xHitNode["orgid"].InnerText) == selectedNode.orgID)
                                   {
                                       sameDepartment = true;
                                   }
                                   else
                                   {
                                       sameDepartment = false;
                                   }

                                   int roomID;
                                   if (selectedNode.nodeType == TreeNodeWithID.type.CABINET)
                                   {
                                       roomID = getRoomForCabinet(selectedNode).ID;
                                   }
                                   else
                                   {
                                       roomID = selectedNode.ID;
                                   }

                                   DialogResult transferResult = new DialogResult();

                                   System.Media.SystemSounds.Asterisk.Play();
                                   this.Invoke(new MethodInvoker(delegate
                                   {
                                       if (FormMain.Get.ShowDialogOnSameDepartment)
                                       {

                                           Form transfer = new FormTransferInventoryDialog(xHitNode, selectedNode, unknownBarcode, database, roomID, orgAr, sameDepartment);

                                           transferResult = transfer.ShowDialog(this.Owner);
                                           transfer.Dispose();

                                       }
                                       else
                                       {
                                           if (!sameDepartment)
                                           {
                                               Form transfer = new FormTransferInventoryDialog(xHitNode, selectedNode, unknownBarcode, database, roomID, orgAr, sameDepartment);

                                               transferResult = transfer.ShowDialog(this.Owner);
                                               transfer.Dispose();
                                           }
                                           else
                                           {
                                               bc = FormMain.getBarcodeService(); // Skapar webserviceobjekt

                                               int destinationCabinet = -1;
                                               if (selectedNode.nodeType == TreeNodeWithID.type.CABINET)
                                               {
                                                   destinationCabinet = selectedNode.ID;
                                               }

                                               try
                                               {

                                                   int StorageProperty = Convert.ToInt32(bc.GetRoomProp(database, roomID, destinationCabinet));
                                                   if (StorageProperty == 0)
                                                   {
                                                       StorageProperty = Convert.ToInt32(bc.GetStorageParent(database, roomID, destinationCabinet));
                                                   }
                                                   else
                                                   {

                                                   }

                                                   // Hantering som gäller för Produktegenskaper
                                                   if (Log == true)
                                                   {
                                                       int n_transid = Convert.ToInt32(xHitNode["transid"].InnerText);


                                                       int klaraid = Convert.ToInt32(xHitNode["klaraid"].InnerText);
                                                       int ProdProperty = Convert.ToInt32(bc.GetPropName(database, n_transid));

                                                       if ((StorageProperty == ProdProperty) || (StorageProperty == Convert.ToInt32(Property.Omärkt)))
                                                       {

                                                           bc.TransferArticle(Convert.ToInt32(xHitNode["transid"].InnerText), Convert.ToInt32(xHitNode["id"].InnerText), destinationCabinet, roomID, selectedNode.orgID, Convert.ToInt32(xHitNode["period"].InnerText), orgAr, FormMain.Get.Username, int.Parse(xHitNode["type"].InnerText), database, selectedNode.Enabled);
                                                           transferResult = System.Windows.Forms.DialogResult.Yes;
                                                           System.Media.SystemSounds.Asterisk.Play();
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

                                                               SoundPlayer pl = new SoundPlayer(BarcodePcApp.Properties.Resources.WrongLocation);
                                                               pl.Play();
                                                               MessageBox.Show("[Barcode: " + unknownBarcode + "]. Property on product and location does not match." + Environment.NewLine + "The product has not any property");
                                                               pl.Stop();
                                                           }

                                                           else
                                                           {
                                                               string ProdPropName = dr["nameEng"].ToString();

                                                               SoundPlayer pl = new SoundPlayer(BarcodePcApp.Properties.Resources.WrongLocation);
                                                               pl.Play();
                                                               MessageBox.Show("[Barcode: " + unknownBarcode + "]. Property on product and location does not match." + Environment.NewLine + "The product is considered to be [ " + ProdPropName + " ]");
                                                               pl.Stop();
                                                           }

                                                       }
                                                   }
                                                   else
                                                   {
                                                       bc.TransferArticle(Convert.ToInt32(xHitNode["transid"].InnerText), Convert.ToInt32(xHitNode["id"].InnerText), destinationCabinet, roomID, selectedNode.orgID, Convert.ToInt32(xHitNode["period"].InnerText), orgAr, FormMain.Get.Username, int.Parse(xHitNode["type"].InnerText), database, selectedNode.Enabled);
                                                       transferResult = System.Windows.Forms.DialogResult.Yes;
                                                   }
                                               }
                                               catch (Exception err)
                                               {
                                                   MessageBox.Show("Error while transfering article.\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "Transfer");
                                               }

                                           }


                                       }

                                       //If the article is moved to user position, make it bold to show it's been added to inventory.
                                       if (transferResult == System.Windows.Forms.DialogResult.Yes)
                                       {
                                           string[] row = new string[11];

                                           row[0] = unknownBarcode;
                                           row[1] = xHitNode["klaraid"].InnerText;
                                           row[2] = xHitNode["itemname"].InnerText;
                                           row[3] = xHitNode["amount"].InnerText;
                                           row[4] = xHitNode["unit"].InnerText;
                                           row[5] = xHitNode["note"].InnerText;
                                           row[6] = xHitNode["transid"].InnerText;
                                           row[7] = xHitNode["id"].InnerText;
                                           row[8] = xHitNode["vem"].InnerText;
                                           row[9] = xHitNode["nar"].InnerText;
                                           row[10] = "yes";

                                           //dgwInventoryItems.Rows.Add(row);
                                           dgwInventoryItems.Rows.Insert(0, row);

                                           //makeRowBold();
                                           barcode = unknownBarcode;
                                           if (selectedNode.Enabled)
                                           {
                                               takeInventoryOfBarcode();
                                           }

                                           hasChanged = true;

                                           barcode.BarcodeService webs = FormMain.getBarcodeService();


                                           webs.UpdateKpinvStatus(database, Convert.ToInt32(xHitNode["orgid"].InnerText), orgAr, Convert.ToInt32(xHitNode["storageid"].InnerText), Convert.ToInt32(xHitNode["lokalid"].InnerText));
                                           webs.Dispose();

                                           partlyDoneButton.Text = "Partly Done";
                                       }
                                   }));
                               }
                               else
                               {
                                   // Hämta kundinformation, kemidatabas mm från webservern
                                   string infoString = "";
                                   try
                                   {
                                       infoString = bc.GetInfo(FormMain.Get.Databas,verkID);
                                   }
                                   catch (Exception err)
                                   {
                                       MessageBox.Show("No contact with the web server!\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message);
                                       Application.Exit();
                                       return;
                                   }
                                   DataSet ds = new DataSet();
                                   StringReader sri = new StringReader(infoString);
                                   ds.ReadXml(sri);
                                   DataTable dt = ds.Tables[0];
                                   DataRow dr = dt.Rows[0];

                                   m_sRelease = dr["release"].ToString().ToUpper();
                                   if (m_sRelease=="1" && m_Status < 0) // ersätts med en parameter i aq tabellen
                                   {
                                       System.Media.SystemSounds.Exclamation.Play();
                                       MessageBox.Show("(Barcode " + unknownBarcode + ") This product has not yet been released and your request is denied.", "Error while reading barcode");

                                   }
                                   else
                                   {
                                       System.Media.SystemSounds.Exclamation.Play();
                                       MessageBox.Show(unknownBarcode + " could not be found in the database. Please check if you scanned the correct barcode or that you entered it correcly.", "Error while reading barcode");
                                   }
                                   
                               }
                           }
                       } //else
                       catch (Exception err)
                       {
                           MessageBox.Show("Communications failed while looking for the barcode in the database.\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message, "checkForBarcodeInDatabase");
                       }

                   }));
        }

        private void codeInputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                addButton.PerformClick();
            }
        }

        private void resetInventoryViewWithText(string displayText)
        {
            partlyDoneButton.Text = "Cancel";
            scanEnabled = false;
            hideInventoryPanel.Visible = true;
            startInventoryButton.Visible = true;
            inventoryInstructionLabel.Text = displayText;
            //Place label in the center of hideInventoryPanel
            //inventoryInstructionLabel.Left = hideInventoryPanel.Width / 2 - inventoryInstructionLabel.Width / 2;

            lockerTreeView.Enabled = true;

            allDoneButton.Enabled = false;
            partlyDoneButton.Enabled = false;
            codeInputBox.Enabled = false;
            scannerCheckBox.Enabled = false;
            addButton.Enabled = false;

            codeInputBox.Clear();
        }

        private void updateKpinvStatusTable(TreeNodeWithID tn)
        {
            barcode.BarcodeService bc = FormMain.getBarcodeService();

            int platsid = 0;
            int lokalid;
            if (tn.nodeType == TreeNodeWithID.type.CABINET)
            {
                platsid = tn.ID;
                lokalid = getRoomForCabinet(tn).ID;
            }
            else
            {
                lokalid = tn.ID;
            }
            try
            {
                bc.UpdateKpinvStatus(database, tn.orgID, orgAr, platsid, lokalid);
                bc.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while updating the database.\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + e.Message, "updateKpinvStatusTable");
                bc.Dispose();
            }
        }

        private void printBarcodesButton_Click(object sender, EventArgs e)
        {
            lockerTreeView.Enabled = false;

            int cabinetID = -1;
            int orgID = selectedNode.orgID;
            int lokalID;

            if (selectedNode.nodeType == TreeNodeWithID.type.CABINET)
            {
                lokalID = getRoomForCabinet(selectedNode).ID;
                cabinetID = selectedNode.ID;
            }
            else
            {
                lokalID = selectedNode.ID;
            }

            Form printMany = new FormPrintBarcodes(orgID, lokalID, cabinetID, orgAr, selectedNode.dataBase);

            DialogResult result = printMany.ShowDialog(this.Owner);
            printMany.Dispose();

            lockerTreeView.Enabled = true;

        }

        private void locationBarcodeButton_Click(object sender, EventArgs e)
        {
            barcode.BarcodeService bc = FormMain.getBarcodeService();
            //TYPE = 0 for rooms	TYPE = 1 for cabinets
            int type = 0;
            //string orgid = Convert.ToString(selectedNode.orgID);
            int orgid =selectedNode.orgID;

            if(selectedNode.nodeType == TreeNodeWithID.type.CABINET)
            {
                type = 1;
            }


            //bc.Dispose();

            try
            {

                string barcode = bc.GetLocationBarcode(database, selectedNode.ID, type, orgid);
                //bc.Dispose();
                if (barcode == null)
                {

                    MessageBox.Show("Tom sträng");

                }
                //else
                //{

                    PrintBarcode(selectedNode.Text, bc.GetLocationBarcode(database, selectedNode.ID, type, orgid));
                //}
            }
            catch (Exception b)
            {

            }
        }

        private string m_sCAS = "";
        private string m_sStreckkod = "";
        private string m_sSummaformel = "";
        private string m_sComment = "";

        private void PrintBarcode(string name, string locationBarcode)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = "Fel";

            foreach (string prt in PrinterSettings.InstalledPrinters)
            {	// Installerade skrivare
                if (prt.StartsWith(FormMain.Get.BarcodePrinter, true, null))
                {
                    pd.PrinterSettings.PrinterName = prt;
                    break;
                }
            }
            if (!pd.PrinterSettings.IsValid)
            {
                MessageBox.Show("No label printer is installed");
                return;
            }

            pd.DocumentName = "Barcode for the product";
            pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocumentOnPrintPage);


            //if (comment.Length >= 6)
            //{
            //    m_sComment = comment.Substring(0, 6);
            //}
            //else
            //{
            //    m_sComment = comment;
            //}

            m_sCAS = name;

            barcodeObject.Text = "";
            barcodeObject.BarHeight = 4;
            barcodeObject.DisplayCode = false;
            barcodeObject.CodeAlignment = Neodynamic.WinControls.BarcodeProfessional.Alignment.AboveLeft;

            m_sStreckkod = locationBarcode;
            barcodeObject.Code = locationBarcode;

            //För att utskriften skall bli korrekt, ändra layout till layout 4 (3) och ändra sedan tillbaka den.
            int currentLayout = FormMain.Get.BarcodeLayout;
            
            FormMain.Get.BarcodeLayout = 3;

            pd.Print();

            FormMain.Get.BarcodeLayout = currentLayout;
        }

        private void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs ppea)
        {
            //TODO: add proper note
            BarcodeLayout.PrintDocumentOnPrintPage(sender, ppea, barcodeObject, m_sCAS, m_sSummaformel, m_sStreckkod, m_sComment,"","","","","","");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

          ////Logga ut med streckkod

        //private void textBoxScanCode_TextChanged(object sender, EventArgs e)
        //{
        //    if (textBoxScanCode.Text != "")
        //    {
        //        //labelShowProduct.Text = textBoxScanCode.Text;
        //    }
        //}



        //private void scannerTimer_Elapsed(object source, ElapsedEventArgs e)
        //{

        //    //scannerTimer.Enabled = false;
        //    scannerTimer.Stop();

        //    //User barcode and scan barcode is the same, check out user and close
        //    if (barcodeInSettings == barcode)
        //    {
        //        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
        //        DialogResult result;
        //        result = MessageBox.Show("Product has the same barcode as user.\n\n Would you like to checkout product? - push YES \n Would you like to checkout user? - push NO", "BarcodePcApp", buttons);
        //        if (result == System.Windows.Forms.DialogResult.Yes)
        //        {
        //            getProduct(barcode);
        //            barcode = "";

        //        }
        //        else
        //        {
        //            this.Close();
        //            this.Owner.Close();
        //        }

        //    }
        //    else if (barcodeInSettings != barcode)
        //    {
        //        getProduct(barcode);
        //        barcode = "";
        //    }

        //    else
        //    {


        //    }

        //}
    }
}