#include <bits/stdc++.h>

bool overflow_add_int(int a, int b){
    bool sign_a = a > 0, sign_b = b > 0;
    if (sign_a != sign_b)
        return false;

    if (sign_a && (INT_MAX - a) < b)
        return true;
    if (!sign_a && (INT_MIN - a) > b)
        return true;
    return false;
}

int add_avoidOverflow_int(int a, int b){
    bool sign_a = a > 0, sign_b = b > 0;
    if (sign_a != sign_b)
        return a+b;

    if (sign_a && (INT_MAX - a) < b)
        return INT_MAX;
    if (!sign_a && (INT_MIN - a) > b)
        return INT_MIN;
    return a+b;
}
