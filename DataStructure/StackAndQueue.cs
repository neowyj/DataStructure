using System;
using System.Collections;

namespace QueueExample
{
    class Client
    {
        public static void Main1(string[] args)
        {
            //// 新建一个栈
            //Stack s = new Stack();
            //// 1进栈
            //s.Push(1);
            //// “ok”进栈（Stack的元素类型是Object，可以为任意类型）
            //s.Push("ok");
            //// Pop()弹出并返回栈顶元素
            //Console.WriteLine(s.Pop());
            //// 栈中的Peek()只返回，不弹出栈顶元素
            //Console.WriteLine(s.Peek());
            //// 获取栈的大小
            //Console.WriteLine(s.Count);
            //// 清空栈
            //s.Clear();

            // 新建一个队列
            Queue q = new Queue();
            // 1进队
            q.Enqueue(1);
            // "ok"进队
            q.Enqueue("ok");
            // Dequeue()移除并返回队首元素
            Console.WriteLine(q.Dequeue());
            // 队列中Peek()只返回，不移除队首元素
            Console.WriteLine(q.Peek());
            // 获取队列的大小
            Console.WriteLine(q.Count);
            // 清空队列
            q.Clear();
        }
    }
}