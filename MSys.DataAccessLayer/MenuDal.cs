using MSys.Interface;
using MSys.Model.Data;
using System.Collections.Generic;
using System.Linq;

namespace MSys.DataAccessLayer
{
    public class MenuDal : IMenu
    {
        private BooksEntities _dataContext;

        public MenuDal()
        {
            _dataContext = new BooksEntities();
        }

        public List<Menu> GetMenuInfo()
        {
           return _dataContext.Menus.OrderBy(m=>m.oder).ToList();
        }
    }
}
