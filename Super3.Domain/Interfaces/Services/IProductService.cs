using Super3.Domain.Model;
using Super3.Domain.Validations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super3.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<Response> CreateAsync(Product product);
        Task<Response> UpdateAsync(Product product);
        Task<Response<Product>> GetByIdAsync(string productId);
        Task<Response<List<Product>>> GetAllAsync();
    }
}
