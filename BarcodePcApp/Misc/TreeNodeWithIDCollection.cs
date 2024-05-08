using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarcodePcApp.Misc
{
    class TreeNodeWithIDCollection : System.Collections.CollectionBase //System.Windows.Forms.TreeNodeCollection // 
    {
        public void Add(TreeNodeWithID node)
        {
            List.Add(node);
        }

        public void Remove(int index)
        {
            if (index > Count - 1 || index < 0)
            // If no widget exists, a messagebox is shown and the operation is cancelled.
            {
                System.Windows.Forms.MessageBox.Show("Index not valid!");
            }
            else
            {
                List.RemoveAt(index);
            }
        }

        public TreeNodeWithID Item(int Index)
        {
            // The appropriate item is retrieved from the List object and
            // explicitly cast to the Widget type, then returned to the 
            // caller.
            return (TreeNodeWithID)List[Index];
        }

        public bool Contains(TreeNodeWithID node)
        {
            foreach (TreeNodeWithID listNode in List)
            {
                if (listNode == node)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns the index of a TreeNodeWithID.
        /// </summary>
        /// <param name="node">The TreeNodeWithID to find</param>
        /// <returns>Index of node in collection.</returns>
        public int IndexOf(TreeNodeWithID node)
        {
            for (int i = 0; i < List.Count; i++)
            {
                if (node == (TreeNodeWithID)List[i])
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
