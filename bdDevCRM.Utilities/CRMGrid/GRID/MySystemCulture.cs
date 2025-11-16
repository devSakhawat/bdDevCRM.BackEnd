using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace bdDevCRM.Entities.CRMGrid.GRID
{
    public class MySystemCulture
    {
        public static void ChangeDatePattern(string formate)
        {
            var culture = (CultureInfo) CultureInfo.CurrentCulture.Clone();
            culture.DateTimeFormat.ShortDatePattern = formate;
            culture.DateTimeFormat.LongTimePattern = "HH:mm:ss";
            Thread.CurrentThread.CurrentCulture = culture;
        }

        public static void ChangeCulture()
        {
            try
            {
                //var datePattern = ConfigurationManager.AppSettings["DatePattern"];
                //ChangeDatePattern(datePattern);
                ChangeDatePattern("MM/dd/yyyy");
            }
            catch (Exception)
            {
            }
        }
    }
}