type expr =
  | CstI of int
  | Var of string
  | Let of (string * expr) list * expr
  // | Let of string * expr * expr
  | Prim of string * expr * expr

let rec lookup env x =
    match env with
    | []        -> failwith (x + " not found")
    | (y, v)::r -> if x=y then v else lookup r x;;

let rec eval e (env : (string * int) list) : int =
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x
    | Let(binds, ebody) ->
        List.fold (fun (env':(string*int) list) x -> (fst x, eval (snd x) env')::env') env binds
        |> eval ebody
    | Prim("+", e1, e2) -> eval e1 env + eval e2 env
    | Prim("*", e1, e2) -> eval e1 env * eval e2 env
    | Prim("-", e1, e2) -> eval e1 env - eval e2 env
    | Prim _            -> failwith "unknown primitive";;

let e1 = Let([("x", CstI 1); ("y", CstI 3)], Prim("+", Var "x", Var "y"))
let e2 = Let(["z", CstI 17], Prim("+", Let(["z", CstI 22], Prim("*", CstI 100, Var "z")), Var "z"))

