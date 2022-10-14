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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        async Task<Response<List<Product>>> IProductService.GetAllAsync()
        {
            var response = new Response<List<Product>>();

            var data = await _unitOfWork.ProductRepository.GetAllAsync();
            response.Data = data;
            return response;
        }
        async Task<Response<Product>> IProductService.GetByIdAsync(string productId)
        {
            var response = new Response<Product>();

           
            var exists = await _unitOfWork.ProductRepository.ExistsByIdAsync(productId);

            if (!exists)
            {
                response.Report.Add(Report.Create($"Customer {productId} doesn't exist!"));
                return response;
            }
            //var customerIdStr = customerId.ToString();
            var data = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            response.Data = data;
            return response;
        }
        async Task<Response> IProductService.CreateAsync(Product product)
        {
            var response = new Response();
            

            var validation = new ProductValidation();

            var errors = validation.Validate(product).GetErrors();
            if (errors.Report.Count > 0) return errors;
            product.Id = Guid.NewGuid().ToString("N");
            await _unitOfWork.ProductRepository.CreateAsync(product);

            return response;
        }
        async Task<Response> IProductService.UpdateAsync(Product product)
        {
            var response = new Response();
            var validation = new ProductValidation();
            var errors = validation.Validate(product).GetErrors();

            if (errors.Report.Count > 0) return errors;

            
            var exists = await _unitOfWork.ProductRepository.ExistsByIdAsync(product.Id);
            if (!exists)
            {
                response.Report.Add(Report.Create($"Customer {product.Id} doesn't exist!"));
                return response;
            }

            await _unitOfWork.ProductRepository.UpdateAsync(product);

            return response;
        }
    }
}
