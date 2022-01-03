
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using StackExchange.Redis;
using System;

using System.Data;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
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
                     string protectedPassword = EncryptPassword(user.Password);
                    using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                    {
                        MySqlCommand cmd = new MySqlCommand("sp_Signup", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sFullNmae", user.FullName);
                        cmd.Parameters.AddWithValue("@semail", user.Email);
                        cmd.Parameters.AddWithValue("@suserPass", protectedPassword);
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
        public string EncryptPassword(string password)
        {
            SHA384 sha384Hash = SHA384.Create(); ////creating object (it is a abstract class thats why we use create() method)
            // ComputeHash - returns byte array  
            byte[] bytesRepresentation = sha384Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytesRepresentation);
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
        public string RestPassword(ResetPasswordModel resetPassword)
        {
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_UpdatePassword", con);
                    string protectedPassword = EncryptPassword(resetPassword.NewPassword);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@femail", resetPassword.Email);
                    cmd.Parameters.AddWithValue("@fnewPassword", protectedPassword);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Password is Reset";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
        public string ForgotPassword(string email)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(this.config["Credentials:Email"]);
                mail.To.Add(email);
                mail.Subject = "Test Mail";
                this.SendMSMQ();
                mail.Body = this.ReceiveMSMQ();
                smtpServer.Port = 587;
                smtpServer.Credentials = new System.Net.NetworkCredential(this.config["Credentials:Email"], this.config["Credentials:Password"]);
                smtpServer.EnableSsl = true;
                smtpServer.Send(mail);
                return "Email send";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// With the Help of Message Queue the email is safely send and receive to the Destination.
        /// </summary>
        public void SendMSMQ()
        {
            MessageQueue messageQueue;
            if (MessageQueue.Exists(@".\Private$\BookStore"))
            {
                messageQueue = new MessageQueue(@".\Private$\BookStore");
            }
            else
            {
                messageQueue = MessageQueue.Create(@".\Private$\BookStore");
            }
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            string body = "This is for Testing SMTP mail from GMAIL";
            messageQueue.Label = "Mail Body";
            messageQueue.Send(body);
        }

        /// <summary>
        /// With the Help of Message Queue the email is safely send and receive to the Destination.
        /// </summary>
        /// <returns>Ensures the message is received to the receive.</returns>
        public string ReceiveMSMQ()
        {
            MessageQueue messageQueue = new MessageQueue(@".\Private$\BookStore");
            var receivemsg = messageQueue.Receive();
            receivemsg.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return receivemsg.Body.ToString();
        }
    }
}
