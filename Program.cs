using System;

class Program
{
    static void Main(string[] args)
    {
        int maxProcesses = Environment.ProcessorCount;
        Console.WriteLine($"CPU cores detected: {maxProcesses}");

        // Этап 1 — генерация файлов
        Console.WriteLine("\nGenerating files...");
        FileGenerator.GenerateAll();
        Console.WriteLine("Done!\n");

        // Этап 2 — бенчмарк
        BenchmarkRunner.Run("Data/Small",  maxProcesses);
        BenchmarkRunner.Run("Data/Medium", maxProcesses);
        BenchmarkRunner.Run("Data/Large",  maxProcesses);

        Console.WriteLine("\nAll benchmarks complete!");
        Console.WriteLine("Check Results/ folder for CSV files.");
    }
}