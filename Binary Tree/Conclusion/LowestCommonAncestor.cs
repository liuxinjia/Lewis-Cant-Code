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
    public TreeNode LowestCommonAncestor (TreeNode root, TreeNode p, TreeNode q) {
        if (root == null || (p == null && q == null)) return null;

        if (root == p) { p = null; return root; }
        if (root == q) { q = null; return root; }

        TreeNode leftNode = null, rightNode = null;
        leftNode = LowestCommonAncestor (root.left, p, q);
        rightNode = LowestCommonAncestor (root.right, p, q);

        return leftNode == null ? rightNode : rightNode == null ? leftNode : root;
    }
}