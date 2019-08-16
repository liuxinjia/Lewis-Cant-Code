public class Solution {
    //fail solution;
    //it cost exponential time
    public double MyPow (double x, int n) {
        return Helper (x, 1.0, n);
    }

    double Helper (double x, double prev, int n) {
        if (n == 0) return 1.0;

        double positive = prev * x;
        if (n == 1) return positive;
        double negative = prev * (1 / x);
        if (n == -1) return negative;

        return Helper (x, n < 0 ? negative : positive, n < 0 ? n + 1 : n - 1);

    }

    //
    public double MyPow (double x, int n) {
        if (n == 0) return 1.0;
        if (n < 0) {
            x = 1 / x;
            return n % 2 == 0 ? MyPow (x * x, n / -2) : x * MyPow (x * x, n / -2);
        }
        return n % 2 == 0 ? MyPow (x * x, n / 2) : x * MyPow (x * x, n / 2);

    }

    public double MyPow (double x, int n) {
        double ans = 1.0;
        double iter = n > 0 ? x : 1 / x;
        //-1 >> 1 is equal to -1; 
        // it will be a loop
        long absN = Math.Abs ((long) n);
        while (absN != 0) {
            if ((absN & 1) == 1) ans *= iter;
            absN >>= 1;
            iter *= iter;
        }

        return ans;
    }

}