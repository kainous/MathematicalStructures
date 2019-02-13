namespace ApplicativeParsers

open System
open FSharpx.Collections
open FSharpx.Collections.LazyList

type Position = uint64

type ParserErrorCodes =
| UnexpectedEndOfStream = 0
| ExpectedEndOfStream = 1

type GoodValue<'T> = {
  StartPosition : Position
  EndPosition   : Position
  Value         : 'T
}

type BadValue<'T> = {
  StartPosition : Position
  EndPosition   : Position
  Code          : 'T
}

type ItemStream<'T> = LazyList<Position * 'T>

type Parser<'TValue, 'TStream, 'TError> = private P of (ItemStream<'TStream> -> Result<List<GoodValue<'TValue>>, List<BadValue<'TError>>>)

module Primitives =
  let run (P parser) (stream:ItemStream<'TStream>) =
    parser stream

  let eof (P parser) =
    P <| function
         | Nil -> Ok  <| [{ StartPosition = 0uL; EndPosition = 0uL; Value = ()}]
         | _ -> Error <| [{ StartPosition = 0uL; EndPosition = 0uL; Code = ParserErrorCodes.ExpectedEndOfStream }]

  let satisfy (P parser) (predicate:'TStream -> bool) =
    P <| function
         | Nil ->
           Error <| [{ StartPosition = 0uL; EndPosition = 0uL; Code = ParserErrorCodes.UnexpectedEndOfStream }]
         | Cons((pos, h), tail) when predicate h ->
           Ok <| ({ Value = h; StartPosition = pos; EndPosition = pos }::tail
         | _ ->