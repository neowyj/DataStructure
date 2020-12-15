using System;
using System.Collections.Generic;

namespace Sort
{
    class Client
    {
        // 插入排序
        public static int[] InsertSort(int[] a)
        {
            int len = a.Length;
            int preIndex, current;
            for (int i = 1; i < len; i++)
            {
                preIndex = i - 1;
                current = a[i];
                while (preIndex >= 0 && current < a[preIndex])
                {
                    a[preIndex + 1] = a[preIndex];
                    preIndex--;
                }
                a[preIndex + 1] = current;
            }
            return a;
        }

        // 折半插入排序
        public static int[] BinaryInsertSort(int[] a)
        {
            int len = a.Length;
            int current;
            int low, mid, high;
            for (int i = 1; i < len; i++)
            {
                current = a[i];
                low = 0;
                high = i - 1;
                while (low <= high)
                {
                    mid = low + (high - low) / 2;
                    if (current >= a[mid])
                    {
                        low = mid + 1;
                    }
                    else
                    {
                        high = mid - 1;
                    }
                }
                // 必须从后往前挪动，如果从前往后挪就会覆盖前面的元素。
                for (int j = i - 1; j > high; j--)
                {
                    a[j + 1] = a[j];
                }
                a[high + 1] = current;
            }
            return a;
        }

        // 希尔排序
        public static int[] ShellSort(int[] a)
        {
            int len = a.Length;
            int d = len / 2;
            int current, loc;
            // 每一个增量排一趟，直到最后一个增量等于1
            while (d >= 1)
            {
                // 每一个增量di分割成di个组
                for (int i = 0; i < d; i++)
                {
                    // 对每一组进行直接插入排序
                    for (int j = i + d; j < len; j += d)
                    {
                        current = a[j];
                        // 组内待排元素插入位置loc
                        loc = j;
                        // 如果组内待排元素比前面的元素小，就把前面的元素向后移动d个单位。
                        while (loc - d >= i && current < a[loc - d])
                        {
                            a[loc] = a[loc - d];
                            loc = loc - d;
                        }
                        // 如果待排元素大于或等于组内前面的元素，前面的元素就不动，直接插入当前位置。
                        a[loc] = current;
                    }
                }
                // 每一趟排完后，就缩小增量继续排下一趟
                d = d / 2;
            }
            return a;
        }

        // 冒泡排序
        public static int[] BubbleSort(int[] a)
        {
            int len = a.Length;
            int temp;
            // 每趟冒泡是否发生元素交换的标志
            bool flag;
            // 每一趟冒泡确定一个最小元素的位置，n-1趟冒泡确定n-1个元素的位置，最后一个元素必定是最大的。
            for (int i = 0; i < len - 1; i++)
            {
                flag = false;
                // 从后往前冒泡，每一趟冒泡把最小的元素冒上去
                // 如果序列前面的元素大多比较小，就从后往前冒泡，这样能减少冒泡的趟数。
                // j > i 表示已经通过冒泡确定位置的最小元素不参与比较
                for (int j = len - 1; j > i; j--)
                {
                    if (a[j] < a[j - 1])
                    {
                        temp = a[j - 1];
                        a[j - 1] = a[j];
                        a[j] = temp;
                        flag = true;
                    }
                }

                // 从前往后冒泡，每一趟冒泡把最大的元素沉下去
                // 如果序列前面的元素大多比较大，就从前往后冒泡，这样能减少冒泡的趟数。
                //for (int j = 0; j < len - 1 - i; j++)
                //{
                //    if (a[j] > a[j + 1])
                //    {
                //        temp = a[j];
                //        a[j] = a[j + 1];
                //        a[j + 1] = temp;
                //        flag = true;
                //    }
                //}

                // 如果这趟冒泡没有发生交换，说明所有元素已经排好序了，因为之前的元素就是最终有序序列的位置，从而减少冒泡的趟数
                if (flag == false)
                {
                    Console.WriteLine("只冒了{0}趟", i+1);
                    return a;
                }
            }
            return a;
        }

