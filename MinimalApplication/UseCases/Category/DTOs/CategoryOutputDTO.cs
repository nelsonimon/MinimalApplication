using System.ComponentModel.DataAnnotations;

namespace MinimalApplication.UseCases.Category.DTOs;

public class CategoryOutputDTO
{
    [Required(ErrorMessage = "The Id is Required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "The Name is Required")]
    [MinLength(3)]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required(ErrorMessage = "The Description is Required")]
    [MinLength(5)]
    [MaxLength(100)]
    public string Description { get; set; }

    [Required(ErrorMessage = "The Date Created is Required")]
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}
