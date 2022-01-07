using System.Collections.Generic;
using UserModel;

namespace BookStoreManager.Interface
{
    public interface IWishListManager
    {
        string AddToWishList(int UserId, int BookId);
        string DeleteWishListItem(int WishListItem);
        IEnumerable<WishListModel> GetWishList(int UserId);
    }
}