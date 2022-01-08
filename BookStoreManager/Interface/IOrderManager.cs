using System.Collections.Generic;
using UserModel;

namespace BookStoreManager.Interface
{
    public interface IOrderManager
    {
        string AddOrder(OrderModel orderModel);
        IEnumerable<OrderModel> GetOrderList(int UserId);
    }
}