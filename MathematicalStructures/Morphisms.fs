module Morphisms

type IMorphism<'Source, 'Target> = 
  abstract Source:'Source
  abstract Target:'Target

//type IMonomor
//type IIso

type IEndomorphism<'T> = IMorphism<'T, 'T>

type IFunction<'Domain, 'Codomain> =
  inherit IMorphism<'Domain, 'Codomain>
  //IFunctionalRelation proofs

type IBinaryFunction<'T1, 'T2, 'TOut> =
  inherit IFunction<'T1, IFunction<'T2, 'TOut>>

type IUnaryOperation<'T> =
  inherit IFunction<'T, 'T>

type Proposition = bool

type Predicate<'T> = 'T -> Proposition

type Comparison =
| Greater
| Less
| Equal

type IPartialOrder<'T> =
  abstract Compare : IFunction<'T, IFunction<'T, Comparison option>>
