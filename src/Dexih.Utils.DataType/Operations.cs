using System;
using System.Data;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace Dexih.Utils.DataType
{
    /// <summary>
    /// Result of a data comparison
    /// </summary>
    public enum ECompareResult
    {
        Less = -1,
        Equal = 0,
        Greater = 1
    }
    
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
        public static T Divide<T>(T a, T b) => Operations<T>.Divide.Value(a, b);
        public static T Multiply<T>(T a, T b) => Operations<T>.Multiply.Value(a, b);
        public static T Negate<T>(T a) => Operations<T>.Negate.Value(a);
        public static bool GreaterThan<T>(T a, T b) => Operations<T>.GreaterThan.Value(a, b);
        public static bool LessThan<T>(T a, T b) => Operations<T>.LessThan.Value(a, b);
        public static bool GreaterThanOrEqual<T>(T a, T b) => Operations<T>.GreaterThanOrEqual.Value(a, b);
        public static bool LessThanOrEqual<T>(T a, T b) => Operations<T>.LessThanOrEqual.Value(a, b);
        public static bool Equal<T>(T a, T b) => Operations<T>.Equal.Value(a, b);
        public static string ToString<T>(T a) => Operations<T>.ToString(a);
        public static T Parse<T>(object a) => Operations<T>.Parse(a);
        public static ECompareResult Compare<T>(T inputValue, T compareTo) => Operations<T>.Compare.Value(inputValue, compareTo);
        public static ECompareResult Compare<T>(object inputValue, object compareTo) => Operations<T>.CompareObject.Value(inputValue, compareTo);

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

        public static ECompareResult Compare(object inputValue, object compareTo)
        {
            if ((inputValue == null || inputValue == DBNull.Value) && (compareTo == null || compareTo == DBNull.Value))
                return ECompareResult.Equal;

            if (inputValue == null || inputValue == DBNull.Value || compareTo == null || compareTo == DBNull.Value)
                return (inputValue == null || inputValue is DBNull) ? ECompareResult.Less : ECompareResult.Greater;

            var type = inputValue.GetType();
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


        public static ECompareResult Compare(DataType.ETypeCode typeCode, object inputValue, object compareTo)
        {
            if ((inputValue == null || inputValue == DBNull.Value) && (compareTo == null || compareTo == DBNull.Value))
                return ECompareResult.Equal;

            if (inputValue == null || inputValue == DBNull.Value || compareTo == null || compareTo == DBNull.Value)
                return (inputValue == null || inputValue is DBNull) ? ECompareResult.Less : ECompareResult.Greater;
            
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
        public static readonly Lazy<Func<T, T>> Negate = CreateExpressionNegate();
        
        public static readonly Lazy<Func<T, T, bool>> GreaterThan = CreateGreaterThan();
        public static readonly Lazy<Func<T, T, bool>> LessThan = CreateLessThan();
        public static readonly Lazy<Func<T, T, bool>> GreaterThanOrEqual = CreateGreaterThanOrEqual();
        public static readonly Lazy<Func<T, T, bool>> LessThanOrEqual = CreateLessThanOrEqual();
        public static readonly Lazy<Func<T, T, bool>> Equal = CreateEqual();
        public new static readonly Func<T, string> ToString = CreateToString();
        public static readonly Func<object, T> Parse = CreateParse();
        public static readonly T Zero = default;
        
        public static readonly Lazy<Func<T, T, ECompareResult>> Compare = CreateCompare();
        public static readonly Lazy<Func<object, object, ECompareResult>> CompareObject = CreateCompareObject();

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
        
        private static Lazy<Func<T, T, ECompareResult>> CreateCompare()
        {
            var type = typeof(T);
            if (typeof(IComparable).IsAssignableFrom(type))
            {
                ECompareResult Compare(T a, T b)
                {
                    return (ECompareResult) ((IComparable) a).CompareTo((IComparable) b);
                }
                
                return new Lazy<Func<T, T, ECompareResult>>(() => Compare);
            }
            else
            {
                return new Lazy<Func<T, T, ECompareResult>>(() => throw new InvalidCastException($"The data type {typeof(T)} is not supported for comparisons."));
            }
        }
        
        private static Lazy<Func<object, object, ECompareResult>> CreateCompareObject()
        {
            var type = typeof(T);
            if (typeof(IComparable).IsAssignableFrom(type))
            {
                ECompareResult Compare(object a, object b)
                {
                    T inputValue, compareTo;

                    if (type == a.GetType())
                    {
                        inputValue = (T) a;
                    }
                    else
                    {
                        inputValue = Parse(a);
                    }
                    
                    if (type == b.GetType())
                    {
                        compareTo = (T) b;
                    }
                    else
                    {
                        compareTo = Parse(b);
                    }
                    
                    return (ECompareResult) ((IComparable) inputValue).CompareTo((IComparable) compareTo);
                }
                
                return new Lazy<Func<object, object, ECompareResult>>(() => Compare);
            }
            else
            {
                return new Lazy<Func<object, object, ECompareResult>>(() => throw new InvalidCastException($"The data type {typeof(T)} is not supported for comparisons."));
            }
        }
        
        private static Lazy<Func<T, T, bool>> CreateEqual()
        {
            var type = typeof(T);
            if (IsBoolSupportedType(type))
            {
                var p1 = Expression.Parameter(typeof(T), "p1");
                var p2 = Expression.Parameter(typeof(T), "p2");
                var exp = Expression.Lambda<Func<T, T, bool>>(Expression.Equal(p1, p2), p1, p2).Compile();
                return new Lazy<Func<T, T, bool>>(() => exp);
            }
            else if (typeof(IComparable).IsAssignableFrom(type))
            {
                bool Compare(T a, T b)
                {
                    return ((IComparable) a).CompareTo((IComparable) b) == 0;
                }
                
                return new Lazy<Func<T, T, bool>>(() => Compare);
            }
            else
            {
                return new Lazy<Func<T, T, bool>>(() => throw new InvalidCastException($"The data type {typeof(T)} is not supported for comparisons."));
            }
        }
        
        private static Lazy<Func<T, T, bool>> CreateGreaterThan()
        {
            var type = typeof(T);
            if (IsBoolSupportedType(type))
            {
                var p1 = Expression.Parameter(typeof(T), "p1");
                var p2 = Expression.Parameter(typeof(T), "p2");
                var exp = Expression.Lambda<Func<T, T, bool>>(Expression.GreaterThan(p1, p2), p1, p2).Compile();
                return new Lazy<Func<T, T, bool>>(() => exp);
            }
            else if (typeof(IComparable).IsAssignableFrom(type))
            {
                bool Compare(T a, T b)
                {
                    return ((IComparable) a).CompareTo((IComparable) b) == 1;
                }
                
                return new Lazy<Func<T, T, bool>>(() => Compare);
            }
            else
            {
                return new Lazy<Func<T, T, bool>>(() => throw new InvalidCastException($"The data type {typeof(T)} is not supported for comparisons."));
            }
        }

        private static Lazy<Func<T, T, bool>> CreateLessThan()
        {
            var type = typeof(T);
            if (IsBoolSupportedType(type))
            {
                var p1 = Expression.Parameter(typeof(T), "p1");
                var p2 = Expression.Parameter(typeof(T), "p2");
                var exp = Expression.Lambda<Func<T, T, bool>>(Expression.LessThan(p1, p2), p1, p2).Compile();
                return new Lazy<Func<T, T, bool>>(() => exp);
            }
            else if (typeof(IComparable).IsAssignableFrom(type))
            {
                bool Compare(T a, T b)
                {
                    return ((IComparable) a).CompareTo((IComparable) b) == -1;
                }
                
                return new Lazy<Func<T, T, bool>>(() => Compare);
            }
            else
            {
                return new Lazy<Func<T, T, bool>>(() => throw new InvalidCastException($"The data type {typeof(T)} is not supported for comparisons."));
            }
        }

        private static Lazy<Func<T, T, bool>> CreateGreaterThanOrEqual()
        {
            var type = typeof(T);
            if (IsBoolSupportedType(type))
            {
                var p1 = Expression.Parameter(typeof(T), "p1");
                var p2 = Expression.Parameter(typeof(T), "p2");
                var exp = Expression.Lambda<Func<T, T, bool>>(Expression.GreaterThanOrEqual(p1, p2), p1, p2).Compile();
                return new Lazy<Func<T, T, bool>>(() => exp);
            }
            else if (typeof(IComparable).IsAssignableFrom(type))
            {
                bool Compare(T a, T b)
                {
                    return ((IComparable) a).CompareTo((IComparable) b) >= 0;
                }
                
                return new Lazy<Func<T, T, bool>>(() => Compare);
            }
            else
            {
                return new Lazy<Func<T, T, bool>>(() => throw new InvalidCastException($"The data type {typeof(T)} is not supported for comparisons."));
            }
        }

        private static Lazy<Func<T, T, bool>> CreateLessThanOrEqual()
        {
            var type = typeof(T);
            if (IsBoolSupportedType(type))
            {
                var p1 = Expression.Parameter(typeof(T), "p1");
                var p2 = Expression.Parameter(typeof(T), "p2");
                var exp = Expression.Lambda<Func<T, T, bool>>(Expression.LessThanOrEqual(p1, p2), p1, p2).Compile();
                return new Lazy<Func<T, T, bool>>(() => exp);
            }
            else if (typeof(IComparable).IsAssignableFrom(type))
            {
                bool Compare(T a, T b)
                {
                    return ((IComparable) a).CompareTo((IComparable) b) <= 0;
                }
                
                return new Lazy<Func<T, T, bool>>(() => Compare);
            }
            else
            {
                return new Lazy<Func<T, T, bool>>(() => throw new InvalidCastException($"The data type {typeof(T)} is not supported for comparisons."));
            }
        }

        
        private static Lazy<Func<T, T>> CreateExpressionNegate()
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
        
        
        public static Func<T, string> CreateToString()
        {
            var dataType = typeof(T); 
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
                    return value => value.ToString();
                case TypeCode.Object:
                    if (dataType == typeof(TimeSpan) || dataType == typeof(TimeSpan?)) return value => value.ToString();
                    if (dataType == typeof(Guid) || dataType == typeof(Guid?)) return value => value.ToString();
                    if (dataType == typeof(byte[])) return value => ByteArrayToHex(value as byte[]);
                    if (dataType == typeof(char[])) return value => value.ToString();
                    if (dataType == typeof(JToken)) return value => value.ToString();
                    if (dataType == typeof(XmlDocument)) return value => (value as XmlDocument)?.InnerXml;
                    break;
            }

            throw new ArgumentOutOfRangeException();
        }

        public static Func<object, T> ConvertToBoolean()
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

        public static Func<object, T> ConvertToCharArray()
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
        
        public static Func<object, T> ConvertToByteArray()
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
        
        public static Func<object, T> ConvertToTimeSpan()
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
        
        public static Func<object, T> ConvertToGuid()
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
        
        public static Func<object, T> ConvertToJson()
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
        
        public static Func<object, T> ConvertToXml()
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

        public static Func<object, T> ConvertToString()
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
                else
                {
                    return (T) (object) value.ToString();
                }

            };
        }


        public static Func<object, T> CreateParse()
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
                    else throw new ArgumentOutOfRangeException();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return exp;
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
        public static string ByteArrayToHex(byte[] bytes)
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
        public static byte[] HexToByteArray(string hex)
        {
            var numberChars = hex.Length;
            var bytes = new byte[numberChars / 2];
            for (var i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

//        public static IOperations<T> Create()
//        {
//            var dataType = typeof(T); 
//            switch (Type.GetTypeCode(dataType))
//            {
//                case TypeCode.Byte:
//                    return (IOperations<T>) new ByteOperations();
//                case TypeCode.Decimal:
//                    return (IOperations<T>) new DecimalOperations();
//                case TypeCode.Double:
//                    return (IOperations<T>) new DoubleOperations();
//                case TypeCode.Int16:
//                    return (IOperations<T>) new Int16Operations();
//                case TypeCode.Int32:
//                    return (IOperations<T>) new Int32Operations();
//                case TypeCode.Int64:
//                    return (IOperations<T>) new Int64Operations();
//                case TypeCode.SByte:
//                    return (IOperations<T>) new SByteOperations();
//                case TypeCode.Single:
//                    return (IOperations<T>) new SingleOperations();
//                case TypeCode.UInt16:
//                    return (IOperations<T>) new UInt16Operations();
//                case TypeCode.UInt32:
//                    return (IOperations<T>) new UInt32Operations();
//                case TypeCode.UInt64:
//                    return (IOperations<T>) new UInt64Operations();
//                case TypeCode.Boolean:
//                    return (IOperations<T>) new BooleanOperations();
//                case TypeCode.DateTime:
//                    return (IOperations<T>) new DateTimeOperations();
//                case TypeCode.DBNull:
//                    break;
//                case TypeCode.Object:
//                    if (dataType == typeof(TimeSpan) || dataType == typeof(TimeSpan?)) return (IOperations<T>) new TimeOperations();
//                    if (dataType == typeof(Guid) || dataType == typeof(Guid?)) return (IOperations<T>) new GuidOperations();
//                    if (dataType == typeof(byte[])) return (IOperations<T>) new ByteArrayOperations();
//                    if (dataType == typeof(char[])) return (IOperations<T>) new CharArrayOperations();
//                    if (dataType == typeof(JToken)) return (IOperations<T>) new JsonOperations();
//                    if (dataType == typeof(XmlDocument)) return (IOperations<T>) new XmlOperations();
//                    break;
//                case TypeCode.String:
//                    return (IOperations<T>) new StringOperations();
//                default:
//                    throw new ArgumentOutOfRangeException();
//            }
//            
//            throw new DataTypeException($"The datatype {typeof(T)} is not a valid type for arithmetic.");
//        }
//
//        class BooleanOperations : IOperations<bool>
//        {
//
//            public bool Add(bool a, bool b) => throw new OverflowException("Can not add a boolean.");
//            public bool Subtract(bool a, bool b)  => throw new OverflowException("Can not subtract a boolean.");
//            public bool Multiply(bool a, bool b)  => throw new OverflowException("Can not multiply a boolean.");
//            public bool Divide(bool a, bool b)  => throw new OverflowException("Can not divide a boolean.");
//            public int Sign(bool a)  => throw new OverflowException("Can not get the sign of a boolean.");
//            public bool Negate(bool a)  => throw new OverflowException("Can not negate a boolean.");
//            public bool Zero => false;
//            public bool One => true;
//            public bool Two => throw new OverflowException("");
//            public bool Equal(bool a, bool b) => a == b;
//            public bool GreaterThan(bool a, bool b) => a.CompareTo(b) == 1;
//            public bool LessThan(bool a, bool b) => a.CompareTo(b) == -1;
//            public bool GreaterThanEqual(bool a, bool b) => a.CompareTo(b) != -1;
//            public bool LessThanEqual(bool a, bool b) => a.CompareTo(b) != 1;
//            public string ToString(bool a) => a.ToString();
//            public bool TryParse(object value) 
//            {
//                switch (value)
//                {
//                    case string stringValue:
//                        var parsed = bool.TryParse(stringValue, out var parsedResult);
//                        if (parsed)
//                        {
//                            return parsedResult;
//                        }
//
//                        parsed = int.TryParse(stringValue, out var numberResult);
//                        if (parsed)
//                        {
//                            return Convert.ToBoolean(numberResult);
//                        }
//                        else
//                        {
//                            throw new FormatException("String was not recognized as a valid boolean");
//                        }
//
//                    default:
//                        return Convert.ToBoolean(value);
//                }            
//            }
//            DataType.ETypeCode IOperations<bool>.TypeCode => DataType.ETypeCode.Boolean;
//            public Func<bool, bool, bool> AddTest => ((a, b) => throw new OverflowException("Can not add a boolean."));
//        }
//        
//        class ByteOperations : IOperations<byte>
//        {
//            public byte Add(byte a, byte b) { return unchecked((byte)(a + b)); }
//            public byte Subtract(byte a, byte b) { return unchecked((byte)(a - b)); }
//            public byte Multiply(byte a, byte b) { return unchecked((byte)(a * b)); }
//            public byte Divide(byte a, byte b) { return unchecked((byte)(a / b)); }
//            public int Sign(byte a) { return 1; }
//            public byte Negate(byte a) { throw new OverflowException("Can not negate an unsigned number."); }
//            public byte Zero => 0;
//            public byte One => 1;
//            public byte Two => 2;
//            public bool Equal(byte a, byte b) => a == b;
//            public bool GreaterThan(byte a, byte b) => a > b;
//            public bool LessThan(byte a, byte b) => a < b;
//            public bool GreaterThanEqual(byte a, byte b) => a >= b;
//            public bool LessThanEqual(byte a, byte b) => a <= b;
//            public string ToString(byte a) => a.ToString();
//            public byte TryParse(object value) => Convert.ToByte(value);
//            DataType.ETypeCode IOperations<byte>.TypeCode => DataType.ETypeCode.Byte;
//            public Func<byte, byte, byte> AddTest => ((a, b) => throw new OverflowException("Can not add a boolean."));
//
//        }
//
//        class DoubleOperations : IOperations<double>
//        {
//            public double Add(double a, double b) { return a + b; }
//            public double Subtract(double a, double b) { return a - b; }
//            public double Multiply(double a, double b) { return a * b; }
//            public double Divide(double a, double b) { return a / b; }
//            public int Sign(double a) { return Math.Sign(a); }
//            public double Negate(double a) { return a * -1; }       
//            public double Zero => 0;
//            public double One => 1;
//            public double Two => 2;
//            public bool Equal(double a, double b) => a == b;
//            public bool GreaterThan(double a, double b) => a > b;
//            public bool LessThan(double a, double b) => a < b;
//            public bool GreaterThanEqual(double a, double b) => a >= b;
//            public bool LessThanEqual(double a, double b) => a <= b;
//            public string ToString(double a) => a.ToString();
//            public double TryParse(object value) => Convert.ToDouble(value);
//            DataType.ETypeCode IOperations<double>.TypeCode => DataType.ETypeCode.Double;
//            public Func<double, double, double> AddTest => ((a, b) => throw new OverflowException("Can not add a boolean."));
//        }
//        
//        class SingleOperations : IOperations<float>
//        {
//            public float Add(float a, float b) { return a + b; }
//            public float Subtract(float a, float b) { return a - b; }
//            public float Multiply(float a, float b) { return a * b; }
//            public float Divide(float a, float b) { return a / b; }
//            public int Sign(float a) { return Math.Sign(a); }
//            public float Negate(float a) { return a * -1; }    
//            public float Zero => 0;
//            public float One => 1;
//            public float Two => 2;
//            public bool Equal(float a, float b) => a == b;
//            public bool GreaterThan(float a, float b) => a > b;
//            public bool LessThan(float a, float b) => a < b;
//            public bool GreaterThanEqual(float a, float b) => a >= b;
//            public bool LessThanEqual(float a, float b) => a <= b;
//            public string ToString(float a) => a.ToString();
//            public float TryParse(object value) => Convert.ToSingle(value);
//            DataType.ETypeCode IOperations<float>.TypeCode => DataType.ETypeCode.Single;
//            public Func<float, float, float> AddTest => ((a, b) => throw new OverflowException("Can not add a boolean."));
//        }
//        
//        class DecimalOperations : IOperations<decimal>
//        {
//            public decimal Add(decimal a, decimal b) { return a + b; }
//            public decimal Subtract(decimal a, decimal b) { return a - b; }
//            public decimal Multiply(decimal a, decimal b) { return a * b; }
//            public decimal Divide(decimal a, decimal b) { return a / b; }
//            public int Sign(decimal a) { return Math.Sign(a); }
//            public decimal Negate(decimal a) { return a * -1; }       
//            public decimal Zero => 0;
//            public decimal One => 1;
//            public decimal Two => 2;
//            public bool Equal(decimal a, decimal b) => a == b;
//            public bool GreaterThan(decimal a, decimal b) => a > b;
//            public bool LessThan(decimal a, decimal b) => a < b;
//            public bool GreaterThanEqual(decimal a, decimal b) => a >= b;
//            public bool LessThanEqual(decimal a, decimal b) => a <= b;
//            public string ToString(decimal a) => a.ToString();
//            public decimal TryParse(object value) => Convert.ToDecimal(value);
//            DataType.ETypeCode IOperations<decimal>.TypeCode => DataType.ETypeCode.Decimal;
//            public Func<decimal, decimal, decimal> AddTest => ((a, b) => throw new OverflowException("Can not add a boolean."));
//        }
//        
//        class Int16Operations : IOperations<short>
//        {
//            public short Add(short a, short b) { return unchecked((short)(a + b)); }
//            public short Subtract(short a, short b) { return unchecked((short)(a - b)); }
//            public short Multiply(short a, short b) { return unchecked((short)(a * b)); }
//            public short Divide(short a, short b) { return unchecked((short)(a / b)); }
//            public int Sign(short a) { return Math.Sign(a); }
//            public short Negate(short a) { return unchecked((short)(a * -1)); }       
//            public short Zero => 0;
//            public short One => 1;
//            public short Two => 2;
//            public bool Equal(short a, short b) => a == b;
//            public bool GreaterThan(short a, short b) => a > b;
//            public bool LessThan(short a, short b) => a < b;
//            public bool GreaterThanEqual(short a, short b) => a >= b;
//            public bool LessThanEqual(short a, short b) => a <= b;
//            public string ToString(short a) => a.ToString();
//            public short TryParse(object value) => Convert.ToInt16(value);
//            DataType.ETypeCode IOperations<short>.TypeCode => DataType.ETypeCode.Int16;
//            public Func<short, short, short> AddTest => ((a, b) => throw new OverflowException("Can not add a boolean."));
//        }
//        
//        class Int32Operations : IOperations<int>
//        {
//            public int Add(int a, int b) { return a + b; }
//            public int Subtract(int a, int b) { return a - b; }
//            public int Multiply(int a, int b) { return a * b; }
//            public int Divide(int a, int b) { return a / b; }
//            public int Sign(int a) { return Math.Sign(a); }
//            public int Negate(int a) { return a * -1; }       
//            public int Zero => 0;
//            public int One => 1;
//            public int Two => 2;
//            public bool Equal(int a, int b) => a == b;
//            public bool GreaterThan(int a, int b) => a > b;
//            public bool LessThan(int a, int b) => a < b;
//            public bool GreaterThanEqual(int a, int b) => a >= b;
//            public bool LessThanEqual(int a, int b) => a <= b;
//            public string ToString(int a) => a.ToString();
//            public int TryParse(object value) => Convert.ToInt32(value);
//            DataType.ETypeCode IOperations<int>.TypeCode => DataType.ETypeCode.Int32;
//            public Func<int, int, int> AddTest => (a,b) => a+b;
//        }
//        
//        class Int64Operations : IOperations<long>
//        {
//            public long Add(long a, long b) { return a + b; }
//            public long Subtract(long a, long b) { return a - b; }
//            public long Multiply(long a, long b) { return a * b; }
//            public long Divide(long a, long b) { return a / b; }
//            public int Sign(long a) { return Math.Sign(a); }
//            public long Negate(long a) { return a * -1; }       
//            public long Zero => 0;
//            public long One => 1;
//            public long Two => 2;
//            public bool Equal(long a, long b) => a == b;
//            public bool GreaterThan(long a, long b) => a > b;
//            public bool LessThan(long a, long b) => a < b;
//            public bool GreaterThanEqual(long a, long b) => a >= b;
//            public bool LessThanEqual(long a, long b) => a <= b;
//            public string ToString(long a) => a.ToString();
//            public long TryParse(object value) => Convert.ToInt64(value);
//            DataType.ETypeCode IOperations<long>.TypeCode => DataType.ETypeCode.Int64;
//            public Func<long, long, long> AddTest => ((a, b) => throw new OverflowException("Can not add a boolean."));
//        }
//        
//        class UInt16Operations : IOperations<ushort>
//        {
//            public ushort Add(ushort a, ushort b) { return unchecked((ushort)(a + b)); }
//            public ushort Subtract(ushort a, ushort b) { return unchecked((ushort)(a - b)); }
//            public ushort Multiply(ushort a, ushort b) { return unchecked((ushort)(a * b)); }
//            public ushort Divide(ushort a, ushort b) { return unchecked((ushort)(a / b)); }
//            public int Sign(ushort a) { return 1; }
//            public ushort Negate(ushort a) { throw new OverflowException("Can not negate an unsigned number."); }       
//            public ushort Zero => 0;
//            public ushort One => 1;
//            public ushort Two => 2;
//            public bool Equal(ushort a, ushort b) => a == b;
//            public bool GreaterThan(ushort a, ushort b) => a > b;
//            public bool LessThan(ushort a, ushort b) => a < b;
//            public bool GreaterThanEqual(ushort a, ushort b) => a >= b;
//            public bool LessThanEqual(ushort a, ushort b) => a <= b;
//            public string ToString(ushort a) => a.ToString();
//            public ushort TryParse(object value) => Convert.ToUInt16(value);
//            DataType.ETypeCode IOperations<ushort>.TypeCode => DataType.ETypeCode.UInt16;
//            public Func<ushort, ushort, ushort> AddTest => ((a, b) => throw new OverflowException("Can not add a boolean."));
//        }
//        
//        class UInt32Operations : IOperations<uint>
//        {
//            public uint Add(uint a, uint b) { return a + b; }
//            public uint Subtract(uint a, uint b) { return a - b; }
//            public uint Multiply(uint a, uint b) { return a * b; }
//            public uint Divide(uint a, uint b) { return a / b; }
//            public int Sign(uint a) { return 1; }
//            public uint Negate(uint a) { throw new OverflowException("Can not negate an unsigned number."); }       
//            public uint Zero => 0;
//            public uint One => 1;
//            public uint Two => 2;
//            public bool Equal(uint a, uint b) => a == b;
//            public bool GreaterThan(uint a, uint b) => a > b;
//            public bool LessThan(uint a, uint b) => a < b;
//            public bool GreaterThanEqual(uint a, uint b) => a >= b;
//            public bool LessThanEqual(uint a, uint b) => a <= b;
//            public string ToString(uint a) => a.ToString();
//            public uint TryParse(object value) => Convert.ToUInt32(value);
//            DataType.ETypeCode IOperations<uint>.TypeCode => DataType.ETypeCode.UInt32;
//            public Func<uint, uint, uint> AddTest => ((a, b) => throw new OverflowException("Can not add a boolean."));
//        }
//        
//        class UInt64Operations : IOperations<ulong>
//        {
//            public ulong Add(ulong a, ulong b) { return a + b; }
//            public ulong Subtract(ulong a, ulong b) { return a - b; }
//            public ulong Multiply(ulong a, ulong b) { return a * b; }
//            public ulong Divide(ulong a, ulong b) { return a / b; }
//            public int Sign(ulong a) { return 1; }
//            public ulong Negate(ulong a) { throw new OverflowException("Can not negate an unsigned number."); }       
//            public ulong Zero => 0;
//            public ulong One => 1;
//            public ulong Two => 2;
//            public bool Equal(ulong a, ulong b) => a == b;
//            public bool GreaterThan(ulong a, ulong b) => a > b;
//            public bool LessThan(ulong a, ulong b) => a < b;
//            public bool GreaterThanEqual(ulong a, ulong b) => a >= b;
//            public bool LessThanEqual(ulong a, ulong b) => a <= b;
//            public string ToString(ulong a) => a.ToString();
//            public ulong TryParse(object value) => Convert.ToUInt64(value);
//            DataType.ETypeCode IOperations<ulong>.TypeCode => DataType.ETypeCode.UInt64;
//            public Func<ulong, ulong, ulong> AddTest => ((a, b) => throw new OverflowException("Can not add a boolean."));
//        }
//        
//        class SByteOperations : IOperations<sbyte>
//        {
//            public sbyte Add(sbyte a, sbyte b) { return unchecked((sbyte)(a + b)); }
//            public sbyte Subtract(sbyte a, sbyte b) { return unchecked((sbyte)(a - b)); }
//            public sbyte Multiply(sbyte a, sbyte b) { return unchecked((sbyte)(a * b)); }
//            public sbyte Divide(sbyte a, sbyte b) { return unchecked((sbyte)(a / b)); }
//            public int Sign(sbyte a) { return Math.Sign(a); }
//            public sbyte Negate(sbyte a) { return unchecked((sbyte)(a * -1)); }       
//            public sbyte Zero => 0;
//            public sbyte One => 1;
//            public sbyte Two => 2;
//            public bool Equal(sbyte a, sbyte b) => a == b;
//            public bool GreaterThan(sbyte a, sbyte b) => a > b;
//            public bool LessThan(sbyte a, sbyte b) => a < b;
//            public bool GreaterThanEqual(sbyte a, sbyte b) => a >= b;
//            public bool LessThanEqual(sbyte a, sbyte b) => a <= b;
//            public string ToString(sbyte a) => a.ToString();
//            public sbyte TryParse(object value) => Convert.ToSByte(value);
//            DataType.ETypeCode IOperations<sbyte>.TypeCode => DataType.ETypeCode.SByte;
//            public Func<sbyte, sbyte, sbyte> AddTest => ((a, b) => throw new OverflowException("Can not add a boolean."));
//        }
//        
//        class DateTimeOperations : IOperations<DateTime>
//        {
//            public DateTime Add(DateTime a, DateTime b) => throw new OverflowException("Can not add a dates.");
//            public DateTime Subtract(DateTime a, DateTime b)  => throw new OverflowException("Can not subtract a dates.");
//            public DateTime Multiply(DateTime a, DateTime b)  => throw new OverflowException("Can not multiply a dates.");
//            public DateTime Divide(DateTime a, DateTime b)  => throw new OverflowException("Can not divide a dates.");
//            public int Sign(DateTime a)  => throw new OverflowException("Can not get the sign of a dates.");
//            public DateTime Negate(DateTime a)  => throw new OverflowException("Can not negate a dates.");    
//            public DateTime Zero => throw new OverflowException("");
//            public DateTime One => throw new OverflowException("");
//            public DateTime Two => throw new OverflowException("");
//            public bool Equal(DateTime a, DateTime b) => a == b;
//            public bool GreaterThan(DateTime a, DateTime b) => a > b;
//            public bool LessThan(DateTime a, DateTime b) => a < b;
//            public bool GreaterThanEqual(DateTime a, DateTime b) => a >= b;
//            public bool LessThanEqual(DateTime a, DateTime b) => a <= b;
//            public string ToString(DateTime a) => a.ToString();
//            public DateTime TryParse(object value) => Convert.ToDateTime(value);
//            DataType.ETypeCode IOperations<DateTime>.TypeCode => DataType.ETypeCode.DateTime;
//            public Func<DateTime, DateTime, DateTime> AddTest => ((a, b) => throw new OverflowException("Can not add a boolean."));
//        }
//        
//        class TimeOperations : IOperations<TimeSpan>
//        {
//            public TimeSpan Add(TimeSpan a, TimeSpan b) => throw new OverflowException("Can not add a dates.");
//            public TimeSpan Subtract(TimeSpan a, TimeSpan b)  => throw new OverflowException("Can not subtract a dates.");
//            public TimeSpan Multiply(TimeSpan a, TimeSpan b)  => throw new OverflowException("Can not multiply a dates.");
//            public TimeSpan Divide(TimeSpan a, TimeSpan b)  => throw new OverflowException("Can not divide a dates.");
//            public int Sign(TimeSpan a)  => throw new OverflowException("Can not get the sign of a dates.");
//            public TimeSpan Negate(TimeSpan a)  => throw new OverflowException("Can not negate a dates.");    
//            public TimeSpan Zero => throw new OverflowException("");
//            public TimeSpan One => throw new OverflowException("");
//            public TimeSpan Two => throw new OverflowException("");
//            public bool Equal(TimeSpan a, TimeSpan b) => a == b;
//            public bool GreaterThan(TimeSpan a, TimeSpan b) => a > b;
//            public bool LessThan(TimeSpan a, TimeSpan b) => a < b;
//            public bool GreaterThanEqual(TimeSpan a, TimeSpan b) => a >= b;
//            public bool LessThanEqual(TimeSpan a, TimeSpan b) => a <= b;
//            public string ToString(TimeSpan a) => a.ToString();
//            public TimeSpan TryParse(object value)
//            {
//                if (value is TimeSpan timeSpan)
//                {
//                    return timeSpan;
//                }
//                return TimeSpan.Parse(value.ToString());
//            } 
//            DataType.ETypeCode IOperations<TimeSpan>.TypeCode => DataType.ETypeCode.Time;
//            public Func<TimeSpan, TimeSpan, TimeSpan> AddTest => ((a, b) => throw new OverflowException("Can not add a boolean."));
//        }
//        
//        class GuidOperations : IOperations<Guid>
//        {
//            public Guid Add(Guid a, Guid b) => throw new OverflowException("Can not add a dates.");
//            public Guid Subtract(Guid a, Guid b)  => throw new OverflowException("Can not subtract a dates.");
//            public Guid Multiply(Guid a, Guid b)  => throw new OverflowException("Can not multiply a dates.");
//            public Guid Divide(Guid a, Guid b)  => throw new OverflowException("Can not divide a dates.");
//            public int Sign(Guid a)  => throw new OverflowException("Can not get the sign of a dates.");
//            public Guid Negate(Guid a)  => throw new OverflowException("Can not negate a dates.");    
//            public Guid Zero  => throw new OverflowException("");
//            public Guid One => throw new OverflowException("");
//            public Guid Two => throw new OverflowException("");
//            public bool Equal(Guid a, Guid b) => a == b;
//            public bool GreaterThan(Guid a, Guid b) => a.CompareTo(b) == 1;
//            public bool LessThan(Guid a, Guid b) => a.CompareTo(b) == -1;
//            public bool GreaterThanEqual(Guid a, Guid b) => a.CompareTo(b) >= 0;
//            public bool LessThanEqual(Guid a, Guid b) => a.CompareTo(b) <= 1;
//            public string ToString(Guid a) => a.ToString();
//            public Guid TryParse(object value) => Guid.Parse(value.ToString());
//            DataType.ETypeCode IOperations<Guid>.TypeCode => DataType.ETypeCode.Guid;
//            public Func<Guid, Guid, Guid> AddTest => ((a, b) => throw new OverflowException("Can not add a boolean."));
//        }
//        
//        class ByteArrayOperations : IOperations<byte[]>
//        {
//            public byte[] Add(byte[] a, byte[] b) => throw new OverflowException("Can not add a dates.");
//            public byte[] Subtract(byte[] a, byte[] b)  => throw new OverflowException("Can not subtract a dates.");
//            public byte[] Multiply(byte[] a, byte[] b)  => throw new OverflowException("Can not multiply a dates.");
//            public byte[] Divide(byte[] a, byte[] b)  => throw new OverflowException("Can not divide a dates.");
//            public int Sign(byte[] a)  => throw new OverflowException("Can not get the sign of a dates.");
//            public byte[] Negate(byte[] a)  => throw new OverflowException("Can not negate a dates.");    
//            public byte[] Zero => throw new OverflowException("");
//            public byte[] One => throw new OverflowException("");
//            public byte[] Two => throw new OverflowException("");
//            public bool Equal(byte[] a, byte[] b) => a.SequenceEqual(b);
//            public bool GreaterThan(byte[] a, byte[] b)
//            {
//                for (var i = 0; i < a.Length; i++)
//                {
//                    if (i > b.Length) return false;
//                    if (a[i] > b[i]) return true;
//                    if (a[i] < b[i]) return false;
//                }
//
//                return a.Length < b.Length;
//            }
//            public bool LessThan(byte[] a, byte[] b)
//            {
//                for (var i = 0; i < a.Length; i++)
//                {
//                    if (i > b.Length) return true;
//                    if (a[i] > b[i]) return false;
//                    if (a[i] < b[i]) return true;
//                }
//
//                return a.Length > b.Length;
//            }
//
//            public bool GreaterThanEqual(byte[] a, byte[] b)
//            {
//                for (var i = 0; i < a.Length; i++)
//                {
//                    if (i > b.Length) return false;
//                    if (a[i] > b[i]) return true;
//                    if (a[i] < b[i]) return false;
//                }
//
//                return a.Length <= b.Length;
//            }
//
//            public bool LessThanEqual(byte[] a, byte[] b)
//            {
//                for (var i = 0; i < a.Length; i++)
//                {
//                    if (i > b.Length) return true;
//                    if (a[i] > b[i]) return true;
//                    if (a[i] < b[i]) return false;
//                }
//
//                return a.Length >= b.Length;
//            }
//            public string ToString(byte[] a) => a.ToString();
//            public byte[] TryParse(object value)
//            {
//                if (value is byte[] bytes) return bytes;
//                if (value is string valueString) return DataType.HexToByteArray(valueString);
//                throw new DataTypeParseException($"Binary type conversion not supported on type {value.GetType()} .");
//            }
//            DataType.ETypeCode IOperations<byte[]>.TypeCode => DataType.ETypeCode.Binary;
//            public Func<byte[], byte[], byte[]> AddTest => ((a, b) => throw new OverflowException("Can not add a boolean."));
//        }
//        
//        class CharArrayOperations : IOperations<char[]>
//        {
//            public char[] Add(char[] a, char[] b) => throw new OverflowException("Can not add a chars.");
//            public char[] Subtract(char[] a, char[] b)  => throw new OverflowException("Can not subtract chars.");
//            public char[] Multiply(char[] a, char[] b)  => throw new OverflowException("Can not multiply chars.");
//            public char[] Divide(char[] a, char[] b)  => throw new OverflowException("Can not divide chars.");
//            public int Sign(char[] a)  => throw new OverflowException("Can not get the sign of chars.");
//            public char[] Negate(char[] a)  => throw new OverflowException("Can not negate a dates.");    
//            public char[] Zero => "0".ToCharArray();
//            public char[] One =>  "1".ToCharArray();
//            public char[] Two => "2".ToCharArray();
//            public bool Equal(char[] a, char[] b) => a.SequenceEqual(b);
//
//            public bool GreaterThan(char[] a, char[] b)
//            {
//                for (var i = 0; i < a.Length; i++)
//                {
//                    if (i >= b.Length) return false;
//                    if (a[i] > b[i]) return true;
//                    if (a[i] < b[i]) return false;
//                }
//
//                return a.Length < b.Length;
//            }
//            public bool LessThan(char[] a, char[] b)
//            {
//                for (var i = 0; i < a.Length; i++)
//                {
//                    if (i >= b.Length) return true;
//                    if (a[i] > b[i]) return false;
//                    if (a[i] < b[i]) return true;
//                }
//
//                return a.Length > b.Length;
//            }
//
//            public bool GreaterThanEqual(char[] a, char[] b)
//            {
//                for (var i = 0; i < a.Length; i++)
//                {
//                    if (i >= b.Length) return false;
//                    if (a[i] > b[i]) return true;
//                    if (a[i] < b[i]) return false;
//                }
//
//                return a.Length <= b.Length;
//            }
//
//            public bool LessThanEqual(char[] a, char[] b)
//            {
//                for (var i = 0; i < a.Length; i++)
//                {
//                    if (i >= b.Length) return true;
//                    if (a[i] > b[i]) return true;
//                    if (a[i] < b[i]) return false;
//                }
//
//                return a.Length >= b.Length;
//            }
//            public string ToString(char[] a) => a.ToString();
//
//            public char[] TryParse(object value)
//            {
//                if (value is char[] chars) return chars;
//                return value.ToString().ToCharArray();
//            }
//            DataType.ETypeCode IOperations<char[]>.TypeCode => DataType.ETypeCode.Char;
//           
//            public Func<char[], char[], char[]> AddTest => ((a, b) => throw new OverflowException("Can not add a boolean."));
//        }
//        
//        class JsonOperations : IOperations<JToken>
//        {
//            public JToken Add(JToken a, JToken b) => throw new OverflowException("Can not add a dates.");
//            public JToken Subtract(JToken a, JToken b)  => throw new OverflowException("Can not subtract a dates.");
//            public JToken Multiply(JToken a, JToken b)  => throw new OverflowException("Can not multiply a dates.");
//            public JToken Divide(JToken a, JToken b)  => throw new OverflowException("Can not divide a dates.");
//            public int Sign(JToken a)  => throw new OverflowException("Can not get the sign of a dates.");
//            public JToken Negate(JToken a)  => throw new OverflowException("Can not negate a dates.");    
//            public JToken Zero  => throw new OverflowException("");
//            public JToken One  => throw new OverflowException("");
//            public JToken Two  => throw new OverflowException("");
//            public bool Equal(JToken a, JToken b) => throw new OverflowException("Can not compare json.");
//            public bool GreaterThan(JToken a, JToken b) => throw new OverflowException("Can not compare json.");
//            public bool LessThan(JToken a, JToken b) => throw new OverflowException("Can not compare json.");
//            public bool GreaterThanEqual(JToken a, JToken b) => throw new OverflowException("Can not compare json.");
//            public bool LessThanEqual(JToken a, JToken b) => throw new OverflowException("Can not compare json.");
//            public string ToString(JToken a) => a.ToString();
//            public JToken TryParse(object value)
//            {
//                switch (value)
//                {
//                    case JToken jToken:
//                        return jToken;
//                    case string stringValue:
//                        return JToken.Parse(stringValue);
//                    default:
//                        throw new DataTypeParseException($"The value is not a valid json string.");
//                }
//            }
//            DataType.ETypeCode IOperations<JToken>.TypeCode => DataType.ETypeCode.Json;
//            public Func<JToken, JToken, JToken> AddTest => ((a, b) => throw new OverflowException("Can not add a boolean."));
//        }
//        
//        class XmlOperations : IOperations<XmlDocument>
//        {
//            public XmlDocument Add(XmlDocument a, XmlDocument b) => throw new OverflowException("Can not add a dates.");
//            public XmlDocument Subtract(XmlDocument a, XmlDocument b)  => throw new OverflowException("Can not subtract a dates.");
//            public XmlDocument Multiply(XmlDocument a, XmlDocument b)  => throw new OverflowException("Can not multiply a dates.");
//            public XmlDocument Divide(XmlDocument a, XmlDocument b)  => throw new OverflowException("Can not divide a dates.");
//            public int Sign(XmlDocument a)  => throw new OverflowException("Can not get the sign of a dates.");
//            public XmlDocument Negate(XmlDocument a)  => throw new OverflowException("Can not negate a dates.");    
//            public XmlDocument Zero => throw new OverflowException("");
//            public XmlDocument One => throw new OverflowException("");
//            public XmlDocument Two => throw new OverflowException("");
//            public bool Equal(XmlDocument a, XmlDocument b) => throw new OverflowException("Can not compare xml.");
//            public bool GreaterThan(XmlDocument a, XmlDocument b) => throw new OverflowException("Can not compare xml.");
//            public bool LessThan(XmlDocument a, XmlDocument b) => throw new OverflowException("Can not compare xml.");
//            public bool GreaterThanEqual(XmlDocument a, XmlDocument b) => throw new OverflowException("Can not compare xml.");
//            public bool LessThanEqual(XmlDocument a, XmlDocument b) => throw new OverflowException("Can not compare xml.");
//            public string ToString(XmlDocument a) => a.ToString();
//            public XmlDocument TryParse(object value)
//            {
//                switch (value)
//                {
//                    case XmlDocument xmlDocument:
//                        return xmlDocument;
//                    case string stringValue:
//                        var xmlDocument1 = new XmlDocument();
//                        xmlDocument1.LoadXml(stringValue);
//                        return xmlDocument1;
//                    default:
//                        throw new DataTypeParseException($"The value is not a valid xml string.");
//                }
//            } 
//            DataType.ETypeCode IOperations<XmlDocument>.TypeCode => DataType.ETypeCode.Xml;
//            public Func<XmlDocument, XmlDocument, XmlDocument> AddTest => ((a, b) => throw new OverflowException("Can not add a boolean."));
//        }
//        
//        class StringOperations : IOperations<String>
//        {
//            public String Add(String a, String b) => throw new OverflowException("Can not add a dates.");
//            public String Subtract(String a, String b)  => throw new OverflowException("Can not subtract a dates.");
//            public String Multiply(String a, String b)  => throw new OverflowException("Can not multiply a dates.");
//            public String Divide(String a, String b)  => throw new OverflowException("Can not divide a dates.");
//            public int Sign(String a)  => throw new OverflowException("Can not get the sign of a dates.");
//            public String Negate(String a)  => throw new OverflowException("Can not negate a dates.");    
//            public String Zero => "0";
//            public String One => "1";
//            public String Two => "2";
//            public bool Equal(String a, String b) => a == b;
//            public bool GreaterThan(String a, String b) => String.Compare(a,b) == 1;
//            public bool LessThan(String a, String b) => String.Compare(a,b) == -1;
//            public bool GreaterThanEqual(String a, String b) => String.Compare(a,b) >= 0;
//            public bool LessThanEqual(String a, String b) => String.Compare(a,b) <= 0;
//            public string ToString(String a) => a;
//            public String TryParse(object value)
//            {
//                switch (value)
//                {
//                    case string stringValue:
//                        return stringValue;
//                    case byte[] byteValue:
//                        return DataType.ByteArrayToHex(byteValue);
//                    case char[] charValue:
//                        return new string(charValue);
//                }
//
//                return value.ToString();
//            }
//            DataType.ETypeCode IOperations<String>.TypeCode => DataType.ETypeCode.String;
//            public Func<String, String, String> AddTest => ((a, b) => throw new OverflowException("Can not add a boolean."));
//        }
        
        
    }
}