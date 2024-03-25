using SqlLightest.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SyntaxTreeBuilders
{
    public class InsertSyntaxTreeBuilder
    {
        public static InsertNode BuildInsertNode(string[] tokens)
        {
            var node = new InsertNode
            {
                Table = tokens[2]
            };
            var valuesList = new List<string>();
            if (tokens[3].Equals("VALUES", StringComparison.CurrentCultureIgnoreCase))
            {
                int index = 5;
                while (index < tokens.Length)
                {

                    if (tokens[index] == ")")
                        break;

                    if (tokens[index] == "'" || tokens[index] == ",")
                    {
                        index++;
                    }
                    else
                    {
                        valuesList.Add(tokens[index++]);
                    }
                }
                node.Values = [.. valuesList];
            }
            else
            {
                var columnsList = new List<string>();
                int index = 4;
                while (index < tokens.Length)
                {
                    if (tokens[index] == ")")
                        break;

                    if (tokens[index] == "'" || tokens[index] == ",")
                    {
                        index++;
                    }
                    columnsList.Add(tokens[index++]);
                }
                node.Columns = [.. columnsList];
                index += 3;
                while (index < tokens.Length)
                {

                    if (tokens[index] == ")")
                        break;

                    if (tokens[index] == "'" || tokens[index] == ",")
                    {
                        index++;
                    }
                    else
                    {
                        valuesList.Add(tokens[index++]);
                    }
                }
                node.Values = [.. valuesList];
            }
            return node;
        }
    }
}
