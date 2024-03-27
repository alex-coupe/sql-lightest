using SqlLightest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SyntaxTreeBuilders
{
    public class SupportSyntaxTreeBuilder
    {
        public static List<Condition> BuildConditionNodes(string[] tokens)
        {
            var nodes = new List<Condition>();
            var index = 1;
            while (tokens[index]!= ";" && 
                !tokens[index].Equals("ORDER",StringComparison.CurrentCultureIgnoreCase) && 
                !tokens[index].Equals("GROUP", StringComparison.CurrentCultureIgnoreCase))
            {
                var node = new Condition();
                switch (tokens[index+1])
                {
                    case "!":
                        node.LHS = tokens[index];
                        node.Operator = tokens[index + 1] + tokens[index + 2];
                        node.RHS = tokens[index + 3];
                        nodes.Add(node);
                        index += 4;
                        break;
                    case "=":
                        node.LHS = tokens[index];
                        node.Operator = tokens[index + 1];
                        node.RHS = tokens[index + 2];
                        nodes.Add(node);
                        index += 3;
                        break;
                }
               
            }

            return nodes;
        }
    }
}
