using CommonLayer.Models.WishList;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IWishListRL
    {
        public string AddWishlist(WishListModel wishlist);
        public string DeleteBookFromWishlist(int wishlistId);
        public List<GetWishListModel> GetWishlist(int UserId);
    }
}
