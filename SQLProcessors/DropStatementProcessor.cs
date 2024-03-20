using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SQLProcessors
{
    public class DropStatementProcessor : ISQLStatementProcessor
    {
        public SQLResult Process(Tree<string> syntaxTree)
        {
            var result = new SQLResult();
            if (syntaxTree.Root.Children[0].Value.Equals("DATABASE", StringComparison.CurrentCultureIgnoreCase))
            {
                var filename = $"{syntaxTree.Root.Children[0].Children[0].Value}.db";
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                    if (!File.Exists(filename))
                        result.Message = "Database Dropped Successfully";
                    else
                        result.Message = "Database Was Not Dropped";

                }
                else
                    result.Message = "Database Does Not Exist";
            }
            return result;
        }
    }
}
