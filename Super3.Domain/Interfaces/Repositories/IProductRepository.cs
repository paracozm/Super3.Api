using Super3.Domain.Model;

namespace Super3.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task<Product> GetByIdAsync(string productId);
        Task<List<Product>> GetAllAsync();
        Task<bool> ExistsByIdAsync(string productId);
    }
    

    
}
 