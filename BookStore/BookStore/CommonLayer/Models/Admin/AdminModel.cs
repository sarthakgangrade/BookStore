using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models.Admin
{
    public class AdminModel
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string MailId { get; set; }
        public string Password { get; set; }

        public string ServiceType { get; } = "Admin";
    }
}
