using AutoMapper;
using MinimalApplication.Domain.Entities.Category;
using MinimalApplication.UseCases.Category.DTOs;
using MinimalApplication.UseCases.Category.Interface;
using MinimalApplication.UseCases.Exceptions;

namespace MinimalApplication.UseCases.Category;

public class CategoryUC : ICategoryUC
{
    private ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryUC(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _mapper = mapper;
    }


    public async Task<int> Add(CategoryCreateDTO categoryDto)
    {
        CategoryEntity categoryEntity = new CategoryEntity(null, categoryDto.Name, categoryDto.Name);
        int newId = await _categoryRepository.Create(categoryEntity);
        return newId;
        
    }

    public async Task<CategoryOutputDTO> GetById(int id)
    {
        var categoryEntity = await _categoryRepository.GetById(id);
        if (categoryEntity == null)
            throw new UseCaseException("Categoria não encontrada!");

        return _mapper.Map<CategoryOutputDTO>(categoryEntity);
    }

    public async Task<IEnumerable<CategoryOutputDTO>> GetCategories()
    {
        var categoriesEntity = await _categoryRepository.GetAll();
        return _mapper.Map<IEnumerable<CategoryOutputDTO>>(categoriesEntity);
    }

    public async Task<bool> Remove(int id)
    {

        CategoryOutputDTO? categoryOutput = await this.GetById(id);
        var categoryEntity = _mapper.Map<CategoryEntity>(categoryOutput);
        if (categoryEntity == null)
            throw new UseCaseException("Categoria não encontrada!");

        var retorno = await _categoryRepository.Remove(categoryEntity);
        return retorno;

    }

    public async Task<bool> Update(CategoryUpdateDTO categoryUpdateDto)
    {
        await this.GetById(categoryUpdateDto.Id.GetValueOrDefault());
        var categoryEntity = _mapper.Map<CategoryEntity>(categoryUpdateDto);
        if (categoryEntity == null)
            throw new UseCaseException("Categoria não encontrada!");

        return await _categoryRepository.Update(categoryEntity);

    }
}
