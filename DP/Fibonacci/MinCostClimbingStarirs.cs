public class Solution {

    // Tree-Traversal
    //Stack Overflow
    public int MinCostClimbingStairs (int[] cost) {
        int startIndex = cost[0] > cost[1] ? cost[1] : cost[0];
        int result = MinHashTree (startIndex, cost, new Dictionary<int, int> ());
        return result;
    }

    int MinHashTree (int i, int[] cost, Dictionary<int, int> map) {
        if (i == cost.Length - 2) {
            return cost[cost.Length - 2];
        }
        if (i == cost.Length - 3) {
            return Math.Min (cost[cost.Length - 2], cost[cost.Length - 1]) + cost[cost.Length - 3];
        }

        if (map.ContainsKey (i)) return map[i];

        int result = Math.Min (MinHashTree (i + 1, cost, map), MinHashTree (i + 2, cost, map)) + cost[i];
        map.TryAdd (i, result);
        return result;
    }

    // DP: Fibonacci
    public int MinCostClimbingStairs (int[] cost) {
        if (cost.Length < 0) return -1;
        if (cost.Length == 1) return cost[0];

        int[] dp = new int[cost.Length];
        dp[0] = cost[0];
        dp[1] = cost[1];

        for (int i = 2; i < cost.Length; i++) {
            dp[i] = cost[i] + Math.Min (dp[i - 2], dp[i - 1]);
        }

        return Math.Min (dp[cost.Length - 1], dp[cost.Length - 2]);
    }

    //O(1) time
    public int MinCostClimbingStairs (int[] cost) {
        if (cost.Length < 0) return -1;
        if (cost.Length == 1) return cost[0];

        int a = cost[0], b = cost[1];
        for (int i = 2; i < cost.Length; i++) {
            int c = cost[i] + Math.Min (a, b);
            a = b;
            b = c;
        }

        return Math.Min (a, b);
    }

    //Recursion
    public int MinCostClimbingStairs (int[] cost) {
        return Helper (cost, new int[cost.Length + 1], cost.Length);
    }

    int Helper (int[] cost, int[] memo, int n) {
        if (n < 2) return cost[n];

        if (memo[n] != 0) return memo[n];
        return memo[n] == 0 ? (n == cost.Length ? 0 : cost[n]) + Math.Min (Helper (cost, memo, n - 1), Helper (cost, memo, n - 2)) : 0;
    }

}