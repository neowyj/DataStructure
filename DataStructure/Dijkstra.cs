using System;
using System.Collections.Generic;

namespace Dijkstra
{
    class Graph
    {
        // 该图对应的邻接矩阵
        public int[,] edges;
        // 顶点数
        int num;

        // 带权图的构造函数，所有权值默认为正无穷大
        public Graph(int num)
        {
            this.num = num;
            edges = new int[num, num];
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    edges[i, j] = int.MaxValue;
                }
            }
        }

        // 添加边
        public void AddEdge(int v1, int v2, int weight)
        {
            // 因为图的元素从1开始，数组下标从0开始，所以添加时需要减1
            edges[v1 - 1, v2 - 1] = weight;
        }

        // 输出图的邻接矩阵
        public void PrintGraph()
        {
            Console.WriteLine("该图的邻接矩阵为：");
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    if (edges[i, j] == int.MaxValue)
                    {
                        Console.Write("∞ ");
                    }
                    else
                    {
                        Console.Write(edges[i, j]+" ");
                    }
                }
                Console.WriteLine();
            }
        }

        // Dijkstra算法的具体实现
        // 因为图的元素从1开始，数组下标从0开始，所以算法中的顶点v都需要减1
        public void Dijkstra(int v)
        {
            // 已找到最短路径的顶点数组
            int[] vs = new int[num];
            
            // 顶点v到其他顶点的最短路径数组
            int[] dist = new int[num];
            for (int i = 0; i < num; i++)
            {
                if (i != v-1) // 顶点v到其本身的最短路径长度为0，不需要赋值
                {
                    // 初始化dist数组
                    dist[i] = edges[v-1, i];
                }
            }

            // 需要num-1趟才能把剩下的顶点找到
            for (int i= 1; i < num; i++)
            {
                int minv = 0; // 当前最短路径顶点，默认为0
                int min = int.MaxValue; // 当前最短路径长度，默认为正无穷大

                // 找出具体的当前最短路径长度
                for (int j = 0; j < num; j++)
                {
                    // vs[j] == 0 已找到最短路径的顶点vs[j]为其最短路径长度，不与更新后的其他顶点比较最短路径长度
                    // j != v-1 表示顶点v本身不参与最短路径长度的比较
                    if (vs[j] == 0 && dist[j] < min && j != v-1) 
                    {
                        min = dist[j];
                        minv = j;
                    }
                }

                // 把找到最短路径的顶点和它的最短路径长度加入vs数组
                vs[minv] = min; // 2,1;

                // 在刚才找到最短路径的顶点的基础上，更新dist数组
                for (int k = 0; k < num; k++)
                {
                    // dist[minv] + edges[minv, k] > 0 是因为C#中的正无穷大是int内存空间里最大的数，如果是正无穷大再加一个正数，就会超出int内存空间变成负数
                    // dist[minv] + edges[minv, k] < dist[k] 是看松弛后是否比直接到达更近，如果是，就更新到该顶点的最短路径长度
                    if (dist[minv] + edges[minv, k] > 0 && dist[minv] + edges[minv, k] < dist[k])
                    {
                        dist[k] = dist[minv] + edges[minv, k];
                    }
                }
            }

            // 输出vs中的顶点和到该顶点的最短路径长度
            Console.WriteLine("顶点{0}到其他各顶点的最短路径长度为：", v);
            for (int i = 0; i < num; i++)
            {
                Console.Write("{0}:{1}, ", i+1, vs[i]);
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

            graph.PrintGraph();

            graph.Dijkstra(1);
        }
    }
}

