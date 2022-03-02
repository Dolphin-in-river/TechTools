open task3;
let program = new Program()
program.InitializationArrays(5)
program.AddDirectedEdge(0, 1);
program.AddDirectedEdge(1, 4);
program.AddDirectedEdge(4, 2);
program.AddDirectedEdge(0, 3);
let a = program.myList[0][0]
printfn $"{a}"
program.Bfs(0);
printfn ""
program = new Program()
program.InitializationArrays(5)
program.AddDirectedEdge(0, 1);
program.AddDirectedEdge(1, 4);
program.AddDirectedEdge(4, 2);
program.AddDirectedEdge(0, 3);
program.Dfs(0);