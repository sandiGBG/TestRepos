using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace BarcodePcApp
{
    public partial class FormOrgtrad : Form
    {
        private int m_nIndex = 0;

        public int m_nOrgnod = 0;
        public string m_nOrgnamn = "";

        public FormOrgtrad()
        {
            InitializeComponent();
        }

        private void FormOrgtrad_Load(object sender, EventArgs e)
        {
            barcode.BarcodeService bc = FormMain.getBarcodeService();

            try
            {
                //OBS! Ändra DateTime.Now.Year till aktuellt år (ÅÅÅÅ-format)
                string sTree = bc.GetOrgtrad(FormMain.Get.Databas, DateTime.Now.Year, FormMain.Get.UserId);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(sTree);

                if (Convert.ToInt32(doc.DocumentElement.Attributes["antal"].Value) > 0)
                {

                    m_tree.BeginUpdate();
                    XmlNode node = doc.DocumentElement.SelectSingleNode("item");
                    if (node != null)
                    {
                        int nRootnode = Convert.ToInt32(node.Attributes["pappa"].Value);

                        TreeNode tNode = m_tree.Nodes.Add("Select a department");
                        tNode.ImageIndex = 0;

                        XmlNodeList xmlnodeList = doc.DocumentElement.SelectNodes("item");
                        AddNode(xmlnodeList, tNode);
                        m_tree.Nodes[0].ExpandAll();
                        m_tree.Nodes[0].EnsureVisible();
                        m_tree.ShowPlusMinus = false;
                        m_tree.ShowRootLines = false;

                        m_tree.Nodes[0].ImageIndex = 0;
                    }
                    m_tree.EndUpdate();
                }
                else
                {
                    DialogResult = DialogResult.Cancel;
                    MessageBox.Show(doc.DocumentElement.Attributes["msg"].Value.ToString());
                    return;
                }
            }
            catch (Exception err)
            {
                DialogResult = DialogResult.Cancel;
                MessageBox.Show("No contact with the web server!\r\n\nCheck your Internet connection.\r\n\nThe following error message was reported:\r\n\n" + err.Message,"GetOrgtrad");
                bc.Dispose();
                return;
            }
        }


        private void AddNode(XmlNodeList xmlnodeList, TreeNode inTreeNode)
        {
            while (m_nIndex < xmlnodeList.Count)
            {
                TreeNode tNode = null;
                XmlNode node = xmlnodeList[m_nIndex];
                if ((inTreeNode.ImageIndex == 0 && Convert.ToInt32(node.Attributes["orgnod"].Value) == Convert.ToInt32(node.Attributes["startorg"].Value)) || (inTreeNode.ImageIndex == Convert.ToInt32(node.Attributes["pappa"].Value)))
                {
                    tNode = inTreeNode.Nodes.Add(node.Attributes["namn"].Value.ToString());
                    tNode.Tag = node.Attributes["orgnod"].Value;
                    tNode.ImageIndex = Convert.ToInt32(node.Attributes["id"].Value);
                }
                else
                    break;

                if ((m_nIndex+1) < xmlnodeList.Count)
                {
                    node = xmlnodeList[m_nIndex+1];
                    if (inTreeNode.ImageIndex != Convert.ToInt32(node.Attributes["pappa"].Value) && tNode != null)
                    {
                        m_nIndex++;
                        AddNode(xmlnodeList, tNode);
                    }
                    else
                        m_nIndex++;
                }
                else
                    m_nIndex++;
            }
        }
        
        private void m_btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void m_btnOK_Click(object sender, EventArgs e)
        {
            if (m_tree.SelectedNode != null && m_tree.SelectedNode.ImageIndex != 0)
            {
                m_nOrgnod = Convert.ToInt32(m_tree.SelectedNode.Tag);
                m_nOrgnamn = m_tree.SelectedNode.Text;
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Du måste välja en avdelning först!");
        }

        private void m_tree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (m_tree.SelectedNode != null && m_tree.SelectedNode.ImageIndex != 0)
            {
                m_nOrgnod = Convert.ToInt32(m_tree.SelectedNode.Tag);
                m_nOrgnamn = m_tree.SelectedNode.Text;
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}