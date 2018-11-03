using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Xunit;
using Xunit.Abstractions;

namespace Dexih.Utils.DataType.Tests
{
    public class PerformanceTests
    {
        private readonly ITestOutputHelper _output;

        public PerformanceTests(ITestOutputHelper output)
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
        
        [Theory]
        [InlineData(10000000)]
        public void CompareAddPerformance(long iterations)
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

            object e = 123;
            object f = 234;

            Timer("Add using object math", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    var c = Operations.Add(DataType.ETypeCode.Int32, e, f);
                }
            });
        }
        
        [Theory]
        [InlineData(10000000)]
        public void CompareGreaterThanPerformance(long iterations)
        {
            var a = 123;
            var b = 234;
            
            Timer("Add integers", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    var c = a > b;
                }
            });
            
            
            
            Timer("Add using operations", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    var c = Operations.GreaterThan(a, b);
                }
            });

            
            
            Timer("Add using func", () =>
            {
                var p1 = Expression.Parameter(typeof(int), "p1");
                var p2 = Expression.Parameter(typeof(int), "p2");
                var add = Expression.Lambda<Func<int, int, bool>>(Expression.GreaterThan(p1, p2), p1, p2).Compile();
                for(var i = 0; i< iterations; i++)
                {
                    var c = add(a, b);
                }
            });
            
            Timer("Add using generic math", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    var c = Generic.Math.GenericMath.GreaterThan(a, b);
                }
            });

            object e = 123;
            object f = 234;

            Timer("Add using object math", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    var c = Operations.GreaterThan(DataType.ETypeCode.Int32, e, f);
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
                var parse = Operations<decimal>.Parse.Value;
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

        [Theory]
        [InlineData(10000000)]
        public void ParsePerformanceDifferentDataTypes(long iterations)
        {
            Timer("Parse Int -> Int", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    Operations.Parse(DataType.ETypeCode.Int32, (object) 123);
                }
            });

            Timer("Parse String -> Int", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    Operations.Parse(DataType.ETypeCode.Int32, (object) "123");
                }
            });

            Timer("Parse Long -> Int", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    Operations.Parse(DataType.ETypeCode.Int32, (object) 123L);
                }
            });

            var type = typeof(int);

            Timer("Parse Int -> Int (type)", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    Operations.Parse(type, (object) 123);
                }
            });

            Timer("Parse String -> Int (type)", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    Operations.Parse(type, (object) "123");
                }
            });

            Timer("Parse Long -> Int (type)", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    Operations.Parse(type, (object) 123L);
                }
            });
        }
        
        [Theory]
        [InlineData(10000000)]
        public void GreaterThan_Performance(long iterations)
        {
            var a = 2;
            var b = 3;
            var blong = 3L;

            Timer("Greater than base (fastest)", () =>
            {
                for (var i = 0; i < iterations; i++)
                {
                    var c = a > b;
                }
            });

            Timer("Greater than using known type", () =>
            {
                for (var i = 0; i < iterations; i++)
                {
                    var c = Operations.GreaterThan(a,b);
                }
            });
            
            Timer("Greater than base same type, but unknown", () =>
            {
                for (var i = 0; i < iterations; i++)
                {
                    var c = Operations.GreaterThan((object)a,(object)b);
                }
            });

            Operations.GreaterThan((short)1, 1);
            

            
            Timer("Greater than base different type", () =>
            {
                for (var i = 0; i < iterations; i++)
                {
                    var c = Operations.GreaterThan((object)a, (object)blong);
                }
            });

        }
        
        [Theory]
        [InlineData(10000000)]
        public void Compare_Performance(long iterations)
        {
            var a = 2;
            var b = 3;

            Timer("Compare integers baseline", () =>
            {
                for (var i = 0; i < iterations; i++)
                {
                    var c = a.CompareTo(b);
                }
            });

            Timer("Compare - func", () =>
            {
                var comp = Operations<int>.Compare.Value;
                for (var i = 0; i < iterations; i++)
                {
                    var c = comp(a, b);
                }
            });

            Timer("Compare - 1", () =>
            {
                for (var i = 0; i < iterations; i++)
                {
                    var c = Operations.Compare(a, b);
                }
            });
            
            Timer("Compare - object", () =>
            {
                for (var i = 0; i < iterations; i++)
                {
                    var c = Operations.Compare((object)a, (object)b);
                }
            });
        }

        [Theory]
        [InlineData(10000000)]
        public void DataType_Performance(long iterations)
        {

        Timer("DataType.IsSimple (int)", () =>
            {
                var type = typeof(int);
                for(var i = 0; i< iterations; i++)
                {
                    var value = DataType.IsSimple(type);
                }
            });
            
            Timer("DataType.IsSimple (int?)", () =>
            {
                var type = typeof(int?);
                for(var i = 0; i< iterations; i++)
                {
                    var value = DataType.IsSimple(type);
                }
            });

            Timer("DataType.IsSimple (int[])", () =>
            {
                var type = typeof(int[]);
                for(var i = 0; i< iterations; i++)
                {
                    var value = DataType.IsSimple(type);
                }
            });

            Timer("DataType.IsSimple (string)", () =>
            {
                var type = typeof(int);
                for(var i = 0; i< iterations; i++)
                {
                    var value = DataType.IsSimple(type);
                }
            });

            Timer("DataType.GetTypeCode(string)", () =>
            {
                var type = typeof(string);
                for(var i = 0; i< iterations; i++)
                {
                    var value = DataType.GetTypeCode(type, out _);
                }
            });
            

//            Timer("DataType.Compare Integers Old", () =>
//            {
//                for(var i = 0; i< iterations; i++)
//                {
//                    var value = DataType.Compare(null ,(object)1, 2);
//                }
//            });
//            
//            Timer("DataType.Compare Nulls Old", () =>
//            {
//                for(var i = 0; i< iterations; i++)
//                {
//                    var value = DataType.Compare(null ,(object)null, null);
//                }
//            });
//            
//            Timer("DataType.Compare DbNulls Old", () =>
//            {
//                for(var i = 0; i< iterations; i++)
//                {
//                    var value = DataType.Compare(null ,(object)DBNull.Value, null);
//                }
//            });
//            
//            Timer("DataType.Compare Integer/Decimal Old", () =>
//            {
//                for(var i = 0; i< iterations; i++)
//                {
//                    var value =DataType.Compare(null ,(object)2, 2d);
//                }
//            });
//            
//            Timer("DataType.Compare Integer/String Old", () =>
//            {
//                for(var i = 0; i< iterations; i++)
//                {
//                    var value = DataType.Compare(null ,(object)2, "2");
//                }
//            });
//            
//            Timer("DataType.Compare String/Intege Oldr", () =>
//            {
//                for(var i = 0; i< iterations; i++)
//                {
//                    var value =DataType.Compare(null ,(object)2, "2");
//                }
//            });
//            
//            
//            Timer("Compare dec-dec  Old", () =>
//            {
//                for(var i = 0; i< iterations; i++)
//                {
//                    var value = DataType.Compare(null ,(object)1.1, 2.2);
//                }
//            });
            
            Timer("DataType.Compare Integers", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    var value = Operations.Compare(1, 2);
                }
            });
            
            Timer("DataType.Compare Nulls", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    var value = Operations.Compare((object)null, null);
                }
            });
            
            Timer("DataType.Compare DbNulls", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    var value = Operations.Compare((object)DBNull.Value, null);
                }
            });
            
            Timer("DataType.Compare Integer/Decimal", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    var value = Operations.Compare((object)2, 2d);
                }
            });
            
            Timer("DataType.Compare Integer/String", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    var value = Operations.Compare((object)2, "2");
                }
            });
            
            Timer("DataType.Compare String/Integer", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    var value = Operations.Compare((object)2, "2");
                }
            });
            
            
            Timer("Compare dec-dec", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    var value = Operations.Compare((object)1.1, 2.2);
                }
            });
        }
    }
}