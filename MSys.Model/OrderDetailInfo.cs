using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSys.Model
{
    public class OrderDetailInfo
    {
        public string id { get; set; }
        public string id_order { get; set; }
        public string id_product { get; set; }
        public int quantity { get; set; }
        public string product_name { get; set; }
        public string image_url { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<decimal> price_import { get; set; }
    }
}
