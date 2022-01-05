using System.Collections.Generic;
using UserModel;

namespace BookstoreRepository.Interface
{
    public interface ICartRepo
    {
        string connectionString { get; set; }

        string AddCart(CartModel cartModel);
        string DeleteCart(int id);
        string UpdateCart(int cartItemId, int QuantityUpdated);
        IEnumerable<CartModel> GetCart(int id);
    }
}