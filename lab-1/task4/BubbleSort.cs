using System;

namespace task4
{
    public class BubbleSort
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
        public static int[] StartExecute(int count)
        {
            var arr = CreateRandomArr(count);
            int temp;
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i] > arr[j])
                    {
                        temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }                   
                }            
            }
            return arr;
        }
    }
}