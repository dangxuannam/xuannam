using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeOnline.Controllers
{
    public class LienHeController : Controller
    {
        // GET: LienHe
        public ActionResult Index()
        {
            return Redirect(Common.CommonConstants.HOMEPAGE);
        }
    }
}