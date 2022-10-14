namespace Super3.Domain.Model
{
    public class Order
    {
        public string Id { get; set; }
        public Customer Customer { get; set; }
        public Product Product { get; set; }
        public OrderItem Item { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItem> Items { get; set; }

    }
}
