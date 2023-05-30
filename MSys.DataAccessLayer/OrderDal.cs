using MSys.Interface;
using MSys.Model.Data;
using System.Collections.Generic;
using System.Linq;
using MSys.Model;
using System;

namespace MSys.DataAccessLayer
{
    public class OrderDal : IOrder
    {
        private BooksEntities _dataContext;

        public OrderDal()
        {
            _dataContext = new BooksEntities();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Order> GetOrderListNotPay()
        {
            //IMapperManager mapper = ServiceLocator.Resolve<IMapperManager>();
            //Hashtable ht = new Hashtable();
            //return mapper.QueryForList<Hashtable, Order>("Order", "GetOrderListNotPay", ht);
            //return _dataContext.Orders.Where(odr => odr.status == 0).ToList();
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Order GetActiveOrder(long userid)
        {
            //IMapperManager mapper = ServiceLocator.Resolve<IMapperManager>();
            //Hashtable ht = new Hashtable();
            //return mapper.QueryForObject<Hashtable, Order>("Order", "GetActiveOrder", ht);
            return _dataContext.Orders.Where(odr => odr.active == 1 && odr.status == 0 && odr.userid == userid).FirstOrDefault();
        }


        public OrderDetail GetExistDetail(OrderDetail orderDetail)
        {
            //IMapperManager mapper = ServiceLocator.Resolve<IMapperManager>();
            //Hashtable ht = new Hashtable();
            //ht.Add("orderid", orderDetail.id_order);
            //ht.Add("product_id", orderDetail.id_product);
            //return mapper.QueryForObject<Hashtable, OrderDetail>("Order", "GetExistDetail", ht);
            return _dataContext.OrderDetails.Where(odr => odr.id_order == orderDetail.id_order && odr.id_product == orderDetail.id_product).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<OrderDetailInfo> GetOrderDetail(string orderId)
        {
            //IMapperManager mapper = ServiceLocator.Resolve<IMapperManager>();
            //Hashtable ht = new Hashtable();
            //ht.Add("id", orderId);
            //return mapper.QueryForList<Hashtable, OrderDetailInfo>("Order", "GetOrderDetail", ht);
            var query = (from odr in _dataContext.OrderDetails
                         from p in _dataContext.Products
                         where p.product_id.ToString() == odr.id_product && odr.id_order == orderId
                         select new OrderDetailInfo()
                        {
                            id_order = odr.id_order,
                            id_product = odr.id_product,
                            quantity = odr.quantity,
                            product_name = p.product_name,
                            image_url = p.image_url,
                            price = p.price,
                            price_import = p.price_import
                        });

            return query.ToList();
        }

        /// <summary>
        /// InsertOrderDetail
        /// </summary>
        /// <returns></returns>
        public bool InsertOrderDetail(OrderDetailInfo orderDetail)
        {
            //IMapperManager mapper = ServiceLocator.Resolve<IMapperManager>();
            //return Core.Util.StringUtil.ConvertIntegerToBool(mapper.Insert<OrderDetailInfo>("Order", "InsertOrderDetail", orderDetail));
            _dataContext.OrderDetails.Add(new OrderDetail {
                id_order = orderDetail.id_order,
                id_product = orderDetail.id_product,
                quantity = orderDetail.quantity,
                name = orderDetail.product_name,
            });
            _dataContext.Configuration.AutoDetectChangesEnabled = true;
            _dataContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// UpdateOrderDetail
        /// </summary>
        /// <returns></returns>
        public bool UpdateQuantityOrderDetail(OrderDetailInfo orderDetail)
        {
            //IMapperManager mapper = ServiceLocator.Resolve<IMapperManager>();
            //Hashtable ht = new Hashtable();
            //ht.Add("orderid", orderDetail.id_order);
            //ht.Add("product_id", orderDetail.id_product);
            //ht.Add("quantity", orderDetail.quantity);
            //return Core.Util.StringUtil.ConvertIntegerToBool(mapper.Update<Hashtable>("Order", "UpdateQuantityOrderDetail", ht));
            var data = _dataContext.OrderDetails.Where(p => p.id_order == orderDetail.id_order && p.id_product == orderDetail.id_product).FirstOrDefault();
            data.quantity = orderDetail.quantity;
            _dataContext.Configuration.AutoDetectChangesEnabled = true;
            _dataContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// UpdateOrderDetail
        /// </summary>
        /// <returns></returns>
        public bool UpdateOrderDetail(OrderDetailInfo orderDetail)
        {
            //IMapperManager mapper = ServiceLocator.Resolve<IMapperManager>();
            //Hashtable ht = new Hashtable();
            //ht.Add("orderid", orderDetail.id_order);
            //ht.Add("product_id", orderDetail.id_product);
            //ht.Add("quantity", orderDetail.quantity);
            //return Core.Util.StringUtil.ConvertIntegerToBool(mapper.Update<Hashtable>("Order", "UpdateOrderDetail", ht));
            var data = _dataContext.OrderDetails.Where(p => p.id_order == orderDetail.id_order && p.id_product == orderDetail.id_product).FirstOrDefault();
            data.quantity = orderDetail.quantity;
            _dataContext.Configuration.AutoDetectChangesEnabled = true;
            _dataContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// UpdateActiveOrder
        /// </summary>
        /// <returns></returns>
        public bool UpdateActiveOrder(string orderId)
        {
            var data = _dataContext.Orders.Where(p => p.id == orderId).FirstOrDefault();
            data.status = 1;
            _dataContext.Configuration.AutoDetectChangesEnabled = true;
            _dataContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// UpdateActiveOrderForMinId
        /// </summary>
        /// <returns></returns>
        public bool UpdateActiveOrderForMinId()
        {
            //IMapperManager mapper = ServiceLocator.Resolve<IMapperManager>();
            //Hashtable ht = new Hashtable();
            //return Core.Util.StringUtil.ConvertIntegerToBool(mapper.Update<Hashtable>("Order", "UpdateActiveOrderForMinId", ht));
            return true;
        }

        /// <summary>
        /// UpdateNotActiveOrder
        /// </summary>
        /// <returns></returns>
        public bool UpdateNotActiveOrder(string orderId)
        {
            //IMapperManager mapper = ServiceLocator.Resolve<IMapperManager>();
            //Hashtable ht = new Hashtable();
            //ht.Add("id", orderId);
            //return Core.Util.StringUtil.ConvertIntegerToBool(mapper.Update<Hashtable>("Order", "UpdateNotActiveOrder", ht));
            return true;
        }

        /// <summary>
        /// UpdatePayment
        /// </summary>
        /// <returns></returns>
        public bool UpdatePayment(string orderId)
        {
            //IMapperManager mapper = ServiceLocator.Resolve<IMapperManager>();
            //Hashtable ht = new Hashtable();
            //ht.Add("id", orderId);
            //return Core.Util.StringUtil.ConvertIntegerToBool(mapper.Update<Hashtable>("Order", "UpdatePayment", ht));
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string CreateMaxOrderCode()
        {
            //IMapperManager mapper = ServiceLocator.Resolve<IMapperManager>();
            //Hashtable ht = new Hashtable();
            return string.Format("{0:00000000000000000}", Convert.ToInt64(_dataContext.Orders.Max(o => o.id)) + 1);
        }
        /// <summary>
        /// RegisterOrder
        /// </summary>
        /// <returns></returns>
        public bool RegisterOrder(Order order)
        {
            //IMapperManager mapper = ServiceLocator.Resolve<IMapperManager>();
            //return Core.Util.StringUtil.ConvertIntegerToBool(mapper.Insert<Order>("Order", "RegisterOrder", order));
            //_dataContext.Entry(customer).State = EntityState.Added;
            _dataContext.Orders.Add(order);
            _dataContext.Configuration.AutoDetectChangesEnabled = true;
            _dataContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// DeleteOrderById
        /// </summary>
        /// <returns></returns>
        public bool DeleteOrderById(string orderId)
        {
            //IMapperManager mapper = ServiceLocator.Resolve<IMapperManager>();
            //Hashtable ht = new Hashtable();
            //ht.Add("orderid", orderId);
            //return Core.Util.StringUtil.ConvertIntegerToBool(mapper.Delete<Hashtable>("Order", "DeleteOrderById", ht));
            return true;
        }

        /// <summary>
        /// DeleteOrderDetailById
        /// </summary>
        /// <returns></returns>
        public bool DeleteOrderDetailById(string orderId)
        {
            //IMapperManager mapper = ServiceLocator.Resolve<IMapperManager>();
            //Hashtable ht = new Hashtable();
            //ht.Add("orderid", orderId);
            //return Core.Util.StringUtil.ConvertIntegerToBool(mapper.Delete<Hashtable>("Order", "DeleteOrderDetailById", ht));
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteProductInOrderDetail(string orderId, string productId)
        {
            OrderDetail deail = _dataContext.OrderDetails.Where(odr => odr.id_order == orderId && odr.id_product == productId).FirstOrDefault();
            _dataContext.OrderDetails.Remove(deail);
            _dataContext.SaveChanges();
            return true;
        }
    }
}
