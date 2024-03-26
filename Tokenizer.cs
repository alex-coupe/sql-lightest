using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest
{
    public class Tokenizer
    {
        private static bool ContainsSpecialCharacter(string token)
        {
            foreach (char c in Constants.SpecialCharacters)
            {
                if (token.Contains(c)) return true;
            }
            return false;
        }

        private static string[] ProcessSubTokens(string token)
        {
            var tokensList = new List<string>();
            StringBuilder sb = new();
            foreach (char c in token)
            {
                if (Constants.SpecialCharacters.Contains(c))
                {
                    if (sb.Length > 0)
                    {
                        tokensList.Add(sb.ToString());
                        sb.Clear();
                    }
                    tokensList.Add(c.ToString());
                }
                else
                {
                    sb.Append(c);
                }
            }
            if (sb.Length > 0)
            {
                tokensList.Add(sb.ToString());
            }
            return [.. tokensList];
        }

        private static string[] ProcessInitialTokens(string[] input)
        {
            var result = new List<string>();
            var tokenList = input.ToList();

            foreach (var token in tokenList) 
            {
                if (ContainsSpecialCharacter(token))
                {
                    result.AddRange(ProcessSubTokens(token));  
                }
                else
                {
                    result.Add(token);
                }
            }
           
            return [.. result];

        }
        public static string[] Tokenize(string? input)
        {
            if (string.IsNullOrEmpty(input)) return [];
            var initialSplit = input.Split(' ');
            return ProcessInitialTokens(initialSplit);
        }
    }
}
