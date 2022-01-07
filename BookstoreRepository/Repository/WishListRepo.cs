using BookstoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BookstoreRepository.Repository
{
    public class WishListRepo : IWishListRepo
    {
        private readonly IConfiguration config;
        public string connectionString { get; set; } = "BookStoreDB";
        public WishListRepo(IConfiguration configration)
        {
            this.config = configration;
        }
        public string AddToWishList(int UserId, int BookId)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("Add_Wishlist", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@wuserId", UserId);
                    cmd.Parameters.AddWithValue("@wbookId", BookId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Added to WishList";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
    }
}
