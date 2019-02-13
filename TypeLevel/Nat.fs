namespace TypeSafe

open TypeLevel
open Bool

module Nat =
  open TypeLevel
  open TypeLevel.TypeEquality

  type Nat = interface end

  type Zero = Z with
    interface Nat
    static member inline (<<=) (Z, S a) = S a
    static member inline (<<=) (Z, Z) = Z
    static member inline (===) (Z, Z) = True
    static member inline (===) (Z, S _) = False
    static member inline (!++) (Z) = (S Z)
    static member inline ( + ) (Z, S a) = S a
    static member inline ( + ) (Z, Z) = S Z
    static member inline ( * ) (Z, _) = Z
    static member inline ( *** ) (Z, S _) = Z // Note that we cannot do Z ** Z
    static member inline ( *** ) (S x, Z) = S x // Note that we cannot do Z ** Z
    static member inline (!!!) Z = S Z
    static member inline ( ~~~ ) Z = S Z
    static member inline ( - ) (x, Z) = x
    static member inline (<&>) (S Z, y) = y
    static member inline (<&>) (x : #Nat, y : #Nat) = TypeEq(x, y) |> ignore; x
    //static member inline (<&>) (x, y) =
      
    
  and Succ<'T when 'T :> Nat > = S of 'T with
    interface Nat
    static member inline (<<=) (S _, Z) = False
    static member inline (<<=) (S a, S b) = a <<= b
    static member inline (===) (S a, S b) = a === b
    static member inline (===) (S _, Z) = False
    static member inline (!++) (S a) = S (S a)
    static member inline ( + ) (S a, Z) = S(a + Z)
    static member inline ( + ) (S a, (S b)) = S(a + (S b))
    static member inline ( * ) (S a, b) = (a * b) + b
    static member inline ( *** ) (S x, y) = (x *** y) * y
    static member inline (!!!) (S a) = a * (S a)
    static member inline ( ~~~ ) (S Z) = S Z
    static member inline (!--) (S x) = x
    static member inline ( - ) (S x, S y) = x - y    
    static member inline (<&>) (S Z, y) = y
    static member inline (<&>) (x : #Nat, y : #Nat) = TypeEq(x, y) |> ignore; x
    //static member inline ( ~~~ ) (S (S x)) = ((~~~) (S x)) + (~~~ x)

    //static member inline ( ** ) ((S _) as a, S b) = b * (a ** b)
    //static member inline Pow ((S _) as a, S b) = b * (a ** b)


  type N0 = Zero
  type N1 = Succ<N0>
  type N2 = Succ<N1>
  type N3 = Succ<N2>
  type N4 = Succ<N3>

  let N0' = Z
  let N1' = S N0'
  let N2' = N1' + N1'
  let N3' = N1' + N2'
  //let N4' = N2' * N2'
  let N5' = N3' + N2'
  let N4' = N2' *** N2'

  let N6' = !!! N3'
  //let N9' = N1' *** N1'

  let test6 = N6' === N5'
  let test6' = (N6' === S N5')
  ///let test6'' = isEq N6' ( N5') // Fails to compile
  //let testneg = !-- N1'

  let test3 = isEq (N6' - N3') N3'

  //let c = TypeLevel.TypeEquality.isEq N3' (!++ N2')

//type Setoid =
