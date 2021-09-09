module Program

open System
open Absyn
open Lexer
open ExprParser
open ExprLexer
open FSharp.Text.Lexing
open Parse

[<EntryPoint>]
let main argv =
      printfn "Hello World from FsLex!\n\nPlease pass a digit:"
      let input = Console.ReadLine()
      let res = Tokenize (LexBuffer<char>.FromString input)
      printfn "The lexer recognizes %s" res

      printfn "%i" (tagOfToken (keyword "let"))

      printfn "%A" (CstI 1)
      printfn "%A" (fromFile "sample.txt")
      0
