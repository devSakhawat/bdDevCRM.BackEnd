//using Utilities.Common.Json;

//namespace Utilities
//{
//    public class JsonLayer
//    {
//        //private static GISServices _gisService;
//        //private static SecurityServices _securityService;
//        //private static LocationService _locService;
//        //private static MessagingService _mesService;
//        //private static Int32 _historyCount = 0;
//        private JsonHelper _jsonHelper = new JsonHelper();

//        static JsonLayer()
//        {
//        }

//        public string GetJsonResult(object obj)
//        {
//            return _jsonHelper.GetJson(obj);
//        }

//        public string GetJsonResult2(object obj)
//        {
//            return _jsonHelper.GetJsonResult(obj);
//        }

//        #region Old JsonLayer content commented By Mazhar

//        // public static string TrackLocation(string phoneNumber, Single latitude, Single longitude, string address, string area, string district, int accuracy, string timeStamp)
//        // {

//        //     string sessionID = HttpContext.Current.Session.SessionID;

//        //     string gpsDate = DateTime.Now.ToString();
//        //     //DateTime dt = DateTime.Parse(timeStamp+" "+DateTime.Now.TimeOfDay);
//        //     DateTime dt = DateTime.Now;
//        //     var result = SavePhoneLocation(latitude, longitude, phoneNumber,
//        //         sessionID, SessionHelper.LoggedInUser.Email, address, area, district, 0, 1, 0, dt, "88017", accuracy, "True", "Valid Location");

//        //     if (result == Constants.SUCCESS)
//        //     {
//        //         HttpContext.Current.Session.Remove(Constants.SESSION_IMAGE_KEY);

//        //     }

//        //     return result;

//        // }

//        // public static string DeleteLocationHistory(string phoneNumber, string sessionID)
//        // {            
//        //     var result = _locService.DeletePhoneLocation(phoneNumber, sessionID);

//        //     if (result == Constants.SUCCESS)
//        //         _historyCount--;

//        //     return result;
//        // }

//        // public static string GetPhoneLocations(string phoneNumber, string trackSession)
//        // {
//        //     if (trackSession == "")
//        //     {
//        //         trackSession = HttpContext.Current.Session.SessionID;
//        //     }
//        //     var locations = _locService.GetPhoneLocations(phoneNumber, trackSession);

//        //     var locs = from loc in locations
//        //                select new
//        //                {
//        //                    loc.GPSLocationID,
//        //                    loc.PhoneNumber,
//        //                    loc.SessionID,
//        //                    loc.TrackedBy,
//        //                    GPSTime = loc.GPSTime.ToString("dd-MMM-yyyy hh:mm:ss tt"),
//        //                    loc.LocationMethod,
//        //                    loc.Latitude,
//        //                    loc.Longitude,
//        //                    loc.Address,
//        //                    loc.Area,
//        //                    loc.District,
//        //                    loc.Accuracy,
//        //                    loc.Direction,
//        //                    loc.Distance,
//        //                    loc.Speed,
//        //                    loc.IsLocationValid,
//        //                    loc.ExtraInfo
//        //                };

//        //     return JsonHelper.GetJson(locs);
//        // }

//        // public static string GetTrackingHistory(string phoneNumber)
//        // {
//        //     var trackingHistory = _locService.GetTrackingHistory(phoneNumber, SessionHelper.LoggedInUser.Email);
//        //     _historyCount = trackingHistory.Count();
//        //     return JsonHelper.GetJson(trackingHistory);
//        // }

//        // public static Boolean IsTrackingAllowed(string phoneNumber)
//        // {
//        //     var storageAllowed = true;
//        //     if (_historyCount == 0)
//        //     {
//        //         _historyCount = _locService.GetTrackingHistory(phoneNumber, SessionHelper.LoggedInUser.Email).Count();
//        //     }
//        //     //if newsession and check history(max 10) from cache and is user trac enabled
//        //     //then allow new tracking
//        //     var session = _locService.GetPhoneLocations(phoneNumber, 
//        //         HttpContext.Current.Session.SessionID).Count;
//        //     var isActive = SessionHelper.LoggedInUser.IsTrackingActive;
//        //     if (isActive)
//        //     {
//        //         if (session == 0 && _historyCount == 10)
//        //         {
//        //             storageAllowed = false;
//        //         }
//        //     }
//        //     else
//        //     {
//        //         storageAllowed = false;
//        //     }

