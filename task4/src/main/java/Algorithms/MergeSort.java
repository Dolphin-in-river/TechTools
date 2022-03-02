package Algorithms;

import java.util.Arrays;

public class MergeSort {
    public static void ExecuteSort(int[] arr, int n) {
        if (n == 1) {
            return;
        }
        int mid = n / 2;
        int[] l = new int[mid];
        int[] r = new int[n - mid];

        for (int i = 0; i < mid; i++) {
            l[i] = arr[i];
        }
        for (int i = mid; i < n; i++) {
            r[i - mid] = arr[i];
        }
        ExecuteSort(l, mid);
        ExecuteSort(r, n - mid);

        Sort(arr, l, r, mid, n - mid);
    }
    public static void Sort(int[] a, int[] l, int[] r, int left, int right) {

        int i = 0, j = 0, k = 0;
        while (i < left && j < right) {
            if (l[i] <= r[j]) {
                a[k++] = l[i++];
            }
            else {
                a[k++] = r[j++];
            }
        }
        while (i < left) {
            a[k++] = l[i++];
        }
        while (j < right) {
            a[k++] = r[j++];
        }
    }
    public static void main(String[] args) {
        int[] arr = new int[10000];
        int[] ar2 = new int[10000];
        for (int i = 0; i < arr.length; i++) {
            arr[i] = (int) (Math.random() * 1000);
            ar2[i] = arr[i];
        }
        ExecuteSort(arr, arr.length);

        Arrays.sort(ar2);
        System.out.println(Arrays.equals(arr, ar2));
    }
}
