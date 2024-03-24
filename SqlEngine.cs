using SqlLightest.SQLProcessors;
using SqlLightest.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLightest
{
    public class SqlEngine
    {
        public static SQLResult ExecuteCreateDatabaseQuery(CreateDatabaseNode node)
        {
            return CreateStatementProcessor.ProcessCreateDatabase(node);
        }

        public static SQLResult ExecuteDropDatabaseQuery(DropDatabaseNode node)
        {
            return DropStatementProcessor.ProcessDropDatabase(node);
        }

        public static SQLResult ExecuteCreateTableQuery(CreateTableNode node, string database) 
        { 
            return CreateStatementProcessor.ProcessCreateTable(node, database);
        }

        public static SQLResult ExecuteDropTableQuery(DropTableNode node,string database)
        {
            return DropStatementProcessor.ProcessDropTable(node,database);
        }


    }
}
