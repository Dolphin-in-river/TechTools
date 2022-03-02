using System;

namespace task4
{
    public class QuickSort
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

        private static int Partition(int[] arr, int l, int r)
        {
            int pivot = arr[(l + r) / 2];
            int leftMove = l;
            int rightMove = r;
            while (leftMove <= rightMove)
            {
                while (arr[leftMove] < pivot)
                {
                    leftMove++;
                }

                while (arr[rightMove] > pivot)
                {
                    rightMove--;
                }

                if (leftMove >= rightMove)
                {
                    break;
                }

                (arr[leftMove], arr[rightMove]) = (arr[rightMove], arr[leftMove]);
                leftMove++;
                rightMove--;
            }

            return rightMove;
        }

        public static void ExecuteSort(int[] arr, int l, int r)
        {
            if (l < r)
            {
                int q = Partition(arr, l, r);
                ExecuteSort(arr, l, q);
                ExecuteSort(arr, q + 1, r);
            }
        }

        public static void StartExecute(int count)
        {
            var arr = CreateRandomArr(count);
            ExecuteSort(arr, 0, arr.Length - 1);
        }
    }
}