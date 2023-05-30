using Msys.WebApp.Models;
using Msys.WebApp.Util.Attributes;
using MSys.Interface;
using MSys.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace Msys.WebApp.Controllers
{
    public class MyCartController : Controller
    {
        private readonly IProducts _productService;
        private readonly IOrder _orderService;
        private readonly ICustomer _accountServices;


        public MyCartController(IProducts productService, IOrder orderService, ICustomer accountService)
        {
            this._productService = productService;
            this._orderService = orderService;
            this._accountServices = accountService;
        }
        // GET: MyCart
        [HttpGet]
        public ActionResult MyCart()
        {
            var detailModel = new MyCartInfo();
            var userInfo = this._accountServices.getCustomerByUserName(User.Identity.Name);
            if(userInfo != null)
            {
                var service = this._orderService;
                var order = service.GetActiveOrder(userInfo.userid);
                if (order != null)
                {
                    detailModel.DetailLst = service.GetOrderDetail(order.id);
                    detailModel.userNm = userInfo.fullname;
                    detailModel.userAddress = userInfo.address;
                    detailModel.userEmail = userInfo.email;
                    detailModel.userPhone = userInfo.phone;
                }
            }
            ViewBag.tax = ConfigurationManager.AppSettings["tax"];
            return View(detailModel);
        }

        // GET: Confirm
        [HttpGet]
        public ActionResult Confirm()
        {
            return View();
        }

        // GET: MyCart
        [HttpPost]
        [AuthorizedSessionFilter]
        [ValidateAntiForgeryToken]
        public ActionResult MyCart(MyCartInfo model, string controlVal)
        {
            ViewBag.tax = ConfigurationManager.AppSettings["tax"];
            var service = this._orderService;
            var serviceCus = this._accountServices;
            if (controlVal.Equals("up") || controlVal.Equals("down"))
            {
                foreach (var item in model.DetailLst)
                {
                    service.UpdateOrderDetail(item);
                }
            }
            else if(controlVal.Contains("delete"))
            {
                var proId = controlVal.Split('-')[1];
                var userInfo = this._accountServices.getCustomerByUserName(User.Identity.Name);
                var order = service.GetActiveOrder(userInfo.userid);
               
                model.DetailLst.Remove(model.DetailLst.Find(odr => odr.id_product == proId && odr.id_order == order.id));
                service.DeleteProductInOrderDetail(order.id, proId);
                return RedirectToAction("MyCart", "MyCart", model);
            }
            //else if (controlVal.Contains("update"))
            //{
            //    var userInfo = this._accountServices.getCustomerByUserName(User.Identity.Name);
            //    userInfo.fullname = model.userNm;
            //    userInfo.address = model.userAddress;
            //    userInfo.email = model.userEmail;
            //    userInfo.phone = model.userPhone;
            //    serviceCus.Update(userInfo);
            //}
            else if (controlVal.Contains("confirm"))
            {
                var userInfo = this._accountServices.getCustomerByUserName(User.Identity.Name);
                userInfo.fullname = model.userNm;
                userInfo.address = model.userAddress;
                userInfo.email = model.userEmail;
                userInfo.phone = model.userPhone;
                serviceCus.Update(userInfo);
                var order = service.GetActiveOrder(userInfo.userid);
                service.UpdateActiveOrder(order.id);
                return RedirectToAction("Confirm");
            }

            return View(model);
        }
    }
}