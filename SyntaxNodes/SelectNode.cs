﻿using SqlLightest.Models;
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
        public string Table { get; set; } = default!;
        public List<Condition> Conditions { get; set; } = [];
    }
}
