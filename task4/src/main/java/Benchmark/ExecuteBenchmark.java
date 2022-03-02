package Benchmark;
import Algorithms.BubbleSort;
import Algorithms.MergeSort;
import Algorithms.QuickSort;
import Algorithms.StandardSort;
import org.openjdk.jmh.annotations.*;
import java.util.concurrent.TimeUnit;

public class ExecuteBenchmark {
    private static final MyArray myArray = new MyArray();
    @State(Scope.Benchmark)
    public static class MyBenchmarkState {

        @Param({ "1000", "10000", "100000" })
        public int value;
    }

    @Benchmark
    @OutputTimeUnit(TimeUnit.NANOSECONDS)
    @BenchmarkMode(Mode.AverageTime)
    @Warmup(iterations = 0)
    @Measurement(iterations = 1)
    @Fork(value = 1, warmups = 0)
    public int BubbleSort(MyBenchmarkState state) {
        int[] array = myArray.GetRandomArr(state.value);
        BubbleSort.ExecuteSort(array);
        return array.length;
    }

    @Benchmark
    @OutputTimeUnit(TimeUnit.NANOSECONDS)
    @BenchmarkMode(Mode.AverageTime)
    @Warmup(iterations = 0)
    @Measurement(iterations = 1)
    @Fork(value = 1, warmups = 0)
    public int MergeSort(MyBenchmarkState state) {
        int[] array = myArray.GetRandomArr(state.value);
        MergeSort.ExecuteSort(array, array.length);
        return array.length;
    }

    @Benchmark
    @OutputTimeUnit(TimeUnit.NANOSECONDS)
    @BenchmarkMode(Mode.AverageTime)
    @Warmup(iterations = 0)
    @Measurement(iterations = 1)
    @Fork(value = 1, warmups = 0)
    public int StandardSort(MyBenchmarkState state) {
        int[] array = myArray.GetRandomArr(state.value);
        StandardSort.ExecuteSort(array);
        return array.length;
    }
    @Benchmark
    @OutputTimeUnit(TimeUnit.NANOSECONDS)
    @BenchmarkMode(Mode.AverageTime)
    @Warmup(iterations = 0)
    @Measurement(iterations = 1)
    @Fork(value = 1, warmups = 0)
    public int QuickSort(MyBenchmarkState state) {
        int[] array = myArray.GetRandomArr(state.value);
        QuickSort.ExecuteSort(array, 0, array.length - 1);
        return array.length;
    }
}
