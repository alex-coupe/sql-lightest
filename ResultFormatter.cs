using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest
{
    public class ResultFormatter
    {
        public static void Print(SQLResult results)
        {
            Console.WriteLine(results.Message);
        }
    }
}
