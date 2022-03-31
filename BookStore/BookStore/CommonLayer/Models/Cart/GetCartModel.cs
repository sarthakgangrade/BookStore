using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models.Cart
{
    public class GetCartModel
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int OrderQuantity { get; set; }
        public BookModel bookModel { get; set; }
    }
}
