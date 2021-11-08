module Program

open Absyn
open ParseAndRunHigher
open HigherFun
open TypeInference

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

(* Exercise 6.5 *)
let consyn5_1 = @"
let f x = 1
in f f end
"

let consyn5_2 = @"
let f g = g g 
in f end
"

let consyn5_3 = @"
let f x =
  let g y = y
  in g false end
in f 42 end
"

let consyn5_4 = @"
let f x =
  let g y = if true then y else x
  in g false end
in f 42 end
"

let consyn6 = @"
let f x =
  let g y = if true then y else x
  in g false end
in f true end
"

[<EntryPoint>]
let main argv =
  (* Exercise 6.1 *)
  // printfn "eval 1: %A" (run (fromString consyn1))
  // printfn "eval 2: %A" (run (fromString consyn2))
  // printfn "eval 3: %A" (run (fromString consyn3))
  // printfn "eval 4: %A" (run (fromString consyn4))

  (*
    In example 3,
    `addtwo` binds the `x` of `add` to 2,
    the later variable `x=77` is irrelevant to the parameter of `add`
    so the result of `Int 7` is expected
  *)

  (* Exercise 6.2 *)
  // printfn "absyn 2_1: %A"      (fromString consyn2_1)
  // printfn "eval 2_1: %A"  (run (fromString consyn2_1))
  // printfn "absyn 2_2: %A"      (fromString consyn2_2)
  // printfn "eval 2_2: %A"  (run (fromString consyn2_2))

  (* Exercise 6.3 *)
  // printfn "absyn 3_1: %A"      (fromString consyn3_1)
  // printfn "eval 3_1: %A"  (run (fromString consyn3_1))
  // printfn "absyn 3_2: %A"      (fromString consyn3_2)
  // printfn "eval 3_2: %A"  (run (fromString consyn3_2))

  (* Exercise 6.5 *)
  // printfn "type5_1: %A" (consyn5_1 |> fromString |> inferType)
  // printfn "type5_2: %A" (consyn5_2 |> fromString |> inferType)
  // printfn "type5_3: %A" (consyn5_3 |> fromString |> inferType)
  // printfn "type5_4: %A" (consyn5_4 |> fromString |> inferType)
  // printfn "type5_5: %A" (consyn5_5 |> fromString |> inferType)

  (*
    concrete syntax 5_2 and concrete syntax 5_5 could not be typed

    5_2
    is has to be of type ('a -> 'b) but then 'a has to be of type ('a -> 'b), because g takes it self as argument.

    5_4
    f takes an int x, g takes a bool y,
    g returns if _ then y else x, but y and x is not of the same type

  *)

  // bool -> bool
  let consyn5_6 = @"let f x = if x then true else false in f end"
  printfn "type5_6: %A" (consyn5_6 |> fromString |> inferType)

  // int -> int
  let consyn5_7 = "let f x = x + 1 in f end"
  printfn "type5_7: %A" (consyn5_7 |> fromString |> inferType)

  // int -> int -> int
  let consyn5_8 = "let f x = let g y = x+y in g end in f end"
  printfn "type5_8: %A" (consyn5_8 |> fromString |> inferType)

  // 'a -> 'b -> 'a
  let consyn5_9 = @"
  let f x =
    let g y =
      x
    in g end
  in f end
  "
  printfn "type5_9: %A" (consyn5_9 |> fromString |> inferType)

  // 'a -> 'b -> 'b
  let consyn5_10 = @"
  let f x =
    let g y =
      y
    in g end
  in f end
  "
  printfn "type5_10: %A" (consyn5_10 |> fromString |> inferType)

  // ('a -> 'b) -> ('b -> 'c) -> ('a -> 'c)
  let consyn5_11 = @"
  let f x =
    let g y =
      f g 
    in g end
  in f end
  "
  printfn "type5_11: %A" (consyn5_11 |> fromString |> inferType)

  // ('a -> 'b)
  let consyn5_12 = @"
  let f x =
    let g y =
      y
    in f (g x) end
  in f end
  "
  printfn "type5_12: %A" (consyn5_12 |> fromString |> inferType)

  // 'a
  let consyn5_13 = @"
  let f x =
    let g = x
    in g end
  in f end
  "
  printfn "type5_13: %A" (consyn5_13 |> fromString |> inferType)

  0