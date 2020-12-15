using System;

namespace ArrayExample2
{
    class Client
    {
        public static void Main1(string[] args)
        {
            // C#二维数组
            // 初始化方式一
            //int[,] numbers = new int[3, 4];

            // 初始化方式二
            //int[,] numbers = new int[3, 4]
            //{
            //    { 1, 2, 3, 4 },
            //    { 5, 6, 7, 8 },
            //    { 9, 10, 11, 12 }
            //};

            // 初始化方式三
            int[,] numbers =
            {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 10, 11, 12 }
            };
            int num = numbers[1, 2];
            //int[] nums = numbers[1]; 报错

            // C#交错数组
            // 初始化方式一，初始化时不能指定列数
            //int[][] numbers2 = new int[3][];

            // 初始化方式二
            //int[][] numbers2 = new int[3][]
            //{
            //    new int[1],
            //    new int[2],
            //    new int[3]
            //};

            // 不支持直接初始化赋值
            //int[][] numbers2 = {
            //    { 1 },
            //    { 1, 2 },
            //    { 1, 2, 3 }
            //};

            // 初始化方式三
            int[][] numbers2 = {
                new int[1] { 1 },
                new int[2] { 1, 2 },
                new int[3] { 1, 2, 3 }
            };
            // 支持直接访问行
            int[] nums2 = numbers2[1];
        }
    }
}
