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
    //it's not relvant to the last kth LargetsElement;
    public int KthSmallest (TreeNode root, int k) {
        int val = 0;
        Inorder (root, k, ref val);
        return val;
    }

    int Inorder (TreeNode root, int k, ref int val) {
        if (root == null) return 0;

        int count = Inorder (root.left, k, ref val);
        if (++count == k) { val = root.val; return count; }
        return count + Inorder (root.right, k - count, ref val);
    }
}