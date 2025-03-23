//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Utilities
//{
//    public class ResizeImage
//    {
//        public static byte[] MakeResize(byte[] myImage, int thumbWidth, int thumbHeight)
//        {
//            try
//            {
//                using (var ms = new MemoryStream())
//                using (var thumbnail = Image.FromStream(new MemoryStream(myImage)).GetThumbnailImage(thumbWidth, thumbHeight, null, new IntPtr()))
//                {
//                    thumbnail.Save(ms, ImageFormat.Png);
//                    return ms.ToArray();
//                }
//            }
//            catch (Exception e)
//            {
//                throw new Exception("Error! While minimizeing Image size: " + e.Message);
//            }
//        }

       
//    }
//}
