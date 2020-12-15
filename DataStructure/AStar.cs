using System;
using System.Collections.Generic;

namespace AStar
{
    // 每个节点的坐标
    public class Location
    {
        private int x;
        private int y;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public Location(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    // 节点的定义
    public class Node
    {
        // 节点坐标
        private Location location;
        // 节点到起点的实际代价
        private int costG;
        // 节点到终点的估算代价
        private int costH;
        // 经过该结点的最短路径的估算代价
        private int costF;
        // 节点的父结点，默认值为null表示没有指定父节点。
        private Node father;
        // 节点是否已被访问，默认值为false表示没有被访问。
        private bool isVisted;

        public Location Location { get => location; set => location = value; }
        public int CostG { get => costG; set => costG = value; }
        public int CostH { get => costH; set => costH = value; }
        public int CostF { get => costF; set => costF = value; }
        public Node Father { get => father; set => father = value; }
        public bool IsVisted { get => isVisted; set => isVisted = value; }

        // 初始化节点
        public Node(Location location, int costG, int costH)
        {
            this.location = location;
            this.costG = costG;
            this.costH = costH;
            this.costF = costG + costH;
        }

        // 节点向周围八个方向走一步的代价
        public int StepCost(Location location)
        {
            int dx = location.X - this.location.X;
            int dy = location.Y - this.location.Y;
            switch (dx, dy)
            {
                // 往上走
                case (0, 1):
                // 往下走
                case (0, -1):
                // 往左走
                case (-1, 0):
                // 往右走
                case (1, 0):
                    {
                        // 往上下左右走每一步代价为10
                        return 10;
                    }
                // 往右上方走
                case (1, 1):
                // 往右下方走
                case (1, -1):
                // 往左下方走
                case (-1, -1):
                // 往左上方走
                case (-1, 1):
                    {
                        // 往四个方向斜着走每一步代价为14，对于一个正方形节点斜边约为直角边的1.4倍。
                        return 14;
                    }
                // 往其他方向走代价默认为0
                default:
                    return 0;
            }
        }
    }

    // 八个方向，以当前节点为基准，上北下南左西右东
    public enum Direction
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest
    }

    // 定义当前地图
    public class Map
    {
        // 地图长度
        private int length;
        // 地图宽度
        private int width;
        // 障碍矩阵，默认为fasle表示没有障碍。
        private bool[,] obstacles;

        public int Length { get => length; set => length = value; }
        public int Width { get => width; set => width = value; }
        public bool[,] Obstacles { get => obstacles; set => obstacles = value; }

        // 地图构造函数
        public Map(int length, int width, List<Location> obstacleList)
        {
            this.Length = length;
            this.Width = width;
            this.Obstacles = new bool[length, width];
            // 初始化地图中的障碍结点
            InitObstacles(obstacleList);
        }

        // 设置障碍函数
        private void InitObstacles(List<Location> obstacleList)
        {
            foreach (Location o in obstacleList)
            {
                Obstacles[o.X, o.Y] = true;
            }
        }

