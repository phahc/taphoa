using MSys.Model;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System;
using MSys.Core.Util;
using MSys.Interface;
using MSys.Model.Data;
using Home;
using Msys.WebApp.Models;

namespace MSys.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProducts _productService;
        private readonly IOrder _orderService;
        private readonly ICategories _categoryService;
        private readonly ITopics _topicService;
        private readonly IMenu _memuService;

        public HomeController(IProducts productService, IOrder orderServices, ICategories categoryServices, ITopics topicServices, IMenu menuServices)
        {
            this._productService = productService;
            this._orderService = orderServices;
            this._categoryService = categoryServices;
            this._topicService = topicServices;
            this._memuService = menuServices;

            MenuHelper.Init(_memuService);
        }

        HomeModel homeModel = new HomeModel();
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            var listOrderInfo = _orderService.GetOrderListNotPay();
            var listMenuInfo = _memuService.GetMenuInfo();
            var listProductInfo = _productService.GetProductsInfo();
            var listMainTopicInfo = _topicService.GetMainTopicInfo(1);
            var listRightTopicInfo = _topicService.GetMainTopicInfo(0);
            var firstCategoriesInfo = _categoryService.GetCategoriesInfo(listMainTopicInfo.FirstOrDefault().id);
            this.homeModel.FUModel.ListOrderNotPayInfo = listOrderInfo;
            this.homeModel.FUModel.MenuInfo = listMenuInfo;
            this.homeModel.FUModel.MainTopicInfo = listMainTopicInfo;
            this.homeModel.FUModel.RightTopicInfo = listRightTopicInfo;
            return View(homeModel.FUModel);
        }

        // GET: Product
        [HttpPost]
        public string ProductList(FormCollection collection)
        {
            var service = this._productService;
            /* Setup default variables that we are going to populate later */
            var pag_content = "";

            /* Define all posted data coming from the view. */
            string id = collection["data[id]"].Split('-')[1]; /* Keyword provided on our search box */
            var lstProduct = service.GetProductsInfoByCategoriesId(Convert.ToInt32(id));

            /* Let's build the query using available data that we received form the front-end via ajax */
            var all_items_query = lstProduct
                .Where(x => Convert.ToDouble(x.product_id) != 0); /* Get only the products to display. */

            /* Get total items in our database */
            var count_query = lstProduct
                .Where(x => Convert.ToDouble(x.product_id) != 0); /* Get total products count. */

            /* We now fetch the data from our database */
            var all_items = all_items_query.ToList();
            int count = count_query.Count();

            /* Loop through each item to create views */
            if (count > 0)
            {
                foreach (var item in all_items)
                {
                    string imageStt = "";
                    if (item.price_percent != 0)
                        imageStt = "<img src ='/Content/images/home/sale.png' class='new' alt='' />";
                    else if (item.create_date != null && DateTime.Now.Subtract(Convert.ToDateTime(item.create_date)).Days <= 7)
                        imageStt = "<img src ='/Content/images/home/new.png' class='new' alt='' />";

                    var date = item.create_date?.ToString("dd/MM/yyyy");
                        pag_content += "<div class='col-sm-4'>" +
                        "<div class='product-image-wrapper'>" +
                           "<div class='single-products'>"+
                               "<div class='productinfo text-center'> " +
                                    "<img src ="+ item.image_url +" alt='' />"+
                                    "<h2>" + DecimalUtil.DecimalToMoney(item.price) + "</h2>" +
                                    "<p>" + item.product_name + "</p>" +
                                    "<a class='btn btn-default add-to-cart' id='add_to_cart-"+ item.product_id + "'><i class='fa fa-shopping-cart'></i>Thêm vào giỏ hàng</a>" +
                                    //"<p class='list-group-item-text'>Số lượng hiện có</p>" +
                                    //"<h4 class='list-group-item-heading item-stock'>" + item.quantity + "</h4>" +
                                    "<ul class='list-inline kp-metadata clearfix'>" +
                                        //"<li class='kp-time'>" + date + "</li>" +
                                        "<li class='kp-view'><span class='icon-eye pull-left'></span>" + item.views + "</li>" +
                                    "</ul>" +
                                "</div>" +
                              imageStt +
                            "</div>" +
                            "<div class='choose'>"+
                                "<ul class='nav nav-pills nav-justified'>"+
                                    "<li><a href ='#' class='btn_select_product' id='btn_select_product-" + item.product_id + "'><i class='fa fa-plus-square'></i>Sách yêu thích</a></li>" +
                                    "<li><a href ='#' class='btn_new_cart' id='btn_new_cart-" + item.product_id + "'><i class='fa fa-plus-square'></i>So sánh</a></li>" +
                               "</ul>" +
                            "</div>" +
                        "</div>" +
                    "</div>";
                }
            }
            else
            {
                pag_content = "";
            }

            /* Lets put our variables in a dictionary */
            var response = new Dictionary<string, string> {
        { "content", pag_content }
        //{ "navigation", pag_navigation }
         };

            /* Then we return the Dictionary in json format to our front-end */
            string json = new JavaScriptSerializer().Serialize(response);
            return json;
        }

        // GET: Product
        [HttpPost]
        public bool UpdateOrderDetail(FormCollection collection)
        {
            string orderid = collection["data[orderid]"]; 
            string productid = collection["data[productid]"];
            int quantity = (collection["data[quantity]"] != null) ? Convert.ToInt32(collection["data[quantity]"]) : 0; /* Keyword provided on our search box */
            OrderDetailInfo orderDetail = new OrderDetailInfo()
            {
                id_order = orderid,
                id_product = productid,
                quantity = quantity
            };

            return this._orderService.UpdateOrderDetail(orderDetail);
        }

        // GET: Product
        [HttpPost]
        public string RegisterOrder(FormCollection collection)
        {
            var service = this._orderService;
            bool ret = true;

            string maxCode = service.CreateMaxOrderCode();
            if (string.IsNullOrEmpty(maxCode))
                maxCode = string.Format("{0:00000000000000000}", 1);
            Order order = new Order()
            {
                id = maxCode,
                name = "HD-" + maxCode,
                address = "",
                email = "",
                phone = "",
                status = 0,
                date = DateTime.Now,
                active = 1
            };

            //
            service.UpdateNotActiveOrder(maxCode);
            // Insert order
            service.RegisterOrder(order);
            // Insert order detail
            if(ret)
            {
                string productid = collection["data[productid]"]; /* Keyword provided on our search box */
                OrderDetailInfo orderDetail = new OrderDetailInfo()
                {
                    id_order = maxCode,
                    id_product = productid,
                    quantity = 1
                };
                ret = service.InsertOrderDetail(orderDetail);
            }
           
            var response = new Dictionary<string, string> {
                { "content", "" }
            };
            /* Then we return the Dictionary in json format to our front-end */
            string json = new JavaScriptSerializer().Serialize(response);
            return json;
        }

        // javascript sửa dụng ajax gọi tới hàm này
        [HttpPost]
        public string Search(FormCollection collection)
        {
            var service = this._productService;
            var pag_content = "";
            var pag_navigation = "";

            string search = collection["data[search]"]; 
            var lstProduct = service.GetProductsInfo();

            var all_items_query = lstProduct
                .Where(x => Convert.ToDouble(x.product_id) != 0); 

            var count_query = lstProduct
                .Where(x => Convert.ToDouble(x.product_id) != 0); 

            if (!string.IsNullOrEmpty(search))
            {
                all_items_query = lstProduct.Where(x =>
                    x.product_id.ToString().Contains(search) ||
                    x.product_name.ToLower().Contains(search.ToLower())
                );
                count_query = lstProduct.Where(x =>
                    x.product_id.ToString().Contains(search) ||
                    x.product_name.ToLower().Contains(search.ToLower())
                );
            }

            var all_items = all_items_query.ToList();
            int count = count_query.Count();

            if (count > 0 && !string.IsNullOrEmpty(search))
            {
                foreach (var item in all_items)
                {
                    pag_content += "<div class='col-sm-3 item-" + item.product_id + "'>" +
                        "<div class='panel panel-default'>" +
                            "<div class='panel-heading item-name'>" +
                                item.product_name +
                            "</div>" +
                            "<div class='panel -body p-0 p-b'>" +
                                "<a href=''><img src='" + item.image_url + "' width='100%' class='img-responsive item-featured' /></a>" +
                                "<div class='list-group m-0'>" +
                                    "<div class='list-group-item b-0 b-t'>" +
                                        "<i class='fa fa-calendar-o fa-2x pull-left ml-r'></i>" +
                                        "<p class='list-group-item-text'>Giá bán</p>" +
                                        "<h4 class='list-group-item-heading'><span class='item-price'>" + DecimalUtil.DecimalToMoney(item.price) + "</span></h4>" +
                                    "</div>" +
                                    //"<div class='list-group-item b-0 b-t'>" +
                                    //     "<i class='fa fa-calendar fa-2x pull-left ml-r'></i>" +
                                    //     "<p class='list-group-item-text'>Số lượng hiện có</p>" +
                                    //     "<h4 class='list-group-item-heading item-stock'>" + item.quantity + "</h4>" +
                                    // "</div>" +
                                "</div>" +
                            "</div>" +
                            "<div class='panel-footer'>" +
                                "<p><a href='#' id='" + item.product_id + "' class='btn btn-success btn-block'>Thêm vào giỏ</a></p>" +
                                //"<p><a href='#' id='search_new_cart-" + item.product_id + "' class='btn btn-warning btn-block'>Tạo giỏ mới</a></p>" +
                             "</div>" +
                        "</div>" +
                    "</div>";
                }
            }
            else
            {
                pag_content = "";
            }

            var response = new Dictionary<string, string> {
        { "content", pag_content },
        { "navigation", pag_navigation }
    };
            string json = new JavaScriptSerializer().Serialize(response);
            return json;
        }

        [HttpPost]
        public string LoadListOrder(FormCollection collection)
        {
            var service = this._orderService;
            /* Setup default variables that we are going to populate later */
            var pag_tab = "";
            var pag_detail = "";
            var lisOrder = service.GetOrderListNotPay();

            /* Loop through each item to create views */
            if (lisOrder != null && lisOrder.Count > 0)
            {
                pag_tab += "<div class='col-sm-12'>"+
                           "<ul class='nav nav-tabs'>";
                foreach (var item in lisOrder)
                {
                    if (item.active != 0)
                    {
                        pag_tab += "<li class='active'><a href='#order-"+ item.id +"' id='" + item.id + "' data-toggle='tab' class='tab-on tab-active'>" + item.name+"</a></li>";
                    }
                    else
                    {
                        pag_tab += "<li><a href='#order-" + item.id + "' id='" + item.id + "' data-toggle='tab' class='tab-active'>" + item.name + "</a></li>";
                    }
                }
                pag_tab += "</ul>"+
                           "</div>";
            }
            else
            {
                pag_tab = "";
            }

            pag_detail += "<div class='tab-content'>";
            if (lisOrder != null && lisOrder.Count > 0)
            {
                foreach (var item in lisOrder)
                {
                    string classActive = (item.active != 0) ? "tab-pane fade active in" : "tab-pane fade ";
                    pag_detail += "<div class='" + classActive + "' id='order-" + item.id + "'>" +
                                    "<section id='cart_items'>"+
                                        "<div class='container'>"+
                                            "<div class='table-responsive cart_info'>"+
                                                "<table class='table table-condensed'>"+
                                                    "<thead>"+
                                                        "<tr class='cart_menu'>"+
                                                            "<td class='image'>Sản phẩm</td>"+
                                                            "<td class='description'></td>"+
                                                            "<td class='price'>Giá</td>"+
                                                            "<td class='quantity'>Số lượng</td>"+
                                                            "<td class='total'>Số tiền</td>" +
                                                            "<td></td>"+
                                                        "</tr>"+
                                                    "</thead>"+
                                                    "<tbody>";
                    var orderDetail = service.GetOrderDetail(item.id);
                    decimal totalOrderMoney = 0;
                    int index = 0;
                    string regexPattern1 = @"^\d*$";
                    // Nếu đơn hàng chưa có sản phẩm
                    if (orderDetail == null || orderDetail.Count <= 0)
                    {
                        pag_detail += "<tr>" +
                                        "<td colspan='6'>" +
                                        "<div class='widget newsletter clearfix'>" +
                                        "<i class='icon-double-angle-left pull-left'  id='icon_double_angle_left-" + item.id + "' style='cursor: pointer;' alt='Huỷ'></i>" +
                                        "<h2 class='pull-left' id='total_order_money-" + item.id + "'><span>Tiền thu: </span>" + 0 + "</h2>" +
                                        "<h2 class='pull-left' id='total_return_money-" + item.id + "'><span> Trả khách: </span>" + 0 + "</h2>" +
                                    "</div>" +
                                    "</td>" +
                                    "</tr>";
                    }
                    foreach (var det in orderDetail)
                    {
                        var totalPrice = Convert.ToInt32(det.quantity) * Convert.ToDecimal(det.price);
                        totalOrderMoney = totalOrderMoney + totalPrice;
                        string img_url = String.IsNullOrEmpty(det.image_url) ? "/Content/images/no-image.png" : det.image_url;
                        pag_detail += "<tr>" +
                                        "<td class='cart_product'>" +
                                            "<a href=''><img src=" + img_url + " alt=''></a>" +
                                        "</td>" +
                                        "<td class='cart_description'>" +
                                            "<h4><a href=''>" + det.product_name + "</a></h4>" +
                                            //"<p>Mã hàng: " + det.id_product + "</p>" +
                                        "</td>" +
                                        "<td class='cart_price'>" +
                                            "<p id='cart_price-" + det.id_order + "-" + det.id_product + "'>Giá bán : " + DecimalUtil.DecimalToMoney(det.price) + "</p>" +
                                            "<p style='font-size: 10px'>Giá vốn: " + DecimalUtil.DecimalToMoney(det.price_import) + "</p>" +
                                        "</td>" +
                                        "<td class='cart_quantity'>" +
                                            "<div class='cart_quantity_button'>" +
                                                "<a class='cart_quantity_up' href='' id='plus-" + det.id_order + "-" + det.id_product + "'> + </a>" +
                                                "<input class='cart_quantity_input' type='text' pattern= '" + regexPattern1 + "' maxlength='2' name='quantity' id='quantity-" + det.id_order + "-" + det.id_product + "' value=" + det.quantity + " autocomplete='on' size='2'>" +
                                                "<a class='cart_quantity_down' href='' id='minus-" + det.id_order + "-" + det.id_product + "'> - </a>" +
                                            "</div>" +
                                        "</td>" +
                                        "<td class='cart_total'>" +
                                            "<p class='cart_total_price' id='cart_total_price-" + det.id_order + "-" + det.id_product + "'>" + DecimalUtil.DecimalToMoney(totalPrice) + "</p>" +
                                        "</td>" +
                                        "<td class='cart_delete'>" +
                                            "<a class='cart_quantity_delete'><i class='fa fa-times' id='cart_quantity_delete-" + det.id_order + "-" + det.id_product + "'></i></a>" +
                                        "</td>" +
                                    "</tr>";

                        if (index == orderDetail.Count - 1)
                        {
                            //pag_detail += "<tr>" +
                            //                "<td colspan='4'>" +
                            //                 "<div class='newsletter clearfix'>" +
                            //                "<h2 class='pull-left' id='total_return_money-" + det.id_order + "'><span> Trả khách: </span>0</h2>" +
                            //                "<div class='newsletter-form pull-right'>" +
                                            
                            //                 "<div class='form-group'>" +
                                               
                            //                "</div>" +
                            //                "<div class='form-group'>" +
                            //                    "<label for='d' >Khách nợ: </label>" +
                            //                    "<select class='form-control post_name m-b'>" +
                            //                        "<option value = 'name' > Name </ option >" +
                            //                        "<option value='price'>Price</option>" +
                            //                        "<option value = 'date' > Date Posted</option>" +
                            //                    "</select>" +
                            //                "</div>" +
                            //               "</div>" +
                            //                "</div>" +
                            //              "</td>" +
                            //            "</tr>";

                           pag_detail += "<tr>" +
                                       "<td colspan='5'>" +
                                       // TỔng chi phí đơn hàng
                                      "<div class='col-sm-3' style='width: 300px'>" +
                                            "<div class='panel panel-default'>" +
                                           "<div class='panel-heading item-name'>Chi phí đơn hàng</div>" +
                                           "<div class='panel -body p-0 p-b'>" +
                                               "<div class='list-group m-0'>" +
                                                   "<div class='list-group-item b-0 b-t'>" +
                                                       "<i class='fa fa-calendar-o fa-2x pull-left ml-r'></i>" +
                                                       "<p class='list-group-item-text'>Tổng chi phí</p>" +
                                                       "<h2 class='list-group-item-heading' id='total_order_money-" + det.id_order + "'>" + DecimalUtil.DecimalToMoney(totalOrderMoney).ToString() + "</h2>" +
                                                   "</div>" +
                                                   "<div class='list-group-item b-0 b-t'>" +
                                                       "<label for='d' >Giảm giá </label>" +
                                                       "<input type='text' name='discount_price' value=" + 0 + " class='form-control discount_price' pattern= '" + regexPattern1 + "' maxlength='12' id='discount_price-" + det.id_order + "'' style='border-color: rgba(229, 103, 23, 0.8);' placeholder='Số tiền giảm giá' />" +
                                                    "</div>" +
                                                    "<div class='list-group-item b-0 b-t'>" +
                                                         "<i class='fa fa-calendar fa-2x pull-left ml-r'></i>" +
                                                         "<p class='list-group-item-text'>Số tiền phải thu</p>" +
                                                         "<h2 class='list-group-item-heading item-stock' id='total_money_payment-" + det.id_order + "'>" + DecimalUtil.DecimalToMoney(totalOrderMoney).ToString() + "</h2>" +
                                                    "</div>" +
                                               "</div>" +
                                           "</div>" +
                                       "</div>" +
                                     "</div>" +
                                   "</div>" +

                                   // Cho phép ghi nợ
                                   "<div class='col-sm-3 hide' id= 'debit_panel-" + det.id_order + "' style='width: 300px'>" +
                                            "<div class='panel panel-default'>" +
                                           "<div class='panel-heading item-name'>Thông tin khách nợ</div>" +
                                           "<div class='panel -body p-0 p-b'>" +
                                               "<div class='list-group m-0'>" +
                                                   "<div class='list-group-item b-0 b-t'>" +

                                                   "</div>" +
                                                   "<div class='list-group-item b-0 b-t'>" +
                                                       
                                                    "</div>" +
                                               "</div>" +
                                           "</div>" +
                                       "</div>" +
                                     "</div>" +
                                   "</div>" +
                                "</td>" +
                                "</tr>";

                            pag_detail += "<tr>" +
                                        "<td colspan='6'>" +
                                        "<div class='widget newsletter clearfix'>" +
                                        "<i class='icon-double-angle-left pull-left' id='icon_double_angle_left-" + det.id_order + "' style='cursor: pointer;' title='Huỷ đơn hàng'></i>" +
                                        "<span class='cr'><i class='cr-icon fa fa-square-o pull-left check_debit' id='debit-" + det.id_order + "' style='cursor: pointer;' title='Cho phép ghi nợ'></i></span>" +
                                        //"<h2 class='pull-left' id='total_order_money-" + det.id_order + "'><span>Tiền thu: </span>" + DecimalUtil.DecimalToMoney(totalOrderMoney).ToString() + "</h2>" +
                                        "<h2 class='pull-left' id='total_return_money-" + det.id_order + "'><span> Tiền hoàn lại khách: </span>0</h2>" +
                                         //"<h2 class='pull-left hide' id='lbl_debit-" + det.id_order + "'>Cho phép ghi nợ: </h2>" +
                                        //"<h2 class='pull-left'><span> Ghi nợ: </span></h2>" +
                                        //"<h2 class='pull-left'><span>(Đơn vị: </span> vnđ)</h2>" +
                                        //"<div class='newsletter-form pull-left'>" +
                                        //    "<form action='/' method='post'>" +
                                        //     "<div class='form-group'>" +
                                        //          "<input type='checkbox' id='debit-" + det.id_order + "' name='' value='' class='form-control hide' style='border-style: none;'/>" +
                                        //     "</div>" +
                                        //    "</form>" +
                                        //"</div>" +
                                        "<div class='newsletter-form pull-right'>" +
                                            "<form action='/' method='post'>" +
                                                "<div class='form-group'>" +
                                                    "<input type='text' name='' value='' class='form-control customer_money' pattern= '" + regexPattern1 + "' maxlength='12' id='customer_money-" + det.id_order + "'' style='border-color: rgba(229, 103, 23, 0.8);' placeholder='Tiền khách đưa' />" +
                                                "</div>" +
                                                "<input type='submit' class='payment-on' id='payment-" + det.id_order + "-" + det.id_product + "' name='' value='Thanh toán' />" +
                                            "</form>" +
                                        "</div>" +
                                    "</div>" +
                                    "</td>" +
                                    "</tr>";
                        }

                        index++;
                    }
                    pag_detail += "</tbody>" +
                                    "</table>" +
                                "</div>" +
                            "</div>" +
                        "</section>"+
                     //"<section id='do_action'>" +
                     //       "<div class='container'>" +
                     //           "<div class='widget newsletter clearfix'>" +
                     //               "<i class='icon-notebook pull-left'></i>" +
                     //               "<h2 class='pull-left'><span>Tổng tiền phải thu: </span><p>"+DecimalUtil.DecimalToMoney(totalOrderMoney) +"</p></h2>" +
                     //               "<h2 class='pull-left'><span> Tiền trả lại khách: </span>0</h2>" +
                     //               "<h2 class='pull-left'><span>(Đơn vị: </span> vnđ)</h2>" +
                     //               "<div class='newsletter-form pull-right'>" +
                     //                   "<form action='/' method='post'>" +
                     //                       "<div class='form-group'>" +
                     //                           "<label for='d' class='hide'>Tiền nhận của khách</label>" +
                     //                           "<input type='text' name='' value='' class='form-control' id='d' placeholder='Tiền nhận của khách' />" +
                     //                       "</div>" +
                     //                       "<input type='submit' name='' value='Thanh toán' />" +
                     //                   "</form>" +
                     //               "</div>" +
                     //           "</div>" +
                     //       "</div>" +
                     //   "</section>" +
                    "</div>";
                }
                pag_detail += "</div>";
            }
               
            /* Lets put our variables in a dictionary */
            var response = new Dictionary<string, string> {
        { "content_tab", pag_tab },
        { "detail_tab", pag_detail }
    };

            /* Then we return the Dictionary in json format to our front-end */
            string json = new JavaScriptSerializer().Serialize(response);
            return json;
        }

        [HttpPost]
        public bool UpdateNotActiveOrder(FormCollection collection)
        {
            var service = this._orderService;
            /* Setup default variables that we are going to populate later */
            string orderid = collection["data[orderid]"]; /* Keyword provided on our search box */
            bool ret = service.UpdateNotActiveOrder(orderid);
            ret = service.UpdateActiveOrder(orderid);
            return ret;
        }

        [HttpPost]
        public string Payment(FormCollection collection)
        {
            bool ret = false;
            var service = this._orderService;
            var pService = this._productService;
            /* Setup default variables that we are going to populate later */
            string orderid = collection["data[orderid]"]; /* Keyword provided on our search box */
            ret = service.UpdatePayment(orderid);
            if (ret)
            {
                List<MSys.Model.OrderDetailInfo> lstOrderDetail = service.GetOrderDetail(orderid);
                foreach (var item in lstOrderDetail)
                {
                    var productInfo = pService.GetProductsInfoById(item.id_product);
                    ret = pService.UpdateQuantityProductExport(productInfo.product_id.ToString(), (int)productInfo.quantity_export + item.quantity);
                }
            }
            if (ret)
                ret = service.UpdateActiveOrderForMinId();
            var response = new Dictionary<string, string> {
                { "status", ret.ToString() }
            };
            string json = new JavaScriptSerializer().Serialize(response);
            return json;
        }

        [HttpPost]
        public string DeleteOrder(FormCollection collection)
        {
            var service = this._orderService;
            /* Setup default variables that we are going to populate later */
            string orderid = collection["data[orderid]"]; /* Keyword provided on our search box */
            service.DeleteOrderDetailById(orderid);
            service.DeleteOrderById(orderid);
            // Cập nhật trạng thái active cho đơn hàng nhỏ nhất
            service.UpdateActiveOrderForMinId();

            var response = new Dictionary<string, string> {
                { "status", "" }
            };
            string json = new JavaScriptSerializer().Serialize(response);
            return json;
        }

        [HttpPost]
        public string DeleteProductInOrderDetail(FormCollection collection)
        {
            bool ret = false;
            var service = this._orderService;
            /* Setup default variables that we are going to populate later */
            string orderid = collection["data[orderid]"]; /* Keyword provided on our search box */
            string productid = collection["data[productid]"]; /* Keyword provided on our search box */
            ret = service.DeleteProductInOrderDetail(orderid, productid);

            var response = new Dictionary<string, string> {
                { "status", ret.ToString() }
            };
            string json = new JavaScriptSerializer().Serialize(response);
            return json;
        }

        public ActionResult GetProductById(int id)
        {
            //var products = Product.GetSampleProducts().Where(x => x.Id == id); ;

            //if (products != null)
            //{
            //    ProductModel model = new ProductModel();

            //    foreach (var item in products)
            //    {
            //        model.Name = item.Name;
            //        model.Price = item.Price;
            //        model.Department = item.Department;
            //    }

            //    return PartialView("_GridEditPartial", model);
            //}

            return View();
        }

        [HttpPost]
        public ActionResult UpdateProduct(ProductModel model)
        {
            //update database
            return Content("Record updated!!", "text/html");
        }
    }
}