using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Linq2DbPerformanceRegression;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.SQLite;
using LinqToDB.Linq;
using Microsoft.Data.Sqlite;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Linq2DbMemoryRegression
{
    [MemoryDiagnoser]
    [SimpleJob(RunStrategy.Throughput, warmupCount: 1, targetCount: 20)]
    public class InsertPerformanceBenchmark
    {
        readonly SqliteConnection connection = CreateConnection();

        public int VariableValue = 0;

        readonly Func<DataConnection, string, string, string, decimal, decimal, int> compiledQuery 
            = CompiledQuery.Compile<DataConnection, string, string, string,  decimal, decimal, int>((ctx, key, value1, value2, value3, value4) =>
                 ctx.GetTable<TESTTABLE>()
                 .Value(i => i.COLUMN1, key)
                 .Value(i => i.COLUMN2, value1)
                 .Value(i => i.COLUMN12, value2)
                 .Value(i => i.COLUMN15, value3)
                 .Value(i => i.COLUMN16, value4)
                 .Insert());

        public static SqliteConnection CreateConnection()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var cmdText = @"CREATE TABLE TESTTABLE (
                COLUMN1 CHAR(36) DEFAULT ' ' NOT NULL,
                COLUMN2 CHAR(10) DEFAULT ' ' NOT NULL,
                COLUMN3 CHAR(10) DEFAULT ' ' NOT NULL,
                COLUMN4 CHAR(10) DEFAULT ' ' NOT NULL,
                COLUMN5 CHAR(10) DEFAULT ' ' NOT NULL,
                COLUMN6 CHAR(10) DEFAULT ' ' NOT NULL,
                COLUMN7 CHAR(10) DEFAULT ' ' NOT NULL,
                COLUMN8 CHAR(10) DEFAULT ' ' NOT NULL,
                COLUMN9 CHAR(10) DEFAULT ' ' NOT NULL,
                COLUMN10 CHAR(10) DEFAULT ' ' NOT NULL,
                COLUMN11 CHAR(10) DEFAULT ' ' NOT NULL,
                COLUMN12 CHAR(1) DEFAULT ' ' NOT NULL,
                COLUMN15 DECIMAL(14,0) DEFAULT 0 NOT NULL,
                COLUMN16 DECIMAL(14,0) DEFAULT 0 NOT NULL
                )";
            var cmd = new SqliteCommand(cmdText, connection);
            cmd.ExecuteNonQuery();
            return connection;
        }

        [Benchmark]
        public async Task Small_InsertStatement_With_Variable_Parameters()
        {
            using (var dc = new DataConnection(new SQLiteDataProvider("Microsoft.Data.Sqlite"), connection))
            {
                var query = await dc.GetTable<TESTTABLE>()
                .Value(i => i.COLUMN1, "61a018e7-6e43-44b7-ad53-5a55e626fbbe")
                .Value(i => i.COLUMN2, "VALUE")
                .Value(i => i.COLUMN12, "N")
                .Value(i => i.COLUMN15, ++VariableValue)
                .Value(i => i.COLUMN16, ++VariableValue)
                .InsertAsync();
            }
        }

        [Benchmark]
        public async Task Small_InsertStatement_With_Static_Parameters()
        {
            using (var dc = new DataConnection(new SQLiteDataProvider("Microsoft.Data.Sqlite"), connection))
            {
                var query = await dc.GetTable<TESTTABLE>()
                .Value(i => i.COLUMN1, "61a018e7-6e43-44b7-ad53-5a55e626fbbe")
                .Value(i => i.COLUMN2, "VALUE")
                .Value(i => i.COLUMN12, "N")
                .Value(i => i.COLUMN15, 654645)
                .Value(i => i.COLUMN16, 4547667897689)
                .InsertAsync();
            }
        }

        [Benchmark]
        public async Task Large_InsertStatement_With_Variable_Parameters()
        {
            using (var dc = new DataConnection(new SQLiteDataProvider("Microsoft.Data.Sqlite"), connection))
            {
                var query = await dc.GetTable<TESTTABLE>()
                .Value(i => i.COLUMN1, "61a018e7-6e43-44b7-ad53-5a55e626fbbe")
                .Value(i => i.COLUMN2, "VALUE")
                .Value(i => i.COLUMN3, "VALUE2")
                .Value(i => i.COLUMN4, "VALUE3")
                .Value(i => i.COLUMN5, "VALUE4")
                .Value(i => i.COLUMN6, "VALUE5")
                .Value(i => i.COLUMN7, "")
                .Value(i => i.COLUMN8, "")
                .Value(i => i.COLUMN9, "VALUE6")
                .Value(i => i.COLUMN10, "VALUE7")
                .Value(i => i.COLUMN11, "Microsoft Windows 10 Enterprise")
                .Value(i => i.COLUMN12, "N")
                .Value(i => i.COLUMN15, ++VariableValue)
                .Value(i => i.COLUMN16, ++VariableValue)
                .InsertAsync();
            }
        }

        [Benchmark]
        public async Task Large_InsertStatement_With_Static_Parameters()
        {
            using (var dc = new DataConnection(new SQLiteDataProvider("Microsoft.Data.Sqlite"), connection))
            {
                var query = await dc.GetTable<TESTTABLE>()
                .Value(i => i.COLUMN1, "61a018e7-6e43-44b7-ad53-5a55e626fbbe")
                .Value(i => i.COLUMN2, "VALUE")
                .Value(i => i.COLUMN3, "VALUE2")
                .Value(i => i.COLUMN4, "VALUE3")
                .Value(i => i.COLUMN5, "VALUE4")
                .Value(i => i.COLUMN6, "VALUE5")
                .Value(i => i.COLUMN7, "")
                .Value(i => i.COLUMN8, "")
                .Value(i => i.COLUMN9, "VALUE6")
                .Value(i => i.COLUMN10, "VALUE7")
                .Value(i => i.COLUMN11, "Microsoft Windows 10 Enterprise")
                .Value(i => i.COLUMN12, "N")
                .Value(i => i.COLUMN15, 3)
                .Value(i => i.COLUMN16, 4)
                .InsertAsync();
            }
        }

        [Benchmark]
        public async Task Large_InsertStatement_With_Variable_Parameters_With_ClearCaches()
        {
            using (var dc = new DataConnection(new SQLiteDataProvider("Microsoft.Data.Sqlite"), connection))
            {
                var query = await dc.GetTable<TESTTABLE>()
                .Value(i => i.COLUMN1, "61a018e7-6e43-44b7-ad53-5a55e626fbbe")
                .Value(i => i.COLUMN2, "VALUE")
                .Value(i => i.COLUMN3, "VALUE2")
                .Value(i => i.COLUMN4, "VALUE3")
                .Value(i => i.COLUMN5, "VALUE4")
                .Value(i => i.COLUMN6, "VALUE5")
                .Value(i => i.COLUMN7, "")
                .Value(i => i.COLUMN8, "")
                .Value(i => i.COLUMN9, "VALUE6")
                .Value(i => i.COLUMN10, "VALUE7")
                .Value(i => i.COLUMN11, "Microsoft Windows 10 Enterprise")
                .Value(i => i.COLUMN12, "N")
                .Value(i => i.COLUMN15, ++VariableValue)
                .Value(i => i.COLUMN16, ++VariableValue)
                .InsertAsync();

                Query.ClearCaches();
            }
        }

        [Benchmark]
        public void Large_Compiled_InsertStatement_With_Variable_Parameters()
        {
            using (var dc = new DataConnection(new SQLiteDataProvider("Microsoft.Data.Sqlite"), connection))
            {
                compiledQuery(dc, "61a018e7-6e43-44b7-ad53-5a55e626fbbe", "VALUE", "N", 3479583475, 3845793);
            }
        }

        [Benchmark]
        public async Task Large__InsertStatement_With_Variable_Parameters_Using_Expression_Overload()
        {
            using (var dc = new DataConnection(new SQLiteDataProvider("Microsoft.Data.Sqlite"), connection))
            {
                var query = await dc.GetTable<TESTTABLE>()
                .Value(i => i.COLUMN1, () => "61a018e7-6e43-44b7-ad53-5a55e626fbbe")
                .Value(i => i.COLUMN2, () => "VALUE")
                .Value(i => i.COLUMN3, () => "VALUE2")
                .Value(i => i.COLUMN4, () => "VALUE3")
                .Value(i => i.COLUMN5, () => "VALUE4")
                .Value(i => i.COLUMN6, () => "VALUE5")
                .Value(i => i.COLUMN7, () => "")
                .Value(i => i.COLUMN8, () => "")
                .Value(i => i.COLUMN9, () => "VALUE6")
                .Value(i => i.COLUMN10, () => "VALUE7")
                .Value(i => i.COLUMN11, () => "Microsoft Windows 10 Enterprise")
                .Value(i => i.COLUMN12, () => "N")
                .Value(i => i.COLUMN15, () => VariableValue + 1)
                .Value(i => i.COLUMN16, () => VariableValue + 1)
                .InsertAsync();

                VariableValue += 2;
            }
        }
    }
}
