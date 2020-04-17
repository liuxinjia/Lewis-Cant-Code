# Sequentailly increasing

 ``std::iota``
Fill the range with sequentially increasing values, starting with value and repetitively evaluating ``++value``
Defined in header <numeric>

## Possible implementation 

``` og
template<class ForwardIt, class T>
void iota(ForwardIt first, Foward last, T value){
    while(first != last){
        *first++ = value;
        ++value;
        // Equiavelent to:
        // *(first) = value
        // *(first+1) = value
        // *(first+2) = value
    }
}

```

### Complexity

O(n), n depends on (last - first)


### Example

<!-- tabs:start -->

#### **Fill container**

fill the associative container with increasing value

``` og
std::vector<int> tmpVec(SIZE)
std::iota(tmpVec.begin(), tmpVec.end(), 0);

// fill a set
std::set<int> mySet(tmpVec.begin(), tmpVec.end());
// fill a map
std::map<int,int> myMap;
for(auto i =0; i<SIZE; i++) myMap[i] = i;
//or
for(auto i: tmpVec) myApp[i] = i;
// Ok, fine! It make no sense.
```


### **Shuffle list**

shuffle the ``std::list`` since you can't do it directly
```
// from https://en.cppreference.com/w/cpp/algorithm/iota
std::list<int> l(10);
std::iota(l.begin(), l.end(), 0);

std::vector<std::list<int>::iterator> v(l.size());
std::iota(v.begin(), v.end(), l.begin());

std::shuffle(v.begin(), v.end(), std::mt19937{std::radnom_device{}()});

std::cout << "Contents of the list: ";
for(auto n: l) std::cout << n << ' ';
std::cout << '\n';
 
std::cout << "Contents of the list, shuffled: ";
for(auto i: v) std::cout << *i << ' ';
std::cout << '\n';

```

<!-- tabs:end -->
