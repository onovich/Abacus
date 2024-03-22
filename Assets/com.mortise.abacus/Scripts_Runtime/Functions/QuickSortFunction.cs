using System;
using System.Collections.Generic;

namespace MortiseFrame.Abacus {

    public class QuickSortFunction {

        #region  Comparer
        internal class DefaultAscendingOrderComparer<T> : Comparer<T> where T : IComparable<T> {
            public override int Compare(T x, T y) {
                return x.CompareTo(y);
            }
        }

        internal class DefaultDescendingOrderComparer<T> : Comparer<T> where T : IComparable<T> {
            public override int Compare(T x, T y) {
                return y.CompareTo(x);
            }
        }
        #endregion

        #region List
        public static void QuickSortListWithComparer<T>(List<T> src, Comparer<T> comparer = null) where T : IComparable<T> {
            comparer ??= new DefaultDescendingOrderComparer<T>();
            QuickSortList(src, 0, src.Count - 1, comparer);
        }

        public static void QuickSortList<T>(List<T> src, bool ascendingOrder = true) where T : IComparable<T> {
            Comparer<T> comparer = ascendingOrder ? new DefaultAscendingOrderComparer<T>() : new DefaultDescendingOrderComparer<T>();
            QuickSortList(src, 0, src.Count - 1, comparer);
        }

        static void QuickSortList<T>(List<T> src, int left, int right, Comparer<T> comparer) where T : IComparable<T> {
            if (left < right) {
                int pivotIndex = PartitionList(src, left, right, comparer);
                QuickSortList(src, left, pivotIndex - 1, comparer);
                QuickSortList(src, pivotIndex + 1, right, comparer);
            }
        }

        static int PartitionList<T>(List<T> arr, int left, int right, Comparer<T> comparer) where T : IComparable<T> {
            T pivot = arr[right];
            int i = left - 1;

            for (int j = left; j < right; j++) {
                if (comparer.Compare(arr[j], pivot) <= 0) {
                    i++;
                    SwapList(arr, i, j);
                }
            }
            SwapList(arr, i + 1, right);
            return i + 1;
        }

        static void SwapList<T>(List<T> src, int i, int j) {
            T temp = src[i];
            src[i] = src[j];
            src[j] = temp;
        }
        #endregion

        #region Array
        public static void QuickSortArrayWithComparer<T>(T[] src, Comparer<T> comparer = null) where T : IComparable<T> {
            comparer ??= new DefaultDescendingOrderComparer<T>();
            QuickSortArray(src, 0, src.Length - 1, comparer);
        }

        public static void QuickSortArray<T>(T[] src, bool ascendingOrder = true) where T : IComparable<T> {
            Comparer<T> comparer = ascendingOrder ? new DefaultAscendingOrderComparer<T>() : new DefaultDescendingOrderComparer<T>();
            QuickSortArray(src, 0, src.Length - 1, comparer);
        }

        static void QuickSortArray<T>(T[] src, int left, int right, Comparer<T> comparer) where T : IComparable<T> {
            if (left < right) {
                int pivotIndex = PartitionArray(src, left, right, comparer);
                QuickSortArray(src, left, pivotIndex - 1, comparer);
                QuickSortArray(src, pivotIndex + 1, right, comparer);
            }
        }

        static int PartitionArray<T>(T[] arr, int left, int right, Comparer<T> comparer) where T : IComparable<T> {
            T pivot = arr[right];
            int i = left - 1;

            for (int j = left; j < right; j++) {
                if (comparer.Compare(arr[j], pivot) <= 0) {
                    i++;
                    SwapArray(arr, i, j);
                }
            }
            SwapArray(arr, i + 1, right);
            return i + 1;
        }

        static void SwapArray<T>(T[] src, int i, int j) {
            T temp = src[i];
            src[i] = src[j];
            src[j] = temp;
        }
        #endregion
    }

}