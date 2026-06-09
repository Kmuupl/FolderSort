using System;
using System.IO;
using System.Diagnostics;

namespace FolderSort.Core;
public static class SequentialProcessor
{
    public static long Run(string folderPath)
    {
        string[] files = Directory.GetFiles(folderPath, "*.txt");
        Stopwatch sw = Stopwatch.StartNew();
        foreach (string file in files)
        {
            FileProcessor.ProcessFile(file);
        }

        sw.Stop();
        Console.WriteLine($"Processed {files.Length} files in {sw.ElapsedMilliseconds} ms");
        return sw.ElapsedMilliseconds;
    }
}