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

        public void HandleInputOutput()
        {
            Console.WriteLine("Welcome to SQLightest");
            while (programRunning)
            {
                Console.Write("> ");
                var commandString = Console.ReadLine();
                var tokens = Tokenizer.Tokenize(commandString);
                if (tokens.Length > 0) { 
                var syntaxTree = SyntaxTreeBuilder.Process(tokens);
                    switch (syntaxTree.Root.Value.ToUpper())
                    {
                        case "EXIT":
                            programRunning = false;
                            break;
                        case "UNKNOWN":
                            Console.WriteLine("Unknown Command");
                            break;
                        case "USE":
                            var db = syntaxTree.Root.Children[0].Value;
                            if (File.Exists($"{db}.db"))
                            {
                                selectedDB = db;
                                Console.WriteLine($"Database set to {db}");
                            }
                            else
                                Console.WriteLine("Database does not exist");
                            break;
                        default:
                            var result = SqlEngine.Execute(syntaxTree, selectedDB);
                            ResultFormatter.Print(result);
                            break;
                    }
                }
            }
        }
    }
}
