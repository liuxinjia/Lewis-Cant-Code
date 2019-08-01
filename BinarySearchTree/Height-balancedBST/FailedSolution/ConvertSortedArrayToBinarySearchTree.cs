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
            // ConstructBBT (nums[i]);
            rootNode = InsertNode (rootNode, nums[i]);
        }

        return rootNode;
    }

    TreeNode InsertNode (TreeNode root, int val) {
        if (rootNode == null) return new TreeNode (val);

        TreeNode node = null;
        if (root.val > val) root.left = InsertNode (root.left, val);
        else if (root.val < val) root.right = InsertNode (root.right, val);
        else return root;

        int bal = IsBalance (root);
        if (bal >= 0) return root;

        if (bal == -1 && root.left.val > val) {
            return RightRotate (root);
        }
        if (bal == -1 && root.left.val < val) {
            root.left = LeftRotate (root.left);
            return RightRotate (root);
        }
        if (bal == 1 && root.right.val < val) {
            return LeftRotate (root);
        } else {
            root.right = RightRotate (root);
            return LeftRotate (root);
        }

    }

    void ConstructBBT (int val) {
        TreeNode node = new TreeNode (val);
        if (rootNode == null) {
            rootNode = node;
            return;
        }

        TreeNode cur = rootNode;
        TreeNode prev = null;
        int depth = 0;
        while (cur != null && cur.val != val) {
            prev = cur;
            depth++;
            if (cur.val > val) cur = cur.left;
            else cur = cur.right;
        }

        if (cur != null && cur.val == val) return;
        if (prev.val > val) prev.left = node;
        else prev.right = node;

        ReBalance (val, depth);
    }

    void ReBalance (int val, int depth) {
        if (depth < 2) return;
        gParents = null;

        int bal = IsBalance (rootNode);
        if (bal >= 0) return;

        TreeNode parent = null, newParent = null;
        if (depth == 2) parent = rootNode;
        else parent = gParents.val > val ? gParents.left : gParents.right;

        bool left = parent.val > val;
        if (left && parent.left.val < val) {
            parent.left = LeftRotate (parent.left);
            newParent = RightRotate (parent);
        } else if (left && parent.left.val > val) {
            newParent = RightRotate (parent);
        } else if (!left && parent.right.val > val) {
            parent.left = RightRotate (parent.right);
            newParent = LeftRotate (parent);
        } else {
            newParent = LeftRotate (parent);
        }

        if (depth == 2) rootNode = newParent;
        else if (gParents.val > val) gParents.left = newParent;
        else gParents.right = newParent;

    }

    //can use stack or not
    int IsBalance (TreeNode root) {
        if (root == null) return 0;

        int left = IsBalance (root.left);
        if (left < 0)
            return -1;

        int right = IsBalance (root.right);
        if (right < 0)
            return -2;

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