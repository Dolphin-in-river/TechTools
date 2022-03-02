package Algorithms;

import java.util.Arrays;

public class BubbleSort {

    public static void ExecuteSort(int[] arr){
        for (int i = 0; i < arr.length; i++){
            for (int j = i + 1; j < arr.length; j++){
                if (arr[i] > arr[j]){
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
        }
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
