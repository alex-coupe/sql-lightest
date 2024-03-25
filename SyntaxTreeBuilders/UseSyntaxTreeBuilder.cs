using SqlLightest.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SyntaxTreeBuilders
{
    public class UseSyntaxTreeBuilder
    {
        public static UseNode BuildUseNode(string[] tokens)
        {
            return new UseNode(tokens[1]);
        }
    }
}
