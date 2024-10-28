using Domain;

namespace Services;

public class ArchiveTableService(MyDbContext context)
{
    public void Archive(MyEntity entity)
    {
        var archivedEntity = new ArchivedEntity(entity.Id, entity.Created, entity.Name);
        context.ArchivedEntities.Add(archivedEntity);
        context.MyEntities.Remove(entity);
        context.SaveChanges();
    }
}