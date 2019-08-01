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
    int pIndex;
    public TreeNode BuildTree (int[] inorder, int[] postorder) {
        int pLength = postorder.Length;
        int iLength = inorder.Length;
        if (pLength == 0 || iLength == 0 || pLength != iLength) return null;

        pIndex = pLength - 1;
        return HelperBuildTree (inorder, postorder, 0, iLength - 1);
    }

    TreeNode HelperBuildTree (int[] inorder, int[] postorder, int start, int end) {
        if (start > end) return null;
        //Attention!!!! the sequence ~~~ pIndex should be subtract
        // if (start == end) return new TreeNode (inorder[start]);

        var node = new TreeNode (postorder[pIndex]);
        int iIndex = SearchIndex (inorder, start, end, postorder[pIndex]);
        pIndex--;
        if (start == end) return node;

        node.right = HelperBuildTree (inorder, postorder, iIndex + 1, end);
        node.left = HelperBuildTree (inorder, postorder, start, iIndex - 1);

        return node;

    }

    int SearchIndex (int[] order, int start, int end, int val) {
        int i = 0;
        for (i = start; i <= end; i++) {
            if (val == order[i]) break;
        }
        return i;
    }
}