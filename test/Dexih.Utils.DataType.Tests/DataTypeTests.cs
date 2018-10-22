using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;
using static Dexih.Utils.DataType.DataType;

namespace Dexih.Utils.DataType.Tests
{
    public class DataTypeTests
    {
        private readonly ITestOutputHelper _output;

        public DataTypeTests(ITestOutputHelper output)
        {
            this._output = output;
        }
        
        [Theory]
        [InlineData(ETypeCode.Boolean, EBasicType.Boolean)]
        [InlineData(ETypeCode.Byte, EBasicType.Numeric)]
        [InlineData(ETypeCode.DateTime, EBasicType.Date)]
        [InlineData(ETypeCode.Decimal, EBasicType.Numeric)]
        [InlineData(ETypeCode.Double, EBasicType.Numeric)]
        [InlineData(ETypeCode.Guid, EBasicType.String)]
        [InlineData(ETypeCode.Int16, EBasicType.Numeric)]
        [InlineData(ETypeCode.Int32, EBasicType.Numeric)]
        [InlineData(ETypeCode.Int64, EBasicType.Numeric)]
        [InlineData(ETypeCode.SByte, EBasicType.Numeric)]
        [InlineData(ETypeCode.Single, EBasicType.Numeric)]
        [InlineData(ETypeCode.String, EBasicType.String)]
        [InlineData(ETypeCode.Json, EBasicType.String)]
        [InlineData(ETypeCode.Xml, EBasicType.String)]
        [InlineData(ETypeCode.Text, EBasicType.String)]
        [InlineData(ETypeCode.Time, EBasicType.Time)]
        [InlineData(ETypeCode.UInt16, EBasicType.Numeric)]
        [InlineData(ETypeCode.UInt32, EBasicType.Numeric)]
        [InlineData(ETypeCode.UInt64, EBasicType.Numeric)]
        [InlineData(ETypeCode.Unknown, EBasicType.Unknown)]
        public void DataType_GetBasicType(ETypeCode inputType, EBasicType expected)
        {
            Assert.Equal(expected, DataType.GetBasicType(inputType));
        }

        [Theory]
        [InlineData(typeof(Byte), ETypeCode.Byte)]
        [InlineData(typeof(SByte), ETypeCode.SByte)]
        [InlineData(typeof(UInt16), ETypeCode.UInt16)]
        [InlineData(typeof(UInt32), ETypeCode.UInt32)]
        [InlineData(typeof(UInt64), ETypeCode.UInt64)]
        [InlineData(typeof(Int16), ETypeCode.Int16)]
        [InlineData(typeof(Int32), ETypeCode.Int32)]
        [InlineData(typeof(Int64), ETypeCode.Int64)]
        [InlineData(typeof(Decimal), ETypeCode.Decimal)]
        [InlineData(typeof(Double), ETypeCode.Double)]
        [InlineData(typeof(Single), ETypeCode.Single)]
        [InlineData(typeof(String), ETypeCode.String)]
        [InlineData(typeof(Boolean), ETypeCode.Boolean)]
        [InlineData(typeof(DateTime), ETypeCode.DateTime)]
        [InlineData(typeof(TimeSpan), ETypeCode.Time)]
        [InlineData(typeof(Guid), ETypeCode.Guid)]
        [InlineData(typeof(byte[]), ETypeCode.Binary)]
        [InlineData(typeof(Int32?), ETypeCode.Int32)]
        [InlineData(typeof(char[]), ETypeCode.Char)]
        public void DataType_GetTypeCode(Type dataType, ETypeCode expectedTypeCode)
        {
            Assert.Equal(expectedTypeCode, DataType.GetTypeCode(dataType, out _));
        }
        
