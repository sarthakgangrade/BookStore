using BusinessLayer.Interface;
using CommonLayer.Models.Address;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressBL : IAddressBL
    {
        IAddressRL addressRL;
        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }

        public string AddAddress(AddressModel address)
        {
            try
            {
                return addressRL.AddAddress(address);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string UpdateAddress(UpdateAddressModel address)
        {
            try
            {
                return addressRL.UpdateAddress(address);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<UpdateAddressModel> GetAllAddresses()
        {
            try
            {
                return addressRL.GetAllAddresses();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<UpdateAddressModel> GetAddressesbyUserid(int UserId)
        {
            try
            {
                return addressRL.GetAddressesbyUserid(UserId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
