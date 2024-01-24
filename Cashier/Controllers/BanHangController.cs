using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeOnline.Models;

namespace CafeOnline.Areas.Cashier.Controllers
{
    public class BanHangController : BaseCashierController
    {


        CafeOnlineDB db = CafeOnlineDB.ConnectDatabase();
        
        [HttpGet]
        public ActionResult Order()
        {
            return RedirectToAction("Index", "TongQuan", new { area = "Cashier" });
        }


        [HttpPost]
        public ActionResult Order(FormCollection form)
        {
            NGUOIDUNG thungan = Session[Common.CommonConstants.CASHIER_SESSION_NAME] as NGUOIDUNG;

            int soBan;
            int.TryParse(form["ip_SoBan"].ToString(), out soBan);


            string MH = form["ip_mon"].ToString();
            int SoLuong;
            int.TryParse(form["ip_sl"].ToString(), out SoLuong);
            string NhanVien = form["ip_manv"].ToString();
            string GhiChu = form["ip_ghi_chu"].ToString();

            ////Validate data input

            int flag = 0;//Không lỗi
            if (SoLuong <= 0) flag = -1;//Lỗi số lượng
            MATHANG mon = db.MATHANGs.FirstOrDefault(n => n.TenMatHang.Equals(MH));
            if (mon == null)
                flag = -2;//Tên món lỗi

            NGUOIDUNG nhanvien = db.NGUOIDUNGs.FirstOrDefault(n => n.HoTenNV.Equals(NhanVien));
            if (nhanvien == null)
                flag = -3;//Lỗi tên nhân viên
            if (flag == 0)
            {
                int SoHD = TableStatus.Get_HoaDonID_by_TableNumber(soBan);


                if (SoHD != -1)//Đã có hóa đơn
                {
                    CTHD order = new CTHD();

                    MATHANG mh = db.MATHANGs.FirstOrDefault(n => n.TenMatHang == MH);
                    NGUOIDUNG nv = db.NGUOIDUNGs.FirstOrDefault(n => n.HoTenNV == NhanVien);

                    //Order món
                    //Lưu vào CSDL
                    order.SoHD = SoHD;
                    order.MatHang = mh.MatHangID;
                    order.NhanVienPhucVu = nv.NguoiDungID;
                    order.GhiChu = GhiChu;
                    order.SoLuong = SoLuong;
                    order.TrangThai = true;

                    db.CTHDs.Add(order);
                    db.SaveChanges();

                    ///Cập nhật tổng tiền
                    decimal tongtienHD = 0;
                    var hd = db.HOADONs.FirstOrDefault(n => n.HoaDonID == SoHD);
                    var lsCT = db.CTHDs.Where(n => n.SoHD == hd.HoaDonID).ToList();
                    foreach (var i in lsCT)
                    {
                        tongtienHD += (decimal)(i.MATHANG1.DonGia * i.SoLuong);
                    }

                    hd.TongTien = tongtienHD;
                    db.HOADONs.Attach(hd);
                    var entry = db.Entry(hd);
                    entry.State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                }
                else//Chưa có hóa đơn: Bàn đang trống
                {
                    HOADON hd_new = new HOADON();
                    hd_new.SoBan = soBan;
                    hd_new.KhachHang = Common.CommonConstants.anonymousUserID ;
                    hd_new.ThoiGianVao = DateTime.Now;
                    hd_new.ThoiGianRa = Common.CommonConstants.minDateTime;
                    hd_new.TongTien = 0;
                    hd_new.TrangThai = true;
                    hd_new.TrangThaiHoaDon = 1;
                    hd_new.GiamGia = 0;
                    hd_new.GhiChu = "";

                    db.HOADONs.Add(hd_new);
                    db.SaveChanges();

                    //var hoatdong = new HOATDONG();
                    //hoatdong.NoiDung = "Khách vào bàn " + hd_new.SoBan;
                    //hoatdong.ThoiGian = DateTime.Now;
                    //hoatdong.LoaiHoatDong = 1;//nhóm hoạt động của thu ngân
                    //hoatdong.NguoiThucHien = thungan.NguoiDungID;
                    //db.HOATDONGs.Add(hoatdong);

                    CTHD order = new CTHD();

                    MATHANG mh = db.MATHANGs.FirstOrDefault(n => n.TenMatHang == MH);
                    NGUOIDUNG nv = db.NGUOIDUNGs.FirstOrDefault(n => n.HoTenNV == NhanVien);

                    order.SoHD = hd_new.HoaDonID;
                    order.MatHang = mh.MatHangID;
                    order.NhanVienPhucVu = nv.NguoiDungID;
                    order.GhiChu = GhiChu;
                    order.SoLuong = SoLuong;
                    order.TrangThai = true;

                    db.CTHDs.Add(order);
                    db.SaveChanges();

                    var hoatdong2 = new HOATDONG();
                    hoatdong2.NoiDung = "Thêm khách vào bàn " + hd_new.SoBan;
                    hoatdong2.ThoiGian = DateTime.Now;
                    hoatdong2.LoaiHoatDong = 1;//nhóm hoạt động của thu ngân
                    hoatdong2.NguoiThucHien = thungan.NguoiDungID;
                    db.HOATDONGs.Add(hoatdong2);

                    ///Cập nhật tổng tiền
                    decimal tongtienHD = 0;
                    var lsCT = db.CTHDs.Where(n => n.SoHD == hd_new.HoaDonID).ToList();
                    foreach (var i in lsCT)
                    {
                        tongtienHD += (decimal)(i.MATHANG1.DonGia * i.SoLuong);
                    }

                    hd_new.TongTien = tongtienHD;
                    db.HOADONs.Attach(hd_new);
                    var entry = db.Entry(hd_new);
                    entry.State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            ViewBag.Ban = soBan;
            return Redirect("/Cashier/TongQuan/?ban=" + soBan.ToString());
        }



        public JsonResult ThanhToan(int ban)
        {
            
            int SoHD = TableStatus.Get_HoaDonID_by_TableNumber(ban);
            if (SoHD == -1)
            {
                return Json(-1, JsonRequestBehavior.AllowGet);//không tìm được hóa đơn
            }
            else
            {
                var hd = db.HOADONs.FirstOrDefault(n => n.HoaDonID == SoHD);
                hd.ThoiGianRa = DateTime.Now;
                hd.TrangThaiHoaDon = 0;
                hd.TrangThai = false;

                db.HOADONs.Attach(hd);
                db.Entry(hd).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                NGUOIDUNG thungan = Session[Common.CommonConstants.CASHIER_SESSION_NAME] as NGUOIDUNG;
                var hoatdong2 = new HOATDONG();
                hoatdong2.NoiDung = "Khách bàn " + hd.SoBan + " thanh toán";
                hoatdong2.ThoiGian = DateTime.Now;
                hoatdong2.LoaiHoatDong = 1;//nhóm hoạt động của thu ngân
                hoatdong2.NguoiThucHien = thungan.NguoiDungID;
                db.HOATDONGs.Add(hoatdong2);
                db.SaveChanges();

                return Json(1, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult InHoaDon(int ban)
        {
            int SoHD = TableStatus.Get_HoaDonID_by_TableNumber(ban);
            if (SoHD == -1)
            {
                return Json(-1, JsonRequestBehavior.AllowGet);//không tìm được hóa đơn
            }
            else
            {
                var hd = db.HOADONs.FirstOrDefault(n => n.HoaDonID == SoHD);
                hd.TrangThaiHoaDon = 2;
                db.HOADONs.Attach(hd);
                db.Entry(hd).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                NGUOIDUNG thungan = Session[Common.CommonConstants.CASHIER_SESSION_NAME] as NGUOIDUNG;
                var hoatdong2 = new HOATDONG();
                hoatdong2.NoiDung = "In hóa đơn bàn " + hd.SoBan + "";
                hoatdong2.ThoiGian = DateTime.Now;
                hoatdong2.LoaiHoatDong = 1;//nhóm hoạt động của thu ngân
                hoatdong2.NguoiThucHien = thungan.NguoiDungID;
                db.HOATDONGs.Add(hoatdong2);
                db.SaveChanges();

                return Json(1, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Lấy thông tin bàn theo số bàn
        /// </summary>
        /// <param name="ban"></param>
        /// <returns></returns>
        public JsonResult HoaDonBan(int ban)
        {
            db.Configuration.ProxyCreationEnabled = false;
            /// <summary>
            /// 0: Da thanh toan
            /// 1: Dang co khach, Chua in Hoa Don
            /// 2: Da in hoa don
            /// -1: Lỗi Null
            /// </summary>
            var HD_Ban = db.HOADONs.FirstOrDefault(n => n.SoBan == ban && (n.TrangThaiHoaDon == 2 || n.TrangThaiHoaDon == 3));
            if (HD_Ban != null)
            {
                return Json(HD_Ban, JsonRequestBehavior.AllowGet);
            }
            else
            {
                HD_Ban = db.HOADONs.FirstOrDefault(n => n.HoaDonID == 9);
                HD_Ban.TrangThaiHoaDon = -1; // Lỗi Null
                return Json(HD_Ban, JsonRequestBehavior.AllowGet);
            }
        }



        /// <summary>
        /// Lấy danh sách hàng đã đặt theo số hóa đơn
        /// </summary>
        /// <param name="HD"></param>
        /// <returns></returns>
        public JsonResult ListCTHD(int HD)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<CTHD> lsct = db.CTHDs.Where(n => n.SoHD == HD).ToList();
            if (lsct != null)
                return Json(lsct, JsonRequestBehavior.AllowGet);
            else
            {
                //Trả về một list ctht ảo, đánh dấu tránh lỗi null của ctdh
                lsct = db.CTHDs.Where(n => n.SoHD == 9 && n.NhanVienPhucVu == 7 && n.TrangThai == false).ToList();
                return Json(lsct, JsonRequestBehavior.AllowGet);
            }
        }


    }
}