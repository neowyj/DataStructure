using System;
using System.Collections.Generic;

namespace LinkedListExample
{
    class Client
    {
        public static void Main1(string[] args)
        {
            // 声明并初始化双向链表
            LinkedList<int> numbers = new LinkedList<int>();
            // 声明并初始化节点
            LinkedListNode<int> node1 = new LinkedListNode<int>(1);
            LinkedListNode<int> node2 = new LinkedListNode<int>(2);
            LinkedListNode<int> node3 = new LinkedListNode<int>(3);
            LinkedListNode<int> node4 = new LinkedListNode<int>(4);

            // 添加节点，添加后的最终顺序为1、6、3、2、5、4
            numbers.AddFirst(node1);
            numbers.AddAfter(node1, node2);
            numbers.AddBefore(node2, node3);
            // 可以直接添加对应的值
            numbers.AddAfter(node2, 5);
            numbers.AddBefore(node3, 6);
            numbers.AddLast(node4);

            // 删除节点，直接删除对应的值，而不是该索引位置上的元素。
            numbers.Remove(5);
            numbers.RemoveLast();
            numbers.RemoveFirst();

            // 获取双向链表的长度
            Console.WriteLine(numbers.Count);

            // 输出双向链表的值
            // 从双向链表的第一个节点（不是头结点）开始遍历
            LinkedListNode<int> p = numbers.First;
            while (p != null)
            {
                Console.Write(p.Value);
                p = p.Next;
            }
            Console.WriteLine();

            // 查找元素，直接查找对应的值，而不是该索引位置上的元素。
            Console.WriteLine(numbers.Find(2).Value);
        }
    }
}