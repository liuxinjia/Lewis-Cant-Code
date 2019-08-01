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
    public TreeNode BuildTree (int[] preorder, int[] inorder) {
        int pLength = preorder.Length;
        int iLength = inorder.Length;
        if (pLength == 0 || iLength == 0 || pLength != iLength) return null;

        pIndex = 0;
        return HelperBuildTree (preorder, inorder, 0, iLength - 1);
    }

    TreeNode HelperBuildTree (int[] preorder, int[] inorder, int start, int end) {
        if (start > end) return null;

        //Attention!!!! the sequence ~~~ pIndex should be subtract
        // if (start == end) return new TreeNode (inorder[start]);

        var node = new TreeNode (preorder[pIndex]);
        int iIndex = SearchIndex (inorder, start, end, preorder[pIndex]);
        pIndex++;
        if (start == end) return node;

        node.left = HelperBuildTree (preorder, inorder, start, iIndex - 1);
        node.right = HelperBuildTree (preorder, inorder, iIndex + 1, end);

        return node;
    }

    int SearchIndex (int[] order, int start, int end, int val) {
        int i = 0;
        for (i = start; i <= end; i++) {
            if (order[i] == val) {
                break;
            }
        }
        return i;
    }

}