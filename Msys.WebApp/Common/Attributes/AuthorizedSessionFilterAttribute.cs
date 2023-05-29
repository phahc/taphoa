using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Msys.WebApp.Util.Attributes
{
    /// <summary>
    /// Kiem soat cac phien lam viec(Kiem tra xac thuc)
    /// </summary>
    public class AuthorizedSessionFilterAttribute : AuthorizeAttribute
    {
        public AuthorizedSessionFilterAttribute()
        {
            // Kiem soat thu tu. Thu tu cang cao uu tien cang cao
            Order = 110;
        }
        // Properties
        public String SessionNm { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //#region Xu ly thuc hien truoc khi bat dau 1 thao tac
            HttpRequestBase request = filterContext.HttpContext.Request;
            HttpResponseBase response = filterContext.HttpContext.Response;
            if (SessionNm != null)
            {
                if (filterContext.HttpContext.Session[SessionNm] == null)
                {
                    response.Redirect("/error/sessiontimeout/");
                }
            }
            else
            {
                base.OnAuthorization(filterContext);
            }
            //#endregion

            //// Redirect to the login page if necessary
            //if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    var helper = new UrlHelper(filterContext.RequestContext);
            //    var url = helper.Action("Login", "Account");
            //    //filterContext.Result = new RedirectResult(System.Web.Security.FormsAuthentication.LoginUrl + "?returnUrl=" + filterContext.HttpContext.Request.Url);
            //    //return;
            //    //FormsAuthentication.SignOut();
            //    //Chuyển hướng đến trang hiện tại và để điều khiển xác thực cho mỗi màn hình
            //    filterContext.Result = new RedirectResult(url);
            //}

            //// Redirect to your "access denied" view here
            //if (filterContext.Result is HttpUnauthorizedResult)
            //{
            //    filterContext.Result = new RedirectResult("~/Account/Login");
            //}
            //base.OnAuthorization(filterContext);
        }
    }
}