        [Theory]
        [InlineData(typeof(Byte), true)]
        [InlineData(typeof(SByte), true)]
        [InlineData(typeof(UInt16), true)]
        [InlineData(typeof(UInt32), true)]
        [InlineData(typeof(UInt64), true)]
        [InlineData(typeof(Int16), true)]
        [InlineData(typeof(Int32), true)]
        [InlineData(typeof(Int64), true)]
        [InlineData(typeof(Int64?), true)]
        [InlineData(typeof(Decimal), true)]
        [InlineData(typeof(Double), true)]
        [InlineData(typeof(Single), true)]
        [InlineData(typeof(String), true)]
        [InlineData(typeof(Boolean), true)]
        [InlineData(typeof(DateTime), true)]
        [InlineData(typeof(TimeSpan), true)]
        [InlineData(typeof(Guid), true)]
        [InlineData(typeof(int[]), false)]
        [InlineData(typeof(string[]), false)]
        [InlineData(typeof(Point), false)]
        [InlineData(typeof(Int32?), true)]
        [InlineData(typeof(char[]), true)]
        [InlineData(typeof(byte[]), true)]
        public void DataType_IsSimple(Type dataType, bool result)
        {
            Assert.Equal(result, DataType.IsSimple(dataType));
        }

        [Theory]
        [InlineData(typeof(Byte), ETypeCode.Byte)]
        [InlineData(typeof(SByte), ETypeCode.SByte)]
        [InlineData(typeof(UInt16), ETypeCode.UInt16)]
        [InlineData(typeof(UInt32), ETypeCode.UInt32)]
        [InlineData(typeof(UInt64), ETypeCode.UInt64)]
        [InlineData(typeof(Int16), ETypeCode.Int16)]
        [InlineData(typeof(Int32), ETypeCode.Int32)]
        [InlineData(typeof(Int64), ETypeCode.Int64)]
        [InlineData(typeof(Decimal), ETypeCode.Decimal)]
        [InlineData(typeof(Double), ETypeCode.Double)]
        [InlineData(typeof(Single), ETypeCode.Single)]
        [InlineData(typeof(String), ETypeCode.String)]
        [InlineData(typeof(Boolean), ETypeCode.Boolean)]
        [InlineData(typeof(DateTime), ETypeCode.DateTime)]
        [InlineData(typeof(TimeSpan), ETypeCode.Time)]
        [InlineData(typeof(Guid), ETypeCode.Guid)]
        public void DataType_GetType(Type expectedType, ETypeCode typeCode)
        {
            Assert.Equal(expectedType, DataType.GetType(typeCode));
        }

