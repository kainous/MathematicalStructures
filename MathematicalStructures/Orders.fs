module Orders

open Morphisms
open Relations

type Comparison =
| Greater
| Less
| Equal

type IPartialOrder<'T>() =
  inherit Relation<'T, 'T>(fun a b -> true)
  abstract Compare : IBinaryFunction<'T, 'T, Comparison option>
//with
//  Extension GetEqualityRelation()

type IMeetSemilattice<'T> =
  inherit IPartialOrder<'T>
  abstract Meet : ICommutativeSemigroup<'T>

type IJoinSemilattice<'T> =
  inherit IPartialOrder<'T>
  abstract Join : ICommutativeSemigroup<'T>

type IBoundedJoinSemilattic<'T> =
  inherit IJoinSemilattice<'T>
  abstract Join : ICommutativeMonoid<'T>





interface IMeetSemilattice<T> : IPartialOrder<T>
    {
        IAbelianSemigroup<T> Meet { get; }
    }

    interface IJoinSemilattice<T> : IPartialOrder<T>
    {
        IAbelianSemigroup<T> Join { get; }
    }

    interface IBoundedJoinSemilattice<T> : IJoinSemilattice<T>
    {
        new IAbelianMonoid<T> Join { get; }
    }

    interface IBoundedMeetSemilattice<T> : IMeetSemilattice<T>
    {
        new IAbelianMonoid<T> Meet { get; }
    }

    interface ILattice<T> : IMeetSemilattice<T>, IJoinSemilattice<T>
    {
    }

    interface IBoundedLattice<T> : IBoundedJoinSemilattice<T>, IBoundedMeetSemilattice<T> { }

    interface IResiduatedLattice<T>
    {
        ILattice<T> Lattice { get; }
        IMonoid<T> Monoid { get; }
        ITupleRelation<T, T, T> ResiduatedProof { get; }
    }

    interface IHeytingAlgebra<T>
    {

    }






  
    public static class IExtension
    {
        IOrderTopology CreateOrderTopology<T>(this IPartialOrder<T>)
    }

        class BinaryInt32
    {
        public IBooleanAlgebra<T> Divisibility { get; }
        public IBooleanAlgebra<T> ArithmeticOrder { get; } //Min and max
        public IBooleanAlgebra<T> BitwiseOrder { get; } //Partial order defined by which bits are set        
        public IExponentialField<T> Exponentiation { get; }
        public IMetriziableField<T> Metric { get; }
    }
}

