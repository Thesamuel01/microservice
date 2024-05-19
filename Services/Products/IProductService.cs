using ProductAPI.Services.Products.Dtos;

namespace ProductAPI.Services.Products;

public interface IProductService
{
    Task<ReadProductDto?> GetProduct(uint sku);

    Task<IEnumerable<ReadProductDto>> GetProducts();

    Task<bool> CreateProduct(CreateProductDto product);

    Task<bool> UpdateProduct(UpdateProductDto product);

    Task<bool> DeleteProduct(uint sku);
}
