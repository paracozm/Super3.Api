﻿namespace Super3.Domain.Model
{
    public class OrderItem
    {
        public string OrderId { get; set; }
        public decimal ProductPrice { get; set; }
        public int TotalAmount { get; set; }
        public Order Order { get; set; }
        public List<Order> Orders { get; set; }
        public Product Product { get; set; }
    }
}
