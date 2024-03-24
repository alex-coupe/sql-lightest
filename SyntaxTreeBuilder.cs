using SqlLightest.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest
{
    public class SyntaxTreeBuilder
    {
        private static readonly string[] DataTypes = ["INT", "VARCHAR", "BIGINT","BOOL","DATETIME","CHAR", "FLOAT"];
        private static readonly string[] Constraints = ["PRIMARY", "FOREIGN", "NULLABLE", "UNIQUE", "DEFAULT"];
        
        public static CreateDatabaseNode BuildCreateDatabaseNode(string[] tokens) 
        {
            if (tokens.Length < 3) 
            {
                Console.WriteLine("Name is required");
                return new CreateDatabaseNode("");
            }
            if (tokens.Length > 3) 
            {
                Console.WriteLine("Invalid syntax");
                return new CreateDatabaseNode("");
            }
            return new CreateDatabaseNode(tokens[2]);
        }

        public static DropDatabaseNode BuildDropDatabaseNode(string[] tokens)
        {
            if (tokens.Length < 3)
            {
                Console.WriteLine("Name is required");
                return new DropDatabaseNode("");
            }
            return new DropDatabaseNode(tokens[2]);
        }

        public static DropTableNode BuildDropTableNode(string[] tokens)
        {
            if (tokens.Length < 3)
            {
                Console.WriteLine("Name is required");
                return new DropTableNode("");
            }
            return new DropTableNode(tokens[2]);
        }

        public static CreateTableNode BuildCreateTableNode(string[] tokens)
        {
            var node = new CreateTableNode("");
            if (tokens.Length < 7)
            {
                Console.WriteLine("Invalid Create Table Statement");
                return node;
            }

            node.Name = tokens[2];

            var tokensList = tokens.ToList();

            int start = tokensList.IndexOf("(");

            if (start == -1)
            {
                Console.WriteLine("Missing (");
                return node;
            }
            start++;
            int end = tokensList.IndexOf(";");
            if (end == -1)
            {
                Console.WriteLine("Missing ;");
                return node;
            }
            end--;
            
            while(start < end)
            {
                var col = new Column(tokensList.ElementAt(start++), tokensList.ElementAt(start++));
                if (!CheckValidDataType(col.DataType))
                {
                    Console.WriteLine($"Syntax Error near {tokensList.ElementAt(start--)}");
                    return node;
                }

                if (col.DataType == "VARCHAR")
                {
                    start++;
                    col.DataTypeSize = tokensList.ElementAt(start++);
                    start++;
                }
                
                if (tokensList.ElementAt(start) == ",")
                {
                    start++;
                }
                else
                {
                    while (tokensList.ElementAt(start) != "," && tokensList.ElementAt(start) != ")")
                    {
                        if (!CheckValidConstraints(tokensList.ElementAt(start)))
                        {
                            Console.WriteLine($"Syntax Error near {tokensList.ElementAt(start)}");
                            return node;
                        }
                        if (tokensList.ElementAt(start) == "UNIQUE")
                        {
                            col.IsUnique = true;
                            start++;
                        }

                        if (tokensList.ElementAt(start) == "NULLABLE")
                        {
                            col.IsNullable = true;
                            start++;
                        }

                        if (tokensList.ElementAt(start) == "DEFAULT")
                        {
                            start+=2;
                            col.DefaultValue = tokensList.ElementAt(start++);
                            start++;
                        }

                        if (tokensList.ElementAt(start) == "PRIMARY")
                        {
                            var peek = start;
                            peek++;
                            if (tokensList.ElementAt(peek) == "KEY")
                            {
                                if (col.IsNullable)
                                {
                                    Console.WriteLine("Cannot have nullable primary key");
                                    return node;
                                }
                                col.IsPrimaryKey = true;
                                start+=2;
                            }
                            else
                            {
                                Console.WriteLine("Syntax Error near PRIMARY");
                                return node;
                            }
                        }

                        if (tokensList.ElementAt(start) == "FOREIGN")
                        {
                            var peek = start;
                            peek++;
                            if (tokensList.ElementAt(peek) == "KEY")
                            {
                                if (col.IsNullable)
                                {
                                    Console.WriteLine("Cannot have nullable foreign key");
                                    return node;
                                }
                                col.IsForeignKey = true;
                                start+=2;
                            }
                            else
                            {
                                Console.WriteLine("Syntax Error near FOREIGN");
                                return node;
                            }
                        }
                    }
                    if (start < end)
                        start++;
                }
                node.Columns.Add(col);
            }         
            return node;
        }

        private static bool CheckValidDataType(string dataType)
        {
            if (DataTypes.Contains(dataType)) return true;
            return false;
        }

        private static bool CheckValidConstraints(string constraint)
        {
            if (Constraints.Contains(constraint)) return true;
            return false;
        }
    }
}
