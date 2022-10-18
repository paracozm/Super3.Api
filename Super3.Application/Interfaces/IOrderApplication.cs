using Super3.Application.DataContract.Request.Customer;
using Super3.Application.DataContract.Request.Order;
using Super3.Application.DataContract.Response.Order;
using Super3.Domain.Validations.Base;

namespace Super3.Application.Interfaces
{
    public interface IOrderApplication
    {
        Task<Response> CreateAsync(CreateOrderRequest order);
        Task<Response<OrderResponse>> GetByIdAsync(string orderId);
        Task<Response<List<OrderResponse>>> GetAllAsync();
        
    }
}
