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
                    if (tokens[0].Equals("EXIT", StringComparison.CurrentCultureIgnoreCase))
                    {
                        programRunning = false;
                        break;
                    }

                    SQLResult res = new();
                    switch (tokens[0].ToUpper())
                    {
                        case "SELECT":
                            res = SqlEngine.ExecuteSelectQuery(tokens);
                            break;
                        case "INSERT":
                            res = SqlEngine.ExecuteInsertQuery(tokens);
                            break;
                        case "UPDATE":
                            res = SqlEngine.ExecuteUpdateQuery(tokens);
                            break;
                        case "DELETE":
                            res = SqlEngine.ExecuteDeleteQuery(tokens);
                            break;
                        case "DROP":
                            res = SqlEngine.ExecuteDropQuery(tokens);
                            break;
                        case "CREATE":
                            res = SqlEngine.ExecuteCreateQuery(tokens);
                            break;
                        case "USE":
                            res = SqlEngine.ExecuteUseQuery(tokens);
                            break;
                        default:
                            Console.WriteLine("Unknown Command");
                            break;
                    }
                    ResultFormatter.Print(res);
                }
            }
        }
    }
}
