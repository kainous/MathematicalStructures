module Algebraic

open Morphisms
open Relations
open System

type Function<'Domain, 'Codomain> =
  abstract Evaluate : 'Domain -> 'Codomain
type Function<'Domain, 'Codomain>('Domain -> 'Codomain) =



type Function<'Domain, 'Codomain> = Function of ('Domain -> 'Codomain)
with
  static member ( >> ) (Function f, Function g) = Function (f >> g)

type Function<'T1, 'T2, 'TOut> = Function<'T1, Function<'T2, 'TOut>>
type Function<'T1, 'T2, 'T3, 'TOut> = Function<'T1, Function<'T2, 'T3, 'TOut>>

let Function3 (h : 'a -> 'b -> 'c) = Function(fun a -> Function(fun b -> h a b))


//type Function<'Domain, 'Codomain>(func:'Domain -> 'Codomain) =
//  //interface IMorphism<'Domain, 'Codomain> with
//  //  member this.Target = 
//  member this.Forward source =
//    func source

//  member this.ComposeWith(func:Function<'Codomain, 'Final>) =
//    Function(this.Forward >> func.Forward)

  //IFunctionalRelation proofs



type UnaryOperator<'T>(func : 'T -> 'T) =
  inherit Function<'T, 'T>(func)
  member this.ComposeWith(op:UnaryOperator<'T>) =
    UnaryOperator(func >> op.Forward)

  static member Compose (f:UnaryOperator<'T>) (g:UnaryOperator<'T>) =
    f.ComposeWith g

  interface IMonoid<UnaryOperator<'T>> with    
    member this.IdentityElement = 
      UnaryOperator(id)
    member this.Associativity =
      Relation(fun _ _ -> true)
    member this.Cospan = 
      BinaryOperator(UnaryOperator<_>.Compose)



and BinaryOperator<'T>(func : 'T -> 'T -> 'T) =
  inherit Function<'T, Function<'T, 'T>>(fun x -> Function(fun y -> func x y))

and IMagma<'T> =
  abstract Cospan : BinaryOperator<'T>

and ISemigroup<'T> =
  inherit IMagma<'T>
  abstract Associativity : Relation<'T * 'T * 'T, 'T>

and IMonoid<'T> =
  inherit ISemigroup<'T>
  abstract IdentityElement:'T

type InvertibleFunction<'Domain, 'Codomain>(forward:'Domain -> 'Codomain, inverse:'Codomain -> 'Domain) =
  inherit Function<'Domain, 'Codomain>(forward)
  member this.Inverse target =
    inverse target

 type UnaryOperatorMonoid<'T>() =
  interface IMonoid<UnaryOperator<'T>> with
    member this.IdentityElement = 
      UnaryOperator(id)
    member this.Associativity =
      Relation(fun _ _ -> true)
    member this.Cospan = 
      BinaryOperator(UnaryOperator<_>.Compose)

type InvertibleOperation<'T>(forward, inverse) =
  inherit InvertibleFunction<'T, 'T>(forward, inverse)

type Transformation<'TParameter, 'TTarget>(func : 'T)

type IGroup<'T> =
  inherit IMonoid<'T>
  abstract InverseElement : 'T -> 'T

type ICommutativeMagma<'T> =
  inherit IMagma<'T>
  abstract Commutativity : Relation<'T * 'T, 'T>

type ICommutativeSemigroup<'T> =
  inherit ISemigroup<'T>
  inherit ICommutativeMagma<'T>

type ICommutativeMonoid<'T> =
  inherit IMonoid<'T>
  inherit ICommutativeSemigroup<'T>



type VAffine<'T> =
  abstract Diff : VAffine<'T> -> VAffine<'T> -> Vector<'T>

and Vector<'T> =
  abstract Add : Vector<'T> -> Vector<'T> -> Vector<'T>
  abstract Mul : 'T -> Vector<'T> -> Vector<'T>
