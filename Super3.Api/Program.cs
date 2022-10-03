using Microsoft.AspNetCore.Hosting;
using Super3.Application.Applications;
using Super3.Application.Interfaces;
using Super3.Application.Mapper;
using Super3.Domain.Interfaces.Repositories;
using Super3.Domain.Interfaces.Repositories.DataConnector;
using Super3.Domain.Interfaces.Services;
using Super3.Domain.Services;
using Super3.Infra.DataConnector;
using Super3.Infra.Repositories;


var builder = WebApplication.CreateBuilder(args);




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Core));

var builder2 = WebApplication.CreateBuilder(args);
string connString = builder2.Configuration.GetConnectionString("default");
builder.Services.AddScoped<IDbConnector>(db => new SqlConnector(connString));

//DI
builder.Services.AddScoped<IUnityOfWork, UnityOfWork>();
builder.Services.AddScoped<ICustomerApplication, CustomerApplication>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductApplication, ProductApplication>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
