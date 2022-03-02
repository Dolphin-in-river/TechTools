module task2.FCode
open System
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
     
[<EntryPoint>]
let main argv =
    let pow = MyPow 2 4
    let circle = Circle 5.
    let square = getShapeSquare circle
    printf $"{pow} {square}"
    0