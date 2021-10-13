using BenchmarkDotNet.Running;
using System;
using System.Threading.Tasks;

namespace Linq2DbMemoryRegression
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<UpdatePerformanceBenchmark>();
            var summary = BenchmarkRunner.Run<InsertPerformanceBenchmark>();
            //var mem = new MemoryUsage();
            //for (int i = 0; i < 200; i++)
            //{
            //    mem.Large_Compiled_UpdateStatement_With_Variable_Paramters();
            //    Console.WriteLine(i);
            //}
            Console.ReadLine();
        }
    }
}
