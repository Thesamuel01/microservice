using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Data;
using ProductAPI.Domains;
using ProductAPI.Services.Products.Dtos;
using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Services.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _repository = productRepository;
        _mapper = mapper;
    }

    public async Task<ReadProductDto?> GetProduct(uint sku)
    {
        var data = await _repository.GetProduct(sku);
        return _mapper.Map<ReadProductDto>(data);
    }

    public async Task<IEnumerable<ReadProductDto>> GetProducts()
    {
        var data = await _repository.GetProducts();
        return _mapper.Map<IEnumerable<ReadProductDto>>(data);
    }

    public async Task<bool> CreateProduct(CreateProductDto product)
    {
        ValidateProductDto(product);

        var checkProduct = await GetProduct(product.SKU);

        if (checkProduct != null)
        {
            throw new InvalidOperationException($"Sku {product.SKU} is already filled.");
        }

        return await _repository.CreateProduct(_mapper.Map<Product>(product));
    }

    public async Task<bool> UpdateProduct(UpdateProductDto product)
    {
        ValidateProductDto(product);

        var checkProduct = await GetProduct(product.SKU);

        if (checkProduct == null)
        {
            throw new InvalidOperationException($"Sku {product.SKU} not found.");
        }

        return await _repository.UpdateProduct(_mapper.Map<Product>(product));
    }

    private static void ValidateProductDto(object product)
    {
        var validationContext = new ValidationContext(product);
        var validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(product, validationContext, validationResults, true);

        if (!isValid)
        {
            var errors = string.Join("; ", validationResults.Select(r => r.ErrorMessage));
            throw new ValidationException($"Validation failed: {errors}");
        }
    }

    public async Task<bool> DeleteProduct(uint sku)
    {
        var result = await _repository.DeleteProduct(sku);
        return result;
    }
}
