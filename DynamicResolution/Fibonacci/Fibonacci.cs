public class Solution {

    //nom-tail recursion
    public int Fib (int N) {
        if (N == 0) return 0;
        if (N == 1) return 1;

        return Fib (N - 1) + Fib (N - 2);
    }

    //tail recursion
    public int Fib (int N, int a = 0, int b = 1) {
        if (N == 0) return a;
        if (N == 1) return b;

        return Fib (N - 1, b, a + b);
    }

    // map solution:
    public int Fib (int N) {
        var map = new Dictionary<int, int> ();
        return Helper (N, map);
    }

    int Helper (int N, Dictionary<int, int> map) {
        if (N < 2) return N;

        if (map.TryAdd (N - 1, 0))
            map[N - 1] = Helper (N - 1, map);

        if (map.TryAdd (N - 2, 0))
            map[N - 2] = Helper (N - 2, map);

        return map[N - 1] + map[N - 2];
    }
    
    // iteration
    public int Fib (int N) {
        if (N == 0) return 0;
        int a = 0;
        int b = 1;

        for (int i = 1; i < N; i++) {
            int c = b;
            b = a + b;
            a = c;
        }

        return b;
    }

}