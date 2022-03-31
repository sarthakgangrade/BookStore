using CommonLayer.Models.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IAdminRL
    {
        //string AddAdmin(AdminModel adminPost);
        string Adminlogin(string MailId, string Password);

    }
}
