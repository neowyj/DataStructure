using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace DynamicProgramming2
{
    class Client
    {
        int tamp = 1;

        public static int F(int n)
        {
            if (n < 1)
                return 0;
            if (n == 1)
                return 1;
            if (n == 2)
                return 1;
            return F(n - 1) + F(n - 2);
        }

        public static int F2(int n, Dictionary<int, int> dict)
        {
            if (n < 1)
                return 0;
            if (n == 1)
                return 1;
            if (n == 2)
                return 1;
            if (dict.ContainsKey(n))
                return dict[n];
            int temp = F2(n - 1, dict) + F2(n - 2, dict);
            dict.Add(n, temp);
            return temp;
        }

        public static int F3(int n)
        {
            if (n < 1)
                return 0;
            if (n == 1)
                return 1;
            if (n == 2)
                return 1;
            int a = 1;
            int b = 1;
            int temp = 0;
            for (int i = 3; i <= n; i++)
            {
                temp = a + b;
                a = b;
                b = temp;
            }
            return temp;
        }

        public static int Power(int x, int n)
        {
            int r = 1;
            while (n-- > 0)
            {
                r *= x;
            }
            return r;
        }

        public static int FastPower(int x, int n)
        {
            // 整个二进制数处理的结果r
            int r = 1;
            // 二进制数第i位处理的结果x^(2^(i-1))，默认为第一位，即x^(2^(1-1))=x
            int b = x;
            while (n != 0)
            {
                // 如果n为奇数，其二进制数的第一位为1，对应处理的结果就是b，先保存结果到r中
                if (n % 2 != 0)
                {
                    r *= b;
                }
                // 如果n为偶数，其二进制数的第一位为0，不保存当前二进制位处理的结果
                // 每处理一个二进制位就让n/2，即二进制数向右移动一位，下一次处理这个二进制数的第一位实际上就是原始n的第二位
                n /= 2;
                // 二进制位移动后记得迭代b，并为下一个二进制位做准备
                b *= b;
                // 这样不断重复，直到n等于0跳出循环，即全部的二进位都处理完了，返回整个二进制数处理的结果r
            }
            return r;
        }

        public static int FastPowerbit(int x, int n)
        {
            int r = 1;
            int b = x;
            while (n != 0)
            {
                if ((n & 1) != 0)
                {
                    r *= b;
                }
                n >>= 1;
                b *= b;
            }
            return r;
        }

        // 两个矩阵相乘
        public static int[][] MatrixMulti(int[][] a, int[][] b)
        {
            // 两个矩阵相乘必须满足：矩阵a的列数等于矩阵b的行数，否则无法相乘
            if (a[0].Length != b.Length)
            {
                return null;
            }
            // 矩阵a的行数m
            int m = a.Length;
            // 矩阵a的列数n，也是矩阵b的行数n
            int n = a[0].Length;
            // 矩阵b的列数p
            int s = b[0].Length;
            // 矩阵a和矩阵b相乘得到的新矩阵m*s
            int[][] c = new int[m][];
            // 初始化矩阵c的每个行
            for (int i = 0; i < m; i++)
            {
                c[i] = new int[s];
            }
            // 开始计算
            for (int i = 0; i < m; i++) 
            {
                for (int j = 0; j < s; j++)
                {
                    c[i][j] = 0;
                    for (int k = 0; k < n; k++)
                    {
                        c[i][j] += a[i][k] * b[k][j];
                    }
                }
            }
            return c;
        }

        // 矩阵快速幂
        public static int[][] MatrixFastPower(int[][] a, int n)
        {
            // 结果矩阵r默认为单位矩阵
            int[][] r =
            {
                new int[] {1, 0},
                new int[] {0, 1}
            };
            // 矩阵底数b
            int[][] b = a;
            while (n != 0)
            {
                if ((n & 1) != 0)
                {
                    r = MatrixMulti(r, b);
                }
                b = MatrixMulti(b, b);
                n >>= 1;
            }
            return r;
        }

        // 矩阵快速幂求解斐波拉契数列
        public static int F4(int n)
        {
            // 底数矩阵
            int[][] a =
            {
                new int[] {1, 1},
                new int[] {1, 0}
            };
            int[][] f = MatrixFastPower(a, n - 1);
            return f[0][0];
        }

        public static void Main1(string[] args)
        {
            Stopwatch sw1 = new Stopwatch();
            Stopwatch sw2 = new Stopwatch();
            Stopwatch sw3 = new Stopwatch();
            Stopwatch sw4 = new Stopwatch();
            sw1.Start();
            Console.WriteLine(F(50));
            sw1.Stop();

            sw2.Start();
            Dictionary<int, int> dict = new Dictionary<int, int>();
            Console.WriteLine(F2(50, dict));
            sw2.Stop();

            sw3.Start();
            Console.WriteLine(F3(50));
            sw3.Stop();

            sw4.Start();
            Console.WriteLine(F4(50));
            sw4.Stop();

            TimeSpan ts1 = sw1.Elapsed;
            TimeSpan ts2 = sw2.Elapsed;
            TimeSpan ts3 = sw3.Elapsed;
            TimeSpan ts4 = sw4.Elapsed;
            Console.WriteLine("F1:" + ts1.TotalMilliseconds);
            Console.WriteLine("F2:" + ts2.TotalMilliseconds);
            Console.WriteLine("F3:" + ts3.TotalMilliseconds);
            Console.WriteLine("F4:" + ts4.TotalMilliseconds);
            //Console.WriteLine(Power(2, 12));
            //Console.WriteLine(FastPower(2, 12));
            //Console.WriteLine(FastPowerbit(2, 12));
            //int[][] a = {
            //    new int[] {1, 1},
            //    new int[] {1, 0},
            //};
            //Console.WriteLine(MatrixMulti(a, a));
        }
    }

}
