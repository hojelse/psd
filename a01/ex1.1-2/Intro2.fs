module Intro2
open System

let env = [("a", 3); ("c", 78); ("baf", 666); ("b", 111)];;

let emptyenv = []; (* the empty environment *)

let rec lookup env x =
    match env with 
    | []        -> failwith (x + " not found")
    | (y, v)::r -> if x=y then v else lookup r x;;

let cvalue = lookup env "c";;

type expr = 
  | CstI of int
  | Var of string
  | Prim of string * expr * expr
  | If of expr * expr * expr;;

let e1 = CstI 17;;

let e2 = Prim("+", CstI 3, Var "a");;

let e3 = Prim("+", Prim("*", Var "b", CstI 9), Var "a");;

let rec eval e (env : (string * int) list) : int =
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x 
    | Prim("+", e1, e2) -> eval e1 env + eval e2 env
    | Prim("*", e1, e2) -> eval e1 env * eval e2 env
    | Prim("-", e1, e2) -> eval e1 env - eval e2 env
    | Prim("min", e1, e2) -> min (eval e1 env) (eval e2 env)
    | Prim("max", e1, e2) -> max (eval e1 env) (eval e2 env)
    | Prim("==", e1, e2) -> if eval e1 env = eval e2 env then 1 else 0
    | Prim _            -> failwith "unknown primitive";;

let e1v  = eval e1 env;;
let e2v1 = eval e2 env;;
let e2v2 = eval e2 [("a", 314)];;
let e3v  = eval e3 env;;

(* Exercise 1.1.2 *)

let e4 = Prim("==", Var "baf", Var "a");;
let e5 = Prim("==", Var "baf", CstI 666);;
let e6 = Prim("min", Var "baf", CstI 665);;
let e7 = Prim("max", Var "baf", CstI 665);;

let e4v  = eval e4 env;;
let e5v  = eval e5 env;;
let e6v  = eval e6 env;;
let e7v  = eval e7 env;;

(* Exercise 1.1.3*)

let rec eval2 e (env : (string * int) list) : int =
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x
    | Prim(op, e1, e2) ->
        let i1 = eval e1 env
        let i2 = eval e2 env
        match op with
        |"+" -> i1 + i2
        |"*" -> i1 * i2
        |"-" -> i1 - i2
        |"min" -> min i1 i2
        |"max" -> max i1 i2
        |"==" -> if i1 = i2 then 1 else 0
        | _ -> failwith "unknown operator"
    (* Exercise 1.1.4 *)
    | If(e1, e2, e3) ->
        match eval e1 env with
        | 0 -> eval e3 env
        | _ -> eval e2 env

let honk1 = If(Var "a", CstI 11, CstI 22);;
let honk2 = If(CstI 0, CstI 11, CstI 22);;

let bonk1 = eval2 honk1 env;;
let bonk2 = eval2 honk2 env;;

(* Exercise 1.2 *)
type aexpr = 
    | CstI of int
    | Var of string
    | Add of aexpr * aexpr
    | Mul of aexpr * aexpr
    | Sub of aexpr * aexpr

(* Exercise 1.2.2 *)

let a1 = Sub(Var "v", Add(Var "w", Var "z"));;
let a2 = Mul(CstI 2, Sub(Var "v", Add(Var "w", Var "z")))
let a3 = Add(Var "x", Add(Var "y", Add(Var "z", Var "v")))

(* Exercise 1.2.3 *)

let rec fmt (a:aexpr) : string =
    match a with
    | CstI x -> string x
    | Var s -> s
    | Add(a1, a2) -> sprintf "(%s + %s)" (fmt a1) (fmt a2)
    | Mul(a1, a2) -> sprintf "(%s * %s)" (fmt a1) (fmt a2)
    | Sub(a1, a2) -> sprintf "(%s - %s)" (fmt a1) (fmt a2)

let aa1 = fmt a1;;
let aa2 = fmt a2;;
let aa3 = fmt a3;;

(* Exercise 1.2.4 *)

let rec simplify (a:aexpr) : aexpr =
    match a with
    | CstI c -> CstI c
    | Var s -> Var s
    | Add (CstI 0, aR) -> simplify aR
    | Add (aL, CstI 0) -> simplify aL
    | Sub (aL, CstI 0) -> simplify aL
    | Mul (CstI 1, aR) -> simplify aR
    | Mul (aL, CstI 1) -> simplify aL
    | Mul (CstI 0, _) -> CstI 0
    | Mul (_, CstI 0) -> CstI 0
    | Sub (aL, aR) when aL = aR -> CstI 0
    | Add (aL, aR) ->
        match simplify aL, simplify aR with
        | CstI x, CstI y -> CstI (x + y)
        | aL', CstI y -> Add (aL', CstI y)
        | CstI x, aR' -> Add (CstI x, aR')
        | aL', aR' -> Add (aL', aR')
    | Sub (aL, aR) ->
        match simplify aL, simplify aR with
        | CstI x, CstI y -> CstI (x - y)
        | aL', CstI y -> Sub (aL', CstI y)
        | CstI x, aR' -> Sub (CstI x, aR')
        | aL', aR' -> Sub (aL', aR')
    | Mul (aL, aR) ->
        match simplify aL, simplify aR with
        | CstI x, CstI y -> CstI (x * y)
        | aL', CstI y -> Mul (aL', CstI y)
        | CstI x, aR' -> Mul (CstI x, aR')
        | aL', aR' -> Mul (aL', aR')
    | _ -> a

(* Exercise 1.2.5 *)

let rec diff (a:aexpr) (v:string) : aexpr =
    match a with
    | CstI _ -> CstI 0
    | Var s when s = v -> CstI 1
    | Var _ -> CstI 0
    | Mul (a1, a2) -> Add (Mul (diff a1 v, a2), Mul (diff a2 v, a1))
    | Sub (a1, a2) -> Sub (diff a1 v, diff a2 v)
    | Add (a1, a2) -> Add (diff a1 v, diff a2 v)

let diff1 = Add(Mul(Var "x", CstI 2), CstI 3);;
let diff2 = diff diff1 "x";;
let diff3 = simplify diff2;;
