using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest
{
    public class SqlEngine
    {
       public static int Execute(string[] tokens)
        {
            return tokens[0].ToUpper() switch
            {
                "EXIT" => 0,
                _ => 1,
            };
        }
    }
}
