
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using StackExchange.Redis;
using System;

using System.Data;
using UserModel;
using UserRepository.Interface;

namespace UserRepository
{
    public class userRepo : IuserRepo
    {
        private readonly IConfiguration config;
        public string connectionString { get; set; } = "BookStoreDB";
        public userRepo(IConfiguration configration)
        {
            this.config = configration;
        }

        public string Register(SignupModel user)
        {
            int result;
            string msg;
          try
          {
                if (user != null)
                {
                    string ConnectionStrings = config.GetConnectionString(connectionString);
                    // string protectedPassword = EncryptPassword(user.userPassword);
                    using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                    {
                        MySqlCommand cmd = new MySqlCommand("sp_Signup", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sFullNmae", user.FullName);
                        cmd.Parameters.AddWithValue("@semail", user.Email);
                        cmd.Parameters.AddWithValue("@suserPass", user.Password);
                        cmd.Parameters.AddWithValue("@sPhoneNumber", user.PhoneNumber);
                        con.Open();
                        //cmd.ExecuteNonQuery();
                        result = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                        //Switch statement
                        msg = result switch
                        {
                            -1 => "Email Already Exist",
                            _ => "Registration Successful",
                        };
                    }
                    return msg;
                }
                else 
                {
                    return "Registration Unsuccessful";
                }
          }
          catch (ArgumentNullException e)
          {
                throw new ArgumentNullException(e.Message);
          }
        }
    }
}
