using System;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Xml;
using NetTopologySuite.Geometries;

namespace Dexih.Utils.DataType
{
    public class DataObject
    {
        private readonly DataValue _dataValue;
        private readonly bool _isNull = false;
        private readonly byte _rank = 0;

        private readonly object _dataObject;
        
        public DataObject(byte[] value)
        {
            _dataValue = new DataValue(ETypeCode.Binary);
            _dataObject = value;
        }

        public DataObject(Geometry value)
        {
            _dataValue = new DataValue(ETypeCode.Geometry);
            _dataObject = value;
        }
        
        public DataObject(Guid value)
        {
            _dataValue = new DataValue(ETypeCode.Guid);
            _dataObject = value;
        }
        
        public DataObject(JsonElement value)
        {
            _dataValue = new DataValue(ETypeCode.Json);
            _dataObject = value;
        }
        
        public DataObject(object value)
        {
            _dataValue = new DataValue(ETypeCode.Object);
            _dataObject = value;
        }
        
        public DataObject(string value)
        {
            _dataValue = new DataValue(ETypeCode.String);
            _dataObject = value;
        }
        
        public DataObject(char[] value)
        {
            _dataValue = new DataValue(ETypeCode.CharArray);
            _dataObject = value;
        }

        public DataObject(bool value)
        {
            _dataValue = new DataValue(value);
        }
        
        public DataObject(byte value)
        {
            _dataValue = new DataValue(value);
        }
        
        public DataObject(char value)
        {
            _dataValue = new DataValue(value);
        }
        
        public DataObject(decimal value)
        {
            _dataValue = new DataValue(value);
        }
        
        public DataObject(double value)
        {
            _dataValue = new DataValue(value);
        }
        
        public DataObject(Int16 value)
        {
            _dataValue = new DataValue(value);
        }

        public DataObject(Int32 value)
        {
            _dataValue = new DataValue(value);
        }

        public DataObject(Int64 value)
        {
            _dataValue = new DataValue(value);
        }
        
        public DataObject(Single value)
        {
            _dataValue = new DataValue(value);
        }
        
        public DataObject(TimeSpan value)
        {
            _dataValue = new DataValue(value);
        }
        
        public DataObject(DateTime value)
        {
            _dataValue = new DataValue(value);
        }
        
        public DataObject(sbyte value)
        {
            _dataValue = new DataValue(value);
        }

        public DataObject(UInt16 value)
        {
            _dataValue = new DataValue(value);
        }

        public DataObject(UInt32 value)
        {
            _dataValue = new DataValue(value);
        }

        public DataObject(UInt64 value)
        {
            _dataValue = new DataValue(value);
        }

        public DataObject(object value, ETypeCode typeCode)
        {
            if (typeCode.IsValueType())
            {
                _dataValue = new DataValue(value, typeCode);
            }
            else
            {
                _dataValue = new DataValue(typeCode);
                _dataObject = value;
            }
        }

        public DataObject(DataObject dataObject)
        {
            _dataObject = dataObject._dataObject;
            _dataValue = dataObject._dataValue;
            _isNull = dataObject._isNull;
            _rank = dataObject._rank;
        }

        public DataObject(DataValue dataValue)
        {
            _dataValue = dataValue;
        }

        public byte[] Binary {
            get {
                if (ETypeCode.Binary == _dataValue.TypeCode)
                {
                    return (byte[]) _dataObject;
                }
                throw new InvalidTypeException(ETypeCode.Binary, _dataValue.TypeCode);
            }
        }
        
        public Geometry Geometry {
            get {
                if (ETypeCode.Geometry == _dataValue.TypeCode)
                {
                    return (Geometry) _dataObject;
                }
                throw new InvalidTypeException(ETypeCode.Geometry, _dataValue.TypeCode);
            }
        }
        
        public Guid Guid {
            get {
                if (ETypeCode.Guid == _dataValue.TypeCode)
                {
                    return (Guid) _dataObject;
                }
                throw new InvalidTypeException(ETypeCode.Guid, _dataValue.TypeCode);
            }
        }
        
        public JsonDocument Json {
            get {
                if (ETypeCode.Json == _dataValue.TypeCode)
                {
                    return (JsonDocument) _dataObject;
                }
                throw new InvalidTypeException(ETypeCode.Json, _dataValue.TypeCode);
            }
        }
        
        public JsonDocument Node {
            get {
                if (ETypeCode.Node == _dataValue.TypeCode)
                {
                    return (JsonDocument) _dataObject;
                }
                throw new InvalidTypeException(ETypeCode.Node, _dataValue.TypeCode);
            }
        }
        
