using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
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
        
//        [Fact]
//        void Operations_Test()
//        {
//            var intOperations = Operations<int>.Default;
//            var result = intOperations.Add(2, 5);
//            
//            Assert.Equal(7, result);
//        }
//
//        [Fact]
//        void CharArray_Test()
//        {
//            var charOperations = Operations<char[]>.Default;
//
//            var a = "123".ToCharArray();
//            var b = "1234".ToCharArray();
//            
//            Assert.True(charOperations.Equal(a, "123".ToCharArray()));
//            Assert.True((charOperations.GreaterThan(a,b)));
//            Assert.True((charOperations.LessThan(b,a)));
//        }
        
        
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
                for(var i = 0; i< iterations; i++)
                {
                    var c = Operations.Add(a, b);
                }
            });

            
            
            Timer("Add using func", () =>
            {
                var p1 = Expression.Parameter(typeof(int), "p1");
                var p2 = Expression.Parameter(typeof(int), "p2");
                var add = Expression.Lambda<Func<int, int, int>>(Expression.Add(p1, p2), p1, p2).Compile();
                for(var i = 0; i< iterations; i++)
                {
                    var c = add(a, b);
                }
            });
            
            Timer("Add using generic math", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    var c = Generic.Math.GenericMath.Add(a, b);
                }
            });

         

        }
        
        [Theory]
        [InlineData(10000000)]
        public void CompareParsePerformance(long iterations)
        {
            var values = new object[] {1.23d, 123, "1.23", 123L};

            Timer("Parse Raw", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    values.Select(Convert.ToDecimal).ToArray();
                }
            });


//            Timer("Parse Old", () =>
//            {
//                for(var i = 0; i< iterations; i++)
//                {
//                    values.Select(c=> DataType.TryParse(DataType.ETypeCode.Decimal, c)).ToArray();
//                }
//            });
            
            Timer("Parse Func", () =>
            {
                var parse = Operations<decimal>.Parse;
                for(var i = 0; i< iterations; i++)
                {
                    values.Select(parse).ToArray();
                }
            });

            Timer("Parse Object", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    values.Select(c=> Operations.Parse(DataType.ETypeCode.Decimal, c)).ToArray();
                }
            });

        }

        [Fact]
        public void ParseTest()
        {
            var c = Operations.Parse(DataType.ETypeCode.Decimal, "1.23");
            
        }

        
    }
}