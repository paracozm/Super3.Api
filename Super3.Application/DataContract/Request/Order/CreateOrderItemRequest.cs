using System.ComponentModel.DataAnnotations;

namespace Super3.Application.DataContract.Request.Order
{
    public sealed class CreateOrderItemRequest
    {
        //public int OrderId { get; set; }
        public string ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public int TotalAmount { get; set; }
    }
}
