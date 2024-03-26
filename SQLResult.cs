using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest
{
    public class SQLResult
    {
        public string Message { get; set; } = default!;

        public List<string> Columns { get; set; } = [];
        public List<List<string>> ResultSet { get; set; } = [];
    }
}
