using System;

namespace GenericOverload
{
    public class Node<T, V>
    {
        public T Add(T a, V b)
        {
            return a;
        }

        public T Add(V a, T b)
        {
            return b;
        }

        public int Add(int a, int b)
        {
            return a + b;
        }
    }

    class Client
    {
        public static void Main1(string[] args)
        {
            Node<int, int> node = new Node<int, int>();
            // 三个Add函数都可以，但Add(int, int)才是最佳匹配项。
            // 当出现多个重载函数都符合要求时，系统会选择匹配最精准的一个。
            Console.WriteLine(node.Add(1, 2));
            Node<int, string> node1 = new Node<int, string>();
            Console.WriteLine(node1.Add(1, "2"));
        }
    }
}
