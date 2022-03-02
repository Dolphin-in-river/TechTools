using System;
using System.Collections.Generic;

namespace task3
{
    public class Program
    {
        public List<int>[] myList = null;
        public bool[] visited = null;

        public void InitializationArrays(int count)
        {
            myList = new List<int>[count];
            for (int i = 0; i < count; i++)
            {
                myList[i] = new List<int>();
            }
            visited = new bool[count];
        }
        public void Dfs(int start)
        {
            Console.Write(start);
            visited[start] = true;
            foreach (int integer in myList[start])
            {
                int nextNode = integer;
                if (!visited[nextNode])
                    Dfs(nextNode);
            }
        }

        public void Bfs(int start)
        {
            visited[start] = true;
            var queue = new List<int>();
            queue.Add(start);
            while (queue.Count > 0)
            {
                int currNode = queue[0];
                Console.Write(currNode);
                queue.RemoveAt(0);
                for (int i = 0; i < myList[currNode].Count; i++)
                {
                    int newNode = myList[currNode][i];
                    if (!visited[newNode])
                    {
                        visited[newNode] = true;
                        queue.Add(newNode);
                    }
                }
            }
        }

        public void AddDirectedEdge(int from, int to)
        {
            myList[from].Add(to);
        }
        public static void Main(String[] args)
        {
        }
    }
}