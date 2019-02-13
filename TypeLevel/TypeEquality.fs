namespace TypeLevel

module TypeEquality =
  open Functional
  open Bool

  type Refl<'T> = TypeEq of 'T * 'T with
    interface Func
    static member inline ( <-- ) (TypeEq(a, b), ()) = True

  let inline isEq a b =
    TypeEq(a, b) |> ignore
    True