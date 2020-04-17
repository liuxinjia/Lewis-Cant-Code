# Map

## Member Functions

### ``map::emplace``
Insert a new element in the map if the key is unqiue. The new element is constructed via either copy or move existing objects into the containers which is expensive.
### ``map::emplace_hint``

``` og



```

The unorder container also offer the overloads, but they have no effect, since there is no way to obtain a useful hint for unordered contains. [](https://stackoverflow.com/questions/41507671/what-is-the-use-of-emplace-hint-in-map)

