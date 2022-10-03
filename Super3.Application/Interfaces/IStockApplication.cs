using Super3.Application.DataContract.Request.Stock;
using Super3.Application.DataContract.Response.Stock;
using Super3.Domain.Validations.Base;

namespace Super3.Application.Interfaces
{
    public interface IStockApplication
    {
        Task<Response> CreateAsync(CreateStockRequest stock);
        Task<Response<StockResponse>> GetByIdAsync(int stockId);
        Task<Response<List<StockResponse>>> GetAllAsync();
        Task<Response> UpdateAsync(CreateStockRequest stock);
    }
}