//        //     return storageAllowed;

//        //     //return JsonHelper.GetJson(storageAllowed);
//        // }

//        // public static string SavePhoneLocation(Single latitude, Single longitude,
//        //     string phoneNumber, string sessionID, string trackedBy, string address,
//        //     string area, string district, int speed, int direction, int distance,
//        //     DateTime gpsTime, string locationMethod, int accuracy,
//        //     string isLocationValid, string extraInfo)
//        // {

//        //     var loc = PopulateLocation(latitude, longitude, phoneNumber, sessionID,
//        //         trackedBy, address, area, district, speed, direction, distance, 
//        //         gpsTime, locationMethod, accuracy, isLocationValid, extraInfo);

//        //     var result = _locService.SavePhoneLocation(loc);

//        //     if (result == Constants.SUCCESS)
//        //     {
//        //         HttpContext.Current.Session.Remove(Constants.SESSION_IMAGE_KEY);
//        //     }

//        //     return result;
//        // }

//        // private static MobileLocations PopulateLocation(Single latitude, Single longitude,
//        //     string phoneNumber, string sessionID, string trackedBy, string address,
//        //     string area, string district, int speed, int direction, int distance,
//        //     DateTime gpsTime, string locationMethod, int accuracy,
//        //     string isLocationValid, string extraInfo)
//        //{
//        //     var loc = new MobileLocations();
//        //     loc.Latitude = (Decimal)latitude;
//        //     loc.Longitude = (Decimal)longitude;
//        //     loc.PhoneNumber = phoneNumber;
//        //     loc.TrackedBy = trackedBy;
//        //     loc.Address = address;
//        //     loc.Area = area;
//        //     loc.District = district;
//        //     loc.Speed = speed;
//        //     loc.Direction = direction;
//        //     loc.Distance = distance;
//        //     loc.GPSTime = gpsTime;
//        //     loc.LastUpdate = gpsTime;
//        //     loc.LocationMethod = locationMethod;
//        //     loc.Accuracy = accuracy;
//        //     loc.IsLocationValid = isLocationValid;
//        //     loc.ExtraInfo = extraInfo;
//        //     loc.SessionID = sessionID == null ? HttpContext.Current.Session.SessionID : sessionID;

//        //     return loc;
//        // }

//        // private static byte[] GetLocationPicture()
//        // {
//        //     var imageFile = HttpContext.Current.Session[Constants.SESSION_IMAGE_KEY] as HttpPostedFile; ;

//        //     if (imageFile != null)
//        //     {
//        //         return ImageHelper.ConvertToByteArray(imageFile.InputStream);
//        //     }
//        //     else
//        //     {
//        //         return null;
//        //     }
//        // }

//        // public static string SaveMobileMessage(string messageID, string sourcePhoneNumber,
//        //     string messagingPhoneNumber, Int32 messageType, string messageStatus,
//        //     DateTime messagingTime, string smsContent, string mmsContent)
//        // {

//        //     var mes = PopulateMessage(messageID, sourcePhoneNumber, messagingPhoneNumber, 
//        //         messageType, messageStatus, messagingTime, smsContent, mmsContent);

//        //     var result = _mesService.SaveMobileMessage(mes);

//        //     if (result == Constants.SUCCESS)
//        //     {
//        //         HttpContext.Current.Session.Remove(Constants.SESSION_IMAGE_KEY);
//        //     }

//        //     return result;
//        // }

//        // private static MobileMessages PopulateMessage(string messageID, string senderPhoneNumber,
//        //     string receiverPhoneNumber, Int32 messageType, string messageStatus,
//        //     DateTime messagingTime, string smsContent, string mmsContent)
//        // {
//        //     var mes = new MobileMessages();
//        //     mes.MessageID = messageID;
//        //     mes.SourcePhoneNumber = senderPhoneNumber;
//        //     mes.MessagingPhoneNumber = receiverPhoneNumber;
//        //     mes.MessageType = messageType;
//        //     mes.MessageStatus = messageStatus;
//        //     mes.MessagingTime = messagingTime;
//        //     mes.SMSContent = smsContent;
//        //     mes.MMSContent = mmsContent;

