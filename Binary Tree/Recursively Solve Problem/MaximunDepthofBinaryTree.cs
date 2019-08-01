/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
using System;
public class Solution {
    int answer;
    public int MaxDepth (TreeNode root) {
        //Top-down recursion
        int length = 0;
        helperMDepth (root, length + 1);
        return answer;

        //Bottom-up recursion
        if (root == null) return length;

        if (root.left != null) {
            length = MaxDepth (root.left);
        }
        if (root.right != null) {
            length = Math.Max (length, MaxDepth (root.right));
        }

        return length + 1;
    }

    void helperMDepth (TreeNode root, int length) {
        if (root == null) return;

        answer = Math.Max (answer, length);
        if (root.left != null) helperMDepth (root.left, length + 1);
        if (root.right != null) helperMDepth (root.right, length + 1);
    }
}