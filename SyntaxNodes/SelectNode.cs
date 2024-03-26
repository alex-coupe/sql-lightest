using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SyntaxNodes
{
    public class SelectNode
    {
        public List<string> Columns = [];
        public string FromTable { get; set; } = default!;
    }
}
