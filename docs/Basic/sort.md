# Sort
## how to sort a collection in C# 

### Sort(Comparison<T>)-----using delegate
we can define a comparison method in our class :
```og
public class Example<T> where T : IComparable {
    public T part;
    public int CompareTo(Example<T> e) {
        return this.part.CompareTo(e.part);
    }
}
```
or using a static method ( friend function):
``` og
    public static int CompareTo(Example<T> e1, Example<T> e2) {
        return e1.part.CompareTo(e2.part);
    }
```
But it's convenitent to use and anonymouse method.
``` og
list.Sort((x,y) => x.elements.CompareTo(y.elements));
```

### Sort(IComparer) ----- suing specified comparer

The Sort function 's default comparer is null. We can implement the ``IComparer`` when comparing elements:
```og
class Example<T> : IComparer<T> where T : IComparable {
    public int Compare(T x, T y) {
        return x.CompareTo(y);
    }
}
```
> [!Note] [Sort(Int32, Int32, IComparer<T>)](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.sort?view=netframework-4.8#System_Collections_Generic_List_1_Sort_System_Int32_System_Int32_System_Collections_Generic_IComparer__0__) 
> Sorts the elements in a range of elements in of elements using the specified comparer.

### Array.Sort
Sorts the elements in an array is [similar](https://docs.microsoft.com/en-us/dotnet/api/system.array.sort?view=netframework-4.8).




## std::sort() in C++ STL

C++ STL provides a convenient function [sort](https://en.cppreference.com/w/cpp/algorithm/sort) that sorts a vector or arry (items with random access).

### sort using a standard library comapre function object
we can use the third parameter to sort in descending order which does a comparison in way that puts greater element before.
``` og
 std::sort(s.begin(), s.end(), std::greater<int>)
````

### sort in custom way
It's similar to what we do in C#. Let's also define a compariso method.
```
struct Example{
    int start, end;
};

bool compare(Example e1, Example e2){
    return (e1.start < e2.end);
}

...
std::vector<Example> arr;

sort(arr, arr+n, compare);

...
```




