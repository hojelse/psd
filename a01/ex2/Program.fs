type expr =
  | CstI of int
  | Var of string
  | Let of (string * expr) list * expr
  // | Let of string * expr * expr
  | Prim of string * expr * expr

let rec lookup env x =
    match env with
    | []        -> failwith (x + " not found")
    | (y, v)::r -> if x=y then v else lookup r x

(* Exercise 2.1 *)

let rec eval e (env : (string * int) list) : int =
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x
    | Let(binds, ebody) ->
        // aux binds env ebody
        List.fold (fun env' (v, ex) -> (v, eval ex env')::env') env binds
        |> eval ebody
    | Prim("+", e1, e2) -> eval e1 env + eval e2 env
    | Prim("*", e1, e2) -> eval e1 env * eval e2 env
    | Prim("-", e1, e2) -> eval e1 env - eval e2 env
    | Prim _            -> failwith "unknown primitive"

// and aux (list: (string * expr) list) env ebody =
//     match list with
//         | [] -> eval ebody env
//         | x::xs ->
//             let xval = eval (snd x) env
//             let env1 = ((fst x), xval) :: env
//             aux xs env1 ebody

let e1 = Let([("x", CstI 1); ("y", CstI 3)], Prim("+", Var "x", Var "y"))
let e2 = Let(["z", CstI 17], Prim("+", Let(["z", CstI 22], Prim("*", CstI 100, Var "z")), Var "z"))

(* Exercise 2.2 *)

let rec mem x vs =
    match vs with
    | []      -> false
    | v :: vr -> x=v || mem x vr

let rec union (xs, ys) =
    match xs with
    | []    -> ys
    | x::xr -> if mem x ys then union(xr, ys)
               else x :: union(xr, ys)

let rec minus (xs, ys) =
    match xs with
    | []    -> []
    | x::xr -> if mem x ys then minus(xr, ys)
               else x :: minus (xr, ys)

let rec containsVar (e:expr) (v:string) : bool =
    match e with
    | Var x when x=v -> true
    | Let(binds, ebody) ->
        List.map fst binds
        |> List.fold(fun acc x -> (x=v)||acc) false
    | Prim(ep, e1, e2) -> containsVar e1 v || containsVar e2 v
    | _ -> false

let rec freevars e : string list =
    match e with
    | CstI i -> []
    | Var x  -> [x]
    | Let(binds, ebody) ->
        List.fold (fun acc (v, e) -> (if containsVar e v then union([v], acc) else acc)) [] binds
    | Prim(ope, e1, e2) -> union (freevars e1, freevars e2)

let e_free = Let([("x1", Prim("+", Var("x1"), CstI 7)); ("x2", CstI 1)], Prim("+", Var("x1"), CstI 8))

(* Exercise 2.3 *)

type texpr =                            (* target expressions *)
  | TCstI of int
  | TVar of int                         (* index into runtime environment *)
  | TLet of texpr * texpr               (* erhs and ebody                 *)
  | TPrim of string * texpr * texpr

let rec getindex vs x =
    match vs with
    | []    -> failwith "Variable not found"
    | y::yr -> if x=y then 0 else 1 + getindex yr x

let rec tcomp (e : expr) (cenv : string list) : texpr =
    match e with
    | CstI i -> TCstI i
    | Var x  -> TVar (getindex cenv x)
    | Let(binds, ebody) ->
        match binds with
        | [] -> tcomp ebody cenv
        | (v, e)::bindsr ->
            let newLet = Let(bindsr, ebody)
            TLet(tcomp e cenv, tcomp newLet (v::cenv))
    | Prim(ope, e1, e2) -> TPrim(ope, tcomp e1 cenv, tcomp e2 cenv)

// let x = 10
// let y = 1 + x
// x + y

            // x = 10;         y = 1 + x;                          x + y
let eT = Let([("x", CstI 10); ("y", Prim("+", CstI 1, Var("x")))], Prim("+", Var("x"), Var("y")))

// TLet(TCstI 10, Let([("y", Prim("+", CstI 1, Var x)], Prim("+", Var("x"), Var("y"))))
// TLet(TCstI 10, TLet(1 + x, Let([], Prim("+", Var("x"), Var("y")))))
// TLet(TCstI 10, TLet(1 + TVar 0, Let([], Prim("+", Var("x"), Var("y")))))
// TLet(TCstI 10, TLet(1 + TVar 0, TPrim (Var "x", Var "y")))
// TLet(TCstI 10, TLet(1 + TVar 0, TPrim (TVar 0 + TVar 1)))

let eTcomped = TLet(TCstI 10, TLet (TPrim ("+", TCstI 1, TVar 0), TPrim ("+", TVar 1, TVar 0)))

