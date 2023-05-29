using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AspMvcAuth
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            UnityConfig.RegisterComponents();

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;
        }

        /// <summary>
        /// Loi xu ly MVC
        /// </summary>
        protected void Application_Error(object sender, EventArgs e)
        {
            int num;
            if (Server != null)
            {
                // Duong goi khi xay ra loi
                System.Data.SqlClient.SqlException sqlException = null;
                var ex = Server.GetLastError();
                if (ex != null)
                {
                    if (ex is HttpException &&
                        ((HttpException)ex).GetHttpCode() == (int)HttpStatusCode.NotFound)
                    {
                        // Loi bo qua dang nhap
                        Response.Redirect("/error/notfound/");
                        return;
                    }

                    // Ngoai le cua SQL
                    sqlException = ex as System.Data.SqlClient.SqlException;
                    if (sqlException != null)
                    {
                        num = sqlException.Number;
                        Response.Redirect("/error/systemerror/");
                        return;
                    }
                    // Cac ngoai le gay chet he thong
                    Response.Redirect("/error/systemerror/");
                }
            }
        }

        #region PreSendRequestHeaders
        /// <summary>
        /// Xoa cac mo ta ve Server khoi header cua thong tin phan hoi
        /// </summary>
        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");
        }
        #endregion

        #region Kiem soat session
        /// <summary>
        /// Khi bat dau 1 hanh dong, se goi thao tac nay
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Session_Start(object sender, EventArgs e)
        {
            if (Session["Session_Start"] == null)
            {
                Session.Add("Session_Start", DateTime.Now);
            }
        }
        #endregion
    }
}
