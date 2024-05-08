using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarcodePcApp.Misc
{
    public class TreeNodeWithID : System.Windows.Forms.TreeNode
    {
        public enum type { DEPARTMENT, ROOM, FLOOR, BUILDING, CABINET};

        public int ID { get; set; }
        public int parentID { get; set; }
        public type nodeType { get; set; }
        public int orgID { get; set; }
        public string dataBase { get; set; }
        public string lokaldb{ get; set; } //dubbla db projekt
        public DateTime date { get; set; }
        public int node_org { get; set; } // lagt till för upp. 14

        public bool Enabled { get; set; }
        public int inventoryPeriod { get; set; }
        
        
        public TreeNodeWithID(string name) : base(name) 
        {
            Enabled = true;
            inventoryPeriod = -1;
        }
        public TreeNodeWithID() : base() 
        {
            Enabled = true;
            inventoryPeriod = -1;
        }




        public static bool operator ==(TreeNodeWithID x, TreeNodeWithID y)
        {
            if (x.ID == y.ID && x.parentID == y.parentID && x.nodeType == y.nodeType && x.orgID == y.orgID && x.dataBase == y.dataBase)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(TreeNodeWithID x, TreeNodeWithID y)
        {
            return !(x == y);
        }

        public override bool Equals(Object obj)
        {
            return obj is TreeNodeWithID && this == (TreeNodeWithID)obj;
        }

    }
}
