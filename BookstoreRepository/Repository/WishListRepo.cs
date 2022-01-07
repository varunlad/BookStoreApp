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
        public string DeleteWishListBook(int WishListId)
        {
            int result;
            string msg;
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_DeleteWishlist", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@dwishlistId", WishListId);
                    con.Open();
                    //ExecuteScalar: This method only returns a single value. This kind of query returns a count of rows or a calculated value.
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    //Switch statement
                    msg = result switch
                    {
                        -1 => "Book is not Removed from Wishlist",
                        _ => "Book Removed From Wishlist",
                    };
                }
                return msg;
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
    }
}
