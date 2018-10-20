using System;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace Dexih.Utils.DataType.Tests
{
    public class OperationsTests
    {
        private readonly ITestOutputHelper _output;

        public OperationsTests(ITestOutputHelper output)
        {
            this._output = output;
        }
        
        public void Timer(string name, Action action)
        {
            var start = Stopwatch.StartNew();
            action.Invoke();
            var time = start.ElapsedMilliseconds;
            _output.WriteLine($"Test \"{name}\" completed in {time}ms");
        }
        
        [Fact]
        void Operations_Test()
        {
            var intOperations = Operations<int>.Default;
            var result = intOperations.Add(2, 5);
            
            Assert.Equal(7, result);
        }

        [Fact]
        void CharArray_Test()
        {
            var charOperations = Operations<char[]>.Default;

            var a = "123".ToCharArray();
            var b = "1234".ToCharArray();
            
            Assert.True(charOperations.Equal(a, "123".ToCharArray()));
            Assert.True((charOperations.GreaterThan(a,b)));
            Assert.True((charOperations.LessThan(b,a)));
        }
        
        
        [Theory]
        [InlineData(10000000)]
        public void ComparePerformance(long iterations)
        {
            var a = 123;
            var b = 234;
            
            Timer("Add integers", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    var c = a + b;
                }
            });
            
            
            
            Timer("Add using operations", () =>
            {
                var intOps =  Operations<int>.Default;
                for(var i = 0; i< iterations; i++)
                {
                    var c = intOps.Add(a,b);
                }
            });

                
            Timer("Add using datatype", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    var c = DataType.Add(DataType.ETypeCode.Int32, a, b);
                }
            });
        }
    }
}