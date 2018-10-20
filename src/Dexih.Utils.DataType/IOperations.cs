namespace Dexih.Utils.DataType
{
    public interface IOperations<T>
    {
        T Add(T a, T b);
        T Subtract(T a, T b);
        T Multiply(T a, T b);
        T Divide(T a, T b);
        T Negate(T a);
        int Sign(T a);
        
    }
}