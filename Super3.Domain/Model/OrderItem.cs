namespace Super3.Domain.Model
{
    public class OrderItem
    {
        public int Id { get; set; }
        public decimal ProductPrice { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }

        
    }
}
