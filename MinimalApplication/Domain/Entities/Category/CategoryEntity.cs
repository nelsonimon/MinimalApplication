using MinimalApplication.Domain.Exceptions;

namespace MinimalApplication.Domain.Entities.Category;

public sealed class CategoryEntity : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public CategoryEntity(int? id, string name, string description)
    {
        if (id != null)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value");
            Id = id.GetValueOrDefault();
        }

        ValidateDomain(name, description);
    }

    private void ValidateDomain(string name, string description)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Name required");
        DomainExceptionValidation.When(name.Length < 3, "Name invalid. Minimun 3 characters");

        DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Description required");
        DomainExceptionValidation.When(description.Length < 5, "Description invalid. Minimun 5 characters");

        Name = name;
        Description = description;
    }


    public void Update(int? id, string name, string description)
    {
        ValidateDomain(name, description);

        if (id != null)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value");
            Id = id.GetValueOrDefault();
        }
    }
}
