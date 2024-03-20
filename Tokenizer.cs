using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest
{
    public class Tokenizer
    {
        public static string[] Tokenize(string? input)
        {
            if (string.IsNullOrEmpty(input)) return [];
            return input.Split(' ');
        }
    }
}
