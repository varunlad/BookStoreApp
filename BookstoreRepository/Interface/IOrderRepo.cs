using System.Collections.Generic;
using UserModel;

namespace BookstoreRepository.Interface
{
    public interface IOrderRepo
    {
        string connectionString { get; set; }

        string AddOrder(OrderModel orderModel);
        IEnumerable<OrderModel> GetOrderList(int UserId);
    }
}