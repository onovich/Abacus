using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace MortiseFrame.Abacus {

    public class SmartList<T> {

        Memory<T> data;
        Type type;
        int capacity;
        public int Length { get; private set; }
        public int currentIndex = -1;

        public SmartList(int capacity) {
            this.capacity = capacity;
            this.type = typeof(T);
            this.data = new T[capacity];
        }
        public T this[int index] {
            get => data.Span[index];
            set => data.Span[index] = value;
        }
        public void Add(T value) {
            if (value.GetType() != type) {
                throw new InvalidOperationException("无法添加非法类型的元素");
            }

            if (Length == data.Length) {
                // 根据实际情况调整扩容幅度
                if (Length < capacity / 2) {
                    capacity *= 2;
                } else if (Length < capacity) {
                    capacity += capacity / 2;
                } else {
                    capacity += capacity / 4;
                }

                // 扩展数组
                var newData = new T[capacity];
                data.Span.CopyTo(newData);
                data = newData;

            }
        }
        public void RemoveAt(int index) {
            // 移动元素
            for (int i = index; i < Length - 1; i++) {
                data.Span[i] = data.Span[i + 1];
            }

            // 清除最后一个元素
            data.Span[--Length] = default(T);
        }
        // 清空List<T>
        public void Clear() {
            for (int i = 0; i < Length; i++) {
                if (data.Span[i] is object) {
                    // 元素是引用类型，需要释放资源
                    (data.Span[i] as IDisposable)?.Dispose();
                }
                data.Span[i] = default(T);
            }
            Length = 0;
            capacity = 0;
            data = null;
            currentIndex = -1;
            System.GC.Collect();
        }
        public bool Contains(T value) {
            for (int i = 0; i < Length; i++) {
                if (data.Span[i].Equals(value)) {
                    return true;
                }
            }
            return false;
        }
        public int RemoveAll(Predicate<T> match) {
            int count = 0;
            for (int i = 0; i < Length; i++) {
                if (match(data.Span[i])) {
                    RemoveAt(i);
                    count++;
                }
            }
            return count;
        }

        public void Foreach(Action<T> action) {
            for (int i = 0; i < Length; i++) {
                action(data.Span[i]);
            }
        }

        public T Current => data.Span[currentIndex];

        public void Reset() {
            currentIndex = -1;
        }

        public bool MoveNext() {
            // 如果枚举器的当前位置已经到达列表末尾，则返回false
            if (currentIndex == data.Length - 1) {
                return false;
            }

            // 将枚举器的当前位置向后移动一位
            currentIndex++;

            // 返回true，表示枚举器已经移动到了下一个位置
            return true;
        }

        public void Sort() {
            QuickSort(data.Span, 0, Length - 1, null);
        }
        public void Sort(IComparer<T> comparer) {
            QuickSort(data.Span, 0, Length - 1, comparer);
        }

        // 快速排序
        public void QuickSort(Span<T> list, int left, int right, IComparer<T> comparer) {
            if (left >= right) return;

            int pivotIndex = Partition(list, left, right, comparer);

            QuickSort(list, left, pivotIndex - 1, comparer);
            QuickSort(list, pivotIndex + 1, right, comparer);
        }

        private int Partition(Span<T> list, int left, int right, IComparer<T> comparer) {
            T pivot = list[(left + right) / 2];
            int i = left - 1;
            int j = right + 1;
            while (true) {
                do {
                    i++;
                } while (comparer.Compare(list[i], pivot) < 0);
                do {
                    j--;
                } while (comparer.Compare(list[j], pivot) > 0);
                if (i >= j) {
                    return j;
                }
                Swap(ref list[i], ref list[j]);
            }
        }

        private void Swap(ref T x, ref T y) {
            T temp = x;
            x = y;
            y = temp;
        }

        public int IndexOf(T item) {
            // 遍历当前内存块，查找指定的元素
            for (int i = 0; i < Length; i++) {
                if (data.Span[i].Equals(item)) {
                    // 如果找到了指定的元素，返回索引
                    return i;
                }
            }

            // 如果没有找到指定的元素，返回 -1
            return -1;
        }

        public void Insert(int index, T value) {
            // 检查索引是否有效
            if (index < 0 || index > Length) {
                throw new IndexOutOfRangeException();
            }

            // 检查类型是否匹配
            if (value.GetType() != type) {
                throw new InvalidOperationException("无法添加非法类型的元素");
            }

            if (Length == data.Length) {
                // 根据实际情况调整扩容幅度
                if (Length < capacity / 2) {
                    capacity *= 2;
                } else if (Length < capacity) {
                    capacity += capacity / 2;
                } else {
                    capacity += capacity / 4;
                }

                // 扩展数组
                var newData = new T[capacity];
                data.Span.CopyTo(newData);
                data = newData;

            }

            // 将元素移动到新位置
            for (int i = Length; i > index; i--) {
                data.Span[i] = data.Span[i - 1];
            }

            // 插入新元素
            data.Span[index] = value;

            // 更新列表长度
            Length++;
        }

    }
}