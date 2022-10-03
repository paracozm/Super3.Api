using Super3.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super3.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task CreateAsync(Order order);
        
        Task<Order> GetByIdAsync(string orderId);
        Task<List<Order>> GetAllAsync();
        
    }
}
