using SqlLightest.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.Models
{
    public class Column(string name, string dataType)
    {
        public string Name { get; set; } = name;
        public string DataType { get; set; } = dataType;
        public string DataTypeSize { get; set; } = default!;
        public bool IsNullable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public ForeignKey? ForeignKey { get; set; }
        public bool IsUnique { get; set; }
        public string? DefaultValue { get; set; }
    }
}
