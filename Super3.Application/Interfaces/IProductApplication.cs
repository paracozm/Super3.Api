using Super3.Application.DataContract.Request.Product;
using Super3.Application.DataContract.Response.Product;
using Super3.Domain.Validations.Base;

namespace Super3.Application.Interfaces
{
    public interface IProductApplication
    {
        Task<Response> CreateAsync(CreateProductRequest product);
        Task<Response<ProductResponse>> GetByIdAsync(int productId);
        Task<Response<List<ProductResponse>>> GetAllAsync();
        Task<Response> UpdateAsync(UpdateProductRequest product);

    }
}

