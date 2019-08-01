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
    public IList<int> PreorderTraversal (TreeNode root) {
        // recursive method with list as returning value:
        IList<int> rlist = Preorder (root);
        return rlist;

        // recursive method with helper method to have a list as paramater
        //Don't need to instantiate a new list at each recursive call
        List<int> list = new List<int> ();
        IList<int> rhList = preHelper (root, list);
        return rhList;
    }

    List<int> Preorder (TreeNode root) {
        var head = new List<int> ();

        if (root == null) return head;

        head.Add (root.val);
        head.AddRange (Preorder (root.left));
        head.AddRange (Preorder (root.right));

        return head;
    }

    void preHelper (TreeNode root, List<int> list) {
        if (root == null) return;

        list.Add (root);
        preHelper (root.left, list);
        preHelper (root.right, list);
    }

    // Iterative with stack
    public IList<int> PreorderTraversal (TreeNode root) {

        var list = new List<int> ();
        var stack = new Stack<TreeNode> ();
        stack.Push (root);
        if (root == null) return list;

        while (stack.Count > 0) {
            var node = stack.Pop ();
            list.Add (node.val);

            if (node.right != null)
                stack.Push (node.right);
            if (node.left != null)
                stack.Push (node.left);
        }

        // IList<int> rList = list;
        return list;
    }

}