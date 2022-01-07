using BookStoreManager.Interface;
using BookstoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class WishListManager : IWishListManager
    {
        private readonly IWishListRepo repository;
        public WishListManager(IWishListRepo repository)
        {
            this.repository = repository;
        }
        public string AddToWishList(int UserId, int BookId)
        {
            try
            {
                return this.repository.AddToWishList(UserId, BookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string DeleteWishListItem(int WishListItem)
        {
            try
            {
                return this.repository.DeleteWishListBook(WishListItem);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
