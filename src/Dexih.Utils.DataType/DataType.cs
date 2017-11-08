using System;
using System.Collections;
using System.Data;
using System.Linq;
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
            if (dataType == typeof(byte) || dataType == typeof(byte?))
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
            Greater,
            Less,
            Equal
        }

        /// <summary>
        /// Compares two values of the specified <see cref="ETypeCode"/> and returns a <see cref="ECompareResult"/> indicating null, greater, less ,equal, not equal.  
        /// <para/>If the two datatypes are null or DBNull the compare result will be "Equal"
        /// <para/>If one value is null, it will be "Less" than the other value.
        /// <para/>Note: The inputValue and compareValue can be of any underlying type, and they will be parsed before comparison.  If the parse fails, an exception will be raised.
        /// </summary>
        /// <param name="dataType">data type to compare</param>
        /// <param name="inputValue">primary value</param>
        /// <param name="compareValue">value to compare against</param>
        /// <returns>Compare Result = Greater, Less, Equal</returns>
        public static ECompareResult Compare(ETypeCode dataType, object inputValue, object compareValue)
        {
            if ((inputValue == null || inputValue is DBNull) && (compareValue == null || compareValue is DBNull))
                return ECompareResult.Equal;

            if (inputValue == null || inputValue is DBNull || compareValue == null || compareValue is DBNull)
                return (inputValue == null || inputValue is DBNull) ? ECompareResult.Less : ECompareResult.Greater;

            var type = GetType(dataType);

            if (inputValue.GetType() != type)
            {
                inputValue = TryParse(dataType, inputValue);
            }

            if (compareValue.GetType() != type)
            {
                compareValue = TryParse(dataType, compareValue);
            }

            switch (dataType)
            {
                case ETypeCode.Byte:
                    return (byte)inputValue == (byte)compareValue ? ECompareResult.Equal : (byte)inputValue > (byte)compareValue ? ECompareResult.Greater : ECompareResult.Less;
                case ETypeCode.SByte:
                    return (sbyte)inputValue == (sbyte)compareValue ? ECompareResult.Equal : (sbyte)inputValue > (sbyte)compareValue ? ECompareResult.Greater : ECompareResult.Less;
                case ETypeCode.UInt16:
                    return (ushort)inputValue == (ushort)compareValue ? ECompareResult.Equal : (ushort)inputValue > (ushort)compareValue ? ECompareResult.Greater : ECompareResult.Less;
                case ETypeCode.UInt32:
                    return (uint)inputValue == (uint)compareValue ? ECompareResult.Equal : (uint)inputValue > (uint)compareValue ? ECompareResult.Greater : ECompareResult.Less;
                case ETypeCode.UInt64:
                    return (ulong)inputValue == (ulong)compareValue ? ECompareResult.Equal : (ulong)inputValue > (ulong)compareValue ? ECompareResult.Greater : ECompareResult.Less;
                case ETypeCode.Int16:
                    return (short)inputValue == (short)compareValue ? ECompareResult.Equal : (short)inputValue > (short)compareValue ? ECompareResult.Greater : ECompareResult.Less;
                case ETypeCode.Int32:
                    return (int)inputValue == (int)compareValue ? ECompareResult.Equal : (int)inputValue > (int)compareValue ? ECompareResult.Greater : ECompareResult.Less;
                case ETypeCode.Int64:
                    return (long)inputValue == (long)compareValue ? ECompareResult.Equal : (long)inputValue > (long)compareValue ? ECompareResult.Greater : ECompareResult.Less;
                case ETypeCode.Decimal:
                    return (decimal)inputValue == (decimal)compareValue ? ECompareResult.Equal : (decimal)inputValue > (decimal)compareValue ? ECompareResult.Greater : ECompareResult.Less;
                case ETypeCode.Double:
                    return Math.Abs((double)inputValue - (double)compareValue) < 0.0001 ? ECompareResult.Equal : (double)inputValue > (double)compareValue ? ECompareResult.Greater : ECompareResult.Less;
                case ETypeCode.Single:
                    return Math.Abs((float)inputValue - (float)compareValue) < 0.0001 ? ECompareResult.Equal : (float)inputValue > (float)compareValue ? ECompareResult.Greater : ECompareResult.Less;
                case ETypeCode.String:
                case ETypeCode.Json:
                case ETypeCode.Xml:
                case ETypeCode.Text:
                    var compareResult = string.Compare((string)inputValue, (string)compareValue);
                    return compareResult == 0 ? ECompareResult.Equal : compareResult < 0 ? ECompareResult.Less : ECompareResult.Greater;
                case ETypeCode.Guid:
                    compareResult = string.Compare(inputValue.ToString(), compareValue.ToString());
                    return compareResult == 0 ? ECompareResult.Equal : compareResult < 0 ? ECompareResult.Less : ECompareResult.Greater;
                case ETypeCode.Boolean:
                    return (bool)inputValue == (bool)compareValue ? ECompareResult.Equal : (bool)inputValue ? ECompareResult.Greater : ECompareResult.Less;
                case ETypeCode.DateTime:
                    return (DateTime)inputValue == (DateTime)compareValue ? ECompareResult.Equal : (DateTime)inputValue > (DateTime)compareValue ? ECompareResult.Greater : ECompareResult.Less;
                case ETypeCode.Time:
                    return (TimeSpan)inputValue == (TimeSpan)compareValue ? ECompareResult.Equal : (TimeSpan)inputValue > (TimeSpan)compareValue ? ECompareResult.Greater : ECompareResult.Less;
                case ETypeCode.Binary:
                    return StructuralComparisons.StructuralEqualityComparer.Equals(inputValue, compareValue) ? ECompareResult.Equal : ECompareResult.Greater;
                default:
                    throw new DataTypeCompareException("Compare failed due to unsupported datatype: " + dataType);
            }
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
            object result = null;
            if (inputValue == null)
            {
                return null;
            }

            if (tryDataType == ETypeCode.String)
            {
                result = inputValue is DBNull ? null : inputValue.ToString();
                if (maxLength != null && result != null && ((string)result).Length > maxLength)
                {
                    throw new DataTypeParseException("The string " + inputValue + " exceeds the maximum length of " + maxLength);
                }
                return result;
            }

            if(tryDataType == ETypeCode.Text || tryDataType == ETypeCode.Xml || tryDataType == ETypeCode.Json)
            {
                result = inputValue is DBNull ? null : inputValue.ToString();
                return result;
            }

            if (tryDataType == ETypeCode.Unknown)
            {
                return inputValue;
            }

            if (inputValue is DBNull)
            {
                result = inputValue;
                return result;
            }

            var inputType = GetTypeCode(inputValue.GetType());

            if (tryDataType == inputType)
            {
                result = inputValue;
                return result;
            }

            var tryBasicType = GetBasicType(tryDataType);
            var inputBasicType = GetBasicType(inputType);

            if (tryBasicType == EBasicType.Numeric && (inputBasicType == EBasicType.Numeric))
            {
                try
                {
                    switch (tryDataType)
                    {
                        case ETypeCode.Byte:
                            result = Convert.ToByte(inputValue);
                            return result;
                        case ETypeCode.SByte:
                            result = Convert.ToSByte(inputValue);
                            return result;
                        case ETypeCode.Int16:
                            result = Convert.ToInt16(inputValue);
                            return result;
                        case ETypeCode.Int32:
                            result = Convert.ToInt32(inputValue);
                            return result;
                        case ETypeCode.Int64:
                            result = Convert.ToInt64(inputValue);
                            return result;
                        case ETypeCode.UInt16:
                            result = Convert.ToUInt16(inputValue);
                            return result;
                        case ETypeCode.UInt32:
                            result = Convert.ToUInt32(inputValue);
                            return result;
                        case ETypeCode.UInt64:
                            result = Convert.ToUInt64(inputValue);
                            return result;
                        case ETypeCode.Double:
                            result = Convert.ToDouble(inputValue);
                            return result;
                        case ETypeCode.Decimal:
                            result = Convert.ToDecimal(inputValue);
                            return result;
                        case ETypeCode.Single:
                            result = Convert.ToSingle(inputValue);
                            return result;
                        case ETypeCode.Binary:
                            throw new DataTypeParseException("Binary type convertion not supported.");
                        default:
                            throw new DataTypeParseException("Unsupported datatype");
                    }
                } catch(Exception ex)
                {
                    throw new DataTypeParseException("Cannot convert value " + inputValue + " from numeric to " + tryDataType + ".  " + ex.Message);
                }
            }

            if (tryBasicType == EBasicType.Boolean && inputBasicType == EBasicType.Numeric)
            {
                try
                {
                    result = Convert.ToBoolean(inputValue);
                } catch(Exception ex)
                {
                    throw new DataTypeParseException("Cannot convert value " + inputValue + " from numeric to boolean.  " + ex.Message);
                }
            }

            if (tryBasicType == EBasicType.Numeric && inputBasicType == EBasicType.Date)
            {
                try
                {
                    result = ((DateTime)inputValue).Ticks;
                }
                catch (Exception ex)
                {
                    throw new DataTypeParseException("Cannot convert value " + inputValue + " from date to numeric.  " + ex.Message);
                }
            }

            if (tryBasicType == EBasicType.Date && inputBasicType == EBasicType.Numeric)
            {
                try
                {
                    result = new DateTime(Convert.ToInt64(inputValue));
                }
                catch (Exception ex)
                {
                    throw new DataTypeParseException("Cannot convert value " + inputValue + " from numeric to date.  " + ex.Message);
                }
            }

            if (result == null && tryBasicType == EBasicType.Date && inputBasicType != EBasicType.String)
            {
                throw new DataTypeParseException("Cannot convert value " + inputValue + " to " + tryDataType);
            }

            if (result == null)
            {
                string value;

                if (inputBasicType != EBasicType.String)
                    value = inputValue.ToString();
                else
                    value = (string)inputValue;

                bool returnValue;
                switch (tryDataType)
                {
                    case ETypeCode.Byte:
                        byte byteResult;
                        returnValue = byte.TryParse(value, out byteResult);
                        if (returnValue == false)
                            throw new DataTypeParseException("The value " + value + " could not be converted to a byte.");

                        result = byteResult;
                        break;
                    case ETypeCode.Int16:
                        short int16Result;
                        returnValue = short.TryParse(value, out int16Result);
                        if (returnValue == false)
                            throw new DataTypeParseException("The value " + value + " could not be converted to a Int16.");
                        result = int16Result;
                        break;
                    case ETypeCode.Int32:
                        int int32Result;
                        returnValue = int.TryParse(value, out int32Result);
                        if (returnValue == false)
                            throw new DataTypeParseException("The value " + value + " could not be converted to a Int32.");
                        result = int32Result;
                        break;
                    case ETypeCode.Int64:
                        long int64Result;
                        returnValue = long.TryParse(value, out int64Result);
                        if (returnValue == false)
                            throw new DataTypeParseException("The value " + value + " could not be converted to a Int64.");
                        result = int64Result;
                        break;
                    case ETypeCode.UInt16:
                        ushort uint16Result;
                        returnValue = ushort.TryParse(value, out uint16Result);
                        if (returnValue == false)
                            throw new DataTypeParseException("The value " + value + " could not be converted to a UInt16.");
                        result = uint16Result;
                        break;
                    case ETypeCode.UInt32:
                        uint uint32Result;
                        returnValue = uint.TryParse(value, out uint32Result);
                        if (returnValue == false)
                            throw new DataTypeParseException("The value " + value + " could not be converted to a UInt32.");
                        result = uint32Result;
                        break;
                    case ETypeCode.UInt64:
                        ulong uint64Result;
                        returnValue = ulong.TryParse(value, out uint64Result);
                        if (returnValue == false)
                            throw new DataTypeParseException("The value " + value + " could not be converted to a Int64.");
                        result = uint64Result;
                        break;
                    case ETypeCode.Double:
                        double doubleResult;
                        returnValue = double.TryParse(value, out doubleResult);
                        if (returnValue == false)
                            throw new DataTypeParseException("The value " + value + " could not be converted to a Double.");
                        result = doubleResult;
                        break;
                    case ETypeCode.Decimal:
                        decimal decimalResult;
                        returnValue = decimal.TryParse(value, out decimalResult);
                        if (returnValue == false)
                            throw new DataTypeParseException("The value " + value + " could not be converted to a Decimal.");
                        result = decimalResult;
                        break;
                    case ETypeCode.Single:
                        float singleResult;
                        returnValue = float.TryParse(value, out singleResult);
                        if (returnValue == false)
                            throw new DataTypeParseException("The value " + value + " could not be converted to a Single.");
                        result = singleResult;
                        break;
                    case ETypeCode.SByte:
                        sbyte sbyteResult;
                        returnValue = sbyte.TryParse(value, out sbyteResult);
                        if (returnValue == false)
                            throw new DataTypeParseException("The value " + value + " could not be converted to a SByte.");
                        result = sbyteResult;
                        break;
                    case ETypeCode.String:
                    case ETypeCode.Xml:
                    case ETypeCode.Json:
                    case ETypeCode.Text:
                        result = value;
                        break;
                    case ETypeCode.Guid:
                        Guid guidResult;
                        returnValue = Guid.TryParse(value, out guidResult);
                        if (returnValue == false)
                            throw new DataTypeParseException("The value " + value + " could not be converted to a Guid.");
                        result = guidResult;
                        break;
                    case ETypeCode.Boolean:
                        bool booleanResult;
                        returnValue = bool.TryParse(value, out booleanResult);
                        if (returnValue == false)
                        {
                            returnValue = short.TryParse(value, out int16Result);
                            if (returnValue == false)
                            {
                                throw new DataTypeParseException("The value " + value + " could not be converted to a boolean.");
                            }
                            switch (int16Result)
                            {
                                case 0:
                                    result = false;
                                    break;
                                case 1:
                                case -1:
                                    result = true;
                                    break;
                                default:
                                    throw new DataTypeParseException("The value " + value + " could not be converted to a boolean.");
                            }
                        }
                        else
                            result = booleanResult;
                        break;
                    case ETypeCode.DateTime:
                        DateTime dateTimeResult;
                        returnValue = DateTime.TryParse(value, out dateTimeResult);
                        if (returnValue == false)
                            throw new DataTypeParseException("The value " + value + " could not be converted to a DataTime.");
                        result = dateTimeResult;
                        break;
                    case ETypeCode.Time:
                        TimeSpan timeResult;
                        returnValue = TimeSpan.TryParse(value, out timeResult);
                        if (returnValue == false)
                            throw new DataTypeParseException("The value " + value + " could not be converted to a DataTime.");
                        result = timeResult;
                        break;
                    default:
                        throw new DataTypeParseException("Cannot convert value " + inputValue + " from to " + tryDataType + ".");
                }
            }

            return result;
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
    }
}