        // 以当前节点为基准，判断某个方向上位置的相邻结点能否到达
        public bool isWalkable(Node currentNode, Location nextLocation)
        {
            // 如果相邻结点在地图之外，则不可到达
            if (nextLocation.X < 0 || nextLocation.X > Length - 1 || nextLocation.Y < 0 || nextLocation.Y > Width - 1)
            {
                return false;
            }

            // 如果相邻结点是障碍，则不可到达
            if (Obstacles[nextLocation.X, nextLocation.Y])
            {
                return false;
            }

            // 如果当前节点右边是一个障碍，且相邻结点在障碍的上方或下方，则该节点不可直接穿墙角到该相邻结点。
            if (currentNode.Location.X + 1 >= 0 && currentNode.Location.X + 1 <= Length - 1 && Obstacles[currentNode.Location.X + 1, currentNode.Location.Y] && currentNode.Location.X + 1 == nextLocation.X)
            {
                return false;
            }

            // 如果当前节点左边是一个障碍，且相邻结点在障碍的上方或下方，则该节点不可直接穿墙角到该相邻结点。
            if (currentNode.Location.X - 1 >= 0 && currentNode.Location.X - 1 <= Length - 1 && Obstacles[currentNode.Location.X - 1, currentNode.Location.Y] && currentNode.Location.X - 1 == nextLocation.X)
            {
                return false;
            }

            // 如果当前节点上方是一个障碍，且相邻结点在障碍的左边或右边，则该节点不可直接穿墙角到该相邻结点。
            if (currentNode.Location.Y + 1 >= 0 && currentNode.Location.Y + 1 <= Width - 1 && Obstacles[currentNode.Location.X, currentNode.Location.Y + 1] && currentNode.Location.Y + 1 == nextLocation.Y)
            {
                return false;
            }

            // 如果当前节点下方是一个障碍，且相邻结点在障碍的左边或右边，则该节点不可直接穿墙角到该相邻结点。
            if (currentNode.Location.Y - 1 >= 0 && currentNode.Location.Y - 1 <= Width - 1 && Obstacles[currentNode.Location.X, currentNode.Location.Y - 1] && currentNode.Location.Y - 1 == nextLocation.Y)
            {
                return false;
            }

            return true;
        }
    }

    // A*算法类
    public class AStar
    {
        private Map map;
        // 起始节点
        private Location source;
        private Node startNode;
        // 终点节点
        private Location destination;
        private Node endNode;
        // 最小代价F节点
        private Node minCostFNode;
        // 存放Direction类型的八个方向。
        private List<Direction> directionList;
        // open表用于存放待处理的节点
        private List<Node> openList;
        // close表用于存放已经处理过的节点
        private List<Node> closeList;
        // 最短路径
        private List<Node> route;

        public List<Node> OpenList { get => openList; set => openList = value; }
        public List<Node> CloseList { get => closeList; set => closeList = value; }
        public Node StartNode { get => startNode; set => startNode = value; }

        // A*类构造函数，参数为：地图，起点坐标，终点坐标
        public AStar(Map map, Location source, Location destination)
        {
            this.map = map;
            this.source = source;
            this.destination = destination;
            this.startNode = new Node(source, 0, (Math.Abs(source.X - destination.X) + Math.Abs(source.Y - destination.Y)) * 10);
            // 终点的代价G默认为无穷大
            this.endNode = new Node(destination, int.MaxValue, 0);
            this.openList = new List<Node>();
            // 将起点加入open表等待处理
            this.closeList = new List<Node>();
            openList.Add(startNode);
            this.route = new List<Node>();
            this.directionList = new List<Direction> { Direction.North, Direction.NorthEast, Direction.East, Direction.SouthEast, Direction.South, Direction.SouthWest, Direction.West, Direction.NorthWest };
        }

