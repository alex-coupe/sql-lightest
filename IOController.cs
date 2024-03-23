using SqlLightest.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest
{
    public class IOController
    {
        private bool programRunning = true;
        private string selectedDB = "";

        private void SetSelectedDB(UseDatabaseNode node)
        {
            var db = node.Name;
            if (!string.IsNullOrEmpty(db) && File.Exists($"{db}.db"))
            {
                this.selectedDB = db;
                Console.WriteLine($"Database set to {db}");
            }
            else
                Console.WriteLine("Database does not exist");
        }

        public void HandleInputOutput()
        {
            Console.WriteLine("Welcome to SQLightest");
            while (programRunning)
            {
                Console.Write("> ");
                var commandString = Console.ReadLine();
                var tokens = Tokenizer.Tokenize(commandString);
                if (tokens.Length > 0) 
                { 
                    if (tokens[0] == "EXIT")
                    {
                        programRunning = false;
                        break;
                    }
                    switch (tokens[0])
                    {
                        case "SELECT":
                            
                            break;
                        case "INSERT":
                           
                            break;
                        case "UPDATE":
                            break;
                        case "DELETE":
                            break;
                        case "DROP":
                            if (tokens[1] == "DATABASE")
                            {
                                var node = SyntaxTreeBuilder.BuildDropDatabaseNode(tokens);
                                if (!string.IsNullOrEmpty(node.Name))
                                {
                                    var res = SqlEngine.ExecuteDropDatabaseQuery(node);
                                    ResultFormatter.Print(res);
                                }
                            }
                            else if (tokens[1] == "TABLE")
                            {

                            }
                            else
                                Console.WriteLine("Unknown Drop Command");
                            break;
                        case "CREATE":
                            if (tokens[1] == "DATABASE")
                            {
                                var createDBNode = SyntaxTreeBuilder.BuildCreateDatabaseNode(tokens);
                                if (!string.IsNullOrEmpty(createDBNode.Name))
                                {
                                    var res = SqlEngine.ExecuteCreateDatabaseQuery(createDBNode);
                                    ResultFormatter.Print(res);
                                }
                            }
                            else if (!string.IsNullOrEmpty(selectedDB) && tokens[1] == "TABLE")
                            {
                                var createTableNode = SyntaxTreeBuilder.BuildCreateTableNode(tokens);
                                if (!string.IsNullOrEmpty(createTableNode.Name))
                                {
                                    var res = SqlEngine.ExecuteCreateTableQuery(createTableNode,selectedDB);
                                    ResultFormatter.Print(res);
                                }
                            }
                            else
                                Console.WriteLine("Unknown Create Command");
                            break;
                        case "USE":
                            SetSelectedDB(new UseDatabaseNode(tokens[1]));
                            break;
                        default:
                            Console.WriteLine("Unknown Command");
                            break;
                            
                    }
                }
            }
        }
    }
}
