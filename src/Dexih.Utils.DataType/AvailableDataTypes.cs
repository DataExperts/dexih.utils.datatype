using System;

namespace Dexih.Utils.DataType
{
    public struct AvailableDataTypes
    {
        private bool _valueChecked;

        public bool CharType { get; set; }
        public bool Int32Type { get; set; }
        public bool Int64Type { get; set; }
        public bool DoubleType { get; set; }
        public bool BooleanType { get; set; }
        public bool DateTimeType { get; set; }
        public bool StringType { get; set; }

        public void Reset()
        {
            _valueChecked = false;
        }
        
        public void CheckValue(string value)
        {
            if (value == null) return;

            if (!_valueChecked)
            {
                CharType = true;
                Int32Type = true;
                Int64Type = true;
                DoubleType = true;
                BooleanType = true;
                DateTimeType = true;
                StringType = true;
            }

            _valueChecked = true;

            if (value.Length > 1)
            {
                CharType = false;
            }

            if (!int.TryParse(value, out var _))
            {
                Int32Type = false;
            }

            if (!long.TryParse(value, out var _))
            {
                Int64Type = false;
            }

            if (!Operations.TryParseBoolean(value, out var _))
            {
                BooleanType = false;
            }

            if (!DateTime.TryParse(value, out var _))
            {
                DateTimeType = false;
            }

            if (!double.TryParse(value, out var _))
            {
                DoubleType = false;
            }
            else
            {
                // if the string is a double then override the datetime parse with a false (as value like 1.1 will incorrectly parse as a date)
                DateTimeType = false;
            }

            if (value.Length > 4000)
            {
                StringType = false;
            }
        }

        public DataType.ETypeCode GetBestType()
        {
            if (!_valueChecked) return DataType.ETypeCode.String;

            if (BooleanType) return DataType.ETypeCode.Boolean;
            if (DateTimeType) return DataType.ETypeCode.DateTime;
            if (Int32Type) return DataType.ETypeCode.Int32;
            if (Int64Type) return DataType.ETypeCode.Int64;
            if (DoubleType) return DataType.ETypeCode.Double;
            if (CharType) return DataType.ETypeCode.Char;
            if (StringType) return DataType.ETypeCode.String;

            return DataType.ETypeCode.Text;
        }
    }
}