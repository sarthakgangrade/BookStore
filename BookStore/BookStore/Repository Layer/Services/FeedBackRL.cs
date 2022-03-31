using CommonLayer.Models;
using CommonLayer.Models.FeedBack;
using CommonLayer.Models.User;
using Microsoft.Extensions.Configuration;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Repository_Layer.Services
{
    public class FeedBackRL : IFeedBackRL
    {
        private SqlConnection sqlConnection;
        public IConfiguration Configuration { get; }
        public FeedBackRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddFeedback(FeedBackModel feedback)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("Sp_AddFeedback", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserId", feedback.UserId);
                    sqlCommand.Parameters.AddWithValue("@BookId", feedback.BookId);
                    sqlCommand.Parameters.AddWithValue("@Comments", feedback.Comments);
                    sqlCommand.Parameters.AddWithValue("@Ratings", feedback.Ratings);
                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 2)
                    {
                        return "BookId not exists";
                    }
                    else if (result == 1)
                    {
                        return "Already given Feedback for this book";
                    }
                    else
                    {
                        return "Feedback added successfully";
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

        public List<GetFeedBackModel> FeedBackDetails(int bookId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "sp_GetFeedback";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@BookId", bookId);
                    sqlConnection.Open();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    List<GetFeedBackModel> feedback = new List<GetFeedBackModel>();
                    if (sqlData.HasRows)
                    {
                        while (sqlData.Read())
                        {
                            GetFeedBackModel feedbackModel = new GetFeedBackModel();
                            GetFeedBackUserDetails user = new GetFeedBackUserDetails();
                            user.FullName = sqlData["FullName"].ToString();
                            user.Email = sqlData["Email"].ToString();
                            feedbackModel.Comments = sqlData["Comments"].ToString();
                            feedbackModel.Ratings = Convert.ToInt32(sqlData["Ratings"]);
                            feedbackModel.UserId = Convert.ToInt32(sqlData["UserId"]);
                            feedbackModel.User = user;
                            feedback.Add(feedbackModel);
                        }
                        return feedback;
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
