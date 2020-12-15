using System;

namespace SingleLinkedList
{
    // 定义一个链表节点类型
    public class Node<T>
    {
        private T data; // 数据域，存储当前节点的数据
        private Node<T> next; // 引用域，存储下一个节点的内存地址

        public T Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }

        public Node<T> Next
        {
            get
            {
                return next;
            }

            set
            {
                next = value;
            }
        }

        public Node(T data, Node<T> next)
        {
            this.data = data;
            this.next = next;
        }

        public Node(Node<T> next)
        {
            this.next = next;
        }

        public Node(T data)
        {
            this.data = data;
        }

        public Node()
        {
            data = default(T);
            next = null;
        }
    }

    public class SingleLinkedList<T>
    {
        // 头指针head
        private Node<T> head;

        public Node<T> Head
        {
            get
            {
                return head;
            }
            set
            {
                head = value;
            }
        }

        // 构造函数初始化一个空表
        public SingleLinkedList()
        {
            head = null;
        }

        public int GetLength()
        {
            // 获取链表的头指针
            Node<T> p = head;
            int len = 0;
            while (p != null)
            {
                len++;
                p = p.Next;
            }
            return len;
        }


        public void Clear()
        {
            // 头引用head为空，其余节点对象依然会存储在内存中，不会立刻销毁。
            // 但当其他节点对象不再使用时，CLR中的GC会回收并销毁这些节点对象。
            head = null;
        }

        public bool isEmpty()
        {
            if (head == null)
            {
                return true;
            }
            return false;
        }

        public void Append(T item)
        {
            Node<T> q = new Node<T>(item);
            Node<T> p = new Node<T>();

            if (head == null)
            {
                head = q;
            }
            else
            {
                p = head;
                while (p.Next != null)
                {
                    p = p.Next;
                }
                p.Next = q;
            }
        }

        // 在单链表某个元素位置之后添加元素
        public void InsertPost(T item, int i)
        {
            Node<T> p = head;
            Node<T> q = new Node<T>(item);
            int j = 1;

            if (isEmpty() || i < 1)
            {
                Console.WriteLine("List is empty or index is error");
                return;
            }

            while (p.Next != null && j < i)
            {
                p = p.Next;
                j++;
            }
            if (j == i)
            {
                q.Next = p.Next;
                p.Next = q;
            }
            else
            {
                Console.WriteLine("Index is out of range.");
            }
        }

        // 删除单链表某个位置的元素
        public void Delete(int i)
        {
            Node<T> p = head;
            int j = 0;

            if (head == null)
            {
                Console.WriteLine("List is empty.");
                return;
            }

            while (p.Next != null && j < i-1)
            {
                p = p.Next;
                j++;
            }
            if (j == i-1)
            {
                p.Next = p.Next.Next;
            }
            else
            {
                Console.WriteLine("Index is out of range.");
            }
        }

        // 获取某个元素
        public T GetElement(int i)
        {
            Node<T> p = head;
            int j = 0;

            if (head == null)
            {
                Console.WriteLine("List is empty.");
            }

            while(p.Next != null && j < i)
            {
                p = p.Next;
                j++;
            }
            if (j == i)
            {
                return p.Data;
            }
            else
            {
                Console.WriteLine("Index is out of range.");
                return default(T);
            }
        }
    }


    class Client
    {
        public static void Main1(string[] args)
        {
            SingleLinkedList<int> sgl = new SingleLinkedList<int>();
            sgl.Append(1);
            sgl.Append(2);
            sgl.InsertPost(2, 1);
            sgl.Delete(2);
            Console.WriteLine(sgl.GetLength());
            Console.WriteLine(sgl.isEmpty());
            Console.WriteLine(sgl.GetElement(1));
        }
    }
}
