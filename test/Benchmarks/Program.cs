using System;
using BenchmarkDotNet.Running;

namespace Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // BenchmarkRunner.Run<Equals>();
            // BenchmarkRunner.Run<AddInt>();
            // BenchmarkRunner.Run<AddDecimal>();
            BenchmarkRunner.Run<Assignment>();
        }
    }
}