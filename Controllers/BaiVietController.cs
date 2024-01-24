using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeOnline.Models;
namespace CafeOnline.Controllers
{
    public class BaiVietController : Controller
    {
        CafeOnlineDB db = CafeOnlineDB.ConnectDatabase();
        public ActionResult Index(int id=5)
        {
            var model = db.BAIVIETs.FirstOrDefault(t => t.BaiVietID == id);
            if (model == null)
                return Redirect(Common.CommonConstants.HOMEPAGE);
            return View(model);
        }
    }
}