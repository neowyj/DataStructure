using System;

namespace CircleQueue
{
    class CircleQueue<T>
    {
        // 循环队列初始容量
        private int size = 10;
        private T[] contents;
        // 队头指针
        private int front;
        // 队尾指针
        private int rear;

        // 构造函数
        public CircleQueue()
        {
            contents = new T[size];
            // 初始化时队头队尾指针都指向0
            front = rear = 0;
        }

        // 判空
        public bool IsEmpty()
        {
            if (rear == front)
            {
                return true;
            }
            return false;
        }

        // 入队，从队尾入，先赋值再让rear加1
        public void EnQueue(T o)
        {
            if ((rear + 1) % size == front)
            {
                throw new Exception("CircleQueue is full.");
            }
            contents[rear] = o;
            rear = (rear + 1) % size;
        }

        // 出队，从队头出，先取值再让front加1
        public T DeQueue()
        {
            if (rear == front)
            {
                throw new Exception("CircleQueue is empty.");
            }
            T temp = contents[front];
            front = (front + 1) % size;
            return temp;
        }
    }

    class Client
    {
        public static void Main1(string[] args)
        {
            CircleQueue<int> cq = new CircleQueue<int>();
            cq.EnQueue(1);
            cq.EnQueue(2);
            Console.WriteLine(cq.DeQueue());
            Console.WriteLine(cq.DeQueue());
        }
    }
}