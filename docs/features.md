# Features
---

- Indentation based
- Transpile to `Golang`
- No brackets (almost)
- Clean and nice syntax
- Auto return the last statement in a block
- Returnable/Assignable statements (`if` only for the moment)
- Inline method declaration in `struct`
- One-liners
- Syntaxic sugars
- Bootstraped language
- Antlr4 parser
- Multithreaded compilation
- CLI tool to parse and debug your files
- Templates (C++ style, at compile time. ALPHA)
- Interpreter (ALPHA)

# Overview
---

# Global

```og
!main // package shorthand

// No parenthesis and single word can omit quotes
import
  fmt
  strings
  "some/repo"

// Alias for `type Foo struct`
struct Foo
  // Classical property declaration
  bar int

  // Inline method declaration that returns `this.bar`
  getBar : int -> @bar

  // Pointer type for receiver
  *setBar(i int) -> @bar = i

// External Foo method declaration
Foo::inc(foo int): int -> @bar + foo

// Pointer type for receiver
Foo::*setInc(foo int) -> @bar = @bar + foo

// Alias for `type Bar interface`
interface Bar
  Foo()
  Bar(i int): SomeType

// Alias for struct
class Test

// Classical top-level function declaration
// Automatic return for last statement of a block
otherFunc(a string): string -> a

autoIfReturn: int ->
  // Auto return for if statement
  if true => 1
  else    => 0

// Template definition (ALPHA)
genericFunction<T>(arg T): T -> arg

main ->
  test := Foo{}
  test.inc(42)

  // Template call for int
  genericFunction<int>(42)

  // Conditional assignation
  var a int = 
    if true => 1
    else    => 0

  // No brackets
  someArr := []string
    "value1"
    "value2"

  // Classical `for in` form
  for i, v in someArray
    fmt.Println(i, v)

  switch test.getBar()
    42 => doSomething()
    _  => doDefault()

  // Function literal with the keywork `fn`
  callback := fn (arg int): int -> arg + 1

  // Auto executed closure when in goroutines
  // No need to add the extra `()`
  go someFunc()
  go -> doSomething()
```

Compiles to:

```go
package main

import (
	"fmt"
	"some/repo"
	"strings"
)

type Foo struct {
	bar int
}

func (this Foo) getBar() int {
	return this.bar
}
func (this *Foo) setBar(i int) {
	this.bar = i
}
func (this Foo) inc(foo int) int {
	return this.bar + foo
}
func (this *Foo) setInc(foo int) {
	this.bar = this.bar + foo
}

type Bar interface {
	Foo()
	Bar(i int) SomeType
}
type Test struct {
}

func otherFunc(a string) string {
	return a
}
func autoIfReturn() int {
	if true {
		return 1
	} else {
		return 0
	}
}
func main() {
	test := Foo{}
	test.inc(42)
	genericFunctionint(42)
	var (
		a int = func() int {
			if true {
				return 1
			} else {
				return 0
			}
		}()
	)
	someArr := []string{
		"value1",
		"value2",
	}
	for i, v := range someArray {
		fmt.Println(i, v)
	}
	switch test.getBar() {
	case 42:
		doSomething()
	default:
		doDefault()
	}
	callback := func(arg int) int {
		return arg + 1
	}
	go someFunc()
	go func() {
		doSomething()
	}()
}
func genericFunctionint(arg int) int {
	return arg
}
```

# Details

## Package

#### Og

```og
!main
// Or
package main
```

#### Go

```go
package main
```

## Import

#### Og

```og
// Single import
import fmt

// Multiple imports
import 
  fmt
  "some/repo"

// Leave the parenthesis if you want
import (
  fmt
)

// Import rename
import
  "some/repo": newName
```

#### Go

```go
import "fmt"

import (
  "fmt"
  "some/repo"
)

import (
  "fmt"
)

import (
  newName "some/repo"
)
```

## Top Level Function

#### Og

```og
// Empty online function
f -> ;

// Return type
g: int -> 2

// Arguments and return
h(a int): int -> a

// Multiline
multi(a, b int): int, bool ->
  c := a + b
  
  return c, false // return needed when multiple return values
```

#### Go

```go
func f() {
}

func g() int {
  return 2
}

func h(a int) int {
  return a
}

func multi(a, b int) (int, bool) {
   c := a + b
  return c + 42, false
}
```

## Function Literal

#### Og

```og
// `fn` keyword to denote the function type
struct Foo
  Bar fn(int): string

// No need of the `fn` in interface
interface Bar
  Foo(string): error

f(callback fn(int): error): error -> callback(0)

var g = fn(a int): error -> nil
```

