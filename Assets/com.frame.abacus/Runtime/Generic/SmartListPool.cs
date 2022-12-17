using System;
using System.Buffers;
using System.Collections.Generic;

namespace MortiseFrame.Abacus {

    public class SmartListPool<T> {

        // 内存池
        static readonly MemoryPool<T> memoryPool = MemoryPool<T>.Shared;

        // 当前内存块
        IMemoryOwner<T> currentBlock;

        // 新内存块，用于双缓冲
        IMemoryOwner<T> newBlock;

        Type type;
        int capacity;
        public int Length { get; private set; }

        public SmartListPool(int capacity) {
            this.capacity = capacity;
            this.type = typeof(T);
            this.currentBlock = memoryPool.Rent(capacity);
            this.newBlock = memoryPool.Rent(capacity);
        }

        public T this[int index] {
            get => currentBlock.Memory.Span[index];
            set => currentBlock.Memory.Span[index] = value;
        }

        public void Add(T value) {
            if (value.GetType() != type) {
                throw new InvalidOperationException("无法添加非法类型的元素");
            }
            EnsureCapacity();
            // 在当前内存块中添加新元素
            currentBlock.Memory.Span[Length++] = value;
        }

        public void RemoveAt(int index) {
            // 移动元素
            for (int i = index; i < Length - 1; i++) {
                currentBlock.Memory.Span[i] = currentBlock.Memory.Span[i + 1];
            }

            // 清除最后一个元素
            currentBlock.Memory.Span[--Length] = default(T);
        }

        public int RemoveAll(Predicate<T> match) {
            int count = 0;
            for (int i = 0; i < Length; i++) {
                if (match(currentBlock.Memory.Span[i])) {
                    RemoveAt(i);
                    count++;
                    i--;
                }
            }
            return count;
        }

        public bool Contains(T value) {
            for (int i = 0; i < Length; i++) {
                if (currentBlock.Memory.Span[i].Equals(value)) {
                    return true;
                }
            }
            return false;
        }

        public void Clear() {
            for (int i = 0; i < Length; i++) {
                if (currentBlock.Memory.Span[i] is object) {
                    // 元素是引用类型，需要释放资源
                    (currentBlock.Memory.Span[i] as IDisposable)?.Dispose();
                }
                currentBlock.Memory.Span[i] = default(T);

            }
            Length = 0;
            capacity = 0;
            memoryPool.Dispose();
            currentBlock.Dispose();
            newBlock.Dispose();
            System.GC.Collect();
        }

        public void Sort(IComparer<T> comparer) {
            // 对列表进行快速排序
            QuickSort(currentBlock.Memory.Span, 0, Length - 1, comparer);
        }

        public void Sort() {
            QuickSort(currentBlock.Memory.Span, 0, Length - 1, null);
        }

        private void QuickSort(Span<T> list, int left, int right, IComparer<T> comparer) {
            if (comparer == null) comparer = Comparer<T>.Default;
            if (left >= right) return;

            int pivotIndex = Partition(list, left, right, comparer);

            QuickSort(list, left, pivotIndex - 1, comparer);
            QuickSort(list, pivotIndex + 1, right, comparer);
        }

        private int Partition(Span<T> list, int left, int right, IComparer<T> comparer) {
            int pivotIndex = (left + right) / 2;
            T pivot = list[pivotIndex];

            Swap(list, pivotIndex, right);

            int storeIndex = left;
            for (int i = left; i < right; i++) {
                if (comparer.Compare(list[i], pivot) < 0) {
                    Swap(list, storeIndex, i);
                    storeIndex++;
                }
            }

            Swap(list, storeIndex, right);
            return storeIndex;
        }
        private void Swap(Span<T> list, int index1, int index2) {
            T temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }

        public void Foreach(Action<T> action) {
            for (int i = 0; i < Length; i++) {
                action(currentBlock.Memory.Span[i]);
            }
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

            // 确保内存块足够大
            EnsureCapacity();

            // 将元素移动到新位置
            for (int i = Length; i > index; i--) {
                currentBlock.Memory.Span[i] = currentBlock.Memory.Span[i - 1];
            }

            // 插入新元素
            currentBlock.Memory.Span[index] = value;

            // 更新列表长度
            Length++;
        }

        private void EnsureCapacity() {
            // 如果当前内存块已满，则使用双缓冲技术
            if (Length == currentBlock.Memory.Length) {
                // 根据实际情况调整扩容幅度
                if (Length < capacity / 2) {
                    capacity *= 2;
                } else if (Length < capacity) {
                    capacity += capacity / 2;
                } else {
                    capacity += capacity / 4;
                }

                // 从内存池中分配新的内存块
                newBlock = memoryPool.Rent(capacity);

                // 将数据复制到新的内存块中
                currentBlock.Memory.Span.CopyTo(newBlock.Memory.Span);

                // 释放旧的内存块
                memoryPool.Dispose();

                // 将新的内存块设为当前内存块
                currentBlock = newBlock;
            }
        }

        public int IndexOf(T item) {
            // 遍历当前内存块，查找指定的元素
            for (int i = 0; i < Length; i++) {
                if (currentBlock.Memory.Span[i].Equals(item)) {
                    // 如果找到了指定的元素，返回索引
                    return i;
                }
            }

            // 如果没有找到指定的元素，返回 -1
            return -1;
        }

    }
}