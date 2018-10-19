using System;

namespace Dexih.Utils.DataType
{
    public class Operations<T>
    {
        public static IOperations<T> Default { get { return Create(); } }

        static IOperations<T> Create()
        {
            switch (Type.GetTypeCode(typeof(T)))
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
            }
            
            throw new DataTypeException($"The datatype {typeof(T)} is not a valid type for arithmetic.");
        }

        class ByteOperations : IOperations<byte>
        {
            public byte Add(byte a, byte b) { return unchecked((byte)(a + b)); }
            public byte Subtract(byte a, byte b) { return unchecked((byte)(a - b)); }
            public byte Multiply(byte a, byte b) { return unchecked((byte)(a * b)); }
            public byte Divide(byte a, byte b) { return unchecked((byte)(a / b)); }
        }

        class DoubleOperations : IOperations<double>
        {
            public double Add(double a, double b) { return a + b; }
            public double Subtract(double a, double b) { return a - b; }
            public double Multiply(double a, double b) { return a * b; }
            public double Divide(double a, double b) { return a / b; }
        }
        
        class SingleOperations : IOperations<Single>
        {
            public Single Add(Single a, Single b) { return a + b; }
            public Single Subtract(Single a, Single b) { return a - b; }
            public Single Multiply(Single a, Single b) { return a * b; }
            public Single Divide(Single a, Single b) { return a / b; }
        }
        
        class DecimalOperations : IOperations<decimal>
        {
            public decimal Add(decimal a, decimal b) { return a + b; }
            public decimal Subtract(decimal a, decimal b) { return a - b; }
            public decimal Multiply(decimal a, decimal b) { return a * b; }
            public decimal Divide(decimal a, decimal b) { return a / b; }
        }
        
        class Int16Operations : IOperations<Int16>
        {
            public Int16 Add(Int16 a, Int16 b) { return unchecked((Int16)(a + b)); }
            public Int16 Subtract(Int16 a, Int16 b) { return unchecked((Int16)(a - b)); }
            public Int16 Multiply(Int16 a, Int16 b) { return unchecked((Int16)(a * b)); }
            public Int16 Divide(Int16 a, Int16 b) { return unchecked((Int16)(a / b)); }
        }
        
        class Int32Operations : IOperations<Int32>
        {
            public Int32 Add(Int32 a, Int32 b) { return a + b; }
            public Int32 Subtract(Int32 a, Int32 b) { return a - b; }
            public Int32 Multiply(Int32 a, Int32 b) { return a * b; }
            public Int32 Divide(Int32 a, Int32 b) { return a / b; }
        }
        
        class Int64Operations : IOperations<Int64>
        {
            public Int64 Add(Int64 a, Int64 b) { return a + b; }
            public Int64 Subtract(Int64 a, Int64 b) { return a - b; }
            public Int64 Multiply(Int64 a, Int64 b) { return a * b; }
            public Int64 Divide(Int64 a, Int64 b) { return a / b; }
        }
        
        class UInt16Operations : IOperations<UInt16>
        {
            public UInt16 Add(UInt16 a, UInt16 b) { return unchecked((UInt16)(a + b)); }
            public UInt16 Subtract(UInt16 a, UInt16 b) { return unchecked((UInt16)(a - b)); }
            public UInt16 Multiply(UInt16 a, UInt16 b) { return unchecked((UInt16)(a * b)); }
            public UInt16 Divide(UInt16 a, UInt16 b) { return unchecked((UInt16)(a / b)); }
        }
        
        class UInt32Operations : IOperations<UInt32>
        {
            public UInt32 Add(UInt32 a, UInt32 b) { return a + b; }
            public UInt32 Subtract(UInt32 a, UInt32 b) { return a - b; }
            public UInt32 Multiply(UInt32 a, UInt32 b) { return a * b; }
            public UInt32 Divide(UInt32 a, UInt32 b) { return a / b; }
        }
        
        class UInt64Operations : IOperations<UInt64>
        {
            public UInt64 Add(UInt64 a, UInt64 b) { return a + b; }
            public UInt64 Subtract(UInt64 a, UInt64 b) { return a - b; }
            public UInt64 Multiply(UInt64 a, UInt64 b) { return a * b; }
            public UInt64 Divide(UInt64 a, UInt64 b) { return a / b; }
        }
        
        class SByteOperations : IOperations<SByte>
        {
            public SByte Add(SByte a, SByte b) { return unchecked((SByte)(a + b)); }
            public SByte Subtract(SByte a, SByte b) { return unchecked((SByte)(a - b)); }
            public SByte Multiply(SByte a, SByte b) { return unchecked((SByte)(a * b)); }
            public SByte Divide(SByte a, SByte b) { return unchecked((SByte)(a / b)); }
        }
    }
}