        [Theory]
        [InlineData(ETypeCode.Byte, 2, 1, ECompareResult.Greater)]
        [InlineData(ETypeCode.Byte, 1, 1, ECompareResult.Equal)]
        [InlineData(ETypeCode.Byte, 1, 2, ECompareResult.Less)]
        [InlineData(ETypeCode.SByte, 2, 1, ECompareResult.Greater)]
        [InlineData(ETypeCode.SByte, 1, 1, ECompareResult.Equal)]
        [InlineData(ETypeCode.SByte, 1, 2, ECompareResult.Less)]
        [InlineData(ETypeCode.UInt16, 2, 1, ECompareResult.Greater)]
        [InlineData(ETypeCode.UInt16, 1, 1, ECompareResult.Equal)]
        [InlineData(ETypeCode.UInt16, 1, 2, ECompareResult.Less)]
        [InlineData(ETypeCode.UInt32, 2, 1, ECompareResult.Greater)]
        [InlineData(ETypeCode.UInt32, 1, 1, ECompareResult.Equal)]
        [InlineData(ETypeCode.UInt32, 1, 2, ECompareResult.Less)]
        [InlineData(ETypeCode.UInt64, 2, 1, ECompareResult.Greater)]
        [InlineData(ETypeCode.UInt64, 1, 1, ECompareResult.Equal)]
        [InlineData(ETypeCode.UInt64, 1, 2, ECompareResult.Less)]
        [InlineData(ETypeCode.Int16, 2, 1, ECompareResult.Greater)]
        [InlineData(ETypeCode.Int16, 1, 1, ECompareResult.Equal)]
        [InlineData(ETypeCode.Int16, 1, 2, ECompareResult.Less)]
        [InlineData(ETypeCode.Int32, 2, 1, ECompareResult.Greater)]
        [InlineData(ETypeCode.Int32, 1, 1, ECompareResult.Equal)]
        [InlineData(ETypeCode.Int32, 1, 2, ECompareResult.Less)]
        [InlineData(ETypeCode.Int64, 2, 1, ECompareResult.Greater)]
        [InlineData(ETypeCode.Int64, 1, 1, ECompareResult.Equal)]
        [InlineData(ETypeCode.Int64, 1, 2, ECompareResult.Less)]
        [InlineData(ETypeCode.Decimal, 1.1, 1.09, ECompareResult.Greater)]
        [InlineData(ETypeCode.Decimal, 1.09, 1.09, ECompareResult.Equal)]
        [InlineData(ETypeCode.Decimal, 1.09, 1.1, ECompareResult.Less)]
        [InlineData(ETypeCode.Double, 1.1, 1.09, ECompareResult.Greater)]
        [InlineData(ETypeCode.Double, 1.09, 1.09, ECompareResult.Equal)]
        [InlineData(ETypeCode.Double, 1.09, 1.1, ECompareResult.Less)]
        [InlineData(ETypeCode.Single, 1.1, 1.09, ECompareResult.Greater)]
        [InlineData(ETypeCode.Single, 1.09, 1.09, ECompareResult.Equal)]
        [InlineData(ETypeCode.Single, 1.09, 1.1, ECompareResult.Less)]
        [InlineData(ETypeCode.String, "01", "001", ECompareResult.Greater)]
        [InlineData(ETypeCode.String, "01", "01", ECompareResult.Equal)]
        [InlineData(ETypeCode.String, "001", "01", ECompareResult.Less)]
        [InlineData(ETypeCode.Text, "01", "001", ECompareResult.Greater)]
        [InlineData(ETypeCode.Text, "01", "01", ECompareResult.Equal)]
        [InlineData(ETypeCode.Text, "001", "01", ECompareResult.Less)]
        [InlineData(ETypeCode.Boolean, true, false, ECompareResult.Greater)]
        [InlineData(ETypeCode.Boolean, true, true, ECompareResult.Equal)]
        [InlineData(ETypeCode.Boolean, false, true, ECompareResult.Less)]
        [InlineData(ETypeCode.DateTime, "2001-01-01", "2000-12-31", ECompareResult.Greater)]
        [InlineData(ETypeCode.DateTime, "2001-01-01", "2001-01-01", ECompareResult.Equal)]
        [InlineData(ETypeCode.DateTime, "2000-01-02", "2000-01-03", ECompareResult.Less)]
        [InlineData(ETypeCode.Time, "00:01:00", "00:00:59", ECompareResult.Greater)]
        [InlineData(ETypeCode.Time, "00:00:59", "00:00:59", ECompareResult.Equal)]
        [InlineData(ETypeCode.Time, "00:01:00", "00:01:01", ECompareResult.Less)]
        [InlineData(ETypeCode.Guid, "6d5bba83-e71b-4ce1-beb8-006085a0a77d", "6d5bba83-e71b-4ce1-beb8-006085a0a77c", ECompareResult.Greater)]
        [InlineData(ETypeCode.Guid, "6d5bba83-e71b-4ce1-beb8-006085a0a77c", "6d5bba83-e71b-4ce1-beb8-006085a0a77c", ECompareResult.Equal)]
        [InlineData(ETypeCode.Guid, "6d5bba83-e71b-4ce1-beb8-006085a0a77c", "6d5bba83-e71b-4ce1-beb8-006085a0a77d", ECompareResult.Less)]
        public void DataType_Compare(ETypeCode dataType, object inputValue, object compareValue, ECompareResult expectedResult)
        {
            var result = Operations.Compare(dataType, inputValue, compareValue);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(ETypeCode.Byte, 2, (Byte)2)]
        [InlineData(ETypeCode.Byte, "2", (Byte)2)]
        [InlineData(ETypeCode.SByte, 2, (SByte)2)]
        [InlineData(ETypeCode.SByte, "2", (SByte)2)]
        [InlineData(ETypeCode.UInt16, 2, (UInt16)2)]
        [InlineData(ETypeCode.UInt16, "2", (UInt16)2)]
        [InlineData(ETypeCode.UInt32, 2, (UInt32)2)]
        [InlineData(ETypeCode.UInt32, "2", (UInt32)2)]
        [InlineData(ETypeCode.UInt64, 2, (UInt64)2)]
        [InlineData(ETypeCode.UInt64, "2", (UInt64)2)]
        [InlineData(ETypeCode.Int16, -2, (Int16)(-2))]
        [InlineData(ETypeCode.Int16, "-2", (Int16)(-2))]
        [InlineData(ETypeCode.Int32, -2, (Int32)(-2))]
        [InlineData(ETypeCode.Int32, "-2", (Int32)(-2))]
        [InlineData(ETypeCode.Int64, -2, (Int64)(-2))]
        [InlineData(ETypeCode.Int64, "-2", (Int64)(-2))]
        [InlineData(ETypeCode.Double, -2.123, (Double)(-2.123))]
        [InlineData(ETypeCode.Double, "-2.123 ", (Double)(-2.123))]
        [InlineData(ETypeCode.String, 01, "1")]
        [InlineData(ETypeCode.String, true, "True")]
        [InlineData(ETypeCode.Text, 01, "1")]
        [InlineData(ETypeCode.Text, true, "True")]
        [InlineData(ETypeCode.Boolean, "true", true)]
        [InlineData(ETypeCode.Boolean, "1", true)]
        [InlineData(ETypeCode.Boolean, 1, true)]
        [InlineData(ETypeCode.Boolean, "0", false)]
        [InlineData(ETypeCode.Boolean, 0, false)]
        [InlineData(ETypeCode.Unknown, "123", "123")]
        [MemberData(nameof(OtherParseDataTypes))]
        public void DataType_TryParse(ETypeCode dataType, object inputValue, object expectedValue)
        {
            var result = Operations.Parse(dataType, inputValue);
            Assert.Equal(expectedValue, result);
        }

