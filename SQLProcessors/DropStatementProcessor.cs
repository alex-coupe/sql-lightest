using SqlLightest.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SQLProcessors
{
    public class DropStatementProcessor
    {
        public static SQLResult ProcessDropDatabase(DropDatabaseNode node)
        {
            var filename = $"{node.Name}.db";
            var result = new SQLResult();
            if (File.Exists(filename))
            {
                try
                {
                    File.Delete(filename);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                if (!File.Exists(filename))
                    result.Message = "Database Dropped Successfully";
                else
                    result.Message = "Database Was Not Dropped";

            }
            else
                result.Message = "Database Does Not Exist";

            return result;
        }
       
    }
}
