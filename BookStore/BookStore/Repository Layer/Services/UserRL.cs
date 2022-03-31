using CommonLayer.Models;
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
    public class UserRL :IUserRL
    {
        private SqlConnection sqlConnection;

        public UserRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public bool Register(RegisterModel register)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));

            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_AddUsers", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@FullName", register.FullName);
                    sqlCommand.Parameters.AddWithValue("@Email", register.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", register.Password);
                    sqlCommand.Parameters.AddWithValue("@PhoneNo", register.PhoneNo);
                   
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public string Login(string EmailId, string password)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    RegisterModel model = new RegisterModel();

                    SqlCommand command = new SqlCommand("sp_Login", sqlConnection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", EmailId);
                    command.Parameters.AddWithValue("@Password", password);
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.Email = Convert.ToString(reader["Email"] == DBNull.Value ? default : reader["Email"]);
                            model.Password = Convert.ToString(reader["Password"] == DBNull.Value ? default : reader["Password"]);
                        }
                        string token = GenerateToken(EmailId);
                        return token;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }

           
        }
        public string GenerateToken(string Email)
        {
            if (Email == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", Email),
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

        private static string GenerateJWTToken(string EmailId, int UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("EmailId", EmailId.ToString()),
                    new Claim("UserId", UserId.ToString())
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

        public bool ForgetPassword(string EmailId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    RegisterModel model = new RegisterModel();
                    SqlCommand command = new SqlCommand("sp_ForgetPassword", sqlConnection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", EmailId);

                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.Email = Convert.ToString(reader["Email"] == DBNull.Value ? default : reader["Email"]);
                        }
                        string token = GenerateToken(EmailId);
                        new MsMq().Sender(token);

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool ResetPassword(ResetPassword newPassword, string EmailId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
                using (this.sqlConnection)
                {
                    RegisterModel model = new RegisterModel();
                    SqlCommand command = new SqlCommand("sp_ResetPassword", this.sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", EmailId);
                    command.Parameters.AddWithValue("@Password", newPassword.Password);
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.Email = Convert.ToString(reader["Email"] == DBNull.Value ? default : reader["Email"]);
                            model.Password = Convert.ToString(reader["Password"] == DBNull.Value ? default : reader["Password"]);

                        }
                        return true;
                    }
                    else
                    {
                        return false;

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
