//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;

//namespace Utilities.Upload
//{
//    public static class FileExtention
//    {
//        public static string GetExtention(HttpPostedFileBase file)
//        {
//            switch (file.ContentType)
//            {
//                case "application/msword":

//                    return ".doc";
//                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":

//                    return ".docx";
//                case "application/pdf":
//                    return ".pdf";
//                default:
//                    return "";
//            }
//        }
//    }
//}