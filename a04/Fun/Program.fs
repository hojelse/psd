module Program
open ParseAndRun

[<EntryPoint>]
let main argv =

  (* Exercise 4.1 *)
  let abst1 = fromString "let f x = if x = 0 then 1 else 0 in f 3 end"
  printfn "abstract syntax : %A" abst1
  printfn "eval: %A" (run abst1)

  (* Exercise 4.2 *)
  let abst_sum = fromString "let sum x = if x = 0 then x else x + sum (x-1) in sum 1000 end"
  printfn "sum abst: %A" abst_sum
  printfn "sum eval: %A" (run abst_sum)

  let abst_3pow8 = fromString "let f x = if x = 1 then x else 3 * f (x-1) in f 9 end"
  printfn "sum abst: %A" abst_3pow8
  printfn "sum eval: %A" (run abst_3pow8)

  (* Exercise 4.3 *)
  

  0