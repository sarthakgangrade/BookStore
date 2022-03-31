using CommonLayer.Models.Admin;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repository_Layer.Services
{
    public class AdminRL : IAdminRL
    {
        private SqlConnection sqlConnection;

        public AdminRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        //public string AddAdmin(AdminModel adminPost)
        //{
        //    sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
        //    try
        //    {
        //        using (sqlConnection)
        //        {
        //            SqlCommand cmd = new SqlCommand("sp_AddAdmin", sqlConnection);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@AdminName", adminPost.AdminName);
        //            cmd.Parameters.AddWithValue("@MailId", adminPost.MailId);
        //            cmd.Parameters.AddWithValue("@Password", adminPost.Password);

        //            sqlConnection.Open();
        //            cmd.ExecuteNonQuery();
        //            return "Admin Added  successfully";

        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
        //    }
        //}


        public string Adminlogin(string MailId, string Password)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));

            try
            {
                using (sqlConnection)
                {

                    SqlCommand cmd = new SqlCommand("sp_LoginAdmin", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MailId", MailId);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            MailId = reader["MailId"].ToString();
                            Password = reader["Password"].ToString();
                            
                        }
                        string token = GenerateAdminToken(MailId);
                        return token;
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

        private static string GenerateAdminToken(string MailId)
        {
            if (MailId == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role,"Admin"),
                    
                    new Claim("MailId", MailId),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}