        public Object Object {
            get {
                if (ETypeCode.Object == _dataValue.TypeCode)
                {
                    return _dataObject;
                }
                throw new InvalidTypeException(ETypeCode.Object, _dataValue.TypeCode);
            }
        }
        
        public string String {
            get {
                if (ETypeCode.String == _dataValue.TypeCode)
                {
                    return (string) _dataObject;
                }
                throw new InvalidTypeException(ETypeCode.String, _dataValue.TypeCode);
            }
        }
        
        public string Text {
            get {
                if (ETypeCode.Text == _dataValue.TypeCode)
                {
                    return (string) _dataObject;
                }
                throw new InvalidTypeException(ETypeCode.Text, _dataValue.TypeCode);
            }
        }
        
        public object Unknown {
            get {
                if (ETypeCode.Unknown == _dataValue.TypeCode)
                {
                    return _dataObject;
                }
                throw new InvalidTypeException(ETypeCode.Unknown, _dataValue.TypeCode);
            }
        }
        
        public object Xml {
            get {
                if (ETypeCode.Xml == _dataValue.TypeCode)
                {
                    return _dataObject;
                }
                throw new InvalidTypeException(ETypeCode.Xml, _dataValue.TypeCode);
            }
        }
        
        public char[] CharArray {
            get {
                if (ETypeCode.CharArray == _dataValue.TypeCode)
                {
                    return (char[]) _dataObject;
                }
                throw new InvalidTypeException(ETypeCode.CharArray, _dataValue.TypeCode);
            }
        }

        public Boolean Boolean => _dataValue.Boolean;
        public Byte Byte => _dataValue.Byte;
        public Char Char => _dataValue.Char;
        public DateTime DateTime => _dataValue.DateTime;
        public Decimal Decimal => _dataValue.Decimal;
        public Double Double => _dataValue.Double;
        public Int16 Int16 => _dataValue.Int16;
        public Int32 Int32 => _dataValue.Int32;
        public Int64 Int64 => _dataValue.Int64;
        public Single Single => _dataValue.Single;
        public sbyte Sbyte => _dataValue.Sbyte;
        public TimeSpan Time => _dataValue.Time;
        public UInt16 UInt16 => _dataValue.UInt16;
        public UInt32 UInt32 => _dataValue.UInt32;
        public UInt64 UInt64 => _dataValue.UInt64;
        public byte Scale => _dataValue.Scale;
        public byte Precision => _dataValue.Precision;

        public int MaxLength
        {
            get
            {
                if (_dataValue.TypeCode == ETypeCode.String)
                {
                    return _dataValue.Int32;
                }
                
                throw new NotSupportedException();
            }
        }

        
  #region Operators

        public override bool Equals(object obj)
        {
            return ((DataObject) obj) == this;
        }

        public override int GetHashCode()
        {
            if (_dataValue.TypeCode.IsValueType())
            {
                return _dataValue.Value.GetHashCode();
            }

            return this._dataObject.GetHashCode();
        }

