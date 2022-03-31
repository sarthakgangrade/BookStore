using BusinessLayer.Interface;
using CommonLayer.Models.WishList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        IWishListBL wishlistBL;

        public WishListController(IWishListBL wishlistBL)
        {
            this.wishlistBL = wishlistBL;
        }

        [HttpPost]
        [Route("addToWishlist")]
        public IActionResult AddWishlist(WishListModel wishlist)
        {
            try
            {
                string result = this.wishlistBL.AddWishlist(wishlist);
                if (result.Equals("Book Wishlisted successfully"))
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

        [HttpDelete]
        [Route("deleteWishlist/{wishlistId}")]
        public IActionResult DeleteBookFromWishlist(int wishlistId)
        {
            try
            {
                string result = this.wishlistBL.DeleteBookFromWishlist(wishlistId);
                if (result.Equals("Wishlist deleted successfully"))
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
        [Route("getWishlistDetails/{UserId}")]
        public IActionResult GetWishlist(int UserId)
        {
            try
            {
                var result = this.wishlistBL.GetWishlist(UserId);
                if (result != null)
                {

                    return this.Ok(new { Status = true, Message = "Wishlist Displayed", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Unable to fetch WishList" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
