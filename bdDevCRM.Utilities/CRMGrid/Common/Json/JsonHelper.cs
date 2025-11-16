//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Script.Serialization;
//using Newtonsoft.Json;


//namespace Utilities.Common.Json
//{
//    public class JsonHelper
//    {
//        public JsonHelper()
//        {
//        }

//        public string GetJson(object obj)
//        {
//            if (null == obj) return string.Empty;

//            var serializer = new JavaScriptSerializer();

//            return serializer.Serialize(obj);
//        }

//        public string GetJsonResult(object obj)
//        {
//            if (null == obj) return string.Empty;

//            var settings = new JsonSerializerSettings();
//            settings.DateParseHandling = DateParseHandling.DateTime;
//            settings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
//            return JsonConvert.SerializeObject(obj, settings);
//        }

//        public dynamic JsonDeserialize<T>(string obj)
//        {
//            try
//            {
//                if (null == obj) return string.Empty;
//                var settings = new JsonSerializerSettings()
//                {
//                    //Culture =new CultureInfo(CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern),
//                    DateTimeZoneHandling = DateTimeZoneHandling.Local
//                };

//                return (T) JsonConvert.DeserializeObject(obj, typeof (T), settings);
//            }
//            catch (Exception ex)
//            {
//                return "Can not convert to input Data " + ex;
//            }
//        }

//        public dynamic JsonDeserializes<T>(string obj)
//        {
//            try
//            {
//                if (null == obj) return string.Empty;
//                var settings = new JsonSerializerSettings()
//                {
//                    //Culture =new CultureInfo(CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern),
//                    DateTimeZoneHandling = DateTimeZoneHandling.Local
//                };

//                var result = JsonConvert.DeserializeObject<List<T>>(obj, settings);
//                return result;
//            }
//            catch (Exception)
//            {
//                return "Can not convert to input Data";
//            }
//        }

//        public dynamic JsonDeserializesGridEntity<T>(string obj)
//        {
//            try
//            {
//                if (null == obj) return string.Empty;
//                var settings = new JsonSerializerSettings()
//                {
//                    //Culture =new CultureInfo(CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern),
//                    DateTimeZoneHandling = DateTimeZoneHandling.Local
//                };
//                var result = JsonConvert.DeserializeObject<GridEntity<T>>(obj, settings);

//                return result;
//            }
//            catch (Exception)
//            {
//                return "Can not convert to input Data";
//            }
//        }

//        public dynamic JsonDeserializesToList<T>(string obj)
//        {
//            try
//            {
//                if (null == obj) return string.Empty;
//                var settings = new JsonSerializerSettings
//                {
//                    DateTimeZoneHandling = DateTimeZoneHandling.Local
//                };
//                var data = JsonConvert.DeserializeObject<GridEntity<T>>(obj, settings);
//                var result = (T) data.Items;
//                return result;
//            }
//            catch (Exception)
//            {
//                return "Can not convert to input Data";
//            }
//        }


//    }
//}