using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CafeOnline.Models;
using CafeOnline.Common;

namespace CafeOnline.API
{
    public class Services : IService
    {
        CafeOnlineDB db = CafeOnlineDB.ConnectDatabase();
        /// <summary>
        /// Đăng nhập nhân viên
        /// </summary>
        /// <param name="TenDangNhap"></param>
        /// <param name="MatKhau"></param>
        /// <returns>id nhân viên</returns>
        public int DangNhap(string taikhoan, string matkhau)
        {
            var user = db.NGUOIDUNGs.SingleOrDefault(n => n.TenDangNhap == taikhoan || n.Email == taikhoan);
            if (user == null)
                return -1; // lỗi -1: không tồn tại tài khoản
            string passwword_MD5 = Encryptor.MD5Hash(matkhau);
            user = db.NGUOIDUNGs.SingleOrDefault(n => ((n.TenDangNhap == taikhoan || n.Email == taikhoan) && n.MatKhau == passwword_MD5));
            if (user == null)
                return -2; // lỗi -2: mật khẩu không đúng

            else
            {
                user = db.NGUOIDUNGs.SingleOrDefault(n => ((n.TenDangNhap == taikhoan || n.Email == taikhoan) && n.MatKhau == passwword_MD5 && (n.NhomSD == 1 || n.NhomSD == 3)));
                if (user == null)
                    return -3;//Nhóm sử dụng không đúng
                else
                    return user.NguoiDungID;
            }
        }
        public List<MATHANG> Menu()
        {
            return db.MATHANGs.ToList();
        }

        public int Order(int soban, string maNV, string maMH, int SoLuong, string GhiChu, string maKH)
        {
            return -1;
        }

        public NGUOIDUNG XemTaiKhoan(int id)
        {
            return db.NGUOIDUNGs.SingleOrDefault(n => n.NguoiDungID == id);
        }
    }
}