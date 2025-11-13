//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;

//namespace Utilities
//{
//    public class bdDevsSessionAuthorizeAttribute : AuthorizeAttribute
//    {
//        protected override bool AuthorizeCore(HttpContextBase httpContext)
//        {
//            return httpContext.Session["CurrentUser"] != null;
//        }

//        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
//        {
//            //filterContext.Result = new HttpNotFoundResult();
//            //filterContext.Result = new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
//            filterContext.Result = new RedirectResult("~/Home/Login");

//        }
//    }

//}
