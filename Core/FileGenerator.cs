using System;
using System.IO;

public static class FileGenerator
{
    public static void GenerateAll()
    {
        Generate("Data/Small",  120, 500);
        Generate("Data/Medium", 120, 3000);
        Generate("Data/Large",  120, 15000);
    }

    private static void Generate(string folderPath, int fileCount, int numbersPerFile)
    {
        Directory.CreateDirectory(folderPath);
        Random random = new Random();

        for (int i = 1; i <= fileCount; i++)
        {
            string filePath = Path.Combine(folderPath, $"file_{i:D3}.txt");
            if (File.Exists(filePath)) continue;

            using StreamWriter writer = new StreamWriter(filePath);
            for (int j = 0; j < numbersPerFile; j++)
            {
                writer.WriteLine(random.Next(1, 100000));
            }
        }

        Console.WriteLine($"Generated: {folderPath}");
    }
}