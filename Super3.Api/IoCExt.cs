using Super3.Application.Applications;
using Super3.Application.Interfaces;
using Super3.Domain.Interfaces.Repositories;
using Super3.Domain.Interfaces.Services;
using Super3.Domain.Services;
using Super3.Infra.Repositories;

namespace Super3.Api
{
    public static class IoCExt
    {
        public static void RegisterIoC(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICustomerApplication, CustomerApplication>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<IProductApplication, ProductApplication>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IOrderApplication, OrderApplication>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IStockApplication, StockApplication>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IStockRepository, StockRepository>();
        }
    }
}