//        //     return mes;
//        // }

//        // public static String GetInboxMessages(string phoneNumber)
//        // {
//        //     var inbox = _mesService.GetInboxMessages(phoneNumber);

//        //     if (inbox.Count == 0) return "N/A";

//        //     var messages = from newInbox in inbox
//        //                    orderby newInbox.MessagingTime descending
//        //                    select new
//        //                    {
//        //                        newInbox.MessageNumber,
//        //                        newInbox.MessageID,
//        //                        newInbox.SourcePhoneNumber,
//        //                        newInbox.MessagingPhoneNumber,
//        //                        newInbox.MessageType,
//        //                        MessagingTime = newInbox.MessagingTime.ToString("dd-MMM-yyyy hh:mm:ss tt"),
//        //                        newInbox.MessageStatus                           
//        //                    };

//        //     return JsonHelper.GetJson(messages);
//        // }

//        // public static String GetSentMessages(string phoneNumber)
//        // {
//        //     var sentbox = _mesService.GetSentMessages(phoneNumber);

//        //     if (sentbox.Count == 0) return "N/A";

//        //     var messages = from newSentbox in sentbox
//        //                    orderby newSentbox.MessagingTime descending
//        //                    select new
//        //                    {
//        //                        newSentbox.MessageNumber,
//        //                        newSentbox.MessageID,
//        //                        newSentbox.SourcePhoneNumber,
//        //                        newSentbox.MessagingPhoneNumber,
//        //                        newSentbox.MessageType,
//        //                        MessagingTime = newSentbox.MessagingTime.ToString("dd-MMM-yyyy hh:mm:ss tt"),
//        //                        newSentbox.MessageStatus
//        //                    };

//        //     return JsonHelper.GetJson(messages);
//        // }

//        // public static String GetDraftMessages(string phoneNumber)
//        // {
//        //     var draftbox = _mesService.GetDraftMessages(phoneNumber);

//        //     if (draftbox.Count == 0) return "N/A";

//        //     var messages = from newDraftbox in draftbox
//        //                    orderby newDraftbox.MessagingTime descending
//        //                    select new
//        //                    {
//        //                        newDraftbox.MessageNumber,
//        //                        newDraftbox.SourcePhoneNumber,
//        //                        newDraftbox.MessagingPhoneNumber,
//        //                        newDraftbox.MessageType,
//        //                        MessagingTime = newDraftbox.MessagingTime.ToString("dd-MMM-yyyy hh:mm:ss tt"),
//        //                        newDraftbox.MessageStatus
//        //                    };

//        //     return JsonHelper.GetJson(messages);
//        // }

//        // public static String GetReadMessage(string messageID, int messageType)
//        // {

//        //     //if (messageType == 4)
//        //     //{
//        //         var readMessage = _mesService.GetMobileMessage(messageID);

//        //         if (readMessage == null) return "N/A";

//        //         var message = new MobileMessage()
//        //         {
//        //             SourcePhoneNumber = readMessage.SourcePhoneNumber,
//        //             MessagingPhoneNumber = readMessage.MessagingPhoneNumber,
//        //             MessagingTime = readMessage.MessagingTime.ToString("dd-MMM-yyyy hh:mm:ss tt"),
//        //             MessageContent = readMessage.SMSContent,
//        //             MMSContent = readMessage.MMSContent
//        //         };

//        //         return JsonHelper.GetJson(message);

//        //     //}
//        //     //else return "N/A";

//        // }
//        // public static String GetDraftMessage(int messageNumber)
//        // {

//        //     var readMessage = _mesService.GetMobileMessage(messageNumber);

//        //     if (readMessage == null) return "N/A";

//        //     var message = new MobileMessage()
//        //     {
//        //         SourcePhoneNumber = readMessage.SourcePhoneNumber,
//        //         MessagingPhoneNumber = readMessage.MessagingPhoneNumber,
//        //         MessagingTime = readMessage.MessagingTime.ToString("dd-MMM-yyyy hh:mm:ss tt"),
//        //         MessageContent = readMessage.SMSContent,
//        //         MMSContent = readMessage.MMSContent
//        //     };

