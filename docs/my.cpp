// 本题为考试单行多行输入输出规范示例，无需提交，不计分。
#include <iostream>
using namespace std;

int isprime(int n)
{
    int min = 0;
    for (int i = 2; i < n; i++)
    {
        if (n % i == 0)
            min = std::min(n - i, min);
    }
    return min;
}

int isprimeRight(int n)
{
    int min = 0;
    for (int i = n; i < n * 2; i++)
    {
        if (n % i == 0)
        {
            min = std::min(n - i, min);
            return min;
        }
    }
    return min;
}
int main()
{
    int a;
    cin >> a;

    int lMin = isprime(a);
    int rMin = isprimeRight(a * 2);

    std::cout << std::min(lMin, rMin);
}