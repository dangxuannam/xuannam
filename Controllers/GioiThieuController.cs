using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeOnline.Controllers
{
    public class GioiThieuController : Controller
    {
        // Trang chủ giới thiệu
        public ActionResult Index()
        {
            return View();
        }
    }
}