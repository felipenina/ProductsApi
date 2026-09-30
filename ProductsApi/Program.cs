using ProductsApi.Repositories.Implementations;
using ProductsApi.Repositories.Interfaces;
using ProductsApi.Services.implementations;
using ProductsApi.Services.interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();