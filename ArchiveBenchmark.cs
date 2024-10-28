using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Exporters.Csv;
using Domain;
using Microsoft.EntityFrameworkCore;
using Services;

namespace ArchivingPresentation;

[MemoryDiagnoser]
[CsvExporter(CsvSeparator.Semicolon)] // Экспорт в CSV
[RPlotExporter]
public class ArchiveBenchmark
{
    private MyDbContext _context;
    private SoftDeleteService _softDeleteService;
    private ArchiveTableService _archiveTableService;
    private PartitioningService _partitioningService;

    [GlobalSetup]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseNpgsql("Host=localhost;Database=archiving_db;Username=postgres;Password=yourpassword")
            .Options;

        _context = new MyDbContext(options);
        _softDeleteService = new SoftDeleteService(_context);
        _archiveTableService = new ArchiveTableService(_context);
        _partitioningService = new PartitioningService(_context);

        // Генерация тестовых данных
        if (_context.MyEntities.Any()) return;

        for (int i = 0; i < 100_000; i++)
        {
            _context.MyEntities.Add(new MyEntity(Guid.NewGuid(), DateTime.Now.AddDays(-i), $"Entity {i}"));
        }

        _context.SaveChanges();
    }

    [Benchmark]
    public void SoftDeleteBenchmark()
    {
        var entities = _context.MyEntities.Take(1000).ToList();
        foreach (var entity in entities)
        {
            _softDeleteService.SoftDelete(entity);
        }
    }

    [Benchmark]
    public void ArchiveTableBenchmark()
    {
        var entities = _context.MyEntities.Take(1000).ToList();
        foreach (var entity in entities)
        {
            _archiveTableService.Archive(entity);
        }
    }

    [Benchmark]
    public void PartitioningBenchmark()
    {
        var entities = _context.MyPartitionEntities.Take(1000).ToList();
        foreach (var entity in entities)
        {
            _partitioningService.MoveToPartition(entity);
        }
    }
}