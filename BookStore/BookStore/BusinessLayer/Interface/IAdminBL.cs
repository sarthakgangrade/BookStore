using CommonLayer.Models.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAdminBL
    {
        //string AddAdmin(AdminModel adminPost);
        string Adminlogin(string MailId, string Password);
    }
}
