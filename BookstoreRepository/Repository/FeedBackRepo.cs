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
    public class FeedBackRepo : IFeedBackRepo
    {
        private readonly IConfiguration config;
        public string connectionString { get; set; } = "BookStoreDB";
        public FeedBackRepo(IConfiguration configration)
        {
            this.config = configration;
        }
        public string AddFeedback(FeedBackModel feedBackModel)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_AddFeedback", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fbookId", feedBackModel.BookId);
                    cmd.Parameters.AddWithValue("@fuserId", feedBackModel.UserId);
                    cmd.Parameters.AddWithValue("@fcomments", feedBackModel.Comments);
                    cmd.Parameters.AddWithValue("@frating", feedBackModel.Ratings);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "FeedBack is Recorder";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
    }
}
