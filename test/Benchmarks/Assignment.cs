using System;
using BenchmarkDotNet.Attributes;
using Dexih.Utils.DataType;

namespace Benchmarks
{
    public class Assignment
    {
        [Params(1000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            value1 = new String('A', 1000);
            da1 = new DataObject() {String = (string) value1};
        }

        private object value1;
        private object value2;
        private DataObject da1;
        private DataObject da2;

        [Benchmark]
        public void ObjectAssign() => value2 = (string) value1;

        [Benchmark]
        public void DACreate() => da2 = new DataObject();
        
        [Benchmark]
        public void ObjectToDA() => da2 = new DataObject(value1, ETypeCode.String);

        [Benchmark]
        public void DAToDaRef() => da2 = da1;

        [Benchmark]
        public void DAToDaCopy() => da2 = new DataObject(da1);


    }
}