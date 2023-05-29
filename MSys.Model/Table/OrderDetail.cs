using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSys.Model.Table
{
    public class OrderDetail
    {
        public string id { get; set; }
        public string id_product { get; set; }
        public string id_order { get; set; }
        public string name { get; set; }
        public string quantity { get; set; }
    }
}
