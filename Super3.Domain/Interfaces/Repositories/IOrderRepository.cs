using Super3.Domain.Model;

namespace Super3.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order order);
        Task CreateItemAsync(OrderItem item);
        Task<Order> GetByIdAsync(int orderId);
        Task<List<Order>> GetAllAsync();
        Task<List<OrderItem>> ListItemByOrderIdAsync(int orderId);
        Task<bool> ExistsByIdAsync(int orderId);
       
    }
}
