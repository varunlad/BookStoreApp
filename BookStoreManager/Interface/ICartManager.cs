using UserModel;

namespace BookStoreManager.Interface
{
    public interface ICartManager
    {
        string AddCart(CartModel cartModel);
        string DeleteCart(int id);
    }
}