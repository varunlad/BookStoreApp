﻿using BookstoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UserModel;

namespace BookstoreRepository.Repository
{
    public class BookRepo : IBookRepo
    {
        private readonly IConfiguration config;
        public string connectionString { get; set; } = "BookStoreDB";
        public BookRepo(IConfiguration configration)
        {
            this.config = configration;
        }

        public string BookDetail(BookModel book)
        {
            try
            {
                if (book != null)
                {
                    string ConnectionStrings = config.GetConnectionString(connectionString);
                    using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                    {
                        MySqlCommand cmd = new MySqlCommand("sp_AddBook", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@abookName", book.bookName);
                        cmd.Parameters.AddWithValue("@abookAuthor", book.Author);
                        cmd.Parameters.AddWithValue("@abookPrice", book.Price);
                        cmd.Parameters.AddWithValue("@abookDiscountprice", book.discountPrice);
                        cmd.Parameters.AddWithValue("@abookDetail", book.Detail);
                        cmd.Parameters.AddWithValue("@abookRating", book.Rating);
                        cmd.Parameters.AddWithValue("@abookReview", book.Review);
                        cmd.Parameters.AddWithValue("@abookImage", book.Image);
                        cmd.Parameters.AddWithValue("@abookQuantity", book.Quantity);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        return "Book is Added";
                    }
                }
                return "Book is Not Added";
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
        public string DeleteBook(int id)
        {
            int result;
            string msg;
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_DeleteBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@dbookId", id);
                    con.Open();
                    //ExecuteScalar: This method only returns a single value. This kind of query returns a count of rows or a calculated value.
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    //Switch statement
                    msg = result switch
                    {
                        -1 => "Book Does not Exits",
                        _ => "Book is Deleted",
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
