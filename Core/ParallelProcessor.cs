using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;

public static class ParallelProcessor
{
    public static long Run(string folderPath, int numThreads)
    {
        string[] files = Directory.GetFiles(folderPath, "*.txt");
        List<string[]> chunks = SplitIntoChunks(files, numThreads);
        Stopwatch sw = Stopwatch.StartNew();
        Thread[] threads = new Thread[chunks.Count];
        for (int i = 0; i < chunks.Count; i++)
        {
            string[] chunk = chunks[i];
            threads[i] = new Thread(() =>
            {
                foreach (string file in chunk)
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
        Console.WriteLine($"Processed {files.Length} files in {sw.ElapsedMilliseconds} ms");
        return sw.ElapsedMilliseconds;
    }
    private static List<string[]> SplitIntoChunks(string[] files, int numChunks)
    {
        List<string[]> chunks = new List<string[]>();
        int chunkSize = files.Length / numChunks;
        for (int i = 0; i < numChunks; i++)
        {
            int start = i * chunkSize;
            int end = (i == numChunks - 1) ? files.Length : start + chunkSize;
            chunks.Add(files[start..end]);
        }
        return chunks;
    }
}