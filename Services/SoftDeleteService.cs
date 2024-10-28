using Domain;

namespace Services;

public class SoftDeleteService
{
    private readonly MyDbContext _context;

    public SoftDeleteService(MyDbContext context)
    {
        _context = context;
    }

    public void SoftDelete(MyEntity entity)
    {
        entity.MarkDelete();
        _context.SaveChanges();
    }
    
    public MyEntity[] GetByFilter(Func<MyEntity, bool> filter)
    {
        var myEntities = _context.MyEntities;

        var entities = myEntities.Where(filter).ToArray();
        return entities;
    }
}