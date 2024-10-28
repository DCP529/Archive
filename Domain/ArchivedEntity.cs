namespace Domain;

public class ArchivedEntity(Guid id, DateTime created, string name) : BaseEntity(id, created)
{
    public string Name { get; private set; } = name;

    public void Update(string newName)
    {
        if (!string.IsNullOrEmpty(newName))
        {
            Name = newName;
        }
    }
}