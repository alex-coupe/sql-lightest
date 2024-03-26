using SqlLightest.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SyntaxTreeBuilders
{
    public class DeleteSyntaxTreeBuilder
    {
        public static DeleteNode BuildDeleteNode(string[] tokens)
        {
            DeleteNode deleteNode = new()
            {
                Table = tokens[2]
            };
            return deleteNode;
        }
    }
}
