using System.ComponentModel.DataAnnotations;

namespace Super3.Application.DataContract.Request.Order
{
    public sealed class CreateOrderRequest
    {
        [Required] public int CustomerId { get; set; } //FK
        [Required] public decimal TotalPrice { get; set; }
        public List<CreateOrderItemRequest> Items { get; set; }
    }
}
