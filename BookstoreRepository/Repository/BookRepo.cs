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
        public string UpdateBook(BookModel bookModel)
        {
            int result;
            string msg;
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_UpdateBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ubookName", bookModel.bookName);
                    cmd.Parameters.AddWithValue("@ubookAuthor", bookModel.Author);
                    cmd.Parameters.AddWithValue("@ubookPrice", bookModel.Price);
                    cmd.Parameters.AddWithValue("@ubookDiscountprice", bookModel.discountPrice);
                    cmd.Parameters.AddWithValue("@ubookDetail", bookModel.Detail);
                    cmd.Parameters.AddWithValue("@ubookRating", bookModel.Rating);
                    cmd.Parameters.AddWithValue("@ubookReview", bookModel.Review);
                    cmd.Parameters.AddWithValue("@ubookImage", bookModel.Image);
                    cmd.Parameters.AddWithValue("@ubookQuantity", bookModel.Quantity);
                    con.Open();
                    //ExecuteScalar: This method only returns a single value. This kind of query returns a count of rows or a calculated value.
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    //Switch statement
                    msg = result switch
                    {
                        -1 => "Book Does not Exits",
                        _ => "Book is Updated",
                    };
                }
                return msg;
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
        public object GetBookId(int id)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_GetBookById", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@gbookId", id);
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    BookModel bookDetail = new BookModel();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            bookDetail.bookId = Convert.ToInt32(rdr["bookId"]);
                            bookDetail.bookName = rdr["bookName"].ToString();
                            bookDetail.Author = rdr["bookAuthor"].ToString();
                            bookDetail.Price = Convert.ToInt32(rdr["bookPrice"]);
                            bookDetail.discountPrice = Convert.ToInt32(rdr["bookDiscountprice"]);
                            bookDetail.Detail = rdr["bookDetail"].ToString();
                            bookDetail.Rating = Convert.ToInt32(rdr["bookRating"]);
                            bookDetail.Review = rdr["bookReview"].ToString();
                            bookDetail.Image = rdr["bookImage"].ToString();
                            bookDetail.Quantity = Convert.ToInt32(rdr["bookQuantity"]);
                            con.Close();
                            return bookDetail;
                        }
                    }
                    con.Close();
                    return null;
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }

        }
    }
}
