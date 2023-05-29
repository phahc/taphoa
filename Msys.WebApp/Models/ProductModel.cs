using MSys.Model;
using MSys.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Msys.WebApp.Models
{
    public class ProductModel : Product
    {
        public ProductModel()
        {
            Products = new List<Product>();
        }
        //Here your other model properties. There is a advantage using viewmodel instead of passing data model directly to page.
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Department { get; set; }

        //pagination
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PagerCount { get; set; }

        public List<Product> Products { get; set; }
    }
}