//        //     return JsonHelper.GetJson(message);            

//        // }
//        // public static String GetUnreadMessage(string messageID, int messageType)
//        // {

//        //         var unreadMessage = _mesService.GetMobileMessage(messageID);

//        //         if (unreadMessage == null) return "N/A";

//        //         var message = new MobileMessage()
//        //         {
//        //             SourcePhoneNumber = unreadMessage.SourcePhoneNumber,
//        //             MessagingPhoneNumber = unreadMessage.MessagingPhoneNumber,
//        //             MessagingTime = unreadMessage.MessagingTime.ToString("dd-MMM-yyyy hh:mm:ss tt"),
//        //             MessageContent = unreadMessage.SMSContent,
//        //             MMSContent = unreadMessage.MMSContent
//        //         };

//        //         unreadMessage.MessageStatus = "Inbox";
//        //         var readMessage = _mesService.SaveMobileMessage(unreadMessage);

//        //         if (readMessage == Constants.SUCCESS)
//        //         {
//        //             return JsonHelper.GetJson(message);
//        //         }
//        //         else
//        //         {
//        //             return readMessage; 
//        //         }

//        // }
//        // public static MobileMessage GetMessageDetails(string messageNo)
//        // {
//        //     int mesNo = int.Parse(messageNo);
//        //     var messageDetails = _mesService.GetMobileMessage(mesNo);

//        //     if (messageDetails == null) return null;

//        //     var message = new MobileMessage()
//        //     {
//        //         SourcePhoneNumber = messageDetails.SourcePhoneNumber,
//        //         MessagingPhoneNumber = messageDetails.MessagingPhoneNumber,
//        //         MessagingTime = messageDetails.MessagingTime.ToString("dd-MMM-yyyy hh:mm:ss tt"),
//        //         MessageContent = messageDetails.SMSContent,
//        //         MMSContent = messageDetails.MMSContent
//        //     };
//        //     return message;             

//        // }

//        // public static String DeleteMessage(string messageNo)
//        // {
//        //     int mesNo = int.Parse(messageNo);
//        //     var result = _mesService.DeleteMobileMessage(mesNo);

//        //     if (result == Constants.SUCCESS)
//        //     {
//        //         HttpContext.Current.Session.Remove(Constants.SESSION_IMAGE_KEY);
//        //     }

//        //     return result;
//        // }

//        // private static void ClearCookie()
//        // {
//        //     HttpContext.Current.Response.Cookies[Constants.COOKIE_KEY].Expires = DateTime.Now.AddDays(-1);
//        // }

//        // //public static string GetSubCategories()
//        // //{
//        // //    var subCategories = _gisService.GetSubCategories();

//        // //    var newSubCategories = from q in subCategories
//        // //                           select new
//        // //                           {
//        // //                               q.SubCategoryCode,
//        // //                               q.SubCategoryName
//        // //                           };

//        // //    return JsonHelper.GetJson(newSubCategories.ToList());
//        // //}

//        // //public static string GetSubCategoriesByCategoryCode(string categoryCode)
//        // //{
//        // //    var subCategories = _gisService.GetSubCategories(categoryCode);

//        // //    var newSubCategories = from q in subCategories
//        // //                           select new
//        // //                           {
//        // //                               q.SubCategoryCode,
//        // //                               q.SubCategoryName
//        // //                           };

//        // //    return JsonHelper.GetJson(newSubCategories);
//        // //}

//        // //public static string GetDistricts()
//        // //{
//        // //    var districts = _gisService.GetDistricts();

//        // //    var newDistricts = from dist in districts
//        // //                       orderby dist.DistrictName ascending
//        // //                       select new
//        // //                       {
//        // //                           dist.DistrictCode,
//        // //                           dist.DistrictName
//        // //                       };

//        // //    return JsonHelper.GetJson(newDistricts);
//        // //}

//        // //public static string GetThana(string districtCode)
//        // //{
//        // //    var thanaList = _gisService.GetThanaList(districtCode);

//        // //    var newThanaList = from thana in thanaList
//        // //                       orderby thana.ThanaName ascending
//        // //                       select new
//        // //                       {
//        // //                           thana.ThanaCode,
//        // //                           thana.ThanaName
//        // //                       };

