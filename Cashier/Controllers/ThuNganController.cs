using CafeOnline.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeOnline.Areas.Cashier.Controllers
{
    public class ThuNganController : Controller
    {
        // GET: Cashier/ThuNgan
        public ActionResult DangXuat()
        {
            Session[CommonConstants.CASHIER_SESSION_NAME] = null;
            if (Request.Cookies[CommonConstants.COOKIE_ACCOUNT_NAME] != null)
            {
                HttpCookie ck = new HttpCookie(CommonConstants.COOKIE_ACCOUNT_NAME);
                ck.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(ck);
            }

            return RedirectToAction("Index", "Home", new { area = ""});
        }
    }
}