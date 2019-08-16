public class Solution {
    char[][] result;
    bool isFound = false;

    public void SolveSudoku (char[][] board) {
        Sovler (board, 0, 0);

        for (int i = 0; i < board.Length; i++) {
            for (int j = 0; j < board[0].Length; j++) {
                board[i][j] = result[i][j];
            }
        }
    }

    bool Sovler (char[][] board, int curRow, int curCol) {
        int rowLength = board.Length;
        int colLength = board[curRow].Length;

        if (curCol == colLength) {
            curCol = 0;
            curRow++;
        }

        for (int i = curRow; i < rowLength; i++) {
            for (int j = curCol; j < colLength; j++) {
                if (!IsNotOccupied (board, i, j)) continue;

                for (char num = '1'; num <= '9'; num++) {
                    if (!IsValid (board, i, j, num)) continue;
                    PlaceNum (board, i, j, num);
                    if (Sovler (board, i, j + 1)) return true;
                    else Remove (board, i, j);
                }
                return false;
            }

        }
        return true;

    }

    bool IsNotOccupied (char[][] board, int i, int j) {
        return board[i][j] == '.';
    }

    void PlaceNum (char[][] board, int curRow, int curCol, char num) {
        board[curRow][curCol] = num;
    }

    void Remove (char[][] board, int curRow, int curCol) {
        board[curRow][curCol] = '.';
    }

    bool IsValid (char[][] board, int curRow, int curCol, char num) {
        for (int i = 0; i < 9; i++) {
            if (board[curRow][i] != '.' && board[curRow][i] == num) return false;
            if (board[i][curCol] != '.' && board[i][curCol] == num) return false;
            int v = 3 * (curRow / 3) + i / 3;
            int v1 = 3 * (curCol / 3) + i % 3;
            if (board[v][v1] != '.' && board[v][v1] == num) return false;
        }
        return true;
    }

}