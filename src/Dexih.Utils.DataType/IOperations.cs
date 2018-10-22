using System;

namespace Dexih.Utils.DataType
{
    public interface IOperations<T>
    {
        T Add(T a, T b);
        T Subtract(T a, T b);
        T Multiply(T a, T b);
        T Divide(T a, T b);
        T Negate(T a);
        T One { get; }
        T Zero { get; }
        T Two { get; }
        int Sign(T a);
        bool Equal(T a, T b);
        bool GreaterThan(T a, T b);
        bool LessThan(T a, T b);
        bool GreaterThanEqual(T a, T b);
        bool LessThanEqual(T a, T b);
        string ToString(T a);
        T TryParse(object value);
        DataType.ETypeCode TypeCode { get; }
        
        Func<T, T, T> AddTest { get; }
        

    }
}