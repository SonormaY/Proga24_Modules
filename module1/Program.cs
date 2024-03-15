Console.Clear();
Console.WriteLine("Initial array:");
var array = ArrayManipulator.GenerateRandomArray(10, 0, 100);
ArrayManipulator.PrintArray(array);
Console.WriteLine($"Max: {ArrayManipulator.FindMax(array)}");
ArrayManipulator.Sort(array);
Console.WriteLine("Sorted array:");
ArrayManipulator.PrintArray(array);