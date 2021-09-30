open System

let merge xs ys =
  let rec aux xs ys acc =
    match xs, ys with
    | [], []                     -> acc
    | [], h2::t2                 -> aux xs t2 (h2::acc)
    | h1::t1, []                 -> aux t1 ys (h1::acc)
    | h1::t1, h2::t2 when h1 < h2 -> aux t1 (h2::t2) (h1::acc)
    | h1::t1, h2::t2              -> aux (h1::t1) t2 (h2::acc)
  aux xs ys []
  |> List.rev

[<EntryPoint>]
let main argv =
  let xs = [0;1;3;6;7;8;9]
  let ys = [2;4;5]
  let merged = merge xs ys
  printfn "%A" merged
  0
