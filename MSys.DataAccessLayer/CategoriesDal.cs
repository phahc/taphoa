using MSys.Interface;
using MSys.Model.Data;
using System.Collections.Generic;
using System.Linq;

namespace MSys.DataAccessLayer
{
    public class CategoriesDal : ICategories
    {
        private BooksEntities _dataContext;

        public CategoriesDal()
        {
            _dataContext = new BooksEntities();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategoriesInfo(int topicId)
        {
            return _dataContext.Categories.Where(ct => ct.parent == topicId).ToList();
        }
    }
}
