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
    public class CartRepo : ICartRepo
    {
        private readonly IConfiguration config;
        public string connectionString { get; set; } = "BookStoreDB";
        public CartRepo(IConfiguration configration)
        {
            this.config = configration;
        }

        public string AddCart(CartModel cartModel)
        {
            int result;
            string msg;
            try
            {
                if (cartModel != null)
                {
                    string ConnectionStrings = config.GetConnectionString(connectionString);
                    using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                    {
                        MySqlCommand cmd = new MySqlCommand("sp_AddCart", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cuserId", cartModel.userId);
                        cmd.Parameters.AddWithValue("@cbookId", cartModel.bookId);
                        cmd.Parameters.AddWithValue("@ccartBookQuantity", cartModel.bookQuantity);
                        con.Open();
                        //ExecuteScalar: This method only returns a single value. This kind of query returns a count of rows or a calculated value.
                        result = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                        //Switch statement
                        msg = result switch
                        {
                            -1 => "Book is Not Present in cart",
                            _ => "Book is Added to cart",
                        };
                    }
                    return msg;
                }
                else
                {
                    return "Book is Not present in cart";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
        public string DeleteCart(int id)
        {
            int result;
            string msg;
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_DeleteCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ditemId", id);
                    con.Open();
                    //ExecuteScalar: This method only returns a single value. This kind of query returns a count of rows or a calculated value.
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    //Switch statement
                    msg = result switch
                    {
                        -1 => "Cart Does not Exits",
                        _ => "Item is Deleted",
                    };
                }
                return msg;
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
        public string UpdateCart(int cartItemId, int QuantityUpdated)
        {
            int result;
            string msg;
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_UpdateCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@uitemId", cartItemId);
                    cmd.Parameters.AddWithValue("@ucartBookQuantity", QuantityUpdated);
                    con.Open();
                    //ExecuteScalar: This method only returns a single value. This kind of query returns a count of rows or a calculated value.
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    //Switch statement
                    msg = result switch
                    {
                        -1 => "Cart Does not Exits",
                        _ => "Cart is Updated",
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
