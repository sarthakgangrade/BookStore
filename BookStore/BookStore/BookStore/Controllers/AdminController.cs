using BusinessLayer.Interface;
using CommonLayer.Models.Admin;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    
        [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {

            IAdminBL adminBL;
            public AdminController(IAdminBL adminBL)
            {
                this.adminBL = adminBL;

            }

            //[HttpPost("addAdmin")]
            //public IActionResult AddAdmin(AdminModel adminPost)
            //{
            //    try
            //    {
            //        var result = this.adminBL.AddAdmin(adminPost);
            //        if (result.Equals("Admin Added  successfully"))
            //        {
            //            return this.Ok(new { success = true, message = $"Admin Added Successfully " });
            //        }
            //        else
            //        {
            //            return this.BadRequest(new { Status = false, Message = result });
            //        }

            //    }
            //    catch (Exception e)
            //    {
            //        throw e;
            //    }

            //}

        [HttpPost("Login/{MailId}/{Password}")]

        public IActionResult AdminLogin(string MailId, string Password)
        {
            try
            {
                var login = this.adminBL.Adminlogin(MailId, Password);
                if (login != null)
                {
                    return this.Ok(new { Success = true, message = "Login Successful", token = login });
                }
                else
                {
                    return this.Ok(new { Success = false, message = "Invalid User Please enter valid email and password." });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });

            }
        }

    }           
}

