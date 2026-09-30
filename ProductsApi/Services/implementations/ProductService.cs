using ProductsApi.Entities;
using ProductsApi.Models.DTOs.requests;
using ProductsApi.Models.DTOs.responses;
using ProductsApi.Models.DTOs.Responses;
using ProductsApi.Repositories.Implementations;
using ProductsApi.Repositories.Interfaces;
using ProductsApi.Services.interfaces;

namespace ProductsApi.Services.implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public List<ProductForReadDto> GetAllProducts()
    {
        return _repository.GetAllProducts().Select(p => new ProductForReadDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price
        }).ToList();
    }

    public ProductForReadDto? GetProductById(int id)
    {
        var product = _repository.GetProductById(id);
        if (product == null) return null;

        return new ProductForReadDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }

    public ProductForReadDto CreateProduct(ProductForCreateDto dto)
    {
        
        var existingProducts = _repository.GetAllProducts();
        if (existingProducts.Any(p => p.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException($"Ya existe un producto con el nombre '{dto.Name}'.");
        }

        var productEntity = new Product
        {
            Name = dto.Name,
            Price = dto.Price
        };

        _repository.AddProduct(productEntity);

        return new ProductForReadDto
        {
            Id = productEntity.Id,
            Name = productEntity.Name,
            Price = productEntity.Price
        };
    }

    public void UpdateProduct(int id, ProductForUpdateDto dto)
    {
        var existingProduct = _repository.GetProductById(id);
        if (existingProduct == null) return;

        
        var all = _repository.GetAllProducts();
        if (all.Any(p => p.Id != id && p.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException($"Ya existe otro producto con el nombre '{dto.Name}'.");
        }

        existingProduct.Name = dto.Name;
        existingProduct.Price = dto.Price;

        _repository.UpdateProduct(existingProduct);
    }

    public void DeleteProduct(int id)
    {
        var existingProduct = _repository.GetProductById(id);
        if (existingProduct != null)
        {
            _repository.DeleteProduct(existingProduct);
        }
    }

    public List<ProductForReadDto> SearchProductsByName(string name)
    {
        var results = _repository.SearchProductsByName(name);
        return results.Select(p => new ProductForReadDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price
        }).ToList();
    }

    
    public ProductStatsDto GetStats()
    {
        var products = _repository.GetAllProducts();

        if (!products.Any())
        {
            return new ProductStatsDto
            {
                Total = 0,
                AveragePrice = 0,
                MostExpensiveName = "No hay productos disponibles"
            };
        }

        return new ProductStatsDto
        {
            Total = products.Count(),
            AveragePrice = products.Average(p => p.Price),
            MostExpensiveName = products.OrderByDescending(p => p.Price).First().Name
        };
    }
}