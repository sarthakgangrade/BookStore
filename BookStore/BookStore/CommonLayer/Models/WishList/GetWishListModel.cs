using CommonLayer.Models.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models.WishList
{
    public class GetWishListModel
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public BookWishModel Books { get; set; }
    }
}
