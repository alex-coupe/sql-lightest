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
            var filename = $"{node.Name}.db";
            if (!File.Exists(filename))
            {
                File.Create(filename);
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
                var lines = File.ReadAllLines(path);
                
                for (int i = 0;i< lines.Length; i++)
                {
                    if (lines[i] == $"[Table {node.Name}]")
                    {
                        result.Message = "Table already exists";
                        return result;
                    }
                }

                using (FileStream fs = new(path, FileMode.Append, FileAccess.Write))
                {
                    
                    using (StreamWriter writer = new(fs))
                    {
                        writer.WriteLine($"[Table {node.Name}]");
                        writer.WriteLine($"Col Count: {node.Columns.Count}");
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
                            writer.WriteLine(sb.ToString());
                        }
                                             
                    };
                };
                result.Message = "Table Created Successfully";
            }
            catch(FileLoadException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }


    }
}
