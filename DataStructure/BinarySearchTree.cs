using System;
using System.Collections.Generic;

namespace BinarySearchTree
{
    public class BSTree<T> where T: IComparable<T>
    {
        private T data;
        private BSTree<T> lchild;
        private BSTree<T> rchild;
        
        public BSTree(T data)
        {
            this.data = data;
            this.lchild = null;
            this.rchild = null;
        }

        // 先序遍历NLR
        public void PreOrder()
        {
            Console.Write(this.data+" ");
            if (this.lchild != null)
            {
                this.lchild.PreOrder();
            }
            if (this.rchild != null)
            {
                this.rchild.PreOrder();
            }
        }

        // 非递归先序遍历
        public void PreOrder2()
        {
            Stack<BSTree<T>> stack = new Stack<BSTree<T>>();
            // 初始化current为当前根结点
            BSTree<T> current = this;

            // current != null是为了保证先将根结点和左孩子结点入栈
            // stack.Count != 0是为了保证依次出栈根结点和左孩子结点，并寻找它们的右兄弟
            while (current != null || stack.Count != 0)
            {
                // 先输出根结点和根节点的左孩子结点
                while (current != null)
                {
                    Console.Write(current.data + " ");
                    stack.Push(current);
                    current = current.lchild;
                }
                // 依次寻找左孩子结点的右兄弟
                current = stack.Pop();
                current = current.rchild;
            }
        }

        // 中序遍历LNR
        public void InOrder()
        {
            if (this.lchild != null)
            {
                this.lchild.InOrder();
            }
            Console.Write(this.data + " ");
            if (this.rchild != null)
            {
                this.rchild.InOrder();
            }
        }

        // 非递归中序遍历
        public void InOrder2()
        {
            Stack<BSTree<T>> stack = new Stack<BSTree<T>>();
            // 初始化current为当前根结点
            BSTree<T> current = this;

            // 和非递归先序遍历类似，都是先将根结点和根节点的左孩子结点入栈，只不过输出时左孩子先输出。
            while (current != null || stack.Count != 0)
            {
                // 先将根结点和根节点的左孩子结点入栈
                while (current != null)
                {
                    stack.Push(current);
                    current = current.lchild;
                }

                // 出栈输出时先输出左孩子再输出左孩子的父结点
                current = stack.Pop();
                Console.Write(current.data + " ");
                // 寻找右孩子结点
                current = current.rchild;
            }
        }

        // 后序遍历LRN
        public void PostOrder()
        {
            if (this.lchild != null)
            {
                this.lchild.PostOrder();
            }
            if (this.rchild != null)
            {
                this.rchild.PostOrder();
            }
            Console.Write(this.data + " ");
        }

        // 非递归后序遍历
        public void PostOrder2()
        {
            Stack<BSTree<T>> stack = new Stack<BSTree<T>>();
            List<BSTree<T>> list = new List<BSTree<T>>();
            // 初始化current为当前根结点
            BSTree<T> current = this;

            // 和先序遍历类似，但先入栈的是NR然后再入栈L，即得到的是NRL，因此最后输出时还要逆序输出才能得到LRN。
            while (current != null || stack.Count != 0)
            {
                // 先将根结点和根节点的右孩子结点入栈
                while (current != null)
                {
                    list.Add(current);
                    stack.Push(current);
                    current = current.rchild;
                }

                // 依次出栈并寻找它们的左孩子结点
                current = stack.Pop();
                current = current.lchild;
            }

            // 逆序输出LRN
            list.Reverse();
            foreach (BSTree<T> b in list)
            {
                Console.Write(b.data+" ");
            }
        }

        // 层次遍历
        public void LevelOrder()
        {
            Queue<BSTree<T>> queue = new Queue<BSTree<T>>();
            BSTree<T> p = null;
            queue.Enqueue(this);
            while (queue.Count != 0)
            {
                p = queue.Dequeue();
                Console.Write(p.data + " ");
                if (p.lchild != null)
                {
                    queue.Enqueue(p.lchild);
                }
                if (p.rchild != null)
                {
                    queue.Enqueue(p.rchild);
                }
            }
        }

        // 增加结点
        public void Insert(T data)
        {
            if (this.data.CompareTo(data) >= 0)
            {
                if (this.lchild == null)
                {
                    this.lchild = new BSTree<T>(data);
                    return;
                }
                this.lchild.Insert(data);
            }
            else
            {
                if (this.rchild == null)
                {
                    this.rchild = new BSTree<T>(data);
                    return;
                }
                this.rchild.Insert(data);
            }
        }

        // 删除结点
        public void Delete(T data)
        {
            if (this.data.CompareTo(data) == 0)
            {
                this.data = default(T);
            }
            else if (this.data.CompareTo(data) > 0)
            {
                this.lchild.Delete(data);
            }
            else
            {
                this.rchild.Delete(data);
            }
        }
    }

    class Client
    {
        public static void Main1(string[] args)
        {
            BSTree<int> BSTree = new BSTree<int>(45);
            BSTree.Insert(24);
            BSTree.Insert(55);
            BSTree.Insert(12);
            BSTree.Insert(37);
            BSTree.Insert(53);
            BSTree.Insert(60);
            BSTree.Insert(28);
            BSTree.Insert(40);
            BSTree.Insert(70);
            //BSTree.Delete(40);

            Console.Write("先序序列:");
            BSTree.PreOrder();
            Console.WriteLine();

            Console.Write("非递归先序序列:");
            BSTree.PreOrder2();
            Console.WriteLine();

            Console.Write("中序序列:");
            BSTree.InOrder();
            Console.WriteLine();

            Console.Write("非递归中序序列:");
            BSTree.InOrder2();
            Console.WriteLine();

            Console.Write("后序序列:");
            BSTree.PostOrder();
            Console.WriteLine();

            Console.Write("非递归后序序列:");
            BSTree.PostOrder2();
            Console.WriteLine();

            Console.Write("层次序列:");
            BSTree.LevelOrder();
            Console.WriteLine();
        }
    }
}