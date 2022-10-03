using Super3.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super3.Domain.Interfaces.Services
{
    public interface IStockService
    {
        Task CreateAsync(Stock stock);
        Task UpdateAsync(Stock stock);
        Task<Stock> GetByIdAsync(string stockId);
        Task<List<Stock>> GetAllAsync();
        }
}