        public static IEnumerable<object[]> OtherParseDataTypes => new[]
        {
            new object[] { ETypeCode.Decimal, -2.123, (Decimal)(-2.123)},
            new object[] { ETypeCode.Decimal, "-2.123", (Decimal)(-2.123)},
            new object[] { ETypeCode.DateTime, "2001-01-01", new DateTime(2001,01,01)},
            new object[] { ETypeCode.DateTime, "2001-01-01T12:59:59", new DateTime(2001,01,01, 12, 59, 59)},
            new object[] { ETypeCode.Time, "12:59:59", new TimeSpan(12, 59, 59)},
            new object[] { ETypeCode.Guid, "6d5bba83-e71b-4ce1-beb8-006085a0a77d", new Guid("6d5bba83-e71b-4ce1-beb8-006085a0a77d")},
            new object[] { ETypeCode.Guid, "6d5bba83-e71b-4ce1-beb8-006085a0a77d", new Guid("6d5bba83-e71b-4ce1-beb8-006085a0a77d")},
            new object[] { ETypeCode.Binary, "61626364", new byte[] { 0x61, 0x62, 0x63, 0x64 }},
            new object[] { ETypeCode.String, new byte[] { 0x61, 0x62, 0x63, 0x64 }, "61626364"},
            new object[] { ETypeCode.Char, "123", "123".ToCharArray()},
            new object[] { ETypeCode.String, "123".ToCharArray(), "123"}
                    
        };

