using SqlLightest.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SqlLightest.SQLProcessors
{
    public class SelectStatementProcessor
    {
        public static SQLResult Process(SelectNode selectNode, string database)
        {
            SQLResult result = new SQLResult();
            try
            {
                var path = $"{database}.db";
                var lines = File.ReadAllLines(path).ToList();

                var tableEntryIndex = lines.IndexOf($"[Table {selectNode.FromTable.ToUpper()}]");
                if (tableEntryIndex == -1)
                {
                    result.Message = "Table Does Not Exist";
                    return result;
                }
                var table = Utilities.LoadTableDef(lines, selectNode.FromTable.ToUpper());
                var tableData = lines.Where(x => x.StartsWith($"[Table Data {selectNode.FromTable.ToUpper()}"));
                bool selectAllCols = selectNode.Columns.Count == 1 && selectNode.Columns.First() == "*";
                result.Columns = selectAllCols
                    ? table.Columns.Select(x => x.Name).ToList() 
                    : selectNode.Columns;
                var columnIndicies = new List<int>();
                if (!selectAllCols)
                {
                    foreach( var col in selectNode.Columns )
                    {
                        var index = table.Columns.FindIndex(x => x.Name.Equals(col,StringComparison.CurrentCultureIgnoreCase));
                        if(index != -1)
                        {
                            columnIndicies.Add(index);
                        }
                    }
                }
                foreach( var row in tableData )
                {
                    var startIndex = row.IndexOf('(');
                    var endIndex = row.IndexOf(')');
                    var payload = row[++startIndex..endIndex];
                    var payloadSplit = payload.Split(',');
                    var res = new List<string>();
                    for (int i = 0; i< payloadSplit.Length;i++ )
                    {
                        if (columnIndicies.Contains(i) || selectAllCols)
                            res.Add(payloadSplit[i].Trim());
                    }
                    result.ResultSet.Add(res);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
