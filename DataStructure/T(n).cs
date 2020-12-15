using System;

namespace Tn
{
    class Client
    {
        public static void Main1(string[] args)
        {
            // O(1)
            //int i = 1;
            //int j = 1;
            //i = i + j;
            //Console.WriteLine(i);

            // O(logn)
            //int i = 1;
            //int n = 1024;
            //while (i < n)
            //{
            //    i = i * 2;
            //}
            //Console.WriteLine(i);

            // O(n)
            //int j = 1;
            //int n = 1024;
            //for (int i = 0; i < n; i++)
            //{
            //    j += i;
            //}
            //Console.WriteLine(j);

            // S(n) = O(n)
            int n = 1024;
            int[] nums = new int[n];
            for (int i = 0; i < n; i++)
            {
                nums[i] = i;
            }
        }
    }
}
