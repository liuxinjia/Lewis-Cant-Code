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

    public bool IsValidBST (TreeNode root) {
        TreeNode prev = null;
        return Helper (root, prev);
    }

    bool Helper (TreeNode root, TreeNode prev) {
        if (root == null) return true;

        if (!Helper (root.left, prev)) return false;

        if (prev != null && root.val <= prev.val) return false;
        prev.val = root.val;

        return Helper (root.right, prev);
    }

    //return node to feedback the last stack state
    TreeNode invalidNode = new TreeNode (Int32.MinValue);
    TreeNode HelperStepByStep (TreeNode root, TreeNode prev) {
        if (root == null) return prev;

        prev = HelperFeedBack (root.left, prev);
        if (prev != null && root.val <= prev.val) return invalidNode;
        return HelperFeedBack (root.right, root);
    }

    public bool IsValidBST (TreeNode root) {
        var list = new List<int> ();
        return Helper (root, list);

    }

    bool Helper (TreeNode node, List<int> list) {
        if (node == null) return true;

        if (!Helper (node.left, list)) return false;

        if (list.Count == 0) {
            list.Add (node.val);
        } else {
            if (node.val <= list[0]) return false;
            else list[0] = node.val;
        }

        return Helper (node.right, list);
    }
}