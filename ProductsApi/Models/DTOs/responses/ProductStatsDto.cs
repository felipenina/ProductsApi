namespace ProductsApi.Models.DTOs.responses;

public class ProductStatsDto
{
    public int Total { get; set; }
    public decimal AveragePrice { get; set; }
    public string MostExpensiveName { get; set; } = string.Empty;
}