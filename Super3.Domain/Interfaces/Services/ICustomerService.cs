using Super3.Domain.Model;
using Super3.Domain.Validations.Base;

namespace Super3.Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<Response> CreateAsync(Customer customer);
        //Task<Response> UpdateAsync(Customer customer);
        Task<Response<Customer>> GetByIdAsync(int customerId);
        Task<Response<List<Customer>>> GetAllAsync();
        //Task<Response<Customer>> GetByIdAsync(int id);
    }
}
