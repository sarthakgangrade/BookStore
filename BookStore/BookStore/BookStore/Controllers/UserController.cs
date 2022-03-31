using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("register")]
        public IActionResult UserRegistration(RegisterModel model)
        {
            try
            {
                if (this.userBL.Register(model))
                {
                    return this.Ok(new { Success = true, message = "Registration Successful", Response = model});
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Registration Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }

        [HttpPost("login/{EmailId}/{Password}")]
        public IActionResult UserLogin(string EmailId, string Password)
        {
            try
            {
                var loginCrendential = this.userBL.Login(EmailId, Password);
                if (loginCrendential != null)
                {
                    return this.Ok(new { Success = true, message = "Login Successful", token = loginCrendential });
                }
                else
                {
                    return this.Ok(new { Success = false, message = "Invalid User Please enter valid email and password." });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = e.Message });

            }
        }

        [HttpPost("forgotPassword/{Email}")]
        public IActionResult ForgetPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return BadRequest("Email should not be null or empty");
            }

            try
            {
                if (this.userBL.ForgetPassword(Email))
                {
                    return Ok(new { Success = true, message = "Password reset link sent on mail successfully" });
                }
                else
                {
                    return Ok(new { Success = false, message = "Password reset link not sent" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message, msg = ex.InnerException });
            }
        }


        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(ResetPassword newPassword)
        {
            try
            {
                var userEmail = User.FindFirst("Email").Value.ToString();
                if (this.userBL.ResetPassword(newPassword, userEmail))
                {
                    return Ok(new { Success = true, message = "Password reset successfully" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Password reset Unsuccesfull"});
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message, msg = ex.InnerException });
            }
        }
    }
}
