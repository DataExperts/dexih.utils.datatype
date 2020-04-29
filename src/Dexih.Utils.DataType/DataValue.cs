using System;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Xml;
using System.Xml.XPath;
using NetTopologySuite.Geometries;

namespace Dexih.Utils.DataType
{
    public readonly struct DataValue
    {
        #region Storage
        internal struct NumericInfo {
            // This is used to store Decimal data
            internal Int32 Data1;
            internal Int32 Data2;
            internal Int32 Data3;
            internal Int32 Data4;
            internal Byte  Precision;
            internal Byte  Scale;
            internal Boolean Positive;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal readonly struct Storage
        {
            [FieldOffset(0)] internal readonly Boolean _boolean;
            [FieldOffset(0)] internal readonly Byte _byte;
            [FieldOffset(0)] internal readonly Char _char;
            [FieldOffset(0)] internal readonly Double _double;
            [FieldOffset(0)] internal readonly NumericInfo _numericInfo;
            [FieldOffset(0)] internal readonly Int16 _int16;
            [FieldOffset(0)] internal readonly Int32 _int32; // also used for rank
            [FieldOffset(0)] internal readonly Int64 _int64; // also used to store UtcDateTime, Date , and Time
            [FieldOffset(0)] internal readonly UInt16 _uint16;
            [FieldOffset(0)] internal readonly UInt32 _uint32;
            [FieldOffset(0)] internal readonly UInt64 _uint64;
            [FieldOffset(0)] internal readonly sbyte _sbyte;
            [FieldOffset(0)] internal readonly Single _single;

            public Storage(bool value) : this()
            {
                _boolean = value;
            }

            public Storage(byte value) : this()
            {
                _byte = value;
            }

            public Storage(char value) : this()
            {
                _char = value;
            }

            public Storage(double value) : this()
            {
                _double = value;
            }

            public Storage(Int16 value) : this()
            {
                _int16 = value;
            }

            public Storage(Int32 value) : this()
            {
                _int32 = value;
            }

            public Storage(Int64 value) : this()
            {
                _int64 = value;
            }
            
            public Storage(decimal value) : this()
            {
                var bytes = Decimal.GetBits(value);
                _numericInfo.Data1 = bytes[0];
                _numericInfo.Data2 = bytes[1];
                _numericInfo.Data3 = bytes[2];
                _numericInfo.Data4 = bytes[3];
                _numericInfo.Precision = 38;
                _numericInfo.Scale = (byte) ((bytes[3] >> 16) & 0x7F);
                _numericInfo.Positive = (bytes[3] & 0x80000000) == 0;
            }

            public Storage(Single value) : this()
            {
                _single = value;
            }

            public Storage(TimeSpan value) : this()
            {
                _int64 = value.Ticks;
                ;
            }

            public Storage(DateTime value) : this()
            {
                _int64 = value.Ticks;
            }

            public Storage(sbyte value) : this()
            {
                _sbyte = value;
            }

            public Storage(UInt16 value) : this()
            {
                _uint16 = value;
            }

            public Storage(UInt32 value) : this()
            {
                _uint32 = value;
            }

            public Storage(UInt64 value) : this()
            {
                _uint64 = value;
            }
            
        }

        private readonly ETypeCode  _typeCode;
        private readonly Storage      _value;
        
        #endregion
        
        #region Initializers

        public DataValue(bool value)
        {
            _typeCode = ETypeCode.Boolean;
            _value = new Storage(value);
        }
        
        public DataValue(byte value)
        {
            _typeCode = ETypeCode.Byte;
            _value = new Storage(value);
        }
        public DataValue(char value)
        {
            _typeCode = ETypeCode.Char;
            _value = new Storage(value);
        }
        public DataValue(decimal value)
        {
            _typeCode = ETypeCode.Decimal;
            _value = new Storage(value);
        }
        public DataValue(double value)
        {
            _typeCode = ETypeCode.Double;
            _value = new Storage(value);
        }
        public DataValue(Int16 value)
        {
            _typeCode = ETypeCode.Int16;
            _value = new Storage(value);
        }
        public DataValue(Int32 value)
        {
            _typeCode = ETypeCode.Int32;
            _value = new Storage(value);
        }
        public DataValue(Int64 value)
        {
            _typeCode = ETypeCode.Int64;
            _value = new Storage(value);
        }
        public DataValue(Single value)
        {
            _typeCode = ETypeCode.Single;
            _value = new Storage(value);
        }
        public DataValue(TimeSpan value)
        {
            _typeCode = ETypeCode.Time;
            _value = new Storage(value);
        }
        public DataValue(DateTime value)
        {
            _typeCode = ETypeCode.DateTime;
            _value = new Storage(value);
        }
        public DataValue(sbyte value)
        {
            _typeCode = ETypeCode.SByte;
            _value = new Storage(value);
        }
        public DataValue(UInt16 value)
        {
            _typeCode = ETypeCode.UInt16;
            _value = new Storage(value);
        }
        public DataValue(UInt32 value)
        {
            _typeCode = ETypeCode.UInt32;
            _value = new Storage(value);
        }
        public DataValue(UInt64 value)
        {
            _typeCode = ETypeCode.UInt64;
            _value = new Storage(value);
        }

        // used to store typecode only.
        public DataValue(ETypeCode typeCode)
        {
            _typeCode = typeCode;
            _value = new Storage();
        }

        public DataValue(object value, ETypeCode typeCode)
        {
          _typeCode = typeCode;
          
            if (value is null)
            {
                _value = new Storage();
                return;
            }

            switch (_typeCode)
            {
                case ETypeCode.Binary:
                    _value = new Storage();
                    break;
                case ETypeCode.Byte:
                    _value = new Storage((byte)value);
                    break;
                case ETypeCode.Char:
                    _value = new Storage((char)value);
                    break;
                case ETypeCode.SByte:
                    _value = new Storage((sbyte)value);
                    break;
                case ETypeCode.UInt16:
                    _value = new Storage((UInt16)value);
                    break;
                case ETypeCode.UInt32:
                    _value = new Storage((UInt32)value);
                    break;
                case ETypeCode.UInt64:
                    _value = new Storage((UInt64)value);
                    break;
                case ETypeCode.Int16:
                    _value = new Storage((Int16)value);
                    break;
                case ETypeCode.Int32:
                    _value = new Storage((Int32)value);
                    break;
                case ETypeCode.Int64:
                    _value = new Storage((Int64)value);
                    break;
                case ETypeCode.Decimal:
                    _value = new Storage((Decimal)value);
                    break;
                case ETypeCode.Double:
                    _value = new Storage((Double)value);
                    break;
                case ETypeCode.Single:
                    _value = new Storage((Single)value);
                    break;
                case ETypeCode.String:
                case ETypeCode.Text:
                    _value = new Storage();
                    break;
                case ETypeCode.CharArray:
                    _value = new Storage();
                    break;
                case ETypeCode.Boolean:
                    _value = new Storage((bool)value);
                    break;
                case ETypeCode.DateTime:
                    _value = new Storage((DateTime)value);
                    break;
                case ETypeCode.Date:
                    _value = new Storage((DateTime)value);
                    break;
                case ETypeCode.Time:
                    _value = new Storage((TimeSpan)value);
                    break;
                case ETypeCode.Guid:
                    _value = new Storage();
                    break;
                case ETypeCode.Enum:
                    _value = new Storage((Int32)value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }        
        }

     #endregion

        #region Properties
     
        public bool IsEmpty => (ETypeCode.Unknown == _typeCode);
        
        public ETypeCode TypeCode => _typeCode;

        public Boolean Boolean {
            get {
                if (ETypeCode.Boolean == _typeCode) {
                    return _value._boolean;
                }

                throw new InvalidTypeException(ETypeCode.Boolean, _typeCode);
            }
        }

        public Byte Byte {
            get {
                if (ETypeCode.Byte == _typeCode) {
                    return _value._byte;
                }
                throw new InvalidTypeException(ETypeCode.Byte, _typeCode);
            }
        }

        public Char Char {
            get {
                if (ETypeCode.Char == _typeCode) {
                    return _value._char;
                }

                throw new InvalidTypeException(ETypeCode.Char, _typeCode);
            }
        }

        public DateTime DateTime {
            get {
                if (ETypeCode.DateTime == _typeCode) {
                    return new DateTime(_value._int64);
                }
                if (ETypeCode.Date == _typeCode) {
                    return new DateTime(_value._int64).Date;
                }
                throw new InvalidTypeException(ETypeCode.DateTime, _typeCode);
            }
        }

        public Decimal Decimal {
            get {
                if (ETypeCode.Decimal == _typeCode) {
                    if (_value._numericInfo.Data4 != 0 && _value._numericInfo.Scale > 28) {
                        throw new OverflowException();
                    }
                    return new Decimal(_value._numericInfo.Data1, _value._numericInfo.Data2, _value._numericInfo.Data3, !_value._numericInfo.Positive, _value._numericInfo.Scale);
                }
                throw new InvalidTypeException(ETypeCode.Decimal, _typeCode);
            }
        }

        public Double Double {
            get {
                if (ETypeCode.Double == _typeCode) {
                    return _value._double;
                }
                throw new InvalidTypeException(ETypeCode.Double, _typeCode);
            }
        }
        
        public Int32 Enum {
            get {
                if (ETypeCode.Enum == _typeCode) {
                    return _value._int32;
                }
                throw new InvalidTypeException(ETypeCode.Enum, _typeCode);
            }
        }

        public Int16 Int16 {
            get {
                if (ETypeCode.Int16 == _typeCode) {
                    return _value._int16;
                }
                throw new InvalidTypeException(ETypeCode.Int16, _typeCode);
            }
        }

        public Int32 Int32 {
            get {
                if (ETypeCode.Int32 == _typeCode) {
                    return _value._int32;
                }
                throw new InvalidTypeException(ETypeCode.Int32, _typeCode);
            }
        }

        public Int64 Int64 {
            get {
                if (ETypeCode.Int64 == _typeCode) {
                    return _value._int64;
                }
                throw new InvalidTypeException(ETypeCode.Int64, _typeCode);
            }
        }

        public Single Single {
            get {
                if (ETypeCode.Single == _typeCode) {
                    return _value._single;
                }
                throw new InvalidTypeException(ETypeCode.Single, _typeCode);
            }
        }
        public SByte Sbyte {
            get {
                if (ETypeCode.SByte == _typeCode)
                {
                    return _value._sbyte;
                }
                throw new InvalidTypeException(ETypeCode.SByte, _typeCode);
            }
        }

        public TimeSpan Time {
            get {
                if (ETypeCode.Time == _typeCode) {
                    return new TimeSpan(_value._int64);
                }

                throw new InvalidTypeException(ETypeCode.Time, _typeCode);
            }
        }
        
        public UInt16 UInt16 {
            get {
                if (ETypeCode.UInt16 == _typeCode) {
                    return _value._uint16;
                }
                throw new InvalidTypeException(ETypeCode.UInt16, _typeCode);
            }
        }

        public UInt32 UInt32 {
            get {
                if (ETypeCode.UInt32 == _typeCode) {
                    return _value._uint32;
                }
                throw new InvalidTypeException(ETypeCode.UInt32, _typeCode);
            }
        }

        public UInt64 UInt64 {
            get {
                if (ETypeCode.UInt64 == _typeCode) {
                    return _value._uint64;
                }
                throw new InvalidTypeException(ETypeCode.UInt64, _typeCode);
            }
        }

        public byte Scale
        {
            get
            {
                if (_typeCode == ETypeCode.Decimal)
                {
                    return _value._numericInfo.Scale;
                }
                
                throw new DataException("Scale is only available for decimal types.");
            }
        }
        
        public byte Precision
        {
            get
            {
                if (_typeCode == ETypeCode.Decimal)
                {
                    return _value._numericInfo.Precision;
                }
                
                throw new DataException("Precision is only available for decimal types.");
            }
        }
        
        
        #endregion
        
        #region Operators

        public override bool Equals(object obj)
        {
            return ((DataValue) obj) == this;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public static bool operator ==(DataValue a, DataValue b)
        {
            if (a._typeCode == b._typeCode)
            {
                return a._typeCode switch
                {
                    ETypeCode.Unknown => false,
                    // ETypeCode.Binary => a.Binary.SequenceEqual(b.Binary),
                    ETypeCode.Byte => (a.Byte == b.Byte),
                    ETypeCode.Char => (a.Char == b.Char),
                    ETypeCode.SByte => (a.Sbyte == b.Sbyte),
                    ETypeCode.UInt16 => (a.UInt16 == b.UInt16),
                    ETypeCode.UInt32 => (a.UInt32 == b.UInt32),
                    ETypeCode.UInt64 => (a.UInt64 == b.UInt64),
                    ETypeCode.Int16 => (a.Int16 == b.Int16),
                    ETypeCode.Int32 => (a.Int32 == b.Int32),
                    ETypeCode.Int64 => (a.Int64 == b.Int64),
                    ETypeCode.Decimal => (a.Decimal == b.Decimal),
                    ETypeCode.Double => (Math.Abs(a.Double - b.Double) < 0.00001),
                    ETypeCode.Single => (Math.Abs(a.Single - b.Single) < 0.00001),
                    ETypeCode.Boolean => (a.Boolean == b.Boolean),
                    ETypeCode.DateTime => (a._value._int64 == b._value._int64),
                    ETypeCode.Date => (a._value._int64 == b._value._int64),
                    ETypeCode.Time => (a._value._int64 == b._value._int64),
                    ETypeCode.Json => throw new CompareTypeException(ETypeCode.Json),
                    ETypeCode.Xml => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.Enum => (a._value._int32 == b._value._int32),
                    ETypeCode.Object => throw new CompareTypeException(ETypeCode.Object),
                    ETypeCode.Node => throw new CompareTypeException(ETypeCode.Node),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            
           return Operations.Equal(DataType.BestCompareType(a._typeCode, b._typeCode), a.Value, b.Value);
        }
        
        
        public static bool operator !=(DataValue a, DataValue b)
        {
            return !(a == b);
        }
        
        public static bool operator <(DataValue a, DataValue b)
        {
            if (a._typeCode == b._typeCode)
            {
                return a._typeCode switch
                {
                    ETypeCode.Unknown => false,
                    //ETypeCode.Binary => Operations.LessThan(a.Binary, b.Binary),
                    ETypeCode.Byte => (a.Byte < b.Byte),
                    ETypeCode.Char => (a.Char < b.Char),
                    ETypeCode.SByte => (a.Sbyte < b.Sbyte),
                    ETypeCode.UInt16 => (a.UInt16 < b.UInt16),
                    ETypeCode.UInt32 => (a.UInt32 < b.UInt32),
                    ETypeCode.UInt64 => (a.UInt64 < b.UInt64),
                    ETypeCode.Int16 => (a.Int16 < b.Int16),
                    ETypeCode.Int32 => (a.Int32 < b.Int32),
                    ETypeCode.Int64 => (a.Int64 < b.Int64),
                    ETypeCode.Decimal => (a.Decimal < b.Decimal),
                    ETypeCode.Double => (a.Double < b.Double),
                    ETypeCode.Single => (a.Single < b.Single),
                    // ETypeCode.String => Operations.LessThan(a.String, b.String),
                    // ETypeCode.Text => Operations.LessThan(a.Text, b.Text),
                    ETypeCode.Boolean => Operations.LessThan(a.Boolean, b.Boolean),
                    ETypeCode.DateTime => (a._value._int64 < b._value._int64),
                    ETypeCode.Date => (a._value._int64 < b._value._int64),
                    ETypeCode.Time => (a._value._int64 < b._value._int64),
                    // ETypeCode.Guid => Operations.LessThan(a.Guid, b.Guid),
                    ETypeCode.Json => throw new CompareTypeException(ETypeCode.Json),
                    ETypeCode.Xml => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.Enum => (a._value._int32 == b._value._int32),
                    //ETypeCode.CharArray => Operations.LessThan(a.CharArray, b.CharArray),
                    ETypeCode.Object => throw new CompareTypeException(ETypeCode.Object),
                    ETypeCode.Node => throw new CompareTypeException(ETypeCode.Node),
                    //ETypeCode.Geometry => Operations.LessThan(a.Geometry, b.Geometry),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            
            return Operations.LessThan(DataType.BestCompareType(a._typeCode, b._typeCode), a.Value, b.Value);
        }

        public static bool operator >(DataValue a, DataValue b)
        {
            if (a._typeCode == b._typeCode)
            {
                return a._typeCode switch
                {
                    ETypeCode.Unknown => false,
                    //ETypeCode.Binary => Operations.GreaterThan(a.Binary, b.Binary),
                    ETypeCode.Byte => (a.Byte > b.Byte),
                    ETypeCode.Char => (a.Char > b.Char),
                    ETypeCode.SByte => (a.Sbyte > b.Sbyte),
                    ETypeCode.UInt16 => (a.UInt16 > b.UInt16),
                    ETypeCode.UInt32 => (a.UInt32 > b.UInt32),
                    ETypeCode.UInt64 => (a.UInt64 > b.UInt64),
                    ETypeCode.Int16 => (a.Int16 > b.Int16),
                    ETypeCode.Int32 => (a.Int32 > b.Int32),
                    ETypeCode.Int64 => (a.Int64 > b.Int64),
                    ETypeCode.Decimal => (a.Decimal > b.Decimal),
                    ETypeCode.Double => (a.Double > b.Double),
                    ETypeCode.Single => (a.Single > b.Single),
                    // ETypeCode.String => Operations.GreaterThan(a.String, b.String),
                    // ETypeCode.Text => Operations.GreaterThan(a.Text, b.Text),
                    ETypeCode.Boolean => Operations.GreaterThan(a.Boolean, b.Boolean),
                    ETypeCode.DateTime => (a._value._int64 > b._value._int64),
                    ETypeCode.Date => (a._value._int64 > b._value._int64),
                    ETypeCode.Time => (a._value._int64 > b._value._int64),
                    // ETypeCode.Guid => Operations.GreaterThan(a.Guid, b.Guid),
                    ETypeCode.Json => throw new CompareTypeException(ETypeCode.Json),
                    ETypeCode.Xml => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.Enum => (a._value._int32 == b._value._int32),
                    //ETypeCode.CharArray => Operations.GreaterThan(a.CharArray, b.CharArray),
                    ETypeCode.Object => throw new CompareTypeException(ETypeCode.Object),
                    ETypeCode.Node => throw new CompareTypeException(ETypeCode.Node),
                    //ETypeCode.Geometry => Operations.GreaterThan(a.Geometry, b.Geometry),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            
            return Operations.GreaterThan(DataType.BestCompareType(a._typeCode, b._typeCode), a.Value, b.Value);
        }
        
        public static bool operator <=(DataValue a, DataValue b)
        {
            if (a._typeCode == b._typeCode)
            {
                return a._typeCode switch
                {
                    ETypeCode.Unknown => false,
                    //ETypeCode.Binary => Operations.LessThanOrEqual(a.Binary, b.Binary),
                    ETypeCode.Byte => (a.Byte <= b.Byte),
                    ETypeCode.Char => (a.Char <= b.Char),
                    ETypeCode.SByte => (a.Sbyte <= b.Sbyte),
                    ETypeCode.UInt16 => (a.UInt16 <= b.UInt16),
                    ETypeCode.UInt32 => (a.UInt32 <= b.UInt32),
                    ETypeCode.UInt64 => (a.UInt64 <= b.UInt64),
                    ETypeCode.Int16 => (a.Int16 <= b.Int16),
                    ETypeCode.Int32 => (a.Int32 <= b.Int32),
                    ETypeCode.Int64 => (a.Int64 <= b.Int64),
                    ETypeCode.Decimal => (a.Decimal <= b.Decimal),
                    ETypeCode.Double => (a.Double <= b.Double),
                    ETypeCode.Single => (a.Single <= b.Single),
                    // ETypeCode.String => Operations.LessThanOrEqual(a.String, b.String),
                    // ETypeCode.Text => Operations.LessThanOrEqual(a.Text, b.Text),
                    ETypeCode.Boolean => Operations.LessThanOrEqual(a.Boolean, b.Boolean),
                    ETypeCode.DateTime => (a._value._int64 <= b._value._int64),
                    ETypeCode.Date => (a._value._int64 <= b._value._int64),
                    ETypeCode.Time => (a._value._int64 <= b._value._int64),
                    // ETypeCode.Guid => Operations.LessThanOrEqual(a.Guid, b.Guid),
                    ETypeCode.Json => throw new CompareTypeException(ETypeCode.Json),
                    ETypeCode.Xml => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.Enum => (a._value._int32 == b._value._int32),
                    //ETypeCode.CharArray => Operations.LessThanOrEqual(a.CharArray, b.CharArray),
                    ETypeCode.Object => throw new CompareTypeException(ETypeCode.Object),
                    ETypeCode.Node => throw new CompareTypeException(ETypeCode.Node),
                    //ETypeCode.Geometry => Operations.LessThanOrEqual(a.Geometry, b.Geometry),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            
            return Operations.LessThanOrEqual(DataType.BestCompareType(a._typeCode, b._typeCode), a.Value, b.Value);
        }
            
        public static bool operator >=(DataValue a, DataValue b)
        {
            if (a._typeCode == b._typeCode)
            {
                return a._typeCode switch
                {
                    ETypeCode.Unknown => false,
                    //ETypeCode.Binary => Operations.GreaterThanOrEqual(a.Binary, b.Binary),
                    ETypeCode.Byte => (a.Byte >= b.Byte),
                    ETypeCode.Char => (a.Char >= b.Char),
                    ETypeCode.SByte => (a.Sbyte >= b.Sbyte),
                    ETypeCode.UInt16 => (a.UInt16 >= b.UInt16),
                    ETypeCode.UInt32 => (a.UInt32 >= b.UInt32),
                    ETypeCode.UInt64 => (a.UInt64 >= b.UInt64),
                    ETypeCode.Int16 => (a.Int16 >= b.Int16),
                    ETypeCode.Int32 => (a.Int32 >= b.Int32),
                    ETypeCode.Int64 => (a.Int64 >= b.Int64),
                    ETypeCode.Decimal => (a.Decimal >= b.Decimal),
                    ETypeCode.Double => (a.Double >= b.Double),
                    ETypeCode.Single => (a.Single >= b.Single),
                    // ETypeCode.String => Operations.GreaterThanOrEqual(a.String, b.String),
                    // ETypeCode.Text => Operations.GreaterThanOrEqual(a.Text, b.Text),
                    ETypeCode.Boolean => Operations.GreaterThanOrEqual(a.Boolean, b.Boolean),
                    ETypeCode.DateTime => (a._value._int64 >= b._value._int64),
                    ETypeCode.Date => (a._value._int64 >= b._value._int64),
                    ETypeCode.Time => (a._value._int64 >= b._value._int64),
                    // ETypeCode.Guid => Operations.GreaterThanOrEqual(a.Guid, b.Guid),
                    ETypeCode.Json => throw new CompareTypeException(ETypeCode.Json),
                    ETypeCode.Xml => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.Enum => (a._value._int32 == b._value._int32),
                    //ETypeCode.CharArray => Operations.GreaterThanOrEqual(a.CharArray, b.CharArray),
                    ETypeCode.Object => throw new CompareTypeException(ETypeCode.Object),
                    ETypeCode.Node => throw new CompareTypeException(ETypeCode.Node),
                    //ETypeCode.Geometry => Operations.GreaterThanOrEqual(a.Geometry, b.Geometry),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            
            return Operations.GreaterThanOrEqual(DataType.BestCompareType(a._typeCode, b._typeCode), a.Value, b.Value);
        }
        
        public static DataValue operator +(DataValue a, DataValue b)
        {
            if (a._typeCode == b._typeCode)
            {
                return a._typeCode switch
                {
                    ETypeCode.Byte => new DataValue(a.Byte + b.Byte),
                    ETypeCode.Char => new DataValue( a.Char + b.Char),
                    ETypeCode.SByte => new DataValue( a.Sbyte + b.Sbyte),
                    ETypeCode.UInt16 => new DataValue( a.UInt16 + b.UInt16),
                    ETypeCode.UInt32 => new DataValue( a.UInt32 + b.UInt32),
                    ETypeCode.UInt64 => new DataValue( a.UInt64 + b.UInt64),
                    ETypeCode.Int16 => new DataValue( a.Int16 + b.Int16),
                    ETypeCode.Int32 => new DataValue( a.Int32 + b.Int32),
                    ETypeCode.Int64 => new DataValue( a.Int64 + b.Int64),
                    ETypeCode.Decimal => new DataValue(a.Decimal + b.Decimal),
                    ETypeCode.Double => new DataValue(a.Double + b.Double),
                    ETypeCode.Single => new DataValue( a.Single + b.Single),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            var type = DataType.BestCompareType(a._typeCode, b._typeCode);
            return new DataValue(Operations.Add(type, a.Value, b.Value), type);
        }
        
        public object Add(DataValue a)
        {
            if (a._typeCode == _typeCode)
            {
                return a._typeCode switch
                {
                    ETypeCode.Byte => a.Byte + Byte,
                    ETypeCode.Char =>  a.Char + Char,
                    ETypeCode.SByte =>  a.Sbyte + Sbyte,
                    ETypeCode.UInt16 =>  a.UInt16 + UInt16,
                    ETypeCode.UInt32 =>  a.UInt32 + UInt32,
                    ETypeCode.UInt64 =>  a.UInt64 + UInt64,
                    ETypeCode.Int16 =>  a.Int16 + Int16,
                    ETypeCode.Int32 =>  a.Int32 + Int32,
                    ETypeCode.Int64 =>  a.Int64 + Int64,
                    ETypeCode.Decimal => a.Decimal + Decimal,
                    ETypeCode.Double => a.Double + Double,
                    ETypeCode.Single =>  a.Single + Single,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            var type = DataType.BestCompareType(a._typeCode, _typeCode);
            return Operations.Add(type, a.Value, Value);
        }
        
        public static DataValue operator -(DataValue a, DataValue b)
        {
            if (a._typeCode == b._typeCode)
            {
                return a._typeCode switch
                {
                    ETypeCode.Byte => new DataValue(a.Byte - b.Byte),
                    ETypeCode.Char => new DataValue(a.Char - b.Char),
                    ETypeCode.SByte => new DataValue(a.Sbyte - b.Sbyte),
                    ETypeCode.UInt16 => new DataValue(a.UInt16 - b.UInt16),
                    ETypeCode.UInt32 => new DataValue(a.UInt32 - b.UInt32),
                    ETypeCode.UInt64 => new DataValue(a.UInt64 - b.UInt64),
                    ETypeCode.Int16 => new DataValue(a.Int16 - b.Int16),
                    ETypeCode.Int32 => new DataValue(a.Int32 - b.Int32),
                    ETypeCode.Int64 => new DataValue(a.Int64 - b.Int64),
                    ETypeCode.Decimal => new DataValue(a.Decimal - b.Decimal),
                    ETypeCode.Double => new DataValue(a.Double - b.Double),
                    ETypeCode.Single => new DataValue(a.Single - b.Single),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            var type = DataType.BestCompareType(a._typeCode, b._typeCode);
            return new DataValue(Operations.Subtract(type, a.Value, b.Value), type);
        }
        
        public object Subtract(DataValue a)
        {
            if (a._typeCode == _typeCode)
            {
                return a._typeCode switch
                {
                    ETypeCode.Byte => a.Byte - Byte,
                    ETypeCode.Char =>  a.Char - Char,
                    ETypeCode.SByte =>  a.Sbyte - Sbyte,
                    ETypeCode.UInt16 =>  a.UInt16 - UInt16,
                    ETypeCode.UInt32 =>  a.UInt32 - UInt32,
                    ETypeCode.UInt64 =>  a.UInt64 - UInt64,
                    ETypeCode.Int16 =>  a.Int16 - Int16,
                    ETypeCode.Int32 =>  a.Int32 - Int32,
                    ETypeCode.Int64 =>  a.Int64 - Int64,
                    ETypeCode.Decimal => a.Decimal - Decimal,
                    ETypeCode.Double => a.Double - Double,
                    ETypeCode.Single =>  a.Single - Single,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            var type = DataType.BestCompareType(a._typeCode, _typeCode);
            return Operations.Subtract(type, a.Value, Value);
        }
        
        public static DataValue operator *(DataValue a, DataValue b)
        {
            if (a._typeCode == b._typeCode)
            {
                return a._typeCode switch
                {
                    ETypeCode.Byte => new DataValue(a.Byte * b.Byte),
                    ETypeCode.Char => new DataValue(a.Char * b.Char),
                    ETypeCode.SByte => new DataValue(a.Sbyte * b.Sbyte),
                    ETypeCode.UInt16 => new DataValue(a.UInt16 * b.UInt16),
                    ETypeCode.UInt32 => new DataValue(a.UInt32 * b.UInt32),
                    ETypeCode.UInt64 => new DataValue(a.UInt64 * b.UInt64),
                    ETypeCode.Int16 => new DataValue(a.Int16 * b.Int16),
                    ETypeCode.Int32 => new DataValue(a.Int32 * b.Int32),
                    ETypeCode.Int64 => new DataValue(a.Int64 * b.Int64),
                    ETypeCode.Decimal => new DataValue(a.Decimal * b.Decimal),
                    ETypeCode.Double => new DataValue(a.Double * b.Double),
                    ETypeCode.Single => new DataValue(a.Single * b.Single),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            var type = DataType.BestCompareType(a._typeCode, b._typeCode);
            return new DataValue(Operations.Multiply(type, a.Value, b.Value), type);
        }
        
        public object Multiply(DataValue a)
        {
            if (a._typeCode == _typeCode)
            {
                return a._typeCode switch
                {
                    ETypeCode.Byte => a.Byte * Byte,
                    ETypeCode.Char =>  a.Char * Char,
                    ETypeCode.SByte =>  a.Sbyte * Sbyte,
                    ETypeCode.UInt16 =>  a.UInt16 * UInt16,
                    ETypeCode.UInt32 =>  a.UInt32 * UInt32,
                    ETypeCode.UInt64 =>  a.UInt64 * UInt64,
                    ETypeCode.Int16 =>  a.Int16 * Int16,
                    ETypeCode.Int32 =>  a.Int32 * Int32,
                    ETypeCode.Int64 =>  a.Int64 * Int64,
                    ETypeCode.Decimal => a.Decimal * Decimal,
                    ETypeCode.Double => a.Double * Double,
                    ETypeCode.Single =>  a.Single * Single,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            var type = DataType.BestCompareType(a._typeCode, _typeCode);
            return Operations.Multiply(type, a.Value, Value);
        }
        
        public static DataValue operator /(DataValue a, DataValue b)
        {
            if (a._typeCode == b._typeCode)
            {
                return a._typeCode switch
                {
                    ETypeCode.Byte => new DataValue(a.Byte / b.Byte),
                    ETypeCode.Char => new DataValue( a.Char / b.Char),
                    ETypeCode.SByte => new DataValue( a.Sbyte / b.Sbyte),
                    ETypeCode.UInt16 => new DataValue( a.UInt16 / b.UInt16),
                    ETypeCode.UInt32 => new DataValue( a.UInt32 / b.UInt32),
                    ETypeCode.UInt64 => new DataValue( a.UInt64 / b.UInt64),
                    ETypeCode.Int16 => new DataValue(a.Int16 / b.Int16),
                    ETypeCode.Int32 => new DataValue( a.Int32 / b.Int32),
                    ETypeCode.Int64 => new DataValue( a.Int64 / b.Int64),
                    ETypeCode.Decimal => new DataValue( a.Decimal / b.Decimal),
                    ETypeCode.Double => new DataValue( a.Double / b.Double),
                    ETypeCode.Single => new DataValue( a.Single / b.Single),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            var type = DataType.BestCompareType(a._typeCode, b._typeCode);
            return new DataValue(Operations.Divide(type, a.Value, b.Value), type);

        }
        
        public object Divide(DataValue a)
        {
            if (a._typeCode == _typeCode)
            {
                return a._typeCode switch
                {
                    ETypeCode.Byte => a.Byte / Byte,
                    ETypeCode.Char =>  a.Char / Char,
                    ETypeCode.SByte =>  a.Sbyte / Sbyte,
                    ETypeCode.UInt16 =>  a.UInt16 / UInt16,
                    ETypeCode.UInt32 =>  a.UInt32 / UInt32,
                    ETypeCode.UInt64 =>  a.UInt64 / UInt64,
                    ETypeCode.Int16 =>  a.Int16 / Int16,
                    ETypeCode.Int32 =>  a.Int32 / Int32,
                    ETypeCode.Int64 =>  a.Int64 / Int64,
                    ETypeCode.Decimal => a.Decimal / Decimal,
                    ETypeCode.Double => a.Double / Double,
                    ETypeCode.Single =>  a.Single / Single,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            var type = DataType.BestCompareType(a._typeCode, _typeCode);
            return Operations.Divide(type, a.Value, Value);
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

                switch (_typeCode) {
                    case ETypeCode.Unknown:           
                        return null;
                    case ETypeCode.Boolean:         
                        return Boolean;
                    case ETypeCode.Byte:            
                        return Byte;
                    case ETypeCode.DateTime:        
                        return DateTime;
                    case ETypeCode.Decimal:         
                        return Decimal;
                    case ETypeCode.Double:          
                        return Double;
                    case ETypeCode.Int16:           
                        return Int16;
                    case ETypeCode.Int32:           
                        return Int32;
                    case ETypeCode.Int64:           
                        return Int64;
                    case ETypeCode.Single:          
                        return Single;
                    case ETypeCode.Time:            
                        return Time;
                    case ETypeCode.Char:
                        return Char;
                    case ETypeCode.SByte:
                        return Sbyte;
                    case ETypeCode.UInt16:
                        return UInt16;
                    case ETypeCode.UInt32:
                        return UInt32;
                    case ETypeCode.UInt64:
                        return UInt64;
                    case ETypeCode.Enum:
                        return Int32;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
  
        public ETypeCode GetTypeCode()
        {
            return _typeCode;
        }

        public Type GetItemType ()
        {
            return DataType.GetType(_typeCode);
        }
        
        public bool IsDiscrete()
        {
            return DataType.IsDiscrete(_typeCode);
        }

        public bool IsDecimal()
        {
            return DataType.IsDecimal(_typeCode);
        }

        public bool IsNumber()
        {
            return DataType.IsNumber(_typeCode);
        }

    }
}