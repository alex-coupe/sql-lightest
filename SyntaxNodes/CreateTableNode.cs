using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SyntaxNodes
{
    public class CreateTableNode(string name)
    {
        public string Name { get; set; } = name;

        public List<Column> Columns {get; set;} = [];
    }

    public class Column(string name, string dataType)
    {
        public string Name { get; set; } = name;
        public string DataType { get; set; } = dataType;
        public string DataTypeSize { get; set; } = default!;
        public bool IsNullable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }
        public bool IsUnique { get; set; }
        public string? DefaultValue { get; set; }

    }
}