#### Go

```go
struct Foo {
  Bar func (int) string
}

interface Bar {
  Foo(string) error
}

func f(callback func (int) error) error {
  return callback(0)
} 

var g = func (a int) error {
  return nil
}
```

## If / Else

#### Og

```og
// Classical
if foo == bar
  foo.Do()
else if foo == other
  bar.Do()
else
  nothing()

// Compressed
if foo == bar => foo.Do()
else          => bar.Do()

// One line
if foo == bar => 1 ; else => 0

//Assignement, type is mandatory here
var a int = 
  if foo == bar => 1
  else          => 0

// Auto return when last statement in function block
f: int ->
  if foo == bar => 1
  else          => 0
```


#### Go

```go
if foo == bar {
  foo.Do()
} else if foo == other{
  bar.Do()
} else {
  nothing()
}

if foo == bar {
  foo.Do()
} else {
  bar.Do()
}

if foo == bar {
  1
} else {
  0
}

var (
  a int = func() int {
    if foo == bar {
      return 1
    } else {
      return 0
    }
  }()
)

func f() int {
  if foo == bar {
    return 1
  } else {
    return 0
  }
}
```

## For

#### Og

```og
// Infinite loop
for
  do()

// Classical
for i < 10
  do()

// Range
for _, v in array
  do(v)

// Maniac style
for i := 0; i < 10; i++
  do(i)
```

#### Go

```go
for {
  do()
}

for i < 10 {
  do()
}

for _, v := range array {
  do(v)
}

for i := 0; i < 10; i++ {
  do(i)
}
```

## Goroutine

#### Og

```og
// classical
go some.Func()

// With auto-executed closure
go -> some.Func()
```

#### Go

```go
go some.Func()

go func() {
  some.Func()
}()
```

## Slice

#### Og

```og
a := []string
  "a"
  "b"
  "c"
```

#### Go

```go
a := []string{
  "a",
  "b",
  "c",
}
```

## Switch/Case

#### Og

```og
// Switch case with default
switch value
  42 => do()
  _  => doDefault()

// Type switch
switch value.(type)
  int => do()
  _   => doDefault()
```

#### Go

```go
switch value {
case 42:
  do()
default:
  doDefault()
}

switch value.(type) {
case int:
  do()
default:
  doDefault()
}
```

## Select

#### Og

```og
select
  <-c      => do()
  x := <-c => doWith(x)
  c2 <- 1  => hasSend()
  _        => doDefault()
```

#### Go

```go
select {
case <-c1:
  do()
case x := <-c2:
  doWith(x)
case c2 <- 1:
  hasSend()
default:
  doDefault()
}
```

## Struct

#### Og

```og
// Struct type with internal method
struct Foo
  Bar int
  GetBar: int    -> @Bar
  *SetBar(v int) -> @Bar = v

// Class is an alias for `struct`
class MyClass
  myValue int `json:"-"`

// External method declaration
MyClass::*MyMethod: int -> @myValue

// Instantiation
NewMyMethod: *MyMethod ->
  &MyMethod
    myValue: 0
```

#### Go

```go
type Foo struct {
  Bar int
}

func (this Foo) GetBar() int {
  return this.Bar
}

func (this *Foo) SetBar(v int) {
  this.Bar = v
}

type MyClass struct {
  myValue int `json:"-"`
}

func (this *MyClass) MyMethod() int {
  return this.myValue
}

func NewMyMethod() *MyMethod {
  return &MyMethod{
    myValue: 0,
  }
}
```

## Interface

#### Og

```og
// Empty interface
interface Foo

// Method decl
interface Bar
  *CompositeField
  Method(Type): ReturnType
```

#### Go

```go
type Foo interface{}

type Bar interface {
  Method(Type) ReturnType
}
```
## Template

Templates can be defined across packages, their definition is kept in a hidden file in order to avoir reparsing the whole package when needed

#### Og

```og
struct Foo<T>
  bar T

genericFunction<T>(arg T): T -> arg

main ->
  a := Foo<int>
    bar: 1

  genericFunction<int>(a)
  genericFunction<string>(a)
```

#### Go

```go
type example_Foo_int struct {
  bar int
}

func example_genericFunction_int(arg int) int {
  return arg
}

func example_genericFunction_string(arg string) string {
  return arg
}

func main() {
  a := example_Foo_int{
    bar: 1,
  }
  example_genericFunction_int(a)
  example_genericFunction_string(a)
} 
```

## Label

#### Og

```og
main ->
  ~here:
  goto here
```

#### Go

```go
func main() {
here:
  goto here
}
```
