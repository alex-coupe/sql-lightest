using SqlLightest.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SQLProcessors
{
    public class CreateStatementProcessor
    {
        private static ValidationResult ValidateCreateDatabaseQuery(string filename)
        {
            var res = new ValidationResult();
            if (!File.Exists(filename))
                res.IsValid = true;
            else
                res.Message = "Database Already Exists";
                
            return res;
        }

        public static SQLResult ProcessCreateCommand(CreateNode node, string database)
        {
            if (string.IsNullOrEmpty(database))
                return ProcessCreateDatabase(node);

            else
                return ProcessCreateTable(node, database);
        }

        private static SQLResult ProcessCreateDatabase(CreateNode node)
        {
            var result = new SQLResult();
            var filename = $"{node.Name.ToLower()}.db";
            var validateQuery = ValidateCreateDatabaseQuery(filename);
            if (validateQuery.IsValid)
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
                result.Message = validateQuery.Message; 
            }
            return result;
        }

        private static SQLResult ProcessCreateTable(CreateNode node, string database)
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
                foreach (var col in node.Columns)
                {
                    var sb = new StringBuilder();
                    sb.Append($"{col.Name}|{col.DataType}");
                    if (!string.IsNullOrEmpty(col.DataTypeSize))
                        sb.Append($"({col.DataTypeSize})");
                    if (col.IsPrimaryKey) sb.Append("|PrimaryKey");
                    if (col.ForeignKey != null) sb.Append($"|ForeignKey({col.ForeignKey.Table},{col.ForeignKey.Column})");
                    if (col.IsUnique) sb.Append("|Unique");
                    if (col.IsNullable) sb.Append("|Nullable");
                    if (!string.IsNullOrEmpty(col.DefaultValue)) sb.Append($"|Default({col.DefaultValue})");
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
