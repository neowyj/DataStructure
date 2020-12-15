using System;

namespace ArrayExample1
{
    class Client
    {
        // 增加数组元素
        public static T[] AddElement<T>(T[] oldArray, params T[] elements)
        {
            int oldLen = oldArray.Length;
            int eleLen = elements.Length;
            int newLen = oldLen + eleLen;
            T[] newArray = new T[newLen];

            //for (int i = 0; i < oldLen; i++)
            //{
            //    newArray[i] = oldArray[i];
            //}
            Array.Copy(oldArray, newArray, oldLen);

            //for (int i = 0; i < eleLen; i++)
            //{
            //    newArray[oldLen+i] = elements[i];
            //}
            elements.CopyTo(newArray, oldLen);

            return newArray;
        }

        public static void Main1(string[] args)
        {
            // 声明数组方式一
            //int[] numbers = new int[5];
            // 声明数组方式二
            //int[] numbers2 = new int[5] { 1, 2, 3, 4, 5 };
            // 声明数组方三
            int[] numbers = { 1, 2, 3, 4, 5 };

            // 访问数组元素并修改元素值
            numbers[0] = 1;

            // 获取数组长度
            Console.WriteLine(numbers.Length);

            // 增加数组元素
            numbers = AddElement<int>(numbers, 6, 7, 8);
            foreach (int i in numbers)
            {
                Console.WriteLine(i);
            }
        }
    }
}
