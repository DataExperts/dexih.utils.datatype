using System;
using System.Collections.Generic;
using System.Text;

namespace Dexih.Utils.DataType
{
    public class DataTypeException : Exception
    {
        public DataTypeException(string message): base(message)
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
