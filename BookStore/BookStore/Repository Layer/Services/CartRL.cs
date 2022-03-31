using CommonLayer.Models;
using CommonLayer.Models.Cart;
using Microsoft.Extensions.Configuration;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Repository_Layer.Services
{
    public class CartRL : ICartRL
    {
        private SqlConnection sqlConnection;
        public IConfiguration Configuration { get; }
        public CartRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddToCart(CartModel cartModel)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                { 
                    SqlCommand sqlCommand = new SqlCommand("sp_AddingCart", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@BookId", cartModel.BookId);
                    sqlCommand.Parameters.AddWithValue("@UserId", cartModel.UserId);
                    sqlCommand.Parameters.AddWithValue("@OrderQuantity", cartModel.OrderQuantity);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    return "Book Added succssfully to Cart";
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

        public string DeleteCart(int cartId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {

                using (sqlConnection)
                {
                    string storeprocedure = "sp_DeleteCartDetails";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@CartID", cartId);
                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 1)
                    {
                        return "CartID does not exists";
                    }
                    else
                    {
                        return "Cart details deleted successfully";
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<GetCartModel> GetCartDetails(int UserId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    List<GetCartModel> cart = new List<GetCartModel>();
                    string storeprocedure = "sp_GetCartDetails";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                    sqlConnection.Open();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    
                    if (sqlData.HasRows)
                    {
                        while (sqlData.Read())
                        {
                            BookModel bookModel = new BookModel();
                            GetCartModel getCartModel = new GetCartModel();
                            bookModel.BookId = Convert.ToInt32(sqlData["BookId"]);
                            bookModel.BookName = sqlData["BookName"].ToString();
                            bookModel.AuthorName = sqlData["AuthorName"].ToString();
                            bookModel.DiscountPrice = Convert.ToInt32(sqlData["DiscountPrice"]);
                            bookModel.OriginalPrice = Convert.ToInt32(sqlData["OriginalPrice"]);
                            bookModel.BookDescription = sqlData["BookDescription"].ToString();
                            bookModel.Rating = Convert.ToInt32(sqlData["Rating"]);
                            bookModel.Reviewer = Convert.ToInt32(sqlData["Reviewer"]);
                            bookModel.Image = sqlData["Image"].ToString();
                            bookModel.BookCount = Convert.ToInt32(sqlData["BookCount"]);

                            getCartModel.UserId = Convert.ToInt32(sqlData["UserId"]);
                            getCartModel.BookId = Convert.ToInt32(sqlData["BookId"]);
                            getCartModel.OrderQuantity = Convert.ToInt32(sqlData["OrderQuantity"]);
                            getCartModel.bookModel = bookModel;
                            cart.Add(getCartModel);
                        }
                        return cart;
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

        public string UpdateCartQuantity(int CartID, int quantity)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_UpdateQuantity", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@CartID", CartID);
                    sqlCommand.Parameters.AddWithValue("@OrderQuantity", quantity);
                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 1)
                    {
                        return "Update is Unsuccessful";
                    }
                    else
                    {
                        return "Quantity Updated Successfully";
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
    }
}
