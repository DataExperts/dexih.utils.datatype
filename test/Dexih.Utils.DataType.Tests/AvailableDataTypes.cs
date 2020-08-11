using System;
using System.Linq;
using Xunit;

namespace Dexih.Utils.DataType.Tests
{
    public class TestAvailableDataTypes
    {
        [Theory]
        [InlineData(new [] {"1.1", "1.2"}, new [] {ETypeCode.Double, ETypeCode.String})]
        [InlineData(new [] {"1.1", "abc"}, new [] {ETypeCode.String})]
        [InlineData(new [] {"1 jan 2000", "2000/01/01"}, new [] {ETypeCode.DateTime, ETypeCode.String})]
        [InlineData(new [] {"1234", "123456"}, new [] {ETypeCode.Int32, ETypeCode.Int64, ETypeCode.Double, ETypeCode.String})]
        [InlineData(new [] {"6000000000", "7000000000"}, new [] {ETypeCode.Int64, ETypeCode.Double, ETypeCode.String})]
        [InlineData(new [] {"6000000000", "700.1"}, new [] {ETypeCode.Double, ETypeCode.String})]
        [InlineData(new [] {"0", "1"}, new [] {ETypeCode.Char, ETypeCode.Int32, ETypeCode.Int64, ETypeCode.Double, ETypeCode.Boolean, ETypeCode.String})]
        [InlineData(new [] {"a", "b"}, new [] {ETypeCode.Char, ETypeCode.String})]
        [InlineData(new [] {"a", "bb"}, new [] {ETypeCode.String})]
        [InlineData(new [] {"yes", "no", "true", "false", "on", "off", "n", "y", "0", "1"}, new [] {ETypeCode.Boolean, ETypeCode.String})]
        [InlineData(new [] {"<xml><test></test></xml>"}, new [] {ETypeCode.Xml, ETypeCode.String})]
        [InlineData(new [] {"[]", "{\"test\": 1}"}, new [] {ETypeCode.Json, ETypeCode.String})]
        [InlineData(new [] {"751b03ec-1a67-4b84-8420-2b7a23ded33a"}, new [] {ETypeCode.Guid, ETypeCode.String})]
        [InlineData(new [] {"abc", "def"}, new [] {ETypeCode.CharArray, ETypeCode.String})]
        public void AvailableDataTypes(string[] values, ETypeCode[] available)
        {
            var avail = new AvailableDataTypes();
            foreach(var value in values) avail.CheckValue(value);
            
            Assert.Equal(available.Contains(ETypeCode.Char), avail.CharType);
            Assert.Equal(available.Contains(ETypeCode.Int32), avail.Int32Type);
            Assert.Equal(available.Contains(ETypeCode.Int64), avail.Int64Type);
            Assert.Equal(available.Contains(ETypeCode.Double), avail.DoubleType);
            Assert.Equal(available.Contains(ETypeCode.Boolean), avail.BooleanType);
            Assert.Equal(available.Contains(ETypeCode.DateTime), avail.DateTimeType);
            Assert.Equal(available.Contains(ETypeCode.Xml), avail.XmlType);
            Assert.Equal(available.Contains(ETypeCode.Json), avail.JsonType);
            Assert.Equal(available.Contains(ETypeCode.Guid), avail.GuidType);
        }

        [Fact]
        void AvailableDataTypesNulls()
        {
            var avail = new AvailableDataTypes();
            avail.CheckValue("1");
            avail.CheckValue(null);
            Assert.True(avail.HasNulls);
            Assert.True(avail.Int32Type);
        }
        
        [Fact]
        void AvailableDataTypesBlanks()
        {
            var avail = new AvailableDataTypes();
            avail.CheckValue("1");
            avail.CheckValue(null);
            Assert.True(avail.HasNulls);
            Assert.True(avail.Int32Type);
        }

        
    }
}