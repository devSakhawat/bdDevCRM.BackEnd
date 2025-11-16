//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace bdDevCRM.Entities.CRMGrid.GRID
//{
//    public static class EscapeSequence
//    {
//        public static string EscChar( this string fieldString)
//        {
//            string returnMessage = "";
//            if (fieldString.Length > 0)
//            {
//                fieldString = fieldString.Replace("'", "''");
//                fieldString = fieldString.Replace('"', '\"');
//                fieldString = fieldString.Replace(@"\", @"\\");
//                returnMessage = fieldString;
//            }
//            return returnMessage;
//        }
//    }
//}
