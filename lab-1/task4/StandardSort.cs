using System;

namespace task4
{
    public class StandardSort
    {
        public static int[] CreateRandomArr(int count)
        {
            var arr = new int[count];
            var random = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next(0, Int32.MaxValue);
            }

            return arr;
        }

        public static void StartExecute(int count)
        {
            var arr = CreateRandomArr(count);
            Array.Sort(arr);
        }
    }
}