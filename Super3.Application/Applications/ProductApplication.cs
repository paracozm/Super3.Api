using AutoMapper;
using Super3.Application.DataContract.Request.Customer;
using Super3.Application.DataContract.Request.Product;
using Super3.Application.DataContract.Response.Customer;
using Super3.Application.DataContract.Response.Product;
using Super3.Application.Interfaces;
using Super3.Domain.Interfaces.Services;
using Super3.Domain.Model;
using Super3.Domain.Services;
using Super3.Domain.Validations.Base;

namespace Super3.Application.Applications
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductApplication(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        public async Task<Response<List<ProductResponse>>> GetAllAsync()
        {
            Response<List<Product>> product = await _productService.GetAllAsync();

            if (product.Report.Any())
                return Response.Unprocessable<List<ProductResponse>>(product.Report);

            var response = _mapper.Map<List<ProductResponse>>(product.Data);

            return Response.OK(response);
        }



        public async Task<Response<ProductResponse>> GetByIdAsync(string productId)
        {
            //var Id2 = Id.ToString();
            Response<Product> product = await _productService.GetByIdAsync(productId);

            if (product.Report.Any())
                return Response.Unprocessable<ProductResponse>(product.Report);

            var response = _mapper.Map<ProductResponse>(product.Data);

            return Response.OK(response);
        }





        public async Task<Response> CreateAsync(CreateProductRequest product)
        {
            try
            {
                var productModel = _mapper.Map<Product>(product);
                await _productService.CreateAsync(productModel);
                return Response.OK(product);
            }
            catch (Exception ex)
            {
                var response = Report.Create(ex.Message);
                return Response.Unprocessable(response);
            }
        }



        public async Task<Response> UpdateAsync(UpdateProductRequest request)
        {
            {
                var product = _mapper.Map<Product>(request);

                return await _productService.UpdateAsync(product);
            }
        }
    }
}
