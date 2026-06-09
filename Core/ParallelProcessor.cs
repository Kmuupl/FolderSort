using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading;


namespace FolderSort.Core;

public static class ParallelProcessor
{
    public static long Run(string folderPath, int numWorkers)
    {
        if (numWorkers <= 0)
            throw new ArgumentException("Number of workers must be greater than 0.", nameof(numWorkers));

        if (!Directory.Exists(folderPath))
            throw new DirectoryNotFoundException($"Folder not found: {folderPath}");

        string[] files = Directory.GetFiles(folderPath, "*.txt");

        ConcurrentQueue<string> queue = new ConcurrentQueue<string>(files);

        Stopwatch sw = Stopwatch.StartNew();

        Thread[] threads = new Thread[numWorkers];
        for (int i = 0; i < numWorkers; i++)
        {
            threads[i] = new Thread(() =>
            {
                while (queue.TryDequeue(out string? file))
                {
                    FileProcessor.ProcessFile(file);
                }
            });
            threads[i].Start();
        }

        foreach (Thread t in threads)
        {
            t.Join();
        }

        sw.Stop();
        Console.WriteLine($"Parallel W={numWorkers} | {folderPath} | Time: {sw.ElapsedMilliseconds} ms");
        return sw.ElapsedMilliseconds;
    }
}