using Super3.Application.DataContract.Request.Customer;
using Super3.Application.DataContract.Response.Customer;
using Super3.Domain.Validations.Base;

namespace Super3.Application.Interfaces
{
    public interface ICustomerApplication
    {
        Task<Response> CreateAsync(CreateCustomerRequest request);
        Task<Response<CustomerResponse>> GetByIdAsync(int Id);
        Task<Response<List<CustomerResponse>>> GetAllAsync();
        
    }

}
