namespace FolderSort.Core;
public class BenchmarkResult
{
    public int Workers { get; set; }
    public long TimeTaken { get; set; }
    public double Speedup { get; set; }
}