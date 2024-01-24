using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeOnline.Models;
namespace CafeOnline.Areas.Cashier.Controllers
{
    public class ChamCongController : BaseCashierController
    {
        CafeOnlineDB db = CafeOnlineDB.ConnectDatabase();

        public ActionResult Index()
        {
            var lsCong = db.CHAMCONGs.Where(n => n.TrangThai == true).ToList();
            var lsCongHomNay = new List<CHAMCONG>();
            foreach (var cong in lsCong)
            {
                if (DateTime.Parse(cong.NgayCham.ToString()).ToShortDateString().Equals(DateTime.Now.ToShortDateString()))
                    lsCongHomNay.Add(cong);
            }
            return View(lsCongHomNay);
        }

        public JsonResult DsCaLam(string term)
        {
            List<string> lsCa = db.CALAMVIECs.Where(n => (n.TenCa.Contains(term) || n.CaLamID.ToString().Contains(term)) && n.TrangThai == true ).Select(t => t.TenCa).ToList();
            return Json(lsCa, JsonRequestBehavior.AllowGet);
        }


        //Danh sách quy đingj làm muộn
        public JsonResult QuyDinhDiMuon(string term)
        {
            //ca muộn có trang thái == false
            List<string> lsCa = db.QUYDINHDIMUONs.Where(n => n.GhiChu.Contains(term) ).Select(t => t.GhiChu).ToList();
            return Json(lsCa, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DsNhanVien(string term)
        {
            List<string> lsNV = db.NGUOIDUNGs.Where(n => (n.HoTenNV.Contains(term) || n.TenDangNhap.Contains(term)) && n.TrangThai == true && (n.NhomSD == 3 || n.NhomSD ==2)).Select(t => t.HoTenNV).ToList();
            return Json(lsNV, JsonRequestBehavior.AllowGet);
        }

        public JsonResult  ThemCongMoi(string tenNhanVien, string TenCaLam, string DiMuon="", string GhiChu="")
        {
            NGUOIDUNG thungan = Session[Common.CommonConstants.CASHIER_SESSION_NAME] as NGUOIDUNG;

            Result res = new Result();
            var nhanvien = db.NGUOIDUNGs.FirstOrDefault(n =>n.HoTenNV.Equals(tenNhanVien) && n.TrangThai == true);
            if (nhanvien == null)
            {
                res.res = -1;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            var calam = db.CALAMVIECs.FirstOrDefault(c => (c.TenCa.Equals(TenCaLam)));
            if(calam == null)
            {
                res.res = -2;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
           

            var cong = new CHAMCONG();
            cong.NhanVienID = nhanvien.NguoiDungID;
            cong.CaLam = calam.CaLamID;
            cong.TrangThai = true;
            cong.Ngay = DateTime.Now;
            cong.GhiChu = GhiChu;
            cong.NgayCham = DateTime.Now;

            QUYDINHDIMUON dimuon;
            if (DiMuon != null && !DiMuon.Equals(""))
            {
                dimuon = db.QUYDINHDIMUONs.FirstOrDefault(c => c.GhiChu.Equals(DiMuon));
                if (dimuon != null)
                    cong.DiMuon = dimuon.QuyDinhDiMuonID;
                else
                {
                    res.res = -3;//Đi muộn lỗi
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
            }

            db.CHAMCONGs.Add(cong);
            db.SaveChanges();

            ///Lưu vào nhật ký

            var hoatdong = new HOATDONG();
            hoatdong.NoiDung = "Chấm công " + nhanvien.HoTenNV;
            hoatdong.ThoiGian = DateTime.Now;
            hoatdong.LoaiHoatDong = 1;//nhóm hoạt động của thu ngân
            hoatdong.NguoiThucHien = thungan.NguoiDungID;
            db.HOATDONGs.Add(hoatdong);
            db.SaveChanges();

            res.res = cong.ChamCongID;
            return Json(res, JsonRequestBehavior.AllowGet);
        }


        public JsonResult XoaCong(int id)
        {
    
            NGUOIDUNG thungan = Session[Common.CommonConstants.CASHIER_SESSION_NAME] as NGUOIDUNG;

            Result res = new Result();
            var cong = db.CHAMCONGs.FirstOrDefault(n => n.ChamCongID == id);
            if(cong != null)
                {
               
                ///Lưu vào nhật ký
                var hoatdong = new HOATDONG();
                hoatdong.NoiDung = thungan.HoTenNV + " đã xóa công " + cong.NGUOIDUNG.HoTenNV;
                hoatdong.ThoiGian = DateTime.Now;
                hoatdong.LoaiHoatDong = 1;//nhóm hoạt động của thu ngân
                hoatdong.NguoiThucHien = thungan.NguoiDungID;
                db.HOATDONGs.Add(hoatdong);
                db.SaveChanges();

                db.CHAMCONGs.Remove(cong);
                db.SaveChanges();


                res.res = 1;
                return Json(res, JsonRequestBehavior.AllowGet);
            }

         

            res.res = -1;//không tồn tại công
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
    class Result
    {
        public int res { get; set; }
    }
}