using System;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Xml;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using NetTopologySuite.Planargraph;

namespace Dexih.Utils.DataType
{
    public class DataObject
    {

        #region Storage
        internal struct NumericInfo {
            // This is used to store Decimal data
            internal Int32 data1;
            internal Int32 data2;
            internal Int32 data3;
            internal Int32 data4;
            internal Byte  precision;
            internal Byte  scale;
            internal Boolean positive;
        }
        

        [StructLayout(LayoutKind.Explicit)]
        internal struct Storage {
            [FieldOffset(0)] internal Boolean       _boolean;
            [FieldOffset(0)] internal Byte          _byte;
            [FieldOffset(0)] internal Char          _char;
            [FieldOffset(0)] internal Double        _double;
            [FieldOffset(0)] internal NumericInfo   _numericInfo;
            [FieldOffset(0)] internal Int16         _int16;
            [FieldOffset(0)] internal Int32         _int32; // also used for rank
            [FieldOffset(0)] internal Int64         _int64;     // also used to store UtcDateTime, Date , and Time
            [FieldOffset(0)] internal UInt16         _uint16;
            [FieldOffset(0)] internal UInt32         _uint32;
            [FieldOffset(0)] internal UInt64         _uint64;
            [FieldOffset(0)] internal sbyte        _sbyte;
            [FieldOffset(0)] internal Single        _single;
        }

        private bool         _isNull;
        private bool         _isArray;
        private ETypeCode  _typeCode;
        private Storage      _value;
        private object       _object;    // String, Binary, Json, Xml, Arrays 

        #endregion
        
        #region Initializers
        
        public DataObject()
        {
            _isNull = true;
            _isArray = false;
            _typeCode = ETypeCode.Unknown;
        }

        public DataObject(ETypeCode typeCode)
        {
            _isNull = true;
            _isArray = false;
            _typeCode = typeCode;
        }

        public DataObject(object value, ETypeCode typeCode, int rank = 0)
        {
            SetValue(value, typeCode, rank);
        }

        public DataObject(DataObject value) { // Clone
            // value types
            _isNull    = value._isNull;
            _isArray = value._isArray;
            _typeCode      = value._typeCode;
            // ref types - should also be read only unless at some point we allow this data
            // to be mutable, then we will need to copy
            _value     = value._value;
            _object    = value._object;
        }

        public DataObject(object value)
        {
            if (value is null)
            {
                _typeCode = ETypeCode.Unknown;
                _isNull = true;
                _isArray = false;
                return;
            }
            
            _isNull = false;
            _typeCode = DataType.GetTypeCode(value, out var rank);

            SetValue(value, _typeCode, rank);
        }

