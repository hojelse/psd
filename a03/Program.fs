module Program

open System
open Absyn
open ExprPar
open ExprLex
open FSharp.Text.Lexing
open Parse
open Expr

(* Ex 2.4 - assemble to integers *)
(* SCST = 0, SVAR = 1, SADD = 2, SSUB = 3, SMUL = 4, SPOP = 5, SSWAP = 6; *)
let sinstrToInt = function
  | SCstI i -> [0;i]
  | SVar i  -> [1;i]
  | SAdd    -> [2]
  | SSub    -> [3]
  | SMul    -> [4]
  | SPop    -> [5]
  | SSwap   -> [6]

let assemble instrs = List.foldBack (fun x acc -> sinstrToInt x @ acc) instrs []

(* Output the integers in list inss to the text file called fname: *)

let intsToFile (inss : int list) (fname : string) = 
  let text = String.concat " " (List.map string inss)
  System.IO.File.WriteAllText(fname, text);;

let printExample() =
  let src = "let z = 17 in z + z end"
  printfn "source   : %A" src
  let expression =  fromString src
  printfn "abstract : %A" expression
  let assembly = scomp expression []
  printfn "assembly : %A" assembly
  let bytes = assemble assembly
  printfn "bytes    : %A" bytes

let srcToBin =
  fromFile
  >> (fun x -> scomp x [])
  >> assemble
  >> intsToFile

let result1 = run (fromString "2 + 3 * 4")
let result2 = (eval (fromString "2 + x * 4") [("x", 3)])
let result3 = (eval (fromString "let x = 1+2 in 2 + x * 4 end") [])

let code1 = scomp (fromString "2 + 3 * 4") []
let result4 = seval code1 []

let code2 = scomp (fromString "2 + x * 4") [Bound "x"]
let result5 = seval code2 [3]

let code3 = scomp (fromString "let x = 1+2 in 2 + x * 4 end") []
let result6 = seval code3 []

let io() =
  Console.ReadLine()
  |> fromString
  |> fun x -> scomp x []
  |> assemble
  |> fun x -> List.foldBack (fun i acc -> sprintf "%i %s" i acc) x ""
  |> Console.WriteLine

(* Exercise 3.5 *)
let example1 =  fromString "1 + 2 * 3"
let example2 =  fromString "1 - 2 - 3"
let example3 =  fromString "1 + -2"
let example4 =  fromString "let z = (17) in z + 2 * 3 end"
let example5 = fromString "1 + let x=5 in let y=7+x in y+y end + x end"
// let example_err1 =  fromString "x++"
// let example_err2 =  fromString "1 + 1.2"
// let example_err3 =  fromString "1 + "
// let example_err4 =  fromString "let z = 17) in z + 2 * 2 end"
// let example_err5 =  fromString "let in = (17) in z + 2 * 2 end"

(* Exercise 3.6 *)
let compString (str:string) : sinstr list =
  str
  |> fromString
  |> fun x -> scomp x []

let compStringExample1 = compString "1 + 2 * 3"
let compStringExample2 = compString "let z = (17) in z + 2 * 3 end"
let compStringExample3 = compString "1 + let x=5 in let y=7+x in y+y end + x end"

let ifExample = fromString "if 1 then (5 + 5) else (5 * 5)"

[<EntryPoint>]
let main argv =
  // printExample()

  // io()

  // srcToBin "src.txt" "bytes.txt"

  // printfn "%A" result1
  // printfn "%A" result2
  // printfn "%A" result3
  // printfn "%A" result4
  // printfn "%A" result5
  // printfn "%A" result6

  // printfn "%A" example1
  // printfn "%A" example2
  // printfn "%A" example3
  // printfn "%A" example4
  // printfn "%A" example5

  // printfn "%A" compStringExample1
  // printfn "%A" compStringExample2
  // printfn "%A" compStringExample3

  printfn "%A" ifExample

  // printfn "Hello World from FsLex!\nPlease pass a digit:"
  // let input = Console.ReadLine()
  // let res = Token (LexBuffer<char>.FromString input)
  // printfn "The lexer recognizes %A" res
  0
