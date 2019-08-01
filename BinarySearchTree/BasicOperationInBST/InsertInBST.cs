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
    public TreeNode InsertIntoBST (TreeNode root, int val) {
        var node = new TreeNode (val);
        if (root == null) return node;

        var cur = root;
        var prev = cur;
        while (cur != null) {
            prev = cur;
            if (cur.val == val) return null;
            cur = cur.val > val ? cur.left : cur.right;
        }

        if (prev.val > val) prev.left = node;
        else if (prev.val < val) prev.right = node;

        return root;
    }

    public TreeNode InsertIntoBST (TreeNode root, int val) {
        HelperInsertIntoBST (root, val);
        return root;
    }
    public bool HelperInsertIntoBST (TreeNode root, int val) {
        if (root == null) return true;

        var node = new TreeNode (Int32.MaxValue);
        if (root.val > val) {
            if (HelperInsertIntoBST (root.left, val)) {
                root.left = new TreeNode (val);
                return false;
            }
        } else if (root.val < val) {
            if (HelperInsertIntoBST (root.right, val)) {
                root.right = new TreeNode (val);
                return false;
            }
        }

        return false;
    }

    public TreeNode InsertIntoBST (TreeNode root, int val) {
        if (root == null) return new TreeNode (val);
        if (root.val > val) root.left = InsertIntoBST (root.left, val);
        else if (root.val < val) root.right = InsertIntoBST (root.right, val);

        return root;
    }