using System;
using System.Linq;
using System.Net;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace Dexih.Utils.DataType
{
    public class Operations<T>
    {
        public static IOperations<T> Default => Create();

        static IOperations<T> Create()
        {
            var dataType = typeof(T); 
            switch (Type.GetTypeCode(dataType))
            {
                case TypeCode.Byte:
                    return (IOperations<T>) new ByteOperations();
                case TypeCode.Decimal:
                    return (IOperations<T>) new DecimalOperations();
                case TypeCode.Double:
                    return (IOperations<T>) new DoubleOperations();
                case TypeCode.Int16:
                    return (IOperations<T>) new Int16Operations();
                case TypeCode.Int32:
                    return (IOperations<T>) new Int32Operations();
                case TypeCode.Int64:
                    return (IOperations<T>) new Int64Operations();
                case TypeCode.SByte:
                    return (IOperations<T>) new SByteOperations();
                case TypeCode.Single:
                    return (IOperations<T>) new SingleOperations();
                case TypeCode.UInt16:
                    return (IOperations<T>) new UInt16Operations();
                case TypeCode.UInt32:
                    return (IOperations<T>) new UInt32Operations();
                case TypeCode.UInt64:
                    return (IOperations<T>) new UInt64Operations();
                case TypeCode.Boolean:
                    return (IOperations<T>) new BooleanOperations();
                case TypeCode.DateTime:
                    return (IOperations<T>) new DateTimeOperations();
                case TypeCode.DBNull:
                    break;
                case TypeCode.Object:
                    if (dataType == typeof(TimeSpan) || dataType == typeof(TimeSpan?)) return (IOperations<T>) new TimeOperations();
                    if (dataType == typeof(Guid) || dataType == typeof(Guid?)) return (IOperations<T>) new GuidOperations();
                    if (dataType == typeof(byte[])) return (IOperations<T>) new ByteArrayOperations();
                    if (dataType == typeof(char[])) return (IOperations<T>) new CharArrayOperations();
                    if (dataType == typeof(JToken)) return (IOperations<T>) new JsonOperations();
                    if (dataType == typeof(XmlDocument)) return (IOperations<T>) new XmlOperations();
                    break;
                case TypeCode.String:
                    return (IOperations<T>) new StringOperations();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            throw new DataTypeException($"The datatype {typeof(T)} is not a valid type for arithmetic.");
        }

        class BooleanOperations : IOperations<bool>
        {
            public bool Add(bool a, bool b) => throw new OverflowException("Can not add a boolean.");
            public bool Subtract(bool a, bool b)  => throw new OverflowException("Can not subtract a boolean.");
            public bool Multiply(bool a, bool b)  => throw new OverflowException("Can not multiply a boolean.");
            public bool Divide(bool a, bool b)  => throw new OverflowException("Can not divide a boolean.");
            public int Sign(bool a)  => throw new OverflowException("Can not get the sign of a boolean.");
            public bool Negate(bool a)  => throw new OverflowException("Can not negate a boolean.");    
            public bool Equal(bool a, bool b) => a == b;
            public bool GreaterThan(bool a, bool b) => a.CompareTo(b) == 1;
            public bool LessThan(bool a, bool b) => a.CompareTo(b) == -1;
            public bool GreaterThanEqual(bool a, bool b) => a.CompareTo(b) != -1;
            public bool LessThanEqual(bool a, bool b) => a.CompareTo(b) != 1;
            public string ToString(bool a) => a.ToString();
            public bool TryParse(object value) 
            {
                switch (value)
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
                            throw new FormatException("String was not recognized as a valid boolean");
                        }

                    default:
                        return Convert.ToBoolean(value);
                }            
            }
            DataType.ETypeCode IOperations<bool>.TypeCode => DataType.ETypeCode.Boolean;
        }
        
        class ByteOperations : IOperations<byte>
        {
            public byte Add(byte a, byte b) { return unchecked((byte)(a + b)); }
            public byte Subtract(byte a, byte b) { return unchecked((byte)(a - b)); }
            public byte Multiply(byte a, byte b) { return unchecked((byte)(a * b)); }
            public byte Divide(byte a, byte b) { return unchecked((byte)(a / b)); }
            public int Sign(byte a) { return 1; }
            public byte Negate(byte a) { throw new OverflowException("Can not negate an unsigned number."); }
            public bool Equal(byte a, byte b) => a == b;
            public bool GreaterThan(byte a, byte b) => a > b;
            public bool LessThan(byte a, byte b) => a < b;
            public bool GreaterThanEqual(byte a, byte b) => a >= b;
            public bool LessThanEqual(byte a, byte b) => a <= b;
            public string ToString(byte a) => a.ToString();
            public byte TryParse(object value) => Convert.ToByte(value);
            DataType.ETypeCode IOperations<byte>.TypeCode => DataType.ETypeCode.Byte;
        }

        class DoubleOperations : IOperations<double>
        {
            public double Add(double a, double b) { return a + b; }
            public double Subtract(double a, double b) { return a - b; }
            public double Multiply(double a, double b) { return a * b; }
            public double Divide(double a, double b) { return a / b; }
            public int Sign(double a) { return Math.Sign(a); }
            public double Negate(double a) { return a * -1; }       
            public bool Equal(double a, double b) => a == b;
            public bool GreaterThan(double a, double b) => a > b;
            public bool LessThan(double a, double b) => a < b;
            public bool GreaterThanEqual(double a, double b) => a >= b;
            public bool LessThanEqual(double a, double b) => a <= b;
            public string ToString(double a) => a.ToString();
            public double TryParse(object value) => Convert.ToDouble(value);
            DataType.ETypeCode IOperations<double>.TypeCode => DataType.ETypeCode.Double;
        }
        
        class SingleOperations : IOperations<float>
        {
            public float Add(float a, float b) { return a + b; }
            public float Subtract(float a, float b) { return a - b; }
            public float Multiply(float a, float b) { return a * b; }
            public float Divide(float a, float b) { return a / b; }
            public int Sign(float a) { return Math.Sign(a); }
            public float Negate(float a) { return a * -1; }       
            public bool Equal(float a, float b) => a == b;
            public bool GreaterThan(float a, float b) => a > b;
            public bool LessThan(float a, float b) => a < b;
            public bool GreaterThanEqual(float a, float b) => a >= b;
            public bool LessThanEqual(float a, float b) => a <= b;
            public string ToString(float a) => a.ToString();
            public float TryParse(object value) => Convert.ToSingle(value);
            DataType.ETypeCode IOperations<float>.TypeCode => DataType.ETypeCode.Single;
        }
        
        class DecimalOperations : IOperations<decimal>
        {
            public decimal Add(decimal a, decimal b) { return a + b; }
            public decimal Subtract(decimal a, decimal b) { return a - b; }
            public decimal Multiply(decimal a, decimal b) { return a * b; }
            public decimal Divide(decimal a, decimal b) { return a / b; }
            public int Sign(decimal a) { return Math.Sign(a); }
            public decimal Negate(decimal a) { return a * -1; }       
            public bool Equal(decimal a, decimal b) => a == b;
            public bool GreaterThan(decimal a, decimal b) => a > b;
            public bool LessThan(decimal a, decimal b) => a < b;
            public bool GreaterThanEqual(decimal a, decimal b) => a >= b;
            public bool LessThanEqual(decimal a, decimal b) => a <= b;
            public string ToString(decimal a) => a.ToString();
            public decimal TryParse(object value) => Convert.ToDecimal(value);
            DataType.ETypeCode IOperations<decimal>.TypeCode => DataType.ETypeCode.Decimal;
        }
        
        class Int16Operations : IOperations<short>
        {
            public short Add(short a, short b) { return unchecked((short)(a + b)); }
            public short Subtract(short a, short b) { return unchecked((short)(a - b)); }
            public short Multiply(short a, short b) { return unchecked((short)(a * b)); }
            public short Divide(short a, short b) { return unchecked((short)(a / b)); }
            public int Sign(short a) { return Math.Sign(a); }
            public short Negate(short a) { return unchecked((short)(a * -1)); }       
            public bool Equal(short a, short b) => a == b;
            public bool GreaterThan(short a, short b) => a > b;
            public bool LessThan(short a, short b) => a < b;
            public bool GreaterThanEqual(short a, short b) => a >= b;
            public bool LessThanEqual(short a, short b) => a <= b;
            public string ToString(short a) => a.ToString();
            public short TryParse(object value) => Convert.ToInt16(value);
            DataType.ETypeCode IOperations<short>.TypeCode => DataType.ETypeCode.Int16;
        }
        
        class Int32Operations : IOperations<int>
        {
            public int Add(int a, int b) { return a + b; }
            public int Subtract(int a, int b) { return a - b; }
            public int Multiply(int a, int b) { return a * b; }
            public int Divide(int a, int b) { return a / b; }
            public int Sign(int a) { return Math.Sign(a); }
            public int Negate(int a) { return a * -1; }       
            public bool Equal(int a, int b) => a == b;
            public bool GreaterThan(int a, int b) => a > b;
            public bool LessThan(int a, int b) => a < b;
            public bool GreaterThanEqual(int a, int b) => a >= b;
            public bool LessThanEqual(int a, int b) => a <= b;
            public string ToString(int a) => a.ToString();
            public int TryParse(object value) => Convert.ToInt32(value);
            DataType.ETypeCode IOperations<int>.TypeCode => DataType.ETypeCode.Int32;
        }
        
        class Int64Operations : IOperations<long>
        {
            public long Add(long a, long b) { return a + b; }
            public long Subtract(long a, long b) { return a - b; }
            public long Multiply(long a, long b) { return a * b; }
            public long Divide(long a, long b) { return a / b; }
            public int Sign(long a) { return Math.Sign(a); }
            public long Negate(long a) { return a * -1; }       
            public bool Equal(long a, long b) => a == b;
            public bool GreaterThan(long a, long b) => a > b;
            public bool LessThan(long a, long b) => a < b;
            public bool GreaterThanEqual(long a, long b) => a >= b;
            public bool LessThanEqual(long a, long b) => a <= b;
            public string ToString(long a) => a.ToString();
            public long TryParse(object value) => Convert.ToInt64(value);
            DataType.ETypeCode IOperations<long>.TypeCode => DataType.ETypeCode.Int64;
        }
        
        class UInt16Operations : IOperations<ushort>
        {
            public ushort Add(ushort a, ushort b) { return unchecked((ushort)(a + b)); }
            public ushort Subtract(ushort a, ushort b) { return unchecked((ushort)(a - b)); }
            public ushort Multiply(ushort a, ushort b) { return unchecked((ushort)(a * b)); }
            public ushort Divide(ushort a, ushort b) { return unchecked((ushort)(a / b)); }
            public int Sign(ushort a) { return 1; }
            public ushort Negate(ushort a) { throw new OverflowException("Can not negate an unsigned number."); }       
            public bool Equal(ushort a, ushort b) => a == b;
            public bool GreaterThan(ushort a, ushort b) => a > b;
            public bool LessThan(ushort a, ushort b) => a < b;
            public bool GreaterThanEqual(ushort a, ushort b) => a >= b;
            public bool LessThanEqual(ushort a, ushort b) => a <= b;
            public string ToString(ushort a) => a.ToString();
            public ushort TryParse(object value) => Convert.ToUInt16(value);
            DataType.ETypeCode IOperations<ushort>.TypeCode => DataType.ETypeCode.UInt16;
        }
        
        class UInt32Operations : IOperations<uint>
        {
            public uint Add(uint a, uint b) { return a + b; }
            public uint Subtract(uint a, uint b) { return a - b; }
            public uint Multiply(uint a, uint b) { return a * b; }
            public uint Divide(uint a, uint b) { return a / b; }
            public int Sign(uint a) { return 1; }
            public uint Negate(uint a) { throw new OverflowException("Can not negate an unsigned number."); }       
            public bool Equal(uint a, uint b) => a == b;
            public bool GreaterThan(uint a, uint b) => a > b;
            public bool LessThan(uint a, uint b) => a < b;
            public bool GreaterThanEqual(uint a, uint b) => a >= b;
            public bool LessThanEqual(uint a, uint b) => a <= b;
            public string ToString(uint a) => a.ToString();
            public uint TryParse(object value) => Convert.ToUInt32(value);
            DataType.ETypeCode IOperations<uint>.TypeCode => DataType.ETypeCode.UInt32;
        }
        
        class UInt64Operations : IOperations<ulong>
        {
            public ulong Add(ulong a, ulong b) { return a + b; }
            public ulong Subtract(ulong a, ulong b) { return a - b; }
            public ulong Multiply(ulong a, ulong b) { return a * b; }
            public ulong Divide(ulong a, ulong b) { return a / b; }
            public int Sign(ulong a) { return 1; }
            public ulong Negate(ulong a) { throw new OverflowException("Can not negate an unsigned number."); }       
            public bool Equal(ulong a, ulong b) => a == b;
            public bool GreaterThan(ulong a, ulong b) => a > b;
            public bool LessThan(ulong a, ulong b) => a < b;
            public bool GreaterThanEqual(ulong a, ulong b) => a >= b;
            public bool LessThanEqual(ulong a, ulong b) => a <= b;
            public string ToString(ulong a) => a.ToString();
            public ulong TryParse(object value) => Convert.ToUInt64(value);
            DataType.ETypeCode IOperations<ulong>.TypeCode => DataType.ETypeCode.UInt64;
        }
        
        class SByteOperations : IOperations<sbyte>
        {
            public sbyte Add(sbyte a, sbyte b) { return unchecked((sbyte)(a + b)); }
            public sbyte Subtract(sbyte a, sbyte b) { return unchecked((sbyte)(a - b)); }
            public sbyte Multiply(sbyte a, sbyte b) { return unchecked((sbyte)(a * b)); }
            public sbyte Divide(sbyte a, sbyte b) { return unchecked((sbyte)(a / b)); }
            public int Sign(sbyte a) { return Math.Sign(a); }
            public sbyte Negate(sbyte a) { return unchecked((sbyte)(a * -1)); }       
            public bool Equal(sbyte a, sbyte b) => a == b;
            public bool GreaterThan(sbyte a, sbyte b) => a > b;
            public bool LessThan(sbyte a, sbyte b) => a < b;
            public bool GreaterThanEqual(sbyte a, sbyte b) => a >= b;
            public bool LessThanEqual(sbyte a, sbyte b) => a <= b;
            public string ToString(sbyte a) => a.ToString();
            public sbyte TryParse(object value) => Convert.ToSByte(value);
            DataType.ETypeCode IOperations<sbyte>.TypeCode => DataType.ETypeCode.SByte;
        }
        
        class DateTimeOperations : IOperations<DateTime>
        {
            public DateTime Add(DateTime a, DateTime b) => throw new OverflowException("Can not add a dates.");
            public DateTime Subtract(DateTime a, DateTime b)  => throw new OverflowException("Can not subtract a dates.");
            public DateTime Multiply(DateTime a, DateTime b)  => throw new OverflowException("Can not multiply a dates.");
            public DateTime Divide(DateTime a, DateTime b)  => throw new OverflowException("Can not divide a dates.");
            public int Sign(DateTime a)  => throw new OverflowException("Can not get the sign of a dates.");
            public DateTime Negate(DateTime a)  => throw new OverflowException("Can not negate a dates.");    
            public bool Equal(DateTime a, DateTime b) => a == b;
            public bool GreaterThan(DateTime a, DateTime b) => a > b;
            public bool LessThan(DateTime a, DateTime b) => a < b;
            public bool GreaterThanEqual(DateTime a, DateTime b) => a >= b;
            public bool LessThanEqual(DateTime a, DateTime b) => a <= b;
            public string ToString(DateTime a) => a.ToString();
            public DateTime TryParse(object value) => Convert.ToDateTime(value);
            DataType.ETypeCode IOperations<DateTime>.TypeCode => DataType.ETypeCode.DateTime;
        }
        
        class TimeOperations : IOperations<TimeSpan>
        {
            public TimeSpan Add(TimeSpan a, TimeSpan b) => throw new OverflowException("Can not add a dates.");
            public TimeSpan Subtract(TimeSpan a, TimeSpan b)  => throw new OverflowException("Can not subtract a dates.");
            public TimeSpan Multiply(TimeSpan a, TimeSpan b)  => throw new OverflowException("Can not multiply a dates.");
            public TimeSpan Divide(TimeSpan a, TimeSpan b)  => throw new OverflowException("Can not divide a dates.");
            public int Sign(TimeSpan a)  => throw new OverflowException("Can not get the sign of a dates.");
            public TimeSpan Negate(TimeSpan a)  => throw new OverflowException("Can not negate a dates.");    
            public bool Equal(TimeSpan a, TimeSpan b) => a == b;
            public bool GreaterThan(TimeSpan a, TimeSpan b) => a > b;
            public bool LessThan(TimeSpan a, TimeSpan b) => a < b;
            public bool GreaterThanEqual(TimeSpan a, TimeSpan b) => a >= b;
            public bool LessThanEqual(TimeSpan a, TimeSpan b) => a <= b;
            public string ToString(TimeSpan a) => a.ToString();
            public TimeSpan TryParse(object value)
            {
                if (value is TimeSpan timeSpan)
                {
                    return timeSpan;
                }
                return TimeSpan.Parse(value.ToString());
            } 
            DataType.ETypeCode IOperations<TimeSpan>.TypeCode => DataType.ETypeCode.Time;
        }
        
        class GuidOperations : IOperations<Guid>
        {
            public Guid Add(Guid a, Guid b) => throw new OverflowException("Can not add a dates.");
            public Guid Subtract(Guid a, Guid b)  => throw new OverflowException("Can not subtract a dates.");
            public Guid Multiply(Guid a, Guid b)  => throw new OverflowException("Can not multiply a dates.");
            public Guid Divide(Guid a, Guid b)  => throw new OverflowException("Can not divide a dates.");
            public int Sign(Guid a)  => throw new OverflowException("Can not get the sign of a dates.");
            public Guid Negate(Guid a)  => throw new OverflowException("Can not negate a dates.");    
            public bool Equal(Guid a, Guid b) => a == b;
            public bool GreaterThan(Guid a, Guid b) => a.CompareTo(b) == 1;
            public bool LessThan(Guid a, Guid b) => a.CompareTo(b) == -1;
            public bool GreaterThanEqual(Guid a, Guid b) => a.CompareTo(b) >= 0;
            public bool LessThanEqual(Guid a, Guid b) => a.CompareTo(b) <= 1;
            public string ToString(Guid a) => a.ToString();
            public Guid TryParse(object value) => Guid.Parse(value.ToString());
            DataType.ETypeCode IOperations<Guid>.TypeCode => DataType.ETypeCode.Guid;
        }
        
        class ByteArrayOperations : IOperations<byte[]>
        {
            public byte[] Add(byte[] a, byte[] b) => throw new OverflowException("Can not add a dates.");
            public byte[] Subtract(byte[] a, byte[] b)  => throw new OverflowException("Can not subtract a dates.");
            public byte[] Multiply(byte[] a, byte[] b)  => throw new OverflowException("Can not multiply a dates.");
            public byte[] Divide(byte[] a, byte[] b)  => throw new OverflowException("Can not divide a dates.");
            public int Sign(byte[] a)  => throw new OverflowException("Can not get the sign of a dates.");
            public byte[] Negate(byte[] a)  => throw new OverflowException("Can not negate a dates.");    
            public bool Equal(byte[] a, byte[] b) => a.SequenceEqual(b);
            public bool GreaterThan(byte[] a, byte[] b)
            {
                for (var i = 0; i < a.Length; i++)
                {
                    if (i > b.Length) return false;
                    if (a[i] > b[i]) return true;
                    if (a[i] < b[i]) return false;
                }

                return a.Length < b.Length;
            }
            public bool LessThan(byte[] a, byte[] b)
            {
                for (var i = 0; i < a.Length; i++)
                {
                    if (i > b.Length) return true;
                    if (a[i] > b[i]) return false;
                    if (a[i] < b[i]) return true;
                }

                return a.Length > b.Length;
            }

            public bool GreaterThanEqual(byte[] a, byte[] b)
            {
                for (var i = 0; i < a.Length; i++)
                {
                    if (i > b.Length) return false;
                    if (a[i] > b[i]) return true;
                    if (a[i] < b[i]) return false;
                }

                return a.Length <= b.Length;
            }

            public bool LessThanEqual(byte[] a, byte[] b)
            {
                for (var i = 0; i < a.Length; i++)
                {
                    if (i > b.Length) return true;
                    if (a[i] > b[i]) return true;
                    if (a[i] < b[i]) return false;
                }

                return a.Length >= b.Length;
            }
            public string ToString(byte[] a) => a.ToString();
            public byte[] TryParse(object value)
            {
                if (value is byte[] bytes) return bytes;
                if (value is string valueString) return DataType.HexToByteArray(valueString);
                throw new DataTypeParseException($"Binary type conversion not supported on type {value.GetType()} .");
            }
            DataType.ETypeCode IOperations<byte[]>.TypeCode => DataType.ETypeCode.Binary;
        }
        
        class CharArrayOperations : IOperations<char[]>
        {
            public char[] Add(char[] a, char[] b) => throw new OverflowException("Can not add a chars.");
            public char[] Subtract(char[] a, char[] b)  => throw new OverflowException("Can not subtract chars.");
            public char[] Multiply(char[] a, char[] b)  => throw new OverflowException("Can not multiply chars.");
            public char[] Divide(char[] a, char[] b)  => throw new OverflowException("Can not divide chars.");
            public int Sign(char[] a)  => throw new OverflowException("Can not get the sign of chars.");
            public char[] Negate(char[] a)  => throw new OverflowException("Can not negate a dates.");    
            public bool Equal(char[] a, char[] b) => a.SequenceEqual(b);

            public bool GreaterThan(char[] a, char[] b)
            {
                for (var i = 0; i < a.Length; i++)
                {
                    if (i >= b.Length) return false;
                    if (a[i] > b[i]) return true;
                    if (a[i] < b[i]) return false;
                }

                return a.Length < b.Length;
            }
            public bool LessThan(char[] a, char[] b)
            {
                for (var i = 0; i < a.Length; i++)
                {
                    if (i >= b.Length) return true;
                    if (a[i] > b[i]) return false;
                    if (a[i] < b[i]) return true;
                }

                return a.Length > b.Length;
            }

            public bool GreaterThanEqual(char[] a, char[] b)
            {
                for (var i = 0; i < a.Length; i++)
                {
                    if (i >= b.Length) return false;
                    if (a[i] > b[i]) return true;
                    if (a[i] < b[i]) return false;
                }

                return a.Length <= b.Length;
            }

            public bool LessThanEqual(char[] a, char[] b)
            {
                for (var i = 0; i < a.Length; i++)
                {
                    if (i >= b.Length) return true;
                    if (a[i] > b[i]) return true;
                    if (a[i] < b[i]) return false;
                }

                return a.Length >= b.Length;
            }
            public string ToString(char[] a) => a.ToString();

            public char[] TryParse(object value)
            {
                if (value is char[] chars) return chars;
                return value.ToString().ToCharArray();
            }
            DataType.ETypeCode IOperations<char[]>.TypeCode => DataType.ETypeCode.Char;
           
        }
        
        class JsonOperations : IOperations<JToken>
        {
            public JToken Add(JToken a, JToken b) => throw new OverflowException("Can not add a dates.");
            public JToken Subtract(JToken a, JToken b)  => throw new OverflowException("Can not subtract a dates.");
            public JToken Multiply(JToken a, JToken b)  => throw new OverflowException("Can not multiply a dates.");
            public JToken Divide(JToken a, JToken b)  => throw new OverflowException("Can not divide a dates.");
            public int Sign(JToken a)  => throw new OverflowException("Can not get the sign of a dates.");
            public JToken Negate(JToken a)  => throw new OverflowException("Can not negate a dates.");    
            public bool Equal(JToken a, JToken b) => throw new OverflowException("Can not compare json.");
            public bool GreaterThan(JToken a, JToken b) => throw new OverflowException("Can not compare json.");
            public bool LessThan(JToken a, JToken b) => throw new OverflowException("Can not compare json.");
            public bool GreaterThanEqual(JToken a, JToken b) => throw new OverflowException("Can not compare json.");
            public bool LessThanEqual(JToken a, JToken b) => throw new OverflowException("Can not compare json.");
            public string ToString(JToken a) => a.ToString();
            public JToken TryParse(object value)
            {
                switch (value)
                {
                    case JToken jToken:
                        return jToken;
                    case string stringValue:
                        return JToken.Parse(stringValue);
                    default:
                        throw new DataTypeParseException($"The value is not a valid json string.");
                }
            }
            DataType.ETypeCode IOperations<JToken>.TypeCode => DataType.ETypeCode.Json;
        }
        
        class XmlOperations : IOperations<XmlDocument>
        {
            public XmlDocument Add(XmlDocument a, XmlDocument b) => throw new OverflowException("Can not add a dates.");
            public XmlDocument Subtract(XmlDocument a, XmlDocument b)  => throw new OverflowException("Can not subtract a dates.");
            public XmlDocument Multiply(XmlDocument a, XmlDocument b)  => throw new OverflowException("Can not multiply a dates.");
            public XmlDocument Divide(XmlDocument a, XmlDocument b)  => throw new OverflowException("Can not divide a dates.");
            public int Sign(XmlDocument a)  => throw new OverflowException("Can not get the sign of a dates.");
            public XmlDocument Negate(XmlDocument a)  => throw new OverflowException("Can not negate a dates.");    
            public bool Equal(XmlDocument a, XmlDocument b) => throw new OverflowException("Can not compare xml.");
            public bool GreaterThan(XmlDocument a, XmlDocument b) => throw new OverflowException("Can not compare xml.");
            public bool LessThan(XmlDocument a, XmlDocument b) => throw new OverflowException("Can not compare xml.");
            public bool GreaterThanEqual(XmlDocument a, XmlDocument b) => throw new OverflowException("Can not compare xml.");
            public bool LessThanEqual(XmlDocument a, XmlDocument b) => throw new OverflowException("Can not compare xml.");
            public string ToString(XmlDocument a) => a.ToString();
            public XmlDocument TryParse(object value)
            {
                switch (value)
                {
                    case XmlDocument xmlDocument:
                        return xmlDocument;
                    case string stringValue:
                        var xmlDocument1 = new XmlDocument();
                        xmlDocument1.LoadXml(stringValue);
                        return xmlDocument1;
                    default:
                        throw new DataTypeParseException($"The value is not a valid xml string.");
                }
            } 
            DataType.ETypeCode IOperations<XmlDocument>.TypeCode => DataType.ETypeCode.Xml;
        }
        
        class StringOperations : IOperations<String>
        {
            public String Add(String a, String b) => throw new OverflowException("Can not add a dates.");
            public String Subtract(String a, String b)  => throw new OverflowException("Can not subtract a dates.");
            public String Multiply(String a, String b)  => throw new OverflowException("Can not multiply a dates.");
            public String Divide(String a, String b)  => throw new OverflowException("Can not divide a dates.");
            public int Sign(String a)  => throw new OverflowException("Can not get the sign of a dates.");
            public String Negate(String a)  => throw new OverflowException("Can not negate a dates.");    
            public bool Equal(String a, String b) => a == b;
            public bool GreaterThan(String a, String b) => String.Compare(a,b) == 1;
            public bool LessThan(String a, String b) => String.Compare(a,b) == -1;
            public bool GreaterThanEqual(String a, String b) => String.Compare(a,b) >= 0;
            public bool LessThanEqual(String a, String b) => String.Compare(a,b) <= 0;
            public string ToString(String a) => a;
            public String TryParse(object value)
            {
                switch (value)
                {
                    case string stringValue:
                        return stringValue;
                    case byte[] byteValue:
                        return DataType.ByteArrayToHex(byteValue);
                    case char[] charValue:
                        return new string(charValue);
                }

                return value.ToString();
            }
            DataType.ETypeCode IOperations<String>.TypeCode => DataType.ETypeCode.String;
        }
        
        
    }
}