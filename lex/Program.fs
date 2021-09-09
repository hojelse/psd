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

[<EntryPoint>]
let main argv =
      // printfn "printing to ./is1.txt"
      // intsToFile (assemble (scomp e1 [])) "is1.txt";

      printfn "%A" (run (fromString "2 + 3 * 4"))
      printfn "%A" (eval (fromString "2 + x * 4") [("x", 3)])
      printfn "%A" (eval (fromString "let x = 1+2 in 2 + x * 4 end") [])
      printfn "%A" (run (fromString "2 + 3 * 4"))

      let code1 = scomp (fromString "2 + 3 * 4") []
      printfn "%A" (seval code1 [])

      let code2 = scomp (fromString "2 + x * 4") [Bound "x"]
      printfn "%A" (seval code2 [3])

      let code3 = scomp (fromString "let x = 1+2 in 2 + x * 4 end") []
      printfn "%A" (seval code3 [])

      printfn "%A" ("let z = 3 in z + 1 end" |> fromString |> run)
      // printfn "%A" ("let z = 3 in let y = 1 in z + y end" |> fromString |> run)

      printfn "Hello World from FsLex!\n\nPlease pass a digit:"
      let input = Console.ReadLine()
      let res = Token (LexBuffer<char>.FromString input)
      printfn "The lexer recognizes %A" res

      printfn "%i" (tagOfToken (keyword "let"))

      printfn "%A" (CstI 1)
      0
