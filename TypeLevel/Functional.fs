namespace TypeLevel

module Functional =
  type Func = interface end

  type Id = Id with
    interface Func
    static member inline (<|-) (Id, x) = x

  type Fst = Fst with
    interface Func
    static member inline (<|-) (Fst, (x, _)) = x

  type Snd = Snd with
    interface Func
    static member inline (<|-) (Snd, (_, y)) = y

  