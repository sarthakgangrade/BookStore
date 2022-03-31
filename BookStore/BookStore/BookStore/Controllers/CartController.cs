using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        ICartBL cartBL;

        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [Authorize]
        [HttpPost]
        [Route("addToCarts")]
        public IActionResult AddToCart(CartModel cart)
        {
            try
            {
                //int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                string result = this.cartBL.AddToCart(cart);
                if (result != null)
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

        //[Authorize]
        [HttpDelete]
        [Route("deleteBook/{CartID}")]
        public IActionResult DeleteCart(int CartID)
        {
            try
            {
                string result = this.cartBL.DeleteCart(CartID);
                if (result.Equals("Cart details deleted successfully"))
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

        [Authorize]
        [HttpGet]
        [Route("getCartDetails/{UserId}")]
        public IActionResult GetCartDetails(int UserId)
        {
            try
            {
                var result = this.cartBL.GetCartDetails(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Cart Data retrieved successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new{ Status = false, Message = "Get cart details is unsuccessful" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        //[Authorize]
        [HttpPut]
        [Route("updateBookQuantity/{CartID}/{quantity}")]
        public IActionResult UpdateCartQuantity(int CartID, int quantity)
        {
            try
            {
                string result = cartBL.UpdateCartQuantity(CartID, quantity);
                if (result.Equals("Quantity Updated Successfully"))
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
    }
}
