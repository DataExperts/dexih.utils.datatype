using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;
using Xunit;

namespace Dexih.Utils.DataType.Tests
{
    public class DataObjectTests
    {
        [Fact]
        void DataObject_Bool()
        {
            var obj = new DataObject();
            obj.Boolean = true;
            Assert.Equal(true, obj.Value);
        }
        
        [Theory]
        [InlineData(ETypeCode.Int16, (Int16) 5, ETypeCode.Int16, (Int16) 5, 10)]
        [InlineData(ETypeCode.Int32, (Int32) 5, ETypeCode.Int32, (Int32) 5, (Int32)10)]
        [InlineData(ETypeCode.Int64, (Int64) 5, ETypeCode.Int64, (Int64) 5, (Int64)10)]
        [InlineData(ETypeCode.UInt16, (UInt16) 5, ETypeCode.UInt16, (UInt16) 5, (int)10)]
        [InlineData(ETypeCode.UInt32, (UInt32) 5, ETypeCode.UInt32, (UInt32) 5,(UInt32) 10)]
        [InlineData(ETypeCode.UInt64, (UInt64) 5, ETypeCode.UInt64, (UInt64) 5,(UInt64) 10)]
        [InlineData(ETypeCode.Double, (Double) 5, ETypeCode.Double, (Double) 5, (Double)10)]
        [InlineData(ETypeCode.Single, (Single) 5, ETypeCode.Single, (Single) 5,(Single) 10)]
        [InlineData(ETypeCode.Byte, (Byte) 5, ETypeCode.Byte, (Byte) 5, 10)]
        [InlineData(ETypeCode.SByte, (SByte) 5, ETypeCode.SByte, (SByte) 5,(int) 10)]
        [MemberData(nameof(OtherAddTypes))]
        void DataObject_Add(ETypeCode typeCode1, object value1, ETypeCode typeCode2, object value2, object expected)
        {
            var da1 = new DataObject(value1, typeCode1);
            var da2 = new DataObject(value2, typeCode2);

            var result = da1 + da2;
            
            Assert.Equal(expected, result.Value);
            
        }
        
        public static IEnumerable<object[]> OtherAddTypes => new[]
        {
            new object[] { ETypeCode.Decimal, (Decimal) 5, ETypeCode.Decimal, (Decimal) 5, (Decimal)10},
        };
        
        
        [Theory]
        [InlineData(ETypeCode.Int16, (Int16) 10, ETypeCode.Int16, (Int16) 5, 5)]
        [InlineData(ETypeCode.Int32, (Int32) 10, ETypeCode.Int32, (Int32) 5, (Int32)5)]
        [InlineData(ETypeCode.Int64, (Int64) 10, ETypeCode.Int64, (Int64) 5, (Int64)5)]
        [InlineData(ETypeCode.UInt16, (UInt16) 10, ETypeCode.UInt16, (UInt16) 5, (int)5)]
        [InlineData(ETypeCode.UInt32, (UInt32) 10, ETypeCode.UInt32, (UInt32) 5,(UInt32) 5)]
        [InlineData(ETypeCode.UInt64, (UInt64) 10, ETypeCode.UInt64, (UInt64) 5,(UInt64) 5)]
        [InlineData(ETypeCode.Double, (Double) 10, ETypeCode.Double, (Double) 5, (Double)5)]
        [InlineData(ETypeCode.Single, (Single) 10, ETypeCode.Single, (Single) 5,(Single) 5)]
        [InlineData(ETypeCode.Byte, (Byte) 10, ETypeCode.Byte, (Byte) 5, 5)]
        [InlineData(ETypeCode.SByte, (SByte) 10, ETypeCode.SByte, (SByte) 5,(int) 5)]
        [MemberData(nameof(OtherSubtractTypes))]
        void DataObject_Subtract(ETypeCode typeCode1, object value1, ETypeCode typeCode2, object value2, object expected)
        {
            var da1 = new DataObject(value1, typeCode1);
            var da2 = new DataObject(value2, typeCode2);

            var result = da1 - da2;
            
            Assert.Equal(expected, result.Value);
            
        }
        
        public static IEnumerable<object[]> OtherSubtractTypes => new[]
        {
            new object[] { ETypeCode.Decimal, (Decimal) 10, ETypeCode.Decimal, (Decimal) 5, (Decimal)5},
        };

