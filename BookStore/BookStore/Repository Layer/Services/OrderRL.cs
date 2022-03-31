using CommonLayer.Models;
using CommonLayer.Models.Book;
using CommonLayer.Models.Order;
using Microsoft.Extensions.Configuration;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Repository_Layer.Services
{
    public class OrderRL : IOrderRL
    {
        private SqlConnection sqlConnection;
        public IConfiguration Configuration { get; }
        public OrderRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddOrder(OrderModel order)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "sp_AddingOrders";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", order.UserId);
                    sqlCommand.Parameters.AddWithValue("@AddressId", order.AddressId);
                    sqlCommand.Parameters.AddWithValue("@BookId", order.BookId);
                    sqlCommand.Parameters.AddWithValue("@BookQuantity", order.BookQuantity);

                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 2)
                    {
                        return "BookId not exists";
                    }
                    else if (result == 1)
                    {
                        return "UserId not exists";
                    }
                    else
                    {
                        return "Ordered successfully";
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

        public List<GetOrderModel> AllOrderDetails(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "sp_GetAllOrders";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    List<GetOrderModel> order = new List<GetOrderModel>();
                    if (sqlData.HasRows)
                    {
                        while (sqlData.Read())
                        {
                            GetOrderModel orderModel = new GetOrderModel();
                            BookGetOrderModel getbookModel = new BookGetOrderModel();
                            getbookModel.BookId = Convert.ToInt32(sqlData["BookId"]);
                            getbookModel.BookName = sqlData["BookName"].ToString();
                            getbookModel.AuthorName = sqlData["AuthorName"].ToString();
                            getbookModel.DiscountPrice = Convert.ToInt32(sqlData["DiscountPrice"]);
                            getbookModel.OriginalPrice = Convert.ToInt32(sqlData["OriginalPrice"]);
                            getbookModel.BookDescription = sqlData["BookDescription"].ToString();
                            getbookModel.Image = sqlData["Image"].ToString();
                            orderModel.OrderId = Convert.ToInt32(sqlData["OrdersId"]);
                            //orderModel.UserId = Convert.ToInt32(sqlData["UserId"]);
                            //orderModel.AddressId = Convert.ToInt32(sqlData["AddressId"]);
                            //orderModel.BookId = Convert.ToInt32(sqlData["BookId"]);
                            //orderModel.BookQuantity = Convert.ToInt32(sqlData["BookQuantity"]);
                            orderModel.OrderDate = sqlData["OrderDate"].ToString();
                            orderModel.getbookModel = getbookModel;
                            order.Add(orderModel);
                        }
                        return order;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
