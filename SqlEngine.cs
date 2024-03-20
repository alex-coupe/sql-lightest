using SqlLightest.SQLProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest
{
    public class SqlEngine
    {
        public static SQLResult Execute(Tree<string> syntaxTree, string selectedDB)
        {

            var sqlResult = new SQLResult
            {
                Message = "Not Implemented"
            };
            if (string.IsNullOrEmpty(selectedDB) && !syntaxTree.Root.Children[0].Value.Equals("DATABASE", StringComparison.CurrentCultureIgnoreCase))
            {
                sqlResult.Message = "No Database Selected";
                return sqlResult;
            }

            return syntaxTree.Root.Value switch
            {
                "CREATE" => new CreateStatementProcessor().Process(syntaxTree),
                "DROP" => new DropStatementProcessor().Process(syntaxTree),
                _ => sqlResult,
            };
        }
    }
}
