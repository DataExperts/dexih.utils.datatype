using System;
using BenchmarkDotNet.Attributes;
using Dexih.Utils.DataType;

namespace Benchmarks
{
    [RPlotExporter, RankColumn]
    public class AddInt
    {
        [Params(1000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            var rnd = new Random(42);
            int1 = rnd.Next();
            int2 = rnd.Next();
            value1 = int1;
            value2 = int2;
            da1 = new DataObject() {Int32 = int1};
            da2 = new DataObject() {Int32 = int2};

            // cache 
            Operations.Add(ETypeCode.Int32, 1, 1);
        }

        private int int1;
        private int int2;
        private object value1;
        private object value2;
        private DataObject da1;
        private DataObject da2;

        [Benchmark]
        public int PureAdd() => int1 + int2;

        [Benchmark]
        public int UnboxAdd() => (int) value1 + (int) value2;

        [Benchmark]
        public int GenericMathAdd() => Generic.Math.GenericMath.Add((int)value1, (int)value2);

        [Benchmark]
        public int OperationsAdd1() => (int) Operations.Add<int>((int)value1, (int)value2);

        [Benchmark]
        public int OperationsAdd2() => (int) Operations.Add(ETypeCode.Int32, value1, value2);

        [Benchmark]
        public int OperationsAdd3() => (int) Operations.Add(value1, value2);

        [Benchmark]
        public DataObject DataObjectAdd() => da1 + da2;

    }
}