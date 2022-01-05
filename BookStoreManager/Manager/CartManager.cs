using BookStoreManager.Interface;
using BookstoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using UserModel;

namespace BookStoreManager.Manager
{
    public class CartManager : ICartManager
    {
        private readonly ICartRepo repository;
        public CartManager(ICartRepo repository)
        {
            this.repository = repository;
        }

        public string AddCart(CartModel cartModel)
        {
            try
            {
                return this.repository.AddCart(cartModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string DeleteCart(int id)
        {
            try
            {
                return this.repository.DeleteCart(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string UpdateCart(int cartItemId, int QuantityUpdated)
        {
            try
            {
                return this.repository.UpdateCart(cartItemId, QuantityUpdated);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
