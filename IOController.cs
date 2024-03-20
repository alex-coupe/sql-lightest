using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest
{
    public class IOController
    {
        private readonly bool dbLoaded = false;
        private bool programRunning = true;

        public void HandleInputOutput()
        {
            Console.WriteLine("Welcome to SQLightest");
            while (programRunning)
            {
                Console.Write("> ");
                var commandString = Console.ReadLine();
                var tokens = Tokenize(commandString);
                var res = SqlEngine.Execute(tokens);
                HandleResults(res);
            }
        }

        private void HandleResults(int res)
        {
            if (res == 0)
            {
                programRunning = false;
            }
            else
            {
                Console.WriteLine("Unknown Command");
            }
        }

        private string[] Tokenize(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return [];
            return input.Trim().Split(';');
        }
    }
}
