using SqlLightest.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SQLProcessors
{
    public class DeleteStatementProcessor
    {
        public static SQLResult ProcessDeleteCommand(DeleteNode node, string selectedDB)
        {
            SQLResult result = new();
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
                List<int> indicesToRemove = [];
                foreach (var item in lines)
                {
                    if (item.StartsWith($"[Table Data {node.Table.ToUpper()}"))
                        indicesToRemove.Add(lines.IndexOf(item));   
                }
                for (int i = indicesToRemove.Count - 1; i >= 0; i--)
                {
                        lines.RemoveAt(indicesToRemove.ElementAt(i));
                }
                File.WriteAllLines(path, lines);
                result.Message = $"{indicesToRemove.Count} Rows(s) Removed";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
