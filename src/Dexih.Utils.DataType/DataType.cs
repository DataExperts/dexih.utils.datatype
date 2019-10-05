using System;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Xml;
using NetTopologySuite.Geometries;

namespace Dexih.Utils.DataType
{
    


    /// <summary>
    /// The data type library includes modified versions of the c# datatype functions, plus simple methods to parse and compare datatypes regardless of their base type.
    /// </summary>
    public static class DataType
    {
        private static readonly Type[] SimpleTypes = {typeof(string), typeof(decimal), typeof(DateTime), typeof(TimeSpan), typeof(Guid), typeof(byte[]), typeof(char[]), typeof(Geometry)};
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
        // [JsonConverter(typeof(StringEnumConverter))]
        public enum EBasicType : byte
        {
            Unknown,
            String,
            Numeric,
            Date,
            Time,
            Boolean,
            Binary,
            Enum,
            Geometry
        }

        /// <summary>
        /// List of supported type codes.  This is a cut down version of <see cref="TypeCode"/> enum.
        /// <para/> Note: Time, Binary & Unknown differ from the TypeCode.
        /// </summary>
        // [JsonConverter(typeof(StringEnumConverter))]
        public enum ETypeCode : byte
        {
            Unknown,
            Binary,
            Byte,
            Char,
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
            Json,
            Xml,
            Enum,
            CharArray,
            Object,
            Node, // a reference to another record-set.
            Geometry
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
                case ETypeCode.Char:
                    return char.MaxValue;
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
                case ETypeCode.CharArray:
                    return new string(char.MaxValue, length).ToCharArray();
                case ETypeCode.Xml:
                    var xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml("<zzzzz></zzzzz>");
                    return xmlDoc;
                case ETypeCode.Json:
                case ETypeCode.Node:
                    return JsonDocument.Parse("{\"zzzzz\": 1}").RootElement;
                case ETypeCode.String:
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
                case ETypeCode.Geometry:
                    return null;
                case ETypeCode.Enum:
                    return Int32.MaxValue;
                default:
                    throw new DataTypeException($"Max value not available for {typeCode}");
            }
        }

