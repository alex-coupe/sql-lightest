using SqlLightest.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest
{
    public class Constants
    {
        public static readonly List<string> DataTypes = ["INT", "VARCHAR", "BIGINT","BOOL","DATETIME","CHAR", "FLOAT"];
        public static readonly List<string> Constraints = ["PRIMARY", "FOREIGN", "NULLABLE", "UNIQUE", "DEFAULT"];
        public static readonly List<char> SpecialCharacters = ['(', ')', '\'', '/', '+', '-', ';','!','@',','];  
            
    }
}
