using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SyntaxNodes
{
    public class DropDatabaseNode(string name)
    {
        public string Name { get; set; } = name;
    }
}
