using SqlLightest.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SyntaxTreeBuilders
{
    public class SelectSyntaxTreeBuilder
    {
        public static SelectNode BuildSelectNode(string[] tokens)
        {
            var selectNode = new SelectNode();
            var tableIndex = 0;
            for (int i = 1; i < tokens.Length; i++)
            {
                if (tokens[i].Equals("FROM", StringComparison.CurrentCultureIgnoreCase))
                {

                    tableIndex = ++i;
                     selectNode.Table = tokens[tableIndex];
                    break;
                }
                if (tokens[i] != ",")
                    selectNode.Columns.Add(tokens[i]);
            }
           
            while (tokens[tableIndex]  != ";")
            {
                ++tableIndex;
                if (tokens[tableIndex].Equals("WHERE", StringComparison.CurrentCultureIgnoreCase))
                {
                    selectNode.Conditions = SupportSyntaxTreeBuilder.BuildConditionNodes(tokens.ToList().Skip(tableIndex).ToArray());
                    ++tableIndex;
                }
            }
      
            return selectNode;
        }
    }
}
