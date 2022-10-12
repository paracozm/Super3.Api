using AutoMapper;
using Super3.Application.DataContract.Request.Customer;
using Super3.Application.DataContract.Request.Product;
using Super3.Application.DataContract.Response.Customer;
using Super3.Application.DataContract.Response.Product;
using Super3.Domain.Model;

namespace Super3.Application.Mapper
{
    public class Core : Profile
    {
        public Core()
        {
            CustomerMap();
            ProductMap();
        }
        private void CustomerMap()
        {
            CreateMap<CreateCustomerRequest, Customer>();
            CreateMap<Customer, CustomerResponse>();
        }

        private void ProductMap()
        {
            CreateMap<CreateProductRequest, Product>();
            CreateMap<Product, ProductResponse>();
            CreateMap<UpdateProductRequest, Product>();
        }
    }
}
 