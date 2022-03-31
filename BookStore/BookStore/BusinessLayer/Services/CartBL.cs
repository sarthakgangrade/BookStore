using BusinessLayer.Interface;
using CommonLayer.Models;
using CommonLayer.Models.Cart;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }
        public string AddToCart(CartModel cartModel)
        {
            try
            {
                return cartRL.AddToCart(cartModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string DeleteCart(int CartID)
        {
            try
            {
                return cartRL.DeleteCart(CartID);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<GetCartModel> GetCartDetails(int UserId)
        {
            try
            {
                return cartRL.GetCartDetails(UserId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string UpdateCartQuantity(int CartID, int quantity)
        {
            try
            {
                return this.cartRL.UpdateCartQuantity(CartID, quantity);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
