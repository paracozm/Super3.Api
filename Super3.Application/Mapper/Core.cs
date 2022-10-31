using AutoMapper;
using Super3.Application.DataContract.Request.Customer;
using Super3.Application.DataContract.Request.Order;
using Super3.Application.DataContract.Request.Product;
using Super3.Application.DataContract.Request.Stock;
using Super3.Application.DataContract.Response.Customer;
using Super3.Application.DataContract.Response.Order;
using Super3.Application.DataContract.Response.Product;
using Super3.Application.DataContract.Response.Stock;
using Super3.Domain.Model;

namespace Super3.Application.Mapper
{
    public class Core : Profile
    {
        public Core()
        {
            CustomerMap();
            ProductMap();
            OrderMap();
            StockMap();
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

        private void OrderMap()
        {
            CreateMap<CreateOrderRequest, Order>()
                .ForPath(dest => dest.Customer.Id, opt => opt.MapFrom(src => src.CustomerId));

            CreateMap<Order, OrderResponse>();

            CreateMap<CreateOrderItemRequest, OrderItem>()
                .ForPath(dest => dest.Product.Id, opt => opt.MapFrom(source => source.ProductId));

            CreateMap<OrderItem, OrderItemResponse>();

            
        }

        private void StockMap()
        {
            CreateMap<CreateStockRequest, Stock>()
                .ForPath(dest => dest.Product.Id, opt => opt.MapFrom(src => src.ProductId));
            CreateMap<Stock, StockResponse>();
        }


    }
}
 