using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AspMvcAuth
{
    [ExcludeFromCodeCoverage]
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "MainProductList",
              url: "Home/ProductList/{id}",
              defaults: new { controller = "Home", action = "ProductList", id = UrlParameter.Optional }
           );
 
            routes.MapRoute(
               name: "Home",
               url: "Home/Home",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "login",
               url: "account/login",
               defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "ProductIndex",
               url: "product/main",
               defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "ProductListByCategory",
              url: "product/product_list_by_category",
              defaults: new { controller = "Product", action = "ProductListByCategory", id = UrlParameter.Optional }
           );

            routes.MapRoute(
             name: "ProductDetail",
             url: "product/product_detail",
             defaults: new { controller = "Product", action = "ProductDetail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "RegisterOrder",
               url: "product/registerorder/",
               defaults: new { controller = "Product", action = "RegisterOrder", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "AccountRegister",
            url: "account/register",
            defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional }
            );

            // Giỏ hàng
            routes.MapRoute(
           name: "MyCart",
           url: "mycart/mycart",
           defaults: new { controller = "MyCart", action = "MyCart", id = UrlParameter.Optional }
           );
            // Đặt hàng thành công
          routes.MapRoute(
           name: "MyCartConfirm",
           url: "mycart/confirm",
           defaults: new { controller = "MyCart", action = "Confirm", id = UrlParameter.Optional }
           );

            // Liên hệ
            routes.MapRoute(
             name: "ContactIndex",
             url: "contact/contact",
             defaults: new { controller = "Contact", action = "Contact", id = UrlParameter.Optional }
             );

            routes.MapRoute(
            name: "Error",
            url: "Error/Critical",
            defaults: new { controller = "Error", action = "Critical" }
             );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
