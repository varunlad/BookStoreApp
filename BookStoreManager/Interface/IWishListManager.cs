﻿namespace BookStoreManager.Interface
{
    public interface IWishListManager
    {
        string AddToWishList(int UserId, int BookId);
        string DeleteWishListItem(int WishListItem);
    }
}