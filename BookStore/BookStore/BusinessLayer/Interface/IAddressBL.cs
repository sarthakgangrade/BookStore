using CommonLayer.Models.Address;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
   public interface IAddressBL
    {
        public string AddAddress(AddressModel address);
        public string UpdateAddress(UpdateAddressModel address);
        public List<UpdateAddressModel> GetAllAddresses();
        public List<UpdateAddressModel> GetAddressesbyUserid(int UserId);

    }
}
