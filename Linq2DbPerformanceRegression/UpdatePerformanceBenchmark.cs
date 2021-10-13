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
    public class UpdatePerformanceBenchmark
    {
        readonly SqliteConnection connection = CreateConnection();

        public int VariableValue = 0;

        readonly Func<DataConnection, string, string, string, decimal, decimal, int> compiledQuery 
            = CompiledQuery.Compile<DataConnection, string, string, string,  decimal, decimal, int>((ctx, key, value1, value2, value3, value4) =>
                 ctx.GetTable<TESTTABLE>()
                 .Where(i => i.COLUMN1 == key)
                 .Set(i => i.COLUMN2, value1)
                 .Set(i => i.COLUMN12, value2)
                 .Set(i => i.COLUMN15, value3)
                 .Set(i => i.COLUMN16, value4)
                 .Update());

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
        public async Task Small_UpdateStatement_With_Variable_Parameters()
        {
            using (var dc = new DataConnection(new SQLiteDataProvider("Microsoft.Data.Sqlite"), connection))
            {
                var query = await dc.GetTable<TESTTABLE>()
                .Where(i => i.COLUMN1 == "61a018e7-6e43-44b7-ad53-5a55e626fbbe")
                .Set(i => i.COLUMN2, "VALUE")
                .Set(i => i.COLUMN12, "N")
                .Set(i => i.COLUMN15, ++VariableValue)
                .Set(i => i.COLUMN16, ++VariableValue)
                .UpdateAsync();
            }
        }

        [Benchmark]
        public async Task Small_UpdateStatement_With_Static_Parameters()
        {
            using (var dc = new DataConnection(new SQLiteDataProvider("Microsoft.Data.Sqlite"), connection))
            {
                var query = await dc.GetTable<TESTTABLE>()
                .Where(i => i.COLUMN1 == "61a018e7-6e43-44b7-ad53-5a55e626fbbe")
                .Set(i => i.COLUMN2, "VALUE")
                .Set(i => i.COLUMN12, "N")
                .Set(i => i.COLUMN15, 654645)
                .Set(i => i.COLUMN16, 4547667897689)
                .UpdateAsync();
            }
        }

        [Benchmark]
        public async Task Large_UpdateStatement_With_Variable_Parameters()
        {
            using (var dc = new DataConnection(new SQLiteDataProvider("Microsoft.Data.Sqlite"), connection))
            {
                var query = await dc.GetTable<TESTTABLE>()
                .Where(i => i.COLUMN1 == "61a018e7-6e43-44b7-ad53-5a55e626fbbe")
                .Set(i => i.COLUMN2, "VALUE")
                .Set(i => i.COLUMN3, "VALUE2")
                .Set(i => i.COLUMN4, "VALUE3")
                .Set(i => i.COLUMN5, "VALUE4")
                .Set(i => i.COLUMN6, "VALUE5")
                .Set(i => i.COLUMN7, "")
                .Set(i => i.COLUMN8, "")
                .Set(i => i.COLUMN9, "VALUE6")
                .Set(i => i.COLUMN10, "VALUE7")
                .Set(i => i.COLUMN11, "Microsoft Windows 10 Enterprise")
                .Set(i => i.COLUMN12, "N")
                .Set(i => i.COLUMN15, ++VariableValue)
                .Set(i => i.COLUMN16, ++VariableValue)
                .UpdateAsync();
            }
        }

        [Benchmark]
        public async Task Large_UpdateStatement_With_Static_Parameters()
        {
            using (var dc = new DataConnection(new SQLiteDataProvider("Microsoft.Data.Sqlite"), connection))
            {
                var query = await dc.GetTable<TESTTABLE>()
                .Where(i => i.COLUMN1 == "61a018e7-6e43-44b7-ad53-5a55e626fbbe")
                .Set(i => i.COLUMN2, "VALUE")
                .Set(i => i.COLUMN3, "VALUE2")
                .Set(i => i.COLUMN4, "VALUE3")
                .Set(i => i.COLUMN5, "VALUE4")
                .Set(i => i.COLUMN6, "VALUE5")
                .Set(i => i.COLUMN7, "")
                .Set(i => i.COLUMN8, "")
                .Set(i => i.COLUMN9, "VALUE6")
                .Set(i => i.COLUMN10, "VALUE7")
                .Set(i => i.COLUMN11, "Microsoft Windows 10 Enterprise")
                .Set(i => i.COLUMN12, "N")
                .Set(i => i.COLUMN15, 3)
                .Set(i => i.COLUMN16, 4)
                .UpdateAsync();
            }
        }

        [Benchmark]
        public async Task Large_UpdateStatement_With_Variable_Parameters_With_ClearCaches()
        {
            using (var dc = new DataConnection(new SQLiteDataProvider("Microsoft.Data.Sqlite"), connection))
            {
                var query = await dc.GetTable<TESTTABLE>()
                .Where(i => i.COLUMN1 == "61a018e7-6e43-44b7-ad53-5a55e626fbbe")
                .Set(i => i.COLUMN2, "VALUE")
                .Set(i => i.COLUMN3, "VALUE2")
                .Set(i => i.COLUMN4, "VALUE3")
                .Set(i => i.COLUMN5, "VALUE4")
                .Set(i => i.COLUMN6, "VALUE5")
                .Set(i => i.COLUMN7, "")
                .Set(i => i.COLUMN8, "")
                .Set(i => i.COLUMN9, "VALUE6")
                .Set(i => i.COLUMN10, "VALUE7")
                .Set(i => i.COLUMN11, "Microsoft Windows 10 Enterprise")
                .Set(i => i.COLUMN12, "N")
                .Set(i => i.COLUMN15, ++VariableValue)
                .Set(i => i.COLUMN16, ++VariableValue)
                .UpdateAsync();

                Query.ClearCaches();
            }
        }

        [Benchmark]
        public void Large_Compiled_UpdateStatement_With_Variable_Parameters()
        {
            using (var dc = new DataConnection(new SQLiteDataProvider("Microsoft.Data.Sqlite"), connection))
            {
                compiledQuery(dc, "61a018e7-6e43-44b7-ad53-5a55e626fbbe", "VALUE", "N", 3479583475, 3845793);
            }
        }

         [Benchmark]
        public async Task Large__UpdateStatement_With_Variable_Parameters_Using_Expression_Overload()
        {
             using (var dc = new DataConnection(new SQLiteDataProvider("Microsoft.Data.Sqlite"), connection))
            {
                var query = await dc.GetTable<TESTTABLE>()
                .Where(i => i.COLUMN1 == "61a018e7-6e43-44b7-ad53-5a55e626fbbe")
                .Set(i => i.COLUMN2, i => "VALUE")
                .Set(i => i.COLUMN3, i => "VALUE2")
                .Set(i => i.COLUMN4, i => "VALUE3")
                .Set(i => i.COLUMN5, i => "VALUE4")
                .Set(i => i.COLUMN6, i => "VALUE5")
                .Set(i => i.COLUMN7, i => "")
                .Set(i => i.COLUMN8, i => "")
                .Set(i => i.COLUMN9, i => "VALUE6")
                .Set(i => i.COLUMN10, i => "VALUE7")
                .Set(i => i.COLUMN11, i => "Microsoft Windows 10 Enterprise")
                .Set(i => i.COLUMN12, i => "N")
                .Set(i => i.COLUMN15, i => VariableValue + 1)
                .Set(i => i.COLUMN16, i => VariableValue + 1)
                .UpdateAsync();

                VariableValue += 2;
            }
        }
    }
}
