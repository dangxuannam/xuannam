using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeOnline.Areas.Admin.Controllers
{
    public class TrangChuController : BaseAdminController
    {
        // GET: Admin/TrangChu
        public ActionResult Index()
        {
            return View();
        }
    }
}