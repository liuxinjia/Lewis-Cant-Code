public class Solution {
    //with extra space but don't change original grid
    public int UniquePathsWithObstacles (int[][] obstacleGrid) {
        if (obstacleGrid == null || obstacleGrid.Length == 0) {
            return 0;
        }
        int i = 0, j = 0;
        int[] cur = new int[obstacleGrid[0].Length];
        for (; i < obstacleGrid.Length; i++) {
            for (j = 0; j < obstacleGrid[i].Length; j++) {
                if (obstacleGrid[i][j] == 1) cur[j] = 0;
                else if (i == 0 || j == 0) {
                    if (j > 0 && cur[j - 1] == 0) cur[j] = 0;
                    else if (i > 0 && cur[j] == 0) cur[j] = 0;
                    else cur[j] = 1;
                } else {
                    if (cur[j - 1] != 0 && cur[j] != 0)
                        cur[j] += cur[j - 1];
                    else if (cur[j] == 0)
                        cur[j] = cur[j - 1];
                }

            }
        }

        return cur[j - 1];
    }

    public int UniquePathsWithObstacles (int[][] obstacleGrid) {
        if (grid == null || grid.Length == 0) {
            return 0;
        }
        int i = 0, j = 0;
        for (; i < grid.Length; i++) {
            for (j = 0; j < grid[i].Length; j++) {
                if (grid[i][j] == 1) continue;
                else if (i == 0 || j == 0) {
                    if (j > 0 && grid[i][j - 1] == 1) grid[i][j] = 1;
                    else if (i > 0 && grid[i - 1][j] == 1) grid[i][j] = 1;
                    else grid[i][j] = -1;
                } else {
                    if (grid[i][j - 1] < 0 && grid[i - 1][j] < 0)
                        grid[i][j] = grid[i][j - 1] + grid[i - 1][j];
                    else if (grid[i][j - 1] >= 0)
                        grid[i][j] = grid[i - 1][j];
                    else grid[i][j] = grid[i][j - 1];
                }
            }
        }

        return grid[i - 1][j - 1] == 1 ? 0 : grid[i - 1][j - 1] * -1;
    }
}

public class Solution {
    public int UniquePathsWithObstacles (int[][] obstacleGrid) {
        int i = 0, j = 0;
        for (; i < grid.Length; i++) {
            for (j = 0; j < grid[i].Length; j++) {
                if (grid[i][j] == 1) continue;
                if (i == 0 || j == 0) grid[i][j] = -1;
                else {
                    if (grid[i][j - 1] < 0 && grid[i - 1][j] < 0) {
                        grid[i][j] = grid[i][j - 1] - grid[i - 1][j];
                    } else if (grid[i][j - 1] < 0) {
                        grid[i][j] = grid[i][j - 1];
                    } else if (grid[i - 1][j] < 0) {
                        grid[i][j] = grid[i - 1][j];
                    }
                }

            }
        }
        return grid[i - 1][j - 1] * -1;

    }
}