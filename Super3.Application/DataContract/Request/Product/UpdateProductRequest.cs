using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super3.Application.DataContract.Request.Product
{
    public sealed class UpdateProductRequest
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
    }
}
