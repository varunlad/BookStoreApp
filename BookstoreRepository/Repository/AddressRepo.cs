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
        public string UpdateAddress(AddressModel addressModel)
        {
            int result;
            string msg;
            try
            {
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_UpdateAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uaddressId", addressModel.AddressId);
                    cmd.Parameters.AddWithValue("@uaddress", addressModel.Address);
                    cmd.Parameters.AddWithValue("@ucity", addressModel.City);
                    cmd.Parameters.AddWithValue("@ustate", addressModel.State);
                    cmd.Parameters.AddWithValue("@utypeId", addressModel.TypeId);

                    con.Open();
                    //ExecuteScalar: This method only returns a single value. This kind of query returns a count of rows or a calculated value.
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    //Switch statement
                    msg = result switch
                    {
                        -1 => "AddressId Does not Exits",
                        _ => "Address is Updated",
                    };
                }
                return msg;
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
        public IEnumerable<AddressModel> DisplayAllAddress()
        {
            try
            {
                List<AddressModel> tempList = new List<AddressModel>();
                IEnumerable<AddressModel> result;
                string ConnectionStrings = config.GetConnectionString(connectionString);
                using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_GetAllAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            AddressModel addressModel = new AddressModel();
                            addressModel.AddressId = Convert.ToInt32(rdr["addressId"]);
                            addressModel.UserId = Convert.ToInt32(rdr["userId"]);
                            addressModel.TypeId = Convert.ToInt32(rdr["typeId"]);
                            addressModel.Address = rdr["typeId"].ToString();
                            addressModel.City = rdr["city"].ToString();
                            addressModel.State = rdr["state"].ToString();
                            tempList.Add(addressModel);
                        }
                    }
                    result = tempList;
                    return result;
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
    }
}
