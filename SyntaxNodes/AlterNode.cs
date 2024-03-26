using SqlLightest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SyntaxNodes
{
    public class AlterNode
    {
        public string Table { get; set; } = default!;

        public Column? AddColumn { get; set; }
        public string? DropColumn { get; set; }
        public KeyValuePair<string, string>? RenameColumn { get; set; }

    }
}
