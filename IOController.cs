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

                    SQLResult res;
                    switch (tokens[0].ToUpper())
                    {
                        case "SELECT":
                            res = SqlEngine.ExecuteSelectQuery(tokens);
                            ResultFormatter.Print(res);
                            break;
                        case "INSERT":
                            res = SqlEngine.ExecuteInsertQuery(tokens);
                            ResultFormatter.Print(res);
                            break;
                        case "UPDATE":
                            break;
                        case "DELETE":
                            break;
                        case "ALTER":
                            break;
                        case "DROP":
                            res = SqlEngine.ExecuteDropQuery(tokens);
                            ResultFormatter.Print(res);
                            break;
                        case "CREATE":
                            res = SqlEngine.ExecuteCreateQuery(tokens);
                            ResultFormatter.Print(res);
                            break;
                        case "USE":
                            res = SqlEngine.ExecuteUseQuery(tokens);
                            ResultFormatter.Print(res);
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
