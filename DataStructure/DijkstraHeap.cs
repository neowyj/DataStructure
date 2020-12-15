using System;
using PriorityQueue;
using System.Diagnostics.CodeAnalysis;

namespace DijkstraHeap
{
    // 边表结点
    class Node: IComparable<Node>, IEquatable<Node>
    {
        // 边表中的顶点
        private int vertex;
        // 顶点表中的顶点到边表顶点对应边上的权值
        private int weight;
        // 边表结点的引用域
        private Node next;

        public int Vertex { get => vertex; set => vertex = value; }
        public int Weight { get => weight; set => weight = value; }
        public Node Next { get => next; set => next = value; }

        public Node(int vertex, int weight)
        {
            this.vertex = vertex;
            this.weight = weight;
        }

        public Node() { }

        public int CompareTo(Node other)
        {
            if (this.weight > other.weight)
            {
                return 1;
            }
            if (this.weight < other.weight)
            {
                return -1;
            }
            return 0;
        }

        public bool Equals(Node other)
        {
            if (this.vertex == other.vertex)
            {
                return true;
            }
            return false;
        }
    }

    // 边表
    class EdgeList
    {
        private Node head;

        public Node Head { get => head; set => head = value; }

        public EdgeList()
        {
            head = null;
        }

        public void Append(int vertex, int weight)
        {
            Node q = new Node(vertex, weight);
            Node p;

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

        public bool isEmpty()
        {
            if (head == null)
            {
                return true;
            }
            return false;
        }
    }

    class Graph
    {
        public EdgeList[] graph;

        int num;

        public Graph(int num)
        {
            this.num = num;
            graph = new EdgeList[num];
            for (int i = 0; i < num; i++)
            {
                graph[i] = new EdgeList();
            }
        }

        public void AddEdge(int v1, int v2, int w)
        {
            graph[v1 - 1].Append(v2 - 1, w);
        }

        public void Dijkstra(int i)
        {
            // 已找到最短路径的顶点数组
            int[] vs = new int[num];
            // 存放顶点v到其他顶点的最短路径
            int[] dists = new int[num];
            // 优先级队列
            PriorityQueue<Node> pq = new PriorityQueue<Node>(num);
            int v;
            int dist;

            Node p = graph[i - 1].Head;
            while (p != null)
            {
                pq.Enqueue(p);
                dists[p.Vertex] = p.Weight;
                p = p.Next;
            }

            while (num > 1)
            {
                Node min = pq.Dequeue();
                v = min.Vertex;
                dist = min.Weight;
                vs[v] = dist;

                Node q = graph[v].Head;
                Node temp = new Node();
                while (q != null)
                {
                    if (dists[q.Vertex] == 0)
                    {
                        pq.Enqueue(q);
                    }
                    if (dist + q.Weight < dists[q.Vertex])
                    {
                        temp.Vertex = q.Vertex;
                        temp.Weight = dist + q.Weight;
                        pq.Modify(temp);
                    }
                    q = q.Next;
                }

                num--;
            }

            // 输出vs中的顶点和到该顶点的最短路径长度
            Console.WriteLine("顶点{0}到其他各顶点的最短路径长度为：", i);
            for (int j = 0; j < num; j++)
            {
                Console.Write("{0}:{1}, ", j + 1, vs[j]);
            }
        }
    }

    class Client
    {
        public static void Main1(string[] args)
        {
            Graph graph = new Graph(5);

            graph.AddEdge(1, 2, 10);
            graph.AddEdge(1, 5, 5);
            graph.AddEdge(2, 3, 1);
            graph.AddEdge(2, 5, 2);
            graph.AddEdge(3, 4, 4);
            graph.AddEdge(4, 1, 7);
            graph.AddEdge(4, 3, 6);
            graph.AddEdge(5, 2, 3);
            graph.AddEdge(5, 3, 9);
            graph.AddEdge(5, 4, 2);

            graph.Dijkstra(1);
        }
    }
}