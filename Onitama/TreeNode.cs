using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    class TreeNode
    {
        private int depth;
        private List<Pawn> data;
        private TreeNode parent;
        private List<TreeNode> children;

        public TreeNode(int depth, List<Pawn> data, TreeNode parent, List<TreeNode> children)
        {
            this.depth = depth;
            this.data = data;
            this.parent = parent;
            this.children = children;
        }
    }
}
