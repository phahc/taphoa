using MSys.Core;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Msys.WebApp.Util.Attributes
{
    /// <summary>
    /// Bộ lọc kiểm soát trạng thái xác thực
    /// </summary>
    public class AuthorizeFilterAttribute : ActionFilterAttribute
    {
        public AuthorizeFilterAttribute()
        {
            Order = 100;
        }

        #region Xử lý ngay trước mỗi lần thực thi hành động
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;
            HttpResponseBase response = filterContext.HttpContext.Response;
            if (request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                if (request.IsAuthenticated)
                {
                    #region Lấy vé xác thực từ cookie
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(request.Cookies[FormsAuthentication.FormsCookieName].Value);
                    Msys.WebApp.Models.LoginViewModel.Login userInfo = js.Deserialize<Msys.WebApp.Models.LoginViewModel.Login>(ticket.UserData);
                    #endregion

                    bool isSessionEnd = HttpContext.Current.Session[ConstSession.TIMEOUT] == null ? true : false;
                    //Chỉ những người chưa đăng ký để tiếp tục đăng nhập để thực hiện kiểm tra xác thực
                    if (!ticket.IsPersistent)
                    {
                        #region Kiểm tra thời gian chờ của phiên
                        if (isSessionEnd)
                        {
                            try
                            {
                                this.LogOff(response);
                                //Chuyển hướng đến trang hiện tại và để điều khiển xác thực cho mỗi màn hình
                                filterContext.Result = new RedirectResult(request.RawUrl);
                            }
                            catch
                            {
                            }
                            return;
                        }
                        #endregion

                        #region So sánh cookie và phiên thông tin xác thực
                        if (!this.CompareCookieAndSession(ticket, userInfo.UserName))
                        {
                            try
                            {
                                this.LogOff(response);
                                //Chuyển hướng đến trang hiện tại và để điều khiển xác thực cho mỗi màn hình
                                filterContext.Result = new RedirectResult(request.RawUrl);
                            }
                            catch { }
                            return;
                        }
                        #endregion
                    }

                }
            }

            #region Tắt bộ nhớ đệm
            response.Cache.SetCacheability(HttpCacheability.NoCache);
            response.Cache.SetNoStore();
            #endregion
        }
        #endregion

        #region So sánh ID thành viên của cookie và ID thành viên của phiên
        /// <summary>
        /// So sánh ID thành viên của cookie và ID thành viên của phiên
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private Boolean CompareCookieAndSession(FormsAuthenticationTicket ticket, string dbCandNo)
        {
            if (ticket == null) return true;

            // Truy xuất thông tin đăng nhập phiên
            //if (HttpContext.Current.Session[ConstSession.COMPARE_COOKIE] == null) return false;
            //string compareSession = (string)(HttpContext.Current.Session[ConstSession.COMPARE_COOKIE]);
            // So sánh cookie và ID đăng nhập phiên
            //if (dbCandNo != compareSession)
            //{
            //    return false;
            //}
            return true;

        }
        #endregion

        #region Đăng xuất
        /// <summary>
        ///  Đăng xuất
        /// </summary>
        /// <remarks></remarks>
        private void LogOff(HttpResponseBase response)
        {
            FormsAuthentication.SignOut();
        }
        #endregion
    }
}