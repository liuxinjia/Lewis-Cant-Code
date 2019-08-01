/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
using System.Collections.Generic;

public class Solution {
    public bool IsSymmetric (TreeNode root) {
        return root == null || helperSymmetric (root.left, root.right);

    }

    public bool helperSymmetric (TreeNode lChild, TreeNode rChild) {
        if (lChild == null || rChild == null) return lChild == rChild;

        if (lChild.val != rChild.val) return false;

        return helperSymmetric (lChild.left, rChild.right) && helperSymmetric (lChild.right, rChild.left);
    }

    public bool IsSymmetric (TreeNode root) {
        if (root == null) return true;

        var lstack = new Stack<TreeNode> ();
        var rstack = new Stack<TreeNode> ();
        lstack.Push (root.left);
        rstack.Push (root.right);

        while (lstack.Count > 0) {
            var lChild = lstack.Pop ();
            var rChild = rstack.Pop ();

            if (lChild == null && rChild == null)
                continue;

            if (lChild == null || rChild == null) {
                return false;
            }

            if (lChild.val != rChild.val) {
                return false;
            } else {
                rstack.Push (rChild.right);
                lstack.Push (lChild.left);
                rstack.Push (rChild.left);
                lstack.Push (lChild.right);
            }
        }

        return true;
    }
}