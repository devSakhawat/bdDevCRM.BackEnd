//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace Utilities.CustomData
//{
//    public class CustomValueBind
//    {
//        public static dynamic ValueConverter(Type prop, dynamic value)
//        {
//            try
//            {
//                switch (prop.Name)
//                {
//                    case "Int16":
//                        //value = value == "" ? "0" : value;

//                        value = Convert.ToInt16(value);
//                        break;

//                    case "Int32":
//                        //value = value == "" ? "0" : value;

//                        value = Convert.ToInt32(value);
//                        break;

//                    case "Int64":
//                        //value = value == "" ? "0" : value;

//                        value = Convert.ToInt64(value);
//                        break;

//                    case "Double":
//                        //value = value == "" ? "0" : value;
//                        value = Convert.ToDouble(value);
//                        break;

//                    case "Decimal":
//                       // value = value == "" ? "0" : value;
//                        value = Convert.ToDecimal(value);
//                        break;
//                    case "SByte":
//                        value = Convert.ToSByte(value);
//                        break;
//                    case "Single":
//                        value = Convert.ToSingle(value);
//                        break;

//                    case "String":
//                        value = Convert.ToString(value);
//                        break;

//                    case "Char":
//                        value = Convert.ToChar(value);
//                        break;

//                    case "Byte":
//                        value = Convert.ToByte(value);
//                        break;

//                    case "Boolean":
//                        value = Convert.ToBoolean(value);
//                        break;

//                    case "DateTime":
//                        value = Convert.ToDateTime(value);
//                        break;

//                }
//            }
//            catch (Exception)
//            {
//                return value = null;
//            }
//            return value;
//        }
//    }
//}
