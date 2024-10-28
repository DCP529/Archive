namespace Domain;

public class MyPartitionEntity(Guid id, DateTime created, string name) : BaseEntity(id, created)
{
    public string Name { get; private set; } = name;
    public bool IsDeleted { get; private set; }
    
    public void Update(string newName)
    {
        if (!string.IsNullOrEmpty(newName))
        {
            Name = newName;
        }
    }
    
    public void MarkDelete()
    {
        IsDeleted = true;
    }
}