namespace FolderSort.Core;

public static class BubbleSorter
{
    public static void Sort(int[] array)
    {
        if (array == null) throw new ArgumentNullException(nameof(array));
        int len = array.Length;
        for (int i = 1; i < len; i++)
        {
            for (int j = 0; j < len - i; j++)
            {
                if (array[j] > array[j + 1])
                {
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }
}