//        // //    return JsonHelper.GetJson(newThanaList);
//        // //}

//        // //public static string GetPostOffices(string districtCode)
//        // //{
//        // //    var postOffices = _gisService.GetPostOffices(districtCode);

//        // //    var newPostOffices = from po in postOffices
//        // //                         orderby po.SubOfficeName ascending
//        // //                         select new
//        // //                         {
//        // //                             po.PostCode,
//        // //                             po.SubOfficeName
//        // //                         };

//        // //    return JsonHelper.GetJson(newPostOffices);
//        // //}

//        // //public static string GetAreas(string districtCode)
//        // //{
//        // //    var area = _locService.GetLocations(districtCode);

//        // //    var newArea = from v in area
//        // //                  orderby v.Title
//        // //                  select new
//        // //                  {
//        // //                      v.LocationKey,
//        // //                      v.Title
//        // //                  };

//        // //    return JsonHelper.GetJson(newArea);
//        // //}

//        // public static string GetTraficData()
//        // {
//        //     var traffic = _locService.GetTrafficSignals();

//        //     var newTraffic = from v in traffic
//        //                      select new
//        //                      {
//        //                          v.Title,
//        //                          v.Latitude,
//        //                          v.Longitude
//        //                      };

//        //     return JsonHelper.GetJson(newTraffic);
//        // }

//        // public static String GetLogedinUserData()
//        // {
//        //     var user = SessionHelper.LoggedInUser;

//        //     if (user == null) return "N/A";            

//        //     var newUser = new MobileUserAccounts
//        //     {
//        //         AccountKey = user.AccountKey,
//        //         Password = EncryptDecryptHelper.Decrypt(user.Password),
//        //         CreatedDate = user.CreatedDate,
//        //         Email = user.Email,
//        //         GPConfCode = user.GPConfCode,
//        //         GPUserId = user.GPUserId,
//        //         GPPassword = user.GPPassword == null ?
//        //                          null : EncryptDecryptHelper.Decrypt(user.GPPassword),
//        //         IsTrackingActive = user.IsTrackingActive,
//        //         IsGPRegistered = user.IsGPRegistered,
//        //         AutoRefreshTime = user.AutoRefreshTime,
//        //         Name = user.Name,
//        //         PhoneNo = user.PhoneNo,
//        //     };

//        //     return JsonHelper.GetJson(newUser);            
//        // }

//        // public static string GetUserAccountByPhone(string phoneNumber)
//        // {
//        //     var user = _securityService.GetUserAccountByPhone(phoneNumber);
//        //     return JsonHelper.GetJson(user);
//        // }

//        // public static string IsPhoneNumberExists(string phoneNumber)
//        // {
//        //     var result = false;
//        //     if (_securityService.IsPhoneNumberExists(phoneNumber))
//        //     {
//        //         result = true;
//        //     }
//        //     return JsonHelper.GetJson(result);
//        // }

//        // public static string IsUserValid(string email, string password, bool rememberMe)
//        // {
//        //     var valid = Constants.FAILED;
//        //     var user = _securityService.GetUserAccount(email, password);

//        //     if (user != null)
//        //     {
//        //         if (user.IsUserActive)
//        //         {
//        //             SessionHelper.LoggedInUser = user;

//        //             var newUser = new MobileUserAccounts
//        //             {
//        //                 Name = user.Name,
//        //                 Email = user.Email,
//        //                 IsTrackingActive = user.IsTrackingActive,
//        //                 PhoneNo = user.PhoneNo
//        //             };                   

//        //             valid = JsonHelper.GetJson(newUser);

//        //             if (rememberMe)
//        //             {
//        //                 var userCookie = new HttpCookie(Constants.COOKIE_KEY);
//        //                 userCookie.Values[Constants.COOKIE_LOGIN_ID] = email;
//        //                 userCookie.Values[Constants.COOKIE_LOGIN_PASSWORD] = EncryptDecryptHelper.Encrypt(password);
//        //                 userCookie.Path = "/";
//        //                 var dtExpires = DateTime.Now.AddHours(360);
//        //                 userCookie.Expires = dtExpires;
//        //                 HttpContext.Current.Response.Cookies.Add(userCookie);
//        //             }
//        //             else
//        //             {
//        //                 if (HttpContext.Current.Request.Cookies[Constants.COOKIE_KEY] != null)
//        //                 {
//        //                     ClearCookie();
//        //                 }
//        //             }
//        //         }
//        //         else
//        //         {
//        //             valid = Constants.INACTIVE_ACCOUNT;
//        //         }
//        //     }
//        //     else
//        //     {
//        //         SessionHelper.LoggedInUser = null;
//        //         ClearCookie();
//        //     }

