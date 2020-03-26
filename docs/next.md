# What's next
---

Here is an overview of what we want Og to look like in the future

Don't mind the syntactic coloration that is not suited for non-existant features

```og
!main

import fmt

// Generics
struct Generic<T>
  // attributes
  pub test T

  // Instance method
  fn : T -> @test

  // Class method with external type declaration
  $ T -> Generic<T>
  pub @new(v) -> Generic<T>{ v }


// External grouped method definition
Generic<T>::(
  // With '*', tells the receiver (this or @) is a pointer to Generic<T>
  *method1 : int -> 1

  // External type declaration with no return
  $ int -> SomeComplexType -> (AnotherOne -> interface{}) -> ()
  method2(a, b, c) -> 2
)

// Returnable statements, nil existence test, predicat recapture (that)
genericFunc<T>(g Generic<T>): T -> if g.fn()? => that
                                   else       => somethingElse

// External type declaration
$ Generic<T> -> T
genericFunc<T>(g) -> g.test

// Multiple return values
$ int -> (int, string)
multipleReturn(i) -> i, "foo"

// Automatic "it" argument when not specified
$ string -> string
someFunc -> it + "Hello"

// No arguments, multiple return values, error bubbling
# (error, SomeType)
failableFunc ->
  res1 := funcsThatMayFail()?
  res2 := funcsThatFail()?

  res1 + res2

// Macro definition (like rust)
$macro my_macro ->
  ($number:expr) =>
    myFunc$(number) : int -> $number
    if $number > 0
      $my_macro($number - 1)

// Generate 10 function `myFunc10(), myFunc9(), .. myFunc0()` that all return their number,
$my_macro(10)

// Operator definition with precedence
operator ~~ 9

// Typeclass (could be implemented with macros ? lets see...)
impl SomeTypeClass for MyType
  x ~~ y = x.DoSomething(y)

main ->
  t := Generic<string>::new("str")

  // Range array creation, call chaining,
  // function currying and function shorthand.
  // Here a == [10, 11, 12, 13, 14, 15]
  a := []int{0..10}
    |> map((+ 10))
    |> filter((<= 15))

  // Function composition
  f := map >> filter

```