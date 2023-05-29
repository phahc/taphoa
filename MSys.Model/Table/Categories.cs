using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSys.Model.Table
{
    public class Categories
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int oder { get; set; }
        public int parent { get; set; }
        public int type { get; set; }
    }
}
