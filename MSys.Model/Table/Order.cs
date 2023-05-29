using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSys.Model.Table
{
    public class Order
    {
        public string id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public int status { get; set; }
        public string date { get; set; }
        public int active { get; set; }
    }
}
