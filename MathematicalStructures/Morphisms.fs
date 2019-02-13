module Morphisms

type Proposition = bool

type Predicate<'T> = 'T -> Proposition


type IMorphism<'Source, 'Target> = 
  abstract Source:'Source
  abstract Target:'Target

//type IMonomor
//type IIso

type IEndomorphism<'T> = IMorphism<'T, 'T>

type Comparison =
| Greater
| Less
| Equal

type IPartialOrder<'T> =
  abstract Compare : IFunction<'T, IFunction<'T, Comparison option>>
