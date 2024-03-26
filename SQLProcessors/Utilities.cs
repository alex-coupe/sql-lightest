using SqlLightest.Models;
using SqlLightest.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SQLProcessors
{
    public class Utilities
    {
        public static CreateNode LoadTableDef(List<string> lines,  string tableName)
        {
            var node = new CreateNode(tableName);
            var index = lines.IndexOf($"[Table {tableName.ToUpper()}]");
            ++index;
            while(lines.ElementAt(index) != "")
            {
                var col = new Column("", "");
                var column = lines.ElementAt(index).Split('|');
                for(int i = 0; i < column.Length; i++)
                {
                    if (i == 0)
                        col.Name = column[i];

                    if (i == 1)
                    {
                        if (column[i].Contains('(') && column[i].Contains(')'))
                        {
                            var openingParen = column[i].IndexOf('(');
                            var closingParen = column[i].IndexOf(')');
                            col.DataType = column[i][..openingParen];
                            col.DataTypeSize = column[i].Substring(++openingParen,closingParen - openingParen);
                        }
                        else
                            col.DataType = column[i];
                    }

                    if (i > 1)
                    {
                        switch(column[i])
                        {
                            case "PrimaryKey":
                                col.IsPrimaryKey = true;
                                col.IsUnique = true;
                                break;
                            case "ForeignKey":

                                var openingParen = column[i].IndexOf('(');
                                var closingParen = column[i].IndexOf(')');
                                var values = column[i][openingParen..closingParen].Split(',');
                                col.ForeignKey = new ForeignKey
                                {
                                    Table = values[0],
                                    Column = values[1]
                                };
                                break;
                            case "Nullable":
                                col.IsNullable = true;
                                break;
                            case "Unique":
                                col.IsUnique = true;
                                break;
                            case "Default":
                                var oP = column[i].IndexOf('(');
                                var cP = column[i].IndexOf(')');
                                col.DefaultValue = column[i][oP..cP];
                                break;
                        }
                        
                    }
                }
                node.Columns.Add(col);
                ++index; 
            }
            return node;
        }
    }
}
