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
    //Recursio method
    public bool HasPathSum (TreeNode root, int sum) {
        if (root == null) return false;
        return HelperHasPathSum (root, sum - root.val);
    }

    public bool HelperHasPathSum (TreeNode root, int sum) {

        bool lchild = false;
        bool rchild = false;
        if (root.left == null && root.right == null && sum == 0) return true;
        if (sum < 0) return false;

        if (root.left != null) {
            lchild = HelperHasPathSum (root.left, sum - root.left.val);
        }
        if (root.right != null) {
            rchild = HelperHasPathSum (root.right, sum - root.right.val);
        }
        return lchild | rchild;
    }

    //Iteration with stack
    public bool HasPathSum (TreeNode root, int sum) {
        if (root == null) return false;
        
        Stack<TreeNode> stackNode = new Stack<TreeNode> ();
        Stack<int> stackLength = new Stack<int> ();

        stackNode.Push (root);
        stackLength.Push (sum - root.val);

        while (stackNode.Count > 0) {
            var node = stackNode.Pop ();
            var length = stackLength.Pop ();
            if (node.left == null && node.right == null && length == 0) return true;

            if (node.right != null) {
                stackNode.Push (node.right);
                stackLength.Push (length - node.right.val);
            }
            if (node.left != null) {
                stackNode.Push (node.left);
                stackLength.Push (length - node.left.val);
            }
        }

        return false;
    }

}