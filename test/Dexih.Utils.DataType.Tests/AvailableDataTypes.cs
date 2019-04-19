using System;
using System.Linq;
using Xunit;

namespace Dexih.Utils.DataType.Tests
{
    public class TestAvailableDataTypes
    {
        [Theory]
        [InlineData(new [] {"1.1", "1.2"}, new [] {TypeCode.Double, TypeCode.String})]
        [InlineData(new [] {"1.1", "abc"}, new [] {TypeCode.String})]
        [InlineData(new [] {"1 jan 2000", "2000/01/01"}, new [] {TypeCode.DateTime, TypeCode.String})]
        [InlineData(new [] {"1234", "123456"}, new [] {TypeCode.Int32, TypeCode.Int64, TypeCode.Double, TypeCode.String})]
        [InlineData(new [] {"6000000000", "7000000000"}, new [] {TypeCode.Int64, TypeCode.Double, TypeCode.String})]
        [InlineData(new [] {"6000000000", "700.1"}, new [] {TypeCode.Double, TypeCode.String})]
        [InlineData(new [] {"0", "1"}, new [] {TypeCode.Char, TypeCode.Int32, TypeCode.Int64, TypeCode.Double, TypeCode.Boolean, TypeCode.String})]
        [InlineData(new [] {"a", "b"}, new [] {TypeCode.Char, TypeCode.String})]
        [InlineData(new [] {"a", "bb"}, new [] {TypeCode.String})]
        [InlineData(new [] {"yes", "no", "true", "false", "on", "off", "n", "y", "0", "1"}, new [] {TypeCode.Boolean, TypeCode.String})]
        public void AvailableDataTypes(string[] values, TypeCode[] available)
        {
            var avail = new AvailableDataTypes();
            foreach(var value in values) avail.CheckValue(value);
            
            Assert.Equal(available.Contains(TypeCode.Char), avail.CharType);
            Assert.Equal(available.Contains(TypeCode.Int32), avail.Int32Type);
            Assert.Equal(available.Contains(TypeCode.Int64), avail.Int64Type);
            Assert.Equal(available.Contains(TypeCode.Double), avail.DoubleType);
            Assert.Equal(available.Contains(TypeCode.String), avail.StringType);
            Assert.Equal(available.Contains(TypeCode.Boolean), avail.BooleanType);
            Assert.Equal(available.Contains(TypeCode.DateTime), avail.DateTimeType);
            
        }
    }
}