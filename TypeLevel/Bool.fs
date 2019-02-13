namespace TypeLevel

open System
open Functional

module Bool =
  type Bool = interface end
  
  type False = False with
    interface Bool
    static member inline (<?>) (False, (_, y)) = y

  type True = True with
    interface Bool
    static member inline (<?>) (True, (x, _)) = x

  type Not = Not with
    interface Func
    static member inline (<|-) (Not, True) = False
    static member inline (<|-) (Not, False) = True

  type Or = Or with
    static member inline (<|-) (Or, (True,  True))  = True
    static member inline (<|-) (Or, (False, True))  = True
    static member inline (<|-) (Or, (True,  False)) = True
    static member inline (<|-) (Or, (False, False)) = False

  type And = And with
    static member inline (<|-) (And, (True,  True))  = True
    static member inline (<|-) (And, (False, True))  = False
    static member inline (<|-) (And, (True,  False)) = False
    static member inline (<|-) (And, (False, False)) = False

  type Implies = Implies with
    static member inline (<|-) (Implies, (True,  True))  = True
    static member inline (<|-) (Implies, (False, True))  = True
    static member inline (<|-) (Implies, (True,  False)) = False
    static member inline (<|-) (Implies, (False, False)) = True

open Bool