        [Theory]
        [InlineData(ETypeCode.Int16, (Int16) 10, ETypeCode.Int16, (Int16) 5, 50)]
        [InlineData(ETypeCode.Int32, (Int32) 10, ETypeCode.Int32, (Int32) 5, (Int32)50)]
        [InlineData(ETypeCode.Int64, (Int64) 10, ETypeCode.Int64, (Int64) 5, (Int64)50)]
        [InlineData(ETypeCode.UInt16, (UInt16) 10, ETypeCode.UInt16, (UInt16) 5, (int)50)]
        [InlineData(ETypeCode.UInt32, (UInt32) 10, ETypeCode.UInt32, (UInt32) 5,(UInt32) 50)]
        [InlineData(ETypeCode.UInt64, (UInt64) 10, ETypeCode.UInt64, (UInt64) 5,(UInt64) 50)]
        [InlineData(ETypeCode.Double, (Double) 10, ETypeCode.Double, (Double) 5, (Double)50)]
        [InlineData(ETypeCode.Single, (Single) 10, ETypeCode.Single, (Single) 5,(Single) 50)]
        [InlineData(ETypeCode.Byte, (Byte) 10, ETypeCode.Byte, (Byte) 5, 50)]
        [InlineData(ETypeCode.SByte, (SByte) 10, ETypeCode.SByte, (SByte) 5,(int) 50)]
        [MemberData(nameof(OtherMultiplyTypes))]
        void DataObject_Multiply(ETypeCode typeCode1, object value1, ETypeCode typeCode2, object value2, object expected)
        {
            var da1 = new DataObject(value1, typeCode1);
            var da2 = new DataObject(value2, typeCode2);

            var result = da1 * da2;
            
            Assert.Equal(expected, result.Value);
            
        }
        
        public static IEnumerable<object[]> OtherMultiplyTypes => new[]
        {
            new object[] { ETypeCode.Decimal, (Decimal) 10, ETypeCode.Decimal, (Decimal) 5, (Decimal)50},
        };
        
        [Theory]
        [InlineData(ETypeCode.Int16, (Int16) 10, ETypeCode.Int16, (Int16) 5, 2)]
        [InlineData(ETypeCode.Int32, (Int32) 10, ETypeCode.Int32, (Int32) 5, (Int32)2)]
        [InlineData(ETypeCode.Int64, (Int64) 10, ETypeCode.Int64, (Int64) 5, (Int64)2)]
        [InlineData(ETypeCode.UInt16, (UInt16) 10, ETypeCode.UInt16, (UInt16) 5, (int)2)]
        [InlineData(ETypeCode.UInt32, (UInt32) 10, ETypeCode.UInt32, (UInt32) 5,(UInt32) 2)]
        [InlineData(ETypeCode.UInt64, (UInt64) 10, ETypeCode.UInt64, (UInt64) 5,(UInt64) 2)]
        [InlineData(ETypeCode.Double, (Double) 10, ETypeCode.Double, (Double) 5, (Double)2)]
        [InlineData(ETypeCode.Single, (Single) 10, ETypeCode.Single, (Single) 5,(Single) 2)]
        [InlineData(ETypeCode.Byte, (Byte) 10, ETypeCode.Byte, (Byte) 5, 2)]
        [InlineData(ETypeCode.SByte, (SByte) 10, ETypeCode.SByte, (SByte) 5,(int) 2)]
        [MemberData(nameof(OtherDivideTypes))]
        void DataObject_Divide(ETypeCode typeCode1, object value1, ETypeCode typeCode2, object value2, object expected)
        {
            var da1 = new DataObject(value1, typeCode1);
            var da2 = new DataObject(value2, typeCode2);

            var result = da1 / da2;
            
            Assert.Equal(expected, result.Value);
            
        }
        
        public static IEnumerable<object[]> OtherDivideTypes => new[]
        {
            new object[] { ETypeCode.Decimal, (Decimal) 10, ETypeCode.Decimal, (Decimal) 5, (Decimal)2},
        };
        
