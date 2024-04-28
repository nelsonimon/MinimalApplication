using MinimalApplication.UseCases.Category.DTOs;

namespace MinimalApplication.UseCases.Category.Interface;

public interface ICategoryUC
{
    Task<IEnumerable<CategoryOutputDTO>> GetCategories();
    Task<CategoryOutputDTO> GetById(int id);
    Task<int> Add(CategoryCreateDTO categoryDto);
    Task<bool> Update(CategoryUpdateDTO categoryDto);
    Task<bool> Remove(int id);
}
