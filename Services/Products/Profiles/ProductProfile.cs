using AutoMapper;
using ProductAPI.Domains;
using ProductAPI.Services.Products.Dtos;

namespace ProductAPI.Services.Products.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ReadProductDto>();
        CreateMap<CreateProductDto, Product>().ReverseMap();
        CreateMap<UpdateProductDto, Product>().ReverseMap();
    }
}
