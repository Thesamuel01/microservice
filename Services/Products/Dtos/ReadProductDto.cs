namespace ProductAPI.Services.Products.Dtos;

public class ReadProductDto
{
    public uint SKU { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime DateUpdate { get; set; }
}
