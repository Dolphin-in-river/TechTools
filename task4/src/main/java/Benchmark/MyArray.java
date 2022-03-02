package Benchmark;

public class MyArray {
    private static int[] arr;
    private boolean create = false;

    public MyArray() {
    }

    public int[] GetRandomArr(int k) {
        if (create) return arr;
        arr = new int[k];
        for (int i = 0; i < arr.length; i++) {
            arr[i] = (int) (Math.random() * 1000);
        }
        create = true;
        return arr;
    }
}
