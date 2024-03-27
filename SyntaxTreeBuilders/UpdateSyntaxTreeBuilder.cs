using SqlLightest.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SyntaxTreeBuilders
{
    public class UpdateSyntaxTreeBuilder
    {
        public static UpdateNode BuildUpdateNode(string[] tokens)
        {
            var node = new UpdateNode();

            node.Table = tokens[1];
            var index = 3;
            while(tokens[index] != ";" && !tokens[index].Equals("WHERE",StringComparison.CurrentCultureIgnoreCase) 
                && !tokens[index].Equals("ORDER") && !tokens[index].Equals("GROUP"))
            {
                if (tokens[index] == ",")
                    ++index;

                var column = tokens[index];
                var value = tokens[index + 2];

                node.ColumnValues.Add(column, value);
                index += 3;
            }

            while (tokens[index] != ";")
            {
                if (tokens[index].Equals("WHERE", StringComparison.CurrentCultureIgnoreCase))
                {
                    node.Conditions = SupportSyntaxTreeBuilder.BuildConditionNodes(tokens.ToList().Skip(index).ToArray());
                    index += 3;
                }
                else
                    ++index;
            }
            return node;
        }
    }
}
