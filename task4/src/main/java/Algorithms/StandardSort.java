package Algorithms;

import java.util.Arrays;

public class StandardSort {
    public static void ExecuteSort(int[] arr){
        Arrays.sort(arr);
    }
    public static void main(String[] args) {
        int[] arr = new int[10000];
        int[] ar2 = new int[10000];
        for (int i = 0; i < arr.length; i++) {
            arr[i] = (int) (Math.random() * 1000);
            ar2[i] = arr[i];
        }
        ExecuteSort(arr);

        Arrays.sort(ar2);
        System.out.println(Arrays.equals(arr, ar2));
    }
}
