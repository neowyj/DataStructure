using System;

namespace MaxHeap
{
    class MaxHeap<T> where T: IComparable<T>
    {
        private T[] elements;
        private int index;

        public MaxHeap(int num)
        {
            elements = new T[num];
        }

        // 判空函数
        public bool isEmpty()
        {
            if (index == 0)
            {
                return true;
            }
            return false;
        }

        private void Swap(int i, int j)
        {
            T temp = elements[i];
            elements[i] = elements[j];
            elements[j] = temp;
        }

        public void Insert(T a)
        {
            if (index == elements.Length)
            {
                throw new IndexOutOfRangeException();
            }
            elements[index] = a;
            index++;
            AdjustUp();
        }

        private void AdjustUp()
        {
            int i = index - 1;
            while (i != 0 && elements[i].CompareTo(elements[(i - 1) / 2]) > 0)
            {
                Swap(i, (i - 1) / 2);
                i = (i - 1) / 2;
            }
        }

        public T Delete()
        {
            if (index == 0)
            {
                throw new IndexOutOfRangeException();
            }
            T result = elements[0];
            elements[0] = elements[index - 1];
            index--;
            AdjustDown();
            return result;
        }

        private void AdjustDown()
        {
            int i = 0;
            while (2 * i + 1 < index)
            {
                int j = 2 * i + 1;
                if (j + 1 < index && elements[j].CompareTo(elements[j + 1]) < 0)
                {
                    j++;
                }
                if (elements[i].CompareTo(elements[j]) > 0)
                {
                    break;
                }
                Swap(i, j);
                i = j;
            }
        }

        public void Print()
        {
            for (int i = 0; i < index; i++)
            {
                Console.WriteLine(elements[i]);
            }
        }
    }

    class Client
    {
        public static void Main1(string[] args)
        {
            MaxHeap<int> maxHeap = new MaxHeap<int>(5);
            maxHeap.Insert(1);
            maxHeap.Insert(2);
            maxHeap.Insert(3);
            maxHeap.Insert(4);
            maxHeap.Insert(5);

            maxHeap.Print();
        }
    }
}