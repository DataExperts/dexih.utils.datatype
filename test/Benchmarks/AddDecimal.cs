using System;
using BenchmarkDotNet.Attributes;
using Dexih.Utils.DataType;

namespace Benchmarks
{
    [RPlotExporter, RankColumn]
    public class AddDecimal
    {
        [Params(1000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            var rnd = new Random(42);
            val1 = NextDecimal(rnd);
            val2 = NextDecimal(rnd);
            value1 = val1;
            value2 = val2;
            da1 = new DataObject() {Decimal = val1};
            da2 = new DataObject() {Decimal = val2};

            // cache 
            Operations.Add(ETypeCode.Int32, 1, 1);
        }

        private Decimal val1;
        private Decimal val2;
        private object value1;
        private object value2;
        private DataObject da1;
        private DataObject da2;

        [Benchmark]
        public Decimal PureAdd() => val1 + val2;

        [Benchmark]
        public decimal UnboxAdd() => (decimal) value1 + (decimal) value2;

        [Benchmark]
        public decimal GenericMathAdd() => Generic.Math.GenericMath.Add((decimal)value1, (decimal)value2);

        [Benchmark]
        public decimal OperationsAdd1() => Operations.Add((decimal)value1, (decimal)value2);

        [Benchmark]
        public decimal OperationsAdd2() => (decimal) Operations.Add(ETypeCode.Decimal, value1, value2);

        [Benchmark]
        public decimal OperationsAdd3() => (decimal) Operations.Add(value1, value2);

        [Benchmark]
        public DataObject DataObjectAdd() => da1 + da2;

        public static decimal NextDecimal(Random rng)
        {
            byte scale = (byte) rng.Next(29);
            bool sign = rng.Next(2) == 1;
            return new decimal(rng.Next(), rng.Next(),
                rng.Next(),
                sign,
                scale);
        }

    }
    
}