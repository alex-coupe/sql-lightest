using SqlLightest.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SQLProcessors
{
    public class UpdateStatementProcessor
    {
        public static SQLResult ProcessUpdateCommand(UpdateNode node, string selectedDB)
        {
            var result = new SQLResult();
            try
            {
                var path = $"{selectedDB}.db";
                var lines = File.ReadAllLines(path).ToList();

                var tableEntryIndex = lines.IndexOf($"[Table {node.Table.ToUpper()}]");
                if (tableEntryIndex == -1)
                {
                    result.Message = "Table Does Not Exist";
                    return result;
                }
                var table = Utilities.LoadTableDef(lines, node.Table.ToUpper());
                var tableData = lines.Where(x => x.StartsWith($"[Table Data {node.Table.ToUpper()}"));

                foreach (var row in tableData)
                {
                    var startIndex = row.IndexOf('(');
                    var endIndex = row.IndexOf(')');
                    var payload = row[++startIndex..endIndex];
                    var payloadSplit = payload.Split(',');
                    var res = new List<string>();
                    if (Utilities.SatisfiesConditions(node.Conditions, payloadSplit, table) || node.Conditions.Count == 0)
                    {
                        foreach(var update in node.ColumnValues)
                        {
                            var index = table.Columns.FindIndex(x => x.Name.Equals(update.Key, StringComparison.CurrentCultureIgnoreCase));
                            if (index != -1)
                            {
                                payloadSplit[index] = update.Value;
                            }
                        }
                    }
                }
                File.WriteAllLines(path, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
   
            return result;
        }
    }
}
