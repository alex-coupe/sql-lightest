using SqlLightest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SyntaxNodes
{
    public class CreateNode(string name)
    {
        public string Name { get; set; } = name;

        public List<Column> Columns {get; set;} = [];
    }
}