//        //     return valid;
//        // }

//        // public static bool CheckUserCookie()
//        // {
//        //     bool valid = false;

//        //     if (HttpContext.Current.Request.Cookies[Constants.COOKIE_KEY] != null)
//        //     {
//        //         var email = HttpContext.Current.Request.Cookies[Constants.COOKIE_KEY][Constants.COOKIE_LOGIN_ID];
//        //         var password = HttpContext.Current.Request.Cookies[Constants.COOKIE_KEY][Constants.COOKIE_LOGIN_PASSWORD];
//        //         password = EncryptDecryptHelper.Decrypt(password);

//        //         var user = _securityService.GetUserAccount(email, password);

//        //         if (user != null)
//        //         {
//        //             //HttpContext.Current.Session.Add(Constants.SESSION_LOGIN_KEY, user);
//        //             SessionHelper.LoggedInUser = user;
//        //             valid = true;
//        //         }
//        //         else
//        //         {
//        //             ClearCookie();
//        //             valid = false;
//        //         }
//        //     }
//        //     else
//        //     {
//        //         valid = false;
//        //     }

//        //     return valid;
//        // }

//        // public static string CreateUserAccount(string email, string password, string phoneNo,
//        //                     string gpUserID, string gpPassword, string fullName, string confCode, Boolean isGPRegistered)
//        // {

//        //     String result = String.Empty;

//        //     if (_securityService.IsPhoneNumberExists(phoneNo))
//        //     {
//        //         return result = "Phone number already registered. Please try with another phone number.";
//        //     }
//        //     else
//        //     {
//        //         var account = new MobileUserAccounts
//        //         {
//        //             GPUserId = gpUserID,
//        //             GPPassword = gpPassword,
//        //             Password = password,
//        //             PhoneNo = phoneNo,
//        //             Email = email,
//        //             Name = fullName,
//        //             GPConfCode = confCode,
//        //             IsGPRegistered = isGPRegistered
//        //         };

//        //         result = _securityService.CreateUserAccount(account);

//        //         if (result == Constants.SUCCESS)
//        //         {
//        //             var errorMessage = _securityService.SendMailAfterNewUserAccount(account);

//        //             if (!String.IsNullOrEmpty(errorMessage))
//        //             {
//        //                 result = errorMessage;
//        //             }
//        //         }
//        //         else
//        //         {
//        //             result = "Failed to create User account. Please check your data.";
//        //         }

//        //         return result;
//        //     }
//        // }

//        // public static string UpdateUserAccount(string email, string password, string phoneNo,
//        //                     string gpUserID, string gpPassword, string fullName, string confCode,
//        //                     Boolean isTrackingActive, Boolean isGPRegistered, int autoRefreshTime)
//        // {

//        //     var account = SessionHelper.LoggedInUser;
//        //     var msg ="";

//        //     if ((account.PhoneNo != phoneNo) && (_securityService.IsPhoneNumberExists(phoneNo)))
//        //     {
//        //         return msg = "Phone number already registered. Please try with another phone number.";
//        //     }
//        //     else
//        //     {
//        //         account.GPUserId = gpUserID;
//        //         account.GPPassword = gpPassword;
//        //         account.Password = password;
//        //         account.PhoneNo = phoneNo;
//        //         account.Name = fullName;
//        //         account.GPConfCode = confCode;
//        //         account.IsTrackingActive = isTrackingActive;
//        //         account.IsGPRegistered = isGPRegistered;
//        //         account.AutoRefreshTime = autoRefreshTime;


//        //         return msg=_securityService.UpdateUserAccount(account);
//        //     }
//        // }

//        // public static string SendPassword(string loginId)
//        // {
//        //     return _securityService.SendPassword(loginId);
//        // }        

//        #endregion
//    } //end class
//} //end namespace