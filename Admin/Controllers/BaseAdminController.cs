using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeOnline.Models;
using System.Web.Routing;
using CafeOnline.Common;

namespace CafeOnline.Areas.Admin.Controllers
{
    public class BaseAdminController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //CafeOnlineDB db = new CafeOnlineDB();
            CafeOnlineDB db = CafeOnlineDB.ConnectDatabase();


            HttpCookie cookie = Request.Cookies[CommonConstants.COOKIE_ACCOUNT_NAME];
            if (cookie != null)
            {
                string ad_userName = cookie["TenDangNhap"].ToString();
                string ad_Password = cookie["MatKhau"].ToString();
                string passwword_MD5 = Encryptor.MD5Hash(ad_Password);

                var ad_cookie = db.NGUOIDUNGs.SingleOrDefault(n => n.TenDangNhap == ad_userName && n.MatKhau == passwword_MD5 && n.NhomSD == 1 && n.TrangThai == true);
                if (ad_cookie == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "Login", action = "Index", area = "Admin" }));

                }
                else
                {
                    Session[CommonConstants.ADMIN_SESSION_NAME] = ad_cookie;
                }

            }
            var admin = Session[Common.CommonConstants.ADMIN_SESSION_NAME] as NGUOIDUNG;
            if (admin == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index", area = "Admin" }));

            }

            base.OnActionExecuting(filterContext);
        }
    }
}