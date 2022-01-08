using BookstoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UserModel;

namespace BookstoreRepository.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly IConfiguration config;
        public string connectionString { get; set; } = "BookStoreDB";
        public OrderRepo(IConfiguration configration)
        {
            this.config = configration;
        }
        public string AddOrder(OrderModel orderModel)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_AddOrder", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@obookId", orderModel.BookId);
                    cmd.Parameters.AddWithValue("@oaddressId", orderModel.AddressId);
                    cmd.Parameters.AddWithValue("@ouserId", orderModel.UserId);
                    cmd.Parameters.AddWithValue("@oorderDate", orderModel.Date);
                    cmd.Parameters.AddWithValue("@oBookQuantity", orderModel.BookQuantity);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Added to Order";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
    }
}
