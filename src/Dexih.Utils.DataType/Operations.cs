using System;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dexih.Utils.DataType
{
    public static class Operations
    {
       
        public static T Add<T>(T a, T b) => Operations<T>.Add.Value(a,b);
        public static T Subtract<T>(T a, T b) => Operations<T>.Subtract.Value(a, b);
        public static T Divide<T>(T a, T b) => Operations<T>.Divide.Value(a, b);
        public static T DivideInt<T>(T a, int value2) => Operations<T>.DivideInt.Value(a, value2);
        public static T Multiply<T>(T a, T b) => Operations<T>.Multiply.Value(a, b);
        public static T Negate<T>(T a) => Operations<T>.Negate.Value(a);
        public static object Negate<T>(object value) => Operations<object>.Negate.Value(value);
        public static T Increment<T>(T a) => Operations<T>.Increment.Value(a);
        public static object Increment<T>(object value) => Operations<object>.Increment.Value(value);
        public static T Decrement<T>(T a) => Operations<T>.Decrement.Value(a);
        public static object Decrement<T>(object value) => Operations<object>.Decrement.Value(value);
        public static bool GreaterThan<T>(T a, T b) => Operations<T>.GreaterThan.Value(a, b);
        public static bool GreaterThan<T>(object value1, object value2) => Operations<object>.GreaterThan.Value(value1, value2);
        public static bool LessThan<T>(object value1, object value2) => Operations<object>.LessThan.Value(value1, value2);
        public static bool LessThan<T>(T a, T b) => Operations<T>.LessThan.Value(a, b);
        public static bool GreaterThanOrEqual<T>(object value1, object value2) => Operations<object>.GreaterThanOrEqual.Value(value1, value2);
        public static bool GreaterThanOrEqual<T>(T a, T b) => Operations<T>.GreaterThanOrEqual.Value(a, b);
        public static bool LessThanOrEqual<T>(T a, T b) => Operations<T>.LessThanOrEqual.Value(a, b);
        public static bool LessThanOrEqual<T>(object value1, object value2) => Operations<object>.LessThanOrEqual.Value(value1, value2);
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
                    return inputValue is byte[] ? inputValue : Parse<byte[]>(inputValue);
                case DataType.ETypeCode.Byte:
                    return inputValue is byte ? inputValue : Parse<byte>(inputValue);
                case DataType.ETypeCode.Char:
                    return inputValue is char ? inputValue : Parse<char>(inputValue);
                case DataType.ETypeCode.SByte:
                    return inputValue is sbyte ? inputValue : Parse<sbyte>(inputValue);
                case DataType.ETypeCode.UInt16:
                    return inputValue is ushort ? inputValue : Parse<ushort>(inputValue);
                case DataType.ETypeCode.UInt32:
                    return inputValue is uint ? inputValue : Parse<uint>(inputValue);
                case DataType.ETypeCode.UInt64:
                    return inputValue is ulong ? inputValue : Parse<ulong>(inputValue);
                case DataType.ETypeCode.Int16:
                    return inputValue is short ? inputValue : Parse<short>(inputValue);
                case DataType.ETypeCode.Int32:
                    return inputValue is int ? inputValue :  Parse<int>(inputValue);
                case DataType.ETypeCode.Int64:
                    return inputValue is long ? inputValue :  Parse<long>(inputValue);
                case DataType.ETypeCode.Decimal:
                    return inputValue is decimal ? inputValue : Parse<decimal>(inputValue);
                case DataType.ETypeCode.Double:
                    return inputValue is double ? inputValue : Parse<double>(inputValue);
                case DataType.ETypeCode.Single:
                    return inputValue is float ? inputValue : Parse<float>(inputValue);
                case DataType.ETypeCode.String:
                    return inputValue is string ? inputValue : Parse<string>(inputValue);
                case DataType.ETypeCode.Text:
                    return inputValue is string ? inputValue : Parse<string>(inputValue);
                case DataType.ETypeCode.Boolean:
                    return inputValue is bool ? inputValue : Parse<bool>(inputValue);
                case DataType.ETypeCode.DateTime:
                    return inputValue is DateTime ? inputValue : Parse<DateTime>(inputValue);
                case DataType.ETypeCode.Time:
                    return inputValue is TimeSpan ? inputValue : Parse<TimeSpan>(inputValue);
                case DataType.ETypeCode.Guid:
                    return inputValue is Guid ? inputValue : Parse<Guid>(inputValue);
                case DataType.ETypeCode.Unknown:
                    return inputValue is string ? inputValue : Parse<string>(inputValue);
                case DataType.ETypeCode.Json:
                case DataType.ETypeCode.Node:
                    return inputValue is JToken ? inputValue : Parse<JToken>(inputValue);
                case DataType.ETypeCode.Xml:
                    return inputValue is XmlDocument ? inputValue : Parse<XmlDocument>(inputValue);
                case DataType.ETypeCode.Enum:
                    return inputValue is Enum ? inputValue : Parse<string>(inputValue);
                case DataType.ETypeCode.CharArray:
                    return inputValue is char[] ? inputValue : Parse<char[]>(inputValue);
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

            if (type == inputValue.GetType())
            {
                return inputValue;
            }

            var typeCode = DataType.GetTypeCode(type, out int rank);
            return Parse(typeCode, rank, inputValue);

        }

        public static object Parse(DataType.ETypeCode tryDataType, int rank, object inputValue)
        {
            if (rank == 0)
            {
                return Parse(tryDataType, inputValue);
            }
            
            var dataType = DataType.GetType(tryDataType);

            if (inputValue is JArray jArray)
            {
                if (rank == 1)
                {
                    var returnValue = Array.CreateInstance(dataType, jArray.Count);
                    for (var i = 0; i < jArray.Count; i++)
                    {
                        returnValue.SetValue(Parse(tryDataType, 0, jArray[i]), i);
                    }

                    return returnValue;
                } else if (rank == 2)
                {
                    var array2 = (JArray) jArray.First();
                    var returnValue = Array.CreateInstance(dataType, jArray.Count, array2.Count);
                    
                    for (var i = 0; i < jArray.Count; i++)
                    {
                        array2 = (JArray) jArray[i];
                        for (var j = 0; j < array2.Count; j++)
                        {
                            returnValue.SetValue(Parse(tryDataType, 0, array2[j]), i, j);
                        }
                    }

                    return returnValue;
                }
            }

            var type = inputValue.GetType();
            if ((type.IsArray) && type != typeof(byte[]) && type != typeof(char[]))
            {
                if (rank == 1)
                {
                    var inputArray = (Array) inputValue;
                    var returnValue = Array.CreateInstance(dataType, inputArray.Length);
                    for (var i = 0; i < inputArray.Length; i++)
                    {
                        returnValue.SetValue(Parse(tryDataType, 0, inputArray.GetValue(i)), i);
                    }

                    return returnValue;
                } else if (rank == 2)
                {
                    var inputArray = (Array) inputValue;
                    var returnValue = Array.CreateInstance(dataType, inputArray.GetLength(0), inputArray.GetLength(1));
                    
                    for (var i = 0; i < inputArray.GetLength(0); i++)
                    {
                        for (var j = 0; j < inputArray.GetLength(1); j++)
                        {
                            returnValue.SetValue(Parse(tryDataType, 0, inputArray.GetValue(i,j)), i, j);
                        }
                    }

                    return returnValue;
                }
            }
            

            if (type == typeof(string))
            {
                var tryType = DataType.GetType(tryDataType);
                var arrayType = tryType.MakeArrayType(rank);
                var result = JsonConvert.DeserializeObject((string)inputValue, arrayType);
                return result;
            }

            return Parse(tryDataType, inputValue);
        }
        
        public static object Parse(Type type, int rank, object inputValue)
        {
            var typeCode = DataType.GetTypeCode(type, out var rank1);

            if (rank1 > 0)
            {
                throw new ArgumentOutOfRangeException(nameof(type), inputValue, "The type was an array, use the rank parameter to specify arrays.");   
            }

            return Parse(typeCode, rank, inputValue);

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
            
            var value1 = Parse(type, inputValue);
            var value2 = Parse(type, compareTo);
            
            return Equals(value1, value2);

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

            var value1Converted = Parse(typeCode, value1);
            var value2Converted = Parse(typeCode, value2);

            return object.Equals(value1Converted, value2Converted);

        }
        
        public static bool GreaterThan(object value1, object value2)
        {
            var type = value1?.GetType();
            return GreaterThan(type, value1, value2);
        }
        
        public static bool GreaterThan(Type type, object value1, object value2)
        {
            if ((value1 == null || value1 == DBNull.Value))
                return false;

            if (value2 == null || value2 == DBNull.Value)
                return true;
            
            var typeCode = DataType.GetTypeCode(type, out _);
            return GreaterThan(typeCode, value1, value2);

        }

        public static bool GreaterThan(DataType.ETypeCode typeCode,  object value1, object value2)
        {
            if ((value1 == null || value1 == DBNull.Value))
                return false;

            if (value2 == null || value2 == DBNull.Value)
                return true;

            value1 = Parse(typeCode, value1);
            value2 = Parse(typeCode, value2);

            switch (typeCode)
            {
                case DataType.ETypeCode.Unknown:
                case DataType.ETypeCode.Json:
                case DataType.ETypeCode.Xml:
                case DataType.ETypeCode.Node:
                    throw new Exception($"Cannot compare {typeCode} types.");
                case DataType.ETypeCode.Binary:
                    return ByteArrayIsGreater((byte[])value1, (byte[])value2,false);
                case DataType.ETypeCode.CharArray:
                    return CharArrayIsGreater((char[])value1, (char[])value2,false);
                case DataType.ETypeCode.String:
                case DataType.ETypeCode.Text:
                    return string.Compare((string)value1, (string)value2) > 0;
                case DataType.ETypeCode.Guid:
                    return string.Compare(value1.ToString(), value2.ToString()) > 0;
                case DataType.ETypeCode.DateTime:
                    return (DateTime) value1 > (DateTime) value2;
                case DataType.ETypeCode.Boolean:
                    return BoolIsGreaterThan((bool) value1, (bool) value2);
                case DataType.ETypeCode.Time:
                    return (TimeSpan) value1 > (TimeSpan) value2;
                case DataType.ETypeCode.Byte:
                    return ((byte) value1 > (byte) value2);
                case DataType.ETypeCode.Char:
                    return ((char) value1 > (char) value2);
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

       public static bool GreaterThanOrEqual(object value1, object value2)
        {
            var type = value1?.GetType();
            return GreaterThanOrEqual(type, value1, value2);
        }
        
        public static bool GreaterThanOrEqual(Type type, object value1, object value2)
        {
            if ((value1 == null || value1 == DBNull.Value))
                return (value2 == null || value2 == DBNull.Value);

            if (value2 == null || value2 == DBNull.Value)
                return true;

            var typeCode = DataType.GetTypeCode(type, out _);
            return GreaterThanOrEqual(typeCode, value1, value2);

        }

        public static bool GreaterThanOrEqual(DataType.ETypeCode typeCode,  object value1, object value2)
        {
            if ((value1 == null || value1 == DBNull.Value))
                return (value2 == null || value2 == DBNull.Value);

            if (value2 == null || value2 == DBNull.Value)
                return true;

            value1 = Parse(typeCode, value1);
            value2 = Parse(typeCode, value2);

            switch (typeCode)
            {

                case DataType.ETypeCode.Unknown:
                case DataType.ETypeCode.Json:
                case DataType.ETypeCode.Xml:
                case DataType.ETypeCode.Node:
                    throw new Exception($"Cannot compare {typeCode} types.");
                case DataType.ETypeCode.CharArray:
                    return CharArrayIsGreater((char[])value1, (char[])value2,true);
                case DataType.ETypeCode.Binary:
                    return ByteArrayIsGreater((byte[])value1, (byte[])value2,true);
                case DataType.ETypeCode.String:
                case DataType.ETypeCode.Text:
                    return string.Compare((string)value1, (string)value2) >= 0;
                case DataType.ETypeCode.Guid:
                    return string.Compare(value1.ToString(), value2.ToString()) >= 0;
                case DataType.ETypeCode.DateTime:
                    return (DateTime) value1 >= (DateTime) value2;
                case DataType.ETypeCode.Boolean:
                    return BoolIsGreaterThanOrEqual((bool) value1, (bool) value2);
                case DataType.ETypeCode.Time:
                    return (TimeSpan) value1 >= (TimeSpan) value2;
                case DataType.ETypeCode.Byte:
                    return ((byte) value1 >= (byte) value2);
                case DataType.ETypeCode.Char:
                    return ((char) value1 >= (char) value2);
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

        
        public static bool LessThan(object value1, object value2)
        {
            var type = value1?.GetType();
            return LessThan(type, value1, value2);
        }
        
        public static bool LessThan(Type type, object value1, object value2)
        {
            if ((value2 == null || value2 == DBNull.Value))
                return false;

            if (value1 == null || value1 == DBNull.Value)
                return true;

            var typeCode = DataType.GetTypeCode(type, out _);
            return LessThan(typeCode, value1, value2);

        }

        public static bool LessThan(DataType.ETypeCode typeCode,  object value1, object value2)
        {
            if ((value2 == null || value2 == DBNull.Value))
                return false;

            if (value1 == null || value1 == DBNull.Value)
                return true;
            
            value1 = Parse(typeCode, value1);
            value2 = Parse(typeCode, value2);

            switch (typeCode)
            {

                case DataType.ETypeCode.Unknown:
                case DataType.ETypeCode.Json:
                case DataType.ETypeCode.Xml:
                case DataType.ETypeCode.Node:
                    throw new Exception($"Cannot compare {typeCode} types.");
                case DataType.ETypeCode.Binary:
                    return ByteArrayIsLessThan((byte[])value1, (byte[])value2,false);
                case DataType.ETypeCode.CharArray:
                    return CharArrayIsLessThan((char[])value1, (char[])value2,false);
                case DataType.ETypeCode.String:
                case DataType.ETypeCode.Text:
                    return string.Compare((string)value1, (string)value2) < 0;
                case DataType.ETypeCode.Guid:
                    return string.Compare(value1.ToString(), value2.ToString()) < 0;
                case DataType.ETypeCode.DateTime:
                    return (DateTime) value1 < (DateTime) value2;
                case DataType.ETypeCode.Boolean:
                    return BoolIsLessThan((bool) value1, (bool) value2);
                case DataType.ETypeCode.Time:
                    return (TimeSpan) value1 < (TimeSpan) value2;
                case DataType.ETypeCode.Byte:
                    return ((byte) value1 < (byte) value2);
                case DataType.ETypeCode.Char:
                    return ((char) value1 < (char) value2);
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

        
        public static bool LessThanOrEqual(object value1, object value2)
        {
            var type = value1?.GetType();
            return LessThanOrEqual(type, value1, value2);
        }
        
        public static bool LessThanOrEqual(Type type, object value1, object value2)
        {
            if ((value2 == null || value2 == DBNull.Value))
                return (value1 == null || value1 == DBNull.Value);

            if (value1 == null || value1 == DBNull.Value)
                return true;

            var typeCode = DataType.GetTypeCode(type, out _);
            return LessThanOrEqual(typeCode, value1, value2);
        }

        public static bool LessThanOrEqual(DataType.ETypeCode typeCode,  object value1, object value2)
        {
            if ((value2 == null || value2 == DBNull.Value))
                return (value1 == null || value1 == DBNull.Value);

            if (value1 == null || value1 == DBNull.Value)
                return true;
            
            value1 = Parse(typeCode, value1);
            value2 = Parse(typeCode, value2);

            switch (typeCode)
            {
                case DataType.ETypeCode.Unknown:
                case DataType.ETypeCode.Json:
                case DataType.ETypeCode.Xml:
                case DataType.ETypeCode.Node:
                    throw new Exception($"Cannot compare {typeCode} types.");
                case DataType.ETypeCode.Binary:
                    return ByteArrayIsLessThan((byte[])value1, (byte[])value2,true);
                case DataType.ETypeCode.CharArray:
                    return CharArrayIsLessThan((char[])value1, (char[])value2,true);
                case DataType.ETypeCode.String:
                case DataType.ETypeCode.Text:
                    return string.Compare((string)value1, (string)value2) <= 0;
                case DataType.ETypeCode.Guid:
                    return string.Compare(value1.ToString(), value2.ToString()) <= 0;
                case DataType.ETypeCode.DateTime:
                    return (DateTime) value1 <= (DateTime) value2;
                case DataType.ETypeCode.Boolean:
                    return BoolIsLessThanOrEqual((bool) value1, (bool) value2);
                case DataType.ETypeCode.Time:
                    return (TimeSpan) value1 <= (TimeSpan) value2;
                case DataType.ETypeCode.Byte:
                    return ((byte) value1 <= (byte) value2);
                case DataType.ETypeCode.Char:
                    return ((char) value1 <= (char) value2);
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

        public static object Add(object value1, object value2)
        {
            var type = value1?.GetType();
            return Add(type, value1, value2);
        }
        
        public static object Add(Type type, object value1, object value2)
        {
            var typeCode = DataType.GetTypeCode(type, out _);
            return Add(typeCode, value1, value2);
        }

        public static object Add(DataType.ETypeCode typeCode,  object value1, object value2)
        {
            value1 = Parse(typeCode, value1);
            value2 = Parse(typeCode, value2);

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
                case DataType.ETypeCode.Node:
                    throw new Exception($"Cannot add {typeCode} types.");
                case DataType.ETypeCode.Time:
                    return (TimeSpan) value1 + (TimeSpan) value2;
                case DataType.ETypeCode.Byte:
                    return (byte)((byte) value1 + (byte) value2);
                case DataType.ETypeCode.Char:
                    return (byte)((char) value1 + (char) value2);
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

        public static object Subtract(object value1, object value2)
        {
            var type = value1?.GetType();
            return Subtract(type, value1, value2);
        }
        
        public static object Subtract(Type type, object value1, object value2)
        {
            var typeCode = DataType.GetTypeCode(type, out _);
            return Subtract(typeCode, value1, value2);
        }

        public static object Subtract(DataType.ETypeCode typeCode,  object value1, object value2)
        {
            value1 = Parse(typeCode, value1);
            value2 = Parse(typeCode, value2);

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
                case DataType.ETypeCode.Node:
                    throw new Exception($"Cannot subtract {typeCode} types.");
                case DataType.ETypeCode.Time:
                    return (TimeSpan) value1 - (TimeSpan) value2;
                case DataType.ETypeCode.Byte:
                    return (byte)((byte) value1 - (byte) value2);
                case DataType.ETypeCode.Char:
                    return (char)((char) value1 - (char) value2);
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

        public static object Divide(object value1, object value2)
        {
            var type = value1?.GetType();
            return Divide(type, value1, value2);
        }
        
        public static object Divide(Type type, object value1, object value2)
        {
            var typeCode = DataType.GetTypeCode(type, out _);
            return Divide(typeCode, value1, value2);
        }

        public static object Divide(DataType.ETypeCode typeCode,  object value1, object value2)
        {
            value1 = Parse(typeCode, value1);
            value2 = Parse(typeCode, value2);

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
                case DataType.ETypeCode.Node:
                    throw new Exception($"Cannot add {typeCode} types.");
                case DataType.ETypeCode.Byte:
                    return (byte)((byte) value1 / (byte) value2);
                case DataType.ETypeCode.Char:
                    return (char)((char) value1 / (char) value2);
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

        public static object Multiply(object value1, object value2)
        {
            var type = value1?.GetType();
            return Multiply(type, value1, value2);
        }
        
        public static object Multiply(Type type, object value1, object value2)
        {
            var typeCode = DataType.GetTypeCode(type, out _);
            return Multiply(typeCode, value1, value2);
        }

        public static object Multiply(DataType.ETypeCode typeCode,  object value1, object value2)
        {
            value1 = Parse(typeCode, value1);
            value2 = Parse(typeCode, value2);

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
                case DataType.ETypeCode.Node:
                    throw new Exception($"Cannot add {typeCode} types.");
                case DataType.ETypeCode.Byte:
                    return (byte)((byte) value1 * (byte) value2);
                case DataType.ETypeCode.Char:
                    return (char)((char) value1 * (char) value2);
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

        public static object DivideInt(object value1, int value2)
        {
            var type = value1?.GetType();
            return DivideInt(type, value1, value2);
        }
        
        public static object DivideInt(Type type, object value1, int value2)
        {
            var typeCode = DataType.GetTypeCode(type, out _);
            return DivideInt(typeCode, value1, value2);
        }

        public static object DivideInt(DataType.ETypeCode typeCode,  object value1, int value2)
        {
            value1 = Parse(typeCode, value1);

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
                case DataType.ETypeCode.Node:
                    throw new Exception($"Cannot add {typeCode} types.");
                case DataType.ETypeCode.Byte:
                    return (byte)((byte) value1 / value2);
                case DataType.ETypeCode.Char:
                    return (char)((char) value1 / value2);
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

       public static object Negate(object value)
        {
            var type = value?.GetType();
            return Negate(type, value);
        }
        
        public static object Negate(Type type, object value)
        {
            var typeCode = DataType.GetTypeCode(type, out _);
            return Negate(typeCode, value);
        }

        public static object Negate(DataType.ETypeCode typeCode,  object value1)
        {
            value1 = Parse(typeCode, value1);

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
                case DataType.ETypeCode.Node:
                    throw new Exception($"Cannot negate {typeCode} types.");
                case DataType.ETypeCode.Byte:
                    return (byte)((byte) value1 * -1);
                case DataType.ETypeCode.Char:
                    return (char)((char) value1 * -1);
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

       public static object Increment(object value)
        {
            var type = value?.GetType();
            return Increment(type, value);
        }
        
        public static object Increment(Type type, object value)
        {
            var typeCode = DataType.GetTypeCode(type, out _);
            return Increment(typeCode, value);
        }

        public static object Increment(DataType.ETypeCode typeCode,  object value1)
        {
            value1 = Parse(typeCode, value1);

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
                case DataType.ETypeCode.Node:
                    throw new Exception($"Cannot negate {typeCode} types.");
                case DataType.ETypeCode.Byte:
                    return (byte)((byte) value1 + 1);
                case DataType.ETypeCode.Char:
                    return (char)((char) value1 + 1);
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
 
       public static object Decrement(object value)
        {
            var type = value?.GetType();
            return Decrement(type, value);
        }
        
        public static object Decrement(Type type, object value)
        {
            var typeCode = DataType.GetTypeCode(type, out _);
            return Decrement(typeCode, value);
        }

        public static object Decrement(DataType.ETypeCode typeCode,  object value1)
        {
            value1 = Parse(typeCode, value1);

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
                case DataType.ETypeCode.Node:
                    throw new Exception($"Cannot negate {typeCode} types.");
                case DataType.ETypeCode.Byte:
                    return (byte)((byte) value1 - 1);
                case DataType.ETypeCode.Char:
                    return (char)((char) value1 - 1);
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

            var typeCode = DataType.GetTypeCode(type, out _);
            return Compare(typeCode, inputValue, compareTo);

        }

        public static int Compare(DataType.ETypeCode typeCode, object inputValue, object compareTo)
        {
            if ((inputValue == null || inputValue == DBNull.Value) && (compareTo == null || compareTo == DBNull.Value))
                return 0;

            if (inputValue == null || inputValue == DBNull.Value || compareTo == null || compareTo == DBNull.Value)
                return (inputValue == null || inputValue is DBNull) ? -1 : 1;
            
            inputValue = Parse(typeCode, inputValue);
            compareTo = Parse(typeCode, compareTo);

            switch (typeCode)
            {
                case DataType.ETypeCode.Binary:
                    return ByteArrayCompareTo((byte[])inputValue,(byte[])compareTo);
                case DataType.ETypeCode.Byte:
                    return ((byte)inputValue).CompareTo((byte)compareTo);
                case DataType.ETypeCode.Char:
                    return ((char)inputValue).CompareTo((char)compareTo);
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
                    return string.Compare((string) inputValue, (string) compareTo);
                case DataType.ETypeCode.Boolean:
                    return ((bool)inputValue).CompareTo((bool)compareTo);
                case DataType.ETypeCode.DateTime:
                    return ((DateTime)inputValue).CompareTo((DateTime)compareTo);
                case DataType.ETypeCode.Time:
                    return ((TimeSpan)inputValue).CompareTo((TimeSpan)compareTo);
                case DataType.ETypeCode.Guid:
                    return ((Guid)inputValue).CompareTo((Guid)compareTo);
                case DataType.ETypeCode.Unknown:
                    return string.Compare((inputValue.ToString()), compareTo.ToString(), StringComparison.Ordinal);
                case DataType.ETypeCode.Json:
                case DataType.ETypeCode.Node:
                    return string.Compare(inputValue.ToString(), compareTo.ToString());
                case DataType.ETypeCode.Xml:
                    return string.Compare(((XmlDocument)inputValue).InnerXml, ((XmlDocument)compareTo).InnerXml);
                case DataType.ETypeCode.Enum:
                    return string.Compare((string) inputValue, (string) compareTo);
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
                case TypeCode.Char:
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
                case TypeCode.Char:
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

                throw new DataTypeParseException("TimeSpan conversion is only supported for strings.");
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
                throw new DataTypeParseException("Guid conversion is only supported for strings.");
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
                throw new DataTypeParseException("Json conversion is only supported for strings.");
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
                
                throw new DataTypeParseException("Xml conversion is only supported for strings.");
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
                else if (value is XmlDocument xmlDocument)
                {
                    return (T) (object) xmlDocument.InnerXml;
                }
                else if (value is JValue jValue)
                {
                    return (T) (object) jValue.Value<string>();
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
                case TypeCode.Char:
                    exp = value => (T)(object) Convert.ToChar(value);
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