        [Fact]
        public void DataType_TryParse_XML()
        {
            var result = Operations.Parse(ETypeCode.Xml, "<note>hi there</note>");
            Assert.IsType<XmlDocument>(result);

            var xmldoc = (XmlDocument) result;
            Assert.Equal("hi there", xmldoc.FirstChild.InnerText);
        }

        [Fact]
        public void DataType_TryParse_Json()
        {
            var result = Operations.Parse(ETypeCode.Json, "{\"note\": \"hi there\"}");
            Assert.IsType<JObject>(result);

            var token = (JObject) result;

            Assert.Equal("hi there", token["note"]);
        }


        //values that should throw a parse error
        [Theory]
        [InlineData(ETypeCode.Byte, -1)]
        [InlineData(ETypeCode.SByte, -200)]
        [InlineData(ETypeCode.UInt16, -1)]
        [InlineData(ETypeCode.UInt32, -1)]
        [InlineData(ETypeCode.UInt64, -1)]
        [InlineData(ETypeCode.Int16, Int16.MaxValue + 1)]
        [InlineData(ETypeCode.Int32, "a123")]
        [InlineData(ETypeCode.Int64, "123a")]
        [InlineData(ETypeCode.Double, "123a")]
        [InlineData(ETypeCode.Decimal, "123a")]
        [InlineData(ETypeCode.DateTime, "2001-01-32")]
        [InlineData(ETypeCode.Time, "12:70:00")]
        [InlineData(ETypeCode.Guid, "asdfadsf")]
        public void DataType_TryParse_False(ETypeCode dataType, object inputValue)
        {
            Assert.ThrowsAny<Exception>( () => Operations.Parse(dataType, inputValue));
        }

        [Fact]
        public void DataType_Add()
        {
            Assert.Equal(8, Operations.Add(3, 5));
            Assert.Equal(8d, Operations.Add(3d, 5d));
            Assert.Equal(8L, Operations.Add(3L, 5L));
        }

        [Fact]
        public void DataType_Substract()
        {
            Assert.Equal(2, Operations.Subtract(5, 3));
            Assert.Equal(2d, Operations.Subtract(5d, 3d));
            Assert.Equal(2L, Operations.Subtract(5L, 3L));
        }

        [Fact]
        public void DataType_Multiply()
        {
            Assert.Equal(15, Operations.Multiply(3, 5));
            Assert.Equal(15d, Operations.Multiply(3d, 5d));
            Assert.Equal(15L, Operations.Multiply(3L, 5L));
        }

        [Fact]
        public void DataType_Divude()
        {
            Assert.Equal(4, Operations.Divide(8, 2));
            Assert.Equal(4.5d, Operations.Divide(9d, 2d));
            Assert.Equal(4L, Operations.Divide(8L, 2L));
        }

