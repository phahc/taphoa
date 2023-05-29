using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSys.Model.Table
{
    public class Products
    {
        public string product_id {get; set;}
        public string product_name { get; set; }
        public string image_url { get; set; }
        public decimal price_import { get; set; }
        public decimal price_export { get; set; }
        public int price_percent { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public int quantity_import { get; set; }
        public int quantity_export { get; set; }
        public string decriptions { get; set; }
        public string madein { get; set; }
        public string userid { get; set; }
        public DateTime expiry_date { get; set; }
        public string promotion { get; set; }
        public DateTime promotion_startdate { get; set; }
        public DateTime promotion_enddate { get; set; }
        public string home_show { get; set; }
        public string product_order { get; set; }
        public string views { get; set; }
        public string enable { get; set; } 
        public int categories { get; set; }
        public decimal wholesale_price { get; set; }
        public DateTime create_date { get; set; }
    }
}
