//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Utilities
//{
//    public class Folder
//    {
//        public static string CreateFolder(string path)
//        {
//            string physicalPath = System.Web.HttpContext.Current.Server.MapPath(path);
//            if (!Directory.Exists(physicalPath))
//            {
//                Directory.CreateDirectory(physicalPath);
//            }
//            //physicalPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("../Import/SalarySheet/"), fileName);
//            return physicalPath;
//        }
//    }
//}