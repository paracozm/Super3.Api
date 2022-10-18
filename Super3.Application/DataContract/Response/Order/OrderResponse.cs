using Super3.Application.DataContract.Request.Order;

namespace Super3.Application.DataContract.Response.Order
{
    public sealed class OrderResponse
    {
        public string Id { get; set; }
        public int CustomerId { get; set; } //FK
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        
        public List<OrderItemResponse> Items { get; set; }
        

    }
}
