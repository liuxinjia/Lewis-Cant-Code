public class Solution {
    public int CountPrimes (int n) {

        int count = 0;
        if (n < 2) return 0;
        SetPrimes (n);
        for (int i = 0; i < n; i++) {
            if (!isNotPrime[i]) count++;
        }
        return count;

        for (int i = 2; i < n; i++) {
            if (IsPrime (i))
                ++count;
        }
        return count;
    }

    bool[] isNotPrime;

    //The Sieve of Eratosthenes 
    //mark up for p*p to p*p + p*n
    // uses an extra O(n) memory 
    // and its runtime complexity is O(n log (log n))
    #region  The Sieve of Eratosthenes  

    void SetPrimes (int n) {
        isNotPrime = new bool[n];
        for (int i = 0; i < 2; i++)
            isNotPrime[i] = true;

        for (int i = 2; i * i < n; i++) {
            if (isNotPrime[i]) continue;
            for (int j = i * i; j < n; j += i) {
                isNotPrime[j] = true;
            }
        }
    }

    //optimized the sieve of eratosthnes
    //first filter the even number,divide it to two part 
    //then use the formula p*p + n*p as the hint
    // its runtime complexity is O (n log log n/2)/2
    int SivePrim (int n) {
        if (n < 3) return 0;

        bool[] f = new bool[n];

        int count = n / 2;
        for (int i = 3; i * i < n; i += 2) {
            if (f[i]) continue;
            for (int j = i * i; j < n; j += 2 * i) {
                if (!f[j]) {
                    --count;
                    f[j] = true;
                }
            }
        }
        if (f == isNotPrime) return 0;
        return count;
    }

    #endregion 

    //optimised O(√n)
    bool IsPrime (int n) {
        if (n <= 1) return false;
        if (n <= 3) return true;
        if (n % 2 == 0 || n % 3 == 0) return false;
        for (int i = 5; i * i <= n; i += 6) {
            if (n % i == 0 || n % (i + 2) == 0) {
                return false;
            }
        }
        return true;
    }

}