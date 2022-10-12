using Super3.Domain.Model;
using Super3.Domain.Validations.Base;

namespace Super3.Domain.Interfaces.Services
{
    public interface IOrderService
    {

        Task<Response<List<Order>>> GetAllAsync();
        Task<Response<Order>> GetByIdAsync(int orderId);
        Task<Response> CreateAsync(Order order);

    }
}
