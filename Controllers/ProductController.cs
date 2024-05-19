using Microsoft.AspNetCore.Mvc;
using ProductAPI.Services.Products;
using ProductAPI.Services.Products.Dtos;

namespace ProductAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _productService.GetProducts();

        return Ok(result);
    }

    [HttpGet("{sku}")]
    public async Task<IActionResult> GetProduct(uint sku)
    {
        var result = await _productService.GetProduct(sku);

        if (result == null)
        {
            return NotFound($"Product {sku} not found");
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductDto data)
    {
        try
        {
            var result = await _productService.CreateProduct(data);

            if (!result)
            {
                return UnprocessableEntity($"Product was not created");
            }

            return Created("Product created", data);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception)
        {

            return StatusCode(500);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(UpdateProductDto data)
    {
        try
        {
            var result = await _productService.UpdateProduct(data);

            if (!result)
            {
                return UnprocessableEntity($"Product was not updated");
            }

            return Created("Product updated", data);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpDelete("{sku}")]
    public async Task<IActionResult> DeleteProduct(uint sku)
    {
        await _productService.DeleteProduct(sku);

        return NoContent();
    }
}
