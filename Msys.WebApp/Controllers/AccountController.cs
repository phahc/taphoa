using Msys.WebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using MSys.Interface;
using MSys.DataAccessLayer;
using MSys.Core.Util;
using MSys.Model.Data;
using MSys.Core;
using System.Web;
using System.Web.Security;
using System;
using System.Web.Script.Serialization;

namespace Msys.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationManager _auth;
        private readonly ICustomer _accountServices;

        public AccountController(IAuthenticationManager auth, ICustomer accountServices)
        {
            this._auth = auth;
            this._accountServices = accountServices;
        }

        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            //if (this._accountServices.getUsers().Any(a => a.acc == model.UserName && a.pass == model.Password))
            var userInfo = this._accountServices.getCustomerByUserName(model.LoginInfo.UserName);
            if (userInfo != null)
            {
                if (ComputeHashUtil.VerifyHash(model.LoginInfo.Password, userInfo.username.ToString(), userInfo.password))
                {
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, model.LoginInfo.UserName), }, DefaultAuthenticationTypes.ApplicationCookie);

                    this._auth.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = model.LoginInfo.RememberMe
                    }, identity);

                    var js = new JavaScriptSerializer();
                    FormsAuthenticationTicket ticket =
                      new FormsAuthenticationTicket(
                      1,
                      model.LoginInfo.UserName,
                      DateTime.Now,
                      DateTime.MaxValue,
                      model.LoginInfo.RememberMe,
                      js.Serialize(model),
                      FormsAuthentication.FormsCookiePath
                      );

                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName);
                    authCookie.Value = encTicket;

                    if (model.LoginInfo.RememberMe)
                    {
                        authCookie.Expires = DateTime.MaxValue;
                    }
                    // Lưu trữ vé xác thực trong cookie
                    HttpContext.Response.Cookies.Add(authCookie);
                    #region Phiên để lọc
                    //ID chứng chỉ xác thực cửa hàng trong phiên
                    HttpContext.Session.Add(ConstSession.COMPARE_COOKIE, model.LoginInfo.UserName.ToString());
                    // Tạo phiên để kiểm tra thời gian chờ
                    HttpContext.Session.Add(ConstSession.TIMEOUT, DateTime.Now);
                    #endregion

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("LoginFailed", "Tên đăng nhập hoặc mật khẩu không đúng");
                return View(model);
            }
            else
            {
                ModelState.AddModelError("LoginError", "Nỗ lực đăng nhập thất bại .");
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(LoginViewModel model)
        {
            if (model.RegisterInfo.Password.Equals(model.RegisterInfo.RePassword))
            {
                ModelState.AddModelError("WrongPass", "Mật khẩu nhập lại không chính xác.");
                return RedirectToAction("Login", model);
            }

            var customer = new Customer();
            customer.password = ComputeHashUtil.ComputeHash(model.RegisterInfo.Password, model.RegisterInfo.UserName);
            customer.username = model.RegisterInfo.UserName;
            var cus = _accountServices.getCustomer();
            if (cus != null && cus.Count() > 0)
            {
                customer.userid = _accountServices.getCustomer().Max(m => m.userid) + 1;
            }
            else
            {
                customer.userid = 1;
            }
            customer.enable_account = 1;
            string stt = _accountServices.Register(customer);
            if (stt.Equals(ConstStatus.Existed))
            {
                ModelState.AddModelError("AccExist", "Tài khoản không hợp lệ.");
                return RedirectToAction("Login", model);
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            this._auth.SignOut();
            return RedirectToAction("Login", "Account");
        }

    }
}