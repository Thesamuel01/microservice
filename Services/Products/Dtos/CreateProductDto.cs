using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Services.Products.Dtos;

public class CreateProductDto
{
    [Required]
    public uint SKU { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }
}
