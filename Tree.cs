using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest
{
    public class Tree<T>(T root)
    {
        public TreeNode<T> Root { get; set; } = new TreeNode<T>(root);
    }

    public class TreeNode<T>(T value)
    {
        public T Value { get; set; } = value;
        public List<TreeNode<T>> Children { get; set;} = [];
    }
}
