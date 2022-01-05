using System.Collections.Generic;
using UserModel;

namespace BookStoreManager.Interface
{
    public interface ICartManager
    {
        string AddCart(CartModel cartModel);
        string DeleteCart(int id);
        string UpdateCart(int cartItemId, int QuantityUpdated);
        IEnumerable<CartModel> GetCart(int id);
    }
}