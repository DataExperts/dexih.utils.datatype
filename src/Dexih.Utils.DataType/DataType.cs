using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Dexih.Utils.DataType
{
    /// <summary>
    /// The datatype library includes modified versions of the c# datatype functions, plus simple methods to parse and compare datatypes regardless of their base type.
    /// </summary>
    public static class DataType
    {

        [JsonConverter(typeof(StringEnumConverter))]
        /// <summary>
        /// A simplified list of primary possible datatypes.
        /// </summary>
        public enum EBasicType
        {
            Unknown,
            String,
            Numeric,
            Date,
            Time,
            Boolean,
            Binary
        }

        [JsonConverter(typeof(StringEnumConverter))]
        /// <summary>
        /// List of supported type codes.  This is a cutdown version of <see cref="TypeCode"/> enum.
        /// <para/> Note: Time, Binray & Unknown differ from the TypeCode.
        /// </summary>
        public enum ETypeCode
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
            Xml
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
                case ETypeCode.Json:
                case ETypeCode.Xml:
                case ETypeCode.Text:
                case ETypeCode.String: return EBasicType.String;
                case ETypeCode.Boolean: return EBasicType.Boolean;
                case ETypeCode.DateTime: return EBasicType.Date;
                case ETypeCode.Time: return EBasicType.Time;
                case ETypeCode.Binary: return EBasicType.Binary;
                default: return EBasicType.Unknown;
            }
        }

        /// <summary>
        /// Converts a <see cref="Type"/> into a <see cref="ETypeCode"/>
        /// </summary>
        /// <param name="dataType">Type value</param>
        /// <returns>ETypeCode</returns>
        public static ETypeCode GetTypeCode(Type dataType)
        {
            if (dataType == typeof(byte) || dataType == typeof(byte?)) // || dataType == typeof(byte&))
                return ETypeCode.Byte;
            if (dataType == typeof(sbyte) || dataType == typeof(sbyte?))
                return ETypeCode.SByte;
            if (dataType == typeof(ushort) || dataType == typeof(ushort?))
                return ETypeCode.UInt16;
            if (dataType == typeof(uint) || dataType == typeof(uint?))
                return ETypeCode.UInt32;
            if (dataType == typeof(ulong) || dataType == typeof(ulong?))
                return ETypeCode.UInt64;
            if (dataType == typeof(short) || dataType == typeof(short?))
                return ETypeCode.Int16;
            if (dataType == typeof(int) || dataType == typeof(int?))
                return ETypeCode.Int32;
            if (dataType == typeof(long) || dataType == typeof(long?))
                return ETypeCode.Int64;
            if (dataType == typeof(decimal) || dataType == typeof(decimal?))
                return ETypeCode.Decimal;
            if (dataType == typeof(double) || dataType == typeof(double?))
                return ETypeCode.Double;
            if (dataType == typeof(float) || dataType == typeof(float?))
                return ETypeCode.Single;
            if (dataType == typeof(string))
                return ETypeCode.String;
            if (dataType == typeof(bool) || dataType == typeof(bool?) )
                return ETypeCode.Boolean;
            if (dataType == typeof(DateTime) || dataType == typeof(DateTime?))
                return ETypeCode.DateTime;
            if (dataType == typeof(TimeSpan) || dataType == typeof(TimeSpan?))
                return ETypeCode.Time;
            if (dataType == typeof(Guid) || dataType == typeof(Guid?))
                return ETypeCode.Guid;
            if (dataType == typeof(byte[]))
                return ETypeCode.Binary;

            return ETypeCode.Unknown;
        }
        
        public static ETypeCode GetTypeCode(object value)
        {
            switch (value)
            {
                case byte byteValue:
                    return ETypeCode.Byte;
                    break;
                case sbyte sbyteValue:
                    return ETypeCode.SByte;
                    break;
                case ushort uint16Value:
                    return ETypeCode.UInt16;
                    break;
                case uint uint32Value:
                    return ETypeCode.UInt32;
                    break;
                case ulong uint64Value:
                    return ETypeCode.UInt64;
                    break;
                case short int16Value:
                    return ETypeCode.Int16;
                    break;
                case int int32Value:
                    return ETypeCode.Int32;
                    break;
                case long int64Value:
                    return ETypeCode.Int64;
                    break;
                case decimal decimalValue:
                    return ETypeCode.Decimal;
                    break;
                case double doubleValue:
                    return ETypeCode.Double;
                    break;
                case float singleValue:
                    return ETypeCode.Single;
                    break;
                case string stringValue:
                    return ETypeCode.String;
                    break;
                case bool boolValue:
                    return ETypeCode.Boolean;
                    break;
                case DateTime datetimeValue:
                    return ETypeCode.DateTime;
                    break;
                case TimeSpan timeValue:
                    return ETypeCode.Time;
                    break;
                case Guid guidValue:
                    return ETypeCode.Guid;
                    break;
                case byte[] binaryValue:
                    return ETypeCode.Binary;
                    break;
            }

            return ETypeCode.Unknown;
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
                case ETypeCode.String:
                case ETypeCode.Xml:
                case ETypeCode.Json:
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


        /// <summary>
        /// Result of a data comparison
        /// </summary>
        public enum ECompareResult
        {
            Less = -1,
            Equal = 0,
            Greater = 1
        }

        /// <summary>
        /// Compares two values of the specified <see cref="ETypeCode"/> and returns a <see cref="ECompareResult"/> indicating null, greater, less ,equal, not equal.  
        /// <para/>If the two datatypes are null or DBNull the compare result will be "Equal"
        /// <para/>If one value is null, it will be "Less" than the other value.
        /// <para/>Note: The inputValue and compareValue can be of any underlying type, and they will be parsed before comparison.  If the parse fails, an exception will be raised.
        /// </summary>
        /// <param name="typeCode">data type to compare, if null datatype will be inferred from inputValue</param>
        /// <param name="inputValue">primary value</param>
        /// <param name="compareValue">value to compare against</param>
        /// <returns>Compare Result = Greater, Less, Equal</returns>
        public static ECompareResult Compare(ETypeCode? typeCode, object inputValue, object compareValue)
        {
            var inputType = inputValue?.GetType();
            var compareType = compareValue?.GetType();

            if ((inputValue == null || inputValue == DBNull.Value) && (compareValue == null || compareValue == DBNull.Value))
                return ECompareResult.Equal;

            if (inputValue == null || inputValue == DBNull.Value || compareValue == null || compareValue == DBNull.Value)
                return (inputValue == null || inputValue is DBNull) ? ECompareResult.Less : ECompareResult.Greater;

            var dataType = typeCode ?? GetTypeCode(inputValue);
            
            var type = GetType(dataType);

            if (inputType != type)
            {
                inputValue = TryParse(dataType, inputValue);
            }

            if (compareType != type)
            {
                compareValue = TryParse(dataType, compareValue);
            }

            return (ECompareResult) ((IComparable) inputValue).CompareTo((IComparable) compareValue);
        }

        /// <summary>
        /// Attempts to parse and convert the input value to the specified datatype.
        /// </summary>
        /// <param name="tryDataType">DataType to convert to</param>
        /// <param name="inputValue">Input Value to convert</param>
        /// <param name="maxLength">Optional: maximum length for a string value.</param>
        /// <returns>True and the converted value for success, false and a message for conversion fail.</returns>
        public static object TryParse(ETypeCode tryDataType, object inputValue, int? maxLength = null)
        {
            object result;
            if (inputValue == null || inputValue == DBNull.Value)
            {
                return null;
            }

            var tryType = GetType(tryDataType);
            var inputType = inputValue.GetType();

            if ((tryType == inputType && maxLength == null) || tryDataType == ETypeCode.Unknown)
            {
                return inputValue;
            }
            
            switch (tryDataType)
            {
                case ETypeCode.Binary:
                    if (inputValue is string inputString)
                    {
                        return HexToByteArray(inputString);
                    }
                    throw new DataTypeParseException("Binary type convertion not supported.");
                case ETypeCode.Byte:
                    return Convert.ToByte(inputValue);
                case ETypeCode.SByte:
                    return Convert.ToSByte(inputValue);
                case ETypeCode.UInt16:
                    return Convert.ToUInt16(inputValue);
                case ETypeCode.UInt32:
                    return Convert.ToUInt32(inputValue);
                case ETypeCode.UInt64:
                    return Convert.ToUInt64(inputValue);
                case ETypeCode.Int16:
                    return Convert.ToInt16(inputValue);
                case ETypeCode.Int32:
                    return Convert.ToInt32(inputValue);
                case ETypeCode.Int64:
                    return Convert.ToInt64(inputValue);
                case ETypeCode.Decimal:
                    return Convert.ToDecimal(inputValue);
                case ETypeCode.Double:
                    return Convert.ToDouble(inputValue);
                case ETypeCode.Single:
                    return Convert.ToSingle(inputValue);
                case ETypeCode.String:
                    string stringResult;
                    switch (inputValue)
                    {
                        case byte[] byteValue:
                            stringResult = ByteArrayToHex(byteValue);
                            break;
                        case string stringValue:
                            stringResult = stringValue;
                            break;
                        default:
                            stringResult = inputValue.ToString();
                            break;
                    }

                    if (maxLength != null && stringResult != null && stringResult.Length > maxLength)
                    {
                        throw new DataTypeParseException("The string " + inputValue + " exceeds the maximum length of " + maxLength);
                    }
                    return stringResult;             
                case ETypeCode.Boolean:
                    switch (inputValue)
                    {
                        case string stringValue:
                            var parsed = bool.TryParse(stringValue, out var parsedResult);
                            if (parsed)
                            {
                                return parsedResult;
                            }

                            parsed = int.TryParse(stringValue, out var numberResult);
                            if (parsed)
                            {
                                return Convert.ToBoolean(numberResult);
                            }
                            else
                            {
                                throw new FormatException("String was not recognized as a valid Boolean");
                            }

                            default:
                                return Convert.ToBoolean(inputValue);
                    }
                    
                case ETypeCode.DateTime:
                    return Convert.ToDateTime(inputValue);
                case ETypeCode.Time:
                    return TimeSpan.Parse(inputValue.ToString());
                case ETypeCode.Guid:
                    return Guid.Parse(inputValue.ToString());
                case ETypeCode.Unknown:
                    break;
                case ETypeCode.Json:
                case ETypeCode.Xml:
                case ETypeCode.Text:
                    return inputValue.ToString();
                default:
                    throw new ArgumentOutOfRangeException(nameof(tryDataType), tryDataType, null);
            }

            throw new ArgumentOutOfRangeException(nameof(tryDataType), tryDataType, null);

//            if (tryDataType == ETypeCode.String)
//            {
//                if (inputValue is byte[] byteValue)
//                {
//                    result = ByteArrayToHex(byteValue);
//                }
//                else
//                {
//                    result = inputValue is DBNull ? null : inputValue.ToString();
//                }
//
//                if (maxLength != null && result != null && ((string)result).Length > maxLength)
//                {
//                    throw new DataTypeParseException("The string " + inputValue + " exceeds the maximum length of " + maxLength);
//                }
//                return result;
//            }
//
//            if(tryDataType == ETypeCode.Text || tryDataType == ETypeCode.Xml || tryDataType == ETypeCode.Json)
//            {
//                result = inputValue is DBNull ? null : inputValue.ToString();
//                return result;
//            }
//
//            if (tryDataType == ETypeCode.Unknown)
//            {
//                return inputValue;
//            }
//
//            if (inputValue is DBNull)
//            {
//                return inputValue;
//            }
//
//            var inputType = GetTypeCode(inputValue.GetType());
//
//            if (tryDataType == inputType)
//            {
//                result = inputValue;
//                return result;
//            }
//
//            var tryBasicType = GetBasicType(tryDataType);
//            var inputBasicType = GetBasicType(inputType);
//
//            if (tryBasicType == EBasicType.Numeric && (inputBasicType == EBasicType.Numeric))
//            {
//                try
//                {
//                    switch (tryDataType)
//                    {
//                        case ETypeCode.Byte:
//                            result = Convert.ToByte(inputValue);
//                            return result;
//                        case ETypeCode.SByte:
//                            result = Convert.ToSByte(inputValue);
//                            return result;
//                        case ETypeCode.Int16:
//                            result = Convert.ToInt16(inputValue);
//                            return result;
//                        case ETypeCode.Int32:
//                            result = Convert.ToInt32(inputValue);
//                            return result;
//                        case ETypeCode.Int64:
//                            result = Convert.ToInt64(inputValue);
//                            return result;
//                        case ETypeCode.UInt16:
//                            result = Convert.ToUInt16(inputValue);
//                            return result;
//                        case ETypeCode.UInt32:
//                            result = Convert.ToUInt32(inputValue);
//                            return result;
//                        case ETypeCode.UInt64:
//                            result = Convert.ToUInt64(inputValue);
//                            return result;
//                        case ETypeCode.Double:
//                            result = Convert.ToDouble(inputValue);
//                            return result;
//                        case ETypeCode.Decimal:
//                            result = Convert.ToDecimal(inputValue);
//                            return result;
//                        case ETypeCode.Single:
//                            result = Convert.ToSingle(inputValue);
//                            return result;
//                        case ETypeCode.Binary:
//                            throw new DataTypeParseException("Binary type convertion not supported.");
//                        default:
//                            throw new DataTypeParseException("Unsupported datatype");
//                    }
//                } catch(Exception ex)
//                {
//                    throw new DataTypeParseException("Cannot convert value " + inputValue + " from numeric to " + tryDataType + ".  " + ex.Message);
//                }
//            }
//
//            if (tryBasicType == EBasicType.Boolean && inputBasicType == EBasicType.Numeric)
//            {
//                try
//                {
//                    result = Convert.ToBoolean(inputValue);
//                } catch(Exception ex)
//                {
//                    throw new DataTypeParseException("Cannot convert value " + inputValue + " from numeric to boolean.  " + ex.Message);
//                }
//            }
//
//            if (tryBasicType == EBasicType.Numeric && inputBasicType == EBasicType.Date)
//            {
//                try
//                {
//                    result = ((DateTime)inputValue).Ticks;
//                }
//                catch (Exception ex)
//                {
//                    throw new DataTypeParseException("Cannot convert value " + inputValue + " from date to numeric.  " + ex.Message);
//                }
//            }
//
//            if (tryBasicType == EBasicType.Date && inputBasicType == EBasicType.Numeric)
//            {
//                try
//                {
//                    result = new DateTime(Convert.ToInt64(inputValue));
//                }
//                catch (Exception ex)
//                {
//                    throw new DataTypeParseException("Cannot convert value " + inputValue + " from numeric to date.  " + ex.Message);
//                }
//            }
//
//            if (result == null && tryBasicType == EBasicType.Date && inputBasicType != EBasicType.String)
//            {
//                throw new DataTypeParseException("Cannot convert value " + inputValue + " to " + tryDataType);
//            }
//
//            if (result == null)
//            {
//                string value;
//
//                if (inputBasicType != EBasicType.String)
//                    value = inputValue.ToString();
//                else
//                    value = (string)inputValue;
//
//                bool returnValue;
//                switch (tryDataType)
//                {
//                    case ETypeCode.Byte:
//                        returnValue = byte.TryParse(value, out var byteResult);
//                        if (returnValue == false)
//                            throw new DataTypeParseException("The value " + value + " could not be converted to a byte.");
//
//                        result = byteResult;
//                        break;
//                    case ETypeCode.Int16:
//                        returnValue = short.TryParse(value, out var int16Result);
//                        if (returnValue == false)
//                            throw new DataTypeParseException("The value " + value + " could not be converted to a Int16.");
//                        result = int16Result;
//                        break;
//                    case ETypeCode.Int32:
//                        returnValue = int.TryParse(value, out var int32Result);
//                        if (returnValue == false)
//                            throw new DataTypeParseException("The value " + value + " could not be converted to a Int32.");
//                        result = int32Result;
//                        break;
//                    case ETypeCode.Int64:
//                        returnValue = long.TryParse(value, out var int64Result);
//                        if (returnValue == false)
//                            throw new DataTypeParseException("The value " + value + " could not be converted to a Int64.");
//                        result = int64Result;
//                        break;
//                    case ETypeCode.UInt16:
//                        returnValue = ushort.TryParse(value, out var uint16Result);
//                        if (returnValue == false)
//                            throw new DataTypeParseException("The value " + value + " could not be converted to a UInt16.");
//                        result = uint16Result;
//                        break;
//                    case ETypeCode.UInt32:
//                        returnValue = uint.TryParse(value, out var uint32Result);
//                        if (returnValue == false)
//                            throw new DataTypeParseException("The value " + value + " could not be converted to a UInt32.");
//                        result = uint32Result;
//                        break;
//                    case ETypeCode.UInt64:
//                        returnValue = ulong.TryParse(value, out var uint64Result);
//                        if (returnValue == false)
//                            throw new DataTypeParseException("The value " + value + " could not be converted to a Int64.");
//                        result = uint64Result;
//                        break;
//                    case ETypeCode.Double:
//                        returnValue = double.TryParse(value, out var doubleResult);
//                        if (returnValue == false)
//                            throw new DataTypeParseException("The value " + value + " could not be converted to a Double.");
//                        result = doubleResult;
//                        break;
//                    case ETypeCode.Decimal:
//                        returnValue = decimal.TryParse(value, out var decimalResult);
//                        if (returnValue == false)
//                            throw new DataTypeParseException("The value " + value + " could not be converted to a Decimal.");
//                        result = decimalResult;
//                        break;
//                    case ETypeCode.Single:
//                        returnValue = float.TryParse(value, out var singleResult);
//                        if (returnValue == false)
//                            throw new DataTypeParseException("The value " + value + " could not be converted to a Single.");
//                        result = singleResult;
//                        break;
//                    case ETypeCode.SByte:
//                        returnValue = sbyte.TryParse(value, out var sbyteResult);
//                        if (returnValue == false)
//                            throw new DataTypeParseException("The value " + value + " could not be converted to a SByte.");
//                        result = sbyteResult;
//                        break;
//                    case ETypeCode.String:
//                    case ETypeCode.Xml:
//                    case ETypeCode.Json:
//                    case ETypeCode.Text:
//                        result = value;
//                        break;
//                    case ETypeCode.Guid:
//                        returnValue = Guid.TryParse(value, out var guidResult);
//                        if (returnValue == false)
//                            throw new DataTypeParseException("The value " + value + " could not be converted to a Guid.");
//                        result = guidResult;
//                        break;
//                    case ETypeCode.Boolean:
//                        returnValue = bool.TryParse(value, out var booleanResult);
//                        if (returnValue == false)
//                        {
//                            returnValue = short.TryParse(value, out int16Result);
//                            if (returnValue == false)
//                            {
//                                throw new DataTypeParseException("The value " + value + " could not be converted to a boolean.");
//                            }
//                            switch (int16Result)
//                            {
//                                case 0:
//                                    result = false;
//                                    break;
//                                case 1:
//                                case -1:
//                                    result = true;
//                                    break;
//                                default:
//                                    throw new DataTypeParseException("The value " + value + " could not be converted to a boolean.");
//                            }
//                        }
//                        else
//                            result = booleanResult;
//                        break;
//                    case ETypeCode.DateTime:
//                        returnValue = DateTime.TryParse(value, out var dateTimeResult);
//                        if (returnValue == false)
//                            throw new DataTypeParseException("The value " + value + " could not be converted to a DataTime.");
//                        result = dateTimeResult;
//                        break;
//                    case ETypeCode.Time:
//                        returnValue = TimeSpan.TryParse(value, out var timeResult);
//                        if (returnValue == false)
//                            throw new DataTypeParseException("The value " + value + " could not be converted to a DataTime.");
//                        result = timeResult;
//                        break;
//                    case ETypeCode.Binary:
//                        if (value != null)
//                        {
//                            result = HexToByteArray(value);
//                            break;
//                        }
//                        throw new DataTypeParseException("The value " + value + " could not be converted to binary.");
//                    default:
//                        throw new DataTypeParseException("Cannot convert value " + inputValue + " from to " + tryDataType + ".");
//                }
//            }
//
//            return result;
        }

        public static object Add(ETypeCode typeCode,  object value1, object value2)
        {
            var convertedValue1 = TryParse(typeCode, value1);
            var convertedValue2 = TryParse(typeCode, value2);
            switch (typeCode)
            {
                case ETypeCode.Binary:
                case ETypeCode.Byte:
                case ETypeCode.SByte:
                case ETypeCode.String:
                case ETypeCode.Text:
                case ETypeCode.DateTime:
                case ETypeCode.Boolean:
                case ETypeCode.Unknown:
                case ETypeCode.Guid:
                case ETypeCode.Time:
                case ETypeCode.Json:
                case ETypeCode.Xml:
                    throw new Exception($"Cannot add {typeCode} types.");
                case ETypeCode.UInt16:
                    return (ushort)((ushort) convertedValue1 + (ushort) convertedValue2);
                case ETypeCode.UInt32:
                    return (uint) convertedValue1 + (uint) convertedValue2;
                case ETypeCode.UInt64:
                    return (ulong) convertedValue1 + (ulong) convertedValue2;
                case ETypeCode.Int16:
                    return (short)((short) convertedValue1 + (short) convertedValue2);
                case ETypeCode.Int32:
                    return (int) convertedValue1 + (int) convertedValue2;
                case ETypeCode.Int64:
                    return (long) convertedValue1 + (long) convertedValue2;
                case ETypeCode.Decimal:
                    return (decimal) convertedValue1 + (decimal) convertedValue2;
                case ETypeCode.Double:
                    return (double) convertedValue1 + (double) convertedValue2;
                case ETypeCode.Single:
                    return (float) convertedValue1 + (float) convertedValue2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }
        
       public static object Subtract(ETypeCode typeCode,  object value1, object value2)
        {
            var convertedValue1 = TryParse(typeCode, value1);
            var convertedValue2 = TryParse(typeCode, value2);
            switch (typeCode)
            {
                case ETypeCode.Binary:
                case ETypeCode.Byte:
                case ETypeCode.SByte:
                case ETypeCode.String:
                case ETypeCode.Text:
                case ETypeCode.DateTime:
                case ETypeCode.Boolean:
                case ETypeCode.Unknown:
                case ETypeCode.Guid:
                case ETypeCode.Time:
                case ETypeCode.Json:
                case ETypeCode.Xml:
                    throw new Exception($"Cannot add {typeCode} types.");
                case ETypeCode.UInt16:
                    return (ushort)((ushort) convertedValue1 - (ushort) convertedValue2);
                case ETypeCode.UInt32:
                    return (uint) convertedValue1 - (uint) convertedValue2;
                case ETypeCode.UInt64:
                    return (ulong) convertedValue1 - (ulong) convertedValue2;
                    break;
                case ETypeCode.Int16:
                    return (short)((short) convertedValue1 - (short) convertedValue2);
                    break;
                case ETypeCode.Int32:
                    return (int) convertedValue1 - (int) convertedValue2;
                    break;
                case ETypeCode.Int64:
                    return (long) convertedValue1 - (long) convertedValue2;
                    break;
                case ETypeCode.Decimal:
                    return (decimal) convertedValue1 - (decimal) convertedValue2;
                    break;
                case ETypeCode.Double:
                    return (double) convertedValue1 - (double) convertedValue2;
                    break;
                case ETypeCode.Single:
                    return (float) convertedValue1 - (float) convertedValue2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }

       public static object Multiply(ETypeCode typeCode,  object value1, object value2)
        {
            var convertedValue1 = TryParse(typeCode, value1);
            var convertedValue2 = TryParse(typeCode, value2);
            switch (typeCode)
            {
                case ETypeCode.Binary:
                case ETypeCode.Byte:
                case ETypeCode.SByte:
                case ETypeCode.String:
                case ETypeCode.Text:
                case ETypeCode.DateTime:
                case ETypeCode.Boolean:
                case ETypeCode.Unknown:
                case ETypeCode.Guid:
                case ETypeCode.Time:
                case ETypeCode.Json:
                case ETypeCode.Xml:
                    throw new Exception($"Cannot add {typeCode} types.");
                case ETypeCode.UInt16:
                    return (ushort)((ushort) convertedValue1 * (ushort) convertedValue2);
                    break;
                case ETypeCode.UInt32:
                    return (uint) convertedValue1 * (uint) convertedValue2;
                    break;
                case ETypeCode.UInt64:
                    return (ulong) convertedValue1 * (ulong) convertedValue2;
                    break;
                case ETypeCode.Int16:
                    return (short)((short) convertedValue1 * (short) convertedValue2);
                    break;
                case ETypeCode.Int32:
                    return (int) convertedValue1 * (int) convertedValue2;
                    break;
                case ETypeCode.Int64:
                    return (long) convertedValue1 * (long) convertedValue2;
                    break;
                case ETypeCode.Decimal:
                    return (decimal) convertedValue1 * (decimal) convertedValue2;
                    break;
                case ETypeCode.Double:
                    return (double) convertedValue1 * (double) convertedValue2;
                    break;
                case ETypeCode.Single:
                    return (float) convertedValue1 * (float) convertedValue2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }
        
           public static object Divide(ETypeCode typeCode,  object value1, object value2)
        {
            var convertedValue1 = TryParse(typeCode, value1);
            var convertedValue2 = TryParse(typeCode, value2);
            switch (typeCode)
            {
                case ETypeCode.Binary:
                case ETypeCode.Byte:
                case ETypeCode.SByte:
                case ETypeCode.String:
                case ETypeCode.Text:
                case ETypeCode.DateTime:
                case ETypeCode.Boolean:
                case ETypeCode.Unknown:
                case ETypeCode.Guid:
                case ETypeCode.Time:
                case ETypeCode.Json:
                case ETypeCode.Xml:
                    throw new Exception($"Cannot add {typeCode} types.");
                case ETypeCode.UInt16:
                    return (ushort)((ushort) convertedValue1 / (ushort) convertedValue2);
                    break;
                case ETypeCode.UInt32:
                    return (uint) convertedValue1 / (uint) convertedValue2;
                    break;
                case ETypeCode.UInt64:
                    return (ulong) convertedValue1 / (ulong) convertedValue2;
                    break;
                case ETypeCode.Int16:
                    return (short)((short) convertedValue1 / (short) convertedValue2);
                    break;
                case ETypeCode.Int32:
                    return (int) convertedValue1 / (int) convertedValue2;
                    break;
                case ETypeCode.Int64:
                    return (long) convertedValue1 / (long) convertedValue2;
                    break;
                case ETypeCode.Decimal:
                    return (decimal) convertedValue1 / (decimal) convertedValue2;
                    break;
                case ETypeCode.Double:
                    return (double) convertedValue1 / (double) convertedValue2;
                    break;
                case ETypeCode.Single:
                    return (float) convertedValue1 / (float) convertedValue2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
        }
        
        /// <summary>
        /// Removes all non alphanumeric characters from the string
        /// </summary>
        /// <returns></returns>
        public static string CleanString(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var arr = value.Where(c => (char.IsLetterOrDigit(c))).ToArray();
            var newValue = new string(arr);
            return newValue;
        }
        
        private static readonly uint[] _lookup32 = CreateLookup32();

        private static uint[] CreateLookup32()
        {
            var result = new uint[256];
            for (var i = 0; i < 256; i++)
            {
                var s=i.ToString("X2");
                result[i] = ((uint)s[0]) + ((uint)s[1] << 16);
            }
            return result;
        }
        
        /// <summary>
        /// Converts a byte[] into a hex string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteArrayToHex(byte[] bytes)
        {
            var lookup32 = _lookup32;
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
        public static byte[] HexToByteArray(string hex)
        {
            var numberChars = hex.Length;
            var bytes = new byte[numberChars / 2];
            for (var i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}
