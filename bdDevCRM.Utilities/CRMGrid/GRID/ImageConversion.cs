using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Entities.CRMGrid.GRID
{
    public class ImageConversion
    {
        public static byte[] GetBinaryImage(string imagePath)
        {
            try
            {
                var systemPath = AppDomain.CurrentDomain.BaseDirectory;
                var fullPath = systemPath + imagePath.Remove(0, 2);
                byte[] buffer = File.ReadAllBytes(fullPath);
                return buffer;
            }
            catch (Exception ex)
            {
                throw new Exception("Error! Converting Image to Binary is Faild");
            }
        }
    }
}
