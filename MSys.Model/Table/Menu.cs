using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSys.Model.Table
{
    [Serializable]
    public class Menu
    {
        public string name { get; set; }
        public string link { get; set; }
        public int oder { get; set; }
    }
}
