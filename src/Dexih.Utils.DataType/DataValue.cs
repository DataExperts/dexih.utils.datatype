//using System;
//using System.Collections;
//using System.Linq;
//using Newtonsoft.Json;
//
//namespace Dexih.Utils.DataType
//{
//    public struct DataValue: IEnumerable
//    {
//        private readonly DataType.ETypeCode _typeCode;
//        private readonly int _arrayDims; 
//        private string _stringValue;
//        private object _value;
//        private bool _isValueSet;
//
//        public DataValue(DataType.ETypeCode typeCode, int arrayDims, string stringValue)
//        {
//            _typeCode = typeCode;
//            _stringValue = stringValue;
//            _value = null;
//            _isValueSet = false;
//            _arrayDims = arrayDims;
//        }
//        
//        public DataValue(DataType.ETypeCode typeCode, int arrayDims, object value)
//        {
//            _typeCode = typeCode;
//            _stringValue = null;
//            _value = value;
//            _isValueSet = true;
//            _arrayDims = arrayDims;
//        }
//
//        [JsonIgnore]
//        public object Value
//        {
//            get
//            {
//                if (_isValueSet) return _value;
//
//                var type = DataType.GetType(_typeCode);
//                if (_arrayDims > 0)
//                {
//                    type = type.MakeArrayType(_arrayDims);
//                }
//                _value = JsonConvert.DeserializeObject(_stringValue, type);
//                _isValueSet = true;
//                return _value;
//            }
//        }
//
//        public object this[int i] => Value.GetType().IsArray ? ((object[]) Value)[i] : null;
//
//        public override string ToString()
//        {
//            if(_stringValue == null)
//            {
//                _stringValue = JsonConvert.SerializeObject(_value);
//            }
//
//            return _stringValue.ToString();
//        }
//
//        public IEnumerator GetEnumerator()
//        {
//            return Value.GetType().IsArray ? ((object[]) Value).GetEnumerator() : Enumerable.Empty<object>().GetEnumerator();
//        }
//    }
//}