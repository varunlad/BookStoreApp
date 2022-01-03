
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
        public string Login(LoginModel login)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_Login", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@lEmail", login.Email);
                    cmd.Parameters.AddWithValue("@lPassword", login.Password);
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    SignupModel userDetail = new SignupModel();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            userDetail.userId = Convert.ToInt32(rdr["userId"]);
                            userDetail.FullName = rdr["userFullName"].ToString();
                            userDetail.Email = rdr["userEmail"].ToString();
                            userDetail.PhoneNumber = Convert.ToInt64(rdr["userPhnoneNumber"]);
                            if (userDetail != null)
                            {
                                ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                                IDatabase database = connectionMultiplexer.GetDatabase();
                                database.StringSet(key: "Full Name", userDetail.FullName);
                                database.StringSet(key: "email", userDetail.Email);
                                database.StringSet(key: "Phn Number", userDetail.PhoneNumber);
                                database.StringSet(key: "User Id", userDetail.userId);
                                con.Close();
                                return "Login Successful";
                            }
                        }
                    }
                    con.Close();
                    return "Login Unsuccessful";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }

        }
    }
}
