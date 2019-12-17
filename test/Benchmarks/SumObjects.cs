using System.Linq;
using BenchmarkDotNet.Attributes;
using Dexih.Utils.DataType;

namespace Benchmarks
{
    [RPlotExporter, RankColumn]
    public class SumObjects
    {
        private int[] ints;
        private object[] objects;
        private DataValue[] dataValues;
        
        [GlobalSetup]
        public void Setup()
        {
            ints = Enumerable.Range(0, 1000).ToArray();
            objects = ints.Select(c => (object) c).ToArray();
            dataValues = objects.Select(c => new DataValue((int)c)).ToArray();
        }

        [Benchmark]
        public void SumIntArray()
        {
            var sum = ints.Sum(c => c);
        }
        
        [Benchmark]
        public void SumObjectArray()
        {
            var sum = objects.Sum(c => (int) c);
        }
        
        [Benchmark]
        public void SumDataValueArray()
        {
            var sum = dataValues.Sum(c => c.Int32);
        }
    }
}