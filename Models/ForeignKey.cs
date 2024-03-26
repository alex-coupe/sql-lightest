using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.Models
{
    public class ForeignKey
    {
        public string Table { get; set; } = default!;
        public string Column { get; set; } = default!;
    }
}
