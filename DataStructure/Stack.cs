using System;

namespace Stack
{
    class Stack<T>
    {
        // 初始栈容量
        private int size = 10;
        private T[] contents;
        // 栈顶指针
        private int top;

        // 栈的构造函数
        public Stack()
        {
            contents = new T[size];
            top = -1;
        }

        // 判空
        public bool isEmpty()
        {
            if (top == -1)
            {
                return true;
            }
            return false;
        }

        // 入栈时，栈顶指针top先加1，再添加元素
        public void Push(T o)
        {
            // 栈满时不能入栈
            if (top == size - 1)
            {
                throw new Exception("stack is full.");
            }
            contents[++top] = o;
        }

        // 出栈时，先删除元素，栈顶指针top再减一
        public T Pop()
        {
            // 栈空时不能出栈
            if (top == -1)
            {
                throw new Exception("stack is empty.");
            }
            return contents[top--];
        }

        // 获取栈元素个数
        public int GetSize()
        {
            return top + 1;
        }
    }

    class Client
    {
        public static void Main1(string[] args)
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(1);
            Console.WriteLine(stack.Pop());
        }
    }
}