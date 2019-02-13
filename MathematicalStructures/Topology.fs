module Topology


// Dual to create interior
type IClosure<'T> =
  abstract Enclose : 'T -> 'T


let Interior = 
  { new IClosure}