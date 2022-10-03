using Super3.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super3.Application.DataContract.Response.Order
{
    public sealed class OrderResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; } //FK
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemResponse> Items { get; set; }
    }
}
