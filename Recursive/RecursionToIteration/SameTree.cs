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
    // Conclusion:
    // Recursion's advantage:
    // Avoid StackOverflow;
    // Reduce additional memory consumption, i.e. the recursion's additional cost of function calls, and duplicate calculation if without memorization
    // Disadvantage:
    // Iteration is not intuitive. It's easy and intuitive to achieve recursion.

    // Time Complexity: O(N);
    // Space Complexity: O(log(N)) best in completely balanced tree
    //                   O(N) worst in completely unbalanced tree.
    //Recursion
    public bool IsSameTree (TreeNode p, TreeNode q) {
        if (p == null && q == null) return true;
        if (p == null || q == null) return false;
        if (p.val != q.val) return false;

        return IsSameTree (p.left, q.left) && IsSameTree (p.right, q.right);
    }

    //Iterative Solution;
    public bool IsSameTree (TreeNode p, TreeNode q) {
        var stackP = new Stack<TreeNode> ();
        var stackQ = new Stack<TreeNode> ();
        stackP.Push (p);
        stackQ.Push (q);

        while (stackP.Count > 0 && stackQ.Count > 0) {
            var nodeP = stackP.Pop ();
            var nodeQ = stackQ.Pop ();
            if (nodeP == null && nodeQ == null) continue;
            if (nodeP == null || nodeQ == null) return false;
            if (nodeP.val != nodeQ.val) return false;

            if (nodeP.left != null)
                stackP.Push (nodeP.left);
            stackP.Push (nodeP.right);
            stackQ.Push (nodeQ.left);
            stackQ.Push (nodeQ.right);
        }

        if (stackP.Count > 0 || stackQ.Count > 0) return false;

        return true;
    }

    //other  recursive version
    public bool IsSameTree (TreeNode p, TreeNode q) {
        if (p == null && q == null) return true;
        if (!IsSame (p, q)) return false;

        var stackP = new Stack<TreeNode> ();
        var stackQ = new Stack<TreeNode> ();
        stackP.Push (p);
        stackQ.Push (q);

        while (stackP.Count > 0 && stackQ.Count > 0) {
            var nodeP = stackP.Pop ();
            var nodeQ = stackQ.Pop ();

            if (!IsSame (nodeP, nodeQ)) return false;
            if (nodeP == null) continue;

            if (!IsSameChild (stackP, stackQ, nodeP.left, nodeQ.left)) return false;
            if (!IsSameChild (stackP, stackQ, nodeP.right, nodeQ.right)) return false;

        }

        return true;
    }

    private bool IsSameChild (Stack<TreeNode> stackP, Stack<TreeNode> stackQ, TreeNode p, TreeNode q) {
        if (!IsSame (p, q)) return false;

        if (p != null) {
            stackP.Push (p);
            stackQ.Push (q);
        }
        return true;
    }

    bool IsSame (TreeNode p, TreeNode q) {
        if (p == null && q == null) return true;
        if (p == null || q == null) return false;
        return p.val == q.val;
    }
}