namespace MinimalApplication.Domain.Entities.Category;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryEntity>> GetAll();
    Task<CategoryEntity> GetById(int? id);
    Task<int> Create(CategoryEntity category);
    Task<bool> Update(CategoryEntity category);
    Task<bool> Remove(CategoryEntity category);
}
