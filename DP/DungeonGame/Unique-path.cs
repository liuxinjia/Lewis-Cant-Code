public class Solution {
    //formula : dp[i][j] = dp[i][j - 1] + dp[i - 1][j]
    public int UniquePaths (int m, int n) {
        int[] cur = new int[n];
        int[] prev = new int[n];

        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (i == 0 || j == 0) cur[j] = 1;
                else
                    cur[j] = prev[j] + cur[j - 1];
            }
            prev = cur;
        }

        return cur[n - 1];
    }

    //Even furtehr:: more space
    public int UniquePaths (int m, int n) {
        int[] cur = new int[n];

        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (i == 0 || j == 0) cur[j] = 1;
                else
                    cur[j] += cur[j - 1];
            }
        }
        return cur[n - 1];
    }
}