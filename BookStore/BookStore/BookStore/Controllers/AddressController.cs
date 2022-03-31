using BusinessLayer.Interface;
using CommonLayer.Models.Address;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        IAddressBL addressBL;
        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }

        [HttpPost]
        [Route("addAddress")]

        public IActionResult AddAddress(AddressModel address)
        {
            try
            {
                string result = this.addressBL.AddAddress(address);
                if (result.Equals("Address Added succssfully"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpPut]
        [Route("updateAddress")]

        public IActionResult UpdateAddress (UpdateAddressModel address)
        {
            try
            {
                string result = this.addressBL.UpdateAddress(address);
                if (result.Equals("Address updated succssfully"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("getAllAddress")]
        public IActionResult GetAllAddresses()
        {
            try
            {
                var result = this.addressBL.GetAllAddresses();
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Retrieval all addresses succssful", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Retrieval is unsucessful" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new{ Status = false, Message = e.Message });
            }
        }


        [HttpGet]
        [Route("getAddressbyUserid/{UserId}")]
        public IActionResult GetAddressesbyUserid(int UserId)
        {
            try
            {
                var result = this.addressBL.GetAddressesbyUserid(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Retrieval all addresses succssful", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "UserId not Exist" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

    }
}
