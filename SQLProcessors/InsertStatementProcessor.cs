using SqlLightest.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SQLProcessors
{
    public class InsertStatementProcessor
    {
        public static SQLResult ProcessInsertIntoTable(InsertNode node, string database)
        {
            var result = new SQLResult();

            try
            {
                var path = $"{database}.db";
                var lines = File.ReadAllLines(path).ToList();
                var tableEntryIndex = lines.IndexOf($"[Table {node.Table.ToUpper()}]");
                if (tableEntryIndex == -1 )
                {
                    result.Message = "Table Does Not Exist";
                    return result;
                }
                var colCount = int.Parse(lines.ElementAt(++tableEntryIndex)[6..]);
                if (colCount != node.Values.Length ) 
                {
                    result.Message = "Values to Columns Mismatch";
                    return result;
                }
                //TODO: Validate data types
                //TODO: Validate constraints
                var sb = new StringBuilder();
                sb.Append($"[Table Data {node.Table.ToUpper()} (");
                for( int i = 0; i < node.Values.Length;i++ )
                {
                    sb.Append($"{node.Values[i]}");
                    if (i < node.Values.Length - 1)
                        sb.Append(',');
                }
                sb.Append(")]");
                lines.Add(sb.ToString());

                File.WriteAllLines(path, lines);
                result.Message = "1 Line Written";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

                return result;
        }
    }
}
