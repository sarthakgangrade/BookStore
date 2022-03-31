using CommonLayer.Models.WishList;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IWishListBL
    {
        public string AddWishlist(WishListModel wishlist);
        public string DeleteBookFromWishlist(int wishlistId);
        public List<GetWishListModel> GetWishlist(int UserId);
    }
}
