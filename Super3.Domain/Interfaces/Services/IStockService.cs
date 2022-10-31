using Super3.Domain.Model;
using Super3.Domain.Validations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super3.Domain.Interfaces.Services
{
    public interface IStockService
    {
        Task<Response> CreateAsync(Stock stock);
        Task<Response> UpdateAsync(Stock stock);
        Task<Response<Stock>> GetByIdAsync(string stockId);
        Task<Response<List<Stock>>> GetAllAsync();
        }
}
