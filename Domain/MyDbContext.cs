using Microsoft.EntityFrameworkCore;

namespace Domain;

public class MyDbContext : DbContext
{
    /// <summary>
    /// Модель для мягкого удаления
    /// </summary>
    public DbSet<MyEntity> MyEntities;
    /// <summary>
    /// Модель для кастомной архивации
    /// </summary>
    public DbSet<ArchivedEntity> ArchivedEntities;
    /// <summary>
    /// Модель для партиционирования(секционирования)
    /// </summary>
    public DbSet<MyPartitionEntity> MyPartitionEntities;

    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions options) : base(options)
    {
    }
}