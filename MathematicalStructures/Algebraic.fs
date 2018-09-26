module Algebraic

open Morphisms
open Relations

type IFunction<'Domain, 'Codomain> =
  inherit IMorphism<'Domain, 'Codomain>
  //IFunctionalRelation proofs

type IBinaryFunction<'T1, 'T2, 'TOut> =
  inherit IFunction<'T1, IFunction<'T2, 'TOut>>

type IUnaryOperator<'T> =
  inherit IFunction<'T, 'T>
  interface IMonoid<IUnaryOperator<'T>> with
    member this.Cospan = compose
    member this.IdentityElement = id

and IBinaryOperator<'T> =
  inherit IFunction<'T, IFunction<'T, 'T>>

and IMagma<'T> =
  abstract Cospan : IBinaryOperator<'T>

and ISemigroup<'T> =
  abstract Associativity : Relation<'T * 'T * 'T, 'T>

and IMonoid<'T> =
  inherit ISemigroup<'T>
  abstract IdentityElement:'T

type ICommutativeMagma<'T> =
  inherit IMagma<'T>
  abstract Commutativity : Relation<'T * 'T, 'T>

type ICommutativeSemigroup<'T> =
  inherit ISemigroup<'T>
  inherit ICommutativeMagma<'T>

type ICommutativeMonoid<'T> =
  inherit ICommutativeSemigroup<'T>
  inherit IMonoid<'T>


type Proposition = bool

type Predicate<'T> = 'T -> Proposition
