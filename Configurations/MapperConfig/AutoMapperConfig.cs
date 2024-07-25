using AutoMapper;
using TestDtoInApi.Models;
using TestDtoInApi.Models.DTO.CategoryDTO;
using TestDtoInApi.Models.DTO.ProductDTO;

namespace TestDtoInApi.Configurations.MapperConfig
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // Category Dto Mapping

            CreateMap<Category, SelectCategoryDto>()
                .ForMember(x => x.CategoryId, opt => opt.MapFrom(x => x.CategoryId))
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.CategoryName))
                .ReverseMap();

            CreateMap<Category, UpdateCategoryDto>()
                .ForMember(x => x.CategoryId, opt => opt.MapFrom(x => x.CategoryId))
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.CategoryName))
                .ReverseMap();

            CreateMap<Category, CreateCategoryDto>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.CategoryName))
                .ReverseMap();

            // Product Dto Mapping

            CreateMap<Product, SelectProductDto>()
                .ForMember(x => x.ProductId, opt => opt.MapFrom(x => x.ProductId))
                .ForMember(x => x.ProductName, opt => opt.MapFrom(x => x.ProductName))
                .ForMember(x => x.ProductPrice, opt => opt.MapFrom(x => x.ProductPrice))
                .ForMember(x => x.InStock, opt => opt.MapFrom(x => x.InStock))
                .ForMember(x => x.CategoryId, opt => opt.MapFrom(x => x.CategoryId))
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.CategoryName))
                .ReverseMap();

            CreateMap<Product, CreateProductDto>()
                .ForMember(x => x.ProductId, opt => opt.MapFrom(x => x.ProductId))
                .ForMember(x => x.ProductName, opt => opt.MapFrom(x => x.ProductName))
                .ForMember(x => x.ProductPrice, opt => opt.MapFrom(x => x.ProductPrice))
                .ForMember(x => x.InStock, opt => opt.MapFrom(x => x.InStock))
                .ForMember(x => x.CategoryId, opt => opt.MapFrom(x => x.CategoryId))
                .ReverseMap();

            CreateMap<Product, UpdateProductDto>()
                .ForMember(x => x.ProductId, opt => opt.MapFrom(x => x.ProductId))
                .ForMember(x => x.ProductName, opt => opt.MapFrom(x => x.ProductName))
                .ForMember(x => x.ProductPrice, opt => opt.MapFrom(x => x.ProductPrice))
                .ForMember(x => x.InStock, opt => opt.MapFrom(x => x.InStock))
                .ForMember(x => x.CategoryId, opt => opt.MapFrom(x => x.CategoryId))
                .ReverseMap();
        }
    }
}