        // 快速排序
        public static int[] QuickSort(int[] a, int low, int high)
        {
            // low和high分别表示待排序列的第一个元素位置（下界）和最后一个元素位置（上界）
            // 当low = high时，表示该序列中只有一个元素不需要划分。
            // 当low > high时，表示该序列为空，也不需要划分。
            // 因此，只有当low < high时，表示该序列中至少有两个元素需要进行划分。
            if (low < high)
            {
                // 先划分子序列，然后再返回划分后该序列pivot基准元素的位置。
                int pivotIndex = Partition(a, low, high);
                // 对小于pivot基准元素的左边部分继续划分
                QuickSort(a, low, pivotIndex - 1);
                // 对大于pivot基准元素的右边部分继续划分
                QuickSort(a, pivotIndex + 1, high);
            }
            return a;
        }

        // 划分函数
        private static int Partition(int[] a, int low, int high)
        {
            // 默认以该序列的第一个元素为基准进行划分
            int pivot = a[low];
            // 如果low < high就继续寻找，直到low = high就停止寻找，这时的low位置就是新的基准位置。
            while (low < high)
            {
                // 先从该序列的最后一个元素开始，依次寻找小于pivot元素的元素，如果大于或等于pivot元素，就继续向左寻找
                while (low < high && a[high] >= pivot)
                {
                    high--;
                }
                // 如果找到小于pivot元素的元素就退出本次寻找，将该元素移动到最左边
                a[low] = a[high];
                // 然后从该序列的第一个元素开始，依次寻找大于或等于pivot元素的元素，如果小于pivot元素，就继续向右寻找
                while (low < high && a[low] <= pivot)
                {
                    low++;
                }
                // 如果找到大于或等于pivot元素的元素就退出本次寻找，将该元素移动到刚才向左寻找时空出来的high位置上
                a[high] = a[low];
            }
            // 把刚才基准元素的值赋给新的基准位置
            a[low] = pivot;
            return low;
        }

        // 选择排序
        public static int[] SelectSort(int[] a)
        {
            int len = a.Length;
            int min;
            // n-1趟
            for (int i = 0; i < len - 1; i++)
            {
                min = i;
                for (int j = i + 1; j < len; j++)
                {
                    if (a[j] < a[min])
                    {
                        min = j;
                    }
                }
                Swap(a, i, min);
            }
            return a;
        }

        // 数组元素交换函数
        public static void Swap(int[] a, int i, int j)
        {
            int temp;
            temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }

        // 堆排序
        // 由于数组元素从0开始，所以堆的最后一个元素位置len=数组Length-1。
        // 且第i个结点的左孩子为2i+1，右孩子为2i+2，双亲结点(i-1)/2。
        public static int[] HeapSort(int[] a, int len)
        {
            // 构建初始大根堆
            BuildMaxHeap(a, len);

            for (int i = len; i > 0; i--)
            {
                // 删除堆顶元素时，先将堆顶元素与最后一个元素进行交换，这样就会将序列中最大的元素放在序列的最后一个位置。
                // 第二大放在倒数第二，依次类推，直到最后删除的堆顶元素就是最小的，放在第一个位置并完成排序。
                Swap(a, i, 0);
                // 每次交换后，都有一个当前最大元素被放在堆的末尾，即确定当前最大元素的位置。
                // 交换后，堆的性质被破坏，需要从堆的根节点开始向下调整，最后一个元素已经确定位置就不再参与堆的调整。
                SiftDown(a, 0, i - 1);
            }
            return a;
        }

        // 构建初始大根堆
        private static void BuildMaxHeap(int[] a, int len)
        {
            // 从最后一个根节点开始，依次向下调整，直到以根节点的子树全部完成建堆。
            for (int i = (len - 1) / 2; i >= 0; i--)
            {
                SiftDown(a, i, len);
            }
        }

