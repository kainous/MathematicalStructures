namespace TypeSafe

module Reflexivity =
  type Refl<'T> = Eq of 'T * 'T with
    interface Func