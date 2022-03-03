# TechTools 
## Lab 1
### Task 1
#### Изучить механизм интеропа между языками, попробовать у себя вызывать C/C++ (Не C++/CLI) код (суммы чисел достаточно) из Java и C#. В отчёте описать логику работы, сложности и ограничения этих механизмов.
Для того, чтобы вызвать написанные на C/C++ функции в C#/Java воспользуемся возможностями динамически-подключаемой библиотекой (.dll).

Код, написанный на С++, который затем будет использоваться в проектах C# и Java.

```#include "framework.h"
#include <cstdio>
#include <iostream>
#include <C:\Users\Иван\.jdks\openjdk-17.0.2\include\jni.h>
extern "C"
{
    jdouble __declspec (dllexport) __stdcall Java_MyClass_AddTwoElements(JNIEnv* env, jobject obj, jdouble firstElement, jdouble secondElement){
        std::cout << "Hello from C++" << std::endl;
        std::cout << firstElement << std::endl;
        std::cout << secondElement << std::endl;
        return firstElement + secondElement;
    }

    double __declspec (dllexport) __stdcall AddTwoElements(double firstElement, double secondElement) {
        std::cout << "Hello from C++" << std::endl;
        std::cout << firstElement << std::endl;
        std::cout << secondElement << std::endl;
        return firstElement + secondElement;
    }
}
```
 

Java / C#:
```
Java:

public class MyClass {
    native public double AddTwoElements(double firstElement, double secondElement);
    static{
        System.loadLibrary("CPPInterop");
    }
    public static void main(String[] args){
        double a = 100;
        double b = 200.5;
        System.out.println(new MyClass().AddTwoElements(a, b));
    }
}

C#:

using System;
using System.Runtime.InteropServices;

namespace lab_1
{
    class Program
    {
        [DllImport(@"C:\Users\Иван\RiderProjects\lab-1\task1\CPPInterop.dll",
            CallingConvention = CallingConvention.StdCall)]
        public static extern double AddTwoElements(double fristElement, double secondElement);

        static void Main()
        {
            double a = 100, b = 200.5;
            Console.WriteLine(AddTwoElements(a, b));
        }
    }   
}
   ```

#####Особенности интеропа в Java:

При использовании в Java из-за JNI - посредника между JVM и Native Code - возникают сложности с возвращением корректного результата сложения. 
Для корректной работы необходимо подключить к проекту С++ файл заголовка JNI (jni.h), он включает в себя все элементы JNI которые могут быть использованы в программе.

Далее в названии функции в С++ также необходимо прописать путь к ее вызову из Java (Java -> MyClass -> AddTwoElements), и при передаче параметров метода добавить указатель на JNIEnv; а также объект Javа.
При вызове из Java необходимо подключить данную библиотеку и в при объявлении метода добавить ключевое слово native.

#####Интероп в Java имеет ограничения:
1) Нельзя написать одну функцию, сделать из нее dll и в дальнейшем использовать для любого нового проекта. -> Придется создавать для каждого конкретного случая свою dll
2) Появляется доп. уровень JNI. Из-за дорогостоящих преобразований данных программа будет выполняться медленно. 

##### Особенности интеропа в С#
Для подключения к managed-приложению unmanaged-библиотеки используется механизм Platform Invoke. 
В нашем случае он позволяет подключить к С#-проекту dll библиотеку, написанную на С++.
Для этого необходимо верно указать путь где находится сам файл .dll.
При вызове из C# необходимо подключить данную библиотеку и в при объявлении метода добавить ключевое слово extern.

##### Ограничения интеропа в C#
1) Возможно вызвать только unmanaged-функции.
2) Импортируемые функции становятся статическими методами классов.

### Task2
Написать немного кода на Scala и F# с использованием уникальных возможностей языка - Pipe operator, Discriminated Union, Computation expressions и т.д. . Вызвать написанный код из обычных соответствующих ООП языков (Java и С#) и посмотреть во что превращается написанный раннее код после декомпиляции в них.