        public DataObject(string value)
        {
            _isNull = value == null;
            _object = value;
            _typeCode = ETypeCode.String;
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
            set {
                _value._boolean = value;
                _typeCode = ETypeCode.Boolean;
                _isNull = false;
                _isArray = false;
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
            set {
                _value._byte = value;
                _typeCode = ETypeCode.Byte;
                _isNull = false;
                _isArray = false;
            }
        }

        public Byte[] Binary {
            get {
                if (ETypeCode.Binary == _typeCode)
                {
                    return (byte[]) _object;
                }
                
                throw new InvalidTypeException(ETypeCode.Binary, _typeCode);
            }
            set
            {
                _object = value;
                _typeCode = ETypeCode.Binary;
                _isNull = value == null;
                _isArray = false;
            }
        }

        public Char Char {
            get {
                ThrowIfNull();

                if (ETypeCode.Char == _typeCode) {
                    return _value._char;
                }

                throw new InvalidTypeException(ETypeCode.Char, _typeCode);
            }
            set {
                _value._char = value;
                _typeCode = ETypeCode.Char;
                _isNull = false;
                _isArray = false;
            }
        }

        public Char[] CharArray {
            get
            {
                if (_object == null) return null;

                if (ETypeCode.CharArray == _typeCode) {
                    return (char[])_object;
                }
                throw new InvalidTypeException(ETypeCode.CharArray, _typeCode);
            }
            set {
                _object = value;
                _typeCode = ETypeCode.CharArray;
                _isNull = value == null;
                _isArray = false;
            }
        }
        
        public DateTime DateTime {
            get {
                ThrowIfNull();

                if (ETypeCode.DateTime == _typeCode) {
                    return new DateTime(_value._int64);
                }
                throw new InvalidTypeException(ETypeCode.DateTime, _typeCode);
            }
            set
            {
                _value._int64 = value.Date.Ticks;
                _typeCode = ETypeCode.DateTime;
                _isNull = false;
                _isArray = false;
            }
        }

        public Decimal Decimal {
            get {
                ThrowIfNull();

                if (ETypeCode.Decimal == _typeCode) {
                    if (_value._numericInfo.data4 != 0 && _value._numericInfo.scale > 28) {
                        throw new OverflowException();
                    }
                    return new Decimal(_value._numericInfo.data1, _value._numericInfo.data2, _value._numericInfo.data3, !_value._numericInfo.positive, _value._numericInfo.scale);
                }
                throw new InvalidTypeException(ETypeCode.Decimal, _typeCode);
            }
            set
            {
                var bytes = Decimal.GetBits(value);
                _value._numericInfo.data1 = bytes[0];
                _value._numericInfo.data2 = bytes[1];
                _value._numericInfo.data3 = bytes[2];
                _value._numericInfo.data4 = bytes[3];
                _value._numericInfo.precision = 38;
                _value._numericInfo.scale = (byte) ((bytes[3] >> 16) & 0x7F);
                _value._numericInfo.positive = (bytes[3] & 0x80000000) == 0;
                _typeCode = ETypeCode.Decimal;
                _isNull = false;
                _isArray = false;
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
            set {
                _value._double = value;
                _typeCode = ETypeCode.Double;
                _isNull = false;
                _isArray = false;
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
            set {
                _value._int32 = value;
                _typeCode = ETypeCode.Enum;
                _isNull = false;
                _isArray = false;
            }
        }
        
        public Geometry Geometry {
            get {
                if (ETypeCode.Geometry == _typeCode) {
                    return (Geometry) _object;
                }
                throw new InvalidTypeException(ETypeCode.Geometry, _typeCode);
            }
            set {
                _object = value;
                _typeCode = ETypeCode.Geometry;
                _isNull = value == null;
                _isArray = false;
            }
        }

        public Guid Guid {
            get {
                // 
                ThrowIfNull();

                if (ETypeCode.Guid == _typeCode)
                {
                    return (Guid)_object;
                }
                
                throw new InvalidTypeException(ETypeCode.Guid, _typeCode);
            }
            set
            {
                _object = value;
                _typeCode = ETypeCode.Guid;
                _isNull = false;
                _isArray = false; 
            }
        }

        public Int16 Int16 {
            get {
                ThrowIfNull();

                if (ETypeCode.Int16 == _typeCode) {
                    return _value._int16;
                }
                throw new InvalidTypeException(ETypeCode.Int16, _typeCode);
            }
            set {
                _value._int16 = value;
                _typeCode = ETypeCode.Int16;
                _isNull = false;
                _isArray = false;
            }
        }

        public Int32 Int32 {
            get {
                ThrowIfNull();

                if (ETypeCode.Int32 == _typeCode) {
                    return _value._int32;
                }
                throw new InvalidTypeException(ETypeCode.Int32, _typeCode);
            }
            set {
                _value._int32 = value;
                _typeCode = ETypeCode.Int32;
                _isNull = false;
                _isArray = false;
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
            set {
                _value._int64 = value;
                _typeCode = ETypeCode.Int64;
                _isNull = false;
                _isArray = false;
            }
        }

        public JsonElement Json {
            get {
                ThrowIfNull();
                
                if (ETypeCode.Json == _typeCode) {
                    return (JsonElement) _object;
                }
                throw new InvalidTypeException(ETypeCode.Json, _typeCode);
            }
            set {
                _object = value;
                _typeCode = ETypeCode.Json;
                _isNull = false;
                _isArray = false;
            }
        }
        
        public JsonElement Node {
            get {
                ThrowIfNull();
                
                if (ETypeCode.Node == _typeCode) {
                    return (JsonElement) _object;
                }
                throw new InvalidTypeException(ETypeCode.Node, _typeCode);
            }
            set {
                _object = value;
                _typeCode = ETypeCode.Node;
                _isNull = false;
                _isArray = false;
            }
        }
        
        public object Object {
            get {
                if (ETypeCode.Object == _typeCode) {
                    return _object;
                }
                throw new InvalidTypeException(ETypeCode.Object, _typeCode);
            }
            set {
                _object = value;
                _typeCode = ETypeCode.Object;
                _isNull = value == null;
                _isArray = false;
            }
        }
        
        public Single Single {
            get {
                ThrowIfNull();

                if (ETypeCode.Single == _typeCode) {
                    return _value._single;
                }
                throw new InvalidTypeException(ETypeCode.Single, _typeCode);
            }
            set {
                _value._single = value;
                _typeCode = ETypeCode.Single;
                _isNull = false;
                _isArray = false;
            }
        }

        public String String {
            get {
                if (_object == null) return null;

                if (ETypeCode.String == _typeCode) {
                    return (String)_object;
                }
                throw new InvalidTypeException(ETypeCode.String, _typeCode);
            }
            set
            {
                _object = value;
                _typeCode = ETypeCode.String;
                _value._int32 = value.Length;
                _isNull = false;
                _isArray = false;
            }
        }

        public SByte Sbyte {
            get {
                ThrowIfNull();

                if (ETypeCode.SByte == _typeCode)
                {
                    return _value._sbyte;
                }
                throw new InvalidTypeException(ETypeCode.SByte, _typeCode);
            }
            set {
                _value._sbyte = value;
                _typeCode = ETypeCode.SByte;
                _isNull = false;
                _isArray = false;
            }
        }
        
        public String Text {
            get {
                if (_object == null) return null;

                if (ETypeCode.Text == _typeCode) {
                    return (String)_object;
                }
                throw new InvalidTypeException(ETypeCode.Text, _typeCode);
            }
            set
            {
                _object = value;
                _typeCode = ETypeCode.Text;
                _isNull = false;
                _isArray = false;
            }
        }
        
        public TimeSpan Time {
            get {
                ThrowIfNull();

                if (ETypeCode.Time == _typeCode) {
                    return new TimeSpan(_value._int64);
                }

                throw new InvalidTypeException(ETypeCode.Time, _typeCode);
            }
            set
            {
                _value._int64 = value.Ticks;
                _typeCode = ETypeCode.Time;
                _isNull = false;
                _isArray = false;
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
            set {
                _value._uint16 = value;
                _typeCode = ETypeCode.UInt16;
                _isNull = false;
                _isArray = false;
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
            set {
                _value._uint32 = value;
                _typeCode = ETypeCode.UInt32;
                _isNull = false;
                _isArray = false;
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
            set {
                _value._uint64 = value;
                _typeCode = ETypeCode.UInt64;
                _isNull = false;
                _isArray = false;
            }
        }
        
        public XmlDocument Xml {
            get {
                ThrowIfNull();
                
                if (ETypeCode.Xml == _typeCode) {
                    return (XmlDocument) _object;
                }
                throw new InvalidTypeException(ETypeCode.Xml, _typeCode);
            }
            set {
                _object = value;
                _typeCode = ETypeCode.Xml;
                _isNull = false;
                _isArray = false;
            }
        }
        
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
                    return _value._numericInfo.scale;
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
                    return _value._numericInfo.precision;
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

        public static bool operator ==(DataObject a, DataObject b)
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
                    ETypeCode.Binary => a.Binary.SequenceEqual(b.Binary),
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
                    ETypeCode.Double => (a.Double == b.Double),
                    ETypeCode.Single => (a.Single == b.Single),
                    ETypeCode.String => (a.String == b.String),
                    ETypeCode.Text => (a.Text == b.Text),
                    ETypeCode.Boolean => (a.Boolean == b.Boolean),
                    ETypeCode.DateTime => (a._value._int64 == b._value._int64),
                    ETypeCode.Time => (a._value._int64 == b._value._int64),
                    ETypeCode.Guid => (a.Guid == b.Guid),
                    ETypeCode.Json => throw new CompareTypeException(ETypeCode.Json),
                    ETypeCode.Xml => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.Enum => (a._value._int32 == b._value._int32),
                    ETypeCode.CharArray => a.CharArray.SequenceEqual(b.CharArray),
                    ETypeCode.Object => throw new CompareTypeException(ETypeCode.Object),
                    ETypeCode.Node => throw new CompareTypeException(ETypeCode.Node),
                    ETypeCode.Geometry => Operations.Equal(a.Geometry, b.Geometry),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            
            return Operations.Equal(DataType.BestCompareType(a._typeCode, b._typeCode), a.Value, b.Value);

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
                    ETypeCode.Binary => Operations.LessThan(a.Binary, b.Binary),
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
                    ETypeCode.String => Operations.LessThan(a.String, b.String),
                    ETypeCode.Text => Operations.LessThan(a.Text, b.Text),
                    ETypeCode.Boolean => Operations.LessThan(a.Boolean, b.Boolean),
                    ETypeCode.DateTime => (a._value._int64 < b._value._int64),
                    ETypeCode.Time => (a._value._int64 < b._value._int64),
                    ETypeCode.Guid => Operations.LessThan(a.Guid, b.Guid),
                    ETypeCode.Json => throw new CompareTypeException(ETypeCode.Json),
                    ETypeCode.Xml => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.Enum => (a._value._int32 == b._value._int32),
                    ETypeCode.CharArray => Operations.LessThan(a.CharArray, b.CharArray),
                    ETypeCode.Object => throw new CompareTypeException(ETypeCode.Object),
                    ETypeCode.Node => throw new CompareTypeException(ETypeCode.Node),
                    ETypeCode.Geometry => Operations.LessThan(a.Geometry, b.Geometry),
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
                    ETypeCode.Binary => Operations.GreaterThan(a.Binary, b.Binary),
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
                    ETypeCode.String => Operations.GreaterThan(a.String, b.String),
                    ETypeCode.Text => Operations.GreaterThan(a.Text, b.Text),
                    ETypeCode.Boolean => Operations.GreaterThan(a.Boolean, b.Boolean),
                    ETypeCode.DateTime => (a._value._int64 > b._value._int64),
                    ETypeCode.Time => (a._value._int64 > b._value._int64),
                    ETypeCode.Guid => Operations.GreaterThan(a.Guid, b.Guid),
                    ETypeCode.Json => throw new CompareTypeException(ETypeCode.Json),
                    ETypeCode.Xml => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.Enum => (a._value._int32 == b._value._int32),
                    ETypeCode.CharArray => Operations.GreaterThan(a.CharArray, b.CharArray),
                    ETypeCode.Object => throw new CompareTypeException(ETypeCode.Object),
                    ETypeCode.Node => throw new CompareTypeException(ETypeCode.Node),
                    ETypeCode.Geometry => Operations.GreaterThan(a.Geometry, b.Geometry),
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
                    ETypeCode.Binary => Operations.LessThanOrEqual(a.Binary, b.Binary),
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
                    ETypeCode.String => Operations.LessThanOrEqual(a.String, b.String),
                    ETypeCode.Text => Operations.LessThanOrEqual(a.Text, b.Text),
                    ETypeCode.Boolean => Operations.LessThanOrEqual(a.Boolean, b.Boolean),
                    ETypeCode.DateTime => (a._value._int64 <= b._value._int64),
                    ETypeCode.Time => (a._value._int64 <= b._value._int64),
                    ETypeCode.Guid => Operations.LessThanOrEqual(a.Guid, b.Guid),
                    ETypeCode.Json => throw new CompareTypeException(ETypeCode.Json),
                    ETypeCode.Xml => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.Enum => (a._value._int32 == b._value._int32),
                    ETypeCode.CharArray => Operations.LessThanOrEqual(a.CharArray, b.CharArray),
                    ETypeCode.Object => throw new CompareTypeException(ETypeCode.Object),
                    ETypeCode.Node => throw new CompareTypeException(ETypeCode.Node),
                    ETypeCode.Geometry => Operations.LessThanOrEqual(a.Geometry, b.Geometry),
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
                    ETypeCode.Binary => Operations.GreaterThanOrEqual(a.Binary, b.Binary),
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
                    ETypeCode.String => Operations.GreaterThanOrEqual(a.String, b.String),
                    ETypeCode.Text => Operations.GreaterThanOrEqual(a.Text, b.Text),
                    ETypeCode.Boolean => Operations.GreaterThanOrEqual(a.Boolean, b.Boolean),
                    ETypeCode.DateTime => (a._value._int64 >= b._value._int64),
                    ETypeCode.Time => (a._value._int64 >= b._value._int64),
                    ETypeCode.Guid => Operations.GreaterThanOrEqual(a.Guid, b.Guid),
                    ETypeCode.Json => throw new CompareTypeException(ETypeCode.Json),
                    ETypeCode.Xml => throw new CompareTypeException(ETypeCode.Xml),
                    ETypeCode.Enum => (a._value._int32 == b._value._int32),
                    ETypeCode.CharArray => Operations.GreaterThanOrEqual(a.CharArray, b.CharArray),
                    ETypeCode.Object => throw new CompareTypeException(ETypeCode.Object),
                    ETypeCode.Node => throw new CompareTypeException(ETypeCode.Node),
                    ETypeCode.Geometry => Operations.GreaterThanOrEqual(a.Geometry, b.Geometry),
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
                    ETypeCode.Byte => new DataObject() {Int32 = a.Byte + b.Byte},
                    ETypeCode.Char => new DataObject() {Int32 = a.Char + b.Char},
                    ETypeCode.SByte => new DataObject() {Int32 = a.Sbyte + b.Sbyte},
                    ETypeCode.UInt16 => new DataObject() {Int32 = a.UInt16 + b.UInt16},
                    ETypeCode.UInt32 => new DataObject() {UInt32 = a.UInt32 + b.UInt32},
                    ETypeCode.UInt64 => new DataObject() {UInt64 = a.UInt64 + b.UInt64},
                    ETypeCode.Int16 => new DataObject() {Int32 = a.Int16 + b.Int16},
                    ETypeCode.Int32 => new DataObject() {Int32 = a.Int32 + b.Int32},
                    ETypeCode.Int64 => new DataObject() {Int64 = a.Int64 + b.Int64},
                    ETypeCode.Decimal => new DataObject() {Decimal = a.Decimal + b.Decimal},
                    ETypeCode.Double => new DataObject() {Double = a.Double + b.Double},
                    ETypeCode.Single => new DataObject() {Single = a.Single + b.Single},
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            var dataObject = new DataObject();
            var type = DataType.BestCompareType(a._typeCode, b._typeCode);
            dataObject.SetValue(Operations.Add(type, a.Value, b.Value), type, 0);
            return dataObject;
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
                    ETypeCode.Byte => new DataObject() {Int32 = a.Byte - b.Byte},
                    ETypeCode.Char => new DataObject() {Int32 = a.Char - b.Char},
                    ETypeCode.SByte => new DataObject() {Int32 = a.Sbyte - b.Sbyte},
                    ETypeCode.UInt16 => new DataObject() {Int32 = a.UInt16 - b.UInt16},
                    ETypeCode.UInt32 => new DataObject() {UInt32 = a.UInt32 - b.UInt32},
                    ETypeCode.UInt64 => new DataObject() {UInt64 = a.UInt64 - b.UInt64},
                    ETypeCode.Int16 => new DataObject() {Int32 = a.Int16 - b.Int16},
                    ETypeCode.Int32 => new DataObject() {Int32 = a.Int32 - b.Int32},
                    ETypeCode.Int64 => new DataObject() {Int64 = a.Int64 - b.Int64},
                    ETypeCode.Decimal => new DataObject() {Decimal = a.Decimal - b.Decimal},
                    ETypeCode.Double => new DataObject() {Double = a.Double - b.Double},
                    ETypeCode.Single => new DataObject() {Single = a.Single - b.Single},
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            var dataObject = new DataObject();
            var type = DataType.BestCompareType(a._typeCode, b._typeCode);
            dataObject.SetValue(Operations.Subtract(type, a.Value, b.Value), type, 0);
            return dataObject;
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
                    ETypeCode.Byte => new DataObject() {Int32 = a.Byte * b.Byte},
                    ETypeCode.Char => new DataObject() {Int32 = a.Char * b.Char},
                    ETypeCode.SByte => new DataObject() {Int32 = a.Sbyte * b.Sbyte},
                    ETypeCode.UInt16 => new DataObject() {Int32 = a.UInt16 * b.UInt16},
                    ETypeCode.UInt32 => new DataObject() {UInt32 = a.UInt32 * b.UInt32},
                    ETypeCode.UInt64 => new DataObject() {UInt64 = a.UInt64 * b.UInt64},
                    ETypeCode.Int16 => new DataObject() {Int32 = a.Int16 * b.Int16},
                    ETypeCode.Int32 => new DataObject() {Int32 = a.Int32 * b.Int32},
                    ETypeCode.Int64 => new DataObject() {Int64 = a.Int64 * b.Int64},
                    ETypeCode.Decimal => new DataObject() {Decimal = a.Decimal * b.Decimal},
                    ETypeCode.Double => new DataObject() {Double = a.Double * b.Double},
                    ETypeCode.Single => new DataObject() {Single = a.Single * b.Single},
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            var dataObject = new DataObject();
            var type = DataType.BestCompareType(a._typeCode, b._typeCode);
            dataObject.SetValue(Operations.Multiply(type, a.Value, b.Value), type, 0);
            return dataObject;
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
                    ETypeCode.Byte => new DataObject() {Int32 = a.Byte / b.Byte},
                    ETypeCode.Char => new DataObject() {Int32 = a.Char / b.Char},
                    ETypeCode.SByte => new DataObject() {Int32 = a.Sbyte / b.Sbyte},
                    ETypeCode.UInt16 => new DataObject() {Int32 = a.UInt16 / b.UInt16},
                    ETypeCode.UInt32 => new DataObject() {UInt32 = a.UInt32 / b.UInt32},
                    ETypeCode.UInt64 => new DataObject() {UInt64 = a.UInt64 / b.UInt64},
                    ETypeCode.Int16 => new DataObject() {Int32 = a.Int16 / b.Int16},
                    ETypeCode.Int32 => new DataObject() {Int32 = a.Int32 / b.Int32},
                    ETypeCode.Int64 => new DataObject() {Int64 = a.Int64 / b.Int64},
                    ETypeCode.Decimal => new DataObject() {Decimal = a.Decimal / b.Decimal},
                    ETypeCode.Double => new DataObject() {Double = a.Double / b.Double},
                    ETypeCode.Single => new DataObject() {Single = a.Single / b.Single},
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            var dataObject = new DataObject();
            var type = DataType.BestCompareType(a._typeCode, b._typeCode);
            dataObject.SetValue(Operations.Divide(type, a.Value, b.Value), type, 0);
            return dataObject;
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

                if (IsArray)
                {
                    return _object;
                }

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
                    case ETypeCode.String:          
                        return String;
                    case ETypeCode.Time:            
                        return Time;
                    case ETypeCode.Binary:
                        return Binary;
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
                    case ETypeCode.Text:
                        return String;
                    case ETypeCode.Guid:
                        return Guid;
                    case ETypeCode.Json:
                        return Json;
                    case ETypeCode.Xml:
                        return Xml;
                    case ETypeCode.Enum:
                        return Int32;
                    case ETypeCode.CharArray:
                        return CharArray;
                    case ETypeCode.Object:
                        return Object;
                    case ETypeCode.Node:
                        return Node;
                    case ETypeCode.Geometry:
                        return Geometry;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                return null; // need to return the value as an object of some CLS type
            }
        }
        
        public void SetValue(object value, ETypeCode typeCode, int rank)
        {
            _typeCode = typeCode;

            if (rank > 0)
            {
                _isArray = true;
                _value._int32 = rank;
                _object = value;
                
            }

            _isArray = false;

            switch (_typeCode)
            {
                case ETypeCode.Binary:
                    Binary = (byte[]) value;
                    break;
                case ETypeCode.Byte:
                    Byte = (byte) value;
                    break;
                case ETypeCode.Char:
                    Char = (char) value;
                    break;
                case ETypeCode.SByte:
                    Sbyte = (SByte) value;
                    break;
                case ETypeCode.UInt16:
                    UInt16 = (UInt16) value;
                    break;
                case ETypeCode.UInt32:
                    UInt32 = (UInt32) value;
                    break;
                case ETypeCode.UInt64:
                    UInt64 = (UInt64) value;
                    break;
                case ETypeCode.Int16:
                    Int16 = (Int16) value;
                    break;
                case ETypeCode.Int32:
                    Int32 = (Int32) value;
                    break;
                case ETypeCode.Int64:
                    Int64 = (Int64) value;
                    break;
                case ETypeCode.Decimal:
                    Decimal = (Decimal) value;
                    break;
                case ETypeCode.Double:
                    Double = (Double) value;
                    break;
                case ETypeCode.Single:
                    Single = (Single) value;
                    break;
                case ETypeCode.String:
                    SetToString((string) value);
                    break;
                case ETypeCode.Text:
                    Text = (string) value;
                    break;
                case ETypeCode.Boolean:
                    Boolean = (bool) value;
                    break;
                case ETypeCode.DateTime:
                    DateTime = (DateTime) value;
                    break;
                case ETypeCode.Time:
                    Time = (TimeSpan) value;
                    break;
                case ETypeCode.Guid:
                    Guid = (Guid) value;
                    break;
                case ETypeCode.Json:
                    Json = (JsonElement) value;
                    break;
                case ETypeCode.Xml:
                    Xml = (XmlDocument) value;
                    break;
                case ETypeCode.Enum:
                    Enum = (int) value;
                    break;
                case ETypeCode.CharArray:
                    CharArray = (char[]) value;
                    break;
                case ETypeCode.Object:
                    Object = value;
                    break;
                case ETypeCode.Node:
                    Node = (JsonElement) value;
                    break;
                case ETypeCode.Geometry:
                    Geometry = (Geometry) value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public ETypeCode GetTypeCode()
        {
            return _typeCode;
        }

        public Type GetType ()
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

        public void Clear() {
            _isNull = false;
            _typeCode = ETypeCode.Unknown;
            _object = null;
        }
        
        public void SetToDecimal(byte precision, byte scale, bool positive, int[] bits) {
            _value._numericInfo.precision = precision;
            _value._numericInfo.scale = scale;
            _value._numericInfo.positive = positive;
            _value._numericInfo.data1 = bits[0];
            _value._numericInfo.data2 = bits[1];
            _value._numericInfo.data3 = bits[2];
            _value._numericInfo.data4 = bits[3];
            _typeCode = ETypeCode.Decimal;
            _isNull = false;
        }
        
        public void SetToNullOfType(ETypeCode datatype) {
            _typeCode = datatype;
            _isNull = true;
            _isArray = false;
            _object = null;
        }

        public void SetToString(string value, int maxLength = -1) {
            _object = value;
            _typeCode = ETypeCode.String;
            _value._int32 = maxLength;
            _isNull = false;
            _isArray = false;
        }
        

        private void ThrowIfNull() {
            if (IsNull) {
                throw new NullValueException();
            }
        }
    }
}