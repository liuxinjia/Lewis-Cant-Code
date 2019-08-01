/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution {
    public bool IsBalanced (TreeNode root) {
        return Helper (root) != -1;
    }

    int Helper (TreeNode root) {
        if (root == null) return 0;

        int left = 0, right = 0;
        left = Helper (root.left);
        right = Helper (root.right);

        if (Math.Abs (left - right) > 1 || left == -1 || right == -1) return -1;

        return Math.Max (++left, ++right);

    }
}