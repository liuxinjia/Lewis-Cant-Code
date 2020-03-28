
Data Types and Size

## Value Type

### Signed& Unsigned Integral Type



| TypeName     | C++       | C# (1 byte = 8 bits)    |
|--------------|-----------|-------------------------|
| char         | * 1  | 2 (2^16-1)            |
| short        | 2         | 2                     |
| unsigned int | 4         | 4 (0, 2^32 -1)        |
| int          | 4         | 4 (-2^31 + 1 , 2^31 -1) |
| long         | *4         | 8                       |
| ulong        |  8         | 8(0, 2^64 -1)         

<!-- tabs:start -->
#### ** C++ **
* Above values have been considerer on  64-bit windows system, and it may vary from compiler to your compiler in  the case of C++. For example, Win64 API use LLP64 which means int and long are32-bit, pointer is 64-bit), [https://en.cppreference.com/w/cpp/language/types](https://en.cppreference.com/w/cpp/language/types)
* Char is  also different. The signnedness of char depends on the compiler and the target platform:  -128 to 127 by default  0 to 255 when compiled by using [/J](https://docs.microsoft.com/en-us/cpp/build/reference/j-default-char-type-is-unsigned?view=vs-2019) in Microsoft compiler.
* More details are below. But C++ Standard guarantees that:

#### ** C# **
That's why use C#. 
<!-- tabs:end -->

``1  ==  sizeof(char)  <=  sizeof(short)  <=  sizeof(int)  <=  sizeof(long)  <=  sizeof(long  long).``

#### Literals:
|             | c++                                                       | C#                                   |
|-------------|-----------------------------------------------------------|--------------------------------------|
| decimal     |                                                           |                                      |
| hexadeciaml | with the  0x or  0X prefix                                | same                                 |
| binary      | with the  0b or  0B prefix (**available in C++14 and later**) | same (**available in C# 7.0 and later**) |

<!-- tabs:start -->
#### ** C# **
 ``` og
 
	var decimalLiteral = 42;
	var hexLiteral = 0x2A;
	var binaryLiteral = 0b_0010_1010;

```

#### ** C++ **
  ``` og
    auto decimalLiteral = 42;
    auto hexLiteral = 0x2A;
    auto binaryLiteral = 0b00101010;
  ```
<!-- tabs:end -->

  
  #### Speical value
  
<!-- tabs:start -->

 #### ** C# **
 e.g. int.MaxValue, int.MinValue

 #### ** C++ **
- You can get everything [here](https://en.cppreference.com/w/cpp/types/numeric_limits)

- But for me, INT_MAX and FIN_MAX is enough.

<!-- tabs:end -->

 
 #### Conversions
There are implict conversion and explicit conversin:

<!-- tabs:start -->

 #### ** C# **
 
- explict conversion for wide range to narrow range with the cast operator

- implicit conversion for narrow range to wide range, e.g. int to long.

 #### ** C++**
  It only throw a warning for you when converint from wide range to narrow. But don't do that it since it may cause overflow exception.

<!-- tabs:end -->


###  Floating-point numeric types
| FloatType | c++         | C#        | Precision     |
|-----------|-------------|-----------|---------------|
| float     | 4           | 4(Single) | ~6-9 digits   |
| double    | 8           | 8         | ~15-17 digits |
|           | *long double  | decimal   | 28-29 digits  |
> [!Note]
> long double is the same as double in window C++.

#### Special Value:


<!-- tabs:start -->

 #### ** C# **
 The float and double types are also provide constants that represent not-a number and infinity values. For example, the double provides the following constants: [Double.NaN](https://docs.microsoft.com/en-us/dotnet/api/system.double.nan), [Double.NegativeInfinity](https://docs.microsoft.com/en-us/dotnet/api/system.double.negativeinfinity), and [Double.PositiveInfinity](https://docs.microsoft.com/en-us/dotnet/api/system.double.positiveinfinity).

 #### ** C++**

- _infinity_ (positive and negative), see [INFINITY](https://en.cppreference.com/w/cpp/numeric/math/INFINITY  "cpp/numeric/math/INFINITY")
- NaN, not a number

- the positive zero :1.0/0.0 == [INFINITY]

- the negative zero, 1.0/ -0.0 == [-INFINITY].

 - [ ] It is meaningful in some arithmetic operations
<!-- tabs:end -->

 
### Character type
The C++ compiler treats  variables of type, signed char,and usigned char as having different types. In Microsoft C++, the default type is signed char, unless the/J compilation options is used. In this case, they are treated as type **unsigned  char**.
How about Unicode? How about wchar_t?
Read the belwo code first.


<iframe height="400px" width="100%" src="https://repl.it/repls/FamiliarEasyNagware?lite=true" scrolling="no" frameborder="no" allowtransparency="true" allowfullscreen="true" sandbox="allow-forms allow-pointer-lock allow-popups allow-same-origin allow-scripts allow-modals"></iframe>

If you are confused, go to paercebal's [answer]( https://stackoverflow.com/a/402918/7360004) about it.
If you are on the impatient side of the spectrum, you have my brief conclusion here:

 - The char type cane be used store characters form the ASCII character or the UTF-8 encoding of the Unicode characte set. And the wchar_t type represents a 16-bit wide type character used Unicode encoded as UTF-16LE, the native character type on WIndows operating systems.
 - But the problem is that neither char nor wchar_t is directly tied to unicode (you have seen from the above code).

 

>[!Note]
> Life is short ! Use othe language but not C++.



## Others

<iframe height="400px" width="100%" src="https://repl.it/repls/BlondSnarlingBucket?lite=true" scrolling="no" frameborder="no" allowtransparency="true" allowfullscreen="true" sandbox="allow-forms allow-pointer-lock allow-popups allow-same-origin allow-scripts allow-modals"></iframe>

## STL
- [ ] Read STL Source code

<iframe height="400px" width="100%" src="https://repl.it/repls/ShallowWavyMonad?lite=true" scrolling="no" frameborder="no" allowtransparency="true" allowfullscreen="true" sandbox="allow-forms allow-pointer-lock allow-popups allow-same-origin allow-scripts allow-modals"></iframe>






