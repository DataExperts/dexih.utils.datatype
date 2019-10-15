using System;

namespace Dexih.Utils.DataType
{
    public class DataTypeException : Exception
    {
        public DataTypeException(): base()
        {

        }
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

    public class NullValueException : DataTypeException
    {
        
    }

    public class InvalidTypeException : DataTypeException
    {
        public InvalidTypeException(ETypeCode tryType, ETypeCode currentType):
            base($"Type type {tryType} was requested when the stored type is {currentType}")
        {
        }
    }

    
    public class IncompatibleTypesException : DataTypeException
    {
        public IncompatibleTypesException(ETypeCode type1, ETypeCode type2):
            base($"Type type {type1} is not compatible with type {type2}")
        {
        }
    }

    public class CompareTypeException : DataTypeException
    {
        public CompareTypeException(ETypeCode type) :
            base($"The type ${type} cannot be used for comparisons.")
        {
            
        }
    }
}
