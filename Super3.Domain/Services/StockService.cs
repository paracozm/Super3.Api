using Super3.Domain.Interfaces.Repositories;
using Super3.Domain.Interfaces.Services;
using Super3.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super3.Domain.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        public Task CreateAsync(Stock stock)
        {
            throw new NotImplementedException();
        }

        public Task<Stock> GetByIdAsync(string stockId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Stock>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Stock stock)
        {
            throw new NotImplementedException();
        }
    }
}
