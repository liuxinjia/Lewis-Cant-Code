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

    // Time-Limit
    public TreeNode DeleteNode (TreeNode root, int key) {
        var cur = root;
        TreeNode prev = null;

        while (cur != null && cur.val != key) {
            prev = cur;
            if (cur.val > key) cur = cur.left;
            else if (cur.val < key) cur = cur.right;
        }

        if (prev == null) return DeleteWantedNode (cur);
        if (prev.left == cur) prev.left = DeleteWantedNode (cur);
        else prev.right = DeleteWantedNode (cur);

        return root;

    }

    TreeNode DeleteWantedNode (TreeNode root) {
        if (root == null) return root;
        if (root.left == null) return root.right;
        if (root.right == null) return root.left;

        var wantedNode = ReplaceNode (root.right);
        wantedNode.left = root.left;
        if (wantedNode != root.right) wantedNode.right = root.right;

        return wantedNode;
    }

    TreeNode ReplaceNode (TreeNode root) {
        var cur = root;
        TreeNode prev = cur;

        while (cur.left != null) {
            prev = cur;
            cur = cur.left;
        }

        if (cur != root && cur.right != null) {
            prev.left = cur.right;
        }

        return cur;
    }

    //Recursion
    public TreeNode DeleteNode (TreeNode cu, int key) {
        if (cu == null) return null;

        if (cu.val > key) {
            cu.left = DeleteNode (cu.left, key);
        } else if (cu.val < key) {
            cu.right = DeleteNode (cu.right, key);
        } else {
            if (cu.left == null) return cu.right;
            if (cu.right == null) return cu.left;

            var minNode = FindMin (cu.right);
            cu.val = minNode.val;
            cu.right = DeleteNode (cu.right, cu.val);
        }

        return cu;
    }

    //recursion optimize 1
    public TreeNode DeleteNode (TreeNode root, int key) {
        if (root == null) return null;

        if (root.val > key) root.left = DeleteNode (root.left, key);
        else if (root.val < key) root.right = DeleteNode (root.right, key);
        else {
            return DeleteRootNode (root);
        }
        return root;
    }

    //Iteration optimize 1
    public TreeNode DeleteNode (TreeNode root, int key) {
        var cur = root;
        TreeNode prev = null;
        while (cur != null && cur.val != key) {
            prev = cur;
            if (cur.val > key) cur = cur.left;
            else if (cur.val < key) cur = cur.right;
        }

        if (prev == null) return DeleteRootNode (cur);
        if (prev.left == cur) prev.left = DeleteRootNode (cur);
        else prev.right = DeleteRootNode (cur);

        return root;
    }

    TreeNode DeleteRootNode (TreeNode cur) {
        if (cur == null) return null;

        if (cur.left == null) return cur.right;
        if (cur.right == null) return cur.left;

        var node = cur.right;
        TreeNode prev = null;

        while (node.left != null) {
            prev = node;
            node = node.left;
        }
        node.left = cur.left;
        if (cur.right != node) {
            prev.left = node.right;
            node.right = cur.right;
        }

        return node;
    }

    TreeNode FindMin (TreeNode root) {
        while (root.left != null) {
            root = root.left;
        }

        return root;
    }
}