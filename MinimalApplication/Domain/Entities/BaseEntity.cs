namespace MinimalApplication.Domain.Entities;

public class BaseEntity
{
    public int Id { get; protected set; }
    public DateTime Updated { get; protected set; }
    public DateTime Created { get; protected set; }

}
