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
            var sb = new StringBuilder();
            
            if (results.Columns.Count > 0)
            {
                foreach (var item in results.Columns)
                {
                    sb.Append(item + " | ");
                }
                Console.WriteLine(sb.ToString());
                sb.Clear();
                if (results.ResultSet.Count > 0)
                {
                    foreach (var row in results.ResultSet)
                    {
                        foreach (var item in row)
                        {
                            sb.Append(item + " | ");
                        }
                        Console.WriteLine(sb.ToString());
                        sb.Clear();
                    }

                }
                else
                {
                    Console.WriteLine("No Results");
                }
            }
           
            Console.WriteLine(results.Message);
        }
    }
}
