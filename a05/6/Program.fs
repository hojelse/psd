module Program

open ParseAndRunHigher
open HigherFun

(* Exercise 6.1 *)

let str1 = @"
let add x =
  let f y =
    x + y
  in f end
in add 2 5 end
"

let str2 = @"
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

let str3 = @"
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

let str4 = @"
let add x =
  let f y =
    x + y in
  f end in
add 2 end
"

let example1 = run (fromString str1)
let example2 = run (fromString str2)
let example3 = run (fromString str3)
let example4 = run (fromString str4)

(* Exercise 6.2 *)

let str2_1 = "fun x -> 2*x"
let str2_2 = "let y = 22 in fun z -> z+y end"

let example2_1 = fromString str2_1
let example2_2 = fromString str2_2

[<EntryPoint>]
let main argv =
  (* Exercise 6.1 *)
  printfn "eval 1: %A" example1
  printfn "eval 2: %A" example2
  printfn "eval 3: %A" example3
  printfn "eval 4: %A" example4

  (*
    In example 3,
    `addtwo` binds the `x` of `add` to 2,
    the later variable `x=77` is irrelevant to the parameter of `add`
    so the result of `Int 7` is expected
  *)

  (* Exercise 6.2 *)
  // printf "absyn 2_1: %A" example2_1
  // printf "absyn 2_2: %A" example2_2

  0