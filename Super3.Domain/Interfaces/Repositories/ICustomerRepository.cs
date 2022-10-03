using Super3.Domain.Model;

namespace Super3.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task CreateAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task<Customer> GetByIdAsync(int customerId);
        Task<List<Customer>> GetAllAsync();
        Task<bool> ExistsByIdAsync(int customerId);
        //Task ExistsByIdAsync(int customerId);
        //Task ExistsByIdAsync(int id);
    }
}
 