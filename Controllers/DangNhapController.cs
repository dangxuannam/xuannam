using CafeOnline.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeOnline.Models;
using CafeOnline.Common;

namespace CafeOnline.Controllers
{
    public class DangNhapController : Controller
    {
        // GET: DangNhap
        [HttpGet]
        public ActionResult Index()
        { return View(); }



        /// <summary>
        /// Đăng nhập hệ thống
        /// </summary>
        /// <param name="tk"></param>
        /// <returns>view tương ứng với nhóm sử dụng</returns>
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
                if (t == -3)
                {
                    ModelState.AddModelError("", "Tài khoản này đang bị tạm khóa!");
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
                        if (user.NhomSD == 2)//Cashier Staff
                        return RedirectToAction("Index", "TongQuan", new { area = "Cashier" });
                    else
                        if (user.NhomSD == 3)//Waiter Staff
                        return RedirectToAction("Index", "",new { area = "Waiter" });
                    else
                        return RedirectToAction("Index", "Home");

                }
            }
            else
            {
                ModelState.AddModelError("", "Đăng nhập không thành công!");
                return View();
            }

        }
        public ActionResult DangXuat()
        {
            Session[CommonConstants.ADMIN_SESSION_NAME] = null;
            Session[CommonConstants.CASHIER_SESSION_NAME] = null;
            Session[CommonConstants.CUSTOMER_SESSION_NAME] = null;
            Session[CommonConstants.WAITER_SESSION_NAME] = null;

            if (Request.Cookies[CommonConstants.COOKIE_ACCOUNT_NAME] != null)
            {
                HttpCookie ck = new HttpCookie(CommonConstants.COOKIE_ACCOUNT_NAME);
                ck.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(ck);
            }

            return Redirect(CommonConstants.HOMEPAGE);
        }
    }
}