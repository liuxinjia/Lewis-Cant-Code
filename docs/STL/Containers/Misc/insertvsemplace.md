# C++ insert vs emplace ( and operator[] )

All containers (vector, stack, map,etc) support both method. They all insert elements before the spcified position.

>[!Note] 
> queue and stack has it's own specific insertion ``push``, but ``emplace`` do the same job but also constructing first. Similar to other containers.

  ``` og
  std::stack<std::string> mystack;

  mystack.emplace ("First sentence");
  mystack.emplace ("Second sentence"); 
  mystack.emplace ("Third sentence");

  std::cout << "mystack contains:\n";
  while (!mystack.empty())
  {
    std::cout << mystack.top() << '\n';
    mystack.pop();
  }

  ```

 The advantage of emplace is, it does in-place insertion and avoid an unnecessary copy of object. For objects, use of ``emplace`` is preferred for efficiency reason
>[!Tip]
> For primitive data types, It doest not matter which one was use. 

** Using operator[]
Yeah! Somebody will tell you for the sake of exception, don't use [] to get access to array elements. But ``at`` can't modified the elements directly.
And another advantage is our STL container is dynamical (Well, it's not good since resizing is not always tasty). We don't need to conside the boundary(except you have exceed to the limit). 
>[!Warning]  You use operator[] to insert elements into std::map. But you can also access elements via operator[] which is something special. Check the below code something happen even the value is not mapped to key.

``` og
	map<char, int> mp; 
	
	// using [] to assign key to value 
	mp['a'] = 5; 
	mp['b'] = 6; 
	mp['c'] = 2; 
  	// printing values 
	cout << "The element keys to a is : "; 
	cout << mp['a'] << endl; 
	
	cout << "The element keys to b is : "; 
	cout << mp['b'] << endl; 
	
	cout << "The element keys to c is : "; 
	cout << mp['c'] << endl; 
	
	// default constructor is called 
	// prints 0 
	cout << "The element keys to d is : "; 
	cout << mp['d'] << endl; 
  
```