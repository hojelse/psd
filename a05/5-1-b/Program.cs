using System;

static class Program
{
    static void Main(string[] args)
    {
        int[] a1 = new int[] { 3, 5, 12 };
        int[] a2 = new int[] { 2, 3, 4, 7 };
        printArray(merge(a1, a2));
    }

    public static void printArray(int[] arr) {
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write(arr[i] + " ");
        }
        Console.WriteLine();
    }

    public static int[] merge(int[] xs, int[] ys) {
        int[] acc = new int[xs.Length + ys.Length];

        int xsLen = xs.Length;
        int ysLen = ys.Length;

        int i = 0, j = 0, k = 0;
        while (!(j >= xsLen && k >= ysLen))
            acc[i++] = xs.get(j) < ys.get(k) ? xs[j++] : ys[k++];

        return acc;
    }


    public static int get(this int[] arr, int idx) {
        int ret = int.MaxValue;
        try {
            ret = arr[idx];
        } catch (Exception) {}
        return ret;
    }
}
