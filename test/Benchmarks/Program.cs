using System;
using BenchmarkDotNet.Running;
using Dexih.Utils.DataType;

namespace Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            // BenchmarkRunner.Run<Equals>();
            BenchmarkRunner.Run<AddInt>();
            // BenchmarkRunner.Run<AddDecimal>();
            // BenchmarkRunner.Run<Assignment>();
            // BenchmarkRunner.Run<SumObjects>();
        }
    }
}