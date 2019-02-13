namespace TypeSafe

//module Semigroups =
//  type Semigroup = interface end

//  type Relation<'Source, 'Target> = Relation of func:('Source -> 'Target -> bool) with
//    interface Semigroup
//    static member inline ( *>> ) (Relation f, Relation g) =
//      Relation <| fun x y ->
//        if f x 