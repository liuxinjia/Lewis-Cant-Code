public class Solution {
    public int MinDistance (string word1, string word2) {
        int m = word1.Length + 1, n = word2.Length + 1;
        int[][] dict = new int[m][];

        for (int i = 0; i < m; i++) {
            dict[i] = new int[n];
            dict[i][0] = i;
        }
        for (int j = 0; j < n; j++) {
            dict[0][j] = j;
        }

        for (int i = 1; i < m; i++) {
            for (int j = 1; j < n; j++) {
                dict[i][j] = word1[i - 1] == word2[j - 1] ? dict[i - 1][j - 1] :
                    Math.Min (Math.Min (dict[i - 1][j], dict[i][j - 1]),
                        dict[i - 1][j - 1]) + 1;
            }
        }

        return dict[m - 1][n - 1];
    }

    //reduce to one list
    public int MinDistance (string word1, string word2) {
        int m = word1.Length + 1, n = word2.Length + 1;
        int[] dict = new int[n];
        for (int j = 0; j < n; j++) {
            dict[j] = j;
        }

        for (int i = 1; i < m; i++) {
            int prev = dict[0];
            dict[0] = i;
            for (int j = 1; j < n; j++) {
                int temp = dict[j];
                dict[j] = word1[i - 1] == word2[j - 1] ? prev :
                    Math.Min (Math.Min (dict[j - 1], dict[j]), prev) + 1;
                prev = temp;
            }
        }

        return dict[n-1];
    }
}