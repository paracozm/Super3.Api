using Super3.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super3.Application.DataContract.Response.Order
{
    public sealed class OrderItemResponse
    {
        
        //public string OrderId { get; set; }
        public string ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public int TotalAmount { get; set; }
    }
}
