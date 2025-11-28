using AutoMapper;
using E_Commerce.Core.Entities.Product;
using E_Commerce.Infrastructure.Data.DTOs;

namespace E_Commerce.API.Mapping
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
