using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Services.Products.Dtos;

public class UpdateProductDto
{
    [Required]
    public uint SKU { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}
