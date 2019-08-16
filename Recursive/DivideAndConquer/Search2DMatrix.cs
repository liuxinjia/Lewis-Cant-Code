public class Solution {

    public bool SearchMatrix (int[, ] matrix, int target) {
        if (matrix == null) return false;
        int l1 = matrix.GetLength (0);
        int l2 = matrix.GetLength (1);
        if (l1 == 0 || l2 == 0) return false;

        if (l1 == 1 && l2 == 1) return matrix[0, 0] == target;
        return Divide (matrix, target, 0, l1 - 1, 0, l2 - 1);
    }

    bool Divide (int[, ] matrix, int target, int left, int right, int up, int down) {

        if (left > right || up > down) return false;

        int midX = left + (right - left) / 2;
        int midY = up + (down - up) / 2;

        if (matrix[midX, midY] == target) return true;
        if (matrix[midX, midY] > target) {
            return Divide (matrix, target, left, midX - 1, midY, down) || Divide (matrix, target, left, right, up, midY - 1);
        } else {
            return Divide (matrix, target, midX + 1, right, up, midY) || Divide (matrix, target, left, right, midY + 1, down);
        }
    }
}