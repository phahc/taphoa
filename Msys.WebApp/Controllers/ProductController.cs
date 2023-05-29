using Msys.WebApp.Util.Attributes;
using MSys.Core;
using MSys.Interface;
using MSys.Model;
using MSys.Model.Data;
using MSys.WebApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Msys.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProducts _productService;
        private readonly IOrder _orderService;
        private readonly ICustomer _accountServices;

        private List<Product> AllProducts { get; set; }


        private int _categorySession = 0;
        public int CategorySession
        {
            get
            {
                var js = new JavaScriptSerializer();
                if (Session[ConstSession.CategorySession] != null)
                {
                    _categorySession = js.Deserialize<int>(Session[ConstSession.CategorySession].ToString());
                }
                return _categorySession;
            }
            set
            {
                var js = new JavaScriptSerializer();
                Session[ConstSession.CategorySession] = js.Serialize(value);
            }
        }

        public ProductController(IProducts productService, IOrder orderService, ICustomer accountService)
        {
            this._productService = productService;
            this._orderService = orderService;
            this._accountServices = accountService;
        }
        // GET: Product
        public ActionResult Index(int? page)
        {
            int pageno = 0;
            pageno = page == null ? 1 : int.Parse(page.ToString());

            int pageSize = 20;
            int totalCount = 0;

            int productSkipCnt = (page == 1) ? 0 : (pageno - 1) * pageSize;

            //calling stored procedure to get paged data.
            if (this.CategorySession == 0)
            {
                AllProducts = _productService.GetProductsInfo();
            }
            else
            {
                AllProducts = _productService.GetProductsInfo().Where(p => p.categories == this.CategorySession).ToList();
            }
            
            //
            // setting total number of records
            totalCount = AllProducts.Count();

            AllProducts = AllProducts.Skip(productSkipCnt).Take(pageSize).ToList();

            PageHelper<Product> pager = new PageHelper<Product>(AllProducts.AsQueryable(), pageno, pageSize, totalCount);
            return View(pager);
        }

        // GET: Product
        [HttpPost]
        public ActionResult ProductListByCategory(int id)
        {
            //セッションシリアライズ用
            var js = new JavaScriptSerializer();

            int pageno = 1;

            int pageSize = 20;
            int totalCount = 0;
            //セッションに保存
            this.CategorySession = id;

            //calling stored procedure to get paged data.
            AllProducts = _productService.GetProductsInfoByCategoriesId(id);
            //
            // setting total number of records
            totalCount = AllProducts.Count();

            AllProducts = AllProducts.Skip(0).Take(20).ToList();

            PageHelper<Product> pager = new PageHelper<Product>(AllProducts.AsQueryable(), pageno, pageSize, totalCount);
            return PartialView("_ProductListPartial", pager);
        }

        // GET: Product
        [HttpGet]
        public ActionResult ProductDetail(int id)
        {
            var product = _productService.GetProductsInfoById(id.ToString());
             _productService.UpdateView(id.ToString());
            product.quantity = 1;
            return View("ProductDetail", product);
        }

        // GET: Product
        [HttpPost]
        [AuthorizedSessionFilter]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterOrder(Product model)
        {
            var service = this._orderService;
            /* Setup default variables that we are going to populate later */
            bool ret = true;
            var order = new Order();

            // Lấy danh sách đơn hàng
            var userInfo = this._accountServices.getCustomerByUserName(User.Identity.Name);
            order = service.GetActiveOrder(userInfo.userid);
            if (order == null)
            {
                // Get max code order
                string maxCode = service.CreateMaxOrderCode();
                if (string.IsNullOrEmpty(maxCode))
                    maxCode = string.Format("{0:00000000000000000}", 1);
                order = new Order()
                {
                    id = maxCode,
                    name = "HD-" + maxCode,
                    address = "",
                    email = "",
                    phone = "",
                    status = 0,
                    date = DateTime.Now,
                    active = 1,
                    userid = userInfo.userid
                };
                _orderService.RegisterOrder(order);
            }

            var productid = model.product_id.ToString();
            OrderDetailInfo orderDetail = new OrderDetailInfo()
            {
                id_order = order.id,
                id_product = productid
            };

            OrderDetail orderDet = new OrderDetail()
            {
                id_order = order.id,
                id_product = productid
            };
            var getOrderDetail = service.GetExistDetail(orderDet);
            if (getOrderDetail != null)
            {
                orderDetail.quantity = getOrderDetail.quantity + model.quantity.Value;
                ret = service.UpdateQuantityOrderDetail(orderDetail);
            }
            else
            {
                orderDetail.quantity = model.quantity.Value;
                ret = service.InsertOrderDetail(orderDetail);
            }

            var response = new Dictionary<string, string> {
                            { "content", "" }
            };

            return RedirectToAction("MyCart", "MyCart");
        }

    }
}