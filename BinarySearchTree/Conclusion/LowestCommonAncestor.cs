public class Solution {
    public TreeNode LowestCommonAncestor (TreeNode root, TreeNode p, TreeNode q) {
        while (root != null && (p.val - root.val) * (q.val - root.val) > 0) {
            root = root.val > p.val ? root.left : root.right;
        }

        return root;
    }

    public TreeNode LowestCommonAncestor (TreeNode root, TreeNode p, TreeNode q) {
        if (root == null || (p.val - root.val) * (q.val - root.val) <= 0) return root;
        return root.val > p.val ? LowestCommonAncestor (root.left, p, q) : LowestCommonAncestor (root.right, p, q);
    }
}