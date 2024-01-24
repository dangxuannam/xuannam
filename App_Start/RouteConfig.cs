using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CafeOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            ///Thu ngân
            routes.MapRoute(
                name: "Admin",
                url: "khu-quan-ly",
                defaults: new { controller = "TrangChu", action = "Index", id = UrlParameter.Optional, area = "Admin" }
            );



            ///Thu ngân
            routes.MapRoute(
                name: "Cashier",
                url: "quay-thu-ngan",
                defaults: new { controller = "TongQuan", action = "Index", id = UrlParameter.Optional , area= "Cashier" },
                 namespaces: new[] { "CafeOnline.Areas.Cashier" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
