using CafeOnline.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeOnline.Models.DAO
{
    public class UserDAO
    {
        //CafeOnlineDB db = new CafeOnlineDB();
        CafeOnlineDB db = CafeOnlineDB.ConnectDatabase();
        /// <summary>
        /// Đăng nhập hệ thôbgs
        /// </summary>
        /// <param name="taikhoan"></param>
        /// <param name="matkhau"></param>
        /// <param name="nhotaikhoan"></param>
        /// <returns>-1: lỗi k có tài khoản</returns>
        ///  <returns>-2: mật khẩu sai</returns>
        ///  <returns>-3: Tài khoản bị khóa</returns>
        ///   <returns>id: id người dùng</returns>
        public int DangNhap(string taikhoan, string matkhau, bool nhotaikhoan)
        {
            var user = db.NGUOIDUNGs.FirstOrDefault(n => n.TenDangNhap == taikhoan || n.Email == taikhoan);
            if (user == null)
                return -1; // lỗi -1: không tồn tại tài khoản
            string passwword_MD5 = Encryptor.MD5Hash(matkhau);
            user = db.NGUOIDUNGs.SingleOrDefault(n => ((n.TenDangNhap == taikhoan || n.Email == taikhoan) && n.MatKhau == passwword_MD5));
            if (user == null)
                return -2; // lỗi -2: mật khẩu không đúng

            else
            {
                user.SoLanDangNhap = user.SoLanDangNhap + 1;
                user.LanCuoiDangNhap = DateTime.Now;
                db.SaveChanges();

                if (user.TrangThai == false)
                    return -3;
                //Add session
                if (user.NhomSD == 1) HttpContext.Current.Session[CommonConstants.ADMIN_SESSION_NAME] = user;
                else
                       if (user.NhomSD == 2) HttpContext.Current.Session[CommonConstants.CASHIER_SESSION_NAME] = user;
                else
                       if (user.NhomSD == 3) HttpContext.Current.Session[CommonConstants.WAITER_SESSION_NAME] = user;
                else
                    HttpContext.Current.Session[CommonConstants.CUSTOMER_SESSION_NAME] = user;

                //add cookie
                if (nhotaikhoan)
                {
                    HttpCookie cookie = new HttpCookie(CommonConstants.COOKIE_ACCOUNT_NAME);
                    cookie["TenDangNhap"] = taikhoan;
                    cookie["MatKhau"] = matkhau;
                    if (user.NguoiDungID == 1) cookie["NhomSD"] = "1";
                    else
                        if (user.NguoiDungID == 2) cookie["NhomSD"] = "2";
                    else
                        if (user.NguoiDungID == 3) cookie["NhomSD"] = "3";
                    else
                        cookie["NhomSD"] = "4";
                    cookie.Expires = DateTime.Now.AddDays(15);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
                //Đăng nhập thành công trả về id người dùng
                return user.NguoiDungID;
            }

        }

        public NGUOIDUNG getUserByID(int ID)
        {
            return db.NGUOIDUNGs.SingleOrDefault(n => n.NguoiDungID == ID);
        }
    }
}