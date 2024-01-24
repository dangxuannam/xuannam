using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeOnline.Models;
using PagedList;

namespace CafeOnline.Controllers
{
    public class ThucDonController : Controller
    {
        CafeOnlineDB db = CafeOnlineDB.ConnectDatabase();
        /// <summary>
        /// Xem thực đơn
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            return View(db.MATHANGs.ToList().OrderBy(x => x.LoaiHang).ToPagedList(pageNumber, pageSize));
        }
        public JsonResult DsTenHang(string term)
        {
            List<string> lsten = db.MATHANGs.Where(n => (n.TenMatHang.Contains(term) || n.MaMatHang.StartsWith(term)) && n.TrangThai == true).Select(t => t.TenMatHang).ToList();
            return Json(lsten, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Xem thực đơn theo nhóm
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Nhom(string id, int? page)
        {
            ViewBag.Title = id;
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            return View(db.MATHANGs.Where(n=>n.LOAIMATHANG.TenLoaiHang.Equals(id)).ToList().OrderBy(x => x.LoaiHang).ToPagedList(pageNumber, pageSize));
        }



        public ActionResult XemMon(int id)
        {
            var model = db.MATHANGs.SingleOrDefault(n => n.MatHangID == id);
            if (model == null) return RedirectToAction("Index");
            return View(model);
        }
        

        [HttpPost]
        public ActionResult TimKiem(FormCollection f, int? trang)
        {
            string tukhoa = f["tukhoa"].ToString();
            int trangso = (trang ?? 1);
            int SoMatHangTren1Trang = 6;
            var model = db.MATHANGs.Where(t => t.TenMatHang.Contains(tukhoa) || t.MaMatHang.Contains(tukhoa) || t.LOAIMATHANG.TenLoaiHang.Contains(tukhoa)).ToList().OrderBy(x => x.LoaiHang).ToPagedList(trangso, SoMatHangTren1Trang);
            ViewBag.ThongBao = "Đã tìm được " + model.Count + " sản phẩm cho từ khóa \"" + tukhoa + "\"";
            return View(model);
        }


        public ActionResult TimKiem(string tukhoa, int? trang)
        {
            int trangso = (trang ?? 1);
            int SoMatHangTren1Trang = 6;

            var model = db.MATHANGs.Where(t=>t.TenMatHang.Contains(tukhoa) || t.MaMatHang.Contains(tukhoa)).ToList().OrderBy(x => x.LoaiHang).ToPagedList(trangso, SoMatHangTren1Trang);
            //Gán thông báo
            ViewBag.ThongBao = "Đã tìm được " + model.Count + " sản phẩm cho từ khóa \"" + tukhoa +"\"";  

             model = db.MATHANGs.ToList().OrderBy(x => x.LoaiHang).ToPagedList(trangso, SoMatHangTren1Trang);
            return View("Index",model);

        }
    }
}