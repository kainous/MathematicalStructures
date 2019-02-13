module Functional

open System

  type Function<'Domain, 'Codomain>(func:'Domain -> 'Codomain) =
    member __.Evaluate (parameter:'Domain) : 'Codomain =
      func parameter

  type Function<'T1, 'T2, 'TOut>(func: Function<'T1, Function<'T2, 'TOut>>) =
    member __.Evaluate (parameter:'T1) : (Function<'T2, 'TOut>) =
      func.Evaluate parameter

    new(func : 'T1 -> 'T2 -> 'TOut) =
      Function<'T1, 'T2, 'TOut>(Function<_,_>(fun x -> Function<_,_>(fun y -> func x y)))

    new(func : 'T1 * 'T2 -> 'TOut) =
      Function<'T1, 'T2, 'TOut>(fun x y -> func(x, y))

  type Function<'T1, 'T2, 'T3, 'TOut>(func : Function<'T1, Function<'T2, Function<'T3, 'TOut>>>) =
    member __.Evaluate (parameter:'T1) : Function<'T2, Function<'T3, 'TOut>> =
      func.Evaluate parameter

    new (func : 'T1 -> 'T2 -> 'T3 -> 'TOut) =
      Function<'T1, 'T2, 'T3, 'TOut>(Function<_,_>(fun x -> Function<_,_>(fun y -> Function<_,_>(fun z -> func x y z))))
    
    new (func : 'T1 * 'T2 * 'T3 -> 'TOut) =
      Function<'T1, 'T2, 'T3, 'TOut>(fun x y z -> func(x, y, z))

  type Operation<'T> = Function<'T, 'T>

  type Magma<'T> = Function<'T, 'T, 'T>

  type Semigroup<'T> = 
    abstract Function : Magma<'T>
    abstract Associativity : Function<'T, 'T, 'T, bool>

  type 

  type Monoid<'T> = Function<'T, 'T, 'T>

  type Function<'Domain, 'Codomain> with
    member __.Compose (f:Function<'A, 'B>) (g:Function<'B, 'C>) : Function<'A, 'C> =
      Function<_,_>(f.Evaluate >> g.Evaluate)

    member __.ComposeMonoid() = 
      Monoid(fun (f:Function<'T, 'T>) (g:Function<'T, 'T>) -> Function<'T, 'T>(f.Evaluate >> g.Evaluate))