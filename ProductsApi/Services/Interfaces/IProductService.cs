using ProductsApi.Models.DTOs.requests;
using ProductsApi.Models.DTOs.responses;
using ProductsApi.Models.DTOs.Responses;

namespace ProductsApi.Services.interfaces;

public interface IProductService
{
    List<ProductForReadDto> GetAllProducts();
    ProductForReadDto? GetProductById(int id);
    ProductForReadDto CreateProduct(ProductForCreateDto dto);
    void UpdateProduct(int id, ProductForUpdateDto dto);
    void DeleteProduct(int id);
    List<ProductForReadDto> SearchProductsByName(string name); 
    ProductStatsDto GetStats();
}