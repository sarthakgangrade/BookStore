using CommonLayer.Models.Address;
using Microsoft.Extensions.Configuration;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Repository_Layer.Services
{
    public class AddressRL : IAddressRL
    {
        private SqlConnection sqlConnection;
        public IConfiguration Configuration { get; }
        public AddressRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddAddress(AddressModel address)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "Sp_AddAddress";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", address.UserId);
                    sqlCommand.Parameters.AddWithValue("@Address", address.Address);
                    sqlCommand.Parameters.AddWithValue("@City", address.City);
                    sqlCommand.Parameters.AddWithValue("@State", address.State);
                    sqlCommand.Parameters.AddWithValue("@TypeId", address.TypeId);

                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 1)
                    {
                        return "UserId not exists";
                    }
                    else
                    {
                        return "Address Added succssfully";
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public string UpdateAddress(UpdateAddressModel address)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "sp_UpdateAddress";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@AddressId", address.AddressId);
                    sqlCommand.Parameters.AddWithValue("@Address", address.Address);
                    sqlCommand.Parameters.AddWithValue("@City", address.City);
                    sqlCommand.Parameters.AddWithValue("@State", address.State);
                    sqlCommand.Parameters.AddWithValue("@TypeId", address.TypeId);

                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 1)
                    {
                        return "Address not exists";
                    }
                    else
                    {
                        return "Address updated succssfully";
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<UpdateAddressModel> GetAllAddresses()
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "sp_GetAllAddresses";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    if (sqlData.HasRows)
                    {
                        List<UpdateAddressModel> allAddress = new List<UpdateAddressModel>();
                        while (sqlData.Read())
                        {
                            UpdateAddressModel address = new UpdateAddressModel();
                            address.AddressId = Convert.ToInt32(sqlData["AddressId"]);
                            address.Address = sqlData["Address"].ToString();
                            address.City = sqlData["City"].ToString();
                            address.State = sqlData["State"].ToString();
                            address.TypeId = Convert.ToInt32(sqlData["TypeId"]);
                            allAddress.Add(address);
                        }
                        return allAddress;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<UpdateAddressModel> GetAddressesbyUserid(int UserId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    List<UpdateAddressModel> address = new List<UpdateAddressModel>();
                    SqlCommand sqlCommand = new SqlCommand("sp_GetAddressbyUserid", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                    sqlConnection.Open();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    if (sqlData.HasRows)
                    {
                        while (sqlData.Read())
                        {
                            UpdateAddressModel model = new UpdateAddressModel();
                            model.AddressId = Convert.ToInt32(sqlData["AddressId"]);
                            model.Address = sqlData["Address"].ToString();
                            model.City = sqlData["City"].ToString();
                            model.State = sqlData["State"].ToString();
                            model.TypeId = Convert.ToInt32(sqlData["TypeId"]);
                            address.Add(model);
                        }
                        return address;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
