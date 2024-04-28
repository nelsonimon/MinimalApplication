using System.ComponentModel.DataAnnotations;

namespace MinimalApplication.UseCases.Category.DTOs;

public class CategoryUpdateDTO
{

    public int? Id { get; set; } = null;
   
    [Required(ErrorMessage = "The Name is Required")]
    [MinLength(3)]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required(ErrorMessage = "The Description is Required")]
    [MinLength(5)]
    [MaxLength(100)]
    public string Description { get; set; }
}
