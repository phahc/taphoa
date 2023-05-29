using MSys.Model.Data;
using System.Collections.Generic;

namespace MSys.Interface
{
    public interface IProducts
    {
        List<Product> GetProductsInfo();
        Product GetProductsInfoById(string product_id);
        List<Product> GetProductsInfoByCategoriesId(int categoriesId);
        bool UpdateQuantityProductExport(string productId, int quantity);

        bool UpdateView(string productId);
    }
}