        // 向下调整函数，将根节点的值与左右子树中最大的值比较，如果大于它就直接退出循环，没有调整，其对应子树也不会变化。
        private static void SiftDown(int[] a, int i, int len)
        {
            int temp = a[i];
            int j;
            // 如果左子树大于len，表示左子树不存在，则该结点为叶子结点没有子树，退出循环。
            while (2 * i + 1 <= len)
            {
                j = 2 * i + 1;
                // 如果右子树存在，就找出左右子树中最大的值与根节点的值进行比较
                if (j + 1 <= len && a[j] < a[j + 1])
                {
                    j++;
                }
                // 如果根节点的值大于子树中最大的值，则满足大根堆的性质，直接退出循环。
                // 没有调整，其对应子树也不会变化，所以不用继续循环判断其子树是否需要调整。
                if (temp >= a[j])
                {
                    break;
                }
                // 如果根节点的值小于子树中最大的值，则不满足大根堆的性质，调整根节点的值。
                a[i] = a[j];
                // 结点的值发生变化，继续向下判断下一级子树是否需要调整，直到以该根节点的子树全部完成建堆。
                i = j;
            }
            // 该根节点的子树全部完成建堆后，将最初的根节点值赋给现在。
            a[i] = temp;
        }

        // 归并排序
        public static void MergeSort(int[] a, int low, int high)
        {
            // 只有序列中至少有2个元素时，即low < high时，才进行分解和合并（归并排序）。
            if (high - low >= 5)
            {
                // 从序列的中间位置进行分解
                int mid = low + (high - low) / 2;
                // 对左边的子序列a[low...mid]继续进行分解
                MergeSort(a, low, mid);
                // 对右边的子序列a[mid+1...high]继续进行分解
                MergeSort(a, mid + 1, high);
                // 当两个子序列a[low...mid]和a[mid+1...high]内部有序时，合并这个两个子序列为一个有序序列
                // 数组中的每个元素可以视为只有一个元素的有序子序列
                Merge(a, low, mid, high);
            }
            if (high - low > 0 && high - low < 5)
            {
                InsertSort(a, low, high);
            }
        }

        // 合并两个子序列为一个有序序列
        private static void Merge(int[] a, int low, int mid, int high)
        {
            // 获取两个子序列a[low...mid]和a[mid+1...high]的长度之和
            int len = high - low + 1;
            // 新建一个临时的辅助数组，用于存放合并后的有序序列
            int[] temp = new int[len];
            // 第一个子序列的开始位置
            int i = low;
            // 第二个子序列的开始位置
            int j = mid + 1;
            // 辅助数组temp的开始位置
            int k = 0;
            // 从两个子序列的第一个元素开始比较，选择一个最小的元素依次放在temp中
            while (i <= mid && j <= high)
            {
                if (a[i] <= a[j])
                {
                    // k++不管是等号左边还是右边，都是先赋值后k再加1
                    // 赋值后就指向下一个元素
                    temp[k++] = a[i++];
                }
                else
                {
                    temp[k++] = a[j++];
                }
            }

            // 如果第二个子序列比较完了，而第一个子序列还有元素没比较完，就第一个子序列把剩下的元素全部赋值给temp剩下的空间，
            // 因为两个子序列中小的元素已经移动到temp数组中了，剩下的元素肯定比之前的元素大，所以就直接依次放在temp的后面位置。
            while (i <= mid)
            {
                temp[k++] = a[i++];
            }
            // 和上面同理，但这两个while循环每次合并时只执行一个
            while (j <= high)
            {
                temp[k++] = a[j++];
            }

            // 把合并后的子序列temp再赋值给原先数组a的对应位置，准备下一次合并
            Array.Copy(temp, 0, a, low, len);
        }

        // 直接插入排序函数重载
        public static void InsertSort(int[] a, int low, int high)
        {
            int len = a.Length;
            int preIndex, current;
            for (int i = low + 1; i <= high; i++)
            {
                preIndex = i - 1;
                current = a[i];
                while (preIndex >= 0 && current < a[preIndex])
                {
                    a[preIndex + 1] = a[preIndex];
                    preIndex--;
                }
                a[preIndex + 1] = current;
            }
        }