#### Scala -> Java
Напишем немного кода на Scala. Будем использовать функцию pipe. 
```
package SecondTask
import scala.util.chaining._
import scala.language.implicitConversions
object SecondTask {

  def Multi(a : Int, b : Int) : Int = {
    a * b
  }
  def Sum(a : Int, b : Int) : Int = {
    a + b
  }
  def FibonacciByCount(a : Int) : Int = {
    var counter = a - 2
    var first : Int = 1
    var second : Int = 1
    while (counter > 0){
      counter -= 1
      val buf = first
      first = second
      second = buf + second
    }
    second
  }
  def main(args: Array[String]): Unit = {
    println(FibonacciByCount(5))
    val s = 2.pipe(res => Sum(res, 10)).pipe(res => Multi(res, 20))
    println(s)
  }
}
```
Создадим новый класс на Java в том же самом  и вызовем данный код. Код успешно компилируется и корректно работает.
```
package SecondTask;

public class Main {
    public static void main(String[] args) {
        SecondTask.main(null);
        SecondTask.FibonacciByCount(8);
    }
}

```
Декомпилируем код в Java. 
При декомпиляции получаем несколько методов для объявления функций.
```public final class SecondTask {
   public static void main(final String[] args) {
      SecondTask$.MODULE$.main(args);
   }

   public static int FibonacciByCount(final int a) {
      return SecondTask$.MODULE$.FibonacciByCount(a);
   }

   public static int Sum(final int a, final int b) {
      return SecondTask$.MODULE$.Sum(a, b);
   }

   public static int Multi(final int a, final int b) {
      return SecondTask$.MODULE$.Multi(a, b);
   }
}
```
Сами функции:
```   public static final SecondTask$ MODULE$ = new SecondTask$();

   public int Multi(final int a, final int b) {
      return a * b;
   }

   public int Sum(final int a, final int b) {
      return a + b;
   }

   public int FibonacciByCount(final int a) {
      int counter = a - 2;
      int first = 1;

      int second;
      int buf;
      for(second = 1; counter > 0; second += buf) {
         --counter;
         buf = first;
         first = second;
      }

      return second;
   }
   ```
Данные функции идентичны функциям из Scala. 
Посмотрим что произошло с pipe оператором.
```
int s = BoxesRunTime.unboxToInt(scala.util.ChainingOps..MODULE$.pipe$extension(scala.util.package.chaining..MODULE$.scalaUtilChainingOps(scala.util.ChainingOps..MODULE$.pipe$extension(scala.util.package.chaining..MODULE$.scalaUtilChainingOps(BoxesRunTime.boxToInteger(2)), (res) -> {
return MODULE$.Sum(res, 10);
})), (res) -> {
return MODULE$.Multi(res, 20);
}));
```
Заметим, что pipe оператор раскрылся в цепочку функций (Sum, Multi), которые меняют входное значение. Также виден boxing переменой.

#### F# -> C#
Код на F#
```
module task2.FCode
open System
 let myInc x = x + 1
 let MyPow a b = 
    let mutable result = 1
    let mutable counter = 0
    while (counter < b) do 
        result <- result * a
        counter <- counter + 1
    result
    
 type Shape =
     | Circle of radius : float
     | Sphere of radius : float
     
 let getShapeSquare (shape : Shape) =
     match shape with
     | Circle(radius) -> radius * radius * 3.1415
     | Sphere(radius) -> 4. * 3.1415 * radius * radius
     
 let doubleInc x = x |> myInc |> myInc
 
[<EntryPoint>]
let main argv =
    let pow = MyPow 2 4
    let circle = Circle 5.
    let square = getShapeSquare circle
    let answer = doubleInc 4    
    printf $"{pow} {square} {answer}"
    0
```

При компиляции и в F# и в С# обычный код превращается в IL-код. Это позволяет вызвывать код из одного языка в другом.
Поэтому при вызове из С# код написанный на F# все успешно скомпилируется и будет дан корректный ответ.        

``` using task2;
using System;
namespace TaskSecond
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(FCode.MyPow(5, 4));
            Console.WriteLine(FCode.doubleInc(4));
            Console.WriteLine(FCode.main(null));
        }
    }   
}
```
При декомпиляции кода написанного на F# в C# получим:
Абстрактный класс Shape, от которого наследуются Circle и Sphere, пустые конструкторы, переопределение ToString, GetHashCode, Equals, CompareTo


