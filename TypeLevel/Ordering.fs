namespace TypeSafe

module Ordering =
  type Order = interface end
  
  type Apart = inherit Order
  type GreaterThan = inherit Apart
  type LessThan = inherit Apart

  type Absurdity<'T> = Absurdity of 'T

  type Equal = Absurdity<Apart>
  type GreaterThanOrEqual = Absurdity<LessThan>
  type LessThanOrEqual = Absurdity<GreaterThan>
  