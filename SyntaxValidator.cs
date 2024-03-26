using SqlLightest.SQLProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest
{
    public class SyntaxValidator
    {
        public static ValidationResult ValidateCreateCommand(string[] tokens)
        {

            return tokens[1] switch
            {
                "database" or "DATABASE" => ValidateBuildCreateDatabaseTokens(tokens),
                "table" or "TABLE" => ValidateBuildCreateTableTokens(tokens),
                _ => new ValidationResult() { Message = "Error Near Create" },
            };
        }

        public static ValidationResult ValidateDropCommand(string[] tokens)
        {
            return tokens[1] switch
            {
                "database" or "DATABASE" => ValidateBuildDropDatabaseTokens(tokens),
                "table" or "TABLE" => ValidateBuildDropTableTokens(tokens),
                _ => new ValidationResult() { Message = "Error Near Drop" },
            };
        }

        private static ValidationResult ValidateBuildDropTableTokens(string[] tokens)
        {
            ValidationResult result = new() { IsValid = true };
            return result;
        }

        private static ValidationResult ValidateBuildDropDatabaseTokens(string[] tokens)
        {
            ValidationResult result = new() { IsValid = true };
            return result;
        }

        public static ValidationResult ValidateUseCommand(string[] tokens)
        {
            ValidationResult result = new() { IsValid = true };
            if (tokens.Length < 3)
            {
                result.Message = "Invalid Syntax";
                result.IsValid = false;
            }
            else
            {
                foreach (var cha in tokens[1])
                {
                    if (Constants.SpecialCharacters.Contains(cha))
                    {
                        result.Message = "Invalid Character in Name";
                        result.IsValid = false;
                    }
                }

                if (tokens[2] != ";")
                {
                    result.Message = "Missing ;";
                    result.IsValid = false;
                }
            }
            return result;
        }

        private static ValidationResult ValidateBuildCreateDatabaseTokens(string[] tokens)
        {
            ValidationResult result = new() { IsValid = true };

            if (string.IsNullOrEmpty(tokens[2]))
            {
                result.Message = "Invalid Database Name";
                result.IsValid = false;
            }

            foreach(var cha in tokens[2])
            {
                if (Constants.SpecialCharacters.Contains(cha))
                {
                    result.Message = "Invalid Character in Name";
                    result.IsValid = false;
                }
            }

            if (tokens[3] != ";")
            {
                result.Message = "Missing ;";
                result.IsValid = false;
            }

            return result;
        }

        private static ValidationResult ValidateBuildCreateTableTokens(string[] tokens)
        {
            ValidationResult result = new() { IsValid = true };

            return result;
        }

        public static ValidationResult ValidateInsertCommand(string[] tokens)
        {
            ValidationResult result = new() { IsValid = true };

            return result;
        }

        public static ValidationResult ValidateSelectCommand(string[] tokens)
        {
            ValidationResult result = new() { IsValid = true };

            return result;
        }
    }
}
