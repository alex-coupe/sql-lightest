using SqlLightest.SQLProcessors;
using SqlLightest.SyntaxNodes;
using SqlLightest.SyntaxTreeBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest
{
    public class SqlEngine
    {
        private static string selectedDB = "";
        public static SQLResult ExecuteCreateQuery(string[] tokens)
        {
            var res = new SQLResult();
            var validator = SyntaxValidator.ValidateCreateCommand(tokens);
            if (validator.IsValid)
            {
                var node = CreateSyntaxTreeBuilder.BuildCreateNode(tokens);
                if (node != null)
                {
                    res = CreateStatementProcessor.ProcessCreateCommand(node,selectedDB);
                }
            }
            else
            {
                res.Message = validator.Message;
            }

            return res;
        }

        public static SQLResult ExecuteDropQuery(string[] tokens)
        {
            var res = new SQLResult();
            var validator = SyntaxValidator.ValidateDropCommand(tokens);
            if (validator.IsValid)
            {
                var node = DropSyntaxTreeBuilder.BuildDropNode(tokens);
                if (node != null)
                {
                    res = DropStatementProcessor.ProcessDropCommand(node, selectedDB);
                }
            }
            else
            {
                res.Message = validator.Message;
            }

            return res;
        }
        public static SQLResult ExecuteInsertQuery(string[] tokens)
        {
            var res = new SQLResult();
            var validator = SyntaxValidator.ValidateInsertCommand(tokens);
            if (validator.IsValid)
            {
                var node = InsertSyntaxTreeBuilder.BuildInsertNode(tokens);
                if (node != null)
                {
                    res = InsertStatementProcessor.ProcessInsertIntoTable(node, selectedDB);
                }
            }
            else
            {
                res.Message = validator.Message;
            }

            return res;
        }

        public static SQLResult ExecuteUseQuery(string[] tokens)
        {
            var res = new SQLResult();
            var validator = SyntaxValidator.ValidateUseCommand(tokens);
            if (validator.IsValid)
            {
                var node = UseSyntaxTreeBuilder.BuildUseNode(tokens);
                if (node != null)
                {
                    if (!string.IsNullOrEmpty(node.Name) && File.Exists($"{node.Name}.db"))
                    {
                        selectedDB = node.Name;
                        res.Message = $"Database set to {node.Name}";
                    }
                    else
                        res.Message = "Database does not exist";
                }
            }
            else
            {
                res.Message = validator.Message;
            }

            return res;
        }

        internal static SQLResult ExecuteSelectQuery(string[] tokens)
        {
            var res = new SQLResult();
            var validator = SyntaxValidator.ValidateSelectCommand(tokens);
            if (validator.IsValid)
            {
                var node = SelectSyntaxTreeBuilder.BuildSelectNode(tokens);
                if (node != null)
                {
                    if (node != null)
                    {
                        res = SelectStatementProcessor.Process(node, selectedDB);
                    }
                }
            }
            else
            {
                res.Message = validator.Message;
            }
            return res;
        }
    }
}
