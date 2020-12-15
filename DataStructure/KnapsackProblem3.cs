using System;

namespace KnapsackProblem3
{
    class Client
    {
        public static int DP3(int[] w, int[] v, int[] n, int W)
        {
            int N = w.Length;
            int[] preRow = new int[W + 1];
            int[] currentRow = new int[W + 1];
            // 当前背包的K种选择策略
            int K = 0;
            // K选一的最大值
            int max = 0;
            int temp = 0;

            // 计算第一行的值
            for (int j = 0; j <= W; j++)
            {
                if (j / w[0] < n[0])
                {
                    preRow[j] = (j / w[0]) * v[0];
                }
                else
                {
                    preRow[j] = n[0] * v[0];
                }
            }

            // 从第二行开始迭代
            for (int i = 1; i < N; i++)
            {
                for (int j = 0; j <= W; j++)
                {
                    
                    if (n[i] > j / w[i])
                    {
                        K = j / w[i];
                    }
                    else
                    {
                        K = n[i];
                    }
                    max = preRow[j];
                    for (int k = 0; k <= K; k++)
                    {
                        temp = preRow[j - k * w[i]] + k * v[i];
                        if (temp > max)
                        {
                            max = temp;
                        }
                    }
                    currentRow[j] = max;
                }

                // 当前行计算结束后赋值给前一行，开始下一行的迭代
                Array.Copy(currentRow, preRow, W + 1);
            }

            // 返回完全背包问题的最优选择
            return preRow[W];
        }

        public static void Main1(string[] args)
        {
            int[] w = { 1, 2, 3, 3, 5 };
            int[] v = { 100, 300, 300, 500, 500 };
            int[] n = { 11, 2, 5, 1, 6 };
            int Vmax = DP3(w, v, n, 10);
            Console.WriteLine(Vmax);
        }
    }
}