          [Theory]
        [InlineData(ETypeCode.Byte, (byte)2, (byte)1, 1)]
        [InlineData(ETypeCode.Byte, (byte)1, (byte)1, 0)]
        [InlineData(ETypeCode.Byte, (byte)1, (byte)2, -1)]
        [InlineData(ETypeCode.SByte, 2, 1, 1)]
        [InlineData(ETypeCode.SByte, 1, 1, 0)]
        [InlineData(ETypeCode.SByte, 1, 2, -1)]
        [InlineData(ETypeCode.UInt16, 2, 1, 1)]
        [InlineData(ETypeCode.UInt16, 1, 1, 0)]
        [InlineData(ETypeCode.UInt16, 1, 2, -1)]
        [InlineData(ETypeCode.UInt32, 2, 1, 1)]
        [InlineData(ETypeCode.UInt32, 1, 1, 0)]
        [InlineData(ETypeCode.UInt32, 1, 2, -1)]
        [InlineData(ETypeCode.UInt64, 2, 1, 1)]
        [InlineData(ETypeCode.UInt64, 1, 1, 0)]
        [InlineData(ETypeCode.UInt64, 1, 2, -1)]
        [InlineData(ETypeCode.Int16, 2, 1, 1)]
        [InlineData(ETypeCode.Int16, 1, 1, 0)]
        [InlineData(ETypeCode.Int16, 1, 2, -1)]
        [InlineData(ETypeCode.Int32, 2, 1, 1)]
        [InlineData(ETypeCode.Int32, 1, 1, 0)]
        [InlineData(ETypeCode.Int32, 1, 2, -1)]
        [InlineData(ETypeCode.Int64, 2, 1, 1)]
        [InlineData(ETypeCode.Int64, 1, 1, 0)]
        [InlineData(ETypeCode.Int64, 1, 2, -1)]
        [InlineData(ETypeCode.Decimal, 1.1, 1.09, 1)]
        [InlineData(ETypeCode.Decimal, 1.09, 1.09, 0)]
        [InlineData(ETypeCode.Decimal, 1.09, 1.1, -1)]
        [InlineData(ETypeCode.Double, 1.1, 1.09, 1)]
        [InlineData(ETypeCode.Double, 1.09, 1.09, 0)]
        [InlineData(ETypeCode.Double, 1.09, 1.1, -1)]
        [InlineData(ETypeCode.Single, 1.1, 1.09, 1)]
        [InlineData(ETypeCode.Single, 1.09, 1.09, 0)]
        [InlineData(ETypeCode.Single, 1.09, 1.1, -1)]
        [InlineData(ETypeCode.String, "01", "001", 1)]
        [InlineData(ETypeCode.String, "01", "01", 0)]
        [InlineData(ETypeCode.String, "001", "01", -1)]
        [InlineData(ETypeCode.Text, "01", "001", 1)]
        [InlineData(ETypeCode.Text, "01", "01", 0)]
        [InlineData(ETypeCode.Text, "001", "01", -1)]
        [InlineData(ETypeCode.Boolean, true, false, 1)]
        [InlineData(ETypeCode.Boolean, true, true, 0)]
        [InlineData(ETypeCode.Boolean, false, true, -1)]
        [InlineData(ETypeCode.DateTime, "2001-01-01", "2000-12-31", 1)]
        [InlineData(ETypeCode.DateTime, "2001-01-01", "2001-01-01", 0)]
        [InlineData(ETypeCode.DateTime, "2000-01-02", "2000-01-03", -1)]
        [InlineData(ETypeCode.Time, "00:01:00", "00:00:59", 1)]
        [InlineData(ETypeCode.Time, "00:00:59", "00:00:59", 0)]
        [InlineData(ETypeCode.Time, "00:01:00", "00:01:01", -1)]
        [InlineData(ETypeCode.Guid, "6d5bba83-e71b-4ce1-beb8-006085a0a77d", "6d5bba83-e71b-4ce1-beb8-006085a0a77c", 1)]
        [InlineData(ETypeCode.Guid, "6d5bba83-e71b-4ce1-beb8-006085a0a77c", "6d5bba83-e71b-4ce1-beb8-006085a0a77c", 0)]
        [InlineData(ETypeCode.Guid, "6d5bba83-e71b-4ce1-beb8-006085a0a77c", "6d5bba83-e71b-4ce1-beb8-006085a0a77d", -1)]
        [MemberData(nameof(OtherCompareTypes))]
        public void DataType_Compare(ETypeCode dataType, object v1, object v2, int expectedResult)
        {
            var inputValue = Operations.Parse(dataType, v1);
            var compareValue = Operations.Parse(dataType, v2);
            var comp1 = Operations.Compare(dataType, inputValue, compareValue);
            Assert.Equal(expectedResult, comp1);

            var comp2 = Operations.Compare(inputValue, compareValue);
            Assert.Equal(expectedResult, comp2);
            
            var input = new DataObject(inputValue, dataType);
            var compare = new DataObject(compareValue, dataType);
            
            Assert.Equal(comp1 == -1, input < compare);
            Assert.Equal(comp1 <= 0, input <= compare);
            Assert.Equal(comp1 == 0, input == compare);
            Assert.Equal(comp1 == 1, input > compare);
            Assert.Equal(comp1 >= 0, input >= compare);
        }

        public static IEnumerable<object[]> OtherCompareTypes => new[]
        {
            new object[] { ETypeCode.CharArray, "001".ToCharArray(), "01".ToCharArray(), -1},
            new object[] { ETypeCode.CharArray, "01".ToCharArray(), "001".ToCharArray(), 1},
            new object[] { ETypeCode.CharArray, "021".ToCharArray(), "01".ToCharArray(), 1},
            new object[] { ETypeCode.CharArray, "001".ToCharArray(), "001".ToCharArray(), 0},
            new object[] { ETypeCode.Binary, new byte[] {1,2,3}, new byte[] {1,2,2}, 1},
            new object[] { ETypeCode.Binary, new byte[] {1,2}, new byte[] {1,2,3}, -1},
            new object[] { ETypeCode.Binary, new byte[] {1,2,2}, new byte[] {1,2,3}, -1},
            new object[] { ETypeCode.Binary, new byte[] {1,2,3}, new byte[] {1,2,3}, 0},
            new object[] { ETypeCode.Geometry, new Point(1,1), new Point(1,1), 0},
        };
    }
}