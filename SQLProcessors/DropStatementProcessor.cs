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

        public static SQLResult ProcessDropTable(DropTableNode node, string database)
        {
            var filename = $"{database}.db";
            var result = new SQLResult();

            if (File.Exists(filename))
            {

                try
                {
                    var lines = File.ReadAllLines(filename).ToList();
                    var indexToDelete = lines.IndexOf($"[Table {node.Name}]");
                    if (indexToDelete == -1)
                    {
                        result.Message = "Table does not exist";
                        return result;
                    }
                    lines.RemoveAt(indexToDelete);
                    while (!lines.ElementAt(indexToDelete).StartsWith("[Table") && lines.ElementAt(indexToDelete) != "")
                    {
                        lines.RemoveAt(indexToDelete);
                    }

                    File.WriteAllLines(filename, lines);
                    result.Message = "Table Dropped Successfully";
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return result;
        }
    }
}
