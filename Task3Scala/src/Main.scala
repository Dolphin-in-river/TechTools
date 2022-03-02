object Main {
  def main(args: Array[String]): Unit = {
    val graph = new Task3()
    graph.InitializationArrays(5)
    graph.AddDirectedEdge(0, 1);
    graph.AddDirectedEdge(1, 4);
    graph.AddDirectedEdge(4, 2);
    graph.AddDirectedEdge(0, 3);
    graph.BFS(0);
    println("")
    graph.InitializationArrays(5)
    graph.AddDirectedEdge(0, 1);
    graph.AddDirectedEdge(1, 4);
    graph.AddDirectedEdge(4, 2);
    graph.AddDirectedEdge(0, 3);
    graph.DFS(0);
  }
}