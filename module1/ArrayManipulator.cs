public class ArrayManipulator
{
    public static int[] GenerateRandomArray(int length, int min, int max)
    {
        int[] array = new int[length];
        Random random = new Random();
        for (int i = 0; i < length; i++)
        {
            array[i] = random.Next(min, max);
        }
        return array;
    }

    public static int FindMax(int[] array)
    {
        int max = array[0];
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] > max)
            {
                max = array[i];
            }
        }
        return max;
    }

    public static void Sort(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[i] > array[j])
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
        }
    }

    public static void PrintArray(int[] array)
    {
        Console.Write("| ");
        Console.Write(string.Join(" | ", array));
        Console.Write(" |\n");
    }
}