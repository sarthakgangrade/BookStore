using CommonLayer.Models.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IOrderBL
    {
        public string AddOrder(OrderModel order);
        public List<GetOrderModel> AllOrderDetails(int UserId);
    }
}
