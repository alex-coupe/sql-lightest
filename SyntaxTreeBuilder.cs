using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest
{
    public class SyntaxTreeBuilder
    {
        public static Tree<string> Process(string[] tokens)
        {
            return tokens[0].ToUpper() switch
            {
                "SELECT" => ProcessSelectStatement(tokens),
                "USE" => ProcessUseStatement(tokens),
                "EXIT" => new Tree<string>("EXIT"),
                "CREATE" => ProcessCreateStatement(tokens),
                "DROP" => ProcessDropStatement(tokens),
                _ => new Tree<string>("UNKNOWN"),
            } ;
        }

        private static Tree<string> ProcessCreateStatement(string[] tokens) 
        {
            var syntaxTree = new Tree<string>(tokens[0].ToUpper());

            if (tokens.Length < 3) 
            {
                Console.WriteLine("Invalid Create Statement");
            }

            if (tokens.Length == 3) 
            {
                syntaxTree.Root.Children.Add(new TreeNode<string>(tokens[1]));
                syntaxTree.Root.Children[0].Children.Add(new TreeNode<string>(tokens[2]));
            }

            return syntaxTree;
        }

        private static Tree<string> ProcessDropStatement(string[] tokens)
        {
            var syntaxTree = new Tree<string>(tokens[0].ToUpper());

            if (tokens.Length < 3)
            {
                Console.WriteLine("Invalid Drop Statement");
            }

            if (tokens.Length == 3)
            {
                syntaxTree.Root.Children.Add(new TreeNode<string>(tokens[1]));
                syntaxTree.Root.Children[0].Children.Add(new TreeNode<string>(tokens[2]));
            }

            return syntaxTree;
        }

        private static Tree<string> ProcessUseStatement(string[] tokens)
        {
            var synxtaxTree = new Tree<string>(tokens[0].ToUpper());
            if (tokens.Length < 2)
            {
                Console.WriteLine("Use Command Requires Database Name");
            }
            else
            {
                synxtaxTree.Root.Children.Add(new TreeNode<string>(tokens[1]));
            }
            return synxtaxTree;
        }

        private static Tree<string> ProcessSelectStatement(string[] tokens)
        {
            var synxtaxTree = new Tree<string>(tokens[0].ToUpper());
            var columnsNode = new TreeNode<string>("COLUMNS");
            var fromNode = new TreeNode<string>("FROM");
            fromNode.Children.Add(new TreeNode<string>(tokens[3]));
            foreach (var col in tokens[1].Split(','))
            {
                columnsNode.Children.Add(new TreeNode<string>(col.Trim()));
            }
            synxtaxTree.Root.Children.Add(columnsNode);
            synxtaxTree.Root.Children.Add(fromNode);
            return synxtaxTree;
        }
    }
}
