using System;
using System.Collections.Generic;

namespace Graph
{
    class Graph
    {
        // 存储边的信息的邻接矩阵
        public int[,] edges;
        // 用于标记顶点是否被访问的辅助数组
        bool[] visited;

        // 图的构造函数，参数为顶点数
        public Graph(int num)
        {
            // 边的默认值全为0
            edges = new int[num, num];
            // 辅助数组默认值全为false
            visited = new bool[num];
        }

        // 添加边
        public void AddEdge(int v1, int v2)
        {
            // 图中元素从1开始，而数组从0开始，因此需要减1
            edges[v1 - 1, v2 - 1] = 1;
            edges[v2 - 1, v1 - 1] = 1;
        }

        // 广度优先搜索遍历
        public void BFS()
        {
            // 初始化一个队列
            Queue<int> q = new Queue<int>();
            // 临时顶点
            int v, w;
            // 第一个顶点入队
            q.Enqueue(0);
            // 入队后将标志置为true
            visited[0] = true;
            // 当队列非空时，已访问的顶点开始出队
            while (q.Count > 0)
            {
                v = q.Dequeue();
                // 输出顶点序号
                Console.WriteLine(v + 1);
                // 获取与该顶点邻接但未被访问的顶点
                w = GetUnVisitedVertex(v);
                while (w != 0)
                {
                    q.Enqueue(w);
                    visited[w] = true;
                    // 继续获取与该顶点邻接但未被访问的顶点，直到这些顶点全部入队
                    w = GetUnVisitedVertex(v);
                }
            }
        }

        // 获取与该顶点邻接但未被访问的顶点
        public int GetUnVisitedVertex(int v)
        {
            int num = visited.Length;
            for (int i = 0; i < num; i++)
            {
                // 如果顶点i与顶点v之间有边（即两顶点相连）且未被访问时，返回顶点i
                if (edges[v, i] == 1 && visited[i] == false)
                {
                    return i;
                }
            }
            return 0;
        }
    }

    class Client
    {
        public static void Main1(string[] args)
        {
            Graph graph = new Graph(5);
            // 添加各顶点的邻接边
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 5);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 2);
            graph.AddEdge(4, 3);
            graph.AddEdge(5, 2);
            graph.AddEdge(5, 4);
            // 广度优先遍历
            graph.BFS();
        }
    }
}