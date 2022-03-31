using CommonLayer.Models;
using CommonLayer.Models.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface ICartRL
    {
        public string AddToCart(CartModel cartModel);
        public string DeleteCart(int CartID);
        public List<GetCartModel> GetCartDetails(int UserId);
        public string UpdateCartQuantity(int CartID, int quantity);
    }
}
