using Domain;

namespace Services;

public class PartitioningService
{
    private readonly MyDbContext _context;

    public PartitioningService(MyDbContext context)
    {
        _context = context;
    }

    public void MoveToPartition(MyPartitionEntity entity)
    {
        // PostgreSQL автоматически распределяет по партициям
        _context.MyPartitionEntities.Add(entity);
        _context.SaveChanges();
    }
    
    public MyPartitionEntity[] GetByFilter(Func<MyPartitionEntity, bool> filter)
    {
        var myEntities = _context.MyPartitionEntities;

        var entities = myEntities.Where(filter).ToArray();
        return entities;
    }
}