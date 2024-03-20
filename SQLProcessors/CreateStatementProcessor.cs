using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SQLProcessors
{
    public class CreateStatementProcessor : ISQLStatementProcessor
    {
        public SQLResult Process(Tree<string> syntaxTree)
        {
            var result = new SQLResult();
            if (syntaxTree.Root.Children[0].Value.Equals("DATABASE", StringComparison.CurrentCultureIgnoreCase))
            {
                var filename = $"{syntaxTree.Root.Children[0].Children[0].Value}.db";
                if (!File.Exists(filename))
                {
                    File.Create(filename);
                    if (File.Exists(filename))
                        result.Message = "Database Created Successfully";
                    else
                        result.Message = "Database Was Not Created";
                }
                else
                {
                    result.Message = "Database Already Exists";
                    return result;
                }
            }
            else
            {
                //Create Table
            }
            return result;
        }
    }
}
