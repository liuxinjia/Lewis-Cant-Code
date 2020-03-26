#include<iostream>
#include <string>
#include <cstdlib>

// GCD(Greatest Common Divisor)
// Euclidian Algorithmn

// check the specific explanation in
// https : //www.freecodecamp.org/news/euclidian-gcd-algorithm-greatest-common-divisor/
int gcd(int a, int b){
    if(b==0)
        return a;
    return gcd(b, a % b);
}


int gcd(int a, int b){
    while((a%b) > 0){
       int m = a % b;
        a = b;
        b = m;
    }
    return b;
}

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

// since the GCD is associate
// you can find GCD of n numbers in the same way
// gcd(gcd(a,b), b)

// Extended Euclidean Algorithmn
// also find interger coefficients x and y such that
// ax + by = gcd(a, b)

int gcd(int a, int b, int x = 0, int y = 0){
    return gcdExtended(a, b, &x, &y);
}


int gcdExtended(int a, int b, int*x , int *y)
{
    if (b == 0){
        *x = 0;
        *y = 1;
        return b;
    }

    int x1, y1;
    int gcd = gcdExtended(b, a % b, &x1, &y1);
    *x = y1;
    *y = x1 - (a / b) * y1;

    return gcd;
}