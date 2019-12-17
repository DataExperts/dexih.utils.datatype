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
            value2 = 123;
            da1 = new DataObject((string)value1);
            dv1 = new DataValue(123);
        }

        private object value1;
        private object value2;
        private DataObject da1;
        private DataObject da2;
        private DataValue dv1;
        private DataValue dv2;

        [Benchmark]
        public void VariableAssignString() => value2 = (string) value1;

        [Benchmark]
        public void DataObjectAssignString() => da2 = new DataObject((string) value1);
        
        [Benchmark]
        public void DataValueAssignExplicit() => dv1 = new DataValue(123);

        [Benchmark]
        public void DataValueAssignObject() => dv1 = new DataValue(value2, ETypeCode.String);

        [Benchmark]
        public void DataObjectCopy() => da2 = new DataObject(da1);

        [Benchmark]
        public void DataValueCopy() => dv2 = dv1;



    }
}