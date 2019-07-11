using System;

namespace Dexih.Utils.DataType
{
    public class DataTypeException : Exception
    {
        public DataTypeException(string message): base(message)
        {

        }
        
        public DataTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class DataTypeParseException: DataTypeException
    {
        public DataTypeParseException(string message): base(message)
        {

        }
    }

    public class DataTypeCompareException : DataTypeException
    {
        public DataTypeCompareException(string message) : base(message)
        {

        }
    }
}
