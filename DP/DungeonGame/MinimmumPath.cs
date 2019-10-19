public class Solution {
    public int MinPathSum (int[][] grid) {
        int[] cur = new int[grid[0].Length];

        int i = 0, j = 0;
        for (; i < grid.Length; i++) {
            for (j = 0; j < grid[i].Length; j++) {
                if (i == 0 || j == 0) {
                    if (i > 0) {
                        cur[j] += grid[i][j];
                    } else if (j > 0) {
                        cur[j] = cur[j - 1] + grid[i][j];
                    } else
                        cur[j] = grid[i][j];
                } else {
                    cur[j] = Math.Min (cur[j], cur[j - 1]) + grid[i][j];
                }
            }
        }
        return cur[j - 1];
    }
}