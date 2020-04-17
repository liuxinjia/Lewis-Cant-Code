## Fill

We all know we can construct an empty container with count copies of elements with value. e.g.

``` og
        vector( size_type count,
                 const T& value,
                 const Allocator& alloc = Allocator());
```

Now, you can use do the same things after initialzation( constructing).


### Possible Implementation

``` og
template<class ForwardIt, class T>
void fill(ForwardIt first, ForwardIt last, cosnt T& value){
    for(; first!=last; first++){
        *first = value;
    }
}
```

### Complexity

O(n), n depends on (last-first)

### Example

``` og
    std::vector<int> v(1, 10);
 
    std::fill(v.begin(), v.end(), -1);
 
    for (auto elem : v) {
        std::cout << elem << " ";
    }
```

### Extension

``std::fill_n(first, count, value)``
The second parameter ``count`` define how many elements we want to replace (fill) from the ``first`` pos.

## Replace

We have known how to replace elements if we know the location. If we don't know that, or we jsut want to replace the specified element.
Instead write your own function, the STL provide us the ``replace`` to repalce all elements are equal to we want and ``replace_if`` replace all emlements with a predication

### Possible Implementation

<!-- tabs:start -->

#### **replace version**


``` og
template<class ForwardIt, class T>
void replace(ForwardIt first, ForwardIt last, cosnt T& old_value,const T& new_value){
    for(; first!=last; first++){
        if(*first != old_value){
            *first = new_value;
        }
    }
}
```


#### **replace_if version**

``` og
template<class ForwardIt, class UnaryPredicate, class T>
void replace_if(FowardIt first, Forward last, UnaryPredicate p, cosnt T& new_value){
    for(; first!= last; first++){
        if(p(*first)){
            *fist = new_value;
        }
    }
}
```

<!-- tabs:end -->

<!-- tabs:start -->

#### **Repalce specfied number**

First replace **all** occurances of ``8`` with ``88`` in the vector
``` og
    std::vector<i> s{5, 7, 4, 2, 8, 6, 8, 9, 0, 3};
 
    std::replace(s.begin(), s.end(), 8, 88);
 
    for (int a : s) {
        std::cout << a << " ";
    }
```

#### **Replace with predicate**

``` og
std::replace_if(s.begin(), s.end(), std::bind(std::less<int>(), std::placeholders::_1, 5), 55);
for(int a: s){
    std::cout << a << " ";
}

```

<!-- tabs:end -->

### replace_copy
OK! Fine! As the name said, frist replace the contianer first and then copy it to the new container. Seriously! I can do it by myself with two separate line.
``` og

    std::vector<int> v1{5, 7, 4, 2, 8, 6, 8, 9, 0, 3};
    std::replace_if(v1.begin(), v1.end(),[](int n){return n > 5;}, 99);
    
    std::vector<int> v2(2,1);
    std::copy(v1.begin(), v1.end(), std::back_inserter(v2));

```
Excellent!

~[Who need Ronaldo]("")


Except for the additional line, we should not do that for the sake of perfomance. We can optimize it by combining the iterating process. Check the possible implementation below.


### Possible Implementation

<!-- tabs:start -->

#### **replace version**


``` og
template<class InputIt, class OutputIt, class T>
OutputIt replace_copy(InputIt first, InputIt last, OutputIt d_first, const T& old_value, const T& new_value){
    for(; first != last; ++first){
        *d_first++ = (*first == old_value) ? new_value : *first;
    }
    return d_first;
}

```

#### **repalce_if version**
``` og
template<class InputIt, class OutputIt, class T, class UnaryPredicate>
OutputIt replace_copy_if(InputIt first, InputIt last, OutputIt d_first, UnaryPredicate p, const T& new_value){
    for(; first != last; ++first){
        *d_first++ = p(*frist) ? new_value : *first;
    }
    return d_first;
}

```
<!-- tabs:end -->

### Example

<!-- tabs:start -->

``` og

```

