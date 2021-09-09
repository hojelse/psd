#r "nuget: FsLexYacc"

let rec dfa (str:string) =
    let rec aux =
        function
        | (0, 'a'::rest) -> aux (0, rest)
        | (0, 'b'::rest) -> aux (1, rest)
        | (0, []) -> false
        | (1, []) -> true
        | _ -> false
    let charList = List.ofArray (str.ToCharArray())
    aux (0, charList)

let c1 = "aaaaab"
let c2 = "b"
let f1 = "aba"
let f2 = "a"