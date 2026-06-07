using System;
using System.Collections.Generic;
using System.IO;

public static class BenchmarkRunner
{
    public static void Run(string folderPath, int maxProcesses)
    {
        Console.WriteLine("Running benchmarks...");
        List<BenchmarkResult> results = new List<BenchmarkResult>();
        long t1 = SequentialProcessor.Run(folderPath);
        results.Add(new BenchmarkResult { Processes = 1, TimeTaken = t1, Speedup = 1.0 });
        for (int p = 2; p <= maxProcesses; p++)
        {
            long tp = ParallelProcessor.Run(folderPath, p);
            double speedup = (double)t1 / tp;
            results.Add(new BenchmarkResult { Processes = p, TimeTaken = tp, Speedup = speedup });  
        }
        SaveToCSV(folderPath, results);
    }

    private static void SaveToCSV(string folderPath, List<BenchmarkResult> results)
    {
        string datasetName = Path.GetFileName(folderPath).ToLower();
        string csvPath = Path.Combine("Results", $"{datasetName}_benchmark.csv");
        Directory.CreateDirectory("Results");
        using StreamWriter writer = new StreamWriter(csvPath);
        writer.WriteLine("Processes;TimeTaken;Speedup");
        foreach (BenchmarkResult result in results)
        {
            writer.WriteLine($"{result.Processes};{result.TimeTaken};{result.Speedup:F2}");
        }
        Console.WriteLine($"Saved benchmark results to {csvPath}");
    }
}