using BenchmarkDotNet.Attributes;
using Dexih.Utils.DataType;

namespace Benchmarks
{
    [RPlotExporter, RankColumn]
    public class Equals
    {
        [Params(1000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            value1 = Operations.Parse(ETypeCode.Int32, "123");
            value2 = Operations.Parse(ETypeCode.Int32, "123");
            dv1 = new DataValue(123);
            dv2 = new DataValue(123);
            do1 = new DataObject(123);
            do2 = new DataObject(123);
        }

        private object value1;
        private object value2;
        private DataValue dv1;
        private DataValue dv2;
        private DataObject do1;
        private DataObject do2;

        [Benchmark]
        public bool ObjectEquals() => Equals(value1, value2);

        [Benchmark]
        public bool UnboxEquals() => (int) value1 == (int) value2;

        [Benchmark]
        public bool OperationsEquals() => Operations.Equal(ETypeCode.Int32, value1, value2);
        
        [Benchmark]
        public bool DataObjectEquals() => do1 == do2;

        [Benchmark]
        public bool DataObjectEquals2() => do1.Int32 == do2.Int32;

        
    }
}