        [Fact]
        public void DataType_DivideInt()
        {
            Assert.Equal(4, Operations.DivideInt(8, 2));
            Assert.Equal(4.5d, Operations.DivideInt(9d, 2));
            Assert.Equal(4L, Operations.DivideInt(8L, 2));
        }

//        [Theory]
//        [InlineData(ETypeCode.UInt16, (ushort)5, (ushort) 3, (ushort) 8)]
//        [InlineData(ETypeCode.UInt32, (uint)5, (uint) 3, (uint) 8)]
//        [InlineData(ETypeCode.UInt64, (ulong)5, (ulong) 3, (ulong) 8)]
//        [InlineData(ETypeCode.Int16, (short)5, (short) 3, (short) 8)]
//        [InlineData(ETypeCode.Int32, (int)5, (int) 3, (int) 8)]
//        [InlineData(ETypeCode.Int64, (long)5, (long) 3, (long) 8)]
//        [InlineData(ETypeCode.Double, (double)5, (double) 3, (double) 8)]
//        //[InlineData(ETypeCode.Decimal, (decimal)5, (decimal) 3, (decimal) 8)]
//        public void DataType_Add(ETypeCode dataType, object value1, object value2, object expected)
//        {
//            Assert.Equal(expected,  DataType.Add(dataType, value1, value2));
//        }
//        
//        [Theory]
//        [InlineData(ETypeCode.UInt16, (ushort)5, (ushort) 3, (ushort) 2)]
//        [InlineData(ETypeCode.UInt32, (uint)5, (uint) 3, (uint) 2)]
//        [InlineData(ETypeCode.UInt64, (ulong)5, (ulong) 3, (ulong) 2)]
//        [InlineData(ETypeCode.Int16, (short)5, (short) 3, (short) 2)]
//        [InlineData(ETypeCode.Int32, (int)5, (int) 3, (int) 2)]
//        [InlineData(ETypeCode.Int64, (long)5, (long) 3, (long) 2)]
//        [InlineData(ETypeCode.Double, (double)5, (double) 3, (double) 2)]
//        //[InlineData(ETypeCode.Decimal, (decimal)5, (decimal) 3, (decimal) 8)]
//        public void DataType_Subtract(ETypeCode dataType, object value1, object value2, object expected)
//        {
//            Assert.Equal(expected, DataType.Subtract(dataType, value1, value2));
//        }
//        
//        [Theory]
//        [InlineData(ETypeCode.UInt16, (ushort)5, (ushort) 3, (ushort) 15)]
//        [InlineData(ETypeCode.UInt32, (uint)5, (uint) 3, (uint) 15)]
//        [InlineData(ETypeCode.UInt64, (ulong)5, (ulong) 3, (ulong) 15)]
//        [InlineData(ETypeCode.Int16, (short)5, (short) 3, (short) 15)]
//        [InlineData(ETypeCode.Int32, (int)5, (int) 3, (int) 15)]
//        [InlineData(ETypeCode.Int64, (long)5, (long) 3, (long) 15)]
//        [InlineData(ETypeCode.Double, (double)5, (double) 3, (double) 15)]
//        //[InlineData(ETypeCode.Decimal, (decimal)5, (decimal) 3, (decimal) 8)]
//        public void DataType_Multiply(ETypeCode dataType, object value1, object value2, object expected)
//        {
//            Assert.Equal(expected, DataType.Multiply(dataType, value1, value2));
//        }
//        
//        [Theory]
//        [InlineData(ETypeCode.UInt16, (ushort)6, (ushort) 3, (ushort) 2)]
//        [InlineData(ETypeCode.UInt32, (uint)6, (uint) 3, (uint) 2)]
//        [InlineData(ETypeCode.UInt64, (ulong)6, (ulong) 3, (ulong) 2)]
//        [InlineData(ETypeCode.Int16, (short)6, (short) 3, (short) 2)]
//        [InlineData(ETypeCode.Int32, (int)6, (int) 3, (int) 2)]
//        [InlineData(ETypeCode.Int64, (long)6, (long) 3, (long) 2)]
//        [InlineData(ETypeCode.Double, (double)6, (double) 3, (double) 2)]
//        //[InlineData(ETypeCode.Decimal, (decimal)5, (decimal) 3, (decimal) 8)]
//        public void DataType_Divide(ETypeCode dataType, object value1, object value2, object expected)
//        {
//            Assert.Equal(expected, DataType.Divide(dataType, value1, value2));
//        }

        public void Timer(string name, Action action)
        {
            var start = Stopwatch.StartNew();
            action.Invoke();
            var time = start.ElapsedMilliseconds;
            _output.WriteLine($"Test \"{name}\" completed in {time}ms");
        }

        [Theory]
        [InlineData(10000000)]
        public void ComparePerformance(long iterations)
        {

            Timer("Compare integers baseline", () =>
            {
                for(var i = 0; i< iterations; i++)
                {
                    var a = 2;
                    var b = 3;
                    if (a.GetType() == b.GetType())
                    {
                        a.CompareTo(b);
                    }
                }
            });


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
                    var value = Operations.Compare((object)1, 2);
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
