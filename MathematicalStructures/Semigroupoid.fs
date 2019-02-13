module Semigroupoid

type Semigroupoid<'T, 'Source, 'Target> = Semigroupoid of ('Source -> 'T) * ('Target -> 'T) with
  static member inline Compose()