        /// <summary>
        /// Minimum value for a <see cref="ETypeCode"/>.
        /// </summary>
        /// <param name="typeCode"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static object GetDataTypeMinValue(ETypeCode typeCode, int length = 0)
        {
            switch (typeCode)
            {
                case ETypeCode.Byte:
                    return byte.MinValue;
                case ETypeCode.Char:
                    return char.MinValue;
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
                case ETypeCode.CharArray:
                    return new string('\x0001', length).ToCharArray();
                case ETypeCode.Xml:
                    var xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml("<aaaa></aaaa>");
                    return xmlDoc;
                case ETypeCode.Json:
                case ETypeCode.Node:
                    return JsonDocument.Parse("{}").RootElement;
                case ETypeCode.String:
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
                case ETypeCode.Geometry:
                    return null;
                case ETypeCode.Enum:
                    return 0;
                default:
                    throw new DataTypeException($"Max value not available for {typeCode}");
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
                case ETypeCode.Char:
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
                case ETypeCode.CharArray:
                case ETypeCode.Json:
                case ETypeCode.Xml:
                case ETypeCode.Text:
                case ETypeCode.Node:
                case ETypeCode.String: return EBasicType.String;
                case ETypeCode.Boolean: return EBasicType.Boolean;
                case ETypeCode.DateTime: return EBasicType.Date;
                case ETypeCode.Time: return EBasicType.Time;
                case ETypeCode.Binary: return EBasicType.Binary;
                case ETypeCode.Enum: return EBasicType.Enum;
                case ETypeCode.Geometry: return EBasicType.Geometry;
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
                if (dataType.IsEnum)
                {
                    return ETypeCode.Enum;
                }

                switch (Type.GetTypeCode(dataType))
                {
                    case TypeCode.Boolean:
                        return ETypeCode.Boolean;
                    case TypeCode.Byte:
                        return ETypeCode.Byte;
                    case TypeCode.Char:
                        return ETypeCode.Char;
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
                        if (dataType == typeof(char[])) return ETypeCode.CharArray;
                        if (dataType == typeof(JsonElement)) return ETypeCode.Json;
                        if (dataType == typeof(XmlDocument)) return ETypeCode.Xml;
                        if (typeof(Geometry).IsAssignableFrom(dataType)) return ETypeCode.Geometry;
                        
                        if (dataType.IsArray)
                        {
                            rank = dataType.GetArrayRank();
                            dataType = dataType.GetElementType();
                            continue;
                        }

                        if (dataType.IsEnum) return ETypeCode.Enum;

                        if (dataType.IsGenericType && dataType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            // if nullable type, then get the generic type and loop again.
                            dataType = dataType.GetGenericArguments()[0];
                            continue;
                        }
                    
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
            if (value == null)
            {
                rank = 0;
                return ETypeCode.Unknown;
            }
            
            var type = value.GetType();
            return GetTypeCode(type, out rank);
        }

        public static ETypeCode GetTypeCode(JsonValueKind valueKind)
        {
            switch (valueKind)
            {
                case JsonValueKind.Undefined:
                case JsonValueKind.Object:
                case JsonValueKind.Array:
                    return ETypeCode.Json;
                    break;
                case JsonValueKind.String:
                case JsonValueKind.Null:
                    return ETypeCode.String;
                    break;
                case JsonValueKind.Number:
                    return ETypeCode.Double;
                    break;
                case JsonValueKind.True:
                case JsonValueKind.False:
                    return ETypeCode.Boolean;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(valueKind), valueKind, null);
            }
        }
        
        /// <summary>
        /// Converts a <see cref="JTokenType"/> into an ETypeCode
        /// </summary>
        /// <param name="jsonType"></param>
        /// <returns></returns>
//        public static ETypeCode GetTypeCode(JTokenType jsonType)
//        {
//            switch (jsonType)
//            {
//                case JTokenType.Object:
//                case JTokenType.Array:
//                case JTokenType.Constructor:
//                case JTokenType.Property:
//                    return ETypeCode.Json;
//                case JTokenType.None:
//                case JTokenType.Comment:
//                case JTokenType.Null:
//                case JTokenType.Undefined:
//                case JTokenType.Raw:
//                case JTokenType.Uri:
//                case JTokenType.String:
//                    return ETypeCode.String;
//                case JTokenType.Integer:
//                    return ETypeCode.Int32;
//                case JTokenType.Float:
//                    return ETypeCode.Double;
//                case JTokenType.Boolean:
//                    return ETypeCode.Boolean;
//                case JTokenType.Date:
//                    return ETypeCode.DateTime;
//                case JTokenType.Bytes:
//                    return ETypeCode.Binary;
//                case JTokenType.Guid:
//                    return ETypeCode.Guid;
//                case JTokenType.TimeSpan:
//                    return ETypeCode.Time;
//                default:
//                    return ETypeCode.String;
//            }
//
//        }

        /// <summary>
        /// Gets the <see cref="Type"/> from the <see cref="ETypeCode"/>.
        /// </summary>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        public static Type GetType(ETypeCode typeCode, int rank = 0)
        {
            Type type;
            switch (typeCode)
            {
                case ETypeCode.Byte:
                    type = typeof(byte);
                    break;
                case ETypeCode.Char:
                    type = typeof(char);
                    break;
                case ETypeCode.SByte:
                    type = typeof(sbyte);
                    break;
                case ETypeCode.UInt16:
                    type = typeof(ushort);
                    break;
                case ETypeCode.UInt32:
                    type = typeof(uint);
                    break;
                case ETypeCode.UInt64:
                    type = typeof(ulong);
                    break;
                case ETypeCode.Int16:
                    type = typeof(short);
                    break;
                case ETypeCode.Int32:
                    type = typeof(int);
                    break;
                case ETypeCode.Int64:
                    type = typeof(long);
                    break;
                case ETypeCode.Decimal:
                    type = typeof(decimal);
                    break;
                case ETypeCode.Double:
                    type = typeof(double);
                    break;
                case ETypeCode.Single:
                    type = typeof(float);
                    break;
                case ETypeCode.CharArray:
                    type = typeof(char[]);
                    break;
                case ETypeCode.Xml:
                    type = typeof(XmlDocument);
                    break;
                case ETypeCode.Json:
                case ETypeCode.Node:
                    type = typeof(JsonElement);
                    break;
                case ETypeCode.String:
                case ETypeCode.Text:
                    type = typeof(string);
                    break;
                case ETypeCode.Boolean:
                    type = typeof(bool);
                    break;
                case ETypeCode.DateTime:
                    type = typeof(DateTime);
                    break;
                case ETypeCode.Time:
                    type = typeof(TimeSpan);
                    break;
                case ETypeCode.Guid:
                    type = typeof(Guid);
                    break;
                case ETypeCode.Binary:
                    type = typeof(byte[]);
                    break;
                case ETypeCode.Geometry:
                    type = typeof(Geometry);
                    break;
                case ETypeCode.Unknown:
                    type = typeof(string);
                    break;
                case ETypeCode.Enum:
                    type = typeof(object);
                    break;
                case ETypeCode.Object:
                    type = typeof(object);
                    break;
                default:
                    type = typeof(object);
                    break;
            }

            if (rank > 0)
            {
                return type.MakeArrayType(rank);
            }

            return type;
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
                case ETypeCode.Char:
                    return DbType.StringFixedLength;
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
                case ETypeCode.CharArray:
                    return DbType.StringFixedLength;
                case ETypeCode.Json:
                case ETypeCode.Node:
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
                case ETypeCode.Geometry:
                    return DbType.String;
                default:
                    return DbType.String;
            }
        }


    }
}
