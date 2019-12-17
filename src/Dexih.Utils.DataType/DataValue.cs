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
    public class DataObject
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

        private readonly bool         _isNull;
        private readonly bool         _isArray;
        private readonly ETypeCode  _typeCode;
        private readonly Storage      _value;
        private readonly object _object;
        
        #endregion
        
        #region Initializers

        public DataObject(ReadOnlyMemory<byte> value)
        {
            _typeCode = ETypeCode.Binary;
            var reference = DataObjectMemory.Add(value);
            _value = new Storage(reference);
            _isArray = false;
            _isNull = false;
        }

        // public DataObject(string value)
        // {
        //     _typeCode = ETypeCode.String;
        //     if (value is null)
        //     {
        //         _isNull = true;
        //         _value = new Storage();
        //         // _object = new ObjectStorage();
        //     }
        //     else
        //     {
        //         _isNull = false;
        //         // _object = new ObjectStorage(value.AsMemory());
        //         _value = new Storage();
        //     }
        //     _isArray = false;
        // }

        public DataObject(bool value)
        {
            _isNull = false;
            _typeCode = ETypeCode.Boolean;
            _value = new Storage(value);
            // _object = new ObjectStorage();
            _isArray = false;
        }
        
        public DataObject(byte value)
        {
            _isNull = false;
            _typeCode = ETypeCode.Byte;
            _value = new Storage(value);
            // _object = new ObjectStorage();
            _isArray = false;
        }
        public DataObject(char value)
        {
            _isNull = false;
            _typeCode = ETypeCode.Char;
            _value = new Storage(value);
            // _object = new ObjectStorage();
            _isArray = false;
        }
        public DataObject(decimal value)
        {
            _isNull = false;
            _typeCode = ETypeCode.Decimal;
            _value = new Storage(value);
            // _object = new ObjectStorage();
            _isArray = false;
        }
        public DataObject(double value)
        {
            _isNull = false;
            _typeCode = ETypeCode.Double;
            _value = new Storage(value);
            // _object = new ObjectStorage();
            _isArray = false;
        }
        
        public DataObject(Guid value)
        {
            _isNull = false;
            _typeCode = ETypeCode.Guid;
            _value = new Storage();
            // _object = new ObjectStorage(value.ToByteArray());
            _isArray = false;
        }

        public DataObject(Int16 value)
        {
            _isNull = false;
            _typeCode = ETypeCode.Int16;
            _value = new Storage(value);
            // _object = new ObjectStorage();
            _isArray = false;
        }
        public DataObject(Int32 value)
        {
            _isNull = false;
            _typeCode = ETypeCode.Int32;
            _value = new Storage(value);
            // _object = new ObjectStorage();
            _isArray = false;
        }
        public DataObject(Int64 value)
        {
            _isNull = false;
            _typeCode = ETypeCode.Int64;
            _value = new Storage(value);
            // _object = new ObjectStorage();
            _isArray = false;
        }
        public DataObject(Single value)
        {
            _isNull = false;
            _typeCode = ETypeCode.Single;
            _value = new Storage(value);
            // _object = new ObjectStorage();
            _isArray = false;
        }
        public DataObject(ReadOnlyMemory<char> value)
        {
            _isNull = false;
            _typeCode = ETypeCode.String;
            // _object = new ObjectStorage(value);
            _value = new Storage();
            _isArray = false;
        }
        public DataObject(TimeSpan value)
        {
            _isNull = false;
            _typeCode = ETypeCode.Time;
            _value = new Storage(value);
            // _object = new ObjectStorage();
            _isArray = false;
        }
        public DataObject(DateTime value)
        {
            _isNull = false;
            _typeCode = ETypeCode.DateTime;
            _value = new Storage(value);
            // _object = new ObjectStorage();
            _isArray = false;
        }
        public DataObject(sbyte value)
        {
            _isNull = false;
            _typeCode = ETypeCode.SByte;
            _value = new Storage(value);
            // _object = new ObjectStorage();
            _isArray = false;
        }
        public DataObject(UInt16 value)
        {
            _isNull = false;
            _typeCode = ETypeCode.UInt16;
            _value = new Storage(value);
            // _object = new ObjectStorage();
            _isArray = false;
        }
        public DataObject(UInt32 value)
        {
            _isNull = false;
            _typeCode = ETypeCode.UInt32;
            _value = new Storage(value);
            // _object = new ObjectStorage();
            _isArray = false;
        }
        public DataObject(UInt64 value)
        {
            _isNull = false;
            _typeCode = ETypeCode.UInt64;
            _value = new Storage(value);
            // _object = new ObjectStorage();
            _isArray = false;
        }
        
     
        public DataObject(object value, ETypeCode typeCode, int rank = 0)
        {
          _typeCode = typeCode;

            if (rank > 0)
            {
                _isArray = true;
                _isNull = value is null;
                _value = new Storage(rank);
                // _object = new ObjectStorage(value);
                return;
            }

            _isArray = false;

            if (value is null)
            {
                _isNull = true;
                _value = new Storage();
                // _object = new ObjectStorage();
                return;
            }

            _isNull = false;

            switch (_typeCode)
            {
                case ETypeCode.Binary:
                    // _object = new ObjectStorage((byte[])value);
                    _value = new Storage();
                    break;
                case ETypeCode.Byte:
                    _value = new Storage((byte)value);
                    // _object = new ObjectStorage();
                    break;
                case ETypeCode.Char:
                    _value = new Storage((char)value);
                    // _object = new ObjectStorage();
                    break;
                case ETypeCode.SByte:
                    _value = new Storage((sbyte)value);
                    // _object = new ObjectStorage();
                    break;
                case ETypeCode.UInt16:
                    _value = new Storage((UInt16)value);
                    // _object = new ObjectStorage();
                    break;
                case ETypeCode.UInt32:
                    _value = new Storage((UInt32)value);
                    // _object = new ObjectStorage();
                    break;
                case ETypeCode.UInt64:
                    _value = new Storage((UInt64)value);
                    // _object = new ObjectStorage();
                    break;
                case ETypeCode.Int16:
                    _value = new Storage((Int16)value);
                    // _object = new ObjectStorage();
                    break;
                case ETypeCode.Int32:
                    _value = new Storage((Int32)value);
                    // _object = new ObjectStorage();
                    break;
                case ETypeCode.Int64:
                    _value = new Storage((Int64)value);
                    // _object = new ObjectStorage();
                    break;
                case ETypeCode.Decimal:
                    _value = new Storage((Decimal)value);
                    // _object = new ObjectStorage();
                    break;
                case ETypeCode.Double:
                    _value = new Storage((Double)value);
                    // _object = new ObjectStorage();
                    break;
                case ETypeCode.Single:
                    _value = new Storage((Single)value);
                    // _object = new ObjectStorage();
                    break;
                case ETypeCode.String:
                case ETypeCode.Text:
                    // _object = new ObjectStorage((string)value);
                    _value = new Storage();
                    break;
                case ETypeCode.CharArray:
                    // _object = new ObjectStorage((char[])value);
                    _value = new Storage();
                    break;
                case ETypeCode.Boolean:
                    _value = new Storage((bool)value);
                    // _object = new ObjectStorage();
                    break;
                case ETypeCode.DateTime:
                    _value = new Storage((DateTime)value);
                    // _object = new ObjectStorage();
                    break;
                case ETypeCode.Time:
                    _value = new Storage((TimeSpan)value);
                    // _object = new ObjectStorage();
                    break;
                case ETypeCode.Guid:
                    // _object = new ObjectStorage((Guid)value);
                    _value = new Storage();
                    break;
                case ETypeCode.Enum:
                    _value = new Storage((Int32)value);
                    // _object = new ObjectStorage();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }        
        }

        public DataObject(DataObject value) { // Clone
            // value types
            _isNull    = value._isNull;
            _isArray = value._isArray;
            _typeCode      = value._typeCode;
            _value     = value._value;
            // _object = value.// _object;
        }

        public DataObject(object value): this(value, DataType.GetTypeCode(value, out var rank), rank)
        {
        }
        
        ~DataObject()
        {
            switch (_typeCode)
            {
                case ETypeCode.Binary:
                case ETypeCode.Geometry:
                case ETypeCode.Guid:
                case ETypeCode.Json:
                case ETypeCode.Node:
                case ETypeCode.Object:
                case ETypeCode.String:
                case ETypeCode.Text:
                case ETypeCode.Unknown:
                case ETypeCode.Xml:
                case ETypeCode.CharArray:
                    DataObjectMemory.Remove(_value._int64);
                    break;
            }
        }
        

     #endregion

        #region Properties
     
        public bool IsEmpty => (ETypeCode.Unknown == _typeCode);

        public bool IsNull => _isNull;

        public bool IsArray => _isArray;

        public ETypeCode TypeCode => _typeCode;

        public Boolean Boolean {
            get {
                ThrowIfNull();

                if (ETypeCode.Boolean == _typeCode) {
                    return _value._boolean;
                }

                throw new InvalidTypeException(ETypeCode.Boolean, _typeCode);
            }
        }

        public Byte Byte {
            get {
                ThrowIfNull();

                if (ETypeCode.Byte == _typeCode) {
                    return _value._byte;
                }
                throw new InvalidTypeException(ETypeCode.Byte, _typeCode);
            }
        }

        // public byte[] Binary {
        //     get {
        //         if (ETypeCode.Binary == _typeCode)
        //         {
        //             return // _object._binary.ToArray();
        //         }
        //         
        //         throw new InvalidTypeException(ETypeCode.Binary, _typeCode);
        //     }
        // }

        public Char Char {
            get {
                ThrowIfNull();

                if (ETypeCode.Char == _typeCode) {
                    return _value._char;
                }

                throw new InvalidTypeException(ETypeCode.Char, _typeCode);
            }
        }

        // public Char[] CharArray {
        //     get
        //     {
        //         if (ETypeCode.CharArray == _typeCode)
        //         {
        //             return // _object._string.ToArray();
        //         }
        //         throw new InvalidTypeException(ETypeCode.CharArray, _typeCode);
        //     }
        // }
        
        public DateTime DateTime {
            get {
                ThrowIfNull();

                if (ETypeCode.DateTime == _typeCode) {
                    return new DateTime(_value._int64);
                }
                throw new InvalidTypeException(ETypeCode.DateTime, _typeCode);
            }
        }

        public Decimal Decimal {
            get {
                ThrowIfNull();

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
                ThrowIfNull();

                if (ETypeCode.Double == _typeCode) {
                    return _value._double;
                }
                throw new InvalidTypeException(ETypeCode.Double, _typeCode);
            }
        }
        
        public Int32 Enum {
            get {
                ThrowIfNull();

                if (ETypeCode.Enum == _typeCode) {
                    return _value._int32;
                }
                throw new InvalidTypeException(ETypeCode.Enum, _typeCode);
            }
        }
        
        // public Geometry Geometry {
        //     get {
        //         if (ETypeCode.Geometry == _typeCode) {
        //             return (Geometry) // _object;
        //         }
        //         throw new InvalidTypeException(ETypeCode.Geometry, _typeCode);
        //     }
        // }

        // public Guid Guid {
        //     get {
        //         // 
        //         ThrowIfNull();
        //
        //         if (ETypeCode.Guid == _typeCode)
        //         {
        //             return new Guid(// _object._binary.ToArray());
        //         }
        //         
        //         throw new InvalidTypeException(ETypeCode.Guid, _typeCode);
        //     }
        // }

        public Int16 Int16 {
            get {
                ThrowIfNull();

                if (ETypeCode.Int16 == _typeCode) {
                    return _value._int16;
                }
                throw new InvalidTypeException(ETypeCode.Int16, _typeCode);
            }
        }

        public Int32 Int32 {
            get {
                // ThrowIfNull();
                //
                // if (ETypeCode.Int32 == _typeCode) {
                    return _value._int32;
                // }
                // throw new InvalidTypeException(ETypeCode.Int32, _typeCode);
            }
        }

        public Int64 Int64 {
            get {
                ThrowIfNull();

                if (ETypeCode.Int64 == _typeCode) {
                    return _value._int64;
                }
                throw new InvalidTypeException(ETypeCode.Int64, _typeCode);
            }
        }

        // public JsonElement Json {
        //     get {
        //         ThrowIfNull();
        //         
        //         if (ETypeCode.Json == _typeCode) {
        //             return (JsonElement) // _object;
        //         }
        //         throw new InvalidTypeException(ETypeCode.Json, _typeCode);
        //     }
        // }
        
        // public JsonElement Node {
        //     get {
        //         ThrowIfNull();
        //         
        //         if (ETypeCode.Node == _typeCode) {
        //             return (JsonElement) // _object;
        //         }
        //         throw new InvalidTypeException(ETypeCode.Node, _typeCode);
        //     }
        // }
        
        // public object Object {
        //     get {
        //         if (ETypeCode.Object == _typeCode) {
        //             return // _object;
        //         }
        //         throw new InvalidTypeException(ETypeCode.Object, _typeCode);
        //     }
        // }
        
        public Single Single {
            get {
                ThrowIfNull();

                if (ETypeCode.Single == _typeCode) {
                    return _value._single;
                }
                throw new InvalidTypeException(ETypeCode.Single, _typeCode);
            }
        }

        // public String String {
        //     get {
        //         if (ETypeCode.String == _typeCode) {
        //             return // _object._string.ToString();
        //         }
        //         throw new InvalidTypeException(ETypeCode.String, _typeCode);
        //     }
        // }

        public SByte Sbyte {
            get {
                ThrowIfNull();

                if (ETypeCode.SByte == _typeCode)
                {
                    return _value._sbyte;
                }
                throw new InvalidTypeException(ETypeCode.SByte, _typeCode);
            }
        }
        
        // public String Text {
        //     get {
        //         if (ETypeCode.Text == _typeCode) {
        //             return // _object._string.ToString();
        //         }
        //         throw new InvalidTypeException(ETypeCode.Text, _typeCode);
        //     }
        // }
        
        public TimeSpan Time {
            get {
                ThrowIfNull();

                if (ETypeCode.Time == _typeCode) {
                    return new TimeSpan(_value._int64);
                }

                throw new InvalidTypeException(ETypeCode.Time, _typeCode);
            }
        }
        
        public UInt16 UInt16 {
            get {
                ThrowIfNull();

                if (ETypeCode.UInt16 == _typeCode) {
                    return _value._uint16;
                }
                throw new InvalidTypeException(ETypeCode.UInt16, _typeCode);
            }
        }

        public UInt32 UInt32 {
            get {
                ThrowIfNull();

                if (ETypeCode.UInt32 == _typeCode) {
                    return _value._uint32;
                }
                throw new InvalidTypeException(ETypeCode.UInt32, _typeCode);
            }
        }

        public UInt64 UInt64 {
            get {
                ThrowIfNull();

                if (ETypeCode.UInt64 == _typeCode) {
                    return _value._uint64;
                }
                throw new InvalidTypeException(ETypeCode.UInt64, _typeCode);
            }
        }
        
        // public XmlDocument Xml {
        //     get {
        //         ThrowIfNull();
        //         
        //         if (ETypeCode.Xml == _typeCode) {
        //             return (XmlDocument) // _object;
        //         }
        //         throw new InvalidTypeException(ETypeCode.Xml, _typeCode);
        //     }
        // }
        
        /// <summary>
        /// If array, the rank
        /// </summary>
        /// <exception cref="DataException"></exception>
        public int Rank {
            get {
                // 
                ThrowIfNull();

                if (IsArray)
                {
                    return _value._int32;
                }
                
                throw new DataException("Rank is only available for arrays.");
            }
        }

        public byte Scale
        {
            get
            {
                ThrowIfNull();

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
                ThrowIfNull();

                if (_typeCode == ETypeCode.Decimal)
                {
                    return _value._numericInfo.Precision;
                }
                
                throw new DataException("Precision is only available for decimal types.");
            }
        }
        
        public int MaxLength
        {
            get
            {
                ThrowIfNull();

                if (_typeCode == ETypeCode.String)
                {
                    return _value._int32;
                }
                
                throw new DataException("Maxlength is only available for string types.");
            }
        }
        
        #endregion
        
        #region Operators

        public override bool Equals(object obj)
        {
            return ((DataObject) obj) == this;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public static bool operator ==(DataObject a, DataObject b)
        {
            // if (a == b)
            // {
            //     return true;
            // }
            // if (a.IsNull || b.IsNull)
            // {
            //     return false;
            // }
            //
            // if (a.IsArray)
            // {
            //     throw new DataException("Cannot compare array types.");
            // }
            //
            // if (a._typeCode == b._typeCode)
            // {
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
                    // ETypeCode.String => (a.String == b.String),
                    // ETypeCode.Text => (a.Text == b.Text),
                    ETypeCode.Boolean => (a.Boolean == b.Boolean),
                    ETypeCode.DateTime => (a._value._int64 == b._value._int64),
                    ETypeCode.Time => (a._value._int64 == b._value._int64),
                    // ETypeCode.Guid => (a.Guid == b.Guid),
                    ETypeCode.Json => throw new CompareTypeException(ETypeCode.Json),
                    ETypeCode.Xml => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.Enum => (a._value._int32 == b._value._int32),
                    //ETypeCode.CharArray => a.CharArray.SequenceEqual(b.CharArray),
                    ETypeCode.Object => throw new CompareTypeException(ETypeCode.Object),
                    ETypeCode.Node => throw new CompareTypeException(ETypeCode.Node),
                    // ETypeCode.Geometry => Operations.Equal(a.Geometry, b.Geometry),
                    _ => throw new ArgumentOutOfRangeException()
                };
            // }
            
           //  return Operations.Equal(DataType.BestCompareType(a._typeCode, b._typeCode), a.Value, b.Value);

        }
        
        
        public static bool operator !=(DataObject a, DataObject b)
        {
            return !(a == b);
        }
        
        public static bool operator <(DataObject a, DataObject b)
        {
            if (a.IsArray)
            {
                throw new DataException("Cannot compare array types.");
            }

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

        public static bool operator >(DataObject a, DataObject b)
        {
            if (a.IsArray)
            {
                throw new DataException("Cannot compare array types.");
            }

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
        
        public static bool operator <=(DataObject a, DataObject b)
        {
            if (a.IsArray)
            {
                throw new DataException("Cannot compare array types.");
            }

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
            
        public static bool operator >=(DataObject a, DataObject b)
        {
            if (a.IsArray)
            {
                throw new DataException("Cannot compare array types.");
            }

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
                
        public static DataObject operator +(DataObject a, DataObject b)
        {
            if (a.IsArray)
            {
                throw new DataException("Cannot add array types.");
            }
            
            if (a._typeCode == b._typeCode)
            {
                return a._typeCode switch
                {
                    ETypeCode.Byte => new DataObject(a.Byte + b.Byte),
                    ETypeCode.Char => new DataObject( a.Char + b.Char),
                    ETypeCode.SByte => new DataObject( a.Sbyte + b.Sbyte),
                    ETypeCode.UInt16 => new DataObject( a.UInt16 + b.UInt16),
                    ETypeCode.UInt32 => new DataObject( a.UInt32 + b.UInt32),
                    ETypeCode.UInt64 => new DataObject( a.UInt64 + b.UInt64),
                    ETypeCode.Int16 => new DataObject( a.Int16 + b.Int16),
                    ETypeCode.Int32 => new DataObject( a.Int32 + b.Int32),
                    ETypeCode.Int64 => new DataObject( a.Int64 + b.Int64),
                    ETypeCode.Decimal => new DataObject(a.Decimal + b.Decimal),
                    ETypeCode.Double => new DataObject(a.Double + b.Double),
                    ETypeCode.Single => new DataObject( a.Single + b.Single),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            var type = DataType.BestCompareType(a._typeCode, b._typeCode);
            return new DataObject(Operations.Add(type, a.Value, b.Value), type, 0);
        }
        
        public static DataObject operator -(DataObject a, DataObject b)
        {
            if (a.IsArray)
            {
                throw new DataException("Cannot add array types.");
            }
            
            if (a._typeCode == b._typeCode)
            {
                return a._typeCode switch
                {
                    ETypeCode.Byte => new DataObject(a.Byte - b.Byte),
                    ETypeCode.Char => new DataObject(a.Char - b.Char),
                    ETypeCode.SByte => new DataObject(a.Sbyte - b.Sbyte),
                    ETypeCode.UInt16 => new DataObject(a.UInt16 - b.UInt16),
                    ETypeCode.UInt32 => new DataObject(a.UInt32 - b.UInt32),
                    ETypeCode.UInt64 => new DataObject(a.UInt64 - b.UInt64),
                    ETypeCode.Int16 => new DataObject(a.Int16 - b.Int16),
                    ETypeCode.Int32 => new DataObject(a.Int32 - b.Int32),
                    ETypeCode.Int64 => new DataObject(a.Int64 - b.Int64),
                    ETypeCode.Decimal => new DataObject(a.Decimal - b.Decimal),
                    ETypeCode.Double => new DataObject(a.Double - b.Double),
                    ETypeCode.Single => new DataObject(a.Single - b.Single),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            var type = DataType.BestCompareType(a._typeCode, b._typeCode);
            return new DataObject(Operations.Subtract(type, a.Value, b.Value), type, 0);
        }
        
        public static DataObject operator *(DataObject a, DataObject b)
        {
            if (a.IsArray)
            {
                throw new DataException("Cannot add array types.");
            }
            
            if (a._typeCode == b._typeCode)
            {
                return a._typeCode switch
                {
                    ETypeCode.Byte => new DataObject(a.Byte * b.Byte),
                    ETypeCode.Char => new DataObject(a.Char * b.Char),
                    ETypeCode.SByte => new DataObject(a.Sbyte * b.Sbyte),
                    ETypeCode.UInt16 => new DataObject(a.UInt16 * b.UInt16),
                    ETypeCode.UInt32 => new DataObject(a.UInt32 * b.UInt32),
                    ETypeCode.UInt64 => new DataObject(a.UInt64 * b.UInt64),
                    ETypeCode.Int16 => new DataObject(a.Int16 * b.Int16),
                    ETypeCode.Int32 => new DataObject(a.Int32 * b.Int32),
                    ETypeCode.Int64 => new DataObject(a.Int64 * b.Int64),
                    ETypeCode.Decimal => new DataObject(a.Decimal * b.Decimal),
                    ETypeCode.Double => new DataObject(a.Double * b.Double),
                    ETypeCode.Single => new DataObject(a.Single * b.Single),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            var type = DataType.BestCompareType(a._typeCode, b._typeCode);
            return new DataObject(Operations.Multiply(type, a.Value, b.Value), type, 0);
        }
        
        public static DataObject operator /(DataObject a, DataObject b)
        {
            if (a.IsArray)
            {
                throw new DataException("Cannot add array types.");
            }
            
            if (a._typeCode == b._typeCode)
            {
                return a._typeCode switch
                {
                    ETypeCode.Byte => new DataObject(a.Byte / b.Byte),
                    ETypeCode.Char => new DataObject( a.Char / b.Char),
                    ETypeCode.SByte => new DataObject( a.Sbyte / b.Sbyte),
                    ETypeCode.UInt16 => new DataObject( a.UInt16 / b.UInt16),
                    ETypeCode.UInt32 => new DataObject( a.UInt32 / b.UInt32),
                    ETypeCode.UInt64 => new DataObject( a.UInt64 / b.UInt64),
                    ETypeCode.Int16 => new DataObject(a.Int16 / b.Int16),
                    ETypeCode.Int32 => new DataObject( a.Int32 / b.Int32),
                    ETypeCode.Int64 => new DataObject( a.Int64 / b.Int64),
                    ETypeCode.Decimal => new DataObject( a.Decimal / b.Decimal),
                    ETypeCode.Double => new DataObject( a.Double / b.Double),
                    ETypeCode.Single => new DataObject( a.Single / b.Single),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            var type = DataType.BestCompareType(a._typeCode, b._typeCode);
            return new DataObject(Operations.Divide(type, a.Value, b.Value), type, 0);

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
                if (IsNull) {
                    return null;
                }

                // if (IsArray)
                // {
                //     return // _object;
                // }

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
                    // case ETypeCode.String:          
                    //     return String;
                    case ETypeCode.Time:            
                        return Time;
                    // case ETypeCode.Binary:
                    //     return Binary;
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
                    // case ETypeCode.Text:
                    //     return String;
                    // case ETypeCode.Guid:
                    //     return Guid;
                    // case ETypeCode.Json:
                    //     return Json;
                    // case ETypeCode.Xml:
                    //     return Xml;
                    case ETypeCode.Enum:
                        return Int32;
                    // case ETypeCode.CharArray:
                    //     return CharArray;
                    // case ETypeCode.Object:
                    //     return Object;
                    // case ETypeCode.Node:
                    //     return Node;
                    // case ETypeCode.Geometry:
                    //     return Geometry;
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

        public bool IsString()
        {
            return DataType.IsString(_typeCode);
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

        private void ThrowIfNull() {
            if (IsNull) {
                throw new NullValueException();
            }
        }

        public void Dispose()
        {
            
        }
    }
}