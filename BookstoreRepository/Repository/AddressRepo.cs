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
    public class AddressRepo : IAddressRepo
    {
        private readonly IConfiguration config;
        public string connectionString { get; set; } = "BookStoreDB";
        public AddressRepo(IConfiguration configration)
        {
            this.config = configration;
        }
        public string AddressDetail(AddressModel addressModel)
        {
            try
            {
                if (addressModel != null)
                {
                    string ConnectionStrings = config.GetConnectionString(connectionString);
                    using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                    {
                        MySqlCommand cmd = new MySqlCommand("sp_AddAddress", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@auserId", addressModel.UserId);
                        cmd.Parameters.AddWithValue("@atypeId", addressModel.TypeId);
                        cmd.Parameters.AddWithValue("@aaddress", addressModel.Address);
                        cmd.Parameters.AddWithValue("@acity", addressModel.City);
                        cmd.Parameters.AddWithValue("@astate", addressModel.State);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        return "Address is Added";
                    }
                }
                return "Address is Not Added";
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
    }
}
