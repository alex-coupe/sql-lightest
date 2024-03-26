using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.Models
{
    public class Condition
    {
        public string RHS { get; set; } = default!; 
        public string LHS { get; set; } = default!;
        public string Operator { get; set; } = default!;    
    }
}
