using System;
using System.Data;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Dexih.Utils.DataType
{
    


    /// <summary>
    /// The data type library includes modified versions of the c# datatype functions, plus simple methods to parse and compare datatypes regardless of their base type.
    /// </summary>
    public static class DataType
    {
        private static readonly Type[] SimpleTypes = {typeof(string), typeof(decimal), typeof(DateTime), typeof(TimeSpan), typeof(Guid), typeof(byte[]), typeof(char[])};
        private static readonly Type Nullable = typeof(Nullable<>);

        /// <summary>
        /// Is a simple type that can be mapped to db.  Includes generic types and decimal, datetime, timespan, guid, byte[] and char[]
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSimple(Type type)
        {
            // type = Nullable.GetUnderlyingType(type) ?? type;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == Nullable)
            {
                type = type.GetGenericArguments()[0];
            }

            return type.IsPrimitive || type.IsEnum || SimpleTypes.Contains(type);
        }

        /// <summary>
        /// Checks if array type.  This excludes byte[] and char[] arrays.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsArray(Type type)
        {
            if (!type.IsArray) return false;

            if (type == typeof(byte[]) || type == typeof(char[]))
                return false;

            return true;
        }

        /// <summary>
        /// A simplified list of primary possible data types.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum EBasicType : byte
        {
            Unknown,
            String,
            Numeric,
            Date,
            Time,
            Boolean,
            Binary,
            Enum
        }

        /// <summary>
        /// List of supported type codes.  This is a cut down version of <see cref="TypeCode"/> enum.
        /// <para/> Note: Time, Binary & Unknown differ from the TypeCode.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ETypeCode : byte
        {
            Binary,
            Byte,
            SByte,
            UInt16,
            UInt32,
            UInt64,
            Int16,
            Int32,
            Int64,
            Decimal,
            Double,
            Single,
            String,
            Text,
            Boolean,
            DateTime,
            Time,
            Guid,
            Unknown,
            Json,
            Xml,
            Enum,
            Char
        }

        /// <summary>
        /// Maximum value for a <see cref="ETypeCode"/>.  
        /// </summary>
        /// <param name="typeCode"></param>
        /// <param name="length">For string values only, specify the length of the string.</param>
        /// <returns></returns>
        public static object GetDataTypeMaxValue(ETypeCode typeCode, int length = 0)
        {
            switch (typeCode)
            {
                case ETypeCode.Byte:
                    return byte.MaxValue;
                case ETypeCode.SByte:
                    return sbyte.MaxValue;
                case ETypeCode.UInt16:
                    return ushort.MaxValue;
                case ETypeCode.UInt32:
                    return uint.MaxValue;
                case ETypeCode.UInt64:
                    return ulong.MaxValue;
                case ETypeCode.Int16:
                    return short.MaxValue;
                case ETypeCode.Int32:
                    return int.MaxValue;
                case ETypeCode.Int64:
                    return long.MaxValue;
                case ETypeCode.Decimal:
                    return (decimal)9999999999999999999;
                case ETypeCode.Double:
                    return double.MaxValue;
                case ETypeCode.Single:
                    return float.MaxValue;
                case ETypeCode.Char:
                    return new string(char.MaxValue, length).ToCharArray();
                case ETypeCode.String:
                case ETypeCode.Json:
                case ETypeCode.Xml:
                case ETypeCode.Text:
                    return new string('Z', length);
                case ETypeCode.Boolean:
                    return true;
                case ETypeCode.DateTime:
                    return DateTime.MaxValue;
                case ETypeCode.Time:
                    return TimeSpan.FromDays(1) - TimeSpan.FromMilliseconds(1);
                case ETypeCode.Guid:
                    return Guid.Parse("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF");
                case ETypeCode.Binary:
                    return new[] { byte.MaxValue, byte.MaxValue, byte.MaxValue };
                case ETypeCode.Unknown:
                    return "";
                default:
                    return typeof(object);
            }
        }

        /// <summary>
        /// Minimum value for a <see cref="ETypeCode"/>.
        /// </summary>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        public static object GetDataTypeMinValue(ETypeCode typeCode)
        {
            switch (typeCode)
            {
                case ETypeCode.Byte:
                    return byte.MinValue;
                case ETypeCode.SByte:
                    return sbyte.MinValue;
                case ETypeCode.UInt16:
                    return ushort.MinValue;
                case ETypeCode.UInt32:
                    return uint.MinValue;
                case ETypeCode.UInt64:
                    return ulong.MinValue;
                case ETypeCode.Int16:
                    return short.MinValue;
                case ETypeCode.Int32:
                    return int.MinValue;
                case ETypeCode.Int64:
                    return long.MinValue;
                case ETypeCode.Decimal:
                    return (decimal)-999999999999999999;
                case ETypeCode.Double:
                    return double.MinValue;
                case ETypeCode.Single:
                    return float.MinValue;
                case ETypeCode.Char:
                    return new[] {char.MinValue};
                case ETypeCode.String:
                case ETypeCode.Xml:
                case ETypeCode.Json:
                case ETypeCode.Text:
                    return "";
                case ETypeCode.Boolean:
                    return false;
                case ETypeCode.DateTime:
                    return new DateTime(0001, 01, 01); // new DateTime(1753,01,01);
                case ETypeCode.Time:
                    return TimeSpan.FromDays(0);
                case ETypeCode.Guid:
                    return Guid.Parse("00000000-0000-0000-0000-000000000000");
                case ETypeCode.Binary:
                    return new[] { byte.MinValue, byte.MinValue, byte.MinValue };
                case ETypeCode.Unknown:
                    return "";
                default:
                    return typeof(object);
            }
        }


        /// <summary>
        /// Converts a <see cref="ETypeCode"/> to a simplified <see cref="EBasicType"/>.
        /// </summary>
        /// <param name="dataType">Data Type</param>
        /// <returns>Basic Datatype</returns>
        public static EBasicType GetBasicType(ETypeCode dataType)
        {
            switch (dataType)
            {
                case ETypeCode.Byte:
                case ETypeCode.SByte:
                case ETypeCode.UInt16:
                case ETypeCode.UInt32:
                case ETypeCode.UInt64:
                case ETypeCode.Int16:
                case ETypeCode.Int32:
                case ETypeCode.Int64:
                case ETypeCode.Decimal:
                case ETypeCode.Double:
                case ETypeCode.Single: return EBasicType.Numeric;
                case ETypeCode.Guid:
                case ETypeCode.Char:
                case ETypeCode.Json:
                case ETypeCode.Xml:
                case ETypeCode.Text:
                case ETypeCode.String: return EBasicType.String;
                case ETypeCode.Boolean: return EBasicType.Boolean;
                case ETypeCode.DateTime: return EBasicType.Date;
                case ETypeCode.Time: return EBasicType.Time;
                case ETypeCode.Binary: return EBasicType.Binary;
                case ETypeCode.Enum: return EBasicType.Enum;
                default: return EBasicType.Unknown;
            }
        }

        /// <summary>
        /// Converts a <see cref="Type"/> into a <see cref="ETypeCode"/>
        /// </summary>
        /// <param name="dataType">Type value</param>
        /// <param name="rank">The number of array dimensions (0 = not array)</param>
        /// <returns>ETypeCode</returns>
        public static ETypeCode GetTypeCode(Type dataType, out int rank)
        {
            rank = 0;
            
            while (true)
            {
                if (dataType.IsArray)
                {
                    if (dataType == typeof(byte[]))
                    {
                        return ETypeCode.Binary;
                    }

                    if (dataType == typeof(char[]))
                    {
                        return ETypeCode.Char;
                    }
                    
                    rank = dataType.GetArrayRank();
                    dataType = dataType.GetElementType();
                }
                    
                if (dataType.IsEnum) return ETypeCode.Enum;

                if (dataType.IsGenericType && dataType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // if nullable type, then get the generic type and loop again.
                    dataType = dataType.GetGenericArguments()[0];
                    continue;
                }

                switch (Type.GetTypeCode(dataType))
                {
                    case TypeCode.Boolean:
                        return ETypeCode.Boolean;
                    case TypeCode.Byte:
                        return ETypeCode.Byte;
                    case TypeCode.Char:
                        return ETypeCode.Unknown;
                    case TypeCode.DateTime:
                        return ETypeCode.DateTime;
                    case TypeCode.DBNull:
                        return ETypeCode.String;
                    case TypeCode.Decimal:
                        return ETypeCode.Decimal;
                    case TypeCode.Double:
                        return ETypeCode.Double;
                    case TypeCode.Empty:
                        return ETypeCode.String;
                    case TypeCode.Int16:
                        return ETypeCode.Int16;
                    case TypeCode.Int32:
                        return ETypeCode.Int32;
                    case TypeCode.Int64:
                        return ETypeCode.Int64;
                    case TypeCode.Object:
                        if (dataType == typeof(TimeSpan) || dataType == typeof(TimeSpan?)) return ETypeCode.Time;
                        if (dataType == typeof(Guid) || dataType == typeof(Guid?)) return ETypeCode.Guid;
                        if (dataType == typeof(byte[])) return ETypeCode.Binary;
                        if (dataType == typeof(char[])) return ETypeCode.Char;
                        return ETypeCode.Unknown;
                    case TypeCode.SByte:
                        return ETypeCode.SByte;
                    case TypeCode.Single:
                        return ETypeCode.Single;
                    case TypeCode.String:
                        return ETypeCode.String;
                    case TypeCode.UInt16:
                        return ETypeCode.UInt16;
                    case TypeCode.UInt32:
                        return ETypeCode.UInt32;
                    case TypeCode.UInt64:
                        return ETypeCode.UInt64;
                }

                return ETypeCode.Unknown;
            }
        }

        public static ETypeCode GetTypeCode(object value, out int rank)
        {
            var type = value.GetType();
            return GetTypeCode(type, out rank);
//            switch (value)
//            {
//                case Enum _:
//                    return ETypeCode.Enum;
//                case byte _:
//                    return ETypeCode.Byte;
//                case sbyte _:
//                    return ETypeCode.SByte;
//                case ushort _:
//                    return ETypeCode.UInt16;
//                case uint _:
//                    return ETypeCode.UInt32;
//                case ulong _:
//                    return ETypeCode.UInt64;
//                case short _:
//                    return ETypeCode.Int16;
//                case int _:
//                    return ETypeCode.Int32;
//                case long _:
//                    return ETypeCode.Int64;
//                case decimal _:
//                    return ETypeCode.Decimal;
//                case double _:
//                    return ETypeCode.Double;
//                case float _:
//                    return ETypeCode.Single;
//                case string _:
//                    return ETypeCode.String;
//                case bool _:
//                    return ETypeCode.Boolean;
//                case DateTime _:
//                    return ETypeCode.DateTime;
//                case TimeSpan _:
//                    return ETypeCode.Time;
//                case Guid _:
//                    return ETypeCode.Guid;
//                case byte[] _:
//                    return ETypeCode.Binary;
//                case char[] _:
//                    return ETypeCode.Char;
//            }
//
//            return ETypeCode.Unknown;
        }


        /// <summary>
        /// Converts a <see cref="JTokenType"/> into an ETypeCode
        /// </summary>
        /// <param name="jsonType"></param>
        /// <returns></returns>
        public static ETypeCode GetTypeCode(JTokenType jsonType)
        {
            switch (jsonType)
            {
                case JTokenType.Object:
                case JTokenType.Array:
                case JTokenType.Constructor:
                case JTokenType.Property:
                    return ETypeCode.Json;
                case JTokenType.None:
                case JTokenType.Comment:
                case JTokenType.Null:
                case JTokenType.Undefined:
                case JTokenType.Raw:
                case JTokenType.Uri:
                case JTokenType.String:
                    return ETypeCode.String;
                case JTokenType.Integer:
                    return ETypeCode.Int32;
                case JTokenType.Float:
                    return ETypeCode.Double;
                case JTokenType.Boolean:
                    return ETypeCode.Boolean;
                case JTokenType.Date:
                    return ETypeCode.DateTime;
                case JTokenType.Bytes:
                    return ETypeCode.Binary;
                case JTokenType.Guid:
                    return ETypeCode.Guid;
                case JTokenType.TimeSpan:
                    return ETypeCode.Time;
                default:
                    return ETypeCode.String;
            }

        }

        /// <summary>
        /// Gets the <see cref="Type"/> from the <see cref="ETypeCode"/>.
        /// </summary>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        public static Type GetType(ETypeCode typeCode)
        {
            switch (typeCode)
            {
                case ETypeCode.Byte:
                    return typeof(byte);
                case ETypeCode.SByte:
                    return typeof(sbyte);
                case ETypeCode.UInt16:
                    return typeof(ushort);
                case ETypeCode.UInt32:
                    return typeof(uint);
                case ETypeCode.UInt64:
                    return typeof(ulong);
                case ETypeCode.Int16:
                    return typeof(short);
                case ETypeCode.Int32:
                    return typeof(int);
                case ETypeCode.Int64:
                    return typeof(long);
                case ETypeCode.Decimal:
                    return typeof(decimal);
                case ETypeCode.Double:
                    return typeof(double);
                case ETypeCode.Single:
                    return typeof(float);
                case ETypeCode.Char:
                    return typeof(char[]);
                case ETypeCode.Xml:
                    return typeof(XmlDocument);
                case ETypeCode.Json:
                    return typeof(JObject);
                case ETypeCode.String:
                case ETypeCode.Text:
                    return typeof(string);
                case ETypeCode.Boolean:
                    return typeof(bool);
                case ETypeCode.DateTime:
                    return typeof(DateTime);
                case ETypeCode.Time:
                    return typeof(TimeSpan);
                case ETypeCode.Guid:
                    return typeof(Guid);
                case ETypeCode.Binary:
                    return typeof(byte[]);
                default:
                    return typeof(object);
            }
        }


        /// <summary>
        /// Returns the <see cref="DbType"/> from a <see cref="ETypeCode"/>
        /// </summary>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        public static DbType GetDbType(ETypeCode typeCode)
        {
            switch (typeCode)
            {
                case ETypeCode.Byte:
                    return DbType.Byte;
                case ETypeCode.SByte:
                    return DbType.SByte;
                case ETypeCode.UInt16:
                    return DbType.UInt16;
                case ETypeCode.UInt32:
                    return DbType.UInt32;
                case ETypeCode.UInt64:
                    return DbType.UInt64;
                case ETypeCode.Int16:
                    return DbType.Int16;
                case ETypeCode.Int32:
                    return DbType.Int32;
                case ETypeCode.Int64:
                    return DbType.Int64;
                case ETypeCode.Decimal:
                    return DbType.Decimal;
                case ETypeCode.Double:
                    return DbType.Double;
                case ETypeCode.Single:
                    return DbType.Single;
                case ETypeCode.String:
                    return DbType.String;
                case ETypeCode.Char:
                    return DbType.StringFixedLength;
                case ETypeCode.Json:
                    return DbType.String;
                case ETypeCode.Xml:
                    return DbType.Xml;
                case ETypeCode.Boolean:
                    return DbType.Boolean;
                case ETypeCode.DateTime:
                    return DbType.DateTime;
                case ETypeCode.Time:
                    return DbType.Time;
                case ETypeCode.Guid:
                    return DbType.Guid;
                case ETypeCode.Binary:
                    return DbType.Binary;
                default:
                    return DbType.String;
            }
        }


//        /// <summary>
//        /// Result of a data comparison
//        /// </summary>
//        public enum ECompareResult
//        {
//            Less = -1,
//            Equal = 0,
//            Greater = 1
//        }
//
//        /// <summary>
//        /// Compares two values of the specified <see cref="ETypeCode"/> and returns a <see cref="ECompareResult"/> indicating null, greater, less ,equal, not equal.  
//        /// <para/>If the two datatypes are null or DBNull the compare result will be "Equal"
//        /// <para/>If one value is null, it will be "Less" than the other value.
//        /// <para/>Note: The inputValue and compareValue can be of any underlying type, and they will be parsed before comparison.  If the parse fails, an exception will be raised.
//        /// </summary>
//        /// <param name="typeCode">data type to compare, if null datatype will be inferred from inputValue</param>
//        /// <param name="inputValue">primary value</param>
//        /// <param name="compareValue">value to compare against</param>
//        /// <returns>Compare Result = Greater, Less, Equal</returns>
//        public static ECompareResult Compare(ETypeCode? typeCode, object inputValue, object compareValue)
//        {
//            var inputType = inputValue?.GetType();
//            var compareType = compareValue?.GetType();
//
//            if ((inputValue == null || inputValue == DBNull.Value) && (compareValue == null || compareValue == DBNull.Value))
//                return ECompareResult.Equal;
//
//            if (inputValue == null || inputValue == DBNull.Value || compareValue == null || compareValue == DBNull.Value)
//                return (inputValue == null || inputValue is DBNull) ? ECompareResult.Less : ECompareResult.Greater;
//
//            var dataType = typeCode ?? GetTypeCode(inputValue, out _);
//            
//            var type = GetType(dataType);
//
//            if (inputType != type)
//            {
//                inputValue = TryParse(dataType, inputValue);
//            }
//
//            if (compareType != type)
//            {
//                compareValue = TryParse(dataType, compareValue);
//            }
//
//            return (ECompareResult) ((IComparable) inputValue).CompareTo((IComparable) compareValue);
//        }
//
//        public static object TryParse(ETypeCode tryDataType, int rank, object inputValue)
//        {
//            if (rank == 0)
//            {
//                return TryParse(tryDataType, inputValue);
//            }
//
//            var type = inputValue.GetType();
//            if (type.IsArray && type != typeof(byte[]) && type != typeof(char[]))
//            {
//                var inputArray = (object[]) inputValue;
//                var returnValue = new object[inputArray.Length];
//                for (var i = 0; i < inputArray.Length; i++)
//                {
//                    returnValue[i] = TryParse(tryDataType, inputArray[i]);
//                }
//
//                return returnValue;
//            }
//            
//            return TryParse(tryDataType, inputValue);
//        }
//
//        /// <summary>
//        /// Attempts to parse and convert the input value to the specified datatype.
//        /// </summary>
//        /// <param name="tryDataType">DataType to convert to</param>
//        /// <param name="inputValue">Input Value to convert</param>
//        /// <param name="maxLength">Optional: maximum length for a string value.</param>
//        /// <returns>True and the converted value for success, false and a message for conversion fail.</returns>
//        public static object TryParse(ETypeCode tryDataType, object inputValue)
//        {
//            if (inputValue == null || inputValue == DBNull.Value)
//            {
//                return null;
//            }
//
//            var tryType = GetType(tryDataType);
//            var inputType = inputValue.GetType();
//
//            if ((tryType == inputType ) || tryDataType == ETypeCode.Unknown)
//            {
//                return inputValue;
//            }
//            
//            switch (tryDataType)
//            {
//                case ETypeCode.Binary:
//                    if (inputValue is string inputString)
//                    {
//                        return HexToByteArray(inputString);
//                    }
//                    throw new DataTypeParseException("Binary type conversion not supported.");
//                case ETypeCode.Byte:
//                    return Convert.ToByte(inputValue);
//                case ETypeCode.SByte:
//                    return Convert.ToSByte(inputValue);
//                case ETypeCode.UInt16:
//                    return Convert.ToUInt16(inputValue);
//                case ETypeCode.UInt32:
//                    return Convert.ToUInt32(inputValue);
//                case ETypeCode.UInt64:
//                    return Convert.ToUInt64(inputValue);
//                case ETypeCode.Int16:
//                    return Convert.ToInt16(inputValue);
//                case ETypeCode.Int32:
//                    return Convert.ToInt32(inputValue);
//                case ETypeCode.Int64:
//                    return Convert.ToInt64(inputValue);
//                case ETypeCode.Decimal:
//                    return Convert.ToDecimal(inputValue);
//                case ETypeCode.Double:
//                    return Convert.ToDouble(inputValue);
//                case ETypeCode.Single:
//                    return Convert.ToSingle(inputValue);
//                case ETypeCode.Char:
//                    char[] charResult;
//                    switch (inputValue)
//                    {
//                        case char[] charValue:
//                            charResult = charValue;
//                            break;
//                        case string stringValue:
//                            charResult = stringValue.ToCharArray();
//                            break;
//                        default:
//                            charResult = ToString(inputValue).ToCharArray();
//                            break;
//                    }                    
//                    return charResult;
//                
//                case ETypeCode.String:
//                    string stringResult = ToString(inputValue);
//                    return stringResult;             
//                case ETypeCode.Boolean:
//                    switch (inputValue)
//                    {
//                        case string stringValue:
//                            var parsed = bool.TryParse(stringValue, out var parsedResult);
//                            if (parsed)
//                            {
//                                return parsedResult;
//                            }
//
//                            parsed = int.TryParse(stringValue, out var numberResult);
//                            if (parsed)
//                            {
//                                return Convert.ToBoolean(numberResult);
//                            }
//                            else
//                            {
//                                throw new FormatException("String was not recognized as a valid boolean");
//                            }
//
//                            default:
//                                return Convert.ToBoolean(inputValue);
//                    }
//                    
//                case ETypeCode.DateTime:
//                    return Convert.ToDateTime(inputValue);
//                case ETypeCode.Time:
//                    return TimeSpan.Parse(ToString(inputValue));
//                case ETypeCode.Guid:
//                    return Guid.Parse(ToString(inputValue));
//                case ETypeCode.Unknown:
//                    break;
//                case ETypeCode.Json:
//                    switch (inputValue)
//                    {
//                        case JToken jToken:
//                            return jToken;
//                        case string stringValue:
//                            return JToken.Parse(stringValue);
//                        default:
//                            throw new DataTypeParseException("The value is not a valid json string.");
//                    }
//                case ETypeCode.Xml:
//                    switch (inputValue)
//                    {
//                        case XmlDocument xmlDocument:
//                            return xmlDocument;
//                        case string stringValue:
//                            var xmlDocument1 = new XmlDocument();
//                            xmlDocument1.LoadXml(stringValue);
//                            return xmlDocument1;
//                        default:
//                            throw new DataTypeParseException("The value is not a valid xml string.");
//                    }
//                case ETypeCode.Text:
//                    return ToString(inputValue);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(tryDataType), tryDataType, null);
//            }
//
//            throw new ArgumentOutOfRangeException(nameof(tryDataType), tryDataType, null);
//
//        }
//
////        /// <summary>
////        /// Converts the object to a string.  Where the object is complex (such as arrays, classes) the string will be a json definition of the object.
////        /// </summary>
////        /// <param name="value"></param>
////        /// <returns></returns>
//        public static string ToString(object value)
//        {
//            switch (value)
//            {
//                case string stringValue:
//                    return stringValue;
//                case byte[] byteValue:
//                    return ByteArrayToHex(byteValue);
//                case char[] charValue:
//                    return new string(charValue);
//            }
//
//            if (!IsSimple(value.GetType()))
//            {
//                return JsonConvert.SerializeObject(value);
//            }
//
//            return value.ToString();
//        }
//
//        
        
//
//        public static object Add(ETypeCode typeCode,  object value1, object value2)
//        {
//            var convertedValue1 = TryParse(typeCode, value1);
//            var convertedValue2 = TryParse(typeCode, value2);
//            switch (typeCode)
//            {
//                case ETypeCode.Binary:
//                case ETypeCode.Byte:
//                case ETypeCode.SByte:
//                case ETypeCode.Char:
//                case ETypeCode.String:
//                case ETypeCode.Text:
//                case ETypeCode.DateTime:
//                case ETypeCode.Boolean:
//                case ETypeCode.Unknown:
//                case ETypeCode.Guid:
//                case ETypeCode.Time:
//                case ETypeCode.Json:
//                case ETypeCode.Xml:
//                    throw new Exception($"Cannot add {typeCode} types.");
//                case ETypeCode.UInt16:
//                    return (ushort)((ushort) convertedValue1 + (ushort) convertedValue2);
//                case ETypeCode.UInt32:
//                    return (uint) convertedValue1 + (uint) convertedValue2;
//                case ETypeCode.UInt64:
//                    return (ulong) convertedValue1 + (ulong) convertedValue2;
//                case ETypeCode.Int16:
//                    return (short)((short) convertedValue1 + (short) convertedValue2);
//                case ETypeCode.Int32:
//                    return (int) convertedValue1 + (int) convertedValue2;
//                case ETypeCode.Int64:
//                    return (long) convertedValue1 + (long) convertedValue2;
//                case ETypeCode.Decimal:
//                    return (decimal) convertedValue1 + (decimal) convertedValue2;
//                case ETypeCode.Double:
//                    return (double) convertedValue1 + (double) convertedValue2;
//                case ETypeCode.Single:
//                    return (float) convertedValue1 + (float) convertedValue2;
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
//            }
//        }
//        
//       public static object Subtract(ETypeCode typeCode,  object value1, object value2)
//        {
//            var convertedValue1 = TryParse(typeCode, value1);
//            var convertedValue2 = TryParse(typeCode, value2);
//            switch (typeCode)
//            {
//                case ETypeCode.Binary:
//                case ETypeCode.Byte:
//                case ETypeCode.SByte:
//                case ETypeCode.Char:
//                case ETypeCode.String:
//                case ETypeCode.Text:
//                case ETypeCode.DateTime:
//                case ETypeCode.Boolean:
//                case ETypeCode.Unknown:
//                case ETypeCode.Guid:
//                case ETypeCode.Time:
//                case ETypeCode.Json:
//                case ETypeCode.Xml:
//                    throw new Exception($"Cannot add {typeCode} types.");
//                case ETypeCode.UInt16:
//                    return (ushort)((ushort) convertedValue1 - (ushort) convertedValue2);
//                case ETypeCode.UInt32:
//                    return (uint) convertedValue1 - (uint) convertedValue2;
//                case ETypeCode.UInt64:
//                    return (ulong) convertedValue1 - (ulong) convertedValue2;
//                case ETypeCode.Int16:
//                    return (short)((short) convertedValue1 - (short) convertedValue2);
//                case ETypeCode.Int32:
//                    return (int) convertedValue1 - (int) convertedValue2;
//                case ETypeCode.Int64:
//                    return (long) convertedValue1 - (long) convertedValue2;
//                case ETypeCode.Decimal:
//                    return (decimal) convertedValue1 - (decimal) convertedValue2;
//                case ETypeCode.Double:
//                    return (double) convertedValue1 - (double) convertedValue2;
//                case ETypeCode.Single:
//                    return (float) convertedValue1 - (float) convertedValue2;
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
//            }
//        }
//
//       public static object Multiply(ETypeCode typeCode,  object value1, object value2)
//        {
//            var convertedValue1 = TryParse(typeCode, value1);
//            var convertedValue2 = TryParse(typeCode, value2);
//            switch (typeCode)
//            {
//                case ETypeCode.Binary:
//                case ETypeCode.Byte:
//                case ETypeCode.SByte:
//                case ETypeCode.Char:
//                case ETypeCode.String:
//                case ETypeCode.Text:
//                case ETypeCode.DateTime:
//                case ETypeCode.Boolean:
//                case ETypeCode.Unknown:
//                case ETypeCode.Guid:
//                case ETypeCode.Time:
//                case ETypeCode.Json:
//                case ETypeCode.Xml:
//                    throw new Exception($"Cannot add {typeCode} types.");
//                case ETypeCode.UInt16:
//                    return (ushort)((ushort) convertedValue1 * (ushort) convertedValue2);
//                case ETypeCode.UInt32:
//                    return (uint) convertedValue1 * (uint) convertedValue2;
//                case ETypeCode.UInt64:
//                    return (ulong) convertedValue1 * (ulong) convertedValue2;
//                case ETypeCode.Int16:
//                    return (short)((short) convertedValue1 * (short) convertedValue2);
//                case ETypeCode.Int32:
//                    return (int) convertedValue1 * (int) convertedValue2;
//                case ETypeCode.Int64:
//                    return (long) convertedValue1 * (long) convertedValue2;
//                case ETypeCode.Decimal:
//                    return (decimal) convertedValue1 * (decimal) convertedValue2;
//                case ETypeCode.Double:
//                    return (double) convertedValue1 * (double) convertedValue2;
//                case ETypeCode.Single:
//                    return (float) convertedValue1 * (float) convertedValue2;
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
//            }
//        }
//        
//           public static object Divide(ETypeCode typeCode,  object value1, object value2)
//        {
//            var convertedValue1 = TryParse(typeCode, value1);
//            var convertedValue2 = TryParse(typeCode, value2);
//            switch (typeCode)
//            {
//                case ETypeCode.Binary:
//                case ETypeCode.Byte:
//                case ETypeCode.SByte:
//                case ETypeCode.Char:
//                case ETypeCode.String:
//                case ETypeCode.Text:
//                case ETypeCode.DateTime:
//                case ETypeCode.Boolean:
//                case ETypeCode.Unknown:
//                case ETypeCode.Guid:
//                case ETypeCode.Time:
//                case ETypeCode.Json:
//                case ETypeCode.Xml:
//                    throw new Exception($"Cannot add {typeCode} types.");
//                case ETypeCode.UInt16:
//                    return (ushort)((ushort) convertedValue1 / (ushort) convertedValue2);
//                case ETypeCode.UInt32:
//                    return (uint) convertedValue1 / (uint) convertedValue2;
//                case ETypeCode.UInt64:
//                    return (ulong) convertedValue1 / (ulong) convertedValue2;
//                case ETypeCode.Int16:
//                    return (short)((short) convertedValue1 / (short) convertedValue2);
//                case ETypeCode.Int32:
//                    return (int) convertedValue1 / (int) convertedValue2;
//                case ETypeCode.Int64:
//                    return (long) convertedValue1 / (long) convertedValue2;
//                case ETypeCode.Decimal:
//                    return (decimal) convertedValue1 / (decimal) convertedValue2;
//                case ETypeCode.Double:
//                    return (double) convertedValue1 / (double) convertedValue2;
//                case ETypeCode.Single:
//                    return (float) convertedValue1 / (float) convertedValue2;
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
//            }
//        }
        
//        /// <summary>
//        /// Removes all non alphanumeric characters from the string
//        /// </summary>
//        /// <returns></returns>
//        public static string CleanString(string value)
//        {
//            if (string.IsNullOrEmpty(value))
//                return value;
//
//            var arr = value.Where(c => (char.IsLetterOrDigit(c))).ToArray();
//            var newValue = new string(arr);
//            return newValue;
//        }
//        
//        private static readonly uint[] _lookup32 = CreateLookup32();
//
//        private static uint[] CreateLookup32()
//        {
//            var result = new uint[256];
//            for (var i = 0; i < 256; i++)
//            {
//                var s=i.ToString("X2");
//                result[i] = s[0] + ((uint)s[1] << 16);
//            }
//            return result;
//        }
//        
//        /// <summary>
//        /// Converts a byte[] into a hex string
//        /// </summary>
//        /// <param name="bytes"></param>
//        /// <returns></returns>
//        public static string ByteArrayToHex(byte[] bytes)
//        {
//            var lookup32 = _lookup32;
//            var result = new char[bytes.Length * 2];
//            for (var i = 0; i < bytes.Length; i++)
//            {
//                var val = lookup32[bytes[i]];
//                result[2*i] = (char)val;
//                result[2*i + 1] = (char) (val >> 16);
//            }
//            return new string(result);
//        }
//        
//        /// <summary>
//        /// Converts a hex string into a byte[]
//        /// </summary>
//        /// <param name="hex"></param>
//        /// <returns></returns>
//        public static byte[] HexToByteArray(string hex)
//        {
//            var numberChars = hex.Length;
//            var bytes = new byte[numberChars / 2];
//            for (var i = 0; i < numberChars; i += 2)
//                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
//            return bytes;
//        }
    }
}
