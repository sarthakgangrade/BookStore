using BusinessLayer.Interface;
using CommonLayer.Models.Admin;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdminBL : IAdminBL
    {
        IAdminRL adminRL;

        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        //public string AddAdmin(AdminModel adminPost)
        //{
        //    try
        //    {
        //        return adminRL.AddAdmin(adminPost);

        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        public string Adminlogin(string MailId, string Password)
        {
            try
            {
                return adminRL.Adminlogin(MailId, Password);


            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
