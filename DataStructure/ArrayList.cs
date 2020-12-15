using System;
using System.Collections;

namespace ArrayExample3
{
    class Client
    {
        public static void Main1(string[] args)
        {
            // 声明并初始化
            ArrayList al = new ArrayList();

            // 添加元素
            al.Add(1);
            al.Add("test");

            // 删除元素
            al.Remove(1);

            // 索引取值
            al[0] = "test2";

            // 查找元素
            Console.WriteLine(al.Contains("test2"));

            // 获取数组长度
            Console.WriteLine(al.Count);
        }
    }
}
