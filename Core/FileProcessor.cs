using System.IO;
namespace FolderSort.Core;
public static class FileProcessor
{
    public static void ProcessFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        int[] numbers = new int[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            numbers[i] = int.Parse(lines[i]);
        }
        BubbleSorter.Sort(numbers);
    }
}