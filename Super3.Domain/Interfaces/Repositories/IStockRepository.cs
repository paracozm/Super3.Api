using Super3.Domain.Model;

namespace Super3.Domain.Interfaces.Repositories
{
    public interface IStockRepository
    {
        Task CreateAsync(Stock stock);
        Task UpdateAsync(Stock stock);
        Task<Stock> GetByIdAsync(string stockId);
        Task<List<Stock>> GetAllAsync();
    }
   
}
