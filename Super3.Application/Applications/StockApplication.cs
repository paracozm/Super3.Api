using AutoMapper;
using Super3.Application.DataContract.Request.Product;
using Super3.Application.DataContract.Request.Stock;
using Super3.Application.DataContract.Response.Customer;
using Super3.Application.DataContract.Response.Stock;
using Super3.Application.Interfaces;
using Super3.Domain.Interfaces.Services;
using Super3.Domain.Model;
using Super3.Domain.Services;
using Super3.Domain.Validations.Base;

namespace Super3.Application.Applications
{
    public class StockApplication : IStockApplication
    {
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public StockApplication(IStockService stockService, IMapper mapper)

        {
            _mapper = mapper;
            _stockService = stockService;
        }

        public async Task<Response> CreateAsync(CreateStockRequest stock)
        {
            try
            {
                var stockModel = _mapper.Map<Stock>(stock);
                return await _stockService.CreateAsync(stockModel);
            }
            catch (Exception ex)
            {
                var response = Report.Create(ex.Message);
                return Response.Unprocessable(response);
            }
        }

        public async Task<Response<List<StockResponse>>> GetAllAsync()
        {
            Response<List<Stock>> stock = await _stockService.GetAllAsync();

            if (stock.Report.Any())
                return Response.Unprocessable<List<StockResponse>>(stock.Report);

            var response = _mapper.Map<List<StockResponse>>(stock.Data);

            return Response.OK(response);
        }

        public Task<Response<StockResponse>> GetByIdAsync(string stockId)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> UpdateAsync(UpdateStockRequest request)
        {
            try
            {
                var stock = _mapper.Map<Stock>(request);
                return await _stockService.UpdateAsync(stock);
            }
            catch (Exception ex)
            {
                var response = Report.Create(ex.Message);
                return Response.Unprocessable(response);
            }
        }
    }
}
