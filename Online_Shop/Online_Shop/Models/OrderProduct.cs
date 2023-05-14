﻿namespace Online_Shop.Models
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}