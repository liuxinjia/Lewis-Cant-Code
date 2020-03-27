
## Nothing

```og 

std::string gcd(std::string str1, std::string str2){
    if(str1.size() < str2.size())
        return gcd(str2, str1);
    if(str2.size() == 0)
        return str1;
    if (str1.rfind(str2, 0) == std
        ::string::npos)
        return "";
    return gcd( str1.substr( str2.length()), str2);
}

```

### jkdsf

#### fdkjf
dsfdsf
```