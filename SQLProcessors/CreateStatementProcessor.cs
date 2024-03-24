using SqlLightest.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SQLProcessors
{
    public class CreateStatementProcessor
    {
        public static SQLResult ProcessCreateDatabase(CreateDatabaseNode node)
        {
            var result = new SQLResult();
            var filename = $"{node.Name.ToLower()}.db";
            if (!File.Exists(filename))
            {
                using (File.Create(filename))
                if (File.Exists(filename))
                {
                    result.Message = "Database Created Successfully";
                }
                    
                else
                    result.Message = "Database Was Not Created";
            }
            else
            {
                result.Message = "Database Already Exists"; 
            }
            return result;
        }

        public static SQLResult ProcessCreateTable(CreateTableNode node, string database)
        {
            var result = new SQLResult();

            try
            {
                var path = $"{database}.db";
                var lines = File.ReadAllLines(path).ToList();
                var index = 0;
                for (int i = 0;i< lines.Count; i++)
                {
                    if (lines[i] == $"[Table {node.Name}]")
                    {
                        result.Message = "Table already exists";
                        return result;
                    }

                    if (lines[i] == "")
                    {
                        index = i; 
                        break;
                    }
                }

                lines.Insert(index++, $"[Table {node.Name}]");
                lines.Insert(index++, $"Cols: {node.Columns.Count}");
                foreach (var col in node.Columns)
                {
                    var sb = new StringBuilder();
                    sb.Append($"{col.Name} | {col.DataType}");
                    if (!string.IsNullOrEmpty(col.DataTypeSize))
                        sb.Append($"-{col.DataTypeSize}");
                    if (col.IsPrimaryKey) sb.Append("| PrimaryKey: True");
                    if (col.IsForeignKey) sb.Append("| ForeignKey: True");
                    if (col.IsUnique) sb.Append("| Unique: True");
                    if (col.IsNullable) sb.Append("| Nullable: True");
                    if (!string.IsNullOrEmpty(col.DefaultValue)) sb.Append($"| Default: {col.DefaultValue}");
                    lines.Insert(index++,sb.ToString());
                    lines.Insert(index, Environment.NewLine);
                }
                File.WriteAllLines(path, lines);
                result.Message = "Table Created Successfully";
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }


    }
}
