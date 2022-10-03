using Super3.Domain.Model;

namespace Super3.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order order);
        Task CreateItemAsync(OrderItem item);
        Task<Order> GetByIdAsync(string orderId);
        Task<List<Order>> GetAllAsync();
        Task<List<OrderItem>> ListItemByOrderIdAsync(string orderId);
        Task<bool> ExistsByIdAsync(string orderId);
       
    }
}
