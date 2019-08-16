public class Solution {
    public int TotalNQueens (int n) {
        var map = new Dictionary<int, int> ();
        return PlaceQueen (n, map, 0);
    }
    //using hashmpa or array will fail
    // Now, do you know why
    // hints: in the back - track core: in line 14,16,
    //  it does not add the child and remove the root instead of the chld
    /* #region  Failed HashMapSoltion */
    //Same algroithm with different data structure
    int PlaceQueen (int n, Dictionary<int, int> map, int curRow) {
        if (curRow == n) return 1;

        int total = 0;
        for (int i = 0; i < n; i++) {
            map.TryAdd (i, curRow);
            if (IsNotUnderStack (map, curRow, i)) total += PlaceQueen (n, map, curRow + 1);
            map.Remove (i);
        }
        return total;
    }

    bool IsNotUnderStack (Dictionary<int, int> map, int curRow, int curCol) {
        for (int i = 0; i < curRow; i++) {
            int diag = curCol - (curRow - i);
            if (map.ContainsKey (diag)) return false;
            diag = curCol + (curRow - i);
            if (map.ContainsKey (diag)) return false;
        }
        return true;
    }

    /* #endregion */

    /* #region  LinkedList */
    public int TotalNQueens (int n) {
        if (n < 0) return -1;
        var linkList = new LinkedList<int> ();
        return PlaceQueen (n, linkList, 0);
    }

    int PlaceQueen (int n, LinkedList<int> list, int curRow) {
        if (curRow == n) return 1;

        int total = 0;
        for (int i = 0; i < n; i++) {
            list.AddLast (i);
            if (IsNotUnderStack (list, curRow, i)) total += PlaceQueen (n, list, curRow + 1);
            list.RemoveLast ();
        }
        return total;
    }

    bool IsNotUnderStack (LinkedList<int> list, int curRow, int curCol) {
        int i = 0;
        foreach (var item in list) {
            if (i == curRow) break;
            if (item == curCol) return false;
            int diag = curCol - (curRow - i);
            if (item == diag) return false;
            diag = curCol + (curRow - i);
            if (item == diag) return false;
            i++;
        }
        return true;
    }
    /* #endregion */

    
}