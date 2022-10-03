using Super3.Application.DataContract.Request.Stock;
using Super3.Application.DataContract.Response.Stock;
using Super3.Application.Interfaces;
using Super3.Domain.Validations.Base;

namespace Super3.Application.Applications
{
    public class StockApplication : IStockApplication
    {
        public Task<Response> CreateAsync(CreateStockRequest stock)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<StockResponse>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<StockResponse>> GetByIdAsync(int stockId)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateAsync(CreateStockRequest stock)
        {
            throw new NotImplementedException();
        }
    }
}
