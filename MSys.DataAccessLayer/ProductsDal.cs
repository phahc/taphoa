using MSys.Interface;
using System.Collections.Generic;
using System.Linq;
using MSys.Model.Data;

namespace MSys.DataAccessLayer
{
    public class ProductsDal: IProducts
    {
        private BooksEntities _dataContext;

        public ProductsDal()
        {
            _dataContext = new BooksEntities();
        }

        public List<Product> GetProductsInfo()
        {
            //IMapperManager mapper = ServiceLocator.Resolve<IMapperManager>();
            //Hashtable ht = new Hashtable();
            //return mapper.QueryForList<Hashtable, Products>("Products", "GetProductsInfo", ht);
            return _dataContext.Products.ToList();
        }

        public Product GetProductsInfoById(string product_id)
        {
            //IMapperManager mapper = ServiceLocator.Resolve<IMapperManager>();
            //Hashtable ht = new Hashtable();
            //ht.Add("product_id", product_id);
            //return mapper.QueryForObject<Hashtable, Product>("Products", "GetProductsInfoById", ht);
            var product = _dataContext.Products.Where(p => p.product_id.ToString() == product_id).FirstOrDefault();
            return product;
        }

        public List<Product> GetProductsInfoByCategoriesId(int categoriesId)
        {
            //IMapperManager mapper = ServiceLocator.Resolve<IMapperManager>();
            //Hashtable ht = new Hashtable();
            //ht.Add("categories", categoriesId);
            //return mapper.QueryForList<Hashtable, Product>("Products", "GetProductsInfoByCategoriesId", ht);
            if (categoriesId == 0)
            {
                return _dataContext.Products.ToList();
            }
            var product = _dataContext.Products.Where(p => p.categories == categoriesId).ToList();
            return product;
        }


        /// <summary>
        /// UpdateQuantityProductExport
        /// </summary>
        /// <returns></returns>
        public bool UpdateQuantityProductExport(string productId, int quantity)
        {
            //IMapperManager mapper = ServiceLocator.Resolve<IMapperManager>();
            //Hashtable ht = new Hashtable();
            //ht.Add("product_id", productId);
            //ht.Add("quantity", quantity);
            //return Core.Util.StringUtil.ConvertIntegerToBool(mapper.Update<Hashtable>("Products", "UpdateQuantityProductExport", ht));
            var product = _dataContext.Products.Where(p => p.product_id.ToString() == productId).FirstOrDefault();
            product.quantity_export = quantity;
            return _dataContext.SaveChanges() > 0;
        }

        public bool UpdateView(string productId)
        {
            var product = _dataContext.Products.Where(p => p.product_id.ToString() == productId).FirstOrDefault();
            product.views = product.views + 1;
            return _dataContext.SaveChanges() > 0;
        }

    }
}
