using Super3.Application.DataContract.Request.Order;
using Super3.Application.DataContract.Response.Order;
using Super3.Application.Interfaces;
using Super3.Domain.Validations.Base;

namespace Super3.Application.Applications
{
    public class OrderApplication : IOrderApplication
    {
        public Task<Response> CreateAsync(CreateOrderRequest order)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<OrderResponse>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<OrderResponse>> GetByIdAsync(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
