using MSys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Msys.WebApp.Models
{
    public class MyCartInfo
    {
        public MyCartInfo()
        {
            DetailLst = new List<OrderDetailInfo>();
        }
        public List<OrderDetailInfo> DetailLst { get; set; }

        public string userNm { get; set; }
        public string userAddress { get; set; }
        public string userPhone { get; set; }
        public string userEmail { get; set; }
    }
}