        // 计数排序
        public static int[] CountSort(int[] a)
        {
            // 将数组长度用变量存储，避免在for循环中频繁获取数组长度降低性能。
            int len = a.Length;
            // 获取数组a的最小值与最大值
            GetMaxMin(a, out int min, out int max);
            // 获取数组a的数值范围的长度
            int k = max - min + 1;
            // 创建一个辅助数组c用于存放a中每个元素出现的次数，c[i]的值为a[i]出现的次数。
            int[] c = new int[k];
            // 创建一个临时数组b用于存储c数组反向输出的元素
            int[] b = new int[len];

            // 遍历a数组获取a[i]出现的次数，并赋值给对应的c[a[i]]
            for (int i = 0; i < len; i++)
            {
                c[a[i]]++;
            }

            // 更新c数组中的次数为元素输出做准备，因此c[i]中值为之前所有的次数之和
            for (int i = 1; i < k; i++)
            {
                c[i] += c[i - 1];
            }

            // 由于必须保证数组元素的相对位置不变，因此必须反向输出，后出现的元素对应的下标必须在b数组的后面。
            // 由于次数是从开始，而数组下标从0开始，所以b数组存放时c[i]-1。
            // 而下面的c[i]--是指c中当前元素输出后，次数减1并指向下一个相同的元素。
            for (int i = len - 1; i >= 0; i--)
            {
                b[c[a[i]] - 1] = a[i];
                c[a[i]]--;
            }

            return b;
        }

        // 优化后的计数排序
        public static int[] CountSort2(int[] a)
        {
            int len = a.Length;
            GetMaxMin(a, out int min, out int max);
            int k = max - min + 1;
            int[] c = new int[k];
            int[] b = new int[len];

            for (int i = 0; i < len; i++)
            {
                c[a[i] - min]++;
            }

            for (int i = 1; i < k; i++)
            {
                c[i] += c[i - 1];
            }

            for (int i = len - 1; i >= 0; i--)
            {
                b[c[a[i] - min] - 1] = a[i];
                c[a[i] - min]--;
            }

            // 还可以直接输出数组c，c数组下标i就是数组a的值，只有出现过的i才是数组a的元素。
            //for (int i = 0, j = 0; i < k; i++)
            //{
            //    while (c[i] > 0)
            //    {
            //        a[j++] = i;
            //        c[i]--;
            //    }
            //}

            return b;
        }

        // 获取数组的最小值和最大值
        private static void GetMaxMin(int[] a, out int min, out int max)
        {
            int len = a.Length;
            min = a[0];
            max = a[0];

            for (int i = 0; i < len; i++)
            {
                if (a[i] < min)
                {
                    min = a[i];
                }
                if (a[i] > max)
                {
                    max = a[i];
                }
            }
        }

        // 桶排序
        public static int[] BucketSort(int[] a, int capacity)
        {
            // 参数capacity为每个桶的容量，即每个桶的元素个数。
            int len = a.Length;
            // 每个桶的索引
            int index = 0;
            // 获取数组a的最小值和最大值
            GetMaxMin(a, out int min, out int max);
            // 根据桶的容量和数组a的数值范围，确定桶的个数。
            int bucketNum = (max - min) / capacity + 1;
            // 创建一个桶数组，每个桶是一个List<int>，用于存放桶中的元素。
            List<int>[] bucketList = new List<int>[bucketNum];

            // 初始化每个桶对应的List<int>
            for (int i = 0; i < bucketNum; i++)
            {
                bucketList[i] = new List<int>();
            }

            // 根据数组a中元素的值，计算元素位于哪个桶，再将该元素放进桶中，这样可以保证桶间有序。
            for (int i = 0; i < len; i++)
            {
                index = (a[i] - min) / capacity;
                bucketList[index].Add(a[i]);
            }

            // 桶间有序，但桶内无序，所以再在桶内进行直接插入排序。
            for (int i = 0, j = 0; i < bucketNum; i++)
            {
                // 桶内直接插入排序
                InsertSort(bucketList[i]);
                // 只要每个桶内有序，再加上桶间有序，则整个序列就有序，所以直接依次将每个桶内的元素赋值给数组a。
                foreach (int k in bucketList[i])
                {
                    a[j++] = k;
                }
            }

            return a;
        }

