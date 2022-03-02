using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace task4
{
    public class Program
    {
        [MemoryDiagnoser]
        public class TheEasiestBenchmark
        {
            [Params(1000, 10000, 100000)]
            public int count;

            [Benchmark(Description = "StandardSort")]
            public void StandardSortTest()
            {
                StandardSort.StartExecute(count);
            }

            [Benchmark(Description = "MergeSort")]
            public void MergeSortTest()
            {
                MergeSort.StartExecute(count);
            }

            [Benchmark(Description = "QuickSort")]
            public void QuickSortTest()
            {
                QuickSort.StartExecute(count);
            }

            [Benchmark(Description = "BubbleSort")]
            public void BubbleSortTest()
            {
                BubbleSort.StartExecute(count);
            }
        }
    }

    public static class MainClass
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Program.TheEasiestBenchmark>();
        }
    }
}