using BusinessLayer.Interface;
using CommonLayer.Models.WishList;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishListBL : IWishListBL
    {
        IWishListRL wishlistRL;
        public WishListBL(IWishListRL wishlistRL)
        {
            this.wishlistRL = wishlistRL;
        }

        public string AddWishlist(WishListModel wishlist)
        {
            try
            {
                return this.wishlistRL.AddWishlist(wishlist);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string DeleteBookFromWishlist(int wishlistId)
        {
            try
            {
                return this.wishlistRL.DeleteBookFromWishlist(wishlistId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<GetWishListModel> GetWishlist(int userId)
        {
            try
            {
                return this.wishlistRL.GetWishlist(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
