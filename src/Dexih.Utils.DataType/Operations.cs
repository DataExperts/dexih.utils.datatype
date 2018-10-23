using System;
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
            typeof(TimeSpan)
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
        
        
        public static T Add<T>(T a, T b) => Operations<T>.Add.Value(a,b);
        public static T Subtract<T>(T a, T b) => Operations<T>.Subtract.Value(a, b);
        public static object Subtract<T>(object a, object b) => Operations<object>.Subtract.Value(a, b);
        public static T Divide<T>(T a, T b) => Operations<T>.Divide.Value(a, b);
        public static object Divide<T>(object a, object b) => Operations<object>.Divide.Value(a, b);
        public static T DivideInt<T>(T a, int b) => Operations<T>.DivideInt.Value(a, b);
        public static object DivideInt<T>(object a, int b) => Operations<object>.DivideInt.Value(a, b);
        public static T Multiply<T>(T a, T b) => Operations<T>.Multiply.Value(a, b);
        public static object Multiply<T>(object a, object b) => Operations<object>.Multiply.Value(a, b);
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
                case DataType.ETypeCode.Char:
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

            if (type == ConvertTypes[ConvertTypeBool]) return Equal<bool>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeSbyte]) return Equal<sbyte>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeByte]) return Equal<byte>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeShort]) return Equal<short>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeUShort]) return Equal<ushort>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeInt]) return Equal<int>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeUint]) return Equal<uint>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeLong]) return Equal<long>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeULong]) return Equal<ulong>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeFloat]) return Equal<float>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeDouble]) return Equal<double>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeDecimal]) return Equal<decimal>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeDateTime]) return Equal<DateTime>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeString]) return Equal<string>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeByteArray]) return Equal<byte[]>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeCharArray]) return Equal<char[]>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeJToken]) return Equal<JToken>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeXmlDocument]) return Equal<XmlDocument>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return Equal<TimeSpan>(inputValue, compareTo);

            throw new ArgumentOutOfRangeException(nameof(type), inputValue, null);
        }

        public static bool Equal(DataType.ETypeCode typeCode, object value1, object value2)
        {
            if ((value1 == null || value1 == DBNull.Value) && (value2 == null || value2 == DBNull.Value))
                return true;

            if (value1 == null || value1 == DBNull.Value || value2 == null || value2 == DBNull.Value)
                return false;
            
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                    return Equal<byte[]>(value1, value2);
                case DataType.ETypeCode.Byte:
                    return Equal<byte>(value1, value2);
                case DataType.ETypeCode.SByte:
                    return Equal<sbyte>(value1, value2);
                case DataType.ETypeCode.UInt16:
                    return Equal<ushort>(value1, value2);
                case DataType.ETypeCode.UInt32:
                    return Equal<uint>(value1, value2);
                case DataType.ETypeCode.UInt64:
                    return Equal<ulong>(value1, value2);
                case DataType.ETypeCode.Int16:
                    return Equal<short>(value1, value2);
                case DataType.ETypeCode.Int32:
                    return Equal<int>(value1, value2);
                case DataType.ETypeCode.Int64:
                    return Equal<long>(value1, value2);
                case DataType.ETypeCode.Decimal:
                    return Equal<decimal>(value1, value2);
                case DataType.ETypeCode.Double:
                    return Equal<double>(value1, value2);
                case DataType.ETypeCode.Single:
                    return Equal<float>(value1, value2);
                case DataType.ETypeCode.String:
                    return Equal<string>(value1, value2);
                case DataType.ETypeCode.Text:
                    return Equal<string>(value1, value2);
                case DataType.ETypeCode.Boolean:
                    return Equal<bool>(value1, value2);
                case DataType.ETypeCode.DateTime:
                    return Equal<DateTime>(value1, value2);
                case DataType.ETypeCode.Time:
                    return Equal<TimeSpan>(value1, value2);
                case DataType.ETypeCode.Guid:
                    return Equal<Guid>(value1, value2);
                case DataType.ETypeCode.Unknown:
                    return Equal<string>(value1, value2);
                case DataType.ETypeCode.Json:
                    return Equal<JToken>(value1, value2);
                case DataType.ETypeCode.Xml:
                    return Equal<XmlDocument>(value1, value2);
                case DataType.ETypeCode.Enum:
                    return Equal<int>(value1, value2);
                case DataType.ETypeCode.Char:
                    return Equal<char[]>(value1, value2);
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }

        public static bool GreaterThan(object inputValue, object compareTo)
        {
            var type = inputValue?.GetType();
            return GreaterThan(type, inputValue, compareTo);
        }
        
        public static bool GreaterThan(Type type, object inputValue, object compareTo)
        {
            if (inputValue == null || inputValue == DBNull.Value || compareTo == null || compareTo == DBNull.Value)
                return false;

            if (type == ConvertTypes[ConvertTypeBool]) return GreaterThan<bool>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeSbyte]) return GreaterThan<sbyte>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeByte]) return GreaterThan<byte>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeShort]) return GreaterThan<short>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeUShort]) return GreaterThan<ushort>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeInt]) return GreaterThan<int>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeUint]) return GreaterThan<uint>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeLong]) return GreaterThan<long>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeULong]) return GreaterThan<ulong>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeFloat]) return GreaterThan<float>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeDouble]) return GreaterThan<double>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeDecimal]) return GreaterThan<decimal>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeDateTime]) return GreaterThan<DateTime>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeString]) return GreaterThan<string>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeByteArray]) return GreaterThan<byte[]>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeCharArray]) return GreaterThan<char[]>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeJToken]) return GreaterThan<JToken>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeXmlDocument]) return GreaterThan<XmlDocument>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return GreaterThan<TimeSpan>(inputValue, compareTo);

            throw new ArgumentOutOfRangeException(nameof(type), inputValue, null);
        }

        public static bool GreaterThan(DataType.ETypeCode typeCode, object value1, object value2)
        {
            if (value1 == null || value1 == DBNull.Value || value2 == null || value2 == DBNull.Value)
                return false;
            
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                    return GreaterThan<byte[]>(value1, value2);
                case DataType.ETypeCode.Byte:
                    return GreaterThan<byte>(value1, value2);
                case DataType.ETypeCode.SByte:
                    return GreaterThan<sbyte>(value1, value2);
                case DataType.ETypeCode.UInt16:
                    return GreaterThan<ushort>(value1, value2);
                case DataType.ETypeCode.UInt32:
                    return GreaterThan<uint>(value1, value2);
                case DataType.ETypeCode.UInt64:
                    return GreaterThan<ulong>(value1, value2);
                case DataType.ETypeCode.Int16:
                    return GreaterThan<short>(value1, value2);
                case DataType.ETypeCode.Int32:
                    return GreaterThan<int>(value1, value2);
                case DataType.ETypeCode.Int64:
                    return GreaterThan<long>(value1, value2);
                case DataType.ETypeCode.Decimal:
                    return GreaterThan<decimal>(value1, value2);
                case DataType.ETypeCode.Double:
                    return GreaterThan<double>(value1, value2);
                case DataType.ETypeCode.Single:
                    return GreaterThan<float>(value1, value2);
                case DataType.ETypeCode.String:
                    return GreaterThan<string>(value1, value2);
                case DataType.ETypeCode.Text:
                    return GreaterThan<string>(value1, value2);
                case DataType.ETypeCode.Boolean:
                    return GreaterThan<bool>(value1, value2);
                case DataType.ETypeCode.DateTime:
                    return GreaterThan<DateTime>(value1, value2);
                case DataType.ETypeCode.Time:
                    return GreaterThan<TimeSpan>(value1, value2);
                case DataType.ETypeCode.Guid:
                    return GreaterThan<Guid>(value1, value2);
                case DataType.ETypeCode.Unknown:
                    return GreaterThan<string>(value1, value2);
                case DataType.ETypeCode.Json:
                    return GreaterThan<JToken>(value1, value2);
                case DataType.ETypeCode.Xml:
                    return GreaterThan<XmlDocument>(value1, value2);
                case DataType.ETypeCode.Enum:
                    return GreaterThan<int>(value1, value2);
                case DataType.ETypeCode.Char:
                    return GreaterThan<char[]>(value1, value2);
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }

        public static bool GreaterThanOrEqual(object inputValue, object compareTo)
        {
            var type = inputValue?.GetType();
            return GreaterThanOrEqual(type, inputValue, compareTo);
        }
        
        public static bool GreaterThanOrEqual(Type type, object inputValue, object compareTo)
        {
            if ((inputValue == null || inputValue == DBNull.Value) && (compareTo == null || compareTo == DBNull.Value))
                return true;

            if (inputValue == null || inputValue == DBNull.Value || compareTo == null || compareTo == DBNull.Value)
                return false;

            if (type == ConvertTypes[ConvertTypeBool]) return GreaterThanOrEqual<bool>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeSbyte]) return GreaterThanOrEqual<sbyte>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeByte]) return GreaterThanOrEqual<byte>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeShort]) return GreaterThanOrEqual<short>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeUShort]) return GreaterThanOrEqual<ushort>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeInt]) return GreaterThanOrEqual<int>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeUint]) return GreaterThanOrEqual<uint>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeLong]) return GreaterThanOrEqual<long>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeULong]) return GreaterThanOrEqual<ulong>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeFloat]) return GreaterThanOrEqual<float>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeDouble]) return GreaterThanOrEqual<double>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeDecimal]) return GreaterThanOrEqual<decimal>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeDateTime]) return GreaterThanOrEqual<DateTime>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeString]) return GreaterThanOrEqual<string>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeByteArray]) return GreaterThanOrEqual<byte[]>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeCharArray]) return GreaterThanOrEqual<char[]>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeJToken]) return GreaterThanOrEqual<JToken>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeXmlDocument]) return GreaterThanOrEqual<XmlDocument>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return GreaterThanOrEqual<TimeSpan>(inputValue, compareTo);

            throw new ArgumentOutOfRangeException(nameof(type), inputValue, null);
        }

        public static bool GreaterThanOrEqual(DataType.ETypeCode typeCode, object value1, object value2)
        {
            if ((value1 == null || value1 == DBNull.Value) && (value2 == null || value2 == DBNull.Value))
                return true;

            if (value1 == null || value1 == DBNull.Value || value2 == null || value2 == DBNull.Value)
                return false;
            
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                    return GreaterThanOrEqual<byte[]>(value1, value2);
                case DataType.ETypeCode.Byte:
                    return GreaterThanOrEqual<byte>(value1, value2);
                case DataType.ETypeCode.SByte:
                    return GreaterThanOrEqual<sbyte>(value1, value2);
                case DataType.ETypeCode.UInt16:
                    return GreaterThanOrEqual<ushort>(value1, value2);
                case DataType.ETypeCode.UInt32:
                    return GreaterThanOrEqual<uint>(value1, value2);
                case DataType.ETypeCode.UInt64:
                    return GreaterThanOrEqual<ulong>(value1, value2);
                case DataType.ETypeCode.Int16:
                    return GreaterThanOrEqual<short>(value1, value2);
                case DataType.ETypeCode.Int32:
                    return GreaterThanOrEqual<int>(value1, value2);
                case DataType.ETypeCode.Int64:
                    return GreaterThanOrEqual<long>(value1, value2);
                case DataType.ETypeCode.Decimal:
                    return GreaterThanOrEqual<decimal>(value1, value2);
                case DataType.ETypeCode.Double:
                    return GreaterThanOrEqual<double>(value1, value2);
                case DataType.ETypeCode.Single:
                    return GreaterThanOrEqual<float>(value1, value2);
                case DataType.ETypeCode.String:
                    return GreaterThanOrEqual<string>(value1, value2);
                case DataType.ETypeCode.Text:
                    return GreaterThanOrEqual<string>(value1, value2);
                case DataType.ETypeCode.Boolean:
                    return GreaterThanOrEqual<bool>(value1, value2);
                case DataType.ETypeCode.DateTime:
                    return GreaterThanOrEqual<DateTime>(value1, value2);
                case DataType.ETypeCode.Time:
                    return GreaterThanOrEqual<TimeSpan>(value1, value2);
                case DataType.ETypeCode.Guid:
                    return GreaterThanOrEqual<Guid>(value1, value2);
                case DataType.ETypeCode.Unknown:
                    return GreaterThanOrEqual<string>(value1, value2);
                case DataType.ETypeCode.Json:
                    return GreaterThanOrEqual<JToken>(value1, value2);
                case DataType.ETypeCode.Xml:
                    return GreaterThanOrEqual<XmlDocument>(value1, value2);
                case DataType.ETypeCode.Enum:
                    return GreaterThanOrEqual<int>(value1, value2);
                case DataType.ETypeCode.Char:
                    return GreaterThanOrEqual<char[]>(value1, value2);
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }

        public static bool LessThan(object inputValue, object compareTo)
        {
            var type = inputValue?.GetType();
            return LessThan(type, inputValue, compareTo);
        }
        
        public static bool LessThan(Type type, object inputValue, object compareTo)
        {
            if (inputValue == null || inputValue == DBNull.Value || compareTo == null || compareTo == DBNull.Value)
                return false;

            if (type == ConvertTypes[ConvertTypeBool]) return LessThan<bool>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeSbyte]) return LessThan<sbyte>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeByte]) return LessThan<byte>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeShort]) return LessThan<short>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeUShort]) return LessThan<ushort>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeInt]) return LessThan<int>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeUint]) return LessThan<uint>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeLong]) return LessThan<long>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeULong]) return LessThan<ulong>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeFloat]) return LessThan<float>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeDouble]) return LessThan<double>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeDecimal]) return LessThan<decimal>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeDateTime]) return LessThan<DateTime>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeString]) return LessThan<string>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeByteArray]) return LessThan<byte[]>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeCharArray]) return LessThan<char[]>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeJToken]) return LessThan<JToken>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeXmlDocument]) return LessThan<XmlDocument>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return LessThan<TimeSpan>(inputValue, compareTo);

            throw new ArgumentOutOfRangeException(nameof(type), inputValue, null);
        }

        public static bool LessThan(DataType.ETypeCode typeCode, object value1, object value2)
        {
            if (value1 == null || value1 == DBNull.Value || value2 == null || value2 == DBNull.Value)
                return false;
            
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                    return LessThan<byte[]>(value1, value2);
                case DataType.ETypeCode.Byte:
                    return LessThan<byte>(value1, value2);
                case DataType.ETypeCode.SByte:
                    return LessThan<sbyte>(value1, value2);
                case DataType.ETypeCode.UInt16:
                    return LessThan<ushort>(value1, value2);
                case DataType.ETypeCode.UInt32:
                    return LessThan<uint>(value1, value2);
                case DataType.ETypeCode.UInt64:
                    return LessThan<ulong>(value1, value2);
                case DataType.ETypeCode.Int16:
                    return LessThan<short>(value1, value2);
                case DataType.ETypeCode.Int32:
                    return LessThan<int>(value1, value2);
                case DataType.ETypeCode.Int64:
                    return LessThan<long>(value1, value2);
                case DataType.ETypeCode.Decimal:
                    return LessThan<decimal>(value1, value2);
                case DataType.ETypeCode.Double:
                    return LessThan<double>(value1, value2);
                case DataType.ETypeCode.Single:
                    return LessThan<float>(value1, value2);
                case DataType.ETypeCode.String:
                    return LessThan<string>(value1, value2);
                case DataType.ETypeCode.Text:
                    return LessThan<string>(value1, value2);
                case DataType.ETypeCode.Boolean:
                    return LessThan<bool>(value1, value2);
                case DataType.ETypeCode.DateTime:
                    return LessThan<DateTime>(value1, value2);
                case DataType.ETypeCode.Time:
                    return LessThan<TimeSpan>(value1, value2);
                case DataType.ETypeCode.Guid:
                    return LessThan<Guid>(value1, value2);
                case DataType.ETypeCode.Unknown:
                    return LessThan<string>(value1, value2);
                case DataType.ETypeCode.Json:
                    return LessThan<JToken>(value1, value2);
                case DataType.ETypeCode.Xml:
                    return LessThan<XmlDocument>(value1, value2);
                case DataType.ETypeCode.Enum:
                    return LessThan<int>(value1, value2);
                case DataType.ETypeCode.Char:
                    return LessThan<char[]>(value1, value2);
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }

        public static bool LessThanOrEqual(object inputValue, object compareTo)
        {
            var type = inputValue?.GetType();
            return LessThanOrEqual(type, inputValue, compareTo);
        }
        
        public static bool LessThanOrEqual(Type type, object inputValue, object compareTo)
        {
            if ((inputValue == null || inputValue == DBNull.Value) && (compareTo == null || compareTo == DBNull.Value))
                return true;

            if (inputValue == null || inputValue == DBNull.Value || compareTo == null || compareTo == DBNull.Value)
                return false;

            if (type == ConvertTypes[ConvertTypeBool]) return LessThanOrEqual<bool>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeSbyte]) return LessThanOrEqual<sbyte>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeByte]) return LessThanOrEqual<byte>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeShort]) return LessThanOrEqual<short>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeUShort]) return LessThanOrEqual<ushort>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeInt]) return LessThanOrEqual<int>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeUint]) return LessThanOrEqual<uint>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeLong]) return LessThanOrEqual<long>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeULong]) return LessThanOrEqual<ulong>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeFloat]) return LessThanOrEqual<float>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeDouble]) return LessThanOrEqual<double>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeDecimal]) return LessThanOrEqual<decimal>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeDateTime]) return LessThanOrEqual<DateTime>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeString]) return LessThanOrEqual<string>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeByteArray]) return LessThanOrEqual<byte[]>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeCharArray]) return LessThanOrEqual<char[]>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeJToken]) return LessThanOrEqual<JToken>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeXmlDocument]) return LessThanOrEqual<XmlDocument>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return LessThanOrEqual<TimeSpan>(inputValue, compareTo);

            throw new ArgumentOutOfRangeException(nameof(type), inputValue, null);
        }

        public static bool LessThanOrEqual(DataType.ETypeCode typeCode, object value1, object value2)
        {
            if ((value1 == null || value1 == DBNull.Value) && (value2 == null || value2 == DBNull.Value))
                return true;

            if (value1 == null || value1 == DBNull.Value || value2 == null || value2 == DBNull.Value)
                return false;
            
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                    return LessThanOrEqual<byte[]>(value1, value2);
                case DataType.ETypeCode.Byte:
                    return LessThanOrEqual<byte>(value1, value2);
                case DataType.ETypeCode.SByte:
                    return LessThanOrEqual<sbyte>(value1, value2);
                case DataType.ETypeCode.UInt16:
                    return LessThanOrEqual<ushort>(value1, value2);
                case DataType.ETypeCode.UInt32:
                    return LessThanOrEqual<uint>(value1, value2);
                case DataType.ETypeCode.UInt64:
                    return LessThanOrEqual<ulong>(value1, value2);
                case DataType.ETypeCode.Int16:
                    return LessThanOrEqual<short>(value1, value2);
                case DataType.ETypeCode.Int32:
                    return LessThanOrEqual<int>(value1, value2);
                case DataType.ETypeCode.Int64:
                    return LessThanOrEqual<long>(value1, value2);
                case DataType.ETypeCode.Decimal:
                    return LessThanOrEqual<decimal>(value1, value2);
                case DataType.ETypeCode.Double:
                    return LessThanOrEqual<double>(value1, value2);
                case DataType.ETypeCode.Single:
                    return LessThanOrEqual<float>(value1, value2);
                case DataType.ETypeCode.String:
                    return LessThanOrEqual<string>(value1, value2);
                case DataType.ETypeCode.Text:
                    return LessThanOrEqual<string>(value1, value2);
                case DataType.ETypeCode.Boolean:
                    return LessThanOrEqual<bool>(value1, value2);
                case DataType.ETypeCode.DateTime:
                    return LessThanOrEqual<DateTime>(value1, value2);
                case DataType.ETypeCode.Time:
                    return LessThanOrEqual<TimeSpan>(value1, value2);
                case DataType.ETypeCode.Guid:
                    return LessThanOrEqual<Guid>(value1, value2);
                case DataType.ETypeCode.Unknown:
                    return LessThanOrEqual<string>(value1, value2);
                case DataType.ETypeCode.Json:
                    return LessThanOrEqual<JToken>(value1, value2);
                case DataType.ETypeCode.Xml:
                    return LessThanOrEqual<XmlDocument>(value1, value2);
                case DataType.ETypeCode.Enum:
                    return LessThanOrEqual<int>(value1, value2);
                case DataType.ETypeCode.Char:
                    return LessThanOrEqual<char[]>(value1, value2);
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
            if (type == ConvertTypes[ConvertTypeBool]) return Subtract<bool>((bool)a, (bool)b);
            if (type == ConvertTypes[ConvertTypeSbyte]) return Subtract<sbyte>((sbyte)a, (sbyte) b);
            if (type == ConvertTypes[ConvertTypeByte]) return Subtract<byte>((byte)a, (byte)b);
            if (type == ConvertTypes[ConvertTypeShort]) return Subtract<short>((short)a, (short)b);
            if (type == ConvertTypes[ConvertTypeUShort]) return Subtract<ushort>((ushort)a, (ushort)b);
            if (type == ConvertTypes[ConvertTypeInt]) return Subtract<int>((int)a, (int)b);
            if (type == ConvertTypes[ConvertTypeUint]) return Subtract<uint>((uint)a, (uint)b);
            if (type == ConvertTypes[ConvertTypeLong]) return Subtract<long>((long)a, (long)b);
            if (type == ConvertTypes[ConvertTypeULong]) return Subtract<ulong>((ulong)a, (ulong)b);
            if (type == ConvertTypes[ConvertTypeFloat]) return Subtract<float>((float)a, (float)b);
            if (type == ConvertTypes[ConvertTypeDouble]) return Subtract<double>((double)a, (double)b);
            if (type == ConvertTypes[ConvertTypeDecimal]) return Subtract<decimal>((decimal)a, (decimal)b);
            if (type == ConvertTypes[ConvertTypeDateTime]) return Subtract<DateTime>((DateTime)a, (DateTime)b);
            if (type == ConvertTypes[ConvertTypeString]) return Subtract<string>((string)a, (string)b);
            if (type == ConvertTypes[ConvertTypeByteArray]) return Subtract<byte[]>((byte[])a, (byte[])b);
            if (type == ConvertTypes[ConvertTypeCharArray]) return Subtract<char[]>((char[])a, (char[])b);
            if (type == ConvertTypes[ConvertTypeJToken]) return Subtract<JToken>((JToken)a, (JToken)b);
            if (type == ConvertTypes[ConvertTypeXmlDocument]) return Subtract<XmlDocument>((XmlDocument)a, (XmlDocument)b);
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return Subtract<TimeSpan>((TimeSpan)a, (TimeSpan)b);

            throw new ArgumentOutOfRangeException(nameof(type), a, null);
        }

        public static object Subtract(DataType.ETypeCode typeCode, object value1, object value2)
        {
           
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                    return Subtract<bool>((bool)value1, (bool)value2);
                case DataType.ETypeCode.Byte:
                    return Subtract<byte>((byte)value1, (byte)value2);
                case DataType.ETypeCode.SByte:
                    return Subtract<sbyte>((sbyte)value1, (sbyte)value2);
                case DataType.ETypeCode.UInt16:
                    return Subtract<ushort>((ushort)value1, (ushort)value2);
                case DataType.ETypeCode.UInt32:
                    return Subtract<uint>((uint)value1, (uint)value2);
                case DataType.ETypeCode.UInt64:
                    return Subtract<ulong>((ulong)value1, (ulong)value2);
                case DataType.ETypeCode.Int16:
                    return Subtract<short>((short)value1, (short)value2);
                case DataType.ETypeCode.Int32:
                    return Subtract<int>((int)value1, (int)value2);
                case DataType.ETypeCode.Int64:
                    return Subtract<long>((long)value1, (long)value2);
                case DataType.ETypeCode.Decimal:
                    return Subtract<decimal>((decimal)value1, (decimal)value2);
                case DataType.ETypeCode.Double:
                    return Subtract<double>((double)value1, (double)value2);
                case DataType.ETypeCode.Single:
                    return Subtract<float>((float)value1, (float)value2);
                case DataType.ETypeCode.String:
                    return Subtract<string>((string)value1, (string)value2);
                case DataType.ETypeCode.Text:
                    return Subtract<string>((string)value1, (string)value2);
                case DataType.ETypeCode.Boolean:
                    return Subtract<bool>((bool)value1, (bool)value2);
                case DataType.ETypeCode.DateTime:
                    return Subtract<DateTime>((DateTime)value1, (DateTime)value2);
                case DataType.ETypeCode.Time:
                    return Subtract<TimeSpan>((TimeSpan)value1, (TimeSpan)value2);
                case DataType.ETypeCode.Guid:
                    return Subtract<Guid>((Guid)value1, (Guid)value2);
                case DataType.ETypeCode.Unknown:
                    return Subtract<string>((string)value1, (string)value2);
                case DataType.ETypeCode.Json:
                    return Subtract<JToken>((JToken)value1, (JToken)value2);
                case DataType.ETypeCode.Xml:
                    return Subtract<XmlDocument>((XmlDocument)value1, (XmlDocument)value2);
                case DataType.ETypeCode.Enum:
                    return Subtract<int>((int)value1, (int)value2);
                case DataType.ETypeCode.Char:
                    return Subtract<char[]>((char[])value1, (char[])value2);
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
            if (type == ConvertTypes[ConvertTypeBool]) return Multiply<bool>((bool)a, (bool)b);
            if (type == ConvertTypes[ConvertTypeSbyte]) return Multiply<sbyte>((sbyte)a, (sbyte) b);
            if (type == ConvertTypes[ConvertTypeByte]) return Multiply<byte>((byte)a, (byte)b);
            if (type == ConvertTypes[ConvertTypeShort]) return Multiply<short>((short)a, (short)b);
            if (type == ConvertTypes[ConvertTypeUShort]) return Multiply<ushort>((ushort)a, (ushort)b);
            if (type == ConvertTypes[ConvertTypeInt]) return Multiply<int>((int)a, (int)b);
            if (type == ConvertTypes[ConvertTypeUint]) return Multiply<uint>((uint)a, (uint)b);
            if (type == ConvertTypes[ConvertTypeLong]) return Multiply<long>((long)a, (long)b);
            if (type == ConvertTypes[ConvertTypeULong]) return Multiply<ulong>((ulong)a, (ulong)b);
            if (type == ConvertTypes[ConvertTypeFloat]) return Multiply<float>((float)a, (float)b);
            if (type == ConvertTypes[ConvertTypeDouble]) return Multiply<double>((double)a, (double)b);
            if (type == ConvertTypes[ConvertTypeDecimal]) return Multiply<decimal>((decimal)a, (decimal)b);
            if (type == ConvertTypes[ConvertTypeDateTime]) return Multiply<DateTime>((DateTime)a, (DateTime)b);
            if (type == ConvertTypes[ConvertTypeString]) return Multiply<string>((string)a, (string)b);
            if (type == ConvertTypes[ConvertTypeByteArray]) return Multiply<byte[]>((byte[])a, (byte[])b);
            if (type == ConvertTypes[ConvertTypeCharArray]) return Multiply<char[]>((char[])a, (char[])b);
            if (type == ConvertTypes[ConvertTypeJToken]) return Multiply<JToken>((JToken)a, (JToken)b);
            if (type == ConvertTypes[ConvertTypeXmlDocument]) return Multiply<XmlDocument>((XmlDocument)a, (XmlDocument)b);
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return Multiply<TimeSpan>((TimeSpan)a, (TimeSpan)b);

            throw new ArgumentOutOfRangeException(nameof(type), a, null);
        }

        public static object Multiply(DataType.ETypeCode typeCode, object value1, object value2)
        {
           
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                    return Multiply<bool>((bool)value1, (bool)value2);
                case DataType.ETypeCode.Byte:
                    return Multiply<byte>((byte)value1, (byte)value2);
                case DataType.ETypeCode.SByte:
                    return Multiply<sbyte>((sbyte)value1, (sbyte)value2);
                case DataType.ETypeCode.UInt16:
                    return Multiply<ushort>((ushort)value1, (ushort)value2);
                case DataType.ETypeCode.UInt32:
                    return Multiply<uint>((uint)value1, (uint)value2);
                case DataType.ETypeCode.UInt64:
                    return Multiply<ulong>((ulong)value1, (ulong)value2);
                case DataType.ETypeCode.Int16:
                    return Multiply<short>((short)value1, (short)value2);
                case DataType.ETypeCode.Int32:
                    return Multiply<int>((int)value1, (int)value2);
                case DataType.ETypeCode.Int64:
                    return Multiply<long>((long)value1, (long)value2);
                case DataType.ETypeCode.Decimal:
                    return Multiply<decimal>((decimal)value1, (decimal)value2);
                case DataType.ETypeCode.Double:
                    return Multiply<double>((double)value1, (double)value2);
                case DataType.ETypeCode.Single:
                    return Multiply<float>((float)value1, (float)value2);
                case DataType.ETypeCode.String:
                    return Multiply<string>((string)value1, (string)value2);
                case DataType.ETypeCode.Text:
                    return Multiply<string>((string)value1, (string)value2);
                case DataType.ETypeCode.Boolean:
                    return Multiply<bool>((bool)value1, (bool)value2);
                case DataType.ETypeCode.DateTime:
                    return Multiply<DateTime>((DateTime)value1, (DateTime)value2);
                case DataType.ETypeCode.Time:
                    return Multiply<TimeSpan>((TimeSpan)value1, (TimeSpan)value2);
                case DataType.ETypeCode.Guid:
                    return Multiply<Guid>((Guid)value1, (Guid)value2);
                case DataType.ETypeCode.Unknown:
                    return Multiply<string>((string)value1, (string)value2);
                case DataType.ETypeCode.Json:
                    return Multiply<JToken>((JToken)value1, (JToken)value2);
                case DataType.ETypeCode.Xml:
                    return Multiply<XmlDocument>((XmlDocument)value1, (XmlDocument)value2);
                case DataType.ETypeCode.Enum:
                    return Multiply<int>((int)value1, (int)value2);
                case DataType.ETypeCode.Char:
                    return Multiply<char[]>((char[])value1, (char[])value2);
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
            if (type == ConvertTypes[ConvertTypeBool]) return Divide<bool>((bool)a, (bool)b);
            if (type == ConvertTypes[ConvertTypeSbyte]) return Divide<sbyte>((sbyte)a, (sbyte) b);
            if (type == ConvertTypes[ConvertTypeByte]) return Divide<byte>((byte)a, (byte)b);
            if (type == ConvertTypes[ConvertTypeShort]) return Divide<short>((short)a, (short)b);
            if (type == ConvertTypes[ConvertTypeUShort]) return Divide<ushort>((ushort)a, (ushort)b);
            if (type == ConvertTypes[ConvertTypeInt]) return Divide<int>((int)a, (int)b);
            if (type == ConvertTypes[ConvertTypeUint]) return Divide<uint>((uint)a, (uint)b);
            if (type == ConvertTypes[ConvertTypeLong]) return Divide<long>((long)a, (long)b);
            if (type == ConvertTypes[ConvertTypeULong]) return Divide<ulong>((ulong)a, (ulong)b);
            if (type == ConvertTypes[ConvertTypeFloat]) return Divide<float>((float)a, (float)b);
            if (type == ConvertTypes[ConvertTypeDouble]) return Divide<double>((double)a, (double)b);
            if (type == ConvertTypes[ConvertTypeDecimal]) return Divide<decimal>((decimal)a, (decimal)b);
            if (type == ConvertTypes[ConvertTypeDateTime]) return Divide<DateTime>((DateTime)a, (DateTime)b);
            if (type == ConvertTypes[ConvertTypeString]) return Divide<string>((string)a, (string)b);
            if (type == ConvertTypes[ConvertTypeByteArray]) return Divide<byte[]>((byte[])a, (byte[])b);
            if (type == ConvertTypes[ConvertTypeCharArray]) return Divide<char[]>((char[])a, (char[])b);
            if (type == ConvertTypes[ConvertTypeJToken]) return Divide<JToken>((JToken)a, (JToken)b);
            if (type == ConvertTypes[ConvertTypeXmlDocument]) return Divide<XmlDocument>((XmlDocument)a, (XmlDocument)b);
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return Divide<TimeSpan>((TimeSpan)a, (TimeSpan)b);

            throw new ArgumentOutOfRangeException(nameof(type), a, null);
        }

        public static object Divide(DataType.ETypeCode typeCode, object value1, object value2)
        {
           
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                    return Divide<bool>((bool)value1, (bool)value2);
                case DataType.ETypeCode.Byte:
                    return Divide<byte>((byte)value1, (byte)value2);
                case DataType.ETypeCode.SByte:
                    return Divide<sbyte>((sbyte)value1, (sbyte)value2);
                case DataType.ETypeCode.UInt16:
                    return Divide<ushort>((ushort)value1, (ushort)value2);
                case DataType.ETypeCode.UInt32:
                    return Divide<uint>((uint)value1, (uint)value2);
                case DataType.ETypeCode.UInt64:
                    return Divide<ulong>((ulong)value1, (ulong)value2);
                case DataType.ETypeCode.Int16:
                    return Divide<short>((short)value1, (short)value2);
                case DataType.ETypeCode.Int32:
                    return Divide<int>((int)value1, (int)value2);
                case DataType.ETypeCode.Int64:
                    return Divide<long>((long)value1, (long)value2);
                case DataType.ETypeCode.Decimal:
                    return Divide<decimal>((decimal)value1, (decimal)value2);
                case DataType.ETypeCode.Double:
                    return Divide<double>((double)value1, (double)value2);
                case DataType.ETypeCode.Single:
                    return Divide<float>((float)value1, (float)value2);
                case DataType.ETypeCode.String:
                    return Divide<string>((string)value1, (string)value2);
                case DataType.ETypeCode.Text:
                    return Divide<string>((string)value1, (string)value2);
                case DataType.ETypeCode.Boolean:
                    return Divide<bool>((bool)value1, (bool)value2);
                case DataType.ETypeCode.DateTime:
                    return Divide<DateTime>((DateTime)value1, (DateTime)value2);
                case DataType.ETypeCode.Time:
                    return Divide<TimeSpan>((TimeSpan)value1, (TimeSpan)value2);
                case DataType.ETypeCode.Guid:
                    return Divide<Guid>((Guid)value1, (Guid)value2);
                case DataType.ETypeCode.Unknown:
                    return Divide<string>((string)value1, (string)value2);
                case DataType.ETypeCode.Json:
                    return Divide<JToken>((JToken)value1, (JToken)value2);
                case DataType.ETypeCode.Xml:
                    return Divide<XmlDocument>((XmlDocument)value1, (XmlDocument)value2);
                case DataType.ETypeCode.Enum:
                    return Divide<int>((int)value1, (int)value2);
                case DataType.ETypeCode.Char:
                    return Divide<char[]>((char[])value1, (char[])value2);
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
            if (type == ConvertTypes[ConvertTypeBool]) return Add<bool>((bool)a, (bool)b);
            if (type == ConvertTypes[ConvertTypeSbyte]) return Add<sbyte>((sbyte)a, (sbyte) b);
            if (type == ConvertTypes[ConvertTypeByte]) return Add<byte>((byte)a, (byte)b);
            if (type == ConvertTypes[ConvertTypeShort]) return Add<short>((short)a, (short)b);
            if (type == ConvertTypes[ConvertTypeUShort]) return Add<ushort>((ushort)a, (ushort)b);
            if (type == ConvertTypes[ConvertTypeInt]) return Add<int>((int)a, (int)b);
            if (type == ConvertTypes[ConvertTypeUint]) return Add<uint>((uint)a, (uint)b);
            if (type == ConvertTypes[ConvertTypeLong]) return Add<long>((long)a, (long)b);
            if (type == ConvertTypes[ConvertTypeULong]) return Add<ulong>((ulong)a, (ulong)b);
            if (type == ConvertTypes[ConvertTypeFloat]) return Add<float>((float)a, (float)b);
            if (type == ConvertTypes[ConvertTypeDouble]) return Add<double>((double)a, (double)b);
            if (type == ConvertTypes[ConvertTypeDecimal]) return Add<decimal>((decimal)a, (decimal)b);
            if (type == ConvertTypes[ConvertTypeDateTime]) return Add<DateTime>((DateTime)a, (DateTime)b);
            if (type == ConvertTypes[ConvertTypeString]) return Add<string>((string)a, (string)b);
            if (type == ConvertTypes[ConvertTypeByteArray]) return Add<byte[]>((byte[])a, (byte[])b);
            if (type == ConvertTypes[ConvertTypeCharArray]) return Add<char[]>((char[])a, (char[])b);
            if (type == ConvertTypes[ConvertTypeJToken]) return Add<JToken>((JToken)a, (JToken)b);
            if (type == ConvertTypes[ConvertTypeXmlDocument]) return Add<XmlDocument>((XmlDocument)a, (XmlDocument)b);
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return Add<TimeSpan>((TimeSpan)a, (TimeSpan)b);

            throw new ArgumentOutOfRangeException(nameof(type), a, null);
        }

        public static object Add(DataType.ETypeCode typeCode, object value1, object value2)
        {
           
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                    return Add<bool>((bool)value1, (bool)value2);
                case DataType.ETypeCode.Byte:
                    return Add<byte>((byte)value1, (byte)value2);
                case DataType.ETypeCode.SByte:
                    return Add<sbyte>((sbyte)value1, (sbyte)value2);
                case DataType.ETypeCode.UInt16:
                    return Add<ushort>((ushort)value1, (ushort)value2);
                case DataType.ETypeCode.UInt32:
                    return Add<uint>((uint)value1, (uint)value2);
                case DataType.ETypeCode.UInt64:
                    return Add<ulong>((ulong)value1, (ulong)value2);
                case DataType.ETypeCode.Int16:
                    return Add<short>((short)value1, (short)value2);
                case DataType.ETypeCode.Int32:
                    return Add<int>((int)value1, (int)value2);
                case DataType.ETypeCode.Int64:
                    return Add<long>((long)value1, (long)value2);
                case DataType.ETypeCode.Decimal:
                    return Add<decimal>((decimal)value1, (decimal)value2);
                case DataType.ETypeCode.Double:
                    return Add<double>((double)value1, (double)value2);
                case DataType.ETypeCode.Single:
                    return Add<float>((float)value1, (float)value2);
                case DataType.ETypeCode.String:
                    return Add<string>((string)value1, (string)value2);
                case DataType.ETypeCode.Text:
                    return Add<string>((string)value1, (string)value2);
                case DataType.ETypeCode.Boolean:
                    return Add<bool>((bool)value1, (bool)value2);
                case DataType.ETypeCode.DateTime:
                    return Add<DateTime>((DateTime)value1, (DateTime)value2);
                case DataType.ETypeCode.Time:
                    return Add<TimeSpan>((TimeSpan)value1, (TimeSpan)value2);
                case DataType.ETypeCode.Guid:
                    return Add<Guid>((Guid)value1, (Guid)value2);
                case DataType.ETypeCode.Unknown:
                    return Add<string>((string)value1, (string)value2);
                case DataType.ETypeCode.Json:
                    return Add<JToken>((JToken)value1, (JToken)value2);
                case DataType.ETypeCode.Xml:
                    return Add<XmlDocument>((XmlDocument)value1, (XmlDocument)value2);
                case DataType.ETypeCode.Enum:
                    return Add<int>((int)value1, (int)value2);
                case DataType.ETypeCode.Char:
                    return Add<char[]>((char[])value1, (char[])value2);
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
            if (type == ConvertTypes[ConvertTypeBool]) return DivideInt<bool>(a, b);
            if (type == ConvertTypes[ConvertTypeSbyte]) return DivideInt<sbyte>(a, b);
            if (type == ConvertTypes[ConvertTypeByte]) return DivideInt<byte>(a, b);
            if (type == ConvertTypes[ConvertTypeShort]) return DivideInt<short>(a, b);
            if (type == ConvertTypes[ConvertTypeUShort]) return DivideInt<ushort>(a, b);
            if (type == ConvertTypes[ConvertTypeInt]) return DivideInt<int>(a, b);
            if (type == ConvertTypes[ConvertTypeUint]) return DivideInt<uint>(a, b);
            if (type == ConvertTypes[ConvertTypeLong]) return DivideInt<long>(a, b);
            if (type == ConvertTypes[ConvertTypeULong]) return DivideInt<ulong>(a, b);
            if (type == ConvertTypes[ConvertTypeFloat]) return DivideInt<float>(a, b);
            if (type == ConvertTypes[ConvertTypeDouble]) return DivideInt<double>(a, b);
            if (type == ConvertTypes[ConvertTypeDecimal]) return DivideInt<decimal>(a, b);
            if (type == ConvertTypes[ConvertTypeDateTime]) return DivideInt<DateTime>(a, b);
            if (type == ConvertTypes[ConvertTypeString]) return DivideInt<string>(a, b);
            if (type == ConvertTypes[ConvertTypeByteArray]) return DivideInt<byte[]>(a, b);
            if (type == ConvertTypes[ConvertTypeCharArray]) return DivideInt<char[]>(a, b);
            if (type == ConvertTypes[ConvertTypeJToken]) return DivideInt<JToken>(a, b);
            if (type == ConvertTypes[ConvertTypeXmlDocument]) return DivideInt<XmlDocument>(a, b);
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return DivideInt<TimeSpan>(a, b);

            throw new ArgumentOutOfRangeException(nameof(type), a, null);
        }

        public static object DivideInt(DataType.ETypeCode typeCode, object value1, int value2)
        {
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                    return DivideInt<byte[]>(value1, value2);
                case DataType.ETypeCode.Byte:
                    return DivideInt<byte>(value1, value2);
                case DataType.ETypeCode.SByte:
                    return DivideInt<sbyte>(value1, value2);
                case DataType.ETypeCode.UInt16:
                    return DivideInt<ushort>(value1, value2);
                case DataType.ETypeCode.UInt32:
                    return DivideInt<uint>(value1, value2);
                case DataType.ETypeCode.UInt64:
                    return DivideInt<ulong>(value1, value2);
                case DataType.ETypeCode.Int16:
                    return DivideInt<short>(value1, value2);
                case DataType.ETypeCode.Int32:
                    return DivideInt<int>(value1, value2);
                case DataType.ETypeCode.Int64:
                    return DivideInt<long>(value1, value2);
                case DataType.ETypeCode.Decimal:
                    return DivideInt<decimal>(value1, value2);
                case DataType.ETypeCode.Double:
                    return DivideInt<double>(value1, value2);
                case DataType.ETypeCode.Single:
                    return DivideInt<float>(value1, value2);
                case DataType.ETypeCode.String:
                    return DivideInt<string>(value1, value2);
                case DataType.ETypeCode.Text:
                    return DivideInt<string>(value1, value2);
                case DataType.ETypeCode.Boolean:
                    return DivideInt<bool>(value1, value2);
                case DataType.ETypeCode.DateTime:
                    return DivideInt<DateTime>(value1, value2);
                case DataType.ETypeCode.Time:
                    return DivideInt<TimeSpan>(value1, value2);
                case DataType.ETypeCode.Guid:
                    return DivideInt<Guid>(value1, value2);
                case DataType.ETypeCode.Unknown:
                    return DivideInt<string>(value1, value2);
                case DataType.ETypeCode.Json:
                    return DivideInt<JToken>(value1, value2);
                case DataType.ETypeCode.Xml:
                    return DivideInt<XmlDocument>(value1, value2);
                case DataType.ETypeCode.Enum:
                    return DivideInt<int>(value1, value2);
                case DataType.ETypeCode.Char:
                    return DivideInt<char[]>(value1, value2);
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
            if (type == ConvertTypes[ConvertTypeBool]) return Negate<bool>(a);
            if (type == ConvertTypes[ConvertTypeSbyte]) return Negate<sbyte>(a);
            if (type == ConvertTypes[ConvertTypeByte]) return Negate<byte>(a);
            if (type == ConvertTypes[ConvertTypeShort]) return Negate<short>(a);
            if (type == ConvertTypes[ConvertTypeUShort]) return Negate<ushort>(a);
            if (type == ConvertTypes[ConvertTypeInt]) return Negate<int>(a);
            if (type == ConvertTypes[ConvertTypeUint]) return Negate<uint>(a);
            if (type == ConvertTypes[ConvertTypeLong]) return Negate<long>(a);
            if (type == ConvertTypes[ConvertTypeULong]) return Negate<ulong>(a);
            if (type == ConvertTypes[ConvertTypeFloat]) return Negate<float>(a);
            if (type == ConvertTypes[ConvertTypeDouble]) return Negate<double>(a);
            if (type == ConvertTypes[ConvertTypeDecimal]) return Negate<decimal>(a);
            if (type == ConvertTypes[ConvertTypeDateTime]) return Negate<DateTime>(a);
            if (type == ConvertTypes[ConvertTypeString]) return Negate<string>(a);
            if (type == ConvertTypes[ConvertTypeByteArray]) return Negate<byte[]>(a);
            if (type == ConvertTypes[ConvertTypeCharArray]) return Negate<char[]>(a);
            if (type == ConvertTypes[ConvertTypeJToken]) return Negate<JToken>(a);
            if (type == ConvertTypes[ConvertTypeXmlDocument]) return Negate<XmlDocument>(a);
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return Negate<TimeSpan>(a);

            throw new ArgumentOutOfRangeException(nameof(type), a, null);
        }

        public static object Negate(DataType.ETypeCode typeCode, object value1)
        {
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                    return Negate<byte[]>(value1);
                case DataType.ETypeCode.Byte:
                    return Negate<byte>(value1);
                case DataType.ETypeCode.SByte:
                    return Negate<sbyte>(value1);
                case DataType.ETypeCode.UInt16:
                    return Negate<ushort>(value1);
                case DataType.ETypeCode.UInt32:
                    return Negate<uint>(value1);
                case DataType.ETypeCode.UInt64:
                    return Negate<ulong>(value1);
                case DataType.ETypeCode.Int16:
                    return Negate<short>(value1);
                case DataType.ETypeCode.Int32:
                    return Negate<int>(value1);
                case DataType.ETypeCode.Int64:
                    return Negate<long>(value1);
                case DataType.ETypeCode.Decimal:
                    return Negate<decimal>(value1);
                case DataType.ETypeCode.Double:
                    return Negate<double>(value1);
                case DataType.ETypeCode.Single:
                    return Negate<float>(value1);
                case DataType.ETypeCode.String:
                    return Negate<string>(value1);
                case DataType.ETypeCode.Text:
                    return Negate<string>(value1);
                case DataType.ETypeCode.Boolean:
                    return Negate<bool>(value1);
                case DataType.ETypeCode.DateTime:
                    return Negate<DateTime>(value1);
                case DataType.ETypeCode.Time:
                    return Negate<TimeSpan>(value1);
                case DataType.ETypeCode.Guid:
                    return Negate<Guid>(value1);
                case DataType.ETypeCode.Unknown:
                    return Negate<string>(value1);
                case DataType.ETypeCode.Json:
                    return Negate<JToken>(value1);
                case DataType.ETypeCode.Xml:
                    return Negate<XmlDocument>(value1);
                case DataType.ETypeCode.Enum:
                    return Negate<int>(value1);
                case DataType.ETypeCode.Char:
                    return Negate<char[]>(value1);
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
            if (type == ConvertTypes[ConvertTypeBool]) return Increment<bool>(a);
            if (type == ConvertTypes[ConvertTypeSbyte]) return Increment<sbyte>(a);
            if (type == ConvertTypes[ConvertTypeByte]) return Increment<byte>(a);
            if (type == ConvertTypes[ConvertTypeShort]) return Increment<short>(a);
            if (type == ConvertTypes[ConvertTypeUShort]) return Increment<ushort>(a);
            if (type == ConvertTypes[ConvertTypeInt]) return Increment<int>(a);
            if (type == ConvertTypes[ConvertTypeUint]) return Increment<uint>(a);
            if (type == ConvertTypes[ConvertTypeLong]) return Increment<long>(a);
            if (type == ConvertTypes[ConvertTypeULong]) return Increment<ulong>(a);
            if (type == ConvertTypes[ConvertTypeFloat]) return Increment<float>(a);
            if (type == ConvertTypes[ConvertTypeDouble]) return Increment<double>(a);
            if (type == ConvertTypes[ConvertTypeDecimal]) return Increment<decimal>(a);
            if (type == ConvertTypes[ConvertTypeDateTime]) return Increment<DateTime>(a);
            if (type == ConvertTypes[ConvertTypeString]) return Increment<string>(a);
            if (type == ConvertTypes[ConvertTypeByteArray]) return Increment<byte[]>(a);
            if (type == ConvertTypes[ConvertTypeCharArray]) return Increment<char[]>(a);
            if (type == ConvertTypes[ConvertTypeJToken]) return Increment<JToken>(a);
            if (type == ConvertTypes[ConvertTypeXmlDocument]) return Increment<XmlDocument>(a);
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return Increment<TimeSpan>(a);

            throw new ArgumentOutOfRangeException(nameof(type), a, null);
        }

        public static object Increment(DataType.ETypeCode typeCode, object value1)
        {
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                    return Increment<byte[]>(value1);
                case DataType.ETypeCode.Byte:
                    return Increment<byte>(value1);
                case DataType.ETypeCode.SByte:
                    return Increment<sbyte>(value1);
                case DataType.ETypeCode.UInt16:
                    return Increment<ushort>(value1);
                case DataType.ETypeCode.UInt32:
                    return Increment<uint>(value1);
                case DataType.ETypeCode.UInt64:
                    return Increment<ulong>(value1);
                case DataType.ETypeCode.Int16:
                    return Increment<short>(value1);
                case DataType.ETypeCode.Int32:
                    return Increment<int>(value1);
                case DataType.ETypeCode.Int64:
                    return Increment<long>(value1);
                case DataType.ETypeCode.Decimal:
                    return Increment<decimal>(value1);
                case DataType.ETypeCode.Double:
                    return Increment<double>(value1);
                case DataType.ETypeCode.Single:
                    return Increment<float>(value1);
                case DataType.ETypeCode.String:
                    return Increment<string>(value1);
                case DataType.ETypeCode.Text:
                    return Increment<string>(value1);
                case DataType.ETypeCode.Boolean:
                    return Increment<bool>(value1);
                case DataType.ETypeCode.DateTime:
                    return Increment<DateTime>(value1);
                case DataType.ETypeCode.Time:
                    return Increment<TimeSpan>(value1);
                case DataType.ETypeCode.Guid:
                    return Increment<Guid>(value1);
                case DataType.ETypeCode.Unknown:
                    return Increment<string>(value1);
                case DataType.ETypeCode.Json:
                    return Increment<JToken>(value1);
                case DataType.ETypeCode.Xml:
                    return Increment<XmlDocument>(value1);
                case DataType.ETypeCode.Enum:
                    return Increment<int>(value1);
                case DataType.ETypeCode.Char:
                    return Increment<char[]>(value1);
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
            if (type == ConvertTypes[ConvertTypeBool]) return Decrement<bool>(a);
            if (type == ConvertTypes[ConvertTypeSbyte]) return Decrement<sbyte>(a);
            if (type == ConvertTypes[ConvertTypeByte]) return Decrement<byte>(a);
            if (type == ConvertTypes[ConvertTypeShort]) return Decrement<short>(a);
            if (type == ConvertTypes[ConvertTypeUShort]) return Decrement<ushort>(a);
            if (type == ConvertTypes[ConvertTypeInt]) return Decrement<int>(a);
            if (type == ConvertTypes[ConvertTypeUint]) return Decrement<uint>(a);
            if (type == ConvertTypes[ConvertTypeLong]) return Decrement<long>(a);
            if (type == ConvertTypes[ConvertTypeULong]) return Decrement<ulong>(a);
            if (type == ConvertTypes[ConvertTypeFloat]) return Decrement<float>(a);
            if (type == ConvertTypes[ConvertTypeDouble]) return Decrement<double>(a);
            if (type == ConvertTypes[ConvertTypeDecimal]) return Decrement<decimal>(a);
            if (type == ConvertTypes[ConvertTypeDateTime]) return Decrement<DateTime>(a);
            if (type == ConvertTypes[ConvertTypeString]) return Decrement<string>(a);
            if (type == ConvertTypes[ConvertTypeByteArray]) return Decrement<byte[]>(a);
            if (type == ConvertTypes[ConvertTypeCharArray]) return Decrement<char[]>(a);
            if (type == ConvertTypes[ConvertTypeJToken]) return Decrement<JToken>(a);
            if (type == ConvertTypes[ConvertTypeXmlDocument]) return Decrement<XmlDocument>(a);
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return Decrement<TimeSpan>(a);

            throw new ArgumentOutOfRangeException(nameof(type), a, null);
        }

        public static object Decrement(DataType.ETypeCode typeCode, object value1)
        {
            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                    return Decrement<byte[]>(value1);
                case DataType.ETypeCode.Byte:
                    return Decrement<byte>(value1);
                case DataType.ETypeCode.SByte:
                    return Decrement<sbyte>(value1);
                case DataType.ETypeCode.UInt16:
                    return Decrement<ushort>(value1);
                case DataType.ETypeCode.UInt32:
                    return Decrement<uint>(value1);
                case DataType.ETypeCode.UInt64:
                    return Decrement<ulong>(value1);
                case DataType.ETypeCode.Int16:
                    return Decrement<short>(value1);
                case DataType.ETypeCode.Int32:
                    return Decrement<int>(value1);
                case DataType.ETypeCode.Int64:
                    return Decrement<long>(value1);
                case DataType.ETypeCode.Decimal:
                    return Decrement<decimal>(value1);
                case DataType.ETypeCode.Double:
                    return Decrement<double>(value1);
                case DataType.ETypeCode.Single:
                    return Decrement<float>(value1);
                case DataType.ETypeCode.String:
                    return Decrement<string>(value1);
                case DataType.ETypeCode.Text:
                    return Decrement<string>(value1);
                case DataType.ETypeCode.Boolean:
                    return Decrement<bool>(value1);
                case DataType.ETypeCode.DateTime:
                    return Decrement<DateTime>(value1);
                case DataType.ETypeCode.Time:
                    return Decrement<TimeSpan>(value1);
                case DataType.ETypeCode.Guid:
                    return Decrement<Guid>(value1);
                case DataType.ETypeCode.Unknown:
                    return Decrement<string>(value1);
                case DataType.ETypeCode.Json:
                    return Decrement<JToken>(value1);
                case DataType.ETypeCode.Xml:
                    return Decrement<XmlDocument>(value1);
                case DataType.ETypeCode.Enum:
                    return Decrement<int>(value1);
                case DataType.ETypeCode.Char:
                    return Decrement<char[]>(value1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }       
        
        public static int Compare(object inputValue, object compareTo)
        {
            var type = inputValue?.GetType();
            return Compare(type, inputValue, compareTo);
        }

        public static int Compare(Type type, object inputValue, object compareTo)
        {
            if ((inputValue == null || inputValue == DBNull.Value) && (compareTo == null || compareTo == DBNull.Value))
                return 0;

            if (inputValue == null || inputValue == DBNull.Value || compareTo == null || compareTo == DBNull.Value)
                return (inputValue == null || inputValue is DBNull) ? -1 : 1;

            
            if (type == ConvertTypes[ConvertTypeBool]) return Compare<bool>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeSbyte]) return Compare<sbyte>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeByte]) return Compare<byte>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeShort]) return Compare<short>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeUShort]) return Compare<ushort>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeInt]) return Compare<int>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeUint]) return Compare<uint>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeLong]) return Compare<long>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeULong]) return Compare<ulong>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeFloat]) return Compare<float>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeDouble]) return Compare<double>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeDecimal]) return Compare<decimal>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeDateTime]) return Compare<DateTime>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeString]) return Compare<string>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeByteArray]) return Compare<byte[]>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeCharArray]) return Compare<char[]>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeJToken]) return Compare<JToken>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeXmlDocument]) return Compare<XmlDocument>(inputValue, compareTo);
            if (type == ConvertTypes[ConvertTypeTimeSpan]) return Compare<TimeSpan>(inputValue, compareTo);

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
                    return Compare<byte[]>(inputValue, compareTo);
                case DataType.ETypeCode.Byte:
                    return Compare<byte>(inputValue, compareTo);
                case DataType.ETypeCode.SByte:
                    return Compare<sbyte>(inputValue, compareTo);
                case DataType.ETypeCode.UInt16:
                    return Compare<ushort>(inputValue, compareTo);
                case DataType.ETypeCode.UInt32:
                    return Compare<uint>(inputValue, compareTo);
                case DataType.ETypeCode.UInt64:
                    return Compare<ulong>(inputValue, compareTo);
                case DataType.ETypeCode.Int16:
                    return Compare<short>(inputValue, compareTo);
                case DataType.ETypeCode.Int32:
                    return Compare<int>(inputValue, compareTo);
                case DataType.ETypeCode.Int64:
                    return Compare<long>(inputValue, compareTo);
                case DataType.ETypeCode.Decimal:
                    return Compare<decimal>(inputValue, compareTo);
                case DataType.ETypeCode.Double:
                    return Compare<double>(inputValue, compareTo);
                case DataType.ETypeCode.Single:
                    return Compare<float>(inputValue, compareTo);
                case DataType.ETypeCode.String:
                    return Compare<string>(inputValue, compareTo);
                case DataType.ETypeCode.Text:
                    return Compare<string>(inputValue, compareTo);
                case DataType.ETypeCode.Boolean:
                    return Compare<bool>(inputValue, compareTo);
                case DataType.ETypeCode.DateTime:
                    return Compare<DateTime>(inputValue, compareTo);
                case DataType.ETypeCode.Time:
                    return Compare<TimeSpan>(inputValue, compareTo);
                case DataType.ETypeCode.Guid:
                    return Compare<Guid>(inputValue, compareTo);
                case DataType.ETypeCode.Unknown:
                    return Compare<string>(inputValue, compareTo);
                case DataType.ETypeCode.Json:
                    return Compare<JToken>(inputValue, compareTo);
                case DataType.ETypeCode.Xml:
                    return Compare<XmlDocument>(inputValue, compareTo);
                case DataType.ETypeCode.Enum:
                    return Compare<int>(inputValue, compareTo);
                case DataType.ETypeCode.Char:
                    return Compare<char[]>(inputValue, compareTo);
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }
    }


    public static class Operations<T>
    {
        public static readonly Lazy<Func<T, T, T>> Add = CreateExpressionNumeric(Expression.Add);
        public static readonly Lazy<Func<T, T, T>> Subtract = CreateExpressionNumeric(Expression.Subtract);
        public static readonly Lazy<Func<T, T, T>> Multiply = CreateExpressionNumeric(Expression.Multiply);
        public static readonly Lazy<Func<T, T, T>> Divide = CreateExpressionNumeric(Expression.Divide);
        public static readonly Lazy<Func<T, int, T>> DivideInt = CreateDivideInt();
        public static readonly Lazy<Func<T, T>> Negate = CreateNegate();
        public static readonly Lazy<Func<T, T>> Increment = CreateIncrement();
        public static readonly Lazy<Func<T, T>> Decrement = CreateDecrement();
        
        public static readonly Lazy<Func<T, T, bool>> GreaterThan = CreateCondition(Expression.Equal, new[] {1});
        public static readonly Lazy<Func<T, T, bool>> LessThan = CreateCondition(Expression.Equal, new[] {-1});
        public static readonly Lazy<Func<T, T, bool>> GreaterThanOrEqual = CreateCondition(Expression.Equal, new[] {0,1});
        public static readonly Lazy<Func<T, T, bool>> LessThanOrEqual = CreateCondition(Expression.Equal, new[] {0,-1});
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