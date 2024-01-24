using CafeOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeOnline.Areas.Cashier.Controllers
{
    public class TimKiemController : Controller
    {
        CafeOnlineDB db = CafeOnlineDB.ConnectDatabase();
        public JsonResult DsTenHang(string term)
        {
            List<string> lsten = db.MATHANGs.Where(n => (n.TenMatHang.Contains(term) || n.MaMatHang.StartsWith(term)) && n.TrangThai == true ).Select(t => t.TenMatHang).ToList();
            return Json(lsten, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DsTenNhanVien(string term)
        {
            List<string> lsten = db.NGUOIDUNGs.Where(n => (n.HoTenNV.Contains(term) || n.TenDangNhap.Contains(term)) && n.TrangThai == true && n.NhomSD == 3).Select(t => t.HoTenNV).ToList();
            return Json(lsten, JsonRequestBehavior.AllowGet);
        }
       
    }
}