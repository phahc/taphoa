using MSys.Model.Data;
using System;
using System.Collections.Generic;
using System.Web;

namespace Home
{
    [Serializable()]
    public class HomeModel
    {
        public const string OrderNotPaySessionKey = "OrderNotPaySession";
        public static string[] SessionKeyOrderNotPay = { OrderNotPaySessionKey };

        /// <summary>
        /// データ入稿画面モデル
        /// </summary>
        private HomeInfoModel _homeModel = null;
        public HomeInfoModel FUModel
        {
            get
            {
                // Khi chưa thiết lập
                if (this._homeModel == null)
                {
                    // Nếu tồn tại session
                    if (HttpContext.Current.Session[OrderNotPaySessionKey] != null && HttpContext.Current.Session[OrderNotPaySessionKey] as HomeInfoModel != null)
                    {
                        this._homeModel = (HomeInfoModel)HttpContext.Current.Session[OrderNotPaySessionKey];
                    }
                    else
                    {
                        // Nếu không tồn tại session. Tạo mới
                        this._homeModel = new HomeInfoModel();
                        HttpContext.Current.Session[OrderNotPaySessionKey] = this._homeModel;
                    }
                }
                return this._homeModel;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Serializable()]
        public class HomeInfoModel
        {
            public List<Order> ListOrderNotPayInfo = new List<Order>();
            public List<Menu> MenuInfo = new List<Menu>();
            public List<Topic> MainTopicInfo = new List<Topic>();
            public List<Topic> RightTopicInfo = new List<Topic>();
            public List<Category> CategoriesInfo = new List<Category>();
            public List<Product> ProductInfo = new List<Product>();
            public List<Product> listFirstProductInfo = new List<Product>();
            

            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Department { get; set; }

            //pagination
            public int TotalCount { get; set; }
            public int PageSize { get; set; }
            public int PageNumber { get; set; }
            public int PagerCount { get; set; }

            public List<Product> ProductDetailInfo = new List<Product>();

        }
    }
}