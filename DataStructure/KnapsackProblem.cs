using System;

namespace KnapsackProblem
{
    class Client
    {
        public static int DP1(int[] w, int[] v, int W)
        {
            // 物品总数
            int n = w.Length;
            // 初始化二维数组f
            int[][] f = new int[n][];
            for (int i = 0; i < n; i++)
            {
                f[i] = new int[W + 1];
            }

            // 计算第一行总价值
            for (int j = 0; j <= W; j++)
            {
                if (j < w[0])
                {
                    f[0][j] = 0;
                }
                else
                {
                    f[0][j] = v[0];
                }
            }

            // 从第二行开始迭代
            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j <= W; j++)
                {
                    if (j < w[i])
                    {
                        f[i][j] = f[i - 1][j];
                    }
                    else
                    {
                        f[i][j] = Math.Max(f[i - 1][j], f[i - 1][j - w[i]] + v[i]);
                    }
                }
            }

            // 返回10kg背包5种物品的最优选择
            return f[n - 1][W];
        }

        public static int DP(int[] w, int[] v, int W)
        {
            // 物品总数
            int n = w.Length;
            // 前一行的结果
            int[] preRow = new int[W + 1];
            // 当前行的结果
            int[] currentRow = new int[W + 1];

            // 计算第一行的结果
            for (int j = 0; j <= W; j++)
            {
                if (j < w[0])
                {
                    preRow[j] = 0;
                }
                else
                {
                    preRow[j] = v[0];
                }
            }

            // 从第二行开始迭代
            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j <= W; j++)
                {
                    if (j < w[i])
                    {
                        currentRow[j] = preRow[j];
                    }
                    else
                    {
                        currentRow[j] = Math.Max(preRow[j], preRow[j - w[i]] + v[i]);
                    }
                }

                // 当前行计算结束后赋值给前一行，开始下一行的迭代
                Array.Copy(currentRow, preRow, W + 1);
            }

            // 返回10kg背包5种物品的最优选择
            return preRow[W];
        }

        public static void Main1(string[] args)
        {
            // 物品重量数组
            int[] w = { 1, 2, 3, 3, 5 };
            // 物品价值数组
            int[] v = { 100, 300, 300, 500, 500 };
            // 10kg5种物品的最大总价值
            int Vmax = DP(w, v, 10);
            Console.WriteLine(Vmax);
        }
    }
}

// 输出结果：1300（与手动模拟的结果一样）