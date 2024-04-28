using AutoMapper;
using MinimalApplication.Domain.Entities.Category;
using MinimalApplication.Infrastructure.Repositories;
using MinimalApplication.UseCases.Category;
using MinimalApplication.UseCases.Category.DTOs;

namespace MinimalApplication.Domain.Mappings;

public class DomainToDTOMapping:Profile
{
    public DomainToDTOMapping()
    {
        CreateMap<CategoryEntity, CategoryCreateDTO>().ReverseMap();
        CreateMap<CategoryEntity, CategoryUpdateDTO>().ReverseMap();
        CreateMap<CategoryEntity, CategoryOutputDTO>().ReverseMap();
        //CreateMap<CategoryModel, CategoryEntity>().ReverseMap();
        //CreateMap<CategoryEntity, CategoryModel>();
    }
}