### Task3




Написать алгоритм обхода графа (DFS и BFS) на языке Java, собрать в пакет и опубликовать (хоть в Maven, хоть в Gradle, не имеет значения). Использовать в другом проекте на Java/Scala этот пакет. Повторить это с C#/F#. В отчёте написать про алгоритм работы пакетных менеджеров, особенности их работы в C# и Java мирах.

Алгоритм обхода графа в С#:
```
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
```

Далее соберем проект и создадим пакет NuGet.
После этого загрузим проект в галерею проектов на сайте nuget.org.
После индексации на сайте я и другие пользователи могут использовать данный проект.
```open task3;
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
```
Программа успешно скомпилировалась и был получен правильный ответ.

Код на Java:
```import java.util.*;

public class Task3 {
    public List<Integer>[] list = null;
    public boolean[] visited = null;
    public void InitializationArrays(int count)
    {
       list = new List[count];
        for (int i = 0; i < count; i++)
        {
            list[i] = new ArrayList<Integer>();
        }
        visited = new boolean[count];
    }
    public void DFS(int start){
        System.out.print(start);
        visited[start] = true;
        for (Integer integer : list[start]) {
            int nextNode = integer;
            if (!visited[nextNode])
                DFS(nextNode);
        }
    }
    public void BFS(int start){
        visited[start] = true;
        Queue<Integer> queue = new ArrayDeque<>();
        queue.add(start);
        while (queue.size() > 0){
            int currNode = queue.remove();
            System.out.print(currNode);
            for (int i = 0; i < list[currNode].size(); i++){
                int newNode = list[currNode].get(i);
                if (!visited[newNode]){
                    visited[newNode] = true;
                    queue.add(newNode);
                }
            }
        }
    }
    public void AddDirectedEdge(int from, int to)
    {
       list[from].add(to);
    }
    public static void main(String[] args) {
    }
}
```

С помощью фреймворка maven был собран пакет из проекта на Java. После этого данный пакет был добавлен в проект на Scala. 
```object Main {
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
```
Программа успешно скомпилировалась и был получен правильный ответ.
Общий вид работы с maven:
1) maven ищет код для сборки в директории src/main/java.
2) Инструкции по сборке maven будет искать в <project>/pom.xml
3) Скомпилированный java код выглядит так же, как исходный код, но вместо файлов с расширением java, там будут файлы с расширением class.
4) Далее данный пакет вместе со скомпилированным java кодом можно будет применять в другом проекте.


###Task4