        public static bool operator ==(DataObject a, DataObject b)
        {
            if (a._dataValue.TypeCode == b._dataValue.TypeCode)
            {
                if (a._dataValue.TypeCode.IsValueType())
                {
                    return a._dataValue == b._dataValue;
                }
                return a._dataValue.TypeCode switch
                {
                    ETypeCode.Unknown => false,
                    ETypeCode.Binary => Operations.Equal(a.Binary, b.Binary),
                    ETypeCode.String => Operations.Equal(a.String, b.String),
                    ETypeCode.Text => Operations.Equal(a.Text, b.Text),
                    ETypeCode.Guid => Operations.Equal(a.Guid, b.Guid),
                    ETypeCode.Json => throw new CompareTypeException(ETypeCode.Json),
                    ETypeCode.Xml => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.Enum => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.CharArray => Operations.Equal(a.CharArray, b.CharArray),
                    ETypeCode.Object => throw new CompareTypeException(ETypeCode.Object),
                    ETypeCode.Node => throw new CompareTypeException(ETypeCode.Node),
                    ETypeCode.Geometry => Operations.Equal(a.Geometry, b.Geometry),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            
            return Operations.Equal(DataType.BestCompareType(a._dataValue.TypeCode, b._dataValue.TypeCode), a.Value, b.Value);
        }
        
        
        public static bool operator !=(DataObject a, DataObject b)
        {
            return !(a == b);
        }
        
        public static bool operator <(DataObject a, DataObject b)
        {
            if (a._dataValue.TypeCode == b._dataValue.TypeCode)
            {
                if (a._dataValue.TypeCode.IsValueType())
                {
                    return a._dataValue < b._dataValue;
                }
                return a._dataValue.TypeCode switch
                {
                    ETypeCode.Unknown => false,
                    ETypeCode.Binary => Operations.LessThan(a.Binary, b.Binary),
                    ETypeCode.String => Operations.LessThan(a.String, b.String),
                    ETypeCode.Text => Operations.LessThan(a.Text, b.Text),
                    ETypeCode.Guid => Operations.LessThan(a.Guid, b.Guid),
                    ETypeCode.Json => throw new CompareTypeException(ETypeCode.Json),
                    ETypeCode.Xml => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.Enum => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.CharArray => Operations.LessThan(a.CharArray, b.CharArray),
                    ETypeCode.Object => throw new CompareTypeException(ETypeCode.Object),
                    ETypeCode.Node => throw new CompareTypeException(ETypeCode.Node),
                    ETypeCode.Geometry => Operations.LessThan(a.Geometry, b.Geometry),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            
            return Operations.LessThan(DataType.BestCompareType(a._dataValue.TypeCode, b._dataValue.TypeCode), a.Value, b.Value);
        }

        public static bool operator >(DataObject a, DataObject b)
        {
            if (a._dataValue.TypeCode == b._dataValue.TypeCode)
            {
                if (a._dataValue.TypeCode.IsValueType())
                {
                    return a._dataValue > b._dataValue;
                }
                return a._dataValue.TypeCode switch
                {
                    ETypeCode.Unknown => false,
                    ETypeCode.Binary => Operations.GreaterThan(a.Binary, b.Binary),
                    ETypeCode.String => Operations.GreaterThan(a.String, b.String),
                    ETypeCode.Text => Operations.GreaterThan(a.Text, b.Text),
                    ETypeCode.Guid => Operations.GreaterThan(a.Guid, b.Guid),
                    ETypeCode.Json => throw new CompareTypeException(ETypeCode.Json),
                    ETypeCode.Xml => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.Enum => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.CharArray => Operations.GreaterThan(a.CharArray, b.CharArray),
                    ETypeCode.Object => throw new CompareTypeException(ETypeCode.Object),
                    ETypeCode.Node => throw new CompareTypeException(ETypeCode.Node),
                    ETypeCode.Geometry => Operations.GreaterThan(a.Geometry, b.Geometry),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            
            return Operations.GreaterThan(DataType.BestCompareType(a._dataValue.TypeCode, b._dataValue.TypeCode), a.Value, b.Value);
        }
        
        public static bool operator <=(DataObject a, DataObject b)
        {
            if (a._dataValue.TypeCode == b._dataValue.TypeCode)
            {
                if (a._dataValue.TypeCode.IsValueType())
                {
                    return a._dataValue <= b._dataValue;
                }
                return a._dataValue.TypeCode switch
                {
                    ETypeCode.Unknown => false,
                    ETypeCode.Binary => Operations.LessThanOrEqual(a.Binary, b.Binary),
                    ETypeCode.String => Operations.LessThanOrEqual(a.String, b.String),
                    ETypeCode.Text => Operations.LessThanOrEqual(a.Text, b.Text),
                    ETypeCode.Guid => Operations.LessThanOrEqual(a.Guid, b.Guid),
                    ETypeCode.Json => throw new CompareTypeException(ETypeCode.Json),
                    ETypeCode.Xml => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.Enum => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.CharArray => Operations.LessThanOrEqual(a.CharArray, b.CharArray),
                    ETypeCode.Object => throw new CompareTypeException(ETypeCode.Object),
                    ETypeCode.Node => throw new CompareTypeException(ETypeCode.Node),
                    ETypeCode.Geometry => Operations.LessThanOrEqual(a.Geometry, b.Geometry),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            
            return Operations.LessThanOrEqual(DataType.BestCompareType(a._dataValue.TypeCode, b._dataValue.TypeCode), a.Value, b.Value);
        }
            
        public static bool operator >=(DataObject a, DataObject b)
        {
            if (a._dataValue.TypeCode == b._dataValue.TypeCode)
            {
                if (a._dataValue.TypeCode.IsValueType())
                {
                    return a._dataValue >= b._dataValue;
                }
                return a._dataValue.TypeCode switch
                {
                    ETypeCode.Unknown => false,
                    ETypeCode.Binary => Operations.GreaterThanOrEqual(a.Binary, b.Binary),
                    ETypeCode.String => Operations.GreaterThanOrEqual(a.String, b.String),
                    ETypeCode.Text => Operations.GreaterThanOrEqual(a.Text, b.Text),
                    ETypeCode.Guid => Operations.GreaterThanOrEqual(a.Guid, b.Guid),
                    ETypeCode.Json => throw new CompareTypeException(ETypeCode.Json),
                    ETypeCode.Xml => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.Enum => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.CharArray => Operations.GreaterThanOrEqual(a.CharArray, b.CharArray),
                    ETypeCode.Object => throw new CompareTypeException(ETypeCode.Object),
                    ETypeCode.Node => throw new CompareTypeException(ETypeCode.Node),
                    ETypeCode.Geometry => Operations.GreaterThanOrEqual(a.Geometry, b.Geometry),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            
            return Operations.GreaterThanOrEqual(DataType.BestCompareType(a._dataValue.TypeCode, b._dataValue.TypeCode), a.Value, b.Value);
        }
                
        public static DataObject operator +(DataObject a, DataObject b)
        {
            if (a._dataValue.IsNumber() && a._dataValue.TypeCode == b._dataValue.TypeCode)
            {
                return new DataObject(a._dataValue + b._dataValue);
            }

            throw new ArgumentOutOfRangeException();
        }
        
        public static DataObject operator -(DataObject a, DataObject b)
        {
            if (a._dataValue.IsNumber() && a._dataValue.TypeCode == b._dataValue.TypeCode)
            {
                return new DataObject(a._dataValue - b._dataValue);
            }

            throw new ArgumentOutOfRangeException();
        }
        
        public static DataObject operator *(DataObject a, DataObject b)
        {
            if (a._dataValue.IsNumber() && a._dataValue.TypeCode == b._dataValue.TypeCode)
            {
                return new DataObject(a._dataValue * b._dataValue);
            }

            throw new ArgumentOutOfRangeException();
        }
        
        public static DataObject operator /(DataObject a, DataObject b)
        {
            if (a._dataValue.IsNumber() && a._dataValue.TypeCode == b._dataValue.TypeCode)
            {
                return new DataObject(a._dataValue / b._dataValue);
            }

            throw new ArgumentOutOfRangeException();
        }
        
        
        #endregion
        
              #region Conversion

        public override string ToString()
        {
            return Value.ToString();
        }

        #endregion

        public T GetValue<T>()
        {
            return (T) Value;
        }
        
        public object Value {
            get {

                switch (_dataValue.TypeCode) {
                    case ETypeCode.Unknown:           
                        return null;
                    case ETypeCode.Boolean:         
                        return _dataValue.Boolean;
                    case ETypeCode.Byte:            
                        return _dataValue.Byte;
                    case ETypeCode.DateTime:        
                        return _dataValue.DateTime;
                    case ETypeCode.Date:        
                        return _dataValue.DateTime.Date;
                    case ETypeCode.Decimal:         
                        return _dataValue.Decimal;
                    case ETypeCode.Double:          
                        return _dataValue.Double;
                    case ETypeCode.Int16:           
                        return _dataValue.Int16;
                    case ETypeCode.Int32:           
                        return _dataValue.Int32;
                    case ETypeCode.Int64:           
                        return _dataValue.Int64;
                    case ETypeCode.Single:          
                        return _dataValue.Single;
                    case ETypeCode.Time:            
                        return _dataValue.Time;
                    case ETypeCode.Char:
                        return _dataValue.Char;
                    case ETypeCode.SByte:
                        return _dataValue.Sbyte;
                    case ETypeCode.UInt16:
                        return _dataValue.UInt16;
                    case ETypeCode.UInt32:
                        return _dataValue.UInt32;
                    case ETypeCode.UInt64:
                        return _dataValue.UInt64;
                    case ETypeCode.Binary:
                    case ETypeCode.String:
                    case ETypeCode.Text:
                    case ETypeCode.Guid:
                    case ETypeCode.Json:
                    case ETypeCode.Xml:
                    case ETypeCode.Enum:
                    case ETypeCode.CharArray:
                    case ETypeCode.Object:
                    case ETypeCode.Node:
                    case ETypeCode.Geometry:
                        return _dataObject;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
  
        public ETypeCode GetTypeCode()
        {
            return _dataValue.TypeCode;
        }

        public Type GetItemType ()
        {
            return DataType.GetType(_dataValue.TypeCode);
        }

        public bool IsString()
        {
            return DataType.IsString(_dataValue.TypeCode);
        }

        public bool IsDiscrete()
        {
            return DataType.IsDiscrete(_dataValue.TypeCode);
        }

        public bool IsDecimal()
        {
            return DataType.IsDecimal(_dataValue.TypeCode);
        }

        public bool IsNumber()
        {
            return DataType.IsNumber(_dataValue.TypeCode);
        }
    }
}