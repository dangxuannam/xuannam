using CafeOnline.Common;
using CafeOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CafeOnline.Areas.Cashier.Controllers
{
    public class BaseCashierController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //CafeOnlineDB db = new CafeOnlineDB();
            CafeOnlineDB db = CafeOnlineDB.ConnectDatabase();


            HttpCookie cookie = Request.Cookies[Common.CommonConstants.COOKIE_ACCOUNT_NAME];
            if (cookie != null)
            {
                string ad_userName = cookie["TenDangNhap"].ToString();
                string ad_Password = cookie["MatKhau"].ToString();
                string passwword_MD5 = Encryptor.MD5Hash(ad_Password);

                var cashier_cookie = db.NGUOIDUNGs.SingleOrDefault(n => n.TenDangNhap == ad_userName && n.MatKhau == passwword_MD5 && n.NhomSD == 2 && n.TrangThai == true);
                if (cashier_cookie == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "DangNhap", action = "Index", area=""}));

                }
                else
                {
                    Session[Common.CommonConstants.CASHIER_SESSION_NAME] = cashier_cookie;
                }

            }
            var cashier = Session[Common.CommonConstants.CASHIER_SESSION_NAME] as NGUOIDUNG;
            if (cashier == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "DangNhap", action = "Index", area = "" }));

            }

            base.OnActionExecuting(filterContext);
        }
    }
}