module AbstractSyntaxTree

type TypeError() =
  new(text:string) = TypeError()

type Var = V of name:string * index:int
type Const =
| Star
| Box

let axiom = function
| Box -> Error(TypeError())
| Star -> Ok()

let rule a b = b

[<CustomEquality; NoComparison>]
type Expr<'T> =
| Const of Const
| Var of Var
| Lambda of varName:string * ty:Expr<'T> * expr:Expr<'T>
| Forall of varName:string * ty:Expr<'T> * expr:Expr<'T>
| Apply  of comb:Expr<'T> * arg:Expr<'T>
| Embed  of 'T
with
  member this.Equals(other:Expr<'T>) =
    let rec go = function
    | Const a, Const b -> a.Equals b
    | Var(V(a, b)), Var(V(c, d)) -> a = c && b = d
    | Lambda(a, b, c), Lambda(d, e, f) -> false
    | _ -> false

    go(this, other)

  override this.Equals(other:obj) =
    this.Equals(other :?> Expr<'T>)

  override this.GetHashCode() =
    let rec go hash = function
    | Const Star -> hash * 67 + 1
    | Const Box  -> hash * 67 + 2
    | Var(V(n, i)) -> (hash * 67 + i) * 67 + n.GetHashCode()
    | Lambda(_) -> 3
    | _ -> 4

    go 197 this

  override this.ToString() =
    let rec go = function
    | Const(Star)       -> "*"
    | Const(Box)        -> "□"
    | Embed a           -> sprintf "%A" a
    | Var(V(a, 0))      -> a
    | Var(V(a, b))      -> sprintf "%s@%d" a b
    | Lambda(a, b, c)   -> sprintf "λ(%s:%s) → %s" a (go b) (go c)
    | Forall("_", b, c) -> sprintf "%s → %s" (go b) (go c)
    | Forall(a, b, c)   -> sprintf "∀(%s:%s) → %s" a (go b) (go c)
    | Apply(a, b)       -> sprintf "%s %s" (go a) (go b)
   
    go this

//| Linear

let rec bind binder = function
| Const c         -> Const c
| Var v           -> Var v
| Lambda(x, a, b) -> Lambda(x, bind binder a, bind binder b)
| Forall(x, a, b) -> Forall(x, bind binder a, bind binder b)
| Apply(f, a)     -> Apply(bind binder f, bind binder a)
| Embed r         -> binder r


let (>>=) m k = bind k m
let map f xs = xs |> bind (f >> Embed)

//let rec Match = function
//| xL, nL, xR, nR, [] -> (xL = xR) && (nL = nR)
//| xL, 0,  xR, 0,  ((xL', xR')::_) when xL = xL' && xR = xR' -> true
//| xL, nL, xR, nR, ((xL', xR')::xs) 

type ExprBuilder() =
  member __.Return v = Embed v
  member __.Bind(x, f) = bind f x

  //static member (<|>)


let typeCheck (context:Map<int * string, Expr<unit>>) = function // (context:Expr<unit>) : Result<Expr<unit>, TypeError> = function
| Const a -> Ok <| (Result.map (fun _ -> Const a) (axiom a))
| Var(V(n, i)) ->
  match context.TryGetValue((i, n)) with
  | false, _ -> Error(TypeError("Unbounded Variable"))
  | _, a -> Ok a
  //as c -> 
  //let s = map Const (axiom Star)
  //Ok s

//let rec subst (x) (a) (e1) = function
//| Lambda(x', a', b') ->
//  let n' = if x = x' then n + 1 else n
//  let b' = subst x n' (shift 1 x' e') b
//  Lambda(x', (subst x n e' a'), b')