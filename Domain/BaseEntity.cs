namespace Domain;

public abstract class BaseEntity(Guid id, DateTime created)
{
    public Guid Id { get; private set; } = id;
    public DateTime Created { get; private set; } = created;
}