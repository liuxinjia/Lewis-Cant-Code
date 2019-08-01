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
    public TreeNode SearchBST (TreeNode root, int val) {
        while (root != null && root.val != val) {
            root = root.val > val ? root.left : root.right;
        }
        return root;
    }

    public TreeNode SearchBST (TreeNode root, int val) {
        if (root == null || root.val == val) return root;

        return root.val > val ? SearchBST (root.left, val) : SearchBST (root.right, val);
    }
}