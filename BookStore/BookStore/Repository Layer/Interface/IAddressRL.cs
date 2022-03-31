using CommonLayer.Models.Address;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IAddressRL
    {
        public string AddAddress(AddressModel address);
        public string UpdateAddress(UpdateAddressModel address);
        public List<UpdateAddressModel> GetAllAddresses();
        public List<UpdateAddressModel> GetAddressesbyUserid(int UserId);
    }
}
