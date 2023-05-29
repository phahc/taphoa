using MSys.Model;
using MSys.Model.Data;
using System.Collections.Generic;

namespace MSys.Interface
{
    public interface IOrder
    {
        List<Order> GetOrderListNotPay();
        Order GetActiveOrder(long userid);
        OrderDetail GetExistDetail(OrderDetail orderDetail);
        List<OrderDetailInfo> GetOrderDetail(string orderId);
        bool InsertOrderDetail(OrderDetailInfo orderDetail);
        bool UpdateOrderDetail(OrderDetailInfo orderDetail);
        bool UpdateQuantityOrderDetail(OrderDetailInfo orderDetail);
        bool UpdateActiveOrder(string orderId);
        bool UpdateNotActiveOrder(string orderId);
        bool UpdatePayment(string orderId);
        bool UpdateActiveOrderForMinId();
        string CreateMaxOrderCode();
        bool RegisterOrder(Order order);
        bool DeleteOrderById(string orderId);
        bool DeleteOrderDetailById(string orderId);
        bool DeleteProductInOrderDetail(string orderId, string productId);
    }
}
