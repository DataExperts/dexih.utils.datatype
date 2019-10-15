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
            value1 = 123;
            value2 = 456;
            da1 = new DataObject() {Int32 = 123};
            da2 = new DataObject() {Int32 = 123};
        }

        private object value1;
        private object value2;
        private DataObject da1;
        private DataObject da2;

        [Benchmark]
        public bool ObjectEquals() => object.Equals(value1, value2);

        [Benchmark]
        public bool UnboxEquals() => (int) value1 == (int) value2;

        [Benchmark]
        public bool OperationsEquals() => Operations.Equal(ETypeCode.Int32, value1, value2);
        
        [Benchmark]
        public bool DataObjectEquals() => da1 == da2;

        [Benchmark]
        public bool DataObjectEquals2() => da1.Int32 == da2.Int32;

        
    }
}