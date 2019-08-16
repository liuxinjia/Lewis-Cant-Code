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
    public IList<int> InorderTraversal (TreeNode root) {
        return Inorder (root);

        List<int> list = new List<int> ();
        IList<int> rhList = list;
        InorderHelper (root, list);
        return rhList;
    }

    //Recursion method
    List<int> Inorder (TreeNode head) {
        List<int> list = new List<int> ();

        if (head == null) return list;

        list.AddRange (Inorder (head.left));
        list.Add (head.val);
        list.AddRange (Inorder (head.right));

        return list;
    }

    // Recursion method with helper method
    void InorderHelper (TreeNode head, List<int> list) {
        if (head == null) return;

        InorderHelper (head.left, list);
        list.Add (head.val);
        InorderHelper (head.right, list);
    }

    // Iteration with stack
    public IList<int> InorderTraversal (TreeNode root) {
        List<int> list = new List<int> ();
        Stack<TreeNode> stack = new Stack<TreeNode> ();
        if (root == null) return list;

        stack.Push (root);

        while (stack.Count > 0) {
            var node = stack.Pop ();
            while (node.left != null) {
                var temp = node.left;
                node.left = null;
                stack.Push (node);
                node = temp;
            }
            list.Add (node.val);

            if (node.right != null) {
                stack.Push (node.right);
            }

        }

        return list;
    }

    // iterate 02
    public IList<int> InorderTraversal (TreeNode root) {
        var stack = new Stack<TreeNode> ();
        var list = new List<int> ();

        while (stack.Count > 0 || root != null) {
            while (root != null) {
                stack.Push (root);
                root = root.left;
            }

            var node = stack.Pop ();
            list.Add (node.val);
            root = node.right;

        }

        return list;

    }

    // Morris Traversal
    public IList<int> InorderTraversal (TreeNode root) {
        var cur = root;
        List<int> list = new List<int> ();

        while (cur != null) {
            if (cur.left == null) {
                list.Add (cur.val);
                cur = cur.right;
            } else {
                var prev = cur.left;
                while (prev.right != null) {
                    prev = prev.right;
                }
                var temp = cur.left;
                cur.left = null;
                prev.right = cur;
                cur = temp;
            }
        }
        return list;
    }

}