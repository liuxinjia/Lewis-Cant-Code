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
    TreeNode rootNode = null;

    public TreeNode SortedArrayToBST (int[] nums) {
        for (int i = 0; i < nums.Length; i++) {
            rootNode = InsertNode (rootNode, nums[i]);
        }

        return rootNode;
    }

    TreeNode InsertNode (TreeNode root, int val) {
        if (root == null) return new TreeNode (val);

        if (root.val > val) root.left = InsertNode (root.left, val);
        else if (root.val < val) root.right = InsertNode (root.right, val);
        else return root;

        int bal = IsBalance (root);
        if (bal >= 0) return root;
        bool left = root.val > val ? true : false;

        if (left && root.left.val > val) {
            return RightRotate (root);
        }
        if (left && root.left.val < val) {
            root.left = LeftRotate (root.left);
            return RightRotate (root);
        }
        if (!left && root.right.val < val) {
            return LeftRotate (root);
        }
        if (!left && root.right.val > val) {
            root.right = RightRotate (root.right);
            return LeftRotate (root);
        }

        return root;

    }

    int IsBalance (TreeNode root) {
        if (root == null) return 0;

        int left = IsBalance (root.left);
        if (left < 0)
            return left;

        int right = IsBalance (root.right);
        if (right < 0)
            return left;

        if (Math.Abs (left - right) > 1)
            return -1;

        return Math.Max (left, right) + 1;
    }

    TreeNode LeftRotate (TreeNode parent) {
        var child = parent.right;
        parent.right = child.left;
        child.left = parent;
        return child;
    }

    TreeNode RightRotate (TreeNode parent) {
        TreeNode child = parent.left;
        parent.left = child.right;
        child.right = parent;
        return child;
    }

}