using CafeOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeOnline.Areas.Cashier.Controllers
{
    public class CashierPartialsController : BaseCashierController
    {
        // GET: Cashier/CashierPartials
        CafeOnlineDB db = CafeOnlineDB.ConnectDatabase();
        public PartialViewResult BottomMenu()
        {
            var model = Session[Common.CommonConstants.CASHIER_SESSION_NAME] as NGUOIDUNG;
            return PartialView(model);
        }
    }
}