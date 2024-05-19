using ProductAPI.Domains;

namespace ProductAPI.Data;

public interface IProductRepository
{
    Task<Product?> GetProduct(uint sku);

    Task<IEnumerable<Product>> GetProducts();

    Task<bool> CreateProduct(Product product);

    Task<bool> UpdateProduct(Product product);

    Task<bool> DeleteProduct(uint sku);
}
