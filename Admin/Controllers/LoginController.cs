using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeOnline.Common;
using CafeOnline.Models;
using CafeOnline.Models.DAO;

namespace CafeOnline.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        //CafeOnlineDB db = new CafeOnlineDB();
        CafeOnlineDB db = CafeOnlineDB.ConnectDatabase();

        public ActionResult DangXuat()
        {
            Session[CommonConstants.ADMIN_SESSION_NAME] = null;
            if (Request.Cookies[CommonConstants.COOKIE_ACCOUNT_NAME] != null)
            {
                HttpCookie ck = new HttpCookie(CommonConstants.COOKIE_ACCOUNT_NAME);
                ck.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(ck);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (Session[CommonConstants.ADMIN_SESSION_NAME] != null)
                return RedirectToAction("Index", "TrangChu", new { area = "Admin" });

            HttpCookie cookie = Request.Cookies[Common.CommonConstants.COOKIE_ACCOUNT_NAME];
            if (cookie != null)
            {
                string ad_userName = cookie["TenDangNhap"].ToString();
                string ad_Password = cookie["MatKhau"].ToString();
                string passwword_MD5 = Encryptor.MD5Hash(ad_Password);

                var ad_cookie = db.NGUOIDUNGs.SingleOrDefault(n => n.TenDangNhap == ad_userName && n.MatKhau == passwword_MD5 && n.NhomSD == 1 && n.TrangThai == true);
                if (ad_cookie != null)
                {
                    Session[Common.CommonConstants.ADMIN_SESSION_NAME] = ad_cookie;
                    return RedirectToAction("Index", "TrangChu", new { area = "Admin" });
                }
            }


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DangNhap tk)
        {
            if (ModelState.IsValid)
            {
                int t = new UserDAO().DangNhap(tk.UserName, tk.Password, tk.NhoTaiKhoan);

                //
                if (t == -1)
                {
                    ModelState.AddModelError("", "Tên đăng nhập không tồn tại");
                    return View();
                }
                else
                if (t == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                    return View();
                }
                else
                {
                    var user = new UserDAO().getUserByID(t);
                    if (user == null)
                    {
                        ModelState.AddModelError("", "Lỗi không xác định!");
                        return View();
                    }
                    else
                        if (user.NhomSD == 1)//Admin
                        return RedirectToAction("Index", "TrangChu", new { area = "Admin" });
                    else
                    {
                        ModelState.AddModelError("", "Bạn phải đăng nhập bằng tài khoản quản trị");
                        return View();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Đăng nhập không thành công!");
                return View();
            }




        }

    }
}
