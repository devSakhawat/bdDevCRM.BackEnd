//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;

//namespace Utilities.Upload
//{
//    public class FileUploadManager
//    {
//        public static string FileUploading(IEnumerable<HttpPostedFileBase> files, int id, string filePath)
//        {
//            string rvPath = "";
//            if (files != null)
//            {
//                string uploadLocation = "";

//                var companyId = id;
//                //Logo Store Location 
//                //Virtual Directory
//                var severPath = filePath + companyId; // @"~/UploadOverTime/OverTime/"
//                //Creating Directory If Not exist
//                uploadLocation = Utilities.Common.Helper.Utility.GetUploadPath(severPath);
//                foreach (var file in files)
//                {
//                    var fileName = Path.GetFileName(file.FileName);
                     
//                    var physicalPath = Path.Combine(HttpContext.Current.Server.MapPath(severPath), fileName);
//                    file.SaveAs(physicalPath);
//                    rvPath = physicalPath;
//                }
//            }
//            return rvPath;
//        }

//        public static UploadFileInfo FileUploading(HttpPostedFileBase file, string filePath, int id)
//        {
//            UploadFileInfo fileInfo = new UploadFileInfo();
//            string rvPath = "";
//            if (file != null)
//            {
//                string uploadLocation = "";

//                var companyId = id;
//                //Logo Store Location 
//                //Virtual Directory
//                var severPath = filePath + companyId; // @"~/UploadOverTime/OverTime/"
//                //fileInfo.FilePath = severPath.Replace("~", "..") + "/" + DateTime.Now.ToString("ddMMyyyy_fffff") +
//                //                    "_" + file.FileName;
//                fileInfo.FilePath = severPath.Replace("~", "..") + "/" + file.FileName;
//                fileInfo.FileName = file.FileName;
//                fileInfo.Extention = FileExtention.GetExtention(file);
//                //Creating Directory If Not exist
//                Utilities.Common.Helper.Utility.GetUploadPath(severPath);
//                var fileName = Path.GetFileName(file.FileName);
//                var physicalPath = Path.Combine(HttpContext.Current.Server.MapPath(severPath), fileName);
//                file.SaveAs(physicalPath);
//            }
//            return fileInfo;
//        }

//        public static string FileDeleting(string[] fileNames, int id, string filePath)
//        {
//            string uploadStatus = "";
//            if (fileNames.Length > 0)
//            {
//                string uploadLocation = "";


//                try
//                {
//                    //Logo Store Location 
//                    //Virtual Directory
//                    //var serverPath = @"~/UploadOverTime/OverTime/" + id;
//                    var serverPath = filePath + id;

//                    foreach (var fullName in fileNames)
//                    {
//                        var fileName = Path.GetFileName(fullName);
//                        var physicalPath = Path.Combine(HttpContext.Current.Server.MapPath(serverPath), fileName);

//                        // TODO: Verify user permissions

//                        if (System.IO.File.Exists(physicalPath))
//                        {
//                            // The files are  actually removed from stored location
//                            System.IO.File.Delete(physicalPath);
//                            uploadStatus = Operation.Success.ToString();
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    uploadStatus = ex.Message;
//                }
//            }
//            return uploadStatus;
//        }
//    }

//    public class UploadFileInfo
//    {
//        public string FileName { get; set; }
//        public string Extention { get; set; }
//        public string FilePath { get; set; }
//        public string FileType { get; set; }
//    }
//}