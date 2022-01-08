using BookStoreManager.Interface;
using BookstoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using UserModel;

namespace BookStoreManager.Manager
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepo repository;
        public OrderManager(IOrderRepo repository)
        {
            this.repository = repository;
        }
        public string AddOrder(OrderModel orderModel)
        {
            try
            {
                return this.repository.AddOrder(orderModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<OrderModel> GetOrderList(int UserId)
        {
            try
            {
                return this.repository.GetOrderList(UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
