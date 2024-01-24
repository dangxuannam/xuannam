using CafeOnline.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeOnline.Areas.Admin.Controllers
{
    public class QuanLyThucDonController : BaseAdminController
    {
        CafeOnlineDB db = CafeOnlineDB.ConnectDatabase();
        // GET: Admin/QuanLyThucDon
        public ActionResult Index()
        {
            var model = db.MATHANGs.ToList();
            return View(model);
        }

        public ActionResult Xoa(int id)
        {
            var item = db.MATHANGs.FirstOrDefault(t => t.MatHangID == id);
            db.MATHANGs.Remove(item);
            db.SaveChanges();

            var model = db.MATHANGs.ToList();
            return View("Index",model);
        }

        [HttpGet]
        public ActionResult ThemMatHang()
        {
            ViewBag.LoaiHang = db.LOAIMATHANGs.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemMatHang(HttpPostedFileBase file, MATHANG mh)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                if (file.ContentLength > 0)
                {
                        string filename = Path.GetFileName(file.FileName);
                        /*Saving the file in server folder*/
                        //Xử lý upload trùng tên
                        var FullPath = Request.MapPath("~/Images/SanPham/" + filename);
                        if (System.IO.File.Exists(FullPath))
                            filename = "1_" + filename;
                        //upload
                        file.SaveAs(Server.MapPath("~/Images/SanPham/" + filename));
                        
                        mh.HinhAnh = filename;
                }
                else
                    mh.HinhAnh = "SanPham_Default.jpg";
                else
                    mh.HinhAnh = "default.jpg";

                mh.TrangThai = true;
                mh.LuotXem = 1;
                mh.NgayTao = DateTime.Now;
                db.MATHANGs.Add(mh);
                db.SaveChanges();

                return View("Index", db.MATHANGs.ToList());
            }
            else
            return View("ThemMatHang");
        }
    }
}