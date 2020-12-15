using System;
using System.Collections.Generic;

namespace Search
{
    // 分块查找类
    class BlockSearch
    {
        // 索引表
        private int[] index;
        // 索引表长度
        private int len;
        // 索引表对应的块数组
        private List<int>[] block;

        // 初始化索引表
        public BlockSearch(int[] elements, int[] index)
        {
            this.index = index;
            this.len = index.Length;
            int num = elements.Length;
            this.block = new List<int>[len];
            // 初始化每个块
            for (int i = 0; i < len; i++)
            {
                block[i] = new List<int>();
            }
            // 对所有元素进行分块
            for (int i = 0; i < num; i++)
            {
                Insert(elements[i]);
            }
        }

        // 用二分查找对分块元素进行索引定位
        private int BlockIndex(int key)
        {
            int start = 0;
            int end = len - 1;
            int mid = 0;
            while (start <= end)
            {
                mid = start + (end - start) / 2;
                if (key < index[mid])
                {
                    end = mid - 1;
                }
                if (key > index[mid])
                {
                    start = mid + 1;
                }
                if (key == index[mid])
                {
                    return mid;
                }
            }
            return start;
        }

        // 插入元素
        public void Insert(int key)
        {
            int start = BlockIndex(key);
            block[start].Add(key);
        }

        // 删除元素
        public void Delete(int key)
        {
            int start = BlockIndex(key);
            block[start].Remove(key);
        }

        // 具体的分块查找，块内采用顺序查找
        public int Search(int key, out int start)
        {
            start = BlockIndex(key);
            int num = block[start].Count;
            for (int i = 0; i < num; i++)
            {
                if (block[start][i] == key)
                {
                    return i;
                }
            }
            return -1;
        }

        // 输出所有元素
        public void Print()
        {
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < block[i].Count; j++)
                {
                    Console.Write(block[i][j]+" ");
                }
            }
            Console.WriteLine();
        }
    }

    // 斐波拉契查找类
    class FibSearch
    {
        // 斐波拉契数组最大长度
        const int max_size = 20;

        // 构造斐波拉契数组
        private int[] Fib()
        {
            int[] F = new int[max_size];
            F[0] = 1;
            F[1] = 1;
            // 通过递推而不是递归的方式，使构造斐波拉契数组的时间复杂度降低为O(n)
            for (int i = 2; i < max_size; i++)
            {
                F[i] = F[i - 1] + F[i - 2];
            }
            return F;
        }

        public int Search(int[] a, int key)
        {
            int n = a.Length;
            int low = 0;
            int high = n - 1;
            int mid = 0;
            int[] F = Fib();
            int k = 0;
            while (n >= F[k] - 1)  // 10 8-13 
            {
                if (n == F[k] - 1)
                {
                    break;
                }
                k++;  // 5 + 1 = 6
            }

            // 由于数组长度必须为F[k]-1个才能进行黄金分割，因此新建一个临时数组temp用来存放已有的元素，空余的位置用最大值（数组a的最后一个元素）填充。
            int[] temp = new int[F[k]-1];
            Array.Copy(a, temp, n);
            for (int i = n; i < F[k]-1; i++)
            {
                temp[i] = a[n - 1];
            }

            while (low <= high)
            {
                mid = low + F[k - 1] - 1;
                if (key < temp[mid])
                {
                    high = mid - 1;
                    k -= 1;
                }
                else if (key > temp[mid])
                {
                    low = mid + 1;
                    k -= 2;
                }
                else
                {
                    // 如果mid超出了原有数组的范围，则表示该元素为最大值，直接返回原有数组的末尾位置。
                    if (mid >= n)
                    {
                        return n - 1;
                    }
                    return mid;
                }
            }

            return -1;
        }
    }

    class Client
    {
        // 顺序查找
        public static int SeqSearch(int[] a, int key)
        {
            int len = a.Length;
            int i = len;
            if (a[0] == key)
            {
                return 0;
            }
            a[0] = key;
            while (a[i - 1] != key)
            {
                i--;
                if (i == 1)
                {
                    return -1;
                }
            }
            return i;
        }

        // 折半查找-递归实现
        public static int BinarySearchDiGui(int[] a, int key, int low, int high)
        {
            if (key < a[low] || key > a[high] || low > high)
            {
                return -1;
            }

            //int mid = (low + high) / 2;
            int mid = low + (high - low) / 2;
            if (key < a[mid])
            {
                BinarySearchDiGui(a, key, low, mid - 1);
            }
            if (key > a[mid])
            {
                BinarySearchDiGui(a, key, mid + 1, high);
            }
            return mid;
        }

        // 折半查找-非递归实现
        public static int BinarySearch(int[] a, int key)
        {
            int low = 0;
            int high = a.Length - 1;
            int mid = 0;
            if (key < a[low] || key > a[high] || low > high)
            {
                return -1;
            }

            while (low <= high)
            {
                mid = low + (high - low) / 2;
                if (key > a[mid])
                {
                    low = mid + 1;
                }
                if (key < a[mid])
                {
                    high = mid - 1;
                }
                return mid;
            }

            return -1;
        }

        // 插值查找
        public static int BinarySearchValue(int[] a, int key)
        {
            int low = 0;
            int high = a.Length - 1;
            int mid = 0;
            if (key < a[low] || key > a[high] || low > high)
            {
                return -1;
            }

            while (low <= high)
            {
                mid = low + (key - a[low]) * (high - low) / (a[high] - a[low]);
                if (key > a[mid])
                {
                    low = mid + 1;
                }
                if (key < a[mid])
                {
                    high = mid - 1;
                }
                return mid;
            }

            return -1;
        }
            
        public static void Main1(string[] args)
        {
            //// 1、顺序查找
            //int[] a = { 1, 4, 5, 6, 0, 3, 8};
            //int b = SeqSearch(a, 5);
            //Console.WriteLine(b);

            //2、折半查找
            //int[] a = { 10, 20, 30, 40, 50, 60, 70 };
            //// 递归实现
            //int b1 = BinarySearchDiGui(a, 20, 0, a.Length - 1);
            //Console.WriteLine(b1);
            //// 非递归实现
            //int b2 = BinarySearch(a, 20);
            //Console.WriteLine(b2);

            //// 3、插值查找
            //int[] a = { 10, 20, 30, 40, 50, 60, 70 };
            //int b = BinarySearchValue(a, 30);
            //Console.WriteLine(b);

            // 4、斐波拉契查找
            //int[] a = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            //FibSearch fs = new FibSearch();
            //int i = fs.Search(a, 10);
            //Console.WriteLine(i);

            // 5、分块查找
            //int[] elements = { 24, 21, 6, 11, 8, 22, 32, 31, 54, 72, 61, 78, 88, 83 };
            //int[] index = { 24, 54, 78, 88 };
            //BlockSearch bs = new BlockSearch(elements, index);
            //bs.Insert(35);
            //bs.Delete(61);
            //bs.Print();
            //int block;
            //int i = bs.Search(78, out block);
            //if (i == -1)
            //{
            //    Console.WriteLine("Not Found");
            //    return;
            //}
            //Console.WriteLine("该元素位于第{0}块第{1}个位置", block + 1, i + 1);

            // 6、散列查找
        }
    }
}
