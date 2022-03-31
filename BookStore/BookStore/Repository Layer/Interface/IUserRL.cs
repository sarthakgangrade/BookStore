using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IUserRL
    {
        public bool Register(RegisterModel userData);
        public string Login(string emailId, string password);
        public bool ForgetPassword(string email);
        public bool ResetPassword(ResetPassword reset, string email);
    }
}
