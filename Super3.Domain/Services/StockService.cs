using Super3.Domain.Interfaces.Repositories;
using Super3.Domain.Interfaces.Services;
using Super3.Domain.Model;
using Super3.Domain.Validations;
using Super3.Domain.Validations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super3.Domain.Services
{
    public class StockService : IStockService
    {

        private readonly IUnitOfWork _unitOfWork;
        public StockService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Response<Stock>> GetByIdAsync(string productId)
        {
            var response = new Response<Stock>();


            var exists = await _unitOfWork.ProductRepository.ExistsByIdAsync(productId);

            if (!exists)
            {
                response.Report.Add(Report.Create($"SKU {productId} doesn't exist!"));
                return response;
            }
            
            var data = await _unitOfWork.StockRepository.GetByIdAsync(productId);
            response.Data = data;
            return response;
        }

        public async Task<Response<List<Stock>>> GetAllAsync()
        {
            var response = new Response<List<Stock>>();


            var data = await _unitOfWork.StockRepository.GetAllAsync();
            response.Data = data;
            return response;
        }



        public async Task<Response> CreateAsync(Stock stock)
        {
            var response = new Response();
            var validation = new StockValidation();
            var errors = validation.Validate(stock).GetErrors();

            if (errors.Report.Count > 0) return errors;


            var exists = await _unitOfWork.ProductRepository.ExistsByIdAsync(stock.Product.Id);
            if (!exists)
            {
                response.Report.Add(Report.Create($"SKU {stock.Product.Id} doesn't exist!"));
                return response;
            }

            await _unitOfWork.StockRepository.CreateAsync(stock);

            return response;
        } 



        public async Task<Response> UpdateAsync(Stock stock)
        {
            var response = new Response();
            var validation = new StockValidation();
            var errors = validation.Validate(stock).GetErrors();

            if (errors.Report.Count > 0) return errors;


            var exists = await _unitOfWork.ProductRepository.ExistsByIdAsync(stock.Product.Id);
            if (!exists)
            {
                response.Report.Add(Report.Create($"SKU {stock.Product.Id} doesn't exist!"));
                return response;
            }

            await _unitOfWork.StockRepository.UpdateAsync(stock);

            return response;

        }
    }
}
