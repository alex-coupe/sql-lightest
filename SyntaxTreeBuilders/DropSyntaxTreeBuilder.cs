using SqlLightest.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SyntaxTreeBuilders
{
    public class DropSyntaxTreeBuilder
    {
        

        public static DropNode BuildDropNode(string[] tokens)
        {
            return new DropNode(tokens[2]);
        }
    }
}
