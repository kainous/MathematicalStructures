module Relations

type RelationPredicate<'L, 'R> = ('L -> 'R -> bool)

//Use relation algebra where possible
type Relation<'L, 'R>(predicate:RelationPredicate<'L,'R>) =
  member __.Predicate = 
    predicate

//  member __.IsSubRelationOf(r : Relation<'L, 'R>) (elements:seq<'L*'R>) =
//    elements |> Seq.forall(fun (a, b) -> (predicate a b) <= (r.Predicate a b))

  member __.Negate() =
    let negation a b = predicate a b |> not
    Relation(negation)

//  static member ( <<== ) (a:Relation<'L, 'R>, b) =
//    fun elements -> a.IsSubRelationOf b elements

//  // Composition
//  static member ( >>> ) (a:Relation<'A,'B>, b:Relation<'B,'C>) =
//    Relation(fun x y -> (a.Predicate x y) && (b.Predicate)

  static member ( &&& ) (a, b) = a


//  static member ( ||| ) (a, b) = a

  static member ( ~~~ ) (r:Relation<_,_>) = 
    r.Negate()

  static member Empty    = Relation(fun _ _ -> false)
  static member Complete = Relation(fun _ _ -> true)


type Endorelation<'T>(predicate:RelationPredicate<'T, 'T>) =
  inherit Relation<'T, 'T>(predicate)
  static member Identity = Endorelation(=)
  //static member 

type EqualityRelation<'T>(predicate:RelationPredicate<'T,'T>) =
  inherit Relation<'T,'T>(predicate)