package Algorithms;

import java.util.Arrays;

public class QuickSort {
    public static int partition(int[] arr, int l, int r){
        int pivot = arr[(l + r) / 2];
        int leftMove = l;
        int rightMove = r;
        while (leftMove <= rightMove) {
            while (arr[leftMove] < pivot) {
                leftMove++;
            }
            while (arr[rightMove] > pivot) {
                rightMove--;
            }
            if (leftMove >= rightMove) {
                break;
            }
            int temp = arr[leftMove];
            arr[leftMove] = arr[rightMove];
            arr[rightMove] = temp;
            leftMove++;
            rightMove--;
        }
        return rightMove;
    }
    public static void ExecuteSort(int[] arr, int l, int r){
        if (l < r){
            int q = partition(arr, l, r);
            ExecuteSort(arr, l, q);
            ExecuteSort(arr, q + 1, r);
        }
    }
    public static void main(String[] args) {
        int[] arr = new int[10000];
        int[] ar2 = new int[10000];
        for (int i = 0; i < arr.length; i++) {
            arr[i] = (int) (Math.random() * 1000);
            ar2[i] = arr[i];
        }
        ExecuteSort(arr, 0, arr.length - 1);

        Arrays.sort(ar2);
        System.out.println(Arrays.equals(arr, ar2));
    }
}
