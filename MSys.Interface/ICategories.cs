using MSys.Model.Data;
using System.Collections.Generic;

namespace MSys.Interface
{
    public interface ICategories
    {
        List<Category> GetCategoriesInfo(int topic);
    }
}
