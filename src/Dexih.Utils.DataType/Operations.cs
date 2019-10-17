using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using System.Text.Json;

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
        
        public static object Parse(ETypeCode typeCode, object inputValue)
        {
            if (inputValue == null || inputValue == DBNull.Value)
            {
                return null;
            }

            if (inputValue is JsonElement jsonElement)
            {
                switch (typeCode)
                {
                    case ETypeCode.Binary:
                        return jsonElement.GetBytesFromBase64();
                    case ETypeCode.Geometry:
                        return jsonElement.GetRawText();
                    case ETypeCode.Byte:
                        return jsonElement.GetByte();
                    case ETypeCode.Char:
                        return Convert.ToChar(jsonElement.GetSByte());
                    case ETypeCode.SByte:
                        return jsonElement.GetSByte();
                    case ETypeCode.UInt16:
                        return jsonElement.GetUInt16();
                    case ETypeCode.UInt32:
                        return jsonElement.GetUInt32();
                    case ETypeCode.UInt64:
                        return jsonElement.GetUInt64();
                    case ETypeCode.Int16:
                        return jsonElement.GetInt16();
                    case ETypeCode.Int32:
                        return jsonElement.GetInt32();
                    case ETypeCode.Int64:
                        return jsonElement.GetInt64();
                    case ETypeCode.Decimal:
                        return jsonElement.GetDecimal();
                    case ETypeCode.Double:
                        return jsonElement.GetDouble();
                    case ETypeCode.Single:
                        return jsonElement.GetSingle();
                    case ETypeCode.String:
                    case ETypeCode.Text:
                        return jsonElement.GetString();
                    case ETypeCode.Boolean:
                        return jsonElement.GetBoolean();
                    case ETypeCode.DateTime:
                        return jsonElement.GetDateTime();
                    case ETypeCode.Time:
                        return TimeSpan.Parse(jsonElement.GetString());
                    case ETypeCode.Guid:
                        return jsonElement.GetGuid();
                    case ETypeCode.Unknown:
                        return jsonElement.GetRawText();
                    case ETypeCode.Json:
                        return jsonElement;
                    case ETypeCode.Xml:
                        return jsonElement.GetRawText();
                    case ETypeCode.Enum:
                        return jsonElement.GetInt32();
                    case ETypeCode.CharArray:
                        return jsonElement.GetString();
                    case ETypeCode.Object:
                        return jsonElement.GetRawText();
                    default:
                        throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
                }
            }
                         
            switch (typeCode)
            {
                case ETypeCode.Binary:
                    return inputValue is byte[] ? inputValue : Parse<byte[]>(inputValue);
                case ETypeCode.Geometry:
                    return inputValue is Geometry? inputValue : Parse<Geometry>(inputValue);
                case ETypeCode.Byte:
                    return inputValue is byte ? inputValue : Parse<byte>(inputValue);
                case ETypeCode.Char:
                    return inputValue is char ? inputValue : Parse<char>(inputValue);
                case ETypeCode.SByte:
                    return inputValue is sbyte ? inputValue : Parse<sbyte>(inputValue);
                case ETypeCode.UInt16:
                    return inputValue is ushort ? inputValue : Parse<ushort>(inputValue);
                case ETypeCode.UInt32:
                    return inputValue is uint ? inputValue : Parse<uint>(inputValue);
                case ETypeCode.UInt64:
                    return inputValue is ulong ? inputValue : Parse<ulong>(inputValue);
                case ETypeCode.Int16:
                    return inputValue is short ? inputValue : Parse<short>(inputValue);
                case ETypeCode.Int32:
                    return inputValue is int ? inputValue :  Parse<int>(inputValue);
                case ETypeCode.Int64:
                    return inputValue is long ? inputValue :  Parse<long>(inputValue);
                case ETypeCode.Decimal:
                    return inputValue is decimal ? inputValue : Parse<decimal>(inputValue);
                case ETypeCode.Double:
                    return inputValue is double ? inputValue : Parse<double>(inputValue);
                case ETypeCode.Single:
                    return inputValue is float ? inputValue : Parse<float>(inputValue);
                case ETypeCode.String:
                    return inputValue is string ? inputValue : Parse<string>(inputValue);
                case ETypeCode.Text:
                    return inputValue is string ? inputValue : Parse<string>(inputValue);
                case ETypeCode.Boolean:
                    return inputValue is bool ? inputValue : Parse<bool>(inputValue);
                case ETypeCode.DateTime:
                    return inputValue is DateTime ? inputValue : Parse<DateTime>(inputValue);
                case ETypeCode.Time:
                    return inputValue is TimeSpan ? inputValue : Parse<TimeSpan>(inputValue);
                case ETypeCode.Guid:
                    return inputValue is Guid ? inputValue : Parse<Guid>(inputValue);
                case ETypeCode.Unknown:
                    return inputValue is string ? inputValue : Parse<string>(inputValue);
                case ETypeCode.Json:
                case ETypeCode.Node:
                    return inputValue is JsonElement ? inputValue : Parse<JsonElement>(inputValue);
                case ETypeCode.Xml:
                    return inputValue is XmlDocument ? inputValue : Parse<XmlDocument>(inputValue);
                case ETypeCode.Enum:
                    return inputValue is Enum ? inputValue : Parse<int>(inputValue);
                case ETypeCode.CharArray:
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

        public static object Parse(ETypeCode tryDataType, int rank, object inputValue)
        {
            try
            {
                if (rank == 0)
                {
                    return Parse(tryDataType, inputValue);
                }

                var dataType = DataType.GetType(tryDataType);

                if (inputValue is JsonElement jsonElement)
                {
                    
                    if (rank == 1)
                    {
                        var returnValue = Array.CreateInstance(dataType, jsonElement.GetArrayLength());
                        for (var i = 0; i < jsonElement.GetArrayLength(); i++)
                        {
                            returnValue.SetValue(Parse(tryDataType, 0, jsonElement[i]), i);
                        }

                        return returnValue;
                    }
                    else if (rank == 2)
                    {
                        var array2 = jsonElement[0];
                        var returnValue = Array.CreateInstance(dataType, jsonElement.GetArrayLength(), array2.GetArrayLength());

                        for (var i = 0; i < jsonElement.GetArrayLength(); i++)
                        {
                            array2 = jsonElement[i];
                            for (var j = 0; j < array2.GetArrayLength(); j++)
                            {
                                returnValue.SetValue(Parse(tryDataType, 0, array2[j]), i, j);
                            }
                        }

                        return returnValue;
                    }
                }

                var type = inputValue.GetType();
                
                // matrix, 2 dim array handling
                if (rank == 2 && inputValue is Array inputArray && inputArray.Rank >= 2)
                {
                    var returnValue =
                        Array.CreateInstance(dataType, inputArray.GetLength(0), inputArray.GetLength(1));

                    for (var i = 0; i < inputArray.GetLength(0); i++)
                    {
                        for (var j = 0; j < inputArray.GetLength(1); j++)
                        {
                            returnValue.SetValue(Parse(tryDataType, 0, inputArray.GetValue(i, j)), i, j);
                        }
                    }
                    return returnValue;
                }

                if (inputValue is ICollection collection && type != typeof(byte[]) && type != typeof(char[]) && rank >= 1)
                {
                    if (rank == 1)
                    {
                        var returnValue = Array.CreateInstance(dataType, collection.Count);
                        var i = 0;
                        foreach (var item in collection)
                        {
                            returnValue.SetValue(Parse(tryDataType, 0, item), i);
                            i++;
                        }

                        return returnValue;
                    }

                    if (rank == 2)
                    {
                        var first = collection.OfType<ICollection>().FirstOrDefault();
                        
                        var returnValue = Array.CreateInstance(dataType, collection.Count, first.Count);
                        var i = 0;
                        foreach (var row in collection.OfType<ICollection>())
                        {
                            var j = 0;
                            foreach (var value in row)
                            {
                                returnValue.SetValue(Parse(tryDataType, 0, value), i, j);
                                j++;
                            }
                                
                            i++;
                        }

                        return returnValue;
                    }
                }

                if (type == typeof(string))
                {
                    var tryType = DataType.GetType(tryDataType);
                    
                    Type arrayType;
                    if (rank == 1)
                    {
                        arrayType = tryType.MakeArrayType();
                        return JsonSerializer.Deserialize((string) inputValue, arrayType, null);;
                    }
                    else if (rank == 2)
                    {
                        arrayType = tryType.MakeArrayType().MakeArrayType();
                        var jaggedArray = JsonSerializer.Deserialize((string) inputValue, arrayType, null);;
                        return ConvertToTwoDimArray((Array)jaggedArray, tryType);
                    }
                    else
                    {
                        throw new DataTypeException("Rank must be 1,2.");
                    }
                }

                return Parse(tryDataType, inputValue);
            }
            catch (Exception ex)
            {
                #if DEBUG
                throw new DataTypeException($"The value {inputValue} could not be parsed to type {tryDataType} rank {rank}", ex);
                #else 
                throw new DataTypeException($"The value could not be parsed to type {tryDataType} rank {rank}");
                #endif
            }
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

        public static bool Equal(ETypeCode typeCode, object value1, object value2)
        {
            if ((value1 == null || value1 == DBNull.Value) && (value2 == null || value2 == DBNull.Value))
                return true;

            if (value1 == null || value1 == DBNull.Value || value2 == null || value2 == DBNull.Value)
                return false;

            if (typeCode == ETypeCode.Binary || typeCode == ETypeCode.CharArray)
            {
                var array1 = (Array) value1;
                var array2 = (Array) value2;
                if (array1.Length != array2.Length) return false;
                var elementType = typeCode == ETypeCode.Binary ? typeof(byte) : typeof(char);

                for (var i = 0; i < array1.Length; i++)
                {
                    if (!Equal(elementType, array1.GetValue(i), array2.GetValue(i))) return false;
                }

                return true;
            }

            var value1Converted = Parse(typeCode, value1);
            var value2Converted = Parse(typeCode, value2);

            return Equals(value1Converted, value2Converted);

        }

        public static bool Evaluate(ECompare Operator, ETypeCode compareDataType, object value1, object value2)
        {
            var parsedValue1 = Parse(compareDataType, value1);
            
            if(Operator == ECompare.IsIn && value2.GetType().IsArray)
            {
                foreach(var value in (IEnumerable)value2)
                {
                    var parsedValue = Parse(compareDataType, value);
                    var compare = Equal(compareDataType, parsedValue1, parsedValue);
                    if(compare)
                    {
                        return true;
                    }
                }
                return false;
            }
            
            var parsedValue2 = Parse(compareDataType, value2);


            if (Operator == ECompare.IsNull)
            {
                return parsedValue1 == null || parsedValue1 is DBNull;
            }

            if (Operator == ECompare.IsNotNull)
            {
                return parsedValue1 != null && !(parsedValue1 is DBNull);
            }

            switch (Operator)
            {
                case ECompare.IsEqual:
                    return Equal(compareDataType, parsedValue1, parsedValue2);
                case ECompare.GreaterThan:
                    return GreaterThan(compareDataType, parsedValue1, parsedValue2);
                case ECompare.GreaterThanEqual:
                    return GreaterThanOrEqual(compareDataType, parsedValue1, parsedValue2);
                case ECompare.LessThan:
                    return LessThan(compareDataType, parsedValue1, parsedValue2);
                case ECompare.LessThanEqual:
                    return LessThanOrEqual(compareDataType, parsedValue1, parsedValue2);
                case ECompare.NotEqual:
                    return !Equal(compareDataType, parsedValue1, parsedValue2);
                case ECompare.Like:
                    return Like(parsedValue1?.ToString(), parsedValue2?.ToString());
                default:
                    throw new ArgumentException($"The {Operator} is not currently supported.");
            }
        }
        
           private static readonly char[] _regexSpecialChars = new char[12]
        {
            '.',
            '$',
            '^',
            '{',
            '[',
            '(',
            '|',
            ')',
            '*',
            '+',
            '?',
            '\\'
        };
        private static readonly string _defaultEscapeRegexCharsPattern = BuildEscapeRegexCharsPattern((IEnumerable<char>) _regexSpecialChars);
        private static readonly TimeSpan _regexTimeout = TimeSpan.FromMilliseconds(1000.0);
        
        private static string BuildEscapeRegexCharsPattern(IEnumerable<char> regexSpecialChars)
        {
            return string.Join("|", regexSpecialChars.Select<char, string>((Func<char, string>) (c => "\\" + c.ToString())));
        }
        
        public static bool Like(string matchExpression, string pattern, string escapeCharacter = null)
        {
            char? singleEscapeCharacter = string.IsNullOrEmpty(escapeCharacter)
                ? new char?()
                : new char?(escapeCharacter.First<char>());
            if (matchExpression == null || pattern == null)
                return false;
            if (matchExpression.Equals(pattern, StringComparison.OrdinalIgnoreCase))
                return true;
            if (matchExpression.Length == 0 || pattern.Length == 0)
                return false;
            string pattern1 = !singleEscapeCharacter.HasValue
                ? _defaultEscapeRegexCharsPattern
                : BuildEscapeRegexCharsPattern(
                    ((IEnumerable<char>) _regexSpecialChars).Where<char>((Func<char, bool>) (c =>
                    {
                        int num = (int) c;
                        char? nullable1 = singleEscapeCharacter;
                        int? nullable2 = nullable1.HasValue
                            ? new int?((int) nullable1.GetValueOrDefault())
                            : new int?();
                        int valueOrDefault = nullable2.GetValueOrDefault();
                        return !(num == valueOrDefault & nullable2.HasValue);
                    })));
            string str1 = Regex.Replace(pattern, pattern1, (MatchEvaluator) (c => "\\" + (object) c), RegexOptions.None);
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < str1.Length; ++index)
            {
                char ch = str1[index];
                char? nullable1;
                int? nullable2;
                int num1;
                if (index > 0)
                {
                    int num2 = (int) str1[index - 1];
                    nullable1 = singleEscapeCharacter;
                    nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                    int valueOrDefault = nullable2.GetValueOrDefault();
                    num1 = num2 == valueOrDefault & nullable2.HasValue ? 1 : 0;
                }
                else
                    num1 = 0;

                bool flag = num1 != 0;
                switch (ch)
                {
                    case '%':
                        stringBuilder.Append(flag ? "%" : ".*");
                        break;
                    case '_':
                        stringBuilder.Append(flag ? '_' : '.');
                        break;
                    default:
                        int num3 = (int) ch;
                        nullable1 = singleEscapeCharacter;
                        nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                        int valueOrDefault1 = nullable2.GetValueOrDefault();
                        if (!(num3 == valueOrDefault1 & nullable2.HasValue))
                        {
                            stringBuilder.Append(ch);
                            break;
                        }

                        break;
                }
            }

            string str2 = stringBuilder.ToString();
            return Regex.IsMatch(matchExpression, "\\A" + str2 + "\\s*\\z",
                RegexOptions.IgnoreCase | RegexOptions.Singleline);
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

        public static bool GreaterThan(ETypeCode typeCode,  object value1, object value2)
        {
            if ((value1 == null || value1 == DBNull.Value))
                return false;

            if (value2 == null || value2 == DBNull.Value)
                return true;

            value1 = Parse(typeCode, value1);
            value2 = Parse(typeCode, value2);

            switch (typeCode)
            {
                case ETypeCode.Unknown:
                case ETypeCode.Json:
                case ETypeCode.Xml:
                case ETypeCode.Node:
                    throw new Exception($"Cannot compare {typeCode} types.");
                case ETypeCode.Binary:
                    return ByteArrayIsGreater((byte[])value1, (byte[])value2,false);
                case ETypeCode.Geometry:
                    return ((Geometry)value1).CompareTo((Geometry)value2) > 0;
                case ETypeCode.CharArray:
                    return CharArrayIsGreater((char[])value1, (char[])value2,false);
                case ETypeCode.String:
                case ETypeCode.Text:
                    return string.Compare((string)value1, (string)value2) > 0;
                case ETypeCode.Guid:
                    return string.Compare(value1.ToString(), value2.ToString()) > 0;
                case ETypeCode.DateTime:
                    return (DateTime) value1 > (DateTime) value2;
                case ETypeCode.Boolean:
                    return BoolIsGreaterThan((bool) value1, (bool) value2);
                case ETypeCode.Time:
                    return (TimeSpan) value1 > (TimeSpan) value2;
                case ETypeCode.Byte:
                    return ((byte) value1 > (byte) value2);
                case ETypeCode.Char:
                    return ((char) value1 > (char) value2);
                case ETypeCode.SByte:
                    return ((sbyte) value1 > (sbyte) value2);
                case ETypeCode.UInt16:
                    return ((ushort) value1 > (ushort) value2);
                case ETypeCode.UInt32:
                    return (uint) value1 > (uint) value2;
                case ETypeCode.UInt64:
                    return (ulong) value1 > (ulong) value2;
                case ETypeCode.Int16:
                    return ((short) value1 > (short) value2);
                case ETypeCode.Int32:
                    return (int) value1 > (int) value2;
                case ETypeCode.Int64:
                    return (long) value1 > (long) value2;
                case ETypeCode.Decimal:
                    return (decimal) value1 > (decimal) value2;
                case ETypeCode.Double:
                    return (double) value1 > (double) value2;
                case ETypeCode.Single:
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

        public static bool GreaterThanOrEqual(ETypeCode typeCode,  object value1, object value2)
        {
            if ((value1 == null || value1 == DBNull.Value))
                return (value2 == null || value2 == DBNull.Value);

            if (value2 == null || value2 == DBNull.Value)
                return true;

            value1 = Parse(typeCode, value1);
            value2 = Parse(typeCode, value2);

            switch (typeCode)
            {

                case ETypeCode.Unknown:
                case ETypeCode.Json:
                case ETypeCode.Xml:
                case ETypeCode.Node:
                    throw new Exception($"Cannot compare {typeCode} types.");
                case ETypeCode.CharArray:
                    return CharArrayIsGreater((char[])value1, (char[])value2,true);
                case ETypeCode.Binary:
                    return ByteArrayIsGreater((byte[])value1, (byte[])value2,true);
                case ETypeCode.Geometry:
                    return ((Geometry)value1).CompareTo((Geometry)value2) >= 0;
                case ETypeCode.String:
                case ETypeCode.Text:
                    return string.Compare((string)value1, (string)value2) >= 0;
                case ETypeCode.Guid:
                    return string.Compare(value1.ToString(), value2.ToString()) >= 0;
                case ETypeCode.DateTime:
                    return (DateTime) value1 >= (DateTime) value2;
                case ETypeCode.Boolean:
                    return BoolIsGreaterThanOrEqual((bool) value1, (bool) value2);
                case ETypeCode.Time:
                    return (TimeSpan) value1 >= (TimeSpan) value2;
                case ETypeCode.Byte:
                    return ((byte) value1 >= (byte) value2);
                case ETypeCode.Char:
                    return ((char) value1 >= (char) value2);
                case ETypeCode.SByte:
                    return ((sbyte) value1 >= (sbyte) value2);
                case ETypeCode.UInt16:
                    return ((ushort) value1 >= (ushort) value2);
                case ETypeCode.UInt32:
                    return (uint) value1 >= (uint) value2;
                case ETypeCode.UInt64:
                    return (ulong) value1 >= (ulong) value2;
                case ETypeCode.Int16:
                    return ((short) value1 >= (short) value2);
                case ETypeCode.Int32:
                    return (int) value1 >= (int) value2;
                case ETypeCode.Int64:
                    return (long) value1 >= (long) value2;
                case ETypeCode.Decimal:
                    return (decimal) value1 >= (decimal) value2;
                case ETypeCode.Double:
                    return (double) value1 >= (double) value2;
                case ETypeCode.Single:
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

        public static bool LessThan(ETypeCode typeCode,  object value1, object value2)
        {
            if ((value2 == null || value2 == DBNull.Value))
                return false;

            if (value1 == null || value1 == DBNull.Value)
                return true;
            
            value1 = Parse(typeCode, value1);
            value2 = Parse(typeCode, value2);

            switch (typeCode)
            {
                case ETypeCode.Unknown:
                case ETypeCode.Json:
                case ETypeCode.Xml:
                case ETypeCode.Node:
                    throw new Exception($"Cannot compare {typeCode} types.");
                case ETypeCode.Geometry:
                    return ((Geometry)value1).CompareTo((Geometry)value2) < 0;
                case ETypeCode.Binary:
                    return ByteArrayIsLessThan((byte[])value1, (byte[])value2,false);
                case ETypeCode.CharArray:
                    return CharArrayIsLessThan((char[])value1, (char[])value2,false);
                case ETypeCode.String:
                case ETypeCode.Text:
                    return string.Compare((string)value1, (string)value2) < 0;
                case ETypeCode.Guid:
                    return string.Compare(value1.ToString(), value2.ToString()) < 0;
                case ETypeCode.DateTime:
                    return (DateTime) value1 < (DateTime) value2;
                case ETypeCode.Boolean:
                    return BoolIsLessThan((bool) value1, (bool) value2);
                case ETypeCode.Time:
                    return (TimeSpan) value1 < (TimeSpan) value2;
                case ETypeCode.Byte:
                    return ((byte) value1 < (byte) value2);
                case ETypeCode.Char:
                    return ((char) value1 < (char) value2);
                case ETypeCode.SByte:
                    return ((sbyte) value1 < (sbyte) value2);
                case ETypeCode.UInt16:
                    return ((ushort) value1 < (ushort) value2);
                case ETypeCode.UInt32:
                    return (uint) value1 < (uint) value2;
                case ETypeCode.UInt64:
                    return (ulong) value1 < (ulong) value2;
                case ETypeCode.Int16:
                    return ((short) value1 < (short) value2);
                case ETypeCode.Int32:
                    return (int) value1 < (int) value2;
                case ETypeCode.Int64:
                    return (long) value1 < (long) value2;
                case ETypeCode.Decimal:
                    return (decimal) value1 < (decimal) value2;
                case ETypeCode.Double:
                    return (double) value1 < (double) value2;
                case ETypeCode.Single:
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

        public static bool LessThanOrEqual(ETypeCode typeCode,  object value1, object value2)
        {
            if ((value2 == null || value2 == DBNull.Value))
                return (value1 == null || value1 == DBNull.Value);

            if (value1 == null || value1 == DBNull.Value)
                return true;
            
            value1 = Parse(typeCode, value1);
            value2 = Parse(typeCode, value2);

            switch (typeCode)
            {
                case ETypeCode.Unknown:
                case ETypeCode.Json:
                case ETypeCode.Xml:
                case ETypeCode.Node:
                    throw new Exception($"Cannot compare {typeCode} types.");
                case ETypeCode.Binary:
                    return ByteArrayIsLessThan((byte[])value1, (byte[])value2,true);
                case ETypeCode.Geometry:
                    return ((Geometry)value1).CompareTo((Geometry)value2) <= 0;
                case ETypeCode.CharArray:
                    return CharArrayIsLessThan((char[])value1, (char[])value2,true);
                case ETypeCode.String:
                case ETypeCode.Text:
                    return string.Compare((string)value1, (string)value2) <= 0;
                case ETypeCode.Guid:
                    return string.Compare(value1.ToString(), value2.ToString()) <= 0;
                case ETypeCode.DateTime:
                    return (DateTime) value1 <= (DateTime) value2;
                case ETypeCode.Boolean:
                    return BoolIsLessThanOrEqual((bool) value1, (bool) value2);
                case ETypeCode.Time:
                    return (TimeSpan) value1 <= (TimeSpan) value2;
                case ETypeCode.Byte:
                    return ((byte) value1 <= (byte) value2);
                case ETypeCode.Char:
                    return ((char) value1 <= (char) value2);
                case ETypeCode.SByte:
                    return ((sbyte) value1 <= (sbyte) value2);
                case ETypeCode.UInt16:
                    return ((ushort) value1 <= (ushort) value2);
                case ETypeCode.UInt32:
                    return (uint) value1 <= (uint) value2;
                case ETypeCode.UInt64:
                    return (ulong) value1 <= (ulong) value2;
                case ETypeCode.Int16:
                    return ((short) value1 <= (short) value2);
                case ETypeCode.Int32:
                    return (int) value1 <= (int) value2;
                case ETypeCode.Int64:
                    return (long) value1 <= (long) value2;
                case ETypeCode.Decimal:
                    return (decimal) value1 <= (decimal) value2;
                case ETypeCode.Double:
                    return (double) value1 <= (double) value2;
                case ETypeCode.Single:
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

        public static object Add(ETypeCode typeCode,  object value1, object value2)
        {
            value1 = Parse(typeCode, value1);
            value2 = Parse(typeCode, value2);

            switch (typeCode)
            {
                case ETypeCode.Binary:
                case ETypeCode.CharArray:
                case ETypeCode.String:
                case ETypeCode.Text:
                case ETypeCode.DateTime:
                case ETypeCode.Boolean:
                case ETypeCode.Unknown:
                case ETypeCode.Guid:
                case ETypeCode.Json:
                case ETypeCode.Xml:
                case ETypeCode.Node:
                case ETypeCode.Geometry:
                    throw new Exception($"Cannot add {typeCode} types.");
                case ETypeCode.Time:
                    return (TimeSpan) value1 + (TimeSpan) value2;
                case ETypeCode.Byte:
                    return (byte)((byte) value1 + (byte) value2);
                case ETypeCode.Char:
                    return (char)((char) value1 + (char) value2);
                case ETypeCode.SByte:
                    return (sbyte)((sbyte) value1 + (sbyte) value2);
                case ETypeCode.UInt16:
                    return (ushort)((ushort) value1 + (ushort) value2);
                case ETypeCode.UInt32:
                    return (uint) value1 + (uint) value2;
                case ETypeCode.UInt64:
                    return (ulong) value1 + (ulong) value2;
                case ETypeCode.Int16:
                    return (short)((short) value1 + (short) value2);
                case ETypeCode.Int32:
                    return (int) value1 + (int) value2;
                case ETypeCode.Int64:
                    return (long) value1 + (long) value2;
                case ETypeCode.Decimal:
                    return (decimal) value1 + (decimal) value2;
                case ETypeCode.Double:
                    return (double) value1 + (double) value2;
                case ETypeCode.Single:
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

        public static object Subtract(ETypeCode typeCode,  object value1, object value2)
        {
            value1 = Parse(typeCode, value1);
            value2 = Parse(typeCode, value2);

            switch (typeCode)
            {
                case ETypeCode.Binary:
                case ETypeCode.CharArray:
                case ETypeCode.String:
                case ETypeCode.Text:
                case ETypeCode.DateTime:
                case ETypeCode.Boolean:
                case ETypeCode.Unknown:
                case ETypeCode.Guid:
                case ETypeCode.Json:
                case ETypeCode.Xml:
                case ETypeCode.Node:
                case ETypeCode.Geometry:
                    throw new Exception($"Cannot subtract {typeCode} types.");
                case ETypeCode.Time:
                    return (TimeSpan) value1 - (TimeSpan) value2;
                case ETypeCode.Byte:
                    return (byte)((byte) value1 - (byte) value2);
                case ETypeCode.Char:
                    return (char)((char) value1 - (char) value2);
                case ETypeCode.SByte:
                    return (sbyte)((sbyte) value1 - (sbyte) value2);
                case ETypeCode.UInt16:
                    return (ushort)((ushort) value1 - (ushort) value2);
                case ETypeCode.UInt32:
                    return (uint) value1 - (uint) value2;
                case ETypeCode.UInt64:
                    return (ulong) value1 - (ulong) value2;
                case ETypeCode.Int16:
                    return (short)((short) value1 - (short) value2);
                case ETypeCode.Int32:
                    return (int) value1 - (int) value2;
                case ETypeCode.Int64:
                    return (long) value1 - (long) value2;
                case ETypeCode.Decimal:
                    return (decimal) value1 - (decimal) value2;
                case ETypeCode.Double:
                    return (double) value1 - (double) value2;
                case ETypeCode.Single:
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

        public static object Divide(ETypeCode typeCode,  object value1, object value2)
        {
            value1 = Parse(typeCode, value1);
            value2 = Parse(typeCode, value2);

            switch (typeCode)
            {
                case ETypeCode.Binary:
                case ETypeCode.CharArray:
                case ETypeCode.String:
                case ETypeCode.Text:
                case ETypeCode.DateTime:
                case ETypeCode.Boolean:
                case ETypeCode.Unknown:
                case ETypeCode.Time:
                case ETypeCode.Guid:
                case ETypeCode.Json:
                case ETypeCode.Xml:
                case ETypeCode.Node:
                case ETypeCode.Geometry:
                    throw new Exception($"Cannot divide {typeCode} types.");
                case ETypeCode.Byte:
                    return (byte)((byte) value1 / (byte) value2);
                case ETypeCode.Char:
                    return (char)((char) value1 / (char) value2);
                case ETypeCode.SByte:
                    return (sbyte)((sbyte) value1 / (sbyte) value2);
                case ETypeCode.UInt16:
                    return (ushort)((ushort) value1 / (ushort) value2);
                case ETypeCode.UInt32:
                    return (uint) value1 / (uint) value2;
                case ETypeCode.UInt64:
                    return (ulong) value1 / (ulong) value2;
                case ETypeCode.Int16:
                    return (short)((short) value1 / (short) value2);
                case ETypeCode.Int32:
                    return (int) value1 / (int) value2;
                case ETypeCode.Int64:
                    return (long) value1 / (long) value2;
                case ETypeCode.Decimal:
                    return (decimal) value1 / (decimal) value2;
                case ETypeCode.Double:
                    return (double) value1 / (double) value2;
                case ETypeCode.Single:
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

        public static object Multiply(ETypeCode typeCode,  object value1, object value2)
        {
            value1 = Parse(typeCode, value1);
            value2 = Parse(typeCode, value2);

            switch (typeCode)
            {
                case ETypeCode.Binary:
                case ETypeCode.CharArray:
                case ETypeCode.String:
                case ETypeCode.Text:
                case ETypeCode.DateTime:
                case ETypeCode.Boolean:
                case ETypeCode.Unknown:
                case ETypeCode.Guid:
                case ETypeCode.Json:
                case ETypeCode.Time:
                case ETypeCode.Xml:
                case ETypeCode.Node:
                case ETypeCode.Geometry:
                    throw new Exception($"Cannot multiply {typeCode} types.");
                case ETypeCode.Byte:
                    return (byte)((byte) value1 * (byte) value2);
                case ETypeCode.Char:
                    return (char)((char) value1 * (char) value2);
                case ETypeCode.SByte:
                    return (sbyte)((sbyte) value1 * (sbyte) value2);
                case ETypeCode.UInt16:
                    return (ushort)((ushort) value1 * (ushort) value2);
                case ETypeCode.UInt32:
                    return (uint) value1 * (uint) value2;
                case ETypeCode.UInt64:
                    return (ulong) value1 * (ulong) value2;
                case ETypeCode.Int16:
                    return (short)((short) value1 * (short) value2);
                case ETypeCode.Int32:
                    return (int) value1 * (int) value2;
                case ETypeCode.Int64:
                    return (long) value1 * (long) value2;
                case ETypeCode.Decimal:
                    return (decimal) value1 * (decimal) value2;
                case ETypeCode.Double:
                    return (double) value1 * (double) value2;
                case ETypeCode.Single:
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

        public static object DivideInt(ETypeCode typeCode,  object value1, int value2)
        {
            value1 = Parse(typeCode, value1);

            switch (typeCode)
            {
                case ETypeCode.Binary:
                case ETypeCode.CharArray:
                case ETypeCode.String:
                case ETypeCode.Text:
                case ETypeCode.DateTime:
                case ETypeCode.Boolean:
                case ETypeCode.Unknown:
                case ETypeCode.Guid:
                case ETypeCode.Json:
                case ETypeCode.Time:
                case ETypeCode.Xml:
                case ETypeCode.Node:
                case ETypeCode.Geometry:
                    throw new Exception($"Cannot divide {typeCode} types.");
                case ETypeCode.Byte:
                    return (byte)((byte) value1 / value2);
                case ETypeCode.Char:
                    return (char)((char) value1 / value2);
                case ETypeCode.SByte:
                    return (sbyte)((sbyte) value1 / value2);
                case ETypeCode.UInt16:
                    return (ushort)((ushort) value1 / value2);
                case ETypeCode.UInt32:
                    return (uint) value1 / value2;
                case ETypeCode.UInt64:
                    return (ulong) value1 / Convert.ToUInt64(value2);
                case ETypeCode.Int16:
                    return (short)((short) value1 / value2);
                case ETypeCode.Int32:
                    return (int) value1 / value2;
                case ETypeCode.Int64:
                    return (long) value1 / value2;
                case ETypeCode.Decimal:
                    return (decimal) value1 / value2;
                case ETypeCode.Double:
                    return (double) value1 / value2;
                case ETypeCode.Single:
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

        public static object Negate(ETypeCode typeCode,  object value1)
        {
            value1 = Parse(typeCode, value1);

            switch (typeCode)
            {
                case ETypeCode.Binary:
                case ETypeCode.CharArray:
                case ETypeCode.String:
                case ETypeCode.Text:
                case ETypeCode.DateTime:
                case ETypeCode.Boolean:
                case ETypeCode.Unknown:
                case ETypeCode.Guid:
                case ETypeCode.Json:
                case ETypeCode.Time:
                case ETypeCode.Xml:
                case ETypeCode.UInt16:
                case ETypeCode.UInt32:
                case ETypeCode.UInt64:
                case ETypeCode.Node:
                case ETypeCode.Geometry:
                    throw new Exception($"Cannot negate {typeCode} types.");
                case ETypeCode.Byte:
                    return (byte)((byte) value1 * -1);
                case ETypeCode.Char:
                    return (char)((char) value1 * -1);
                case ETypeCode.SByte:
                    return (sbyte)((sbyte) value1 * -1);
                case ETypeCode.Int16:
                    return (short)((short) value1 * -1);
                case ETypeCode.Int32:
                    return (int) value1 * -1;
                case ETypeCode.Int64:
                    return (long) value1 * -1;
                case ETypeCode.Decimal:
                    return (decimal) value1 * -1;
                case ETypeCode.Double:
                    return (double) value1 * -1;
                case ETypeCode.Single:
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

        public static object Increment(ETypeCode typeCode,  object value1)
        {
            value1 = Parse(typeCode, value1);

            switch (typeCode)
            {
                case ETypeCode.Binary:
                case ETypeCode.CharArray:
                case ETypeCode.String:
                case ETypeCode.Text:
                case ETypeCode.DateTime:
                case ETypeCode.Boolean:
                case ETypeCode.Unknown:
                case ETypeCode.Guid:
                case ETypeCode.Json:
                case ETypeCode.Time:
                case ETypeCode.Xml:
                case ETypeCode.Node:
                case ETypeCode.Geometry:
                    throw new Exception($"Cannot increment {typeCode} types.");
                case ETypeCode.Byte:
                    return (byte)((byte) value1 + 1);
                case ETypeCode.Char:
                    return (char)((char) value1 + 1);
                case ETypeCode.SByte:
                    return (sbyte)((sbyte) value1 + 1);
                case ETypeCode.Int16:
                    return (short)((short) value1 + 1);
                case ETypeCode.Int32:
                    return (int) value1 + 1;
                case ETypeCode.Int64:
                    return (long) value1 + 1;
                case ETypeCode.UInt16:
                    return (ushort) value1 + 1;
                case ETypeCode.UInt32:
                    return (uint) value1 + 1;
                case ETypeCode.UInt64:
                    return (ulong) value1 + 1;
                case ETypeCode.Decimal:
                    return (decimal) value1 + 1;
                case ETypeCode.Double:
                    return (double) value1 + 1;
                case ETypeCode.Single:
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

        public static object Decrement(ETypeCode typeCode,  object value1)
        {
            value1 = Parse(typeCode, value1);

            switch (typeCode)
            {
                case ETypeCode.Binary:
                case ETypeCode.CharArray:
                case ETypeCode.String:
                case ETypeCode.Text:
                case ETypeCode.DateTime:
                case ETypeCode.Boolean:
                case ETypeCode.Unknown:
                case ETypeCode.Guid:
                case ETypeCode.Json:
                case ETypeCode.Time:
                case ETypeCode.Xml:
                case ETypeCode.Node:
                case ETypeCode.Geometry:
                    throw new Exception($"Cannot decrement {typeCode} types.");
                case ETypeCode.Byte:
                    return (byte)((byte) value1 - 1);
                case ETypeCode.Char:
                    return (char)((char) value1 - 1);
                case ETypeCode.SByte:
                    return (sbyte)((sbyte) value1 - 1);
                case ETypeCode.Int16:
                    return (short)((short) value1 - 1);
                case ETypeCode.Int32:
                    return (int) value1 - 1;
                case ETypeCode.Int64:
                    return (long) value1 - 1;
                case ETypeCode.UInt16:
                    return (ushort) value1 - 1;
                case ETypeCode.UInt32:
                    return (uint) value1 - 1;
                case ETypeCode.UInt64:
                    return (ulong) value1 - 1;
                case ETypeCode.Decimal:
                    return (decimal) value1 - 1;
                case ETypeCode.Double:
                    return (double) value1 - 1;
                case ETypeCode.Single:
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

        public static int Compare(ETypeCode typeCode, object inputValue, object compareTo)
        {
            if ((inputValue == null || inputValue == DBNull.Value) && (compareTo == null || compareTo == DBNull.Value))
                return 0;

            if (inputValue == null || inputValue == DBNull.Value || compareTo == null || compareTo == DBNull.Value)
                return (inputValue == null || inputValue is DBNull) ? -1 : 1;
            
            inputValue = Parse(typeCode, inputValue);
            compareTo = Parse(typeCode, compareTo);

            switch (typeCode)
            {
                case ETypeCode.Binary:
                    return ByteArrayCompareTo((byte[])inputValue,(byte[])compareTo);
                case ETypeCode.Geometry:
                    return ((Geometry)inputValue).CompareTo((Geometry)compareTo);
                case ETypeCode.Byte:
                    return ((byte)inputValue).CompareTo((byte)compareTo);
                case ETypeCode.Char:
                    return ((char)inputValue).CompareTo((char)compareTo);
                case ETypeCode.SByte:
                    return ((sbyte)inputValue).CompareTo((sbyte)compareTo);
                case ETypeCode.UInt16:
                    return ((ushort)inputValue).CompareTo((ushort)compareTo);
                case ETypeCode.UInt32:
                    return ((uint)inputValue).CompareTo((uint)compareTo);
                case ETypeCode.UInt64:
                    return ((ulong)inputValue).CompareTo((ulong)compareTo);
                case ETypeCode.Int16:
                    return ((short)inputValue).CompareTo((short)compareTo);
                case ETypeCode.Int32:
                    return ((int)inputValue).CompareTo((int)compareTo);
                case ETypeCode.Int64:
                    return ((long)inputValue).CompareTo((long)compareTo);
                case ETypeCode.Decimal:
                    return ((decimal)inputValue).CompareTo((decimal)compareTo);
                case ETypeCode.Double:
                    return ((double)inputValue).CompareTo((double)compareTo);
                case ETypeCode.Single:
                    return ((float)inputValue).CompareTo((float)compareTo);
                case ETypeCode.String:
                case ETypeCode.Text:
                    return string.Compare((string) inputValue, (string) compareTo);
                case ETypeCode.Boolean:
                    return ((bool)inputValue).CompareTo((bool)compareTo);
                case ETypeCode.DateTime:
                    return ((DateTime)inputValue).CompareTo((DateTime)compareTo);
                case ETypeCode.Time:
                    return ((TimeSpan)inputValue).CompareTo((TimeSpan)compareTo);
                case ETypeCode.Guid:
                    return ((Guid)inputValue).CompareTo((Guid)compareTo);
                case ETypeCode.Unknown:
                    return string.Compare((inputValue.ToString()), compareTo.ToString(), StringComparison.Ordinal);
                case ETypeCode.Json:
                case ETypeCode.Node:
                    return string.Compare(inputValue.ToString(), compareTo.ToString());
                case ETypeCode.Xml:
                    return string.Compare(((XmlDocument)inputValue).InnerXml, ((XmlDocument)compareTo).InnerXml);
                case ETypeCode.Enum:
                    return ((int)inputValue).CompareTo((int)compareTo);
                case ETypeCode.CharArray:
                    return CharArrayCompareTo((char[])inputValue,(char[])compareTo);
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }
        
        /// <summary>
        /// More flexible version of bool.TryParse, will accept values on/off/true/false/yes/no.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryParseBoolean(object value, out bool result)
        {
            switch (value)
            {
                case bool boolValue:
                    result = boolValue;
                    return true;
                case string stringValue:
                    var isParsed = bool.TryParse(stringValue, out var parsedResult);
                    if (isParsed)
                    {
                        result = parsedResult;
                        return true;
                    }

                    isParsed = int.TryParse(stringValue, out var numberResult);
                    if (isParsed)
                    {
                        result = (numberResult != 0);
                        return numberResult == -1 || numberResult == 0 || numberResult == 1;
                    }

                    switch (stringValue.ToLower())
                    {
                        case "yes":
                        case "y":
                        case "on":
                            result = true;
                            return true;
                        case "no":
                        case "n":
                        case "off":
                            result = false;
                            return true;
                    }

                    result = false;
                    return false;
                case char charValue:
                    switch (charValue)
                    {
                        case 'y':
                            result = true;
                            return true;
                        case 'n':
                            result = false;
                            return true;
                    }

                    result = false;
                    return false;
                case Single singleValue:
                    result = (singleValue != 0);
                    return singleValue == 0 || singleValue == -1 || singleValue == 1;
                case Double doubleValue:
                    result = (doubleValue != 0);
                    return doubleValue == 0 || doubleValue == -1 || doubleValue == 1;
                case long longValue:
                    result = (longValue != 0);
                    return longValue == 0 || longValue == -1 || longValue == 1;
                case int intValue:
                    result = (intValue != 0);
                    return intValue == 0 || intValue == -1 || intValue == 1;
                case short shortValue:
                    result = (shortValue != 0);
                    return shortValue == 0 || shortValue == -1 || shortValue == 1;
                case ulong ulongValue:
                    result = (ulongValue != 0);
                    return ulongValue == 0 || ulongValue == 1;
                case uint uintValue:
                    result = (uintValue != 0);
                    return uintValue == 0 || uintValue == 1;
                case ushort ushortValue:
                    result = (ushortValue != 0);
                    return ushortValue == 0 || ushortValue == 1;
            }
            
            result = false;
            return false;
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
                a1 = Parse(bType, a);
                b1 = b;
                return (bType);
            }
            else
            {
                a1 = a;
                b1 = Parse(aType, b);
                return (aType);
            }
        }
        
        public static object[] ConvertToJaggedArray(Array array)
        {
            int rowsFirstIndex = array.GetLowerBound(0);
            int rowsLastIndex = array.GetUpperBound(0);
            int numberOfRows = rowsLastIndex + 1;

            int columnsFirstIndex = array.GetLowerBound(1);
            int columnsLastIndex = array.GetUpperBound(1);
            int numberOfColumns = columnsLastIndex + 1;

            var jaggedArray = new object[numberOfRows][];
            for (int i = rowsFirstIndex; i <= rowsLastIndex; i++)
            {
                jaggedArray[i] = new object[numberOfColumns];

                for (var j = columnsFirstIndex; j <= columnsLastIndex; j++)
                {
                    jaggedArray[i][j] = array.GetValue(i, j);
                }
            }
            return jaggedArray;
        }

        public static object ConvertToTwoDimArray(Array array, Type elementType)
        {
            int rowsFirstIndex = array.GetLowerBound(0);
            int rowsLastIndex = array.GetUpperBound(0);
            int numberOfRows = rowsLastIndex + 1;

            var firstRow = (Array) array.GetValue(0);
            int columnsFirstIndex = firstRow.GetLowerBound(0);
            int columnsLastIndex = firstRow.GetUpperBound(0);
            int numberOfColumns = columnsLastIndex + 1;
           
            var twoDimArray = Array.CreateInstance(elementType, numberOfRows, numberOfColumns);
            for (int i = rowsFirstIndex; i <= rowsLastIndex; i++)
            {
                var row = (Array) array.GetValue(i);

                for (int j = columnsFirstIndex; j <= columnsLastIndex; j++)
                {
                    twoDimArray.SetValue(row.GetValue(j), i, j);
                }
            }
            return twoDimArray;
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
            if (type.IsEnum) return false;
            
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
            if (type.IsEnum) return false;
            
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
            else if (type == typeof(byte[]))
            {
                int Compare(T a, T b)
                {
                    return Operations.ByteArrayCompareTo( (byte[])(object) a, (byte[])(object) b);
                }
                return new Lazy<Func<T, T, int>>(() => Compare);
            }           
            else if(type == typeof(object))
            {
                int Compare(T a, T b)
                {
                    if (a.GetType() == b.GetType())
                    {
                        if (a is IComparable comparable)
                        {
                            return comparable.CompareTo(b);
                        }

                        if (a is byte[] aByte && b is byte[] bByte)
                        {
                            int compare = 0;
                            for (var i = 0; i < aByte.Length; i++)
                            {
                                compare = aByte[i].CompareTo(bByte[i]);
                                if (compare != 0) return compare;
                            }

                            return compare;
                        }
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
            else if (type == typeof(char[]))
            {
                bool Condition(T a, T b)
                {
                    return compareResults.Contains(Operations.CharArrayCompareTo((char[]) (object) a, (char[]) (object) b));
                }
                return new Lazy<Func<T, T, bool>>(() => Condition);
            }
            else if (type == typeof(byte[]))
            {
                bool Condition(T a, T b)
                {
                    return compareResults.Contains(Operations.ByteArrayCompareTo((byte[]) (object) a, (byte[]) (object) b));
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
            var type = typeof(T);
            if (type.IsEnum)
            {
                return new Lazy<Func<T, T>>(() => throw new InvalidCastException($"The data type {typeof(T)} is not supported for negate."));
            }
            
            switch (Type.GetTypeCode(type))
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
                    else if (dataType == typeof(JsonElement)) exp = value => value.ToString();
                    else if (dataType == typeof(XmlDocument)) exp = value => (value as XmlDocument)?.InnerXml;
                    else if (dataType == typeof(object)) exp = value => value.ToString();
                    else if (dataType == typeof(Geometry)) exp = value => (value as Geometry).AsText();
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
                var isParsed = Operations.TryParseBoolean(value, out var result);

                if (!isParsed)
                {
                    throw new FormatException("Value was not recognized as a valid boolean");
                }

                return (T) (object) result;
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

                if(value is Geometry geometry)
                {
                    return (T) (object) geometry.ToBinary();
                }

                if (value is string stringValue)
                {
                    return (T) (object) HexToByteArray(stringValue);
                }
                throw new DataTypeParseException("Binary type conversion only supported for hex strings.");
            };
        }

        private static Func<object, T> ConvertGeometry()
        {
            return value =>
            {
                if (value is byte[] byteArray)
                {
                    var binReader = new WKBReader();
                    return (T)(object)binReader.Read(byteArray);
                }

                if (value is Geometry geometry)
                {
                    return (T)(object)geometry;
                }

                if (value is string stringValue)
                {
                    var textReader = new WKTReader();
                    return (T)(object)textReader.Read(stringValue);
                }

                throw new DataTypeParseException("Binary type conversion only supported for hex strings.");
            };
        }

        private static Func<object, T> ConvertToTimeSpan()
        {
            return value =>
            {
                switch (value)
                {
                    case TimeSpan timeSpan:
                        return (T) (object) timeSpan;
                    case string stringValue:
                        return (T) (object) TimeSpan.Parse(stringValue);
                    case long longValue:
                        return (T) (object) TimeSpan.FromTicks(longValue);
                    case int intValue:
                        return (T) (object) TimeSpan.FromTicks(intValue);
                    case short ushortValue:
                        return (T) (object) TimeSpan.FromTicks(ushortValue);
                    case ulong ulongValue:
                        return (T) (object) TimeSpan.FromTicks((long)ulongValue);
                    case uint uintValue:
                        return (T) (object) TimeSpan.FromTicks(uintValue);
                    case ushort ushortValue:
                        return (T) (object) TimeSpan.FromTicks(ushortValue);
                }


                throw new DataTypeParseException("TimeSpan conversion is only supported for strings or longs.");
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

        private static Func<object, T> ConvertToJsonElement()
        {
            return value =>
            {
                if (value is JsonDocument jsonDocument)
                {
                    return (T) (object) jsonDocument.RootElement;
                }

                if (value is JsonElement jsonElement)
                {
                    return (T) (object) jsonElement;
                }

                if (value is string stringValue)
                {
                    return (T) (object) JsonDocument.Parse(stringValue).RootElement;
                }

                var json = JsonSerializer.Serialize(value);
                return (T) (object) JsonDocument.Parse(json).RootElement;
//                throw new DataTypeParseException("Json conversion is only supported for strings.");
            };
        }

        private static Func<object, T> ConvertToJsonDocument()
        {
            return value => 
            {
                if (value is JsonDocument jsonDocument)
                {
                    return (T) (object) jsonDocument;
                }

                if (value is string stringValue)
                {
                    return (T) (object) JsonDocument.Parse(stringValue);
                }
                var json = JsonSerializer.Serialize(value);
                return (T) (object) JsonDocument.Parse(json);
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

        private static Func<object, T> ConvertToTwoDimArray()
        {
            return value =>
            {
                if (value is Array array && array.Rank == 2)
                {
                    return (T) (object) array;
                }

                if (value is string stringValue)
                {
                    var elementType = typeof(T).GetElementType();
                    var arrayType = elementType.MakeArrayType().MakeArrayType();
                    var jaggedArray = JsonSerializer.Deserialize(stringValue, arrayType);
                    var returnValue = Operations.ConvertToTwoDimArray((Array)jaggedArray, elementType);
                    return (T) (object) returnValue;
                }
                
                throw new DataTypeParseException("Two Dim Array conversion is only supported for json types.");
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
                else if (value is Memory<char> memory)
                {
                    return (T) (object) memory.ToString();
                }
                else if (value is ReadOnlyMemory<char> memory2)
                {
                    return (T) (object) memory2.ToString();
                }
                else if (value is XmlDocument xmlDocument)
                {
                    return (T) (object) xmlDocument.InnerXml;
                }
                else if (value is JsonElement jValue)
                {
                    return (T) (object) jValue.GetRawText();
                }
                else if (value is Geometry geometry)
                {
                    return (T)(object) geometry.ToText();
                }
                else
                {
                    var type = value.GetType();
                    if (type.IsArray)
                    {
                        var rank = type.GetArrayRank();
                        if (rank == 1)
                        {
                            return (T) (object) JsonSerializer.Serialize(value);
                        }

                        if (rank == 2)
                        {
                            var jagged = Operations.ConvertToJaggedArray((Array) value);
                            return (T) (object) JsonSerializer.Serialize(jagged);
                        }

                        if (rank > 2)
                        {
                            throw new ParseException("Arrays with a rank greater than two are not supported.");
                        }
                    } else if (DataType.IsSimple(type))
                    {
                        return (T) (object) value.ToString();
                    }
                    else
                    {
                        return (T) (object) (T) (object) JsonSerializer.Serialize(value);
                    }
                }
                
                // shouldn't get here.
                throw new ArgumentOutOfRangeException();

            };
        }

   
        
        private static Lazy<Func<object, T>> CreateParse()
        {
            Func<object, T> exp;

            var dataType = typeof(T);
            switch (Type.GetTypeCode(dataType))
            {
                case TypeCode.Double:
                    exp = value => (T) (object) Convert.ToDouble(value);
                    break;
                case TypeCode.Decimal:
                    exp = value => (T) (object) Convert.ToDecimal(value);
                    break;
                case TypeCode.Byte:
                    exp = value => (T) (object) Convert.ToByte(value);
                    break;
                case TypeCode.Char:
                    exp = value => (T) (object) Convert.ToChar(value);
                    break;
                case TypeCode.Int16:
                    exp = value => (T) (object) Convert.ToInt16(value);
                    break;
                case TypeCode.Int32:
                    exp = value => (T) (object) Convert.ToInt32(value);
                    break;
                case TypeCode.Int64:
                    exp = value => (T) (object) Convert.ToInt64(value);
                    break;
                case TypeCode.SByte:
                    exp = value => (T) (object) Convert.ToSByte(value);
                    break;
                case TypeCode.Single:
                    exp = value => (T) (object) Convert.ToSingle(value);
                    break;
                case TypeCode.UInt16:
                    exp = value => (T) (object) Convert.ToUInt16(value);
                    break;
                case TypeCode.UInt32:
                    exp = value => (T) (object) Convert.ToUInt32(value);
                    break;
                case TypeCode.UInt64:
                    exp = value => (T) (object) Convert.ToUInt64(value);
                    break;
                case TypeCode.DateTime:
                    exp = value => (T) (object) Convert.ToDateTime(value);
                    break;
                case TypeCode.DBNull:
                    exp = value => (T) (object) DBNull.Value;
                    break;
                case TypeCode.String:
                    exp = ConvertToString();
                    break;
                case TypeCode.Boolean:
                    exp = ConvertToBoolean();
                    break;
                case TypeCode.Object:
                    if (dataType == typeof(TimeSpan) || dataType == typeof(TimeSpan?)) exp = ConvertToTimeSpan();
                    else if (dataType.IsEnum) exp = value => (T) (object) Convert.ToInt32(value);
                    else if (dataType == typeof(Guid) || dataType == typeof(Guid?)) exp = ConvertToGuid();
                    else if (dataType == typeof(byte[])) exp = ConvertToByteArray();
                    else if (dataType == typeof(char[])) exp = ConvertToCharArray();
                    else if (dataType == typeof(JsonElement)) exp = ConvertToJsonElement();
                    else if (dataType == typeof(JsonDocument)) exp = ConvertToJsonDocument();
                    else if (dataType == typeof(XmlDocument)) exp = ConvertToXml();
                    else if (dataType == typeof(Geometry)) exp = ConvertGeometry();
                    else if (dataType.IsArray && dataType.GetArrayRank() == 2 ) exp = ConvertToTwoDimArray();
                    else
                        exp = value =>
                            throw new NotSupportedException($"The datatype {dataType} is not supported for Parse.");
                    break;
                default:
                    exp = value =>
                        throw new NotSupportedException($"The datatype {dataType} is not supported for Parse.");
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