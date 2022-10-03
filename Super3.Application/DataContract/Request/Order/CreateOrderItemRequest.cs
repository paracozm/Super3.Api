using System.ComponentModel.DataAnnotations;

namespace Super3.Application.DataContract.Request.Order
{
    public sealed class CreateOrderItemRequest
    {
        public int OrderId { get; set; }
        [Required] public int ProductId { get; set; }
        [Required] public decimal ProductPrice { get; set; }
    }
}
