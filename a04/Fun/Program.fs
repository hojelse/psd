module Program
open ParseAndRun
open Absyn
open Fun

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

  let abst_3pow8 = fromString "let f x = if x = 0 then 1 else 3 * f (x-1) in f 8 end"
  printfn "3pow8 abst: %A" abst_3pow8
  printfn "3pow8 eval: %A" (run abst_3pow8)

  let abst_pows = fromString "let g y = if y = 0 then 1 else 3 * g (y-1) in let f x = if x = 0 then 1 else g x + f (x-1) in f 11 end end"
  printfn "pows abst: %A" abst_pows
  printfn "pows eval: %A" (run abst_pows)

  let abst_pows2 = fromString "let f x = let aux y = if y = 0 then 1 else x * aux (y-1) in aux 8 end in let g z = if z = 0 then 0 else (f z) + g (z-1) in g 10 end end"
  printfn "pows2 abst: %A" abst_pows2
  printfn "pows2 eval: %A" (run abst_pows2)

  (* Exercise 4.3 *)
  let multi_arg = Letfun("f", ["x"; "y"], Prim("+", Var "x", Var "y"), Call(Var "f", [CstI 10; CstI 1]))
  printfn "multi_arg abst: %A" (eval multi_arg [])

  (* Exercise 4.4 *)
  let multi_arg1 = fromString "let f a b = a + b in f 1 2 end"
  printfn "multi_arg1 abst: %A" multi_arg1
  printfn "multi_arg1 eval: %A" (run multi_arg1)

  let multi_arg2_pow = fromString "let pow x n = if n=0 then 1 else x * pow x (n-1) in pow 3 8 end"
  printfn "multi_arg2_pow abst: %A" multi_arg2_pow
  printfn "multi_arg2_pow eval: %A" (run multi_arg2_pow)

  let multi_arg3_max = fromString @"
    let max2 a b = if a<b then b else a
    in let max3 a b c = max2 a (max2 b c)
      in max3 25 6 62 end
    end
  "
  printfn "multi_arg3_max abst: %A" multi_arg3_max
  printfn "multi_arg3_max eval: %A" (run multi_arg3_max)

  (* Exercise 4.5 *)
  

  0