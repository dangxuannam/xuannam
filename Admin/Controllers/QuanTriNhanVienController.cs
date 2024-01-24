using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeOnline.Models;
using System.IO;

namespace CafeOnline.Areas.Admin.Controllers
{
    public class QuanTriNhanVienController : BaseAdminController
    {
        CafeOnlineDB db = CafeOnlineDB.ConnectDatabase();
        public ActionResult DanhSachThuNgan()
        {
            var lsTN = db.NGUOIDUNGs.Where(n => n.NhomSD == 2).ToList();
            return View(lsTN);
        }

        public ActionResult DanhSachPhucVu()
        {
            var lsPV = db.NGUOIDUNGs.Where(n => n.NhomSD == 3).ToList();
            return View(lsPV);
        }

        [HttpGet]
        public ActionResult ThemNhanVien()
        {
            ViewBag.nhomSD = db.NHOMNGUOIDUNGs.Where(n=>n.TrangThai == true).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemNhanVien(HttpPostedFileBase file, NGUOIDUNG nv)
        {
            if(ModelState.IsValid)
            {
                if (file != null)
                    if (file.ContentLength > 0)
                    {
                        string filename = Path.GetFileName(file.FileName);
                        /*Saving the file in server folder*/
                        //Xử lý upload trùng tên
                        var FullPath = Request.MapPath("~/Images/User/" + filename);
                        if (System.IO.File.Exists(FullPath))
                            filename = "1_" + filename;
                        //upload
                        file.SaveAs(Server.MapPath("~/Images/User/" + filename));

                        nv.AnhDaiDien = filename;
                    }
                else
                        nv.AnhDaiDien = "default.jpg";
                else
                    nv.AnhDaiDien = "default.jpg";
                nv.LanCuoiDangNhap = DateTime.Now;
                nv.SoLanDangNhap = 0;
                nv.NgayVaoLam = DateTime.Now;

                nv.MatKhau = Common.Encryptor.MD5Hash(nv.MatKhau);
                nv.TenDangNhap = nv.TenDangNhap.Trim().ToLower();
                nv.NgayTao = DateTime.Now;
                db.NGUOIDUNGs.Add(nv);
                db.SaveChanges();

                var hd = new HOATDONG();
                hd.NoiDung = "Thêm nhân viên " + nv.HoTenNV;
                hd.ThoiGian = DateTime.Now;
                hd.LoaiHoatDong = 2;//Nhóm hoạt động của quản trị viên
                db.HOATDONGs.Add(hd);
                db.SaveChanges();
                return RedirectToAction("DanhSachPhucVu");
            }
           
            return RedirectToAction("DanhSachThuNgan");
        }

    }
}