####C#
В качестве бенчмарка для проверки времени выполнения и аллокации памяти мной был выбран BenchmarkDotNet. 
Общий алогритм работы:
Для каждого значения из params запускается процесс BenchmarkDotNet несколько раз 
Проиходит некая последовательность итераций.
1) Pilot - Benchmark выбирает оптимальное количество операций
2) OverheadWarmup - оценка накладных расходов
3) ActualWarmup - прогрев метода рабочей нагрузки
4) ActualWorkload - фактические измерения 
5) Результаты
```using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace task4
{
    public class Program
    {
        [MemoryDiagnoser]
        public class TheEasiestBenchmark
        {
            [Params(1000, 10000, 100000)]
            public int count;

            [Benchmark(Description = "StandardSort")]
            public void StandardSortTest()
            {
                StandardSort.StartExecute(count);
            }

            [Benchmark(Description = "MergeSort")]
            public void MergeSortTest()
            {
                MergeSort.StartExecute(count);
            }

            [Benchmark(Description = "QuickSort")]
            public void QuickSortTest()
            {
                QuickSort.StartExecute(count);
            }

            [Benchmark(Description = "BubbleSort")]
            public void BubbleSortTest()
            {
                BubbleSort.StartExecute(count);
            }
        }
    }

    public static class MainClass
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Program.TheEasiestBenchmark>();
        }
    }
}
```
Проанализируем полученные результаты.
![alt text](https://github.com/Dolphin-in-river/TechTools/blob/main/CSharpBenchmark.png?raw=true)
![text](https://github.com/Dolphin-in-river/TechTools/CSharpBenchmark.png)

Видим, что стандартная сортировка является самой быстрой из представленных, а BubbleSort самой медленной.
После стандартной сортировки идет QuickSort, затем Merge. Также бенчмарк показал, что объем выделенной памяти получился у 3 сортировок одинаковым, а у Merge на порядок больше.
![text](https://github.com/Dolphin-in-river/TechTools/StanadardAndMerge.png)
![text](https://github.com/Dolphin-in-river/TechTools/QuickSortBubble.png)
Чем ближе синяя полоска (кол-во итераций) будет к оранжевой, тем более быстрыми является алгоритм. На данных графиках видно, что StandardSort и QuickSort являются самыми быстрыми, затем идет Merge, замыкает BubbleSort.
Таким образом, тесты показывают, что реализация 3 из 4 алгоритмов имеют достаточно приемлемое масштабирование.

####Java

Для оценки алгоритмов в Java я использовал JMH(Java Microbenchmarking Harness)
Данный бенчмарк позволяет писать аннотацию к функциям. Так, например, @Fork определяет количество прогревочных запусков и количество настоящих, прямо влияющих на результат.
@Warmup показывает количество прогревочных операций, а @Measurement количество настоящих.
В данных тестах использовались те же параметры (1000, 10000, 100000), что и на С#.
```package Benchmark;
import Algorithms.BubbleSort;
import Algorithms.MergeSort;
import Algorithms.QuickSort;
import Algorithms.StandardSort;
import org.openjdk.jmh.annotations.*;
import java.util.concurrent.TimeUnit;

public class ExecuteBenchmark {
    private static final MyArray myArray = new MyArray();
    @State(Scope.Benchmark)
    public static class MyBenchmarkState {

        @Param({ "1000", "10000", "100000" })
        public int value;
    }

    @Benchmark
    @OutputTimeUnit(TimeUnit.NANOSECONDS)
    @BenchmarkMode(Mode.AverageTime)
    @Warmup(iterations = 0)
    @Measurement(iterations = 1)
    @Fork(value = 1, warmups = 0)
    public int BubbleSort(MyBenchmarkState state) {
        int[] array = myArray.GetRandomArr(state.value);
        BubbleSort.ExecuteSort(array);
        return array.length;
    }

    @Benchmark
    @OutputTimeUnit(TimeUnit.NANOSECONDS)
    @BenchmarkMode(Mode.AverageTime)
    @Warmup(iterations = 0)
    @Measurement(iterations = 1)
    @Fork(value = 1, warmups = 0)
    public int MergeSort(MyBenchmarkState state) {
        int[] array = myArray.GetRandomArr(state.value);
        MergeSort.ExecuteSort(array, array.length);
        return array.length;
    }

    @Benchmark
    @OutputTimeUnit(TimeUnit.NANOSECONDS)
    @BenchmarkMode(Mode.AverageTime)
    @Warmup(iterations = 0)
    @Measurement(iterations = 1)
    @Fork(value = 1, warmups = 0)
    public int StandardSort(MyBenchmarkState state) {
        int[] array = myArray.GetRandomArr(state.value);
        StandardSort.ExecuteSort(array);
        return array.length;
    }
    @Benchmark
    @OutputTimeUnit(TimeUnit.NANOSECONDS)
    @BenchmarkMode(Mode.AverageTime)
    @Warmup(iterations = 0)
    @Measurement(iterations = 1)
    @Fork(value = 1, warmups = 0)
    public int QuickSort(MyBenchmarkState state) {
        int[] array = myArray.GetRandomArr(state.value);
        QuickSort.ExecuteSort(array, 0, array.length - 1);
        return array.length;
    }
}
```

![text](https://github.com/Dolphin-in-river/TechTools/JavaBenchmark.jfif)
Анализ результатов:
Самым быстрой оказалась стандартная сортировка, затем идет Quick, Merge и замыкает Bubble.
Эти результаты корелируются с оценкой сложности этих алгоритмов.


###Task 5

В 5 задании было создано 2 цикла. Каждый из которых делает 1
