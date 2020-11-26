using System;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using NetTopologySuite.Geometries.Implementation;

namespace Dexih.Utils.DataType
{
    /// <summary>
    /// Checks string values for potential datatypes and eliminates non-compliant values.
    /// </summary>
    public struct AvailableDataTypes
    {
        private bool _valueChecked;

        public bool CharType { get; private set; }
        public bool CharArrayType { get; private set; }
        public bool JsonType { get; private  set; }
        public bool XmlType { get; private  set; }
        public bool GuidType { get; private  set; }
        public bool DateType { get; private  set; }
        public bool TimeType { get; private  set; }
        public bool Int32Type { get; private  set; }
        public bool Int64Type { get; private  set; }
        public bool DoubleType { get; private  set; }
        public bool BooleanType { get; private  set; }
        public bool DateTimeType { get; private  set; }
        
        public bool DateTimeOffsetType { get; private  set; }
        public int MinLength { get; private set; }
        public int MaxLength { get; private  set; }
        public bool HasNulls { get; private  set; }
        public bool HasBlanks { get; private set; }

        public void Reset()
        {
            _valueChecked = false;
        }
        
        /// <summary>
        /// Checks the value for different data types and eliminates the ones that are invalid.
        /// Can be called multiple times to check values as needed.
        /// </summary>
        /// <param name="value"></param>
        public void CheckValue(string value)
        {
            if (!_valueChecked)
            {
                CharType = true;
                Int32Type = true;
                Int64Type = true;
                DoubleType = true;
                BooleanType = true;
                DateTimeType = true;
                DateTimeOffsetType = true;
                JsonType = true;
                XmlType = true;
                GuidType = true;
                DateType = true;
                CharArrayType = true;
                MinLength = Int32.MaxValue;
                MaxLength = 0;
                HasNulls = false;
                HasBlanks = false;
            }

            _valueChecked = true;

            if (value == null)
            {
                HasNulls = true;
                return;
            }

            if (value is string stringValue && string.IsNullOrWhiteSpace(stringValue))
            {
                HasBlanks = true;
                return;
            }

            if (CharType && value.Length > 1)
            {
                CharType = false;
            }

            if (Int32Type && !int.TryParse(value, out _))
            {
                Int32Type = false;
            }

            if (Int64Type && !long.TryParse(value, out _))
            {
                Int64Type = false;
            }

            if (BooleanType && !Operations.TryParseBoolean(value, out _))
            {
                BooleanType = false;
            }

            if (DateTimeType && DateTime.TryParse(value, out var dateTime))
            {
                if (dateTime.TimeOfDay != TimeSpan.Zero)
                {
                    DateType = false;
                }
            }
            else
            {
                DateTimeType = false;
                DateType = false;
                DateTimeOffsetType = false;
            }

            if (TimeType && !TimeSpan.TryParse(value, out _))
            {
                TimeType = false;
            }

            if (DateTimeType && !DateTime.TryParse(value, out _))
            {
                DateType = false;
            }
            
            if (DoubleType)
            {
                if (!double.TryParse(value, out _))
                {
                    DoubleType = false;
                }
                else
                {
                    // if the string is a double then override the datetime parse with a false (as value like 1.1 will incorrectly parse as a date)
                    DateTimeType = false;
                    DateTimeOffsetType = false;
                }
            }

            if (JsonType)
            {
                try
                {
                    if(value.IndexOfAny(new []{'{', '['}) < 0)
                    {
                        JsonType = false;
                    }
                    JsonDocument.Parse(value);
                }
                catch
                {
                    JsonType = false;
                }
            }

            if (XmlType)
            {
                try
                {
                    var doc = new XmlDocument();
                    doc.LoadXml(value);
                }
                catch
                {
                    XmlType = false;
                }
            }

            if (GuidType && !Guid.TryParse(value, out _))
            {
                GuidType = false;
            }
            

            if (value.Length < MinLength)
            {
                MinLength = value.Length;
            }

            if (value.Length > MaxLength)
            {
                MaxLength = value.Length;
            }

            if (MinLength != MaxLength)
            {
                CharArrayType = false;
            }

        }

        /// <summary>
        /// Get the most likely time by testing for non-string types, and then falling back to string if nothing else matches.
        /// </summary>
        /// <returns></returns>
        public ETypeCode GetBestType()
        {
            if (!_valueChecked) return ETypeCode.String;

            if (BooleanType) return ETypeCode.Boolean;
            if (DateType) return ETypeCode.Date;
            if (DateTimeType) return ETypeCode.DateTime;
            if (TimeType) return ETypeCode.Time;
            if (Int32Type) return ETypeCode.Int32;
            if (Int64Type) return ETypeCode.Int64;
            if (DoubleType) return ETypeCode.Double;
            if (CharType) return ETypeCode.Char;
            if (CharArrayType) return ETypeCode.CharArray;
            if (JsonType) return ETypeCode.Json;
            if (XmlType) return ETypeCode.Xml;
            if (GuidType) return ETypeCode.Guid;
            
            return ETypeCode.String;
        }
    }
}