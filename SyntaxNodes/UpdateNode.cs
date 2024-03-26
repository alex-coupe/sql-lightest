using SqlLightest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SyntaxNodes
{
    public class UpdateNode
    {
        public string Table { get; set; } = default!;
        public Dictionary<string, string> ColumnValues { get; set; } = [];
        public List<Condition> Conditions { get; set; } = [];
    }
}
