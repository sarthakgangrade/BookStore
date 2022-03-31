﻿using CommonLayer.Models;
using CommonLayer.Models.Book;
using CommonLayer.Models.WishList;
using Microsoft.Extensions.Configuration;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Repository_Layer.Services
{
    public class WishListRL : IWishListRL
    {
        private SqlConnection sqlConnection;
        public IConfiguration Configuration { get; }
        public WishListRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddWishlist(WishListModel wishlist)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "sp_CreateWishlist";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", wishlist.UserId);
                    sqlCommand.Parameters.AddWithValue("@BookId", wishlist.BookId);

                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 2)
                    {
                        return "BookId not exists";
                    }
                    else if (result == 1)
                    {
                        return "Book already added to wishlist";
                    }
                    else
                    {
                        return "Book Wishlisted successfully";
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

        public string DeleteBookFromWishlist(int wishlistId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {

                using (sqlConnection)
                {
                    string storeprocedure = "sp_DeleteWishlist";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@WishlistId", wishlistId);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    return "Wishlist deleted successfully";
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

        public List<GetWishListModel> GetWishlist(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "sp_ShowWishlistbyUserId";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    List<GetWishListModel> wishlist = new List<GetWishListModel>();
                    if (sqlData.HasRows)
                    {
                        while (sqlData.Read())
                        {
                            GetWishListModel wish = new GetWishListModel();
                            BookWishModel bookModel = new BookWishModel();
                            bookModel.BookName = sqlData["BookName"].ToString();
                            bookModel.AuthorName = sqlData["AuthorName"].ToString();
                            bookModel.DiscountPrice = Convert.ToInt32(sqlData["DiscountPrice"]);
                            bookModel.OriginalPrice = Convert.ToInt32(sqlData["OriginalPrice"]);
                            bookModel.BookDescription = sqlData["BookDescription"].ToString();
                            bookModel.Image = sqlData["Image"].ToString();
                            wish.UserId = Convert.ToInt32(sqlData["UserId"]);
                            wish.BookId = Convert.ToInt32(sqlData["BookId"]);
                            wish.Books = bookModel;
                            wishlist.Add(wish);
                        }
                        return wishlist;
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
