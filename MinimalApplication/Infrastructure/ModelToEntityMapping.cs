using AutoMapper;
using MinimalApplication.Domain.Entities.Category;
using MinimalApplication.Infrastructure.Repositories;

namespace MinimalApplication.Infrastructure;

public class ModelToEntityMapping:Profile
{
    public ModelToEntityMapping()
    {
       // CreateMap<CategoryModel, CategoryEntity>().ReverseMap();
        //CreateMap<CategoryEntity, CategoryModel>();
        //CreateMap<CategoryModel, CategoryEntity>();
    }
    
}
