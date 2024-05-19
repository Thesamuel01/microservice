using ProductAPI.Data.DataBase;
using ProductAPI.Data.Helpers;
using ProductAPI.Domains;

namespace ProductAPI.Data;

public class ProductRepository : IProductRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public ProductRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Product?> GetProduct(uint sku)
    {
        var query = $@"SELECT cod_sku_pk, name, description, date_update FROM products WHERE cod_sku_pk = @sku;";
        var parameters = new Dictionary<string, object> { { "@sku", int.Parse(sku.ToString()) } };

        using var conn = _connectionFactory.GetConnection();
        var data = await conn.ExecuteReaderAsync(query, parameters);

        if (data == null) return null;

        return ProductMapper.Map<Product>(data).FirstOrDefault();
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        var query = $@"SELECT cod_sku_pk, name, description, date_update FROM products;";
        var parameters = new Dictionary<string, object>();

        using var conn = _connectionFactory.GetConnection();
        var data = await conn.ExecuteReaderAsync(query, parameters);

        if (data == null) return Enumerable.Empty<Product>();

        return ProductMapper.Map<Product>(data);
    }

    public async Task<bool> CreateProduct(Product product)
    {
        var query = $@"
            INSERT INTO products
                (cod_sku_pk, name, description)
            VALUES
                (@cod_sku_pk, @name, @description);";
        var parameters = new Dictionary<string, object>
        {
            { "@cod_sku_pk", int.Parse(product.SKU.ToString()) },
            { "@name", product.Name },
            { "@description", product.Description },
        };

        using var conn = _connectionFactory.GetConnection();
        var hasCreated = await conn.ExecuteQueryAsync(query, parameters);

        return hasCreated;
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var updateFields = new List<string>();
        var parameters = new Dictionary<string, object>
        {
            { "@cod_sku_pk", int.Parse(product.SKU.ToString()) }
        };

        if (!string.IsNullOrEmpty(product.Name))
        {
            updateFields.Add("name = @name");
            parameters.Add("@name", product.Name);
        }

        if (!string.IsNullOrEmpty(product.Description))
        {
            updateFields.Add("description = @description");
            parameters.Add("@description", product.Description);
        }

        if (updateFields.Count == 0)
        {
            return false;
        }

        var query = $@"
            UPDATE products
            SET {string.Join(", ", updateFields)}
            WHERE cod_sku_pk = @cod_sku_pk;";

        using var conn = _connectionFactory.GetConnection();
        var hasUpdated = await conn.ExecuteQueryAsync(query, parameters);

        return hasUpdated;
    }

    public async Task<bool> DeleteProduct(uint sku)
    {
        var query = $@"DELETE FROM products WHERE cod_sku_pk = @sku;";
        var parameters = new Dictionary<string, object> { { "@sku", int.Parse(sku.ToString()) } };
        
        using var conn = _connectionFactory.GetConnection();
        var hasDeleted = await conn.ExecuteQueryAsync(query, parameters);

        return hasDeleted;
    }
}
