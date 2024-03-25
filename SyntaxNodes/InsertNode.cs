﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SyntaxNodes
{
    public class InsertNode
    {
        public string Table { get; set; } = default!;

        public string[]? Columns { get; set; }
        public string[] Values { get; set; } = default!;
    }
}
