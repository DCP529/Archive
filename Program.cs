using ArchivingPresentation;
using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<ArchiveBenchmark>();
Console.WriteLine(summary);