        // 直接插入排序函数重载
        public static void InsertSort(List<int> a)
        {
            int len = a.Count;
            int preIndex, current;
            for (int i = 1; i < len; i++)
            {
                preIndex = i - 1;
                current = a[i];
                while (preIndex >= 0 && current < a[preIndex])
                {
                    a[preIndex + 1] = a[preIndex];
                    preIndex--;
                }
                a[preIndex + 1] = current;
            }
        }

        // 基数排序
        public static int[] RadixSort(int[] a)
        {
            int len = a.Length;
            // 获取数组中元素的最大位数
            int d = MaxBit(a, len);
            // 用于每轮计数排序反向输出时，存储a元素的临时数组
            int[] b = new int[len];
            // 用于每轮计数排序的辅助数组，即下标从0到9的10个桶，存储每个关键字位数字出现的次数。
            int[] c = new int[10];
            int i, j, k;
            // 基数，即每个关键字位的权重
            int radix = 1;
            // 基数排序需要d趟，从最低位开始依次对每个关键字位进行“分配”和“收集”工作。
            for (i = 0; i < d; i++)
            {
                // 每趟计数排序前，先将辅助数组中每个桶的元素清0
                for (j = 0; j < 10; j++)
                {
                    c[j] = 0;
                }
                // 计算该关键字位上每个数字出现的次数，这称为“分配”操作。
                for (j = 0; j < len; j++)
                {
                    // 没有这个关键字位的用0补上，即对应的k为0
                    k = (a[j] / radix) % 10;
                    c[k]++;
                }
                // 更新辅助数组元素出现的次数，为反向输出做准备
                for (j = 1; j < 10; j++)
                {
                    c[j] += c[j - 1];
                }
                // 与计数排序不同的是，这里反向输出的不是单个关键字位，而是单个关键字位对应的整数，这称为“收集”操作。
                for (j = len - 1; j >= 0; j--)
                {
                    k = (a[j] / radix) % 10;
                    b[c[k] - 1] = a[j];
                    c[k]--;
                }
                // 收集完成，将按该关键字位排序的结果覆盖原始数组，并用于按下一个关键字位排序的起始数组。
                for (j = 0; j < len; j++)
                {
                    a[j] = b[j];
                }
                // 按该关键字位排序完成，更新radix，准备下一趟的基数排序。
                radix *= 10;
            }
            return a;
        }

        // 求数组的最大位数
        private static int MaxBit(int[] a, int len)
        {
            int max = a[0];
            // 最大位数
            int d = 1;
            int p = 10;
            for (int i = 0; i < len; i++)
            {
                if (a[i] > max)
                {
                    max = a[i];
                }
            }
            while (max >= p)
            {
                max /= 10;
                d++;
            }
            return d;
        }

        public static void Main1(string[] args)
        {
            int[] a = { 1, 5, 2, 6, 3, 8, 4, 0, 9, 7 };
            int[] b = { 11, 422, 54, 999, 378 };
            int len = a.Length;
            //InsertSort(a);
            //BinaryInsertSort(a);
            //ShellSort(a);
            //BubbleSort(a);
            //QuickSort(a, 0, len - 1);
            //SelectSort(a);
            //HeapSort(a, len - 1);
            MergeSort(a, 0, len - 1);
            //a = CountSort2(a);
            //BucketSort(a, 3);
            //a = RadixSort(b);
            for (int i = 0; i < len; i++)
            {
                Console.Write(a[i] + " ");
            }
        }
    }
}
