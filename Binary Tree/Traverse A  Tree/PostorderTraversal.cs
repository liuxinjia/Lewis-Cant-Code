using System.Collections.Generic;
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
    public IList<int> PostorderTraversal (TreeNode root) {
        //Recursion method
        return Postorder (root);

        //Recursion method with helper function
        var list = new List<int> ();
        PostorderHelper (root, list);
        return list;
    }

    List<int> Postorder (TreeNode head) {
        var list = new List<int> ();

        if (head == null) return list;

        Postorder (head.left);
        Postorder (head.right);
        list.Add (head.val);

        return list;
    }

    void PostorderHelper (TreeNode head, List<int> list) {
        if (head == null) return;

        PostorderHelper (head.left, list);
        PostorderHelper (head.right, list);
        list.Add (head.val);
    }

    // Iteration with two stack
    public IList<int> PostorderTraversal (TreeNode root) {
        var list = new List<int> ();
        var result = new Stack<int> ();

        if (root == null) return list;

        Stack<TreeNode> stack = new Stack<TreeNode> ();
        stack.Push (root);

        while (stack.Count > 0) {
            var node = stack.Pop ();
            result.Push (node.val);

            if (node.left != null)
                stack.Push (node.left);
            if (node.right != null) {
                stack.Push (node.right);
            }
        }

        while (result.Count > 0) {
            list.Add (result.Pop ());
        }

        return list;
    }

    // Iteration with one stack
    //push directly root node two times while traversing to the left
    // Where Amazing happens
    //Fuck your amazing things. You are just fucking write something wrong in comparing line 93 - 73;
    //1 false
    public IList<int> PostorderTraversal (TreeNode root) {
        var list = new List<int> ();
        if (root == null) return list;

        Stack<TreeNode> stack = new Stack<TreeNode> ();
        var cur = root;

        while (true) {
            while (cur != null) {
                stack.Push (cur);
                stack.Push (cur);
                cur = cur.left;
            }

            if (stack.Count == 0) break;

            cur = stack.Pop ();

            if (stack.Count > 0 && root == stack.Peek ()) {
                cur = cur.right;
            } else {
                list.Add (cur.val);
                cur = null;
            }
        }

        return list;
    }

    //2 correct
    public IList<int> PostorderTraversal (TreeNode root) {
        var list = new List<int> ();
        if (root == null) return list;

        Stack<TreeNode> stack = new Stack<TreeNode> ();

        while (true) {
            while (root != null) {
                stack.Push (root);
                stack.Push (root);
                root = root.left;
            }

            if (stack.Count == 0) break;

            root = stack.Pop ();

            if (stack.Count > 0 && root == stack.Peek ()) {
                root = root.right;
            } else {
                list.Add (root.val);
                root = null;
            }
        }

        return list;
    }

    //push root and root's right child to stack while moving down
    // yes, it's the pushing sequence
    // https://www.geeksforgeeks.org/iterative-postorder-traversal-using-stack/
    public IList<int> PostorderTraversal (TreeNode root) {
        var list = new List<int> ();
        if (root == null) return list;
        var stack = new Stack<TreeNode> ();

        do {
            while (root != null) {
                stack.Push (root.right);
                stack.Push (root);
                root = root.left;
            }

            var node = stack.Pop ();
            if (stack.Count > 0 && stack.Peek () == node.right) {
                root = stack.Pop ();
                stack.Push (node);
            } else {
                list.Add (node.val);
                root = null;
            }
        } while (stack.Count > 0);

        return list;
    }

}