using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest.SQLProcessors
{
    public interface ISQLStatementProcessor
    {
        public SQLResult Process(Tree<string> syntaxTree);
    }
}
