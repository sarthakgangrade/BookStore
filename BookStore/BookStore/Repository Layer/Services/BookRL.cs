using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Repository_Layer.Services
{
    public class BookRL : IBookRL
    {
        private SqlConnection sqlConnection;
        public IConfiguration Configuration { get; }
        public BookRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddBook(BookModel book)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "Sp_AddBooks";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@BookName", book.BookName);
                    sqlCommand.Parameters.AddWithValue("@AuthorName", book.AuthorName);
                    sqlCommand.Parameters.AddWithValue("@DiscountPrice", book.DiscountPrice);
                    sqlCommand.Parameters.AddWithValue("@OriginalPrice", book.OriginalPrice);
                    sqlCommand.Parameters.AddWithValue("@BookDescription", book.BookDescription);
                    sqlCommand.Parameters.AddWithValue("@Rating", book.Rating);
                    sqlCommand.Parameters.AddWithValue("@Reviewer", book.Reviewer);
                    sqlCommand.Parameters.AddWithValue("@Image", book.Image);
                    sqlCommand.Parameters.AddWithValue("@BookCount", book.BookCount);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    return "Book Added succssfully";
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

        public string UpdateBookDetails(UpdateBookModel update,int BookId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "sp_UpdateBooks";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@BookId",BookId);
                    sqlCommand.Parameters.AddWithValue("@BookName", update.BookName);
                    sqlCommand.Parameters.AddWithValue("@AuthorName", update.AuthorName);
                    sqlCommand.Parameters.AddWithValue("@DiscountPrice", update.DiscountPrice);
                    sqlCommand.Parameters.AddWithValue("@OriginalPrice", update.OriginalPrice);
                    sqlCommand.Parameters.AddWithValue("@BookDescription", update.BookDescription);
                    sqlCommand.Parameters.AddWithValue("@Rating", update.Rating);
                    sqlCommand.Parameters.AddWithValue("@Reviewer", update.Reviewer);
                    sqlCommand.Parameters.AddWithValue("@Image", update.Image);
                    sqlCommand.Parameters.AddWithValue("@BookCount", update.BookCount);
                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                        if (result == 1)
                        {
                        return "Update book details Unsuccessful";
                    }
                    else
                    {
                        return "Details Updated Successfully";
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

        public string DeleteBook(int BookId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {

                using (sqlConnection)
                {
                    string storeprocedure = "spDeleteBooks";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@BookId", BookId);
                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 1)
                    {
                        return "Bookid does not exists";
                    }
                    else
                    {
                        return "Book details deleted successfully";
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

        public List<BookModel> GetAllBooks()
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "spGetAllBooks";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    if (sqlData.HasRows)
                    {
                        List<BookModel> allBook = new List<BookModel>();
                        while (sqlData.Read())
                        {
                            BookModel bookModel = new BookModel();
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
                            allBook.Add(bookModel);
                        }
                        return allBook;
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

        public List<BookModel> GetAllBooksbyBookId(int BookId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    List<BookModel> BookList = new List<BookModel>();
                    string storeprocedure = "spGetBooks";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    BookModel bookModel = new BookModel();
                    sqlCommand.Parameters.AddWithValue("@BookId", BookId);
                    sqlConnection.Open();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    if (sqlData.HasRows)
                    {
                        while (sqlData.Read())
                        {
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
                            BookList.Add(bookModel);
                        }
                        return BookList;
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
