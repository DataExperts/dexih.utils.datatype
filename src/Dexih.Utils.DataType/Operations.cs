using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dexih.Utils.DataType
{
    public static class Operations
    {
        internal static readonly Type[] ConvertTypes = {
            typeof (object),
            typeof (DBNull),
            typeof (bool),
            typeof (char),
            typeof (sbyte),
            typeof (byte),
            typeof (short),
            typeof (ushort),
            typeof (int),
            typeof (uint),
            typeof (long),
            typeof (ulong),
            typeof (float),
            typeof (double),
            typeof (decimal),
            typeof (DateTime),
            typeof (string),
            typeof(byte[]),
            typeof(char[]),
            typeof(JToken),
            typeof(XmlDocument),
            typeof(TimeSpan),
            typeof(Guid)
        };

        private const int ConvertTypeObject = 0;
        private const int ConvertTypeDbNull = 1;
        private const int ConvertTypeBool = 2;
        private const int ConvertTypeChar = 3;
        private const int ConvertTypeSbyte = 4;
        private const int ConvertTypeByte = 5;
        private const int ConvertTypeShort = 6;
        private const int ConvertTypeUShort = 7;
        private const int ConvertTypeInt = 8;
        private const int ConvertTypeUint = 9;
        private const int ConvertTypeLong = 10;
        private const int ConvertTypeULong = 11;
        private const int ConvertTypeFloat = 12;
        private const int ConvertTypeDouble = 13;
        private const int ConvertTypeDecimal = 14;
        private const int ConvertTypeDateTime = 15;
        private const int ConvertTypeString = 16;
        private const int ConvertTypeByteArray = 17;
        private const int ConvertTypeCharArray = 18;
        private const int ConvertTypeJToken = 19;
        private const int ConvertTypeXmlDocument = 20;
        private const int ConvertTypeTimeSpan = 21;
        private const int ConvertTypeGuid = 22;
        
        public static T Add<T>(T a, T b) => Operations<T>.Add.Value(a,b);
        public static T Subtract<T>(T a, T b) => Operations<T>.Subtract.Value(a, b);
        public static T Divide<T>(T a, T b) => Operations<T>.Divide.Value(a, b);
        public static T DivideInt<T>(T a, int b) => Operations<T>.DivideInt.Value(a, b);
        public static T Multiply<T>(T a, T b) => Operations<T>.Multiply.Value(a, b);
        public static T Negate<T>(T a) => Operations<T>.Negate.Value(a);
        public static object Negate<T>(object a) => Operations<object>.Negate.Value(a);
        public static T Increment<T>(T a) => Operations<T>.Increment.Value(a);
        public static object Increment<T>(object a) => Operations<object>.Increment.Value(a);
        public static T Decrement<T>(T a) => Operations<T>.Decrement.Value(a);
        public static object Decrement<T>(object a) => Operations<object>.Decrement.Value(a);
        public static bool GreaterThan<T>(T a, T b) => Operations<T>.GreaterThan.Value(a, b);
        public static bool GreaterThan<T>(object a, object b) => Operations<object>.GreaterThan.Value(a, b);
        public static bool LessThan<T>(object a, object b) => Operations<object>.LessThan.Value(a, b);
        public static bool LessThan<T>(T a, T b) => Operations<T>.LessThan.Value(a, b);
        public static bool GreaterThanOrEqual<T>(object a, object b) => Operations<object>.GreaterThanOrEqual.Value(a, b);
        public static bool GreaterThanOrEqual<T>(T a, T b) => Operations<T>.GreaterThanOrEqual.Value(a, b);
        public static bool LessThanOrEqual<T>(T a, T b) => Operations<T>.LessThanOrEqual.Value(a, b);
        public static bool LessThanOrEqual<T>(object a, object b) => Operations<object>.LessThanOrEqual.Value(a, b);
        public static bool Equal<T>(T a, T b) => Operations<T>.Equal.Value(a, b);
        public static bool Equal<T>(object a, object b) => Operations<object>.Equal.Value(a, b);
        public static string ToString<T>(T a) => Operations<T>.ToString.Value(a);
        public static T Parse<T>(object a) => Operations<T>.Parse.Value(a);
        public static int Compare<T>(T inputValue, T compareTo) => Operations<T>.Compare.Value(inputValue, compareTo);
        public static int Compare<T>(object inputValue, object compareTo) => Operations<object>.Compare.Value(inputValue, compareTo);

        public static object Parse(DataType.ETypeCode typeCode, object inputValue)
        {
            if (inputValue == null || inputValue == DBNull.Value)
            {
                return null;
            }

            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                    return Parse<byte[]>(inputValue);
                case DataType.ETypeCode.Byte:
                    return Parse<byte>(inputValue);
                case DataType.ETypeCode.SByte:
                    return Parse<sbyte>(inputValue);
                case DataType.ETypeCode.UInt16:
                    return Parse<ushort>(inputValue);
                case DataType.ETypeCode.UInt32:
                    return Parse<uint>(inputValue);
                case DataType.ETypeCode.UInt64:
                    return Parse<ulong>(inputValue);
                case DataType.ETypeCode.Int16:
                    return Parse<short>(inputValue);
                case DataType.ETypeCode.Int32:
                    return Parse<int>(inputValue);
                case DataType.ETypeCode.Int64:
                    return Parse<long>(inputValue);
                case DataType.ETypeCode.Decimal:
                    return Parse<decimal>(inputValue);
                case DataType.ETypeCode.Double:
                    return Parse<double>(inputValue);
                case DataType.ETypeCode.Single:
                    return Parse<float>(inputValue);
                case DataType.ETypeCode.String:
                    return Parse<string>(inputValue);
                case DataType.ETypeCode.Text:
                    return Parse<string>(inputValue);
                case DataType.ETypeCode.Boolean:
                    return Parse<bool>(inputValue);
                case DataType.ETypeCode.DateTime:
                    return Parse<DateTime>(inputValue);
                case DataType.ETypeCode.Time:
                    return Parse<TimeSpan>(inputValue);
                case DataType.ETypeCode.Guid:
                    return Parse<Guid>(inputValue);
                case DataType.ETypeCode.Unknown:
                    return Parse<string>(inputValue);
                case DataType.ETypeCode.Json:
                    return Parse<JToken>(inputValue);
                case DataType.ETypeCode.Xml:
                    return Parse<XmlDocument>(inputValue);
                case DataType.ETypeCode.Enum:
                    return Parse<int>(inputValue);
                case DataType.ETypeCode.CharArray:
                    return Parse<char[]>(inputValue);
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }
        
        public static object Parse(Type type, object inputValue)
        {
            if (inputValue == null || inputValue == DBNull.Value)
            {
                return null;
            }

            if (type.IsArray && type != typeof(byte[]) && type != typeof(char[]))
            {
                var elementType = type.GetElementType();
                var rank = type.GetArrayRank();
                var inputArray = (object[]) inputValue;
                var returnValue = new object[inputArray.Length];
                for (var i = 0; i < inputArray.Length; i++)
                {
                    returnValue[i] = Parse(elementType, rank -1, inputArray[i]);
                }

                return returnValue;
            }

            if (type == ConvertTypes[ConvertTypeBool]) return Parse<bool>(inputValue);
            if (type == ConvertTypes[ConvertTypeSbyte]) return Parse<sbyte>(inputValue);
            if (type == ConvertTypes[ConvertTypeByte]) return Parse<byte>(inputValue);
            if (type == ConvertTypes[ConvertTypeShort]) return Parse<short>(inputValue);
            if (type == ConvertTypes[ConvertTypeUShort]) return Parse<ushort>(inputValue);
            if (type == ConvertTypes[ConvertTypeInt]) return Parse<int>(inputValue);
            if (type == ConvertTypes[ConvertTypeUint]) return Parse<uint>(inputValue);
            if (type == ConvertTypes[ConvertTypeLong]) return Parse<long>(inputValue);
            if (type == ConvertTypes[ConvertTypeULong]) return Parse<ulong>(inputValue);
            if (type == ConvertTypes[ConvertTypeFloat]) return Parse<float>(inputValue);
            if (type == ConvertTypes[ConvertTypeDouble]) return Parse<double>(inputValue);
            if (type == ConvertTypes[ConvertTypeDecimal]) return Parse<decimal>(inputValue);
            if (type == ConvertTypes[ConvertTypeDateTime]) return Parse<DateTime>(inputValue);
            if (type == ConvertTypes[ConvertTypeString]) return Parse<string>(inputValue);
            if (type == ConvertTypes[ConvertTypeGuid]) return Parse<Guid>(inputValue);
            if (type == ConvertTypes[ConvertTypeByteArray]) return Parse<byte[]>(inputValue);
            if (type == ConvertTypes[ConvertTypeCharArray]) return Parse<char[]>(inputValue);
            if (type == ConvertTypes[ConvertTypeJToken]) return Parse<JToken>(inputValue);
            if (type == ConvertTypes[ConvertTypeXmlDocument]) return Parse<XmlDocument>(inputValue);
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return Parse<TimeSpan>(inputValue);

            throw new ArgumentOutOfRangeException(nameof(type), inputValue, null);
        }

        public static object Parse(DataType.ETypeCode tryDataType, int rank, object inputValue)
        {
            if (rank == 0)
            {
                return Parse(tryDataType, inputValue);
            }

            var type = inputValue.GetType();
            if (type.IsArray && type != typeof(byte[]) && type != typeof(char[]))
            {
                var inputArray = (object[]) inputValue;
                var returnValue = new object[inputArray.Length];
                for (var i = 0; i < inputArray.Length; i++)
                {
                    returnValue[i] = Parse(tryDataType, rank -1, inputArray[i]);
                }

                return returnValue;
            }
            
            return Parse(tryDataType, inputValue);
        }
        
        public static object Parse(Type tryType, int rank, object inputValue)
        {
            if (rank == 0)
            {
                return Parse(tryType, inputValue);
            }

            var type = inputValue.GetType();
            if (type.IsArray && type != typeof(byte[]) && type != typeof(char[]))
            {
                var inputArray = (object[]) inputValue;
                var returnValue = new object[inputArray.Length];
                for (var i = 0; i < inputArray.Length; i++)
                {
                    returnValue[i] = Parse(tryType, rank -1, inputArray[i]);
                }

                return returnValue;
            }

            if (type == typeof(string))
            {
                var arrayType = tryType.MakeArrayType(rank);
                var result = JsonConvert.DeserializeObject((string)inputValue, arrayType);
                return result;
            }
            
            return Parse(type, inputValue);
        }
        
        public static bool Equal(object inputValue, object compareTo)
        {
            var type = inputValue?.GetType();
            return Equal(type, inputValue, compareTo);
        }
        
        public static bool Equal(Type type, object inputValue, object compareTo)
        {
            if ((inputValue == null || inputValue == DBNull.Value) && (compareTo == null || compareTo == DBNull.Value))
                return true;

            if (inputValue == null || inputValue == DBNull.Value || compareTo == null || compareTo == DBNull.Value)
                return false;

            if (type.IsArray)
            {
                var array1 = (Array) inputValue;
                var array2 = (Array) compareTo;
                if (array1.Length != array2.Length) return false;
                var elementType = type.GetElementType();

                for (var i = 0; i < array1.Length; i++)
                {
                    if (!Equal(elementType, array1.GetValue(i), array2.GetValue(i))) return false;
                }

                return true;
            }
            return object.Equals(inputValue, compareTo);
            
//            if (type == ConvertTypes[ConvertTypeBool]) return Equal<bool>(inputValue, compareTo);
//            if (type == ConvertTypes[ConvertTypeSbyte]) return Equal<sbyte>(inputValue, compareTo);
//            if (type == ConvertTypes[ConvertTypeByte]) return Equal<byte>(inputValue, compareTo);
//            if (type == ConvertTypes[ConvertTypeShort]) return Equal<short>(inputValue, compareTo);
//            if (type == ConvertTypes[ConvertTypeUShort]) return Equal<ushort>(inputValue, compareTo);
//            if (type == ConvertTypes[ConvertTypeInt]) return Equal<int>(inputValue, compareTo);
//            if (type == ConvertTypes[ConvertTypeUint]) return Equal<uint>(inputValue, compareTo);
//            if (type == ConvertTypes[ConvertTypeLong]) return Equal<long>(inputValue, compareTo);
//            if (type == ConvertTypes[ConvertTypeULong]) return Equal<ulong>(inputValue, compareTo);
//            if (type == ConvertTypes[ConvertTypeFloat]) return Equal<float>(inputValue, compareTo);
//            if (type == ConvertTypes[ConvertTypeDouble]) return Equal<double>(inputValue, compareTo);
//            if (type == ConvertTypes[ConvertTypeDecimal]) return Equal<decimal>(inputValue, compareTo);
//            if (type == ConvertTypes[ConvertTypeDateTime]) return Equal<DateTime>(inputValue, compareTo);
//            if (type == ConvertTypes[ConvertTypeString]) return Equal<string>(inputValue, compareTo);
//            if (type == ConvertTypes[ConvertTypeGuid]) return Equal<Guid>(inputValue, compareTo);
//            if (type == ConvertTypes[ConvertTypeByteArray]) return Equal<byte[]>(inputValue, compareTo);
//            if (type == ConvertTypes[ConvertTypeCharArray]) return Equal<char[]>(inputValue, compareTo);
//            if (type == ConvertTypes[ConvertTypeJToken]) return Equal<JToken>(inputValue, compareTo);
//            if (type == ConvertTypes[ConvertTypeXmlDocument]) return Equal<XmlDocument>(inputValue, compareTo);
//            if (type == ConvertTypes[ConvertTypeTimeSpan]) return Equal<TimeSpan>(inputValue, compareTo);
//
//            throw new ArgumentOutOfRangeException(nameof(type), inputValue, null);
        }

        public static bool Equal(DataType.ETypeCode typeCode, object value1, object value2)
        {
            if ((value1 == null || value1 == DBNull.Value) && (value2 == null || value2 == DBNull.Value))
                return true;

            if (value1 == null || value1 == DBNull.Value || value2 == null || value2 == DBNull.Value)
                return false;

            if (typeCode == DataType.ETypeCode.Binary || typeCode == DataType.ETypeCode.CharArray)
            {
                var array1 = (Array) value1;
                var array2 = (Array) value2;
                if (array1.Length != array2.Length) return false;
                var elementType = typeCode == DataType.ETypeCode.Binary ? typeof(byte) : typeof(char);

                for (var i = 0; i < array1.Length; i++)
                {
                    if (!Equal(elementType, array1.GetValue(i), array2.GetValue(i))) return false;
                }

                return true;
            }

            return object.Equals(value1, value2);
//            switch (typeCode)
//            {
//                case DataType.ETypeCode.Binary:
//                    return Equal<byte[]>(value1, value2);
//                case DataType.ETypeCode.Byte:
//                    return Equal<byte>(value1, value2);
//                case DataType.ETypeCode.SByte:
//                    return Equal<sbyte>(value1, value2);
//                case DataType.ETypeCode.UInt16:
//                    return Equal<ushort>(value1, value2);
//                case DataType.ETypeCode.UInt32:
//                    return Equal<uint>(value1, value2);
//                case DataType.ETypeCode.UInt64:
//                    return Equal<ulong>(value1, value2);
//                case DataType.ETypeCode.Int16:
//                    return Equal<short>(value1, value2);
//                case DataType.ETypeCode.Int32:
//                    return Equal<int>(value1, value2);
//                case DataType.ETypeCode.Int64:
//                    return Equal<long>(value1, value2);
//                case DataType.ETypeCode.Decimal:
//                    return Equal<decimal>(value1, value2);
//                case DataType.ETypeCode.Double:
//                    return Equal<double>(value1, value2);
//                case DataType.ETypeCode.Single:
//                    return Equal<float>(value1, value2);
//                case DataType.ETypeCode.String:
//                    return Equal<string>(value1, value2);
//                case DataType.ETypeCode.Text:
//                    return Equal<string>(value1, value2);
//                case DataType.ETypeCode.Boolean:
//                    return Equal<bool>(value1, value2);
//                case DataType.ETypeCode.DateTime:
//                    return Equal<DateTime>(value1, value2);
//                case DataType.ETypeCode.Time:
//                    return Equal<TimeSpan>(value1, value2);
//                case DataType.ETypeCode.Guid:
//                    return Equal<Guid>(value1, value2);
//                case DataType.ETypeCode.Unknown:
//                    return Equal<string>(value1, value2);
//                case DataType.ETypeCode.Json:
//                    return Equal<JToken>(value1, value2);
//                case DataType.ETypeCode.Xml:
//                    return Equal<XmlDocument>(value1, value2);
//                case DataType.ETypeCode.Enum:
//                    return Equal<int>(value1, value2);
//                case DataType.ETypeCode.Char:
//                    return Equal<char[]>(value1, value2);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
//            }
        }

        public static bool GreaterThan(object a, object b)
        {
            var type = a?.GetType();
            return GreaterThan(type, a, b);
        }
        
        public static bool GreaterThan(Type type, object a, object b)
        {
            if (type == ConvertTypes[ConvertTypeBool]) return BoolIsGreaterThan((bool)a, (bool)b);
            if (type == ConvertTypes[ConvertTypeSbyte]) return (sbyte)a > (sbyte) b;
            if (type == ConvertTypes[ConvertTypeByte]) return (byte)a > (byte) b;
            if (type == ConvertTypes[ConvertTypeShort]) return (short)a > (short) b;
            if (type == ConvertTypes[ConvertTypeUShort]) return (ushort)a > (ushort) b;
            if (type == ConvertTypes[ConvertTypeInt]) return (int)a > (int) b;
            if (type == ConvertTypes[ConvertTypeUint]) return (uint)a > (uint) b;
            if (type == ConvertTypes[ConvertTypeLong]) return (long)a > (long) b;
            if (type == ConvertTypes[ConvertTypeULong]) return (ulong)a > (ulong) b;
            if (type == ConvertTypes[ConvertTypeFloat]) return  (float)a > (float) b;
            if (type == ConvertTypes[ConvertTypeDouble]) return (double)a > (double) b;
            if (type == ConvertTypes[ConvertTypeDecimal]) return (decimal)a > (decimal) b;
            if (type == ConvertTypes[ConvertTypeDateTime]) return (DateTime)a > (DateTime) b;
            if (type == ConvertTypes[ConvertTypeString]) return String.CompareOrdinal((string)a, (string) b) > 0;
            if (type == ConvertTypes[ConvertTypeGuid]) return String.CompareOrdinal(a.ToString(), b.ToString()) > 0;
            if (type == ConvertTypes[ConvertTypeByteArray]) return ByteArrayIsGreater((byte[])a, (byte[])b,false);
            if (type == ConvertTypes[ConvertTypeCharArray]) return CharArrayIsGreater((char[])a, (char[])b,false);
            // if (type == ConvertTypes[ConvertTypeJToken]) return (JToken)a > (JToken) b;
            // if (type == ConvertTypes[ConvertTypeXmlDocument]) return (XmlDocument)a > (XmlDocument) b;
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return (TimeSpan)a > (TimeSpan) b;

            throw new ArgumentOutOfRangeException(nameof(type), a, null);
        }

        public static bool GreaterThan(DataType.ETypeCode typeCode,  object value1, object value2)
        {
            switch (typeCode)
            {

                case DataType.ETypeCode.Unknown:
                case DataType.ETypeCode.Json:
                case DataType.ETypeCode.Xml:
                    throw new Exception($"Cannot compare {typeCode} types.");
                case DataType.ETypeCode.Binary:
                    return ByteArrayIsGreater((byte[])value1, (byte[])value2,false);
                case DataType.ETypeCode.CharArray:
                    return CharArrayIsGreater((char[])value1, (char[])value2,false);
                case DataType.ETypeCode.String:
                case DataType.ETypeCode.Text:
                    return string.CompareOrdinal((string)value1, (string)value2) > 0;
                case DataType.ETypeCode.Guid:
                    return string.CompareOrdinal(value1.ToString(), value2.ToString()) > 0;
                case DataType.ETypeCode.DateTime:
                    return (DateTime) value1 > (DateTime) value2;
                case DataType.ETypeCode.Boolean:
                    return BoolIsGreaterThan((bool) value1, (bool) value2);
                case DataType.ETypeCode.Time:
                    return (TimeSpan) value1 > (TimeSpan) value2;
                case DataType.ETypeCode.Byte:
                    return ((byte) value1 > (byte) value2);
                case DataType.ETypeCode.SByte:
                    return ((sbyte) value1 > (sbyte) value2);
                case DataType.ETypeCode.UInt16:
                    return ((ushort) value1 > (ushort) value2);
                case DataType.ETypeCode.UInt32:
                    return (uint) value1 > (uint) value2;
                case DataType.ETypeCode.UInt64:
                    return (ulong) value1 > (ulong) value2;
                case DataType.ETypeCode.Int16:
                    return ((short) value1 > (short) value2);
                case DataType.ETypeCode.Int32:
                    return (int) value1 > (int) value2;
                case DataType.ETypeCode.Int64:
                    return (long) value1 > (long) value2;
                case DataType.ETypeCode.Decimal:
                    return (decimal) value1 > (decimal) value2;
                case DataType.ETypeCode.Double:
                    return (double) value1 > (double) value2;
                case DataType.ETypeCode.Single:
                    return (float) value1 > (float) value2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }

       public static bool GreaterThanOrEqual(object a, object b)
        {
            var type = a?.GetType();
            return GreaterThanOrEqual(type, a, b);
        }
        
        public static bool GreaterThanOrEqual(Type type, object a, object b)
        {
            if (type == ConvertTypes[ConvertTypeBool]) return BoolIsGreaterThanOrEqual((bool)a, (bool)b);
            if (type == ConvertTypes[ConvertTypeSbyte]) return (sbyte)a >= (sbyte) b;
            if (type == ConvertTypes[ConvertTypeByte]) return (byte)a >= (byte) b;
            if (type == ConvertTypes[ConvertTypeShort]) return (short)a >= (short) b;
            if (type == ConvertTypes[ConvertTypeUShort]) return (ushort)a >= (ushort) b;
            if (type == ConvertTypes[ConvertTypeInt]) return (int)a >= (int) b;
            if (type == ConvertTypes[ConvertTypeUint]) return (uint)a >= (uint) b;
            if (type == ConvertTypes[ConvertTypeLong]) return (long)a >= (long) b;
            if (type == ConvertTypes[ConvertTypeULong]) return (ulong)a >= (ulong) b;
            if (type == ConvertTypes[ConvertTypeFloat]) return  (float)a >= (float) b;
            if (type == ConvertTypes[ConvertTypeDouble]) return (double)a >= (double) b;
            if (type == ConvertTypes[ConvertTypeDecimal]) return (decimal)a >= (decimal) b;
            if (type == ConvertTypes[ConvertTypeDateTime]) return (DateTime)a >= (DateTime) b;
            if (type == ConvertTypes[ConvertTypeString]) return String.CompareOrdinal((string)a, (string) b) >= 0;
            if (type == ConvertTypes[ConvertTypeGuid]) return String.CompareOrdinal(a.ToString(), b.ToString()) >= 0;
            if (type == ConvertTypes[ConvertTypeByteArray]) return ByteArrayIsGreater((byte[])a, (byte[])b,true);
            if (type == ConvertTypes[ConvertTypeCharArray]) return CharArrayIsGreater((char[])a, (char[])b,true);
            // if (type == ConvertTypes[ConvertTypeJToken]) return (JToken)a >= (JToken) b;
            // if (type == ConvertTypes[ConvertTypeXmlDocument]) return (XmlDocument)a >= (XmlDocument) b;
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return (TimeSpan)a >= (TimeSpan) b;

            throw new ArgumentOutOfRangeException(nameof(type), a, null);
        }

        public static bool GreaterThanOrEqual(DataType.ETypeCode typeCode,  object value1, object value2)
        {
            switch (typeCode)
            {

                case DataType.ETypeCode.Unknown:
                case DataType.ETypeCode.Json:
                case DataType.ETypeCode.Xml:
                    throw new Exception($"Cannot compare {typeCode} types.");
                case DataType.ETypeCode.CharArray:
                    return CharArrayIsGreater((char[])value1, (char[])value2,true);
                case DataType.ETypeCode.Binary:
                    return ByteArrayIsGreater((byte[])value1, (byte[])value2,true);
                case DataType.ETypeCode.String:
                case DataType.ETypeCode.Text:
                    return string.CompareOrdinal((string)value1, (string)value2) >= 0;
                case DataType.ETypeCode.Guid:
                    return string.CompareOrdinal(value1.ToString(), value2.ToString()) >= 0;
                case DataType.ETypeCode.DateTime:
                    return (DateTime) value1 >= (DateTime) value2;
                case DataType.ETypeCode.Boolean:
                    return BoolIsGreaterThanOrEqual((bool) value1, (bool) value2);
                case DataType.ETypeCode.Time:
                    return (TimeSpan) value1 >= (TimeSpan) value2;
                case DataType.ETypeCode.Byte:
                    return ((byte) value1 >= (byte) value2);
                case DataType.ETypeCode.SByte:
                    return ((sbyte) value1 >= (sbyte) value2);
                case DataType.ETypeCode.UInt16:
                    return ((ushort) value1 >= (ushort) value2);
                case DataType.ETypeCode.UInt32:
                    return (uint) value1 >= (uint) value2;
                case DataType.ETypeCode.UInt64:
                    return (ulong) value1 >= (ulong) value2;
                case DataType.ETypeCode.Int16:
                    return ((short) value1 >= (short) value2);
                case DataType.ETypeCode.Int32:
                    return (int) value1 >= (int) value2;
                case DataType.ETypeCode.Int64:
                    return (long) value1 >= (long) value2;
                case DataType.ETypeCode.Decimal:
                    return (decimal) value1 >= (decimal) value2;
                case DataType.ETypeCode.Double:
                    return (double) value1 >= (double) value2;
                case DataType.ETypeCode.Single:
                    return (float) value1 >= (float) value2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }

        
        public static bool LessThan(object a, object b)
        {
            var type = a?.GetType();
            return LessThan(type, a, b);
        }
        
        public static bool LessThan(Type type, object a, object b)
        {
            if (type == ConvertTypes[ConvertTypeBool]) return BoolIsLessThan((bool)a, (bool)b);
            if (type == ConvertTypes[ConvertTypeSbyte]) return (sbyte)a < (sbyte) b;
            if (type == ConvertTypes[ConvertTypeByte]) return (byte)a < (byte) b;
            if (type == ConvertTypes[ConvertTypeShort]) return (short)a < (short) b;
            if (type == ConvertTypes[ConvertTypeUShort]) return (ushort)a < (ushort) b;
            if (type == ConvertTypes[ConvertTypeInt]) return (int)a < (int) b;
            if (type == ConvertTypes[ConvertTypeUint]) return (uint)a < (uint) b;
            if (type == ConvertTypes[ConvertTypeLong]) return (long)a < (long) b;
            if (type == ConvertTypes[ConvertTypeULong]) return (ulong)a < (ulong) b;
            if (type == ConvertTypes[ConvertTypeFloat]) return  (float)a < (float) b;
            if (type == ConvertTypes[ConvertTypeDouble]) return (double)a < (double) b;
            if (type == ConvertTypes[ConvertTypeDecimal]) return (decimal)a < (decimal) b;
            if (type == ConvertTypes[ConvertTypeDateTime]) return (DateTime)a < (DateTime) b;
            if (type == ConvertTypes[ConvertTypeString]) return String.CompareOrdinal((string)a, (string) b) < 0;
            if (type == ConvertTypes[ConvertTypeGuid]) return String.CompareOrdinal(a.ToString(), b.ToString()) < 0;
            if (type == ConvertTypes[ConvertTypeByteArray]) return ByteArrayIsLessThan((byte[])a, (byte[])b,false);
            if (type == ConvertTypes[ConvertTypeCharArray]) return CharArrayIsLessThan((char[])a, (char[])b,false);
            // if (type == ConvertTypes[ConvertTypeJToken]) return (JToken)a < (JToken) b;
            // if (type == ConvertTypes[ConvertTypeXmlDocument]) return (XmlDocument)a < (XmlDocument) b;
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return (TimeSpan)a < (TimeSpan) b;

            throw new ArgumentOutOfRangeException(nameof(type), a, null);
        }

        public static bool LessThan(DataType.ETypeCode typeCode,  object value1, object value2)
        {
            switch (typeCode)
            {

                case DataType.ETypeCode.Unknown:
                case DataType.ETypeCode.Json:
                case DataType.ETypeCode.Xml:
                    throw new Exception($"Cannot compare {typeCode} types.");
                case DataType.ETypeCode.Binary:
                    return ByteArrayIsLessThan((byte[])value1, (byte[])value2,false);
                case DataType.ETypeCode.CharArray:
                    return CharArrayIsLessThan((char[])value1, (char[])value2,false);
                case DataType.ETypeCode.String:
                case DataType.ETypeCode.Text:
                    return string.CompareOrdinal((string)value1, (string)value2) < 0;
                case DataType.ETypeCode.Guid:
                    return string.CompareOrdinal(value1.ToString(), value2.ToString()) < 0;
                case DataType.ETypeCode.DateTime:
                    return (DateTime) value1 < (DateTime) value2;
                case DataType.ETypeCode.Boolean:
                    return BoolIsLessThan((bool) value1, (bool) value2);
                case DataType.ETypeCode.Time:
                    return (TimeSpan) value1 < (TimeSpan) value2;
                case DataType.ETypeCode.Byte:
                    return ((byte) value1 < (byte) value2);
                case DataType.ETypeCode.SByte:
                    return ((sbyte) value1 < (sbyte) value2);
                case DataType.ETypeCode.UInt16:
                    return ((ushort) value1 < (ushort) value2);
                case DataType.ETypeCode.UInt32:
                    return (uint) value1 < (uint) value2;
                case DataType.ETypeCode.UInt64:
                    return (ulong) value1 < (ulong) value2;
                case DataType.ETypeCode.Int16:
                    return ((short) value1 < (short) value2);
                case DataType.ETypeCode.Int32:
                    return (int) value1 < (int) value2;
                case DataType.ETypeCode.Int64:
                    return (long) value1 < (long) value2;
                case DataType.ETypeCode.Decimal:
                    return (decimal) value1 < (decimal) value2;
                case DataType.ETypeCode.Double:
                    return (double) value1 < (double) value2;
                case DataType.ETypeCode.Single:
                    return (float) value1 < (float) value2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }

        
        public static bool LessThanOrEqual(object a, object b)
        {
            var type = a?.GetType();
            return LessThanOrEqual(type, a, b);
        }
        
        public static bool LessThanOrEqual(Type type, object a, object b)
        {
            if (type == ConvertTypes[ConvertTypeBool]) return BoolIsLessThanOrEqual((bool)a, (bool)b);
            if (type == ConvertTypes[ConvertTypeSbyte]) return (sbyte)a <= (sbyte) b;
            if (type == ConvertTypes[ConvertTypeByte]) return (byte)a <= (byte) b;
            if (type == ConvertTypes[ConvertTypeShort]) return (short)a <= (short) b;
            if (type == ConvertTypes[ConvertTypeUShort]) return (ushort)a <= (ushort) b;
            if (type == ConvertTypes[ConvertTypeInt]) return (int)a <= (int) b;
            if (type == ConvertTypes[ConvertTypeUint]) return (uint)a <= (uint) b;
            if (type == ConvertTypes[ConvertTypeLong]) return (long)a <= (long) b;
            if (type == ConvertTypes[ConvertTypeULong]) return (ulong)a <= (ulong) b;
            if (type == ConvertTypes[ConvertTypeFloat]) return  (float)a <= (float) b;
            if (type == ConvertTypes[ConvertTypeDouble]) return (double)a <= (double) b;
            if (type == ConvertTypes[ConvertTypeDecimal]) return (decimal)a <= (decimal) b;
            if (type == ConvertTypes[ConvertTypeDateTime]) return (DateTime)a <= (DateTime) b;
            if (type == ConvertTypes[ConvertTypeString]) return String.CompareOrdinal((string)a, (string) b) <= 0;
            if (type == ConvertTypes[ConvertTypeGuid]) return String.CompareOrdinal(a.ToString(), b.ToString()) <= 0;
            if (type == ConvertTypes[ConvertTypeByteArray]) return ByteArrayIsLessThan((byte[])a, (byte[])b,true);
            if (type == ConvertTypes[ConvertTypeCharArray]) return CharArrayIsLessThan((char[])a, (char[])b,true);
            // if (type == ConvertTypes[ConvertTypeJToken]) return (JToken)a <= (JToken) b;
            // if (type == ConvertTypes[ConvertTypeXmlDocument]) return (XmlDocument)a <= (XmlDocument) b;
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return (TimeSpan)a <= (TimeSpan) b;

            throw new ArgumentOutOfRangeException(nameof(type), a, null);
        }

        public static bool LessThanOrEqual(DataType.ETypeCode typeCode,  object value1, object value2)
        {
            switch (typeCode)
            {
                case DataType.ETypeCode.Unknown:
                case DataType.ETypeCode.Json:
                case DataType.ETypeCode.Xml:
                    throw new Exception($"Cannot compare {typeCode} types.");
                case DataType.ETypeCode.Binary:
                    return ByteArrayIsLessThan((byte[])value1, (byte[])value2,true);
                case DataType.ETypeCode.CharArray:
                    return CharArrayIsLessThan((char[])value1, (char[])value2,true);
                case DataType.ETypeCode.String:
                case DataType.ETypeCode.Text:
                    return string.CompareOrdinal((string)value1, (string)value2) <= 0;
                case DataType.ETypeCode.Guid:
                    return string.CompareOrdinal(value1.ToString(), value2.ToString()) <= 0;
                case DataType.ETypeCode.DateTime:
                    return (DateTime) value1 <= (DateTime) value2;
                case DataType.ETypeCode.Boolean:
                    return BoolIsLessThanOrEqual((bool) value1, (bool) value2);
                case DataType.ETypeCode.Time:
                    return (TimeSpan) value1 <= (TimeSpan) value2;
                case DataType.ETypeCode.Byte:
                    return ((byte) value1 <= (byte) value2);
                case DataType.ETypeCode.SByte:
                    return ((sbyte) value1 <= (sbyte) value2);
                case DataType.ETypeCode.UInt16:
                    return ((ushort) value1 <= (ushort) value2);
                case DataType.ETypeCode.UInt32:
                    return (uint) value1 <= (uint) value2;
                case DataType.ETypeCode.UInt64:
                    return (ulong) value1 <= (ulong) value2;
                case DataType.ETypeCode.Int16:
                    return ((short) value1 <= (short) value2);
                case DataType.ETypeCode.Int32:
                    return (int) value1 <= (int) value2;
                case DataType.ETypeCode.Int64:
                    return (long) value1 <= (long) value2;
                case DataType.ETypeCode.Decimal:
                    return (decimal) value1 <= (decimal) value2;
                case DataType.ETypeCode.Double:
                    return (double) value1 <= (double) value2;
                case DataType.ETypeCode.Single:
                    return (float) value1 <= (float) value2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }

        public static object Add(object a, object b)
        {
            var type = a?.GetType();
            return Add(type, a, b);
        }
        
        public static object Add(Type type, object a, object b)
        {
            // if (type == ConvertTypes[ConvertTypeBool]) return (bool)a + (bool)b;
            if (type == ConvertTypes[ConvertTypeSbyte]) return (sbyte)a + (sbyte) b;
            if (type == ConvertTypes[ConvertTypeByte]) return (byte)a + (byte) b;
            if (type == ConvertTypes[ConvertTypeShort]) return (short)a + (short) b;
            if (type == ConvertTypes[ConvertTypeUShort]) return (ushort)a + (ushort) b;
            if (type == ConvertTypes[ConvertTypeInt]) return (int)a + (int) b;
            if (type == ConvertTypes[ConvertTypeUint]) return (uint)a + (uint) b;
            if (type == ConvertTypes[ConvertTypeLong]) return (long)a + (long) b;
            if (type == ConvertTypes[ConvertTypeULong]) return (ulong)a + (ulong) b;
            if (type == ConvertTypes[ConvertTypeFloat]) return  (float)a + (float) b;
            if (type == ConvertTypes[ConvertTypeDouble]) return (double)a + (double) b;
            if (type == ConvertTypes[ConvertTypeDecimal]) return (decimal)a + (decimal) b;
            // if (type == ConvertTypes[ConvertTypeDateTime]) return (DateTime)a + (DateTime) b;
            // if (type == ConvertTypes[ConvertTypeString]) return (string)a + (string) b;
            // if (type == ConvertTypes[ConvertTypeByteArray]) return (byte[])a + (byte[]) b;
            // if (type == ConvertTypes[ConvertTypeCharArray]) return (char[])a + (char[]) b;
            // if (type == ConvertTypes[ConvertTypeJToken]) return (JToken)a + (JToken) b;
            // if (type == ConvertTypes[ConvertTypeXmlDocument]) return (XmlDocument)a + (XmlDocument) b;
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return (TimeSpan)a + (TimeSpan) b;

            throw new ArgumentOutOfRangeException(nameof(type), a, null);
        }

        public static object Add(DataType.ETypeCode typeCode,  object value1, object value2)
        {
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                case DataType.ETypeCode.CharArray:
                case DataType.ETypeCode.String:
                case DataType.ETypeCode.Text:
                case DataType.ETypeCode.DateTime:
                case DataType.ETypeCode.Boolean:
                case DataType.ETypeCode.Unknown:
                case DataType.ETypeCode.Guid:
                case DataType.ETypeCode.Json:
                case DataType.ETypeCode.Xml:
                    throw new Exception($"Cannot add {typeCode} types.");
                case DataType.ETypeCode.Time:
                    return (TimeSpan) value1 + (TimeSpan) value2;
                case DataType.ETypeCode.Byte:
                    return (byte)((byte) value1 + (byte) value2);
                case DataType.ETypeCode.SByte:
                    return (sbyte)((sbyte) value1 + (sbyte) value2);
                case DataType.ETypeCode.UInt16:
                    return (ushort)((ushort) value1 + (ushort) value2);
                case DataType.ETypeCode.UInt32:
                    return (uint) value1 + (uint) value2;
                case DataType.ETypeCode.UInt64:
                    return (ulong) value1 + (ulong) value2;
                case DataType.ETypeCode.Int16:
                    return (short)((short) value1 + (short) value2);
                case DataType.ETypeCode.Int32:
                    return (int) value1 + (int) value2;
                case DataType.ETypeCode.Int64:
                    return (long) value1 + (long) value2;
                case DataType.ETypeCode.Decimal:
                    return (decimal) value1 + (decimal) value2;
                case DataType.ETypeCode.Double:
                    return (double) value1 + (double) value2;
                case DataType.ETypeCode.Single:
                    return (float) value1 + (float) value2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }

        public static object Subtract(object a, object b)
        {
            var type = a?.GetType();
            return Subtract(type, a, b);
        }
        
        public static object Subtract(Type type, object a, object b)
        {
            // if (type == ConvertTypes[ConvertTypeBool]) return (bool)a - (bool)b;
            if (type == ConvertTypes[ConvertTypeSbyte]) return (sbyte)a - (sbyte) b;
            if (type == ConvertTypes[ConvertTypeByte]) return (byte)a - (byte) b;
            if (type == ConvertTypes[ConvertTypeShort]) return (short)a - (short) b;
            if (type == ConvertTypes[ConvertTypeUShort]) return (ushort)a - (ushort) b;
            if (type == ConvertTypes[ConvertTypeInt]) return (int)a - (int) b;
            if (type == ConvertTypes[ConvertTypeUint]) return (uint)a - (uint) b;
            if (type == ConvertTypes[ConvertTypeLong]) return (long)a - (long) b;
            if (type == ConvertTypes[ConvertTypeULong]) return (ulong)a - (ulong) b;
            if (type == ConvertTypes[ConvertTypeFloat]) return  (float)a - (float) b;
            if (type == ConvertTypes[ConvertTypeDouble]) return (double)a - (double) b;
            if (type == ConvertTypes[ConvertTypeDecimal]) return (decimal)a - (decimal) b;
            // if (type == ConvertTypes[ConvertTypeDateTime]) return (DateTime)a - (DateTime) b;
            // if (type == ConvertTypes[ConvertTypeString]) return (string)a - (string) b;
            // if (type == ConvertTypes[ConvertTypeByteArray]) return (byte[])a - (byte[]) b;
            // if (type == ConvertTypes[ConvertTypeCharArray]) return (char[])a - (char[]) b;
            // if (type == ConvertTypes[ConvertTypeJToken]) return (JToken)a - (JToken) b;
            // if (type == ConvertTypes[ConvertTypeXmlDocument]) return (XmlDocument)a - (XmlDocument) b;
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return (TimeSpan)a - (TimeSpan) b;

            throw new ArgumentOutOfRangeException(nameof(type), a, null);
        }

        public static object Subtract(DataType.ETypeCode typeCode,  object value1, object value2)
        {
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                case DataType.ETypeCode.CharArray:
                case DataType.ETypeCode.String:
                case DataType.ETypeCode.Text:
                case DataType.ETypeCode.DateTime:
                case DataType.ETypeCode.Boolean:
                case DataType.ETypeCode.Unknown:
                case DataType.ETypeCode.Guid:
                case DataType.ETypeCode.Json:
                case DataType.ETypeCode.Xml:
                    throw new Exception($"Cannot subtract {typeCode} types.");
                case DataType.ETypeCode.Time:
                    return (TimeSpan) value1 - (TimeSpan) value2;
                case DataType.ETypeCode.Byte:
                    return (byte)((byte) value1 - (byte) value2);
                case DataType.ETypeCode.SByte:
                    return (sbyte)((sbyte) value1 - (sbyte) value2);
                case DataType.ETypeCode.UInt16:
                    return (ushort)((ushort) value1 - (ushort) value2);
                case DataType.ETypeCode.UInt32:
                    return (uint) value1 - (uint) value2;
                case DataType.ETypeCode.UInt64:
                    return (ulong) value1 - (ulong) value2;
                case DataType.ETypeCode.Int16:
                    return (short)((short) value1 - (short) value2);
                case DataType.ETypeCode.Int32:
                    return (int) value1 - (int) value2;
                case DataType.ETypeCode.Int64:
                    return (long) value1 - (long) value2;
                case DataType.ETypeCode.Decimal:
                    return (decimal) value1 - (decimal) value2;
                case DataType.ETypeCode.Double:
                    return (double) value1 - (double) value2;
                case DataType.ETypeCode.Single:
                    return (float) value1 - (float) value2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }

        public static object Divide(object a, object b)
        {
            var type = a?.GetType();
            return Divide(type, a, b);
        }
        
        public static object Divide(Type type, object a, object b)
        {
            // if (type == ConvertTypes[ConvertTypeBool]) return (bool)a / (bool)b;
            if (type == ConvertTypes[ConvertTypeSbyte]) return (sbyte)a / (sbyte) b;
            if (type == ConvertTypes[ConvertTypeByte]) return (byte)a / (byte) b;
            if (type == ConvertTypes[ConvertTypeShort]) return (short)a / (short) b;
            if (type == ConvertTypes[ConvertTypeUShort]) return (ushort)a / (ushort) b;
            if (type == ConvertTypes[ConvertTypeInt]) return (int)a / (int) b;
            if (type == ConvertTypes[ConvertTypeUint]) return (uint)a / (uint) b;
            if (type == ConvertTypes[ConvertTypeLong]) return (long)a / (long) b;
            if (type == ConvertTypes[ConvertTypeULong]) return (ulong)a / (ulong) b;
            if (type == ConvertTypes[ConvertTypeFloat]) return  (float)a / (float) b;
            if (type == ConvertTypes[ConvertTypeDouble]) return (double)a / (double) b;
            if (type == ConvertTypes[ConvertTypeDecimal]) return (decimal)a / (decimal) b;
            // if (type == ConvertTypes[ConvertTypeDateTime]) return (DateTime)a / (DateTime) b;
            // if (type == ConvertTypes[ConvertTypeString]) return (string)a / (string) b;
            // if (type == ConvertTypes[ConvertTypeByteArray]) return (byte[])a / (byte[]) b;
            // if (type == ConvertTypes[ConvertTypeCharArray]) return (char[])a / (char[]) b;
            // if (type == ConvertTypes[ConvertTypeJToken]) return (JToken)a / (JToken) b;
            // if (type == ConvertTypes[ConvertTypeXmlDocument]) return (XmlDocument)a / (XmlDocument) b;
            // if (type == ConvertTypes[ConvertTypeTimeSpan]) return (TimeSpan)a / (TimeSpan) b;

            throw new ArgumentOutOfRangeException(nameof(type), a, null);
        }

        public static object Divide(DataType.ETypeCode typeCode,  object value1, object value2)
        {
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                case DataType.ETypeCode.CharArray:
                case DataType.ETypeCode.String:
                case DataType.ETypeCode.Text:
                case DataType.ETypeCode.DateTime:
                case DataType.ETypeCode.Boolean:
                case DataType.ETypeCode.Unknown:
                case DataType.ETypeCode.Time:
                case DataType.ETypeCode.Guid:
                case DataType.ETypeCode.Json:
                case DataType.ETypeCode.Xml:
                    throw new Exception($"Cannot add {typeCode} types.");
                case DataType.ETypeCode.Byte:
                    return (byte)((byte) value1 / (byte) value2);
                case DataType.ETypeCode.SByte:
                    return (sbyte)((sbyte) value1 / (sbyte) value2);
                case DataType.ETypeCode.UInt16:
                    return (ushort)((ushort) value1 / (ushort) value2);
                case DataType.ETypeCode.UInt32:
                    return (uint) value1 / (uint) value2;
                case DataType.ETypeCode.UInt64:
                    return (ulong) value1 / (ulong) value2;
                case DataType.ETypeCode.Int16:
                    return (short)((short) value1 / (short) value2);
                case DataType.ETypeCode.Int32:
                    return (int) value1 / (int) value2;
                case DataType.ETypeCode.Int64:
                    return (long) value1 / (long) value2;
                case DataType.ETypeCode.Decimal:
                    return (decimal) value1 / (decimal) value2;
                case DataType.ETypeCode.Double:
                    return (double) value1 / (double) value2;
                case DataType.ETypeCode.Single:
                    return (float) value1 / (float) value2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }

        public static object Multiply(object a, object b)
        {
            var type = a?.GetType();
            return Multiply(type, a, b);
        }
        
        public static object Multiply(Type type, object a, object b)
        {
            // if (type == ConvertTypes[ConvertTypeBool]) return (bool)a * (bool)b;
            if (type == ConvertTypes[ConvertTypeSbyte]) return (sbyte)a * (sbyte) b;
            if (type == ConvertTypes[ConvertTypeByte]) return (byte)a * (byte) b;
            if (type == ConvertTypes[ConvertTypeShort]) return (short)a * (short) b;
            if (type == ConvertTypes[ConvertTypeUShort]) return (ushort)a * (ushort) b;
            if (type == ConvertTypes[ConvertTypeInt]) return (int)a * (int) b;
            if (type == ConvertTypes[ConvertTypeUint]) return (uint)a * (uint) b;
            if (type == ConvertTypes[ConvertTypeLong]) return (long)a * (long) b;
            if (type == ConvertTypes[ConvertTypeULong]) return (ulong)a * (ulong) b;
            if (type == ConvertTypes[ConvertTypeFloat]) return  (float)a * (float) b;
            if (type == ConvertTypes[ConvertTypeDouble]) return (double)a * (double) b;
            if (type == ConvertTypes[ConvertTypeDecimal]) return (decimal)a * (decimal) b;
            // if (type == ConvertTypes[ConvertTypeDateTime]) return (DateTime)a * (DateTime) b;
            // if (type == ConvertTypes[ConvertTypeString]) return (string)a * (string) b;
            // if (type == ConvertTypes[ConvertTypeByteArray]) return (byte[])a * (byte[]) b;
            // if (type == ConvertTypes[ConvertTypeCharArray]) return (char[])a * (char[]) b;
            // if (type == ConvertTypes[ConvertTypeJToken]) return (JToken)a * (JToken) b;
            // if (type == ConvertTypes[ConvertTypeXmlDocument]) return (XmlDocument)a * (XmlDocument) b;
            // if (type == ConvertTypes[ConvertTypeTimeSpan]) return (TimeSpan)a * (TimeSpan) b;

            throw new ArgumentOutOfRangeException(nameof(type), a, null);
        }

        public static object Multiply(DataType.ETypeCode typeCode,  object value1, object value2)
        {
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                case DataType.ETypeCode.CharArray:
                case DataType.ETypeCode.String:
                case DataType.ETypeCode.Text:
                case DataType.ETypeCode.DateTime:
                case DataType.ETypeCode.Boolean:
                case DataType.ETypeCode.Unknown:
                case DataType.ETypeCode.Guid:
                case DataType.ETypeCode.Json:
                case DataType.ETypeCode.Time:
                case DataType.ETypeCode.Xml:
                    throw new Exception($"Cannot add {typeCode} types.");
                case DataType.ETypeCode.Byte:
                    return (byte)((byte) value1 * (byte) value2);
                case DataType.ETypeCode.SByte:
                    return (sbyte)((sbyte) value1 * (sbyte) value2);
                case DataType.ETypeCode.UInt16:
                    return (ushort)((ushort) value1 * (ushort) value2);
                case DataType.ETypeCode.UInt32:
                    return (uint) value1 * (uint) value2;
                case DataType.ETypeCode.UInt64:
                    return (ulong) value1 * (ulong) value2;
                case DataType.ETypeCode.Int16:
                    return (short)((short) value1 * (short) value2);
                case DataType.ETypeCode.Int32:
                    return (int) value1 * (int) value2;
                case DataType.ETypeCode.Int64:
                    return (long) value1 * (long) value2;
                case DataType.ETypeCode.Decimal:
                    return (decimal) value1 * (decimal) value2;
                case DataType.ETypeCode.Double:
                    return (double) value1 * (double) value2;
                case DataType.ETypeCode.Single:
                    return (float) value1 * (float) value2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }

        public static object DivideInt(object a, int b)
        {
            var type = a?.GetType();
            return DivideInt(type, a, b);
        }
        
        public static object DivideInt(Type type, object a, int b)
        {
            // if (type == ConvertTypes[ConvertTypeBool]) return (bool)a / (bool)b;
            if (type == ConvertTypes[ConvertTypeSbyte]) return (sbyte)a / b;
            if (type == ConvertTypes[ConvertTypeByte]) return (byte)a / b;
            if (type == ConvertTypes[ConvertTypeShort]) return (short)a / b;
            if (type == ConvertTypes[ConvertTypeUShort]) return (ushort)a / b;
            if (type == ConvertTypes[ConvertTypeInt]) return (int)a / b;
            if (type == ConvertTypes[ConvertTypeUint]) return (uint)a / b;
            if (type == ConvertTypes[ConvertTypeLong]) return (long)a / b;
            if (type == ConvertTypes[ConvertTypeULong]) return (ulong)a / Convert.ToUInt64(b);
            if (type == ConvertTypes[ConvertTypeFloat]) return  (float)a / b;
            if (type == ConvertTypes[ConvertTypeDouble]) return (double)a / b;
            if (type == ConvertTypes[ConvertTypeDecimal]) return (decimal)a / b;
            // if (type == ConvertTypes[ConvertTypeDateTime]) return (DateTime)a / (DateTime) b;
            // if (type == ConvertTypes[ConvertTypeString]) return (string)a / (string) b;
            // if (type == ConvertTypes[ConvertTypeByteArray]) return (byte[])a / (byte[]) b;
            // if (type == ConvertTypes[ConvertTypeCharArray]) return (char[])a / (char[]) b;
            // if (type == ConvertTypes[ConvertTypeJToken]) return (JToken)a / (JToken) b;
            // if (type == ConvertTypes[ConvertTypeXmlDocument]) return (XmlDocument)a / (XmlDocument) b;
            // if (type == ConvertTypes[ConvertTypeTimeSpan]) return (TimeSpan)a / (TimeSpan) b;

            throw new ArgumentOutOfRangeException(nameof(type), a, null);
        }

        public static object DivideInt(DataType.ETypeCode typeCode,  object value1, int value2)
        {
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                case DataType.ETypeCode.CharArray:
                case DataType.ETypeCode.String:
                case DataType.ETypeCode.Text:
                case DataType.ETypeCode.DateTime:
                case DataType.ETypeCode.Boolean:
                case DataType.ETypeCode.Unknown:
                case DataType.ETypeCode.Guid:
                case DataType.ETypeCode.Json:
                case DataType.ETypeCode.Time:
                case DataType.ETypeCode.Xml:
                    throw new Exception($"Cannot add {typeCode} types.");
                case DataType.ETypeCode.Byte:
                    return (byte)((byte) value1 / value2);
                case DataType.ETypeCode.SByte:
                    return (sbyte)((sbyte) value1 / value2);
                case DataType.ETypeCode.UInt16:
                    return (ushort)((ushort) value1 / value2);
                case DataType.ETypeCode.UInt32:
                    return (uint) value1 / value2;
                case DataType.ETypeCode.UInt64:
                    return (ulong) value1 / Convert.ToUInt64(value2);
                case DataType.ETypeCode.Int16:
                    return (short)((short) value1 / value2);
                case DataType.ETypeCode.Int32:
                    return (int) value1 / value2;
                case DataType.ETypeCode.Int64:
                    return (long) value1 / value2;
                case DataType.ETypeCode.Decimal:
                    return (decimal) value1 / value2;
                case DataType.ETypeCode.Double:
                    return (double) value1 / value2;
                case DataType.ETypeCode.Single:
                    return (float) value1 / value2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }          

       public static object Negate(object a)
        {
            var type = a?.GetType();
            return Negate(type, a);
        }
        
        public static object Negate(Type type, object a)
        {
            // if (type == ConvertTypes[ConvertTypeBool]) return (bool)a / (bool)b;
            if (type == ConvertTypes[ConvertTypeSbyte]) return (sbyte)a * -1;
            if (type == ConvertTypes[ConvertTypeByte]) return (byte)a * -1;
            if (type == ConvertTypes[ConvertTypeShort]) return (short)a * -1;
            // if (type == ConvertTypes[ConvertTypeUShort]) return (ushort)a * -1;
            if (type == ConvertTypes[ConvertTypeInt]) return (int)a * -1;
            //if (type == ConvertTypes[ConvertTypeUint]) return (uint)a * -1;
            if (type == ConvertTypes[ConvertTypeLong]) return (long)a * -1;
            //if (type == ConvertTypes[ConvertTypeULong]) return (ulong)a -1;
            if (type == ConvertTypes[ConvertTypeFloat]) return  (float)a * -1;
            if (type == ConvertTypes[ConvertTypeDouble]) return (double)a * -1;
            if (type == ConvertTypes[ConvertTypeDecimal]) return (decimal)a * -1;
            // if (type == ConvertTypes[ConvertTypeDateTime]) return (DateTime)a / (DateTime) b;
            // if (type == ConvertTypes[ConvertTypeString]) return (string)a / (string) b;
            // if (type == ConvertTypes[ConvertTypeByteArray]) return (byte[])a / (byte[]) b;
            // if (type == ConvertTypes[ConvertTypeCharArray]) return (char[])a / (char[]) b;
            // if (type == ConvertTypes[ConvertTypeJToken]) return (JToken)a / (JToken) b;
            // if (type == ConvertTypes[ConvertTypeXmlDocument]) return (XmlDocument)a / (XmlDocument) b;
            // if (type == ConvertTypes[ConvertTypeTimeSpan]) return (TimeSpan)a / (TimeSpan) b;

            throw new ArgumentOutOfRangeException(nameof(type), a, null);
        }

        public static object Negate(DataType.ETypeCode typeCode,  object value1)
        {
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                case DataType.ETypeCode.CharArray:
                case DataType.ETypeCode.String:
                case DataType.ETypeCode.Text:
                case DataType.ETypeCode.DateTime:
                case DataType.ETypeCode.Boolean:
                case DataType.ETypeCode.Unknown:
                case DataType.ETypeCode.Guid:
                case DataType.ETypeCode.Json:
                case DataType.ETypeCode.Time:
                case DataType.ETypeCode.Xml:
                case DataType.ETypeCode.UInt16:
                case DataType.ETypeCode.UInt32:
                case DataType.ETypeCode.UInt64:
                    throw new Exception($"Cannot negate {typeCode} types.");
                case DataType.ETypeCode.Byte:
                    return (byte)((byte) value1 * -1);
                case DataType.ETypeCode.SByte:
                    return (sbyte)((sbyte) value1 * -1);
                case DataType.ETypeCode.Int16:
                    return (short)((short) value1 * -1);
                case DataType.ETypeCode.Int32:
                    return (int) value1 * -1;
                case DataType.ETypeCode.Int64:
                    return (long) value1 * -1;
                case DataType.ETypeCode.Decimal:
                    return (decimal) value1 * -1;
                case DataType.ETypeCode.Double:
                    return (double) value1 * -1;
                case DataType.ETypeCode.Single:
                    return (float) value1 * -1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }          

       public static object Increment(object a)
        {
            var type = a?.GetType();
            return Increment(type, a);
        }
        
        public static object Increment(Type type, object a)
        {
            // if (type == ConvertTypes[ConvertTypeBool]) return (bool)a / (bool)b;
            if (type == ConvertTypes[ConvertTypeSbyte]) return (sbyte)a + 1;
            if (type == ConvertTypes[ConvertTypeByte]) return (byte)a + 1;
            if (type == ConvertTypes[ConvertTypeShort]) return (short)a + 1;
            if (type == ConvertTypes[ConvertTypeUShort]) return (ushort)a + 1;
            if (type == ConvertTypes[ConvertTypeInt]) return (int)a + 1;
            if (type == ConvertTypes[ConvertTypeUint]) return (uint)a + 1;
            if (type == ConvertTypes[ConvertTypeLong]) return (long)a + 1;
            if (type == ConvertTypes[ConvertTypeULong]) return (ulong)a -1;
            if (type == ConvertTypes[ConvertTypeFloat]) return  (float)a + 1;
            if (type == ConvertTypes[ConvertTypeDouble]) return (double)a + 1;
            if (type == ConvertTypes[ConvertTypeDecimal]) return (decimal)a + 1;
            // if (type == ConvertTypes[ConvertTypeDateTime]) return (DateTime)a / (DateTime) b;
            // if (type == ConvertTypes[ConvertTypeString]) return (string)a / (string) b;
            // if (type == ConvertTypes[ConvertTypeByteArray]) return (byte[])a / (byte[]) b;
            // if (type == ConvertTypes[ConvertTypeCharArray]) return (char[])a / (char[]) b;
            // if (type == ConvertTypes[ConvertTypeJToken]) return (JToken)a / (JToken) b;
            // if (type == ConvertTypes[ConvertTypeXmlDocument]) return (XmlDocument)a / (XmlDocument) b;
            // if (type == ConvertTypes[ConvertTypeTimeSpan]) return (TimeSpan)a / (TimeSpan) b;

            throw new ArgumentOutOfRangeException(nameof(type), a, null);
        }

        public static object Increment(DataType.ETypeCode typeCode,  object value1)
        {
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                case DataType.ETypeCode.CharArray:
                case DataType.ETypeCode.String:
                case DataType.ETypeCode.Text:
                case DataType.ETypeCode.DateTime:
                case DataType.ETypeCode.Boolean:
                case DataType.ETypeCode.Unknown:
                case DataType.ETypeCode.Guid:
                case DataType.ETypeCode.Json:
                case DataType.ETypeCode.Time:
                case DataType.ETypeCode.Xml:
                    throw new Exception($"Cannot negate {typeCode} types.");
                case DataType.ETypeCode.Byte:
                    return (byte)((byte) value1 + 1);
                case DataType.ETypeCode.SByte:
                    return (sbyte)((sbyte) value1 + 1);
                case DataType.ETypeCode.Int16:
                    return (short)((short) value1 + 1);
                case DataType.ETypeCode.Int32:
                    return (int) value1 + 1;
                case DataType.ETypeCode.Int64:
                    return (long) value1 + 1;
                case DataType.ETypeCode.UInt16:
                    return (ushort) value1 + 1;
                case DataType.ETypeCode.UInt32:
                    return (uint) value1 + 1;
                case DataType.ETypeCode.UInt64:
                    return (ulong) value1 + 1;
                case DataType.ETypeCode.Decimal:
                    return (decimal) value1 + 1;
                case DataType.ETypeCode.Double:
                    return (double) value1 + 1;
                case DataType.ETypeCode.Single:
                    return (float) value1 + 1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }      
 
       public static object Decrement(object a)
        {
            var type = a?.GetType();
            return Decrement(type, a);
        }
        
        public static object Decrement(Type type, object a)
        {
            // if (type == ConvertTypes[ConvertTypeBool]) return (bool)a / (bool)b;
            if (type == ConvertTypes[ConvertTypeSbyte]) return (sbyte)a - 1;
            if (type == ConvertTypes[ConvertTypeByte]) return (byte)a - 1;
            if (type == ConvertTypes[ConvertTypeShort]) return (short)a - 1;
            if (type == ConvertTypes[ConvertTypeUShort]) return (ushort)a - 1;
            if (type == ConvertTypes[ConvertTypeInt]) return (int)a - 1;
            if (type == ConvertTypes[ConvertTypeUint]) return (uint)a - 1;
            if (type == ConvertTypes[ConvertTypeLong]) return (long)a - 1;
            if (type == ConvertTypes[ConvertTypeULong]) return (ulong)a -1;
            if (type == ConvertTypes[ConvertTypeFloat]) return  (float)a - 1;
            if (type == ConvertTypes[ConvertTypeDouble]) return (double)a - 1;
            if (type == ConvertTypes[ConvertTypeDecimal]) return (decimal)a - 1;
            // if (type == ConvertTypes[ConvertTypeDateTime]) return (DateTime)a / (DateTime) b;
            // if (type == ConvertTypes[ConvertTypeString]) return (string)a / (string) b;
            // if (type == ConvertTypes[ConvertTypeByteArray]) return (byte[])a / (byte[]) b;
            // if (type == ConvertTypes[ConvertTypeCharArray]) return (char[])a / (char[]) b;
            // if (type == ConvertTypes[ConvertTypeJToken]) return (JToken)a / (JToken) b;
            // if (type == ConvertTypes[ConvertTypeXmlDocument]) return (XmlDocument)a / (XmlDocument) b;
            // if (type == ConvertTypes[ConvertTypeTimeSpan]) return (TimeSpan)a / (TimeSpan) b;

            throw new ArgumentOutOfRangeException(nameof(type), a, null);
        }

        public static object Decrement(DataType.ETypeCode typeCode,  object value1)
        {
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                case DataType.ETypeCode.CharArray:
                case DataType.ETypeCode.String:
                case DataType.ETypeCode.Text:
                case DataType.ETypeCode.DateTime:
                case DataType.ETypeCode.Boolean:
                case DataType.ETypeCode.Unknown:
                case DataType.ETypeCode.Guid:
                case DataType.ETypeCode.Json:
                case DataType.ETypeCode.Time:
                case DataType.ETypeCode.Xml:
                    throw new Exception($"Cannot negate {typeCode} types.");
                case DataType.ETypeCode.Byte:
                    return (byte)((byte) value1 - 1);
                case DataType.ETypeCode.SByte:
                    return (sbyte)((sbyte) value1 - 1);
                case DataType.ETypeCode.Int16:
                    return (short)((short) value1 - 1);
                case DataType.ETypeCode.Int32:
                    return (int) value1 - 1;
                case DataType.ETypeCode.Int64:
                    return (long) value1 - 1;
                case DataType.ETypeCode.UInt16:
                    return (ushort) value1 - 1;
                case DataType.ETypeCode.UInt32:
                    return (uint) value1 - 1;
                case DataType.ETypeCode.UInt64:
                    return (ulong) value1 - 1;
                case DataType.ETypeCode.Decimal:
                    return (decimal) value1 - 1;
                case DataType.ETypeCode.Double:
                    return (double) value1 - 1;
                case DataType.ETypeCode.Single:
                    return (float) value1 - 1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }      

        public static int Compare(object inputValue, object compareTo)
        {

            var type = SetDataTypes(inputValue, compareTo, out object a, out object b);
            return Compare(type, a, b);
        }

        public static int Compare(Type type, object inputValue, object compareTo)
        {
            if ((inputValue == null || inputValue == DBNull.Value) && (compareTo == null || compareTo == DBNull.Value))
                return 0;

            if (inputValue == null || inputValue == DBNull.Value || compareTo == null || compareTo == DBNull.Value)
                return (inputValue == null || inputValue is DBNull) ? -1 : 1;

            
            if (type == ConvertTypes[ConvertTypeBool]) return ((bool)inputValue).CompareTo((bool)compareTo);
            if (type == ConvertTypes[ConvertTypeSbyte]) return ((sbyte)inputValue).CompareTo((sbyte)compareTo);
            if (type == ConvertTypes[ConvertTypeByte]) return ((byte)inputValue).CompareTo((byte)compareTo);
            if (type == ConvertTypes[ConvertTypeShort]) return ((short)inputValue).CompareTo((short)compareTo);
            if (type == ConvertTypes[ConvertTypeUShort]) return  ((ushort)inputValue).CompareTo((ushort)compareTo);
            if (type == ConvertTypes[ConvertTypeInt]) return ((int)inputValue).CompareTo((int)compareTo);
            if (type == ConvertTypes[ConvertTypeUint]) return ((uint)inputValue).CompareTo((uint)compareTo);
            if (type == ConvertTypes[ConvertTypeLong]) return  ((long)inputValue).CompareTo((long)compareTo);
            if (type == ConvertTypes[ConvertTypeULong]) return ((ulong)inputValue).CompareTo((ulong)compareTo);
            if (type == ConvertTypes[ConvertTypeFloat]) return ((float)inputValue).CompareTo((float)compareTo);
            if (type == ConvertTypes[ConvertTypeDouble]) return ((double)inputValue).CompareTo((double)compareTo);
            if (type == ConvertTypes[ConvertTypeDecimal]) return ((decimal)inputValue).CompareTo((decimal)compareTo);
            if (type == ConvertTypes[ConvertTypeDateTime]) return  ((DateTime)inputValue).CompareTo((DateTime)compareTo);
            if (type == ConvertTypes[ConvertTypeString]) return String.CompareOrdinal(((string)inputValue), (string)compareTo);
            if (type == ConvertTypes[ConvertTypeGuid]) return  ((Guid)inputValue).CompareTo((Guid)compareTo);
            if (type == ConvertTypes[ConvertTypeByteArray]) return ByteArrayCompareTo((byte[])inputValue,(byte[])compareTo);
            if (type == ConvertTypes[ConvertTypeCharArray]) return CharArrayCompareTo((char[])inputValue,(char[])compareTo);
            if (type == ConvertTypes[ConvertTypeJToken]) return  String.CompareOrdinal(inputValue.ToString(), compareTo.ToString());
            if (type == ConvertTypes[ConvertTypeXmlDocument]) return  String.CompareOrdinal(((XmlDocument)inputValue).InnerXml, ((XmlDocument)compareTo).InnerXml);;
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return  ((TimeSpan)inputValue).CompareTo((TimeSpan)compareTo);

            throw new ArgumentOutOfRangeException(nameof(type), inputValue, null);
        }

        public static int Compare(DataType.ETypeCode typeCode, object inputValue, object compareTo)
        {
            if ((inputValue == null || inputValue == DBNull.Value) && (compareTo == null || compareTo == DBNull.Value))
                return 0;

            if (inputValue == null || inputValue == DBNull.Value || compareTo == null || compareTo == DBNull.Value)
                return (inputValue == null || inputValue is DBNull) ? -1 : 1;
            
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                    return ByteArrayCompareTo((byte[])inputValue,(byte[])compareTo);
                case DataType.ETypeCode.Byte:
                    return ((byte)inputValue).CompareTo((byte)compareTo);
                case DataType.ETypeCode.SByte:
                    return ((sbyte)inputValue).CompareTo((sbyte)compareTo);
                case DataType.ETypeCode.UInt16:
                    return ((ushort)inputValue).CompareTo((ushort)compareTo);
                case DataType.ETypeCode.UInt32:
                    return ((uint)inputValue).CompareTo((uint)compareTo);
                case DataType.ETypeCode.UInt64:
                    return ((ulong)inputValue).CompareTo((ulong)compareTo);
                case DataType.ETypeCode.Int16:
                    return ((short)inputValue).CompareTo((short)compareTo);
                case DataType.ETypeCode.Int32:
                    return ((int)inputValue).CompareTo((int)compareTo);
                case DataType.ETypeCode.Int64:
                    return ((long)inputValue).CompareTo((long)compareTo);
                case DataType.ETypeCode.Decimal:
                    return ((decimal)inputValue).CompareTo((decimal)compareTo);
                case DataType.ETypeCode.Double:
                    return ((double)inputValue).CompareTo((double)compareTo);
                case DataType.ETypeCode.Single:
                    return ((float)inputValue).CompareTo((float)compareTo);
                case DataType.ETypeCode.String:
                case DataType.ETypeCode.Text:
                    return string.CompareOrdinal((string) inputValue, (string) compareTo);
                case DataType.ETypeCode.Boolean:
                    return ((bool)inputValue).CompareTo((bool)compareTo);
                case DataType.ETypeCode.DateTime:
                    return ((DateTime)inputValue).CompareTo((DateTime)compareTo);
                case DataType.ETypeCode.Time:
                    return ((TimeSpan)inputValue).CompareTo((TimeSpan)compareTo);
                case DataType.ETypeCode.Guid:
                    return ((Guid)inputValue).CompareTo((Guid)compareTo);
                case DataType.ETypeCode.Unknown:
                    return String.Compare((inputValue.ToString()), compareTo.ToString(), StringComparison.Ordinal);
                case DataType.ETypeCode.Json:
                    return String.CompareOrdinal(inputValue.ToString(), compareTo.ToString());
                case DataType.ETypeCode.Xml:
                    return String.CompareOrdinal(((XmlDocument)inputValue).InnerXml, ((XmlDocument)compareTo).InnerXml);
                case DataType.ETypeCode.Enum:
                    return ((int)inputValue).CompareTo((int)compareTo);
                case DataType.ETypeCode.CharArray:
                    return CharArrayCompareTo((char[])inputValue,(char[])compareTo);
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }
        
        // c# can't compare bool, to create bool logic
        public static bool BoolIsGreaterThan(bool a, bool b) => a && !b;
        public static bool BoolIsGreaterThanOrEqual(bool a, bool b) => a || !b;
        public static bool BoolIsLessThan(bool a, bool b) => !a && b;
        public static bool BoolIsLessThanOrEqual(bool a, bool b) => !a || b;

        public static bool CharArrayIsGreater(char[] a, char[] b, bool orEqual)
        {
            for(var i = 0; i < a.Length; i++)
            {
                if (i > b.Length) return true;
                if(a[i] == b[i]) continue;
                if (a[i] > b[i]) return true;
                return false;
            }

            if (a.Length < b.Length) return false;

            return orEqual;
        }
        public static bool CharArrayIsLessThan(char[] a, char[] b, bool orEqual)
        {
            for(var i = 0; i < a.Length; i++)
            {
                if (i > b.Length) return false;
                if(a[i] == b[i]) continue;
                if (a[i] < b[i]) return true;
                return false;
            }

            if (a.Length < b.Length) return true;
            return orEqual;
        }
        
        public static int CharArrayCompareTo(char[] a, char[] b)
        {
            for(var i = 0; i < a.Length; i++)
            {
                if (i > b.Length) return 1;
                if(a[i] == b[i]) continue;
                if (a[i] > b[i]) return 1;
                return -1;
            }

            if (a.Length < b.Length) return -1;

            return 0;
        }
        
        public static bool ByteArrayIsGreater(byte[] a, byte[] b, bool orEqual)
        {
            for(var i = 0; i < a.Length; i++)
            {
                if (i > b.Length) return true;
                if(a[i] == b[i]) continue;
                if (a[i] > b[i]) return true;
                return false;
            }
            
            if (a.Length < b.Length) return false;
            return orEqual;
        }
        public static bool ByteArrayIsLessThan(byte[] a, byte[] b, bool orEqual)
        {
            for(var i = 0; i < a.Length; i++)
            {
                if (i > b.Length) return false;
                if(a[i] == b[i]) continue;
                if (a[i] < b[i]) return true;
                return false;
            }

            if (a.Length < b.Length) return true;
            return orEqual;
        }
        
        public static int ByteArrayCompareTo(byte[] a, byte[] b)
        {
            for(var i = 0; i < a.Length; i++)
            {
                if (i > b.Length) return 1;
                if(a[i] == b[i]) continue;
                if (a[i] > b[i]) return 1;
                return -1;
            }

            if (a.Length < b.Length) return -1;
            return 0;
        }

        // makes datatypes the same when they are different.
        private static Type SetDataTypes(object a, object b, out object a1, out object b1)
        {
            // if types don't match, attempt to convert to common type.
            if (a == null || b == null)
            {
                a1 = a;
                b1 = b;
                return typeof(string);
            }
            var aType = a.GetType();
            var aTypeCode = Type.GetTypeCode(aType);
            var bType = b.GetType();
            var bTypeCode = Type.GetTypeCode(bType);
            if (aTypeCode < bTypeCode)
            {
                a1 = Operations.Parse(bType, a);
                b1 = b;
                return (bType);
            }
            else
            {
                a1 = a;
                b1 = Operations.Parse(aType, b);
                return (aType);
            }
        }
    }


    public static class Operations<T>
    {
        // Lazy<Func used so expressions are only created when used.  This avoids issues with non-supported 
        // datatypes causing errors when creating the class.
        public static readonly Lazy<Func<T, T, T>> Add = CreateExpressionNumeric(Expression.Add);
        public static readonly Lazy<Func<T, T, T>> Subtract = CreateExpressionNumeric(Expression.Subtract);
        public static readonly Lazy<Func<T, T, T>> Multiply = CreateExpressionNumeric(Expression.Multiply);
        public static readonly Lazy<Func<T, T, T>> Divide = CreateExpressionNumeric(Expression.Divide);
        public static readonly Lazy<Func<T, int, T>> DivideInt = CreateDivideInt();
        public static readonly Lazy<Func<T, T>> Negate = CreateNegate();
        public static readonly Lazy<Func<T, T>> Increment = CreateIncrement();
        public static readonly Lazy<Func<T, T>> Decrement = CreateDecrement();
        
        public static readonly Lazy<Func<T, T, bool>> GreaterThan = CreateCondition(Expression.GreaterThan, new[] {1});
        public static readonly Lazy<Func<T, T, bool>> LessThan = CreateCondition(Expression.LessThan, new[] {-1});
        public static readonly Lazy<Func<T, T, bool>> GreaterThanOrEqual = CreateCondition(Expression.GreaterThanOrEqual, new[] {0,1});
        public static readonly Lazy<Func<T, T, bool>> LessThanOrEqual = CreateCondition(Expression.LessThanOrEqual, new[] {0,-1});
        public static readonly Lazy<Func<T, T, bool>> Equal = CreateCondition(Expression.Equal, new[] {0});
        
        public new static readonly Lazy<Func<T, string>> ToString = CreateToString();
        public static readonly Lazy<Func<object, T>> Parse = CreateParse();
        public static readonly T Zero = default;
        
        public static readonly Lazy<Func<T, T, int>> Compare = CreateCompare();

        public static bool IsNumericType(Type type)
        {   
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
        
        private static Lazy<Func<T,T,T>> CreateExpressionNumeric(Func<Expression, Expression, BinaryExpression> body)
        {
            if (IsNumericType(typeof(T)))
            {
                var p1 = Expression.Parameter(typeof(T), "p1");
                var p2 = Expression.Parameter(typeof(T), "p2");
                    
                var exp = Expression.Lambda<Func<T, T, T>>(body(p1, p2), p1, p2).Compile();
                return new Lazy<Func<T, T, T>>(() => exp);
            }
            else
            {
                return new Lazy<Func<T, T, T>>(() => throw new InvalidCastException($"The data type {typeof(T)} is not numeric."));
            }
        }
        
        private static Lazy<Func<T, int, T>> CreateDivideInt()
        {
            if (IsNumericType(typeof(T)))
            {
                var p1 = Expression.Parameter(typeof(T), "p1");
                var p2 = Expression.Parameter(typeof(int), "p2");
                var conv = Expression.Convert(p2, typeof(T));
                var exp = Expression.Lambda<Func<T, int, T>>(Expression.Divide(p1, conv), p1, p2).Compile();
                return new Lazy<Func<T, int, T>>(() => exp);
            }
            else
            {
                return new Lazy<Func<T, int, T>>(() => throw new InvalidCastException($"The data type {typeof(T)} is not numeric."));
            }
        }
        
        public static bool IsBoolSupportedType(Type type)
        {   
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                case TypeCode.DateTime:
                    return true;
                default:
                    return false;
            }
        }

      

        
        private static Lazy<Func<T, T, int>> CreateCompare()
        {
            var type = typeof(T);
            if (typeof(IComparable).IsAssignableFrom(type))
            {
                int Compare(T a, T b)
                {
                    return ((IComparable) a).CompareTo(b);
                }
                
                return new Lazy<Func<T, T, int>>(() => Compare);
            }
            else if (type == typeof(char[]))
            {
                int Compare(T a, T b)
                {
                    return Operations.CharArrayCompareTo( (char[])(object) a, (char[])(object) b);
                }
                return new Lazy<Func<T, T, int>>(() => Compare);
            }
            else if(type == typeof(object))
            {
                int Compare(T a, T b)
                {
                    if (a.GetType() == b.GetType() && a is IComparable comparable)
                    {
                        return comparable.CompareTo(b);
                    }

                    // if types don't match, attempt to convert to common type.
                    var aType = a.GetType();
                    var aTypeCode = Type.GetTypeCode(aType);
                    var bType = b.GetType();
                    var bTypeCode = Type.GetTypeCode(bType);
                    if (aTypeCode < bTypeCode)
                    {
                        var a1 = Operations.Parse(bType, a);
                        return ((IComparable) a1).CompareTo(b);
                    }
                    var b1 = Operations.Parse(aType, b);
                    return ((IComparable) b1).CompareTo(b);
                }
                
                return new Lazy<Func<T, T, int>>(() => Compare);
            }
            else
            {
                return new Lazy<Func<T, T, int>>(() => throw new InvalidCastException($"The data type {typeof(T)} is not supported for comparisons."));
            }
        }
        
        private static Lazy<Func<T, T, bool>> CreateCondition(Func<Expression, Expression, BinaryExpression> body, int[] compareResults)
        {
            var type = typeof(T);
            if (IsBoolSupportedType(type))
            {
                var p1 = Expression.Parameter(typeof(T), "p1");
                var p2 = Expression.Parameter(typeof(T), "p2");
                var exp = Expression.Lambda<Func<T, T, bool>>(body(p1, p2), p1, p2).Compile();
                return new Lazy<Func<T, T, bool>>(() => exp);
            }
            else if (typeof(IComparable).IsAssignableFrom(type))
            {
                bool Condition(T a, T b)
                {
                    return compareResults.Contains(((IComparable) a).CompareTo((IComparable) b));
                }
                
                return new Lazy<Func<T, T, bool>>(() => Condition);
            }
            else if (type == typeof(object))
            {
                bool Condition(T a, T b)
                {
                    if (a.GetType() == b.GetType() && a is IComparable comparable)
                    {
                        return compareResults.Contains(comparable.CompareTo((IComparable) b));
                    }

                    // if types don't match, attempt to convert to common type.
                    var aType = a.GetType();
                    var aTypeCode = Type.GetTypeCode(aType);
                    var bType = b.GetType();
                    var bTypeCode = Type.GetTypeCode(bType);
                    if (aTypeCode < bTypeCode)
                    {
                        var a1 = Operations.Parse(bType, a);
                        return compareResults.Contains(((IComparable) a).CompareTo((IComparable) b));
                    }

                    var b1 = Operations.Parse(aType, b);
                    return compareResults.Contains(((IComparable) a).CompareTo((IComparable) b));
                }

                return new Lazy<Func<T, T, bool>>(() => Condition);
            }
            else
            {
                return new Lazy<Func<T, T, bool>>(() => throw new InvalidCastException($"The data type {typeof(T)} is not supported for comparisons."));
            }
        }
        
        private static Lazy<Func<T, T>> CreateNegate()
        {
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    var p1 = Expression.Parameter(typeof(T), "p1");
                    var exp = Expression.Lambda<Func<T, T>>(Expression.Negate(p1), p1).Compile();
                    return new Lazy<Func<T, T>>(() => exp);
                default:
                    return new Lazy<Func<T, T>>(() => throw new InvalidCastException($"The data type {typeof(T)} is not supported for negate."));
            }
        }
        
        private static Lazy<Func<T, T>> CreateIncrement()
        {
            if (IsNumericType(typeof(T)))
            {
                var p1 = Expression.Parameter(typeof(T), "p1");
                var exp = Expression.Lambda<Func<T, T>>(Expression.Increment(p1), p1).Compile();
                return new Lazy<Func<T, T>>(() => exp);
            }
            return new Lazy<Func<T, T>>(() => throw new InvalidCastException($"The data type {typeof(T)} is not supported for negate."));
        }

        private static Lazy<Func<T, T>> CreateDecrement()
        {
            if (IsNumericType(typeof(T)))
            {
                var p1 = Expression.Parameter(typeof(T), "p1");
                var exp = Expression.Lambda<Func<T, T>>(Expression.Decrement(p1), p1).Compile();
                return new Lazy<Func<T, T>>(() => exp);
            }
            return new Lazy<Func<T, T>>(() => throw new InvalidCastException($"The data type {typeof(T)} is not supported for negate."));
        }


        private static Lazy<Func<T, string>> CreateToString()
        {
            var dataType = typeof(T);
            Func<T, string> exp;
            
            switch (Type.GetTypeCode(dataType))
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.DateTime:
                case TypeCode.DBNull:
                case TypeCode.String:
                case TypeCode.Boolean:
                    exp = value => value.ToString();
                    break;
                case TypeCode.Object:
                    if (dataType == typeof(TimeSpan) || dataType == typeof(TimeSpan?)) exp = value => value.ToString();
                    else if (dataType == typeof(Guid) || dataType == typeof(Guid?)) exp = value => value.ToString();
                    else if (dataType == typeof(byte[])) exp = value => ByteArrayToHex(value as byte[]);
                    else if (dataType == typeof(char[])) exp = value => new string(value as char[]);
                    else if (dataType == typeof(JToken)) exp = value => value.ToString();
                    else if (dataType == typeof(XmlDocument)) exp = value => (value as XmlDocument)?.InnerXml;
                    else if (dataType == typeof(object)) exp = value => value.ToString();
                    else exp = value => throw new NotSupportedException($"The datatype {dataType} is not supported for ToString conversion.");
                    break;
                default:
                    exp = value => throw new NotSupportedException($"The datatype {dataType} is not supported for ToString conversion.");
                    break;
            }
            
            return new Lazy<Func<T, string>>(() => exp);
        }

        private static Func<object, T> ConvertToBoolean()
        {
            return value =>
            {
                if (value is bool boolValue)
                {
                    return (T) (object) boolValue;
                }

                if (value is string stringValue)
                {
                    var parsed = bool.TryParse(stringValue, out var parsedResult);
                    if (parsed)
                    {
                        return (T) (object) parsedResult;
                    }

                    parsed = int.TryParse(stringValue, out var numberResult);
                    if (parsed)
                    {
                        return (T) (object) (numberResult != 0);
                    }

                    throw new FormatException("String was not recognized as a valid boolean");
                }

                return (T) (object)Convert.ToBoolean(value);
            };
        }

        private static Func<object, T> ConvertToCharArray()
        {
            return value =>
            {
                if (value is char[] charArray)
                {
                    return (T) (object) charArray;
                }

                return (T) (object) value.ToString().ToCharArray();
            };
        }

        private static Func<object, T> ConvertToByteArray()
        {
            return value =>
            {
                if (value is byte[] byteArray)
                {
                    return (T) (object) byteArray;
                }

                if (value is string stringValue)
                {
                    return (T) (object) HexToByteArray(stringValue);
                }
                throw new DataTypeParseException("Binary type conversion only supported for hex strings.");
            };
        }

        private static Func<object, T> ConvertToTimeSpan()
        {
            return value =>
            {
                if (value is TimeSpan timeSpan)
                {
                    return (T) (object) timeSpan;
                }

                if (value is string stringValue)
                {
                    return (T) (object) TimeSpan.Parse(stringValue);
                }
                throw new DataTypeParseException("Time conversion only supported for strings.");
            };
        }

        private static Func<object, T> ConvertToGuid()
        {
            return value =>
            {
                if (value is Guid guid)
                {
                    return (T) (object) guid;
                }

                if (value is string stringValue)
                {
                    return (T) (object) Guid.Parse(stringValue);
                }
                throw new DataTypeParseException("Guid conversion only supported for strings.");
            };
        }

        private static Func<object, T> ConvertToJson()
        {
            return value =>
            {
                if (value is JToken jToken)
                {
                    return (T) (object) jToken;
                }

                if (value is string stringValue)
                {
                    return (T) (object) JToken.Parse(stringValue);
                }
                throw new DataTypeParseException("Json conversion only supported for strings.");
            };
        }

        private static Func<object, T> ConvertToXml()
        {
            return value =>
            {
                if (value is XmlDocument xmlDocument)
                {
                    return (T) (object) xmlDocument;
                }

                if (value is string stringValue)
                {
                    var xmlDocument2 = new XmlDocument();
                    xmlDocument2.LoadXml(stringValue);
                    return (T) (object) xmlDocument2;
                }
                
                throw new DataTypeParseException("Xml conversion only supported for strings.");
            };
        }

        private static Func<object, T> ConvertToString()
        {
            return value =>
            {
                if (value is byte[] byteArray)
                {
                    return (T) (object) ByteArrayToHex(byteArray);
                }
                else if (value is char[] charArray)
                {
                    return (T) (object) new string(charArray);
                }
                else if (!DataType.IsSimple(value.GetType()))
                {
                    return (T) (object) JsonConvert.SerializeObject(value);
                }
                else
                {
                    return (T) (object) value.ToString();
                }

            };
        }

        private static Lazy<Func<object, T>> CreateParse()
        {
            Func<object, T> exp;
            
            var dataType = typeof(T); 
            switch (Type.GetTypeCode(dataType))
            {
                case TypeCode.Double:
                    exp = value => (T)(object) Convert.ToDouble(value);
                    break;
                case TypeCode.Decimal:
                    exp = value => (T)(object) Convert.ToDecimal(value);
                    break;
                case TypeCode.Byte:
                    exp = value => (T)(object) Convert.ToByte(value);
                    break;
                case TypeCode.Int16:
                    exp = value => (T)(object) Convert.ToInt16(value);
                    break;
                case TypeCode.Int32:
                    exp = value => (T)(object) Convert.ToInt32(value);
                    break;
                case TypeCode.Int64:
                    exp = value => (T)(object) Convert.ToInt64(value);
                    break;
                case TypeCode.SByte:
                    exp = value => (T)(object) Convert.ToSByte(value);
                    break;
                case TypeCode.Single:
                    exp = value => (T)(object) Convert.ToSingle(value);
                    break;
                case TypeCode.UInt16:
                    exp = value => (T)(object) Convert.ToUInt16(value);
                    break;
                case TypeCode.UInt32:
                    exp = value => (T)(object) Convert.ToUInt32(value);
                    break;
                case TypeCode.UInt64:
                    exp = value => (T)(object) Convert.ToUInt64(value);
                    break;
                case TypeCode.DateTime:
                    exp = value => (T)(object) Convert.ToDateTime(value);
                    break;
                case TypeCode.DBNull:
                    exp = value => (T)(object) DBNull.Value;
                    break;
                case TypeCode.String:
                    exp = ConvertToString();
                    break;
                case TypeCode.Boolean:
                    exp = ConvertToBoolean();
                    break;
                case TypeCode.Object:
                    if (dataType == typeof(TimeSpan) || dataType == typeof(TimeSpan?)) exp = ConvertToTimeSpan();
                    else if (dataType == typeof(Guid) || dataType == typeof(Guid?)) exp = ConvertToGuid();
                    else if (dataType == typeof(byte[])) exp = ConvertToByteArray();
                    else if (dataType == typeof(char[])) exp = ConvertToCharArray();
                    else if (dataType == typeof(JToken)) exp = ConvertToJson();
                    else if (dataType == typeof(XmlDocument)) exp = ConvertToXml();
                    else exp = value => throw new NotSupportedException($"The datatype {dataType} is not supported for Parse.");
                    break;
                default:
                    exp = value => throw new NotSupportedException($"The datatype {dataType} is not supported for Parse.");
                    break;
            }
            return new Lazy<Func<object, T>>(() => exp);
        }
        
        private static readonly uint[] Lookup32 = CreateLookup32();

        private static uint[] CreateLookup32()
        {
            var result = new uint[256];
            for (var i = 0; i < 256; i++)
            {
                var s=i.ToString("X2");
                result[i] = s[0] + ((uint)s[1] << 16);
            }
            return result;
        }
        
        /// <summary>
        /// Converts a byte[] into a hex string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static string ByteArrayToHex(byte[] bytes)
        {
            var lookup32 = Lookup32;
            var result = new char[bytes.Length * 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                var val = lookup32[bytes[i]];
                result[2*i] = (char)val;
                result[2*i + 1] = (char) (val >> 16);
            }
            return new string(result);
        }
        
        /// <summary>
        /// Converts a hex string into a byte[]
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        private static byte[] HexToByteArray(string hex)
        {
            var numberChars = hex.Length;
            var bytes = new byte[numberChars / 2];
            for (var i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
        
    }
}