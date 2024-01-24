using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeOnline.Models;
using CafeOnline.Models.DAO;
using CafeOnline.Common;
using System.Configuration;
using Facebook;

namespace CafeOnline.Controllers
{
    public class NguoiDungController : Controller
    {
        //CafeOnlineDB db = new CafeOnlineDB();
        CafeOnlineDB db = CafeOnlineDB.ConnectDatabase();

        /// <summary>
        /// Page Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult KhachHangDangKyNhanh()
        {
            return View();
        }



        /// <summary>
        /// Khách hàng sử dụng form đăng ký nhanh
        /// </summary>
        /// <param name="kh"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KhachHangDangKyNhanh(KhachHangDangKyNhanh kh)
        {
            if (ModelState.IsValid)
            {

                var exits_user = db.NGUOIDUNGs.SingleOrDefault(n => n.TenDangNhap == kh.TenDangNhap);
                if (exits_user != null)
                {
                    ModelState.AddModelError("", "Tên đăng nhập " + kh.TenDangNhap + " đã được sử dụng");
                    return View();
                }
                exits_user = db.NGUOIDUNGs.SingleOrDefault(n => n.Email == kh.Email);
                if (exits_user != null)
                {
                    ModelState.AddModelError("", "Email " + kh.Email + " đã được sử dụng");
                    return View();
                }

                var new_user = new NGUOIDUNG();
                new_user.TenDangNhap = kh.TenDangNhap;
                new_user.Email = kh.Email;
                new_user.TrangThai = true;
                new_user.MatKhau = Encryptor.MD5Hash(kh.MatKhau);
                new_user.NgayTao = DateTime.Now;
                new_user.NhomSD = 4;
                new_user.SoLanDangNhap = 1;
                new_user.HoTenNV = kh.HoTenKH;
                new_user.GhiChu = "KH Đăng ký nhanh";

                db.NGUOIDUNGs.Add(new_user);
                db.SaveChanges();

                Session[CommonConstants.CUSTOMER_SESSION_NAME] = new_user;
                HttpCookie cookie = new HttpCookie(CommonConstants.COOKIE_ACCOUNT_NAME);
                cookie["TenDangNhap"] = new_user.TenDangNhap;
                cookie["MatKhau"] = new_user.MatKhau;
                cookie["NhomSD"] = "KhachHang";
                cookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(cookie);

                return View("Index", "Home");

            }
            else
            {
                ModelState.AddModelError("", "Đăng ký không thành công");
                return View();
            }

        }



        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        public ActionResult DangNhapBangFacebook()
        {
            var fb = new FacebookClient();
            var LoginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["fbAppID"],
                client_secret = ConfigurationManager.AppSettings["fbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",

            });
            return Redirect(LoginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["fbAppID"],
                client_secret = ConfigurationManager.AppSettings["fbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email,name,location");
                string email = me.email;
                string userName = me.email;
                string firstName = me.first_name;
                string middleName = me.middle_name;
                string lastName = me.last_name;
                string Name = me.name;
                string DiaCHi = "Unknown";
                var user = db.NGUOIDUNGs.SingleOrDefault(n => (n.Email == email || n.TenDangNhap == email));
                if (user == null)
                {
                    var ng = new NGUOIDUNG();
                    ng.TenDangNhap = email;
                    ng.Email = email;
                    ng.HoTenNV = Name;
                    ng.NhomSD = 4;
                    ng.GhiChu = "Đăng nhập bằng Facebook";
                    ng.DiaChi = DiaCHi;
                    Random rd = new Random();
                    int t = rd.Next(0900000000, 01699999999);
                    ng.DienThoai = "0" + Convert.ToString(t);
                    t = rd.Next(0, 1000000000);
                    ng.MatKhau = "fb_" + Common.Encryptor.MD5Hash(Convert.ToString(t));
                    ng.TrangThai = true;
                    ng.NgayTao = DateTime.Now;
                    ng.NgaySinh = DateTime.Now;
                    ng.LuongTheoGio = 0;
                    ng.NgayVaoLam = DateTime.Now;
                    ng.SoLanDangNhap = 1;

                    db.NGUOIDUNGs.Add(ng);
                    db.SaveChanges();
                    Session[CommonConstants.CUSTOMER_SESSION_NAME] = ng;

                }
                else
                    Session[CommonConstants.CUSTOMER_SESSION_NAME] = user;
            }
            return Redirect(CommonConstants.HOMEPAGE);
        }



    }
}