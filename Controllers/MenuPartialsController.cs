using CafeOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeOnline.Controllers
{
    public class MenuPartialsController : Controller
    {

        CafeOnlineDB db = CafeOnlineDB.ConnectDatabase();
        public PartialViewResult SanPham()
        {
            var model = db.MATHANGs.Take(6).ToList();
            return PartialView(model);
        }

        public PartialViewResult HeaderMenu()
        {
            return PartialView();
        }

        public PartialViewResult FooterMenu()
        {
            return PartialView();
        }


    }
}