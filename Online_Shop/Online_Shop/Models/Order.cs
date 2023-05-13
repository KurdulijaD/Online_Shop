﻿using Online_Shop.Common;

namespace Online_Shop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public string Comment { get; set; }
        public string Address { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public EOrderStatus Status { get; set; }
    }
}
