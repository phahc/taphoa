using MSys.Interface;
using System.Text;
using System.Web.Mvc;

namespace MSys.WebApp
{
    /// <summary>
    /// 
    /// </summary>
    public static class MenuHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private static IMenu _menuService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuService"></param>
        public static void Init(IMenu menuService)
        {
            _menuService = menuService;
        }
        /// <summary>
        /// Thiet lap top menu
        /// </summary>
        /// <returns></returns>
        public static MvcHtmlString Menu()
        {
            var sb = new StringBuilder();  
            // Lay danh sach menu trong Db
            var menuList = _menuService.GetMenuInfo();
            sb.AppendFormat("<ul class='sf-menu' id='main-menu'>");
            foreach (var menu in menuList)
            {
                sb.AppendFormat((menu.oder == 1) ? "<li class='current-item-menu'>" : "<li>");
                sb.AppendFormat("<a href='{0}'>{1}</a>", menu.link, menu.name, "");
                sb.AppendFormat("</li>");
            }
            sb.AppendFormat("</ul>");
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}