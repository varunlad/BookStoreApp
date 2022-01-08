using UserModel;

namespace BookStoreManager.Interface
{
    public interface IOrderManager
    {
        string AddOrder(OrderModel orderModel);
    }
}