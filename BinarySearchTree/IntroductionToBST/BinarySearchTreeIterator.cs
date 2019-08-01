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

public class BSTIterator {
    Queue<int> list = new Queue<int> ();
    public BSTIterator (TreeNode root) {
        var stack = new Stack<TreeNode> ();

        while (root != null || stack.Count > 0) {
            while (root != null) {
                stack.Push (root);
                root = root.left;
            }

            var node = stack.Pop ();
            list.Enqueue (node.val);
            root = node.right;

        }

    }

    /** @return the next smallest number */
    public int Next () {
        if (HasNext ())
            return list.Dequeue ();
        else
            return -1;

    }

    /** @return whether we have a next smallest number */
    public bool HasNext () {
        return list.Count > 0;
    }
}

/**
 * Your BSTIterator object will be instantiated and called as such:
 * BSTIterator obj = new BSTIterator(root);
 * int param_1 = obj.Next();
 * bool param_2 = obj.HasNext();
 */

//Just one stack to save space and time

public class BSTIterator {
    Stack<TreeNode> stack = new Stack<TreeNode> ();
    public BSTIterator (TreeNode root) {
        IteratorInorderTree (root);
    }

    void IteratorInorderTree (TreeNode root) {
        while (root != null) {
            stack.Push (root);
            root = root.left;
        }
    }

    /** @return the next smallest number */
    public int Next () {
        if (HasNext ()) {
            var node = stack.Pop ();
            IteratorInorderTree (node.right);
            return node.val;
        }
        return -1;
    }

    /** @return whether we have a next smallest number */
    public bool HasNext () {
        return stack.Count > 0;
    }
}