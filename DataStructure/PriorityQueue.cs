using System;

namespace PriorityQueue
{
    class PriorityQueue<T> where T : IComparable<T>, IEquatable<T>
    {
        // 声明一个存放小根堆的数组
        private T[] minHeap;
        // 数组中的游标
        private int index;

        public int Index { get => index; set => index = value; }

        public PriorityQueue(int num)
        {
            minHeap = new T[num];
        }

        // 判空函数
        public bool isEmpty()
        {
            if (Index == 0)
            {
                return true;
            }
            return false;
        }

        // 交换数组中对应位置的元素
        private void Swap(int i, int j)
        {
            T temp = minHeap[i];
            minHeap[i] = minHeap[j];
            minHeap[j] = temp;
        }

        // 元素进队，即往小根堆中插入元素
        public void Enqueue(T a)
        {
            if (Index == minHeap.Length)
            {
                throw new IndexOutOfRangeException();
            }
            // 元素先直接插入小根堆的末尾
            minHeap[Index] = a;
            Index++;
            // 再自底向上调整小根堆
            SiftUp();
        }

        // 元素出队，每次出队时选择优先级最高的元素。
        // 即返回小根堆的根结点，然后将堆中最后一个元素移动到根结点，再自顶向下调整小根堆
        public T Dequeue()
        {
            if (Index == 0)
            {
                throw new IndexOutOfRangeException();
            }
            T result = minHeap[0];
            minHeap[0] = minHeap[Index - 1];
            Index--;
            SiftDown();
            return result;
        }

        // 修改对应元素
        public void Modify(T a)
        {
            for (int i = 0; i < index; i++)
            {
                if (minHeap[i].Equals(a))
                {
                    minHeap[i] = a;
                    SiftDown();
                    return;
                }
            }
        }

        // 只返回队头元素，不删除该元素
        public T Peek()
        {
            if (Index == 0)
            {
                throw new IndexOutOfRangeException();
            }
            return minHeap[0];
        }

        // 自底向上调整小根堆
        private void SiftUp()
        {
            int i = Index - 1;
            // 由于堆是一棵完全二叉树的顺序存储，以第一个元素从0开始为基准，则第i个结点的左孩子为2i+1，右孩子为2i+2。
            // 当两个整数相除时，/符号得到的结果是一个整数，小数部分直接舍去。
            while (i != 0 && minHeap[i].CompareTo(minHeap[(i - 1) / 2]) < 0)
            {
                Swap(i, (i - 1) / 2);
                i = (i - 1) / 2;
            }
        }

        // 自顶向下调整小根堆
        private void SiftDown()
        {
            int i = 0;
            while (2 * i + 1 < Index)
            {
                int j = 2 * i + 1;
                if (j + 1 < Index && minHeap[j].CompareTo(minHeap[j + 1]) > 0)
                {
                    j++;
                }
                if (minHeap[i].CompareTo(minHeap[j]) < 0)
                {
                    break;
                }
                Swap(i, j);
                i = j;
            }
        }

        // 按从上到下，从左到右依次输出小根堆中的元素
        public void Print()
        {
            for (int i = 0; i < Index; i++)
            {
                Console.WriteLine(minHeap[i]);
            }
        }
    }

    class Client
    {
        public static void Main1(string[] args)
        {
            PriorityQueue<int> pq = new PriorityQueue<int>(5);

            pq.Enqueue(2);
            pq.Enqueue(3);
            pq.Enqueue(4);
            pq.Enqueue(1);
            pq.Enqueue(5);

            pq.Print();
        }
    }
}
