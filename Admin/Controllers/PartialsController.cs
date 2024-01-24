using CafeOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeOnline.Areas.Admin.Controllers
{
    public class PartialsController : BaseAdminController
    {
       public PartialViewResult AdminDropdownMenu()
        {
            var model = Session[Common.CommonConstants.ADMIN_SESSION_NAME] as NGUOIDUNG;
            return PartialView(model);
        }


        public PartialViewResult PrimaryAdminMenu()
        {
            return PartialView();
        }
    }
}