        // A*算法具体的递归执行函数
        public List<Node> DoAStar(Node currentNode, List<Node> openList, List<Node> closeList)
        {
            // 每次寻找相邻节点时，先把open表中所有节点的IsVisted标志位置为true，表示它们已经被访问过。
            foreach (Node node in openList)
            {
                node.IsVisted = true;
            }

            // 开始遍历当前节点周围的八个方向寻找相邻节点
            foreach (Direction direction in directionList)
            {
                // 获取某个方向上的相邻节点
                Node nextNode = GetAdjacentLocation(currentNode, direction);
                // 判断该节点位置是否能够到达，不能到达直接跳过本次循环，继续寻找下一个方向
                if (!map.isWalkable(currentNode, nextNode.Location))
                {
                    continue;
                }

                // 如果相邻节点的估价H为0，表示该相邻节点是终点
                if (nextNode.CostH == 0)
                {
                    // 把终点的父节点指向当前节点
                    nextNode.Father = currentNode;
                    // 沿着终点的父节点回溯得到最短路径
                    while (nextNode != null)
                    {
                        // 由于是回溯，因此需要依次将节点插入到route的第一个位置
                        route.Insert(0, nextNode);
                        nextNode = nextNode.Father;
                    }
                    // 返回最短路径，A*算法结束
                    return route;
                }

                // 如果相邻节点已经在open表，即已经被访问过，就判断当前节点的代价G加上到该相邻节点的代价G，是否小于该相邻节点之前的代价G。
                // 如果是，就更新该相邻节点的代价G和代价F，已经更改它的父节点为当前节点。如果不是，则不做任何操作。
                if (isOpened(nextNode.Location) && nextNode.Father != null && nextNode.CostG < nextNode.Father.CostG + nextNode.Father.StepCost(nextNode.Location))
                {
                    // 如果是就说明找到了一条经过该相邻节点的更短路径，把该相邻节点的父节点改为当前节点。因为该相邻节点的代价H是不会变，代价G变小了，因此代价F也就更小了。
                    nextNode.Father = currentNode;
                }

                // 如果相邻节点不在open表和close表中，就说明该相邻节点是一个新节点，把它的父节点指向当前节点并加入到open表中。
                if (!isOpened(nextNode.Location) && !isClosed(nextNode.Location))
                {
                    nextNode.Father = currentNode;
                    // 每一轮代价F排序先从新加入的节点开始，这里的nextNode只是一个“哨兵”，具体值不重要。
                    minCostFNode = nextNode;
                    openList.Add(nextNode);
                }
            }

            // 当周围八个方向上的相邻结点都已经处理过了，就把该当前节点移除open表加入到close表中，表示该节点不再处理及参与代价F的排序。
            openList.Remove(currentNode);
            closeList.Add(currentNode);

            // 从open表中获取一个最小代价F节点，作为最短路径上的下一个节点。
            minCostFNode = GetMinCostFNode();
            // 如果最小代价F节点为空，即open表为空，A*算法搜索失败，没有找到最短路径。
            if (minCostFNode == null)
            {
                return null;
            }
            // 把最小代价F节点的父节点指向当前节点，确保最短路径上的结点不“断链”。
            minCostFNode.Father = currentNode;

            // 把最小代价F节点作为下一轮递归执行的开始结点
            return DoAStar(minCostFNode, openList, closeList);
        }

        // 显示最短路径
        public void ShowRoute()
        {
            bool flag = false;
            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Length; j++)
                {
                    if (map.Obstacles[j, i])
                    {
                        Console.Write("■ ");
                    }
                    else if ((j == source.X && i == source.Y) || (j == destination.X && i == destination.Y))
                    {
                        Console.Write("◆ ");
                    }
                    else
                    {
                        flag = false;
                        foreach (Node node in route)
                        {
                            if (j == node.Location.X && i == node.Location.Y)
                            {
                                flag = true;
                                Console.Write("▲ ");
                            }
                        }
                        if (flag == false)
                        {
                            Console.Write("□ ");
                        }
                    }
                }

                Console.WriteLine();
            }
        }

        // 获取最小代价F节点
        public Node GetMinCostFNode()
        {
            // 如果open表为空，则最小代价F节点为空
            if (openList.Count == 0)
            {
                return null;
            }

            // 如果open表中的所有节点都已经被访问过，则重新遍历open表寻找最小代价F节点。
            if (isAllVisited())
            {
                minCostFNode = openList[0];
                foreach (Node node in openList)
                {
                    if (node.CostF < minCostFNode.CostF)
                    {
                        minCostFNode = node;
                    }
                }
            }

            // 如果有新加入的节点和代价F更新的节点，就从这些节点中找最小代价节点，基准元素就是刚才的“哨兵”。
            foreach (Node node in openList)
            {
                if (node.CostF < minCostFNode.CostF && !node.IsVisted)
                {
                    minCostFNode = node;
                }
            }

            return minCostFNode;
        }

        // 判断open表中的所有节点是否已经被访问过
        public bool isAllVisited()
        {
            foreach (Node node in openList)
            {
                if (!node.IsVisted)
                {
                    return false;
                }
            }

            return true;
        }

