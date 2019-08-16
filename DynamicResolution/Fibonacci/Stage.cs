public class Solution {
    //n must be positive

    //BruteForce: binary-tree

    public int ClimbStairs (int n) {
        int[] memo = new int[n + 1];
        return BF (n, 0, memo);

    }
    //TLR
    int BF (int n, int i, int[] memo) {
        if (n < i) return 0;
        if (n == i) return 1;
        if (memo[i] != 0) return memo[i];
        memo[i] = BF (n, i + 1, memo) + BF (n, i + 2, memo);
        return memo[i];
    }

    // Finbonacci
    public int ClimbStairs (int n) {
        int a = 1;
        int b = 1;

        for (int i = 1; i < n; i++) {
            int c = b;
            b = a + b;
            a = c;
        }

        return b;
    }

    public int ClimbStairs (int n) {
        int[] fib = new int[n + 1];
        fib[1] = 1;
        fib[2] = 2;

        for (int i = 3; i <= n; i++) {
            fib[i] = fib[i - 1] + fib[i - 2];
        }

        return fib[n];
    }

    // TLR
    public int ClimbStairs (int n) {
        if (n == 0) return 1;
        if (n == 1) return 1;

        return ClimbStairs (n - 1) + ClimbStairs (n - 2);
    }
    //Time complexity: O(n); Space complexity: O(1);
    public int ClimbStairs (int n) {
        return ClimbStairs_TailRecursion (n, 1, 2);
    }

    public int ClimbStairs_TailRecursion (int n, int a, int b) {
        if (n == 1) return a;
        if (n == 2) return b;

        return ClimbStairs_TailRecursion (n - 1, b, a + b);
    }

    public int ClimbStairs (int n) {
        var map = new Dictionary<int, int> ();
        return HashTree (n, 0, map);
    }

    int HashTree (int n, int i, Dictionary<int, int> map) {
        if (i == n - 2) {
            return 2;
        }
        if (i == n - 1) {
            return 1;
        }

        if (map.ContainsKey (i)) return map[i];
        int r = HashTree (n, i + 1, map) + HashTree (n, i + 2, map);
        map.TryAdd (i, r);
        return r;
    }
}