module Program

open System
open Absyn
open Lexer
open ExprParser
open ExprLexer
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

// let printExample() =
//   let src = "let z = 17 in z + z end"
//   printfn "source   : %A" src
//   let expression =  fromString src
//   printfn "abstract : %A" expression
//   let assembly = scomp expression []
//   printfn "assembly : %A" assembly
//   let bytes = assemble assembly
//   printfn "bytes    : %A" bytes

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


[<EntryPoint>]
let main argv =
  // printExample()

  Console.ReadLine()
  |> fromString
  |> fun x -> scomp x []
  |> assemble
  |> fun x -> List.foldBack (fun i acc -> sprintf "%i %s" i acc) x ""
  |> Console.WriteLine

  // srcToBin "src.txt" "bytes.txt"

  // printfn "%A" result1
  // printfn "%A" result2
  // printfn "%A" result3
  // printfn "%A" result4
  // printfn "%A" result5
  // printfn "%A" result6

  // printfn "Hello World from FsLex!\nPlease pass a digit:"
  // let input = Console.ReadLine()
  // let res = Token (LexBuffer<char>.FromString input)
  // printfn "The lexer recognizes %A" res
  0
