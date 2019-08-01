using System.Collections.Generic;

public class Solution {
    public bool IsValidSudoku (char[][] board) {
        var set = new HashSet<string> ();

        for (int i = 0; i < board.Length; i++) {
            for (int j = 0; j < board[i].Length; j++) {
                if (board[i][j] != '.') {
                    string val = "(" + board[i][j] + ")";
                    if (!set.Add (i + val) || !set.Add (val + j) || !set.Add ((i / 3) + val + (j / 3)))
                        return false;
                }
            }
        }
        return true;
    }
    //Reverse
    public bool IsValidSudoku (char[][] board) {
        for (int i = 0; i < board.Length; i++) {
            var rowSet = new HashSet<char> ();
            var colSet = new HashSet<char> ();
            var blockSet = new HashSet<char> ();
            for (int j = 0; j < board[i].Length; j++) {
                var val = board[i][j];
                if (board[i][j] != '.' && !rowSet.Add (board[i][j])) return false;
                if (board[j][i] != '.' && !colSet.Add (board[j][i])) return false;

                int colIndex = (i % 3) * 3 + j % 3;
                int rowIndex = (i / 3) * 3 + j / 3;

                if (board[rowIndex][colIndex] != '.' && !blockSet.Add (board[rowIndex][colIndex])) return false;
            }
        }
        return true;
    }
}