using BusinessLayer.Interface;
using CommonLayer.Models.Order;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrderBL : IOrderBL
    {
        IOrderRL orderRL;
        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }

        public string AddOrder(OrderModel order)
        {
            try
            {
                return this.orderRL.AddOrder(order);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<GetOrderModel> AllOrderDetails(int UserId)
        {
            try
            {
                return this.orderRL.AllOrderDetails(UserId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
