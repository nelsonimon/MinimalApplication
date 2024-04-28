using Microsoft.AspNetCore.Mvc;
using MinimalApplication.Application.Api.Shared;
using MinimalApplication.UseCases.Category.DTOs;
using MinimalApplication.UseCases.Category.Interface;
using MinimalApplication.UseCases.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace MinimalApplication.Application.Api.Controllers;

[ApiController]
[Route("v1/[controller]/[action]")]
public class CategoriesController : ApiControllerBase
{
    private readonly ICategoryUC _categoryUC;
    public CategoriesController(ICategoryUC categoryUC)
    {
        _categoryUC = categoryUC;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Obtém a lista de todas as categorias cadastratras.")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoryOutputDTO>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            IEnumerable<CategoryOutputDTO> categories = await _categoryUC.GetCategories();
            if (categories.Count() == 0)
                return ResponseNoContent();

            return ResponseOk(categories);
        }
        catch (Exception ex)
        {
            return ResponseBadRequest(ex.Message);
        }

    }

    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Obtém a categoria pelo seu id.")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CategoryOutputDTO))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            CategoryOutputDTO category = await _categoryUC.GetById(id);
            return ResponseOk(category);
        }
        catch (UseCaseException ex)
        {
            return ResponseNotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return ResponseBadRequest(ex.Message);
        }
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Adiciona uma nova categoria retornando o seu respectivo Id")]
    [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(int))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Add(CategoryCreateDTO category)
    {
        try
        {
            int newId = await _categoryUC.Add(category);
            return ResponseCreated(newId);
        }
        catch (Exception ex) 
        {
            return ResponseBadRequest(ex.Message);
        }
        
    }

    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Atualiza uma nova categoria.")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(bool))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(int id, CategoryUpdateDTO category)
    {
        try
        {
            category.Id = id;
            bool result = await _categoryUC.Update(category);
            return ResponseOk(result);
        }
        catch (UseCaseException ex)
        {
            return ResponseNotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return ResponseBadRequest(ex.Message);
        }


    }

    [HttpDelete]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Remove uma categoria.")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(bool))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            bool result = await _categoryUC.Remove(id);
            return ResponseOk(result);
        }
        catch (UseCaseException ex)
        {
            return ResponseNotFound(ex.Message);
}
        catch (Exception ex)
        {
            return ResponseBadRequest(ex.Message);
        }
    }

}