        // 判断相邻节点是否已经在open表中
        public bool isOpened(Location location)
        {
            foreach (Node node in openList)
            {
                if (node.Location.X == location.X && node.Location.Y == location.Y)
                {
                    return true;
                }
            }

            return false;
        }

        // 判断相邻节点是否已经在close表中
        public bool isClosed(Location location)
        {
            foreach (Node node in closeList)
            {
                if (node.Location.X == location.X && node.Location.Y == location.Y)
                {
                    return true;
                }
            }

            return false;
        }

        // 获取当前节点某个位置的相邻节点
        public Node GetNode(Node currentNode, Location location)
        {
            // 该相邻节点的代价G为当前节点的代价G加上到该相邻节点位置上这一步的代价
            int costG = currentNode.CostG + currentNode.StepCost(location);
            // 采用曼哈顿距离估算该相邻节点到终点的代价H
            int costH = (Math.Abs(location.X - endNode.Location.X) + Math.Abs(location.Y - endNode.Location.Y)) * 10;
            return new Node(location, costG, costH);
        }

        // 获取当前节点某个方向上的相邻节点
        public Node GetAdjacentLocation(Node currentNode, Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    {
                        Location locationNorth = new Location(currentNode.Location.X, currentNode.Location.Y + 1);
                        return GetNode(currentNode, locationNorth);
                    }
                case Direction.NorthEast:
                    {
                        Location locationNorthEast = new Location(currentNode.Location.X + 1, currentNode.Location.Y + 1);
                        return GetNode(currentNode, locationNorthEast);
                    }
                case Direction.East:
                    {
                        Location locationEast = new Location(currentNode.Location.X + 1, currentNode.Location.Y);
                        return GetNode(currentNode, locationEast);
                    }
                case Direction.SouthEast:
                    {
                        Location locationSouthEast = new Location(currentNode.Location.X + 1, currentNode.Location.Y - 1);
                        return GetNode(currentNode, locationSouthEast);
                    }
                case Direction.South:
                    {
                        Location locationSouth = new Location(currentNode.Location.X, currentNode.Location.Y - 1);
                        return GetNode(currentNode, locationSouth);
                    }
                case Direction.SouthWest:
                    {
                        Location locationSouthWest = new Location(currentNode.Location.X - 1, currentNode.Location.Y - 1);
                        return GetNode(currentNode, locationSouthWest);
                    }
                case Direction.West:
                    {
                        Location locationWest = new Location(currentNode.Location.X - 1, currentNode.Location.Y);
                        return GetNode(currentNode, locationWest);
                    }
                case Direction.NorthWest:
                    {
                        Location locationNorthWest = new Location(currentNode.Location.X - 1, currentNode.Location.Y + 1);
                        return GetNode(currentNode, locationNorthWest);
                    }
                default:
                    {
                        return currentNode;
                    }
            }
        }
    }

    class Client
    {
        public static void Main1(string[] args)
        {
            // 设置障碍
            List<Location> obstacleList = new List<Location>() { new Location(4, 1), new Location(4, 2), new Location(4, 3) };
            // 创建地图
            Map map = new Map(8, 6, obstacleList);
            // 指定地图，起点坐标，终点坐标，开始初始化A*算法对象。
            AStar aStar = new AStar(map, new Location(2, 2), new Location(6, 2));
            // 指定开始节点，open表，close表，开始执行A*算法，并返回最短路径。
            aStar.DoAStar(aStar.StartNode, aStar.OpenList, aStar.CloseList);
            //List<Node> route = aStar.DoAStar(aStar.StartNode, aStar.OpenList, aStar.CloseList);
            //// 输出最短路径节点
            //foreach (Node node in route)
            //{
            //    Console.WriteLine("({0},{1}):{2} ", node.Location.X, node.Location.Y, node.CostG);
            //}
            Console.WriteLine("显示最短路径: ");
            aStar.ShowRoute();
        }
    }
}

