namespace ProductHub.API.DTOs;

public class ProductDto
{
    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string Category { get; set; } = string.Empty;
}