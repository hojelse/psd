open ParseAndComp
open Comp

[<EntryPoint>]
let main argv =
  (* Exercise 8.1.ii *)
  // compileToFile (fromFile "./ex/ex3.c") "./ex/ex3.out"
  // compileToFile (fromFile "./ex/ex5.c") "./ex/ex5.out"

  printf "%A" (compileToFile (fromFile "./ex/ex3.c") "./ex/ex3.out")

  // `java Machine.java ./ex3.out 10`
  // `java Machine.java ./ex5.out 10`

  // `java Machinetrace ex3.out 4`

  (* Exercise 8.3 *)
  // let a = fromFile "./ex/preex.c"
  // compileToFile a "./ex/preex.out"

  (* Exercise 8.4 *)
  // compileToFile (fromFile "./ex/ex8.c") "./ex/ex8.out"

  0