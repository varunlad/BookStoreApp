using UserModel;

namespace BookstoreRepository.Interface
{
    public interface ICartRepo
    {
        string connectionString { get; set; }

        string AddCart(CartModel cartModel);
        string DeleteCart(int id);
    }
}