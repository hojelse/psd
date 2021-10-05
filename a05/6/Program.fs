module Program

open Absyn
open ParseAndRunHigher
open HigherFun

(* Exercise 6.1 *)

let consyn1 = @"
let add x =
  let f y =
    x + y
  in f end
in add 2 5 end
"

let consyn2 = @"
let add x =
  let f y =
    x + y in
  f
end in
let addtwo =
  add 2 in
addtwo 5
end
end
"

let consyn3 = @"
let add x =
  let f y =
    x + y in
  f end in
let addtwo =
  add 2 in
let x = 77 in
addtwo 5
end end end
"

let consyn4 = @"
let add x =
  let f y =
    x + y in
  f end in
add 2 end
"

(* Exercise 6.2 *)
let consyn2_1 = "fun x -> 2*x"
let consyn2_2 = "let y = 22 in fun z -> z+y end"

(* Exercise 6.3 *)
let consyn3_1 = @"
let add x = fun y -> x+y
in add 2 5 end
"

let consyn3_2 = @"
let add = fun x -> fun y -> x+y
in add 2 5 end
"

[<EntryPoint>]
let main argv =
  (* Exercise 6.1 *)
  printfn "eval 1: %A" (run (fromString consyn1))
  printfn "eval 2: %A" (run (fromString consyn2))
  printfn "eval 3: %A" (run (fromString consyn3))
  printfn "eval 4: %A" (run (fromString consyn4))

  (*
    In example 3,
    `addtwo` binds the `x` of `add` to 2,
    the later variable `x=77` is irrelevant to the parameter of `add`
    so the result of `Int 7` is expected
  *)

  (* Exercise 6.2 *)
  printfn "absyn 2_1: %A"      (fromString consyn2_1)
  printfn "eval 2_1: %A"  (run (fromString consyn2_1))
  printfn "absyn 2_2: %A"      (fromString consyn2_2)
  printfn "eval 2_2: %A"  (run (fromString consyn2_2))

  (* Exercise 6.3 *)
  printfn "absyn 3_1: %A"      (fromString consyn3_1)
  printfn "eval 3_1: %A"  (run (fromString consyn3_1))
  printfn "absyn 3_2: %A"      (fromString consyn3_2)
  printfn "eval 3_2: %A"  (run (fromString consyn3_2))

  0