namespace Dexih.Utils.DataType
{
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
            Geometry,
            Date,
            DateTimeOffset
        }
        
        // [JsonConverter(typeof(StringEnumConverter))]
        public enum ECompare
        {
            IsEqual,
            GreaterThan,
            GreaterThanEqual,
            LessThan,
            LessThanEqual,
            NotEqual,
            IsIn,
            IsNull,
            IsNotNull,
            Like,
            IsNotIn
        }
}