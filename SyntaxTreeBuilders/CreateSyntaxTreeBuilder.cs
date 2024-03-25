using SqlLightest.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SyntaxTreeBuilders
{
    public class CreateSyntaxTreeBuilder
    {
        public static CreateNode? BuildCreateNode(string[] tokens)
        {
            if (tokens[1].ToUpper() == "DATABASE")
                return BuildCreateDatabaseNode(tokens);

            return null;
        }

        private static CreateNode BuildCreateTableNode(string[] tokens)
        {
            var node = new CreateNode("");
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

            while (start < end)
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
                            start += 2;
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
                                start += 2;
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
                                start += 2;
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
        private static CreateNode? BuildCreateDatabaseNode(string[] tokens)
        {
            var validator = SyntaxValidator.ValidateCreateCommand(tokens);
            if (validator.IsValid)
                return new CreateNode(tokens[2]);

            Console.WriteLine(validator.Message);

            return null;
        }

        private static bool CheckValidDataType(string dataType)
        {
            if (Constants.DataTypes.Contains(dataType)) return true;
            return false;
        }

        private static bool CheckValidConstraints(string constraint)
        {
            if (Constants.Constraints.Contains(constraint)) return true;